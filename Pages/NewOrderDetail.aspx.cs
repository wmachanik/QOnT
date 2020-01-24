using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;

namespace QOnT.Pages
{

  public partial class NewOrderDetail : System.Web.UI.Page
  {
    public const string CONST_ZZNAME_DEFAULTID = "9";

//    const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_DELIVERY_DEFAULT = TrackerTools.CONST_DEFAULT_DELIVERYBYABBREVIATION;
    const string CONST_UPDATELINES = "UpdateOrderLines";
    const string CONST_LINESADDED = "OrderLinesAdded";
    const string CONST_ORDERLINEIDS = "OrderLineIDS";
    const string CONST_ORDERLINEITEMIDS = "OrderLineItemIDS";
    const string CONST_WATERFILTER = "8ClarFltr";   // white filters 
    const string CONST_BLUEWATERFILTER = "8ClarBlue";   // blue filters 
    const int CONST_ORDERIDCOL = 4;
    const int CONST_NOTEITEMTIMEID = 100;    // this is the ID in the database that refers to item type note.
    //  URL request strings
    public const string CONST_URL_REQUEST_CUSTOMERID = "CoID";
    public const string CONST_URL_REQUEST_NAME = "Name";
    public const string CONST_URL_REQUEST_COMPANYNAME = "CoName";
    public const string CONST_URL_REQUEST_EMAIL = "EMail";
    public const string CONST_URL_REQUEST_LASTORDER = "LastOrder";
    public const string CONST_URL_REQUEST_SKU1 = "SKU1";

    //private OleDbConnection OpenTrackerOleDBConnection()
    //{
    //  OleDbConnection pConn = null;
    //  string _connectionString;

    //  if (ConfigurationManager.ConnectionStrings[CONST_CONSTRING] == null ||
    //      ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString.Trim() == "")
    //  {
    //    throw new Exception("A connection string named " + CONST_CONSTRING + " with a valid connection string " +
    //                        "must exist in the <connectionStrings> configuration section for the application.");
    //  }
    //  _connectionString = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;
    //  pConn = new OleDbConnection(_connectionString);

    //  return pConn;
    //}
    protected void SetContactByID(string pCoNameID)
    {
      // Set the Contact Name to string passed
      // check for null string 
      if (!string.IsNullOrEmpty(pCoNameID))
      {
        // now find it
        if (ddlContacts.Items.FindByValue(pCoNameID) != null)
        {
          ddlContacts.SelectedValue = pCoNameID;

          TrackerTools tt = new TrackerTools();
          TrackerTools.ContactPreferedItems _ContactPreferedItems = tt.RetrieveCustomerPrefs(Convert.ToInt64(pCoNameID));
          if (ddlToBeDeliveredBy.Items.FindByValue(_ContactPreferedItems.DeliveryByID.ToString()) != null)
            ddlToBeDeliveredBy.SelectedValue = _ContactPreferedItems.DeliveryByID.ToString();
        }
        else
        { // did not find company to set it to generic
          ddlContacts.SelectedValue = CONST_ZZNAME_DEFAULTID;
          tbxNotes.Text += "ID note found: " + pCoNameID + ": ";
        }
      }
    }

