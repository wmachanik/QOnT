// #debug #define _DEBUG
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;
using System.Configuration; 

/*    PageLogic
Customer Reminder:

Create class of items to be displayed

First the recoccuring table 
1. For each customerid and itemtypeid in the ReoccuringTbl set the at least DateLastDone to the date the Item of of the servicetype was supplied

FOR EACH CustomerID and ItemRequired found update the Date to LastDatePerItem if the Date is < LastDatePerItem

2. For each enabled record in the ReoccuringTbl:

if the LastDatePerItem + DateAdd(ReocurranceType) < NextCityDeliveryDate then add them into the temporary reminder table, but set the value in the tempoary reminder table to remember that it is a reoccuring and an auto fulfil.

3. Then for all the other clients not in the temp table, and have not received reminders today (see below) and whose nextrequireddate for each serviceitem is < NextCityDeliveryDate add to temp table

4. Dispay form and templ table

5. Wait for user to enter body header then click send.

On send for each client:

Send email explaining why they are getting they email and wether they are a reocuring client, auto fulfill client or etc. Explain bulk roasting and top up roasting. Add a link to the bottom of the email to allowing us to add the order by clicking the link, or delay.

- if autofulful add to order table,and add comment append comment "reoccuring" if also a reoccuring client


For each client that receives a reminder:
1.  add last remidner date to the customer table, to make sure we do not resend
2. Increment the reminder count.
3. for all reminder counts >= 5 set the next reminder date to today + 14 days. 
   */


namespace QOnT.Pages
{
  public partial class SendCoffeeCheckup : System.Web.UI.Page
  {
    const string CONST_SESSIONVAR_EXISTINGORDERS = "ExistingOrdersData";
    const string CONST_PREPDATEMERGE = "[#PREPDATE#]";
    const string CONST_DELIVERYDATE = "[#DELIVERYDATE#]";
    const int CONST_FORCEREMINDERDELAYCOUNT = 4;
    const int CONST_MAXREMINDERS = 7;

    const string CONST_SUMMARYTABLEDEF = "<table border='0' style='border: 1px solid'>";
    const string CONST_SUMMARYTABLEHEADER = "<thead><tr><td style='background-color: #EFFFE9; font-weight: bold'>{0}</td><td style='background-color: #EFFFE9;' colspan='2'>{1}</td></thead>";
    const string CONST_SUMMARYTABLEBODYSTART = "<tbody>";
    const string CONST_SUMMARYTABLEBODYROWSTART = "<tr>";
    const string CONST_SUMMARYTABLEBODYROWEND = "</tr>";
    const string CONST_SUMMARYTABLEBODYEND = "</tbody></table>";
    const string CONST_SUMMARYTABLECELL1 = "<td style='background-color: #E9FFE0'>{1}</td>";
    const string CONST_SUMMARYTABLECELL2 = "<td style='background-color: #E9FFE0'>{2}</td>";
    const string CONST_SUMMARYTABLECELLBOLD = "<td style='background-color: #E9FFE0; color: #593020; font-weight: bold'>{0}</td>";
    const string CONST_SUMMARYTABLECELL2COL = "<td style='background-color: #E9FFE0; text-align:center' colspan='2'>{1}</td>";
    const string CONST_SUMMARYTABLECELL3COL = "<td style='background-color: #E9FFE0; text-align:center' colspan='3'>{0}</td>";

    const string CONST_SUMMARYTABLBODYROW = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLECELLBOLD
                                          + CONST_SUMMARYTABLECELL1 + CONST_SUMMARYTABLECELL2 + CONST_SUMMARYTABLEBODYROWEND;
    const string CONST_SUMMARYTABLBODYROW2COL = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLECELLBOLD 
                                          + CONST_SUMMARYTABLECELL2COL + CONST_SUMMARYTABLEBODYROWEND;
    const string CONST_SUMMARYTABLBODYROW3COL = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLECELL3COL + CONST_SUMMARYTABLEBODYROWEND;

    const string CONST_SUMMARYTABLEALTCELL1 = "<td style='background-color: #CFEFC9'>{1}</td>";
    const string CONST_SUMMARYTABLEALTCELL2 = "<td style='background-color: #CFEFC9'>{2}</td>";
    const string CONST_SUMMARYTABLEALTCELLBOLD = "<td style='background-color: #CFEFC9; color: #694030; font-weight: bold'>{0}</td>";
    const string CONST_SUMMARYTABLEALTCELL2COL = "<td style='background-color: #CFEFC9; color: #694030; text-align:center' colspan='2'>{1}</td>";
    const string CONST_SUMMARYTABLEALTCELL3COL = "<td style='background-color: #CFEFC9; color: #694030; text-align:center' colspan='3'>{0}</td>";
    const string CONST_SUMMARYTABLBODYALTROW = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLEALTCELLBOLD
                                          + CONST_SUMMARYTABLEALTCELL1 + CONST_SUMMARYTABLEALTCELL2 + CONST_SUMMARYTABLEBODYROWEND;
    const string CONST_SUMMARYTABLBODYALTROW2COL = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLEALTCELLBOLD
                                          + CONST_SUMMARYTABLEALTCELL2COL + CONST_SUMMARYTABLEBODYROWEND;
    const string CONST_SUMMARYTABLBODYALTROW3COL = CONST_SUMMARYTABLEBODYROWSTART + CONST_SUMMARYTABLEALTCELL3COL + CONST_SUMMARYTABLEBODYROWEND;

    private List<ContactToRemindWithItems> GetReocurringContacts()
    {
      // General VAR definitions
      List<ContactToRemindWithItems > _ContactsToRemind = new List<ContactToRemindWithItems>();
      TrackerTools _TrackerTools = new classes.TrackerTools();     // . from TrackerDotNet.classes.
      DateTime _dtTemp = DateTime.MinValue;
      DateTime _dtNextRoast = DateTime.MinValue;

      // get the syustem minimum date for if we are closed
      SysDataTbl _SysData = new SysDataTbl();
      DateTime _MinDate = _SysData.GetMinReminderDate();

      // 1. 
      // make sure the dates in the Reoccuring table match the last delivery date
      ReoccuringOrderDAL _ReoccuringOrderDAL = new ReoccuringOrderDAL();
      // Need to add this to the classs
      if (!_ReoccuringOrderDAL.SetReoccuringItemsLastDate())
      {
        showMessageBox _smb = new showMessageBox(this.Page, "Set Reoccuring Item Last Date", "Could not set the re-occuring last date");
        return _ContactsToRemind;
      }
      // if we get here we could set the last date  ??????????????

      /* 2. For each enabled record in the ReoccuringTbl:
       *   if the LastDatePerItem + DateAdd(ReocurranceType) < NextCityDeliveryDate 
       *     then add them into the temporary reminder table, but set the value in the tempoary reminder table to remember that it is a reoccuring and an auto fulfil.
       */

      // now make sure that only records the are enbabled and need to be added from Reoccuring Are added 
      List<ReoccuringOrderExtData> _ReoccuringOrders = _ReoccuringOrderDAL.GetAll(ReoccuringOrderDAL.CONST_ENABLEDONLY, "CustomersTbl.CustomerID" );   // -1 is all re
      ///////////// perhaps we should return all details in the above query?
      for (int i = 0; i < _ReoccuringOrders.Count; i++)
      {
        switch (_ReoccuringOrders[i].ReoccuranceTypeID)
        {
          case ReoccuranceTypeTbl.CONST_WEEKTYPEID:
            _ReoccuringOrders[i].NextDateRequired = _ReoccuringOrders[i].DateLastDone.AddDays(_ReoccuringOrders[i].ReoccuranceValue * 7).Date;
            break;
          case ReoccuranceTypeTbl.CONST_DAYOFMONTHID:
            _ReoccuringOrders[i].NextDateRequired = _ReoccuringOrders[i].DateLastDone.AddMonths(1).Date;
            _ReoccuringOrders[i].NextDateRequired = new DateTime(_ReoccuringOrders[i].NextDateRequired.Year, _ReoccuringOrders[i].NextDateRequired.Month, _ReoccuringOrders[i].ReoccuranceValue).Date;
            break;
          default:
            break;   // not a type we know so exit
        }
        // disable the order if this is the last time
        if ((_ReoccuringOrders[i].RequireUntilDate > TrackerTools.STATIC_TrackerMinDate) && (_ReoccuringOrders[i].NextDateRequired > _ReoccuringOrders[i].RequireUntilDate))
          _ReoccuringOrders[i].Enabled = false;

        _dtNextRoast = _TrackerTools.GetNextRoastDateByCustomerID(_ReoccuringOrders[i].CustomerID, ref _dtTemp);
        // check if the the Next roast date is less than the system Minimum date
        if (_dtNextRoast < _MinDate) _dtNextRoast = _MinDate;
        // if DateNext < NextDeliveryDate for client the add to list
        if (_ReoccuringOrders[i].NextDateRequired <= _dtNextRoast)
        {
          // we have a winner we need to add this item but first check if there is an order for this item type.
          OrderCheck _orderCheck = new OrderCheck();
          List<OrderCheckData> _OrdersToCheck = _orderCheck.GetSimilarItemInOrders(_ReoccuringOrders[i].CustomerID, _ReoccuringOrders[i].ItemRequiredID,
                                                                                           DateTimeExtensions.GetFirstDayOfWeek(_dtNextRoast),
                                                                                           DateTimeExtensions.GetLastDayOfWeek(_dtNextRoast));
          if (_OrdersToCheck == null)
          {
            // there are no orders that have the same item type

            ItemContactRequires _ItemRequired = new ItemContactRequires();

            _ItemRequired.CustomerID = _ReoccuringOrders[i].CustomerID;
            _ItemRequired.AutoFulfill = false;      // it is a reoccuring order not auto ff
            _ItemRequired.ReoccurID = _ReoccuringOrders[i].ReoccuringOrderID;
            _ItemRequired.ReoccurOrder = true;     // remember it is from reoccuring
            _ItemRequired.ItemID = _ReoccuringOrders[i].ItemRequiredID;
            _ItemRequired.ItemQty = _ReoccuringOrders[i].QtyRequired;
            _ItemRequired.ItemPackagID = _ReoccuringOrders[i].PackagingID;

            // check if the customer exists
            if (!_ContactsToRemind.Exists(x => x.CustomerID == _ItemRequired.CustomerID))
            {
              ContactToRemindWithItems _ContactToRemind = new ContactToRemindWithItems();
              _ContactToRemind = _ContactToRemind.GetCustomerDetails(_ItemRequired.CustomerID);
              _ContactToRemind.ItemsContactRequires.Add(_ItemRequired);
              _ContactsToRemind.Add(_ContactToRemind);
            }
            else
            {
              int _ContactIdx = _ContactsToRemind.FindIndex(x => x.CustomerID == _ItemRequired.CustomerID);
              _ContactsToRemind[_ContactIdx].ItemsContactRequires.Add(_ItemRequired);
            }
          }
          else
          {
            List<OrderCheckData> _OrderCheckData = null;
            // store that and order has identified
            if (Session[CONST_SESSIONVAR_EXISTINGORDERS] == null)
              _OrderCheckData = new List<OrderCheckData>();
            else
              _OrderCheckData = (List<OrderCheckData>)Session[CONST_SESSIONVAR_EXISTINGORDERS];

            for (int j = 0; j < _OrdersToCheck.Count; j++)
            {
              _OrderCheckData.Add(_OrdersToCheck[j]);
            }

//            this.gvReminderOfOrdersPlaced.DataSource = _OrderCheckData;
//            this.gvReminderOfOrdersPlaced.DataBind();
//            Session[CONST_SESSIONVAR_EXISTINGORDERS] = _OrderCheckData;
          }

        } /// we found an item in reoccuring that we need to send a reminder too
      }

      return _ContactsToRemind;
    }