    protected void SetContactValue(string pCoName, string pName, string pEmail)
    {
      // Set the Contact Name to string passed
      int i = 0;
      // force null string 
      if (pCoName == null) pCoName = "";
      if (pName == null) pName = "";
      // now find it but make it ignore case
      while ((i < ddlContacts.Items.Count) && (!pCoName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
        i++;

      if ((i < ddlContacts.Items.Count) && (pCoName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
      {
        ddlContacts.SelectedValue = ddlContacts.Items[i].Value;
      }
      else
      {
        pCoName += "_" + pCoName; // see if they are disabled
        while ((i < ddlContacts.Items.Count) && (!pCoName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
          i++;

        if ((i < ddlContacts.Items.Count) && (pCoName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
        {
          ddlContacts.SelectedValue = ddlContacts.Items[i].Value;
        }
        else
        {
          // did not find co, now look for name
          if (pCoName != pName)
          {
            i = 0;
            while ((i < ddlContacts.Items.Count) && (!pName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
              i++;

            if ((i < ddlContacts.Items.Count) && (pName.Equals(ddlContacts.Items[i].Text, StringComparison.OrdinalIgnoreCase)))
              ddlContacts.SelectedValue = ddlContacts.Items[i].Value;
            else
            { // did not find company check email

              CustomersTbl _CustomerTbl = new CustomersTbl();
              List<CustomersTbl> _Customers = _CustomerTbl.GetAllCustomerWithEmailLIKE(pEmail);

              if (_Customers.Count > 0)
              {
                SetContactByID(_Customers[0].CustomerID.ToString());
              }
              else
              {
                // did not fine the name to set it to generic
                ddlContacts.SelectedValue = CONST_ZZNAME_DEFAULTID;
                if (String.IsNullOrEmpty(pCoName))
                  tbxNotes.Text += pName + ": ";
                else
                  tbxNotes.Text += pCoName + ", " + pName + ": ";

                if (!String.IsNullOrEmpty(pEmail))
                  tbxNotes.Text += " [#" + pEmail + "#]";
              }
            }
          }
          else
          { // did not find company to set it to generic
            ddlContacts.SelectedValue = CONST_ZZNAME_DEFAULTID;
            tbxNotes.Text += pCoName + ": ";
          }
        }
      }
      if ((ddlContacts.SelectedIndex > 0) && (ddlContacts.SelectedValue != CONST_ZZNAME_DEFAULTID))
      {
        TrackerTools tt = new TrackerTools();
        TrackerTools.ContactPreferedItems _ContactPreferedItems = tt.RetrieveCustomerPrefs(Convert.ToInt64(ddlContacts.SelectedValue));
        if (ddlToBeDeliveredBy.Items.FindByValue(_ContactPreferedItems.DeliveryByID.ToString()) != null)
          ddlToBeDeliveredBy.SelectedValue = _ContactPreferedItems.DeliveryByID.ToString();
      }
    }

    protected void SetButtonState(bool pState)
    {
      btnCheckDetails.Enabled = pState;
      updtButtonPanel.Update();
    }
    /// <summary>
    /// Add the Last ORder for this client
    /// </summary>
    protected bool AddLastOrder() { return AddLastOrder(false); }
    protected bool AddLastOrder(bool pSetDates)
    {
      //string _LastOrderSQL, _sFrom, _sWhere;
      //string _sCustID;
      bool _LastOrder = (Session[CONST_LINESADDED] != null) ? (bool)Session[CONST_LINESADDED] : false;

      if (ddlContacts.SelectedValue != null)
      {
        SetUpdateBools();
        // retrieve the last order items from the database.
        long _CustID = Convert.ToInt64(ddlContacts.SelectedValue);
        // if they want us to set the delivery and roast dates
        if (pSetDates)
        {
          SetPrepAndDeliveryValues(_CustID);
        }

        ItemUsageTbl _ItemUsage = new ItemUsageTbl();
        List<ItemUsageTbl> _LastItemsOrdered  = _ItemUsage.GetLastItemsUsed(_CustID, TrackerTools.CONST_SERVTYPECOFFEE);
        
        if (_LastItemsOrdered.Count > 0)
        {
          foreach (ItemUsageTbl _LastItemOrder in _LastItemsOrdered)
          {
            if (_LastItemOrder.ItemProvidedID > 0) // now a a order line
            {
              _LastOrder = (AddNewOrderLine(_LastItemOrder.ItemProvidedID, _LastItemOrder.AmountProvided, _LastItemOrder.PackagingID) || (_LastOrder));
              if (!string.IsNullOrEmpty(_LastItemOrder.Notes))
              {
                tbxNotes.Text += String.Format("{0}last order used a group item, so next item in group selected.", (tbxNotes.Text.Length > 0) ? "; " : "");
              }
            }
          }
        }
        else
        {
          TrackerTools _TT = new TrackerTools();
          TrackerTools.ContactPreferedItems _ContactPreferedItems = _TT.RetrieveCustomerPrefs(_CustID);
          _LastOrder = (AddNewOrderLine(_ContactPreferedItems.PreferedItem, _ContactPreferedItems.PreferedQty, _ContactPreferedItems.PrefPackagingID) || (_LastOrder));
        }
        Session[CONST_LINESADDED] = _LastOrder;
      }

      return _LastOrder;
    }

    protected OrderDetailData GetNewOrderItemFromSKU(string pSKU, double pSKUQTY)
    {
      const string CONST_PACKAGINGNOTE = "Please check packing setting";

      // retrieve item from stock database and add to order table
      // string _connectionString, 
      string _ItemProvidedID;
      string _ItemsSQL = "SELECT ItemTypeID, SKU, ItemEnabled FROM ItemTypeTbl WHERE (SKU = ?)";
      OrderDetailData _OrderDetailItem = null; // assume not added

      if (ddlContacts.SelectedValue != null)
      {
        TrackerDb _TDB = new TrackerDb();
        if (pSKU == CONST_BLUEWATERFILTER)
          _TDB.AddWhereParams(CONST_WATERFILTER, System.Data.DbType.String);
        else
          _TDB.AddWhereParams(pSKU, System.Data.DbType.String);
        IDataReader _ItemsReader = _TDB.ExecuteSQLGetDataReader(_ItemsSQL);
        if (_ItemsReader != null)
        {

          // there should only be one record, if not something strange, but add only the first one
          if ((_ItemsReader.Read()) && (_ItemsReader["ItemTypeID"] != null))
          {
            _ItemProvidedID = _ItemsReader["ItemTypeID"].ToString();

            // set Packaging IDs
            int _PackagingID = (pSKUQTY < 1) ? PackagingTbl.CONST_PACKID_LESS1KG : PackagingTbl.CONST_PACKID_NA;
            if (!_PackagingID.Equals(PackagingTbl.CONST_PACKID_NA))
            {
              if (!tbxNotes.Text.Contains(CONST_PACKAGINGNOTE))
                tbxNotes.Text += (tbxNotes.Text.Length > 0) ? " " : "" + "Please check packing setting";
            }
            // ? May need to get default packaging from client data base? now a a order line
            if (pSKU == CONST_BLUEWATERFILTER)
              _PackagingID = PackagingTbl.CONST_PACKID_BLUEFILTER;
            else if (pSKU == CONST_WATERFILTER)
              _PackagingID = PackagingTbl.CONST_PACKID_WHITEFILTER;

            _OrderDetailItem = new OrderDetailData
            {
              ItemTypeID = Convert.ToInt32(_ItemProvidedID),
              QuantityOrdered = pSKUQTY,
              PackagingID = _PackagingID
            };
          }
          else
          {
            // sku not found, maybe add delivery next time
            tbxNotes.Text += "SKU Not Found: " + pSKU + " QTY: " + pSKUQTY.ToString();
          }

          _ItemsReader.Dispose();
        }
        _TDB.Close();
      }
      // now return the item
      return _OrderDetailItem;

    }

    private void BindRowQueryParameters()
    {
      Int64 _BoundCustomerId = Convert.ToInt64(ddlContacts.SelectedValue);
      Session[OrderHeaderData.CONST_BOUNDCUSTOMERID] = _BoundCustomerId;
      DateTime _BoundDeliveryDate = Convert.ToDateTime(tbxRequiredByDate.Text).Date;
      Session[OrderHeaderData.CONST_BOUNDDELIVERYDATE] = _BoundDeliveryDate.Date;
      String _BoundNotes = tbxNotes.Text;
      Session[OrderHeaderData.CONST_BOUNDNOTES] = _BoundNotes;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        DateTime dtThis = DateTime.Now.Date;
        int NumDaysToAdd = 0;
        tbxOrderDate.Text = dtThis.ToShortDateString();

        // Set prep date to Monday if it in neither Wed or Thursday
        dtThis = DateTime.Now.Date;
        // set roast date to be either Monday or Wed
        if ((dtThis.DayOfWeek > DayOfWeek.Tuesday) && (dtThis.DayOfWeek < DayOfWeek.Friday))
          NumDaysToAdd = (int)DayOfWeek.Wednesday - (int)dtThis.DayOfWeek;
        else
        {
          if (dtThis.DayOfWeek < DayOfWeek.Wednesday)  // we are still before the next prep date so set to Monday
            NumDaysToAdd = (int)DayOfWeek.Monday - (int)dtThis.DayOfWeek;
          else if (dtThis.DayOfWeek < DayOfWeek.Friday)
            NumDaysToAdd = (int)DayOfWeek.Wednesday - (int)dtThis.DayOfWeek;
          else // this is in the next week so set it to Monday next week)
            NumDaysToAdd = 8 - (int)dtThis.DayOfWeek;
        }
        dtThis = dtThis.AddDays(NumDaysToAdd);
        tbxRoastDate.Text = dtThis.ToShortDateString();
        // now set delivery date
        if (dtThis.DayOfWeek < DayOfWeek.Friday)
          tbxRequiredByDate.Text = dtThis.AddDays(1).ToShortDateString(); // the next day
        else
          tbxRequiredByDate.Text = dtThis.AddDays(3).ToShortDateString();  // Monday

        // set the page session variables
        bool _UpdateLines = false;   // if true then we will up date all lines
        bool _LinesAdded = false;   // if true then we added lines to the order
        Session[CONST_UPDATELINES] = _UpdateLines;
        Session[CONST_LINESADDED] = _LinesAdded;

        BindRowQueryParameters();
      }
      else
      {
        /// took this out since should only bind on an update
        //// check that the parameters have not changed
        //bool _LinesAdded = (Session[CONST_UPDATELINES] == null) ? true : (bool)Session[CONST_UPDATELINES];
        //if (!_LinesAdded)
        //{   // check if there have been changes
        //  Int64 _BoundCustomerId = (Int64)Session[OrderHeaderData.CONST_BOUNDCUSTOMERID];
        //  DateTime _BoundDeliveryDate = ((DateTime)Session[OrderHeaderData.CONST_BOUNDDELIVERYDATE]).Date;
        //  String _BoundNotes = (String)Session[OrderHeaderData.CONST_BOUNDNOTES];
        //  if ((_BoundCustomerId != Convert.ToInt32(ddlContacts.SelectedValue)) || (_BoundDeliveryDate != Convert.ToDateTime(tbxRequiredByDate.Text)) || (_BoundNotes != tbxNotes.Text))
        //    BindRowQueryParameters();
        //}
      }
    }
    void Page_Unload (Object sender , EventArgs e)
    {
    }

    void RedirectToOrderDetail()
    {
      Response.Redirect(ResolveUrl("~/Pages/OrderDetail.aspx") + "?" +
                                    String.Format("CustomerID={0}&DeliveryDate={1:d}&Notes={2}",
                                                   HttpContext.Current.Server.UrlEncode(ddlContacts.SelectedValue),
                                                   Convert.ToDateTime(tbxRequiredByDate.Text),
                                                   HttpContext.Current.Server.UrlEncode(tbxNotes.Text)));
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)  //Complete
    {
      if (!IsPostBack) 
      {
        // hand query string if sent
        // Look for first Customer Name, and then ann stock items sent, also allow or Last Order.
        if (Request.QueryString.Count > 0)
        {
          if (Request.QueryString[CONST_URL_REQUEST_CUSTOMERID] != null)
            SetContactByID(Request.QueryString[CONST_URL_REQUEST_CUSTOMERID]); 
          else if (Request.QueryString[CONST_URL_REQUEST_NAME] != null)
            SetContactValue(Request.QueryString[CONST_URL_REQUEST_COMPANYNAME], Request.QueryString[CONST_URL_REQUEST_NAME], Request.QueryString[CONST_URL_REQUEST_EMAIL]);

          // check if they want last order
          if (Request.QueryString[CONST_URL_REQUEST_LASTORDER ] != null)
          {
            if (Request.QueryString[CONST_URL_REQUEST_LASTORDER] == "Y")
            {
              if (AddLastOrder(true))
                RedirectToOrderDetail();
            }
            // now add stock item
          }
          if (Request.QueryString[CONST_URL_REQUEST_SKU1] != null)
          {
            // set the dates and delivery
            if ((ddlContacts != null) && (Convert.ToInt64(ddlContacts.SelectedValue) > 0))
              SetPrepAndDeliveryValues(Convert.ToInt64(ddlContacts.SelectedValue));
            ///
            List<TrackerDotNet.control.OrderDetailData> _newOrderItems = new List<OrderDetailData>();
            bool _hasNullItems = false;
            // loop through all the SKUs and add them
            int i = 1;
            while (Request.QueryString["SKU" + i.ToString()] != null)
            {
              // check if there is such an item and return it if not null
              OrderDetailData _newItem = GetNewOrderItemFromSKU(Request.QueryString["SKU" + i], Convert.ToDouble(Request.QueryString["SKUQTY" + i]));
              if (_newItem != null)
                _newOrderItems.Add(_newItem);
              else
                if (!_hasNullItems)
                {
                  _hasNullItems = true;
                  // add a new item that is a note.
                  _newOrderItems.Add(new OrderDetailData { ItemTypeID = CONST_NOTEITEMTIMEID, QuantityOrdered = 1, PackagingID = 0 });
                }
              i++;
            }
            tbxNotes.Text += String.Format(">{0} items added", i - 1);
            // now add the items to the database
            foreach (OrderDetailData _newItem in _newOrderItems)
              AddNewOrderLine(_newItem.ItemTypeID, _newItem.QuantityOrdered, _newItem.PackagingID);

            bool _LinesAdded = true;   // lines should have been added since tthere are items.
            Session[CONST_LINESADDED] = _LinesAdded;
            RedirectToOrderDetail();
          }
        }
      }
    }

    //public string Check4Null(string pID)
    //{
    //  if (pID == null)
    //    return "0";
    //  else
    //    return pID;
    //}
    public void DeleteOrderItem(string pOrderID)
    {
      OrderTbl _Order = new OrderTbl();
      string _ErrorStr = _Order.DeleteOrderById(Convert.ToInt64(pOrderID));
      ltrlStatus.Text = (_ErrorStr.Length == 0) ? "Item deleted" : _ErrorStr;
    }
    protected void gvOrderLines_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("DeleteItem"))
      {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int _OrderID = Convert.ToInt32(e.CommandArgument);

        DeleteOrderItem(e.CommandArgument.ToString());
        // Get the last name of the sele  cted author from the appropriate
        // cell in the GridView control.
        //GridViewRow _row = gvItemsInList.Rows[_index];
       
        
        //DropDownList _ddlItem = (DropDownList)_row.FindControl("ddlItemDesc");
        //Label _lblItemSortPos = (Label)_row.FindControl("lblItemSortPos");

        //TrackerDotNet.control.ItemGroupTbl _ITG = new control.ItemGroupTbl();
        //_ITG.GroupItemTypeID = Convert.ToInt32(ddlGroupItems.SelectedValue);
        //_ITG.ItemTypeID = Convert.ToInt32(_ddlItem.SelectedValue);
        //_ITG.ItemTypeSortPos = Convert.ToInt32(_lblItemSortPos.Text);

        //if (e.CommandName.Equals("MoveUp"))
        //{
        //  _ITG.DecItemSortPos(_ITG);
        //}
        //else
        //  _ITG.IncItemSortPos(_ITG);

        gvOrderLines.DataBind();
      }
    }

/*
      string _connectionString;
      if (ConfigurationManager.ConnectionStrings[CONST_CONSTRING] == null ||
          ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString.Trim() == "")
      {
        throw new Exception("A connection string named " + CONST_CONSTRING + " with a valid connection string " +
                            "must exist in the <connectionStrings> configuration section for the application.");
      }
      _connectionString = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;

      string _sqlCmd = "DELETE FROM OrdersTbl WHERE (OrderId = ?)";
      OleDbConnection _conn = new OleDbConnection(_connectionString);

      // add parameters in the order they appear in the update command
      OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
      _cmd.Parameters.Add(new OleDbParameter { Value = pOrderID }); 
      
      Label _OrderIDLabel = (Label)gvOrderLines.FindControl("lblOrderID");
      _cmd.Parameters.Add(new OleDbParameter { Value = _OrderIDLabel.Text});

      try
      {
        _conn.Open();
        if (_cmd.ExecuteNonQuery() > 0)
          _ErrorStr="No records deleted";
      }
      catch (OleDbException oleDbErr)
      {
        // Handle exception.
        _ErrorStr = "Error: " + oleDbErr.Message;
      }
      finally
      {
        _conn.Close();
      }

      ltrlStatus.Text = (_ErrorStr.Length == 0) ? "Item deleted" : _ErrorStr;
    }
*/
    protected void btnNewItem_Click(object sender, EventArgs e)
    {
      // show all buttons and text entry
      btnAdd.Visible = true;
      btnCancel.Visible = true;
      pnlNewItem.Visible = true;
      // now hide new button
      btnNewItem.Visible = false;
      upnlNewOrderItem.Update();

//      ddlNewItemDesc.DataBind();
//      ddlNewPackaging.DataBind();

    }

    private void UpdateDataDisplay()
    {
      gvOrderLines.DataBind();
      upnlNewOrderItem.Update();
      upnlOrderLines.Update();
    }
    private void HideNewOrderItemPanel()
    {
      // hide all buttons and text entry
      btnAdd.Visible = false;
      btnCancel.Visible = false;
      pnlNewItem.Visible = false;
      // now show new button
      btnNewItem.Visible = true;
      //update panels
      upnlNewOrderItem.Update();
      UpdateDataDisplay();
    }
    
     protected bool AddNewOrderLine(int pNewItemID, double pNewQuantityOrdered, int pNewPackagingID)
    {
      string _ErrorStr = "";
            
      OrderTblData _OrderData = new OrderTblData();
      OrderTbl _OrderTbl = new OrderTbl();

      _OrderData.CustomerId  = Convert.ToInt64(ddlContacts.SelectedValue);
      _OrderData.OrderDate = Convert.ToDateTime(tbxOrderDate.Text).Date;
      _OrderData.RoastDate = Convert.ToDateTime(tbxRoastDate.Text).Date;
      _OrderData.RequiredByDate = Convert.ToDateTime(tbxRequiredByDate.Text).Date;
      _OrderData.ToBeDeliveredBy = Convert.ToInt32( ddlToBeDeliveredBy.SelectedValue);
      _OrderData.PurchaseOrder = tbxPurchaseOrder.Text;
      _OrderData.Confirmed = Convert.ToBoolean(cbxConfirmed.Checked);
      _OrderData.InvoiceDone = cbxInvoiceDone.Checked;
      _OrderData.Done = Convert.ToBoolean(cbxDone.Checked);
      _OrderData.Notes = tbxNotes.Text;
      // Now line data
      Session[OrderHeaderData.CONST_BOUNDOLDDELIVERYDATE] = _OrderData.RequiredByDate.Date;
      TrackerTools _TT = new TrackerTools();
      _OrderData.ItemTypeID = _TT.ChangeItemIfGroupToNextItemInGroup(_OrderData.CustomerId, pNewItemID, _OrderData.RequiredByDate);
      _OrderData.QuantityOrdered = pNewQuantityOrdered;
      _OrderData.PackagingID = pNewPackagingID;

      _ErrorStr = _OrderTbl.InsertNewOrderLine(_OrderData);

      BindRowQueryParameters();

      return String.IsNullOrEmpty(_ErrorStr);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
      const string CONST_SERVICINGNOTE = "Should this not rather be a repair?";

      int _ItemID = Convert.ToInt32(ddlNewItemDesc.SelectedValue);
      bool _ItemAdded = 
        AddNewOrderLine(_ItemID, Convert.ToDouble(tbxNewQuantityOrdered.Text), Convert.ToInt32(ddlNewPackaging.SelectedValue));

      if (_ItemID.Equals(ItemTypeTbl.CONST_SERVICEITEMID))
      {
        if (!tbxNotes.Text.Contains(CONST_SERVICINGNOTE))
          tbxNotes.Text += (tbxNotes.Text.Length > 0) ? " " : "" + CONST_SERVICINGNOTE;
      }
      SetButtonState((_ItemAdded) || btnCancel.Enabled);

      Session[CONST_LINESADDED] = _ItemAdded; // a line has been added

      ltrlStatus.Text = (_ItemAdded == true ? "Item Added" : "Error adding item"); //: " + _ErrorStr);
      HideNewOrderItemPanel();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      HideNewOrderItemPanel();
    }

    protected virtual void dvOrderHeader_OnItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
    {
      UpdateDataDisplay(); 
      //odsOrderDetail.DataBind();
      //gvOrderLines.DataBind();
      //upnlOrderLines.Update();
    }

    protected void ddlToBeDeliveredBy_OnDataBound(object sender, EventArgs e)
    {
      // Find Sino as the preferred default
      int i = 0;
      while ((i < ddlToBeDeliveredBy.Items.Count) && (ddlToBeDeliveredBy.Items[i].Text !=  CONST_DELIVERY_DEFAULT))
        i++;

      if (i < ddlToBeDeliveredBy.Items.Count)
        ddlToBeDeliveredBy.SelectedValue = ddlToBeDeliveredBy.Items[i].Value; // should be Sino

    }

    protected void btnAddLastOrder_Click(object sender, EventArgs e)
    {
      AddLastOrder();
      SetButtonState(true);
      UpdateDataDisplay();
      //gvOrderLines.DataBind();
      //upnlOrderLines.Update();
      //upnlNewOrderItem.Update();
    }

    protected void btnCancelled_Click(object sender, EventArgs e)
    {
      MembershipUser _currMember = Membership.GetUser();

      if (_currMember.UserName.ToLower() == "warren")
      {
        // only if it is warren allow this
        foreach (GridViewRow gvr in gvOrderLines.Rows)
        {
          Label myOrderIDLablel = (Label)gvr.FindControl("lblOrderID"); //find contol since it is a template field
          DeleteOrderItem(myOrderIDLablel.Text);
        }

        // ReturnTo delivery sheet
        Response.Redirect("DeliverySheet.aspx");
      }
    }

    protected void tbxNotes_TextChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void gvOrderLines_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnRefreshDetails_Click(object sender, EventArgs e)
    {
      UpdateDataDisplay();
      ddlContacts.DataBind();
    }

    protected void tmrOrderItem_OnTick(object sender, EventArgs e)
    {
      // only do this once quickly since the pre-render cannto bind the grid.
      bool _LinesAdded = (Session[CONST_LINESADDED] != null) ? (bool)Session[CONST_LINESADDED] : false;
      if (_LinesAdded)
        UpdateDataDisplay();    ///        btnRefreshDetails_Click(sender, e);
      tmrOrderItem.Enabled = false;
    }
    protected void SetUpdateBools()
    {
      if (ddlContacts.SelectedIndex > 0) 
      {
        if (ddlContacts.SelectedValue.Equals(CONST_ZZNAME_DEFAULTID) && (String.IsNullOrEmpty(tbxNotes.Text)))
        {
          btnNewItem.Enabled = false;
          upnlNewOrderItem.Update();
        }
        else
        {
          if (!btnNewItem.Enabled)
          {
            btnNewItem.Enabled = true;
            upnlNewOrderItem.Update();
          }
          if (!btnAddLastOrder.Enabled)
          {
            btnAddLastOrder.Enabled = true;
            updtButtonPanel.Update();
          }
        }
      }
      // Update all the orders to reflect this. 
      bool _LinesAdded = (Session[CONST_LINESADDED] != null) ? (bool)Session[CONST_LINESADDED] : false;
      bool _UpdateLines = (Session[CONST_UPDATELINES] != null) ? (bool)Session[CONST_UPDATELINES] : false;
      if ((_LinesAdded) && (!_UpdateLines))
      {
        // items need to be updated set the button to visible
        _UpdateLines = true;
        btnUpdate.Visible = _UpdateLines;
        upnlOrderSummary.Update();
        Session[CONST_UPDATELINES] = _UpdateLines;
        // if there are lines add the refresh)
        if (!btnRefreshDetails.Enabled)
        {
          btnRefreshDetails.Enabled = true;
          updtButtonPanel.Update();
        }
      }
      // dont need this as was not update
      //else // lines have not been added or update has 
      //  BindRowQueryParameters();
    }
    /// <summary>
    /// Something in the header data has changed so do a generic update
    /// </summary>
    protected void DoHeaderUpdate()
    {
      List<string> _OrderIds = (List<string>)Session[CONST_ORDERLINEIDS];
      List<int> _OrderItemIds = (List<int>)Session[CONST_ORDERLINEITEMIDS];
      if (_OrderIds.Count > 0)
      {
        OrderDataControl _myDB = new OrderDataControl();
        OrderHeaderData _OrderHeader = new OrderHeaderData();

        _OrderHeader.CustomerID = Convert.ToInt64(ddlContacts.SelectedValue);
        _OrderHeader.OrderDate = Convert.ToDateTime(tbxOrderDate.Text);
        _OrderHeader.RoastDate = Convert.ToDateTime(tbxRoastDate.Text);
        _OrderHeader.ToBeDeliveredBy = Convert.ToInt64(ddlToBeDeliveredBy.SelectedValue);
        _OrderHeader.RequiredByDate = Convert.ToDateTime(tbxRequiredByDate.Text);
        _OrderHeader.Confirmed = cbxConfirmed.Checked;
        _OrderHeader.Done = cbxDone.Checked;
        _OrderHeader.InvoiceDone = cbxInvoiceDone.Checked;
        _OrderHeader.PurchaseOrder = tbxPurchaseOrder.Text;
        _OrderHeader.Notes = tbxNotes.Text;
        
        _myDB.UpdateOrderHeader(_OrderHeader,_OrderIds);

        // if date has changed since item added then see if one of the items was a group item and change that date too
        DateTime _OldRequiredByDate = _OrderHeader.RequiredByDate;
        if (Session[OrderHeaderData.CONST_BOUNDOLDDELIVERYDATE] != null)
          _OldRequiredByDate = ((DateTime)Session[OrderHeaderData.CONST_BOUNDOLDDELIVERYDATE]).Date;
        if (!_OldRequiredByDate.Equals(_OrderHeader.RequiredByDate))
        {
          UsedItemGroupTbl _UsedItems = new UsedItemGroupTbl();

          foreach (int _OrderItemId in _OrderItemIds)
          {
            _UsedItems.UpdateIfGroupItem(_OrderHeader.CustomerID, _OrderItemId, _OldRequiredByDate, _OrderHeader.RequiredByDate);
          }
          Session[OrderHeaderData.CONST_BOUNDOLDDELIVERYDATE] = _OrderHeader.RequiredByDate.Date;
        }
      }
      BindRowQueryParameters();
    }
    private TrackerTools.ContactPreferedItems SetPrepAndDeliveryValues(long pCustomerID)
    {
      DateTime _DeliveryDate = DateTime.Now.Date;
      TrackerTools _TT = new classes.TrackerTools();     // . from TrackerDotNet.classes.

      DateTime _dtNextRoastDay = _TT.GetNextRoastDateByCustomerID(pCustomerID, ref _DeliveryDate);
      TrackerTools.ContactPreferedItems _ContactPreferedItems = _TT.RetrieveCustomerPrefs(pCustomerID);

      // set dates 
      tbxRoastDate.Text = String.Format("{0:d}", _dtNextRoastDay);
      tbxRequiredByDate.Text = String.Format("{0:d}", _DeliveryDate);
      // make sure the default delivery person can delvier on that day
      PersonsTbl _Person = new PersonsTbl();
      int _DeliveryBy = _Person.IsNormalDeliveryDoW(_ContactPreferedItems.DeliveryByID, (int)(_DeliveryDate.DayOfWeek) + 1) ?
        _ContactPreferedItems.DeliveryByID : TrackerTools.CONST_DEFAULT_DELIVERYBYID;
      // ad a note if we changed it
      if (!_DeliveryBy.Equals(_ContactPreferedItems.DeliveryByID))
        tbxNotes.Text += ((tbxNotes.Text.Length > 0) ? " " : "") + "!!!default delivery person changed due to DoW calculation";
      if (ddlToBeDeliveredBy.Items.FindByValue(_DeliveryBy.ToString()) != null)
        ddlToBeDeliveredBy.SelectedValue = _DeliveryBy.ToString();
      if (_ContactPreferedItems.RequiresPurchOrder)
        tbxPurchaseOrder.Text = TrackerTools.CONST_POREQUIRED;

      return _ContactPreferedItems;
    }

    protected void ddlContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList _ddlCustomers = (DropDownList)sender;
      // roast day vars
      // preference vars;
      Int64 _CustID = Convert.ToInt64(_ddlCustomers.SelectedValue);
      TrackerTools.ContactPreferedItems _ContactPreferedItems = SetPrepAndDeliveryValues(_CustID);
      // set a default item and quantity if not set
      if (ddlNewItemDesc.SelectedIndex == TrackerDb.CONST_INVALIDID)
      {
        if (ddlNewItemDesc.Items.FindByValue(_ContactPreferedItems.PreferedItem.ToString()) != null)
          ddlNewItemDesc.SelectedValue = _ContactPreferedItems.PreferedItem.ToString();
        tbxNewQuantityOrdered.Text = _ContactPreferedItems.PreferedQty.ToString();
      }
      // setup values
      upnlOrderSummary.Update();
      SetUpdateBools();
    }

    protected void tbxOrderDate_TextChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void tbxRoastDate_TextChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void ddlToBeDeliveredBy_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void tbxRequiredByDate_TextChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void tbxPurchaseOrder_TextChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void cbxConfirmed_CheckedChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void cbxInvoiceDone_CheckedChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void cbxDone_CheckedChanged(object sender, EventArgs e)
    {
      SetUpdateBools();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DoHeaderUpdate();
        btnUpdate.Visible = false;
        // now set update to false for next time.
        bool _UpdateLines = false;
        Session[CONST_UPDATELINES] = _UpdateLines;
    }

    protected void gvOrderLines_RowUpdated(Object sender, GridViewUpdatedEventArgs e)
    {
      gvOrderLines.DataBind();
      upnlOrderLines.Update();
    }

    protected void gvOrderLines_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.Header)
      {
        //  clear the list of order ids
        List<string> _OrderIds = new List<string>();
        List<int> _OrderItemIds = new List<int>();
        
        Session[CONST_ORDERLINEIDS] = _OrderIds;
        Session[CONST_ORDERLINEITEMIDS] = _OrderItemIds;
      }
      else if(e.Row.RowType == DataControlRowType.DataRow)
      {
        // For each line store the OrderID and ItemID
        List<string> _OrderIds = (List<string>)Session[CONST_ORDERLINEIDS];
        if (_OrderIds == null)
          _OrderIds = new List<string>();

        Label _lblOrderID = (Label)e.Row.FindControl("lblOrderId");
        _OrderIds.Add(_lblOrderID.Text);
        Session[CONST_ORDERLINEIDS] = _OrderIds;

        List<int> _OrderItemIds = (List<int>)Session[CONST_ORDERLINEITEMIDS];
        if (_OrderItemIds == null)
          _OrderItemIds = new List<int>();
        DropDownList _ddlItemDesc = (DropDownList)e.Row.FindControl("ddlItemDesc");
        _OrderItemIds.Add(Convert.ToInt32(_ddlItemDesc.SelectedValue));
        Session[CONST_ORDERLINEITEMIDS] = _OrderItemIds;
      }
    }

    protected void btnCheckDetails_Click(object sender, EventArgs e)
    {
      RedirectToOrderDetail();
    }


  }
}