    private void AddAllContactsToRemind(ref List<ContactToRemindWithItems> pContactsToRemind) //, ref List<string> strs, ref List<DateTime> dts)
    {
//      strs.Add("add all the rest of the clients");
//      dts.Add(DateTime.Now);
      /* now add all the rest of the clients
      /// 3. Then for all the other clies not in the temp table, and have not received reminders today (see below) 
      /// and whose nextrequireddate for each serviceitem is < NextCityDeliveryDate add to temp table
      /// check if item type is the same and also
      */
      ContactsThatMayNeedNextWeek _ContactsThatMayNeedNextWeek = new control.ContactsThatMayNeedNextWeek();

//      strs.Add("GetContactsThatMayNeedNextWeek");
//      dts.Add(DateTime.Now);
      List<ContactsThayMayNeedData> _ContactsThayMayNeed = _ContactsThatMayNeedNextWeek.GetContactsThatMayNeedNextWeek();

//      List<ContactToRemind> _ContactsToRemind = new List<ContactToRemind>();
      CustomerTrackedServiceItems _CustomterTrackerItem = new CustomerTrackedServiceItems();

//      strs.Add("add them including a check for if they need other items: " + _ContactsThayMayNeedData.Count.ToString());
//      dts.Add(DateTime.Now);
      for (int i = 0; i < _ContactsThayMayNeed.Count; i++)
			{
        ///// now add them including a check for if they need other items.
        List<CustomerTrackedServiceItems.CustomerTrackedServiceItemsData> _ThisCustItems = _CustomterTrackerItem.GetAllByCustomerTypeID(_ContactsThayMayNeed[i].CustomerData.CustomerTypeID);

        ContactToRemindWithItems _ContactToRemind = new ContactToRemindWithItems();
        
        // copy all data across
        _ContactToRemind.CustomerID = _ContactsThayMayNeed[i].CustomerData.CustomerID;
        _ContactToRemind.CompanyName = _ContactsThayMayNeed[i].CustomerData.CompanyName;
        _ContactToRemind.ContactFirstName = _ContactsThayMayNeed[i].CustomerData.ContactFirstName;
        _ContactToRemind.ContactAltFirstName = _ContactsThayMayNeed[i].CustomerData.ContactAltFirstName;
        _ContactToRemind.EmailAddress = _ContactsThayMayNeed[i].CustomerData.EmailAddress;
        _ContactToRemind.AltEmailAddress = _ContactsThayMayNeed[i].CustomerData.AltEmailAddress;
        _ContactToRemind.CityID = _ContactsThayMayNeed[i].CustomerData.City;
        _ContactToRemind.CustomerTypeID = _ContactsThayMayNeed[i].CustomerData.CustomerTypeID;
        _ContactToRemind.enabled = _ContactsThayMayNeed[i].CustomerData.enabled;
        _ContactToRemind.EquipTypeID = _ContactsThayMayNeed[i].CustomerData.EquipType;
        _ContactToRemind.TypicallySecToo = _ContactsThayMayNeed[i].CustomerData.TypicallySecToo;
        _ContactToRemind.PreferedAgentID = _ContactsThayMayNeed[i].CustomerData.PreferedAgent;
        _ContactToRemind.SalesAgentID = _ContactsThayMayNeed[i].CustomerData.SalesAgentID;
        _ContactToRemind.UsesFilter = _ContactsThayMayNeed[i].CustomerData.UsesFilter;
        _ContactToRemind.enabled = _ContactsThayMayNeed[i].CustomerData.enabled;
        _ContactToRemind.AlwaysSendChkUp = _ContactsThayMayNeed[i].CustomerData.AlwaysSendChkUp;
        _ContactToRemind.RequiresPurchOrder = _ContactsThayMayNeed[i].RequiresPurchOrder;
        _ContactToRemind.ReminderCount = _ContactsThayMayNeed[i].CustomerData.ReminderCount;
        // prep and delivery dates
        _ContactToRemind.NextPrepDate = _ContactsThayMayNeed[i].NextRoastDateByCityData.PrepDate.Date;
        _ContactToRemind.NextDeliveryDate = _ContactsThayMayNeed[i].NextRoastDateByCityData.DeliveryDate.Date;

        // usage dates
        /// ClientUsageTbl _ClientUsage = new ClientUsageTbl();

        // _ClientUsage.GetUsageData(_ContactToRemind.CustomerID);
        _ContactToRemind.NextCoffee = _ContactsThayMayNeed[i].ClientUsageData.NextCoffeeBy.Date;
        _ContactToRemind.NextClean = _ContactsThayMayNeed[i].ClientUsageData.NextCleanOn.Date;
        _ContactToRemind.NextDescal = _ContactsThayMayNeed[i].ClientUsageData.NextDescaleEst.Date;
        _ContactToRemind.NextFilter = _ContactsThayMayNeed[i].ClientUsageData.NextFilterEst.Date;
        _ContactToRemind.NextService = _ContactsThayMayNeed[i].ClientUsageData.NextServiceEst.Date;
        /// now add the items the customer needs.
        DateTime _dtNextRequired = DateTime.MaxValue;
        ItemUsageTbl _ItemUsage = new ItemUsageTbl();
        List<ItemUsageTbl> _LastItemsOrder = null;
        for (int j = 0; j < _ThisCustItems.Count; j++)
        {

          switch (_ThisCustItems[j].ServiceTypeID)
          {
            case TrackerTools.CONST_SERVTYPECOFFEE:
              _dtNextRequired = _ContactToRemind.NextCoffee;
              break;
            case TrackerTools.CONST_SERVTYPECLEAN:
              _dtNextRequired = _ContactToRemind.NextClean;
              break;
            case TrackerTools.CONST_SERVTYPEDESCALE:
              _dtNextRequired = _ContactToRemind.NextDescal;
              break;
            case TrackerTools.CONST_SERVTYPEFILTER:
              _dtNextRequired = _ContactToRemind.NextFilter;
              break;
            case TrackerTools.CONST_SERVTYPESERVICE:
              _dtNextRequired = _ContactToRemind.NextService;
              break;
            default:
              _dtNextRequired = DateTime.MaxValue;
              break;
          }
          // do a tweak so that if they have had this thing for over year they do not get reminded and then if the will need?
          if ((_dtNextRequired  > DateTime.Now.AddYears(-1)) && (_dtNextRequired <= _ContactsThayMayNeed[i].NextRoastDateByCityData.DeliveryDate))
          {
            // add items they are marked as needing
            _LastItemsOrder = _ItemUsage.GetLastItemsUsed(_ContactsThayMayNeed[i].CustomerData.CustomerID, _ThisCustItems[j].ServiceTypeID);
            // they are done for needing this item
            for (int k = 0; k < _LastItemsOrder.Count; k++)
            {
              // only add an item if they need that item
              ItemContactRequires _ItemRequired = new ItemContactRequires();

              _ItemRequired.CustomerID = _ContactsThayMayNeed[i].CustomerData.CustomerID;
              _ItemRequired.AutoFulfill = _ContactsThayMayNeed[i].CustomerData.autofulfill;      // we mark it as an autofulfill so it gets added
              _ItemRequired.ReoccurID = 0;   /// false it is not from reoccuring
              _ItemRequired.ItemID = _LastItemsOrder[k].ItemProvidedID;
              _ItemRequired.ItemQty = _LastItemsOrder[k].AmountProvided;
              _ItemRequired.ItemPackagID = _LastItemsOrder[k].PackagingID;

              // check if the customer exists
              if (!pContactsToRemind.Exists(x => x.CustomerID == _ItemRequired.CustomerID))
              {
                _ContactToRemind.ItemsContactRequires.Add(_ItemRequired);
                pContactsToRemind.Add(_ContactToRemind);
              }
              else
              {
                int _ContactIdx = pContactsToRemind.FindIndex(x => x.CustomerID == _ItemRequired.CustomerID);
                if (!pContactsToRemind[_ContactIdx].ItemsContactRequires.Exists(x => x.ItemID == _ItemRequired.ItemID))
                {
                  // only add an item if it does not exists
                  pContactsToRemind[_ContactIdx].ItemsContactRequires.Add(_ItemRequired);
                }
              }
            }
          }  // if they need this before the next delivery date.
//          _LastItemsOrder = null;
        }
			}

    }
    private void SetListOfContactsToSendReminderTo() // ref List<string> strs, ref List<DateTime> dts)
    {
//      strs.Add("first get a list of contact that are on the reoccuringlist");
//      dts.Add(DateTime.Now);
      // first get a list of contact that are on the reoccuringlist
      List<ContactToRemindWithItems> _ContactsToRemind = GetReocurringContacts();

//      strs.Add("add the clients to that list that probably need in the next sechduled delivery period");
//      dts.Add(DateTime.Now);
      // now add the clients to that list that probably need in the next sechduled delivery period
      AddAllContactsToRemind(ref _ContactsToRemind);  //, ref strs, ref dts);

      _ContactsToRemind.Sort((a, b) => String.Compare(a.CompanyName, b.CompanyName)); // need to sort by name

//      strs.Add("write data to the table so it can be displayed");
//      dts.Add(DateTime.Now);
      /// write data to the table so it can be displayed
      TempCoffeeCheckup _TempCoffeeCheckup = new TempCoffeeCheckup();
      bool _CouldDelete = (_TempCoffeeCheckup.DeleteAllContactRecords()) && (_TempCoffeeCheckup.DeleteAllContactItems());
      if (!_CouldDelete)
      {
        showMessageBox _smb = new showMessageBox(this.Page,"Old Temp Table delete","Error deleting old temp tables");
      }
      else
      {
        ItemTypeTbl _ITT = new ItemTypeTbl();
        List<int> _OnlyCoffeeItems = _ITT.GetAllItemIDsofServiceType(TrackerTools.CONST_SERVTYPECOFFEE);
        // also the group items - since they are coffee too.
        _OnlyCoffeeItems.AddRange(_ITT.GetAllItemIDsofServiceType(TrackerTools.CONST_SERVTYPEGROUPITEM));

        bool _AllAdded = false;
        bool _DoAdd = false;
        int j = 0;
        for (int i = 0; i < _ContactsToRemind.Count; i++)
        {
          // only insert client if the contain coffee item
          _DoAdd = false;
          j = 0;
          while ((j < _ContactsToRemind[i].ItemsContactRequires.Count) && (!_DoAdd))
          {
            _DoAdd = _OnlyCoffeeItems.Contains(_ContactsToRemind[i].ItemsContactRequires[j].ItemID);
            j++;
          }
          if (_DoAdd)
          {
            _AllAdded = _TempCoffeeCheckup.InsertContacts(_ContactsToRemind[i]) || _AllAdded;

            foreach (ItemContactRequires _Item in _ContactsToRemind[i].ItemsContactRequires)  // (int j = 0; j < _ContactsToRemind[i].ItemsContactRequires.Count; j++)
              _AllAdded = _TempCoffeeCheckup.InsertContactItems(_Item) || _AllAdded;
          }
        }
        if (!_AllAdded)
        {
          showMessageBox _smb = new showMessageBox(this.Page, "Not all records added to Temp Table", "Error adding some records added to Temp Table");
        }
      }
//      gvCustomerCheckup.DataSource = _ContactsToRemind;
//      gvCustomerCheckup.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // get email data
        LoadEmailTexts();
        PrepPageData();
      }
    }
    protected void PrepPageData()
    {
      // load the data into the grid view
      TrackerDotNet.classes.TrackerTools _TTools = new TrackerTools();
      if (!_TTools.IsNextRoastDateByCityTodays())
        _TTools.SetNextRoastDateByCity();           // The next roast days have not been calcualted, calculate them and save. When customers city is found save the details

      // add all people into the grid that are to receive reminders
      SetListOfContactsToSendReminderTo(); //(ref strs, ref dts);

      upnlCustomerCheckup.Visible = true;
      upnlContactItems.Visible = true;
    }
    /// <summary>
    /// Display rotuines to dispay items in a gridview
    /// </summary>
    /// <param name="pItemID"></param>
    /// <returns></returns>
    public string GetItemDesc(int pItemID)
    {
      if (pItemID > 0)
      {
        ItemTypeTbl _ItemType = new ItemTypeTbl();
        return _ItemType.GetItemTypeDesc(pItemID);
      }
      else
        return String.Empty;
    }
    public string GetItemSKU(int pItemID)
    {
      if (pItemID > 0)
      {
        ItemTypeTbl _ItemType = new ItemTypeTbl();
        return _ItemType.GetItemTypeSKU(pItemID);
      }
      else
        return String.Empty;
    }
    public string GetCityName(int pCityID)
    {
      if (pCityID > 0)
      {
        CityTblDAL _City = new CityTblDAL();
        return _City.GetCityName(pCityID);
      }
      else
        return String.Empty;
    }
    public string GetPackagingDesc(int pPackagingID)
    {
      if (pPackagingID > 0)
      {
        PackagingTbl _Packaging = new PackagingTbl();
        return _Packaging.GetPackagingDesc(pPackagingID);
      }
      else
        return String.Empty;
    }
    public string GetItemUoM(int pItemID)
    {
      if (pItemID > 0)
      {
        ItemTypeTbl _ItemType = new ItemTypeTbl();
        return _ItemType.GetItemUnitOfMeasure(pItemID);
      }
      else
        return String.Empty;
    }
    string AddUnitsToQty(int pItemTypeID, double pQty)
    {
      string _UoMString = GetItemUoM(pItemTypeID);

      if (String.IsNullOrEmpty(_UoMString))
        return pQty.ToString();
      else
      {
        double _Qty = Convert.ToDouble(pQty);
        return pQty.ToString() + " " + ((_Qty == 1) ? _UoMString : _UoMString + "s");
      }
    }

    string ReplaceMergeTextWithDate(string pString, string pMergeFiled, DateTime pMergeDate)
    {
      while (pString.Contains(pMergeFiled))
      {
        pString = pString.Replace(pMergeFiled, String.Format("{0:dddd, dd MMM}", pMergeDate));
      }

      return pString;
    }
    string PersonalizeBodyText(ContactToRemindWithItems pContact)
    {

      /*0. Replace [#PREDATE#] with the clients next prep date, and [#DELIVERYDATE#] delivery Date */
      if (pContact.NextPrepDate < DateTime.Now.Date)     /// this should never happend
        pContact.NextPrepDate = DateTime.Now.AddDays(1).Date;
      string _BodyText = ReplaceMergeTextWithDate(tbxEmailBody.Text, CONST_PREPDATEMERGE, pContact.NextPrepDate);

      if (pContact.NextDeliveryDate < DateTime.Now.Date)
        pContact.NextDeliveryDate = DateTime.Now.AddDays(3).Date;
      _BodyText = ReplaceMergeTextWithDate(_BodyText, CONST_DELIVERYDATE, pContact.NextDeliveryDate);

      return _BodyText + "<br /><br />";
    }
    private string GetTheOrderType(ContactToRemindWithItems pContact)
    {
      bool _IsAutoFulfill = false, _IsReoccuring = false;

      string _OrderType = String.Empty;
      _IsAutoFulfill = pContact.ItemsContactRequires.Exists(x => x.AutoFulfill == true);
      _IsReoccuring = pContact.ItemsContactRequires.Exists(x => x.ReoccurOrder == true);
      //_IsAutoFulfill = _IsReoccuring = false;
      //for (int j = 0; j < pContact.ItemsContactRequires.Count; j++)
      //{
      //  _IsAutoFulfill = (_IsAutoFulfill) || (pContact.ItemsContactRequires[j].AutoFulfill);
      //  _IsReoccuring = (_IsReoccuring) || (pContact.ItemsContactRequires[j].ReoccurID > 0);
      //}
      if ((_IsReoccuring) && (_IsAutoFulfill))
        _OrderType = "combination of a reoccuring and autofulfill order";
      else if (_IsReoccuring)
        _OrderType = "reoccuring order";
      else if (_IsAutoFulfill)
        _OrderType = "automatically fulfilled order";
      
      return _OrderType;
    }
    string AddLastOrderTableRow(ItemContactRequires pItemContactRequires, bool bAltRow)
    {
      string _TableRow = bAltRow ? CONST_SUMMARYTABLEALTCELLBOLD : CONST_SUMMARYTABLECELLBOLD;
      string _QtyDesc = AddUnitsToQty(pItemContactRequires.ItemID, pItemContactRequires.ItemQty);

      if (pItemContactRequires.ItemPackagID > 0)
      {
        if (bAltRow)  
          _TableRow += CONST_SUMMARYTABLEALTCELL1 + CONST_SUMMARYTABLEALTCELL2;
        else
          _TableRow += CONST_SUMMARYTABLECELL1 + CONST_SUMMARYTABLECELL2;

        _TableRow = String.Format(_TableRow, GetItemDesc(pItemContactRequires.ItemID), _QtyDesc,
                                             GetPackagingDesc(pItemContactRequires.ItemPackagID));
      }
      else
      {
        if (bAltRow)
          _TableRow += CONST_SUMMARYTABLEALTCELL2COL;
        else
          _TableRow += CONST_SUMMARYTABLECELL2COL;

        _TableRow = String.Format(_TableRow, GetItemDesc(pItemContactRequires.ItemID), _QtyDesc);
      }
      
      return _TableRow;      
    }
    /// <summary>
    /// Returns true of the character passed is a vowel
    /// </summary>
    /// <param name="pChar">the character</param>
    /// <returns>is it a vowel</returns>
    bool IsVowel(char pChar)
    {
      string _Vowels = "aeiou";
      pChar = Char.ToLower(pChar);

      return (_Vowels.Contains(pChar));
    }
    string UsageSummaryTableHeader(ContactToRemindWithItems pContact)
    {
      string _TableHeader = CONST_SUMMARYTABLEDEF + CONST_SUMMARYTABLEBODYSTART;

      return _TableHeader;
    }
#if _DEBUG
    bool SendReminder(SendCheckEmailTextsData pEmailTextData, ContactToRemindWithItems pContact, string pOrderType, int pReminderCount)
#else
    bool SendReminder(SendCheckEmailTextsData pEmailTextData, ContactToRemindWithItems pContact, string pOrderType)
#endif
    {
      const string CONST_SERVERURL = "http://tracker.quaffee.co.za/QonT/";
      const string CONST_HTMLBREAK = "<br /><br />";
      const string CONST_ADDORDERURL = "Internal: Order Ref <a href='" + CONST_SERVERURL + "{0}'>here</a>.";
      const string CONST_DISABLECUSTOMERURL = "If you would prefer to be disabled then click <a href='" + CONST_SERVERURL + "DisableClient.aspx?CoID={0}'>disable me</a>";

      bool _ReminderSent = false;
      bool _HasAutoFulfilElements = false, _HasReoccuringElements = false,_HadError = false;
      SentRemindersLogTbl _ReminderLog = new SentRemindersLogTbl();

      #region CreateAndPopulateUsageSummaryTable

      string _EmailBodyTable = UsageSummaryTableHeader(pContact);
      string _OrderType = (String.IsNullOrEmpty(pOrderType)) ? "a reminder only" : pOrderType;
      // send a reminder
      // build up Table
      _EmailBodyTable += String.Format(CONST_SUMMARYTABLEHEADER, "Company/Contact", pContact.CompanyName);
      _EmailBodyTable += String.Format(CONST_SUMMARYTABLBODYROW2COL, "Next estimate prep date", String.Format("{0:d MMM, ddd}",pContact.NextPrepDate));
      _EmailBodyTable += String.Format(CONST_SUMMARYTABLBODYALTROW2COL, "Next estimate dispatch date", String.Format("{0:d MMM, ddd}",pContact.NextDeliveryDate));
      _EmailBodyTable += String.Format(CONST_SUMMARYTABLBODYALTROW2COL, "Type", _OrderType);
      if (pContact.ItemsContactRequires.Count > 0)
      {
        _EmailBodyTable += String.Format(CONST_SUMMARYTABLBODYROW3COL, "<b>List of Items</b>");

        for (int j = 0; j < pContact.ItemsContactRequires.Count; j++)
        {
          _EmailBodyTable += CONST_SUMMARYTABLEBODYROWSTART + AddLastOrderTableRow(pContact.ItemsContactRequires[j], (j % 2 == 0)) + CONST_SUMMARYTABLEBODYROWEND;
        }
      }
      _EmailBodyTable += CONST_SUMMARYTABLEBODYEND;
      #endregion
      // add to orders if it is a autofulful or 
      #region AddOrderLines
      if (!String.IsNullOrWhiteSpace(pOrderType))
      {
        OrderTblData _OrderData = new OrderTblData();

        _HasAutoFulfilElements = true;
        _OrderData.CustomerId = pContact.CustomerID;
        _OrderData.OrderDate = DateTime.Now.Date;
        _OrderData.RoastDate = pContact.NextPrepDate.Date;
        _OrderData.RequiredByDate = pContact.NextDeliveryDate.Date;
        _OrderData.ToBeDeliveredBy = (pContact.PreferedAgentID < 0) ? TrackerTools.CONST_DEFAULT_DELIVERYBYID : pContact.PreferedAgentID;   // the prefered person is the delivery agent
        _OrderData.Confirmed = false;    // we have not confirmed the order, it is a reoccuring or auto order
        _OrderData.InvoiceDone = false;    // we have not confirmed the order, it is a reoccuring or auto order
        _OrderData.PurchaseOrder =  string.Empty;    // we have not confirmed the order, it is a reoccuring or auto order
        _OrderData.Notes = pOrderType;

        ReoccuringOrderDAL _Reoccur = new ReoccuringOrderDAL();   // to set the last date
        OrderTbl _OrderTbl = new OrderTbl();
        string _InsertErr = String.Empty;
        int j = 0;
        // insert each itm that is Reoccuing or auto
        while ((j < pContact.ItemsContactRequires.Count) && String.IsNullOrEmpty(_InsertErr))
        {
          _OrderData.ItemTypeID = pContact.ItemsContactRequires[j].ItemID;
          _OrderData.QuantityOrdered = pContact.ItemsContactRequires[j].ItemQty;
          _OrderData.PackagingID = pContact.ItemsContactRequires[j].ItemPackagID;
          _OrderData.PrepTypeID = pContact.ItemsContactRequires[j].ItemPrepID;
          _InsertErr = _OrderTbl.InsertNewOrderLine(_OrderData);

          // mark a reoccuring item as done with the current prep date 
          if (pContact.ItemsContactRequires[j].ReoccurOrder)
          {
            _Reoccur.SetReoccuringOrdersLastDate(pContact.NextPrepDate, pContact.ItemsContactRequires[j].ReoccurID);
            _HasReoccuringElements = true;
          }

          j++;
        }
        // add a record into the log to say it was sent
        _HadError = !String.IsNullOrEmpty(_InsertErr);
        if (_HadError)
        {
          showMessageBox _MsgBox = new showMessageBox(this.Page, "Error Inserting order for :" + pContact.CompanyName, _InsertErr);
        }
        else
        {
          pEmailTextData.Footer += CONST_HTMLBREAK+"Items added to orders.";
        }
      }
      else     /// we are not autoadding so lets append a "LastOrder Equiv to the message"
      {

        string _AddOrder = String.Format("Pages/NewOrderDetail.aspx?Z&{0}={1}" , NewOrderDetail.CONST_URL_REQUEST_CUSTOMERID, pContact.CustomerID) ;
        for (int j = 0; j < pContact.ItemsContactRequires.Count; j++)
        {
          _AddOrder += String.Format("&SKU{0}={1}&SKUQty{2}={3}", j + 1, GetItemSKU(pContact.ItemsContactRequires[j].ItemID), j + 1, 
                                                                        pContact.ItemsContactRequires[j].ItemQty);
        }

        pEmailTextData.Footer += CONST_HTMLBREAK + String.Format(CONST_ADDORDERURL, _AddOrder);

        pEmailTextData.Footer += CONST_HTMLBREAK + String.Format(CONST_DISABLECUSTOMERURL, pContact.CustomerID);

      }
#endregion

#if _DEBUG
      pContact.EmailAddress = "warren@machanik.za.net";
      pContact.AltEmailAddress = "";

#endif

      #region SendEmail

      string _FromEmail = ConfigurationManager.AppSettings[EmailCls.CONST_APPSETTING_FROMEMAILKEY];
      string _Greetings = String.Empty;

      EmailCls _Email = new EmailCls();
      _Email.SetEmailFrom(_FromEmail);
      _Email.SetEmailSubject(tbxEmailSubject.Text);

      if (pContact.EmailAddress.Contains("@"))
      {
        _Email.SetEmailTo(pContact.EmailAddress, true);
        _Greetings = String.IsNullOrWhiteSpace(pContact.ContactFirstName) ? "<p>Hi Coffee Lover,</p>" : "<p>Hi " + pContact.ContactFirstName + ",</p>";

        _Email.MsgBody = _Greetings + "<p>" + pEmailTextData.Header + "</p><p>" + pEmailTextData.Body + "</p><br />" + 
                        _EmailBodyTable + "<br /><br />" + pEmailTextData.Footer;

#if _DEBUG
        _ReminderSent = true;
        //if ((pReminderCount < 5) || (!String.IsNullOrWhiteSpace(pOrderType)))
        //  _ReminderSent = _Email.SendEmail();
        //else 
        //  _ReminderSent = true;
#else
        _ReminderSent = _Email.SendEmail();
#endif
      }
      if (pContact.AltEmailAddress.Contains("@"))
      {
        _Email.SetEmailTo(pContact.AltEmailAddress, true);
        _Greetings = String.IsNullOrWhiteSpace(pContact.ContactAltFirstName) ? "<p>Hi Coffee Lover,</p>" : "<p>Hi " + pContact.ContactAltFirstName + ",</p>";
        _Email.MsgBody = _Greetings + "<p>" + pEmailTextData.Header + "</p><p>" + pEmailTextData.Body + "</p><br />" + 
                        _EmailBodyTable + CONST_HTMLBREAK + pEmailTextData.Footer;

#if _DEBUG
        _ReminderSent = true;
#else
        _ReminderSent = (_ReminderSent) || (_Email.SendEmail());
#endif
      }
 
      #endregion

      /// cipy values across and add to database
      _ReminderLog.CustomerID = pContact.CustomerID;
      _ReminderLog.DateSentReminder = System.DateTime.Now.Date;
      _ReminderLog.NextPrepDate = pContact.NextCoffee.Date;   // use the coffee date
      _ReminderLog.ReminderSent = _ReminderSent;
      _ReminderLog.HadAutoFulfilItem = _HasAutoFulfilElements;
      _ReminderLog.HadReoccurItems = _HasReoccuringElements;

      _ReminderLog.InsertLogItem(_ReminderLog);

      return _ReminderSent;
    }

    /// <summary>
    /// Handle the send button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
      uprgSendEmail.DisplayAfter = 0;
      string _OrderType = "";
      TempCoffeeCheckup _TempCoffeeCheckup = new TempCoffeeCheckup();
      // get all the contacts that need to be reminded
      List<ContactToRemindWithItems> _ContactsToRemind = _TempCoffeeCheckup.GetAllContactAndItems();
      SendCheckEmailTextsData _EmailTextData = new SendCheckEmailTextsData();
      CustomersTbl _Customer = new CustomersTbl();
      int _RemindersSent = 0, _RemindersFailed = 0;

      for (int i = 0; i < _ContactsToRemind.Count; i++)
      {
        try
        {

          _EmailTextData.Header = tbxEmailIntro.Text;
          _EmailTextData.Footer = tbxEmailFooter.Text;
          if (String.Concat(_ContactsToRemind[i].EmailAddress, _ContactsToRemind[i].AltEmailAddress).Contains("@"))
          {
            // customer has an email address
            _ContactsToRemind[i].ReminderCount++;
            if (_ContactsToRemind[i].ReminderCount < CONST_MAXREMINDERS)
            {
              _EmailTextData.Body = PersonalizeBodyText(_ContactsToRemind[i]);
              // look through items to see if this is a reoccuring or auto order
              _OrderType = GetTheOrderType(_ContactsToRemind[i]);
              // if auto fulfil add to orders with auto add in the note
              if (String.IsNullOrWhiteSpace(_OrderType))
                _EmailTextData.Body += "We will only place an order on your request, no order has been added, this just a reminder.";
              else
              {
                _EmailTextData.Body += "This is a" + (IsVowel(_OrderType[0]) ? "n " : " ") + _OrderType + ", please respond to cancel.";
              }
              if (_ContactsToRemind[i].ReminderCount == CONST_MAXREMINDERS - 1)
                _EmailTextData.Body = String.Concat("This is your last reminder email. Next time reminders will be disabled until you order again.", _EmailTextData.Body);
              // else next reminder in week*reminder count???

#if _DEBUG
            if (SendReminder(_EmailTextData, _ContactsToRemind[i], _OrderType,_RemindersSent))
#else
              if (SendReminder(_EmailTextData, _ContactsToRemind[i], _OrderType))
#endif
                _RemindersSent++;
              else
                _RemindersFailed++;

              //--- delay reminders for those that have too many, and update reminder count for all
              if (_ContactsToRemind[i].ReminderCount >= CONST_FORCEREMINDERDELAYCOUNT)
              {
                DateTime _ForceDate = _ContactsToRemind[i].NextPrepDate.AddDays(10 * (_ContactsToRemind[i].ReminderCount - CONST_FORCEREMINDERDELAYCOUNT + 1));

                ClientUsageTbl _ClientUsage = new ClientUsageTbl();
                _ClientUsage.ForceNextCoffeeDate(_ForceDate, _ContactsToRemind[i].CustomerID);
              }
              // icrement count and set last date
              _Customer.SetSentReminderAndIncrementReminderCount(DateTime.Now.Date, _ContactsToRemind[i].CustomerID);

              ////---- check the last sent date?
            }
            else
            {
              _Customer.DisableCustomer(_ContactsToRemind[i].CustomerID);
              _RemindersFailed++;
            }
#if _DEBUG
//          if (_RemindersSent > 5) break;
#endif
          }
        }
        catch (Exception _Ex)
        {
          // just incse we have an error we normally do not have
          showMessageBox _ExMsg = new showMessageBox(this.Page, "Error sending...", _Ex.Message);
          _RemindersFailed++;
        }
      }

      showMessageBox _MsgBox = new showMessageBox(this.Page, "Reminder emails status", String.Format("Reminders processed. Sent: {0}; Failed: {1}",_RemindersSent,_RemindersFailed)); 

      string _RedirectURL = String.Format("{0}?LastSentDate={1:d}", 
                                                 ResolveUrl("~/Pages/SentRemindersSheet.aspx"), DateTime.Now.Date);
      Response.Redirect(_RedirectURL);
      PrepPageData();
    }

    protected void LoadEmailTexts()
    {
      SendCheckEmailTextsData _EmailTextData = new SendCheckEmailTextsData();
      _EmailTextData = _EmailTextData.GetTexts();

      if (_EmailTextData.SCEMTID > 0)
      {
        ltrlEmailTextID.Text = _EmailTextData.SCEMTID.ToString();
        tbxEmailIntro.Text = HttpUtility.HtmlDecode(_EmailTextData.Header);
        tbxEmailBody.Text = HttpUtility.HtmlDecode(_EmailTextData.Body);
        tbxEmailFooter.Text = _EmailTextData.Footer; //  HttpUtility.HtmlDecode(_EmailTextData.Footer);
      }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      if (!String.IsNullOrWhiteSpace(ltrlEmailTextID.Text))
      {
        SendCheckEmailTextsData _EmailTextData = new SendCheckEmailTextsData();

        _EmailTextData.Header = HttpUtility.HtmlEncode(tbxEmailIntro.Text);
        _EmailTextData.Body = HttpUtility.HtmlEncode(tbxEmailBody.Text);
        
        _EmailTextData.Footer = HttpUtility.HtmlEncode(tbxEmailFooter.Text);

        ltrlStatus.Text = _EmailTextData.UpdateTexts(_EmailTextData, Convert.ToInt32(ltrlEmailTextID.Text));
      }
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
      LoadEmailTexts();
    }

    protected void btnPrepData_Click(object sender, EventArgs e)
    {
      // load all the data
      PrepPageData();
    }

  }
}