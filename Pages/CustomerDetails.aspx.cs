using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;

namespace QOnT.Pages
{
  public partial class CustomerDetails : System.Web.UI.Page
  {
    const string CONST_URL_REQUEST_CUSTOMERID = "ID";
    const string CONST_URL_REQUEST_CUSTOMERACCFOCUS = "Focus_AccInfo";
    const string CONST_FORCE_GVITEMS_BIND = "ForceGVItemsToBind";
    const string CONST_LASTTABCONTER = "LastTabcCustomerIndex";

    const int CONST_GVITEMS_COL_ITEMDATE = 1;
    const int CONST_GVITEMS_COL_ITEMPROVIDEDID = 1;


    static string prevPage = String.Empty;

    private void SetButtonStatus(bool pEditMode)
    {
      string[] _UserRoles = Roles.GetRolesForUser();

      bool _EnableEdit = !((Roles.IsUserInRole("repair")) && (_UserRoles.Length == 1));   // they are only in repair

      if (pEditMode)
      {
        btnUpdate.Enabled = _EnableEdit;
        btnUpdateAndReturn.Enabled = _EnableEdit;
        btnCopy2AccInfo.Enabled = _EnableEdit;
        btnAddLasOrder.Enabled = _EnableEdit;
        btnForceNext.Enabled = _EnableEdit;
        btnInsert.Enabled = false;
        tabcCustomer.Visible = true;
        btnRecalcAverage.Enabled = _EnableEdit;
        accAddDetailsButton.Enabled = false;
        accUpdateButton.Enabled = _EnableEdit;
      }
      else
      {
        btnUpdate.Enabled = false;
        btnUpdateAndReturn.Enabled = false;
        btnCopy2AccInfo.Enabled = false;
        btnAddLasOrder.Enabled = false;
        btnForceNext.Enabled = false;
        btnInsert.Enabled = _EnableEdit;
        btnRecalcAverage.Enabled = false;
        enabledCheckBox.Checked = _EnableEdit;   // if it is an insert enable the customer
        tabcCustomer.Visible = false;

        // if it is an insert enable then there is no customer acc info
        accAddDetailsButton.Enabled = _EnableEdit;
        accUpdateButton.Enabled = false;
      }
      upnlCustomerDetails.Update();
      dvCustomersAccInfoUpdatePanel.Update();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //uppnlTabContainer.Update();

        // get referring page
        if ((Request.UrlReferrer == null))
          prevPage = String.Empty;
        else
          prevPage = Request.UrlReferrer.ToString();
        // if the id is past and is not null then set it
        if ((Request.QueryString[CONST_URL_REQUEST_CUSTOMERID]) != null)
        {
          CustomersTbl _ctd = new CustomersTbl();
          SetButtonStatus(true);
          accInvoiceTypesDropDownList.DataBind();
          accPaymentTermsDropDownList.DataBind();
          accPriceLevelsDropDownList.DataBind();
          gvItems.Sort("Date", SortDirection.Descending);
          PutDataOnForm(_ctd.GetCustomersByCustomerID(GetCustomerIDFromRequest(), ""));
        }
        else
        {
          SetButtonStatus(false);
        }


        if (Session[CONST_LASTTABCONTER] != null)
        {
          int _LastTabIndex = (int)Session[CONST_LASTTABCONTER];
          if (_LastTabIndex > 0)
            tabcCustomer.ActiveTabIndex = _LastTabIndex;
        }
      }
      if (Session[CONST_FORCE_GVITEMS_BIND] != null)
      {
        if ((bool)Session[CONST_FORCE_GVITEMS_BIND])
        {
          Session[CONST_FORCE_GVITEMS_BIND] = null;
        }
      }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if ((Request.QueryString[CONST_URL_REQUEST_CUSTOMERID]) != null)
        {
        }
        if ((Request.QueryString[CONST_URL_REQUEST_CUSTOMERACCFOCUS]) != null)
        {
          if (Request.QueryString[CONST_URL_REQUEST_CUSTOMERACCFOCUS].StartsWith("Y", StringComparison.OrdinalIgnoreCase))
          {
            tabcCustomer.ActiveTabIndex = 0;
            this.Page.SetFocus(accFullCoNameTextBox);
            uppnlTabContainer.Update();
          }
        }
      }
    }

    private long GetCustomerIDFromRequest()
    {
      long _CustID = 0;
      if (Request.QueryString[CONST_URL_REQUEST_CUSTOMERID] != null)
      {
        if (!Int64.TryParse(Request.QueryString[CONST_URL_REQUEST_CUSTOMERID], out _CustID))
          _CustID = 0;
      }
      return _CustID;
    }
    private void PutDataOnForm(CustomersTbl pCustomersTblData)
    {
      CompanyNameTextBox.Text = pCustomersTblData.CompanyName;
      CompanyIDLabel.Text = pCustomersTblData.CustomerID.ToString();
      ContactFirstNameTextBox.Text = pCustomersTblData.ContactFirstName;
      ContactLastNameTextBox.Text = pCustomersTblData.ContactLastName;
      ContactTitleTextBox.Text = pCustomersTblData.ContactTitle;
      ContactAltFirstNameTextBox.Text = pCustomersTblData.ContactAltFirstName;
      ContactAltLastNameTextBox.Text = pCustomersTblData.ContactAltLastName;
      BillingAddressTextBox.Text = pCustomersTblData.BillingAddress;
      DepartmentTextBox.Text = pCustomersTblData.Department;
      PostalCodeTextBox.Text = pCustomersTblData.PostalCode;
      ddlCities.SelectedValue = pCustomersTblData.City.ToString();
      ProvinceTextBox.Text = pCustomersTblData.Province;
      PhoneNumberTextBox.Text = pCustomersTblData.PhoneNumber;
      CellNumberTextBox.Text = pCustomersTblData.CellNumber;
      FaxNumberTextBox.Text = pCustomersTblData.FaxNumber;
      EmailAddressTextBox.Text = pCustomersTblData.EmailAddress;
      AltEmailAddressTextBox.Text = pCustomersTblData.AltEmailAddress;
      ddlCustomerTypes.SelectedValue = pCustomersTblData.CustomerTypeID.ToString();
      ddlEquipTypes.SelectedValue = pCustomersTblData.EquipType.ToString();
      MachineSNTextBox.Text = pCustomersTblData.MachineSN;
      ddlFirstPreference.SelectedValue = pCustomersTblData.CoffeePreference.ToString();
      PriPrefQtyTextBox.Text = pCustomersTblData.PriPrefQty.ToString();
      ddlPackagingTypes.SelectedValue = pCustomersTblData.PrefPackagingID.ToString();
      ddlDeliveryBy.SelectedValue = pCustomersTblData.PreferedAgent.ToString();
      ddlAgent.SelectedValue = pCustomersTblData.SalesAgentID.ToString();
      ReminderCountLabel.Text = pCustomersTblData.ReminderCount.ToString();
      LastReminderLabel.Text = String.Format("{0:d}", pCustomersTblData.LastDateSentReminder);
      enabledCheckBox.Checked = pCustomersTblData.enabled;
      autofulfillCheckBox.Checked = pCustomersTblData.autofulfill;
      UsesFilterCheckBox.Checked = pCustomersTblData.UsesFilter;
      PredictionDisabledCheckBox.Checked = pCustomersTblData.PredictionDisabled;
      AlwaysSendChkUpCheckBox.Checked = pCustomersTblData.AlwaysSendChkUp;
      NormallyRespondsCheckBox.Checked = pCustomersTblData.NormallyResponds;
      NotesTextBox.Text = pCustomersTblData.Notes;

      PutAccDataOnForm(pCustomersTblData.CustomerID);
    }

    private void PlaceAccDataOnForm(CustomersAccInfoTbl pCustomersAccInfo)
    {
      accFullCoNameTextBox.Text = pCustomersAccInfo.FullCoName;
      accCustomerVATNoTextBox.Text = pCustomersAccInfo.CustomerVATNo;
      accInvoiceTypesDropDownList.SelectedValue = pCustomersAccInfo.InvoiceTypeID.ToString();
      accRequiresPurchOrderCheckBox.Checked = pCustomersAccInfo.RequiresPurchOrder;
      accEnabledCheckBox.Checked = pCustomersAccInfo.Enabled;
      accBillAddr1TextBox.Text = pCustomersAccInfo.BillAddr1;
      accBillAddr2TextBox.Text = pCustomersAccInfo.BillAddr2;
      accBillAddr3TextBox.Text = pCustomersAccInfo.BillAddr3;
      accBillAddr4TextBox.Text = pCustomersAccInfo.BillAddr4;
      accBillAddr5TextBox.Text = pCustomersAccInfo.BillAddr5;
      accShipAddr1TextBox.Text = pCustomersAccInfo.ShipAddr1;
      accShipAddr2TextBox.Text = pCustomersAccInfo.ShipAddr2;
      accShipAddr3TextBox.Text = pCustomersAccInfo.ShipAddr3;
      accShipAddr4TextBox.Text = pCustomersAccInfo.ShipAddr4;
      accShipAddr5TextBox.Text = pCustomersAccInfo.ShipAddr5;
      accFirstNameTextBox.Text = pCustomersAccInfo.AccFirstName;
      accLastNameTextBox.Text = pCustomersAccInfo.AccLastName;
      getaccAccEmailTextBox.Text = pCustomersAccInfo.AccEmail;
      accAltFirstNameTextBox.Text = pCustomersAccInfo.AltAccFirstName;
      accAltLastNameTextBox.Text = pCustomersAccInfo.AltAccLastName;
      accAltEmailTextBox.Text = pCustomersAccInfo.AltAccEmail;
      accPaymentTermsDropDownList.SelectedValue = pCustomersAccInfo.PaymentTermID.ToString();
      accPriceLevelsDropDownList.SelectedValue = pCustomersAccInfo.PriceLevelID.ToString();
      accRegNoTextBox.Text = pCustomersAccInfo.RegNo;
      accLimitTextBox.Text = string.Format("{0:0.00}", pCustomersAccInfo.Limit);
      accBankAccNoTextBox.Text = pCustomersAccInfo.BankAccNo;
      accBankBranchTextBox.Text = pCustomersAccInfo.BankBranch;
      accNotesTextBox.Text = pCustomersAccInfo.Notes;
      CustomersAccInfoIDLabel.Text = pCustomersAccInfo.CustomersAccInfoID.ToString();
    }

    private void PutAccDataOnForm(long pCustomerID)
    {
      CustomersAccInfoTbl _CustomersAccInfo = new CustomersAccInfoTbl();
      if (pCustomerID > 0)
      {
        _CustomersAccInfo = _CustomersAccInfo.GetByCustomerID(pCustomerID);
        pCustomerID = _CustomersAccInfo.CustomerID; // if 0 does not exist
      }

      string[] _UserRoles = Roles.GetRolesForUser();

      bool _EnableEdit = !((Roles.IsUserInRole("repair")) && (_UserRoles.Length == 1));   // they are only in repair

      if ((_CustomersAccInfo.CustomersAccInfoID == 0))
      {
        accAddDetailsButton.Enabled = _EnableEdit;    // records are not there so can insert
        accUpdateButton.Enabled = false;
      }
      else
      {
        accAddDetailsButton.Enabled = false;   // cannot insert records already there
        accUpdateButton.Enabled = _EnableEdit;

        PlaceAccDataOnForm(_CustomersAccInfo);
      }
      dvCustomersAccInfoUpdatePanel.Update();

    }
    private CustomersAccInfoTbl GetAccDataFromForm()
    {
      CustomersAccInfoTbl _CustomersAccInfo = new CustomersAccInfoTbl();

      if (!string.IsNullOrEmpty(CustomersAccInfoIDLabel.Text))
        _CustomersAccInfo.CustomersAccInfoID = StringToInt32(CustomersAccInfoIDLabel.Text);
      _CustomersAccInfo.CustomerID = StringToInt64(CompanyIDLabel.Text);
      _CustomersAccInfo.FullCoName = accFullCoNameTextBox.Text;
      _CustomersAccInfo.CustomerVATNo = accCustomerVATNoTextBox.Text;
      _CustomersAccInfo.InvoiceTypeID = StringToInt32(accInvoiceTypesDropDownList.SelectedValue);
      _CustomersAccInfo.RequiresPurchOrder = accRequiresPurchOrderCheckBox.Checked;
      _CustomersAccInfo.Enabled = accEnabledCheckBox.Checked;
      _CustomersAccInfo.BillAddr1 = accBillAddr1TextBox.Text;
      _CustomersAccInfo.BillAddr2 = accBillAddr2TextBox.Text;
      _CustomersAccInfo.BillAddr3 = accBillAddr3TextBox.Text;
      _CustomersAccInfo.BillAddr4 = accBillAddr4TextBox.Text;
      _CustomersAccInfo.BillAddr5 = accBillAddr5TextBox.Text;
      _CustomersAccInfo.ShipAddr1 = accShipAddr1TextBox.Text;
      _CustomersAccInfo.ShipAddr2 = accShipAddr2TextBox.Text;
      _CustomersAccInfo.ShipAddr3 = accShipAddr3TextBox.Text;
      _CustomersAccInfo.ShipAddr4 = accShipAddr4TextBox.Text;
      _CustomersAccInfo.ShipAddr5 = accShipAddr5TextBox.Text;
      _CustomersAccInfo.AccFirstName = accFirstNameTextBox.Text;
      _CustomersAccInfo.AccLastName = accLastNameTextBox.Text;
      _CustomersAccInfo.AccEmail = accAccEmailTextBox.Text;
      _CustomersAccInfo.AltAccFirstName = accAltFirstNameTextBox.Text;
      _CustomersAccInfo.AltAccLastName = accAltLastNameTextBox.Text;
      _CustomersAccInfo.AltAccEmail = accAltEmailTextBox.Text;
      _CustomersAccInfo.PaymentTermID = StringToInt32(accPaymentTermsDropDownList.SelectedValue);
      _CustomersAccInfo.PriceLevelID = StringToInt32(accPriceLevelsDropDownList.SelectedValue);
      _CustomersAccInfo.RegNo = accRegNoTextBox.Text;
      _CustomersAccInfo.Limit = StringToDouble(accLimitTextBox.Text);
      _CustomersAccInfo.BankAccNo = accBankAccNoTextBox.Text;
      _CustomersAccInfo.BankBranch = accBankBranchTextBox.Text;
      _CustomersAccInfo.Notes = accNotesTextBox.Text;
      return _CustomersAccInfo;
    }

    private int StringToInt32(string pValue)
    {
      int _result = 0;
      int.TryParse(pValue, out _result);

      return _result;
    }
    private Int64 StringToInt64(string pValue)
    {
      Int64 _result = 0;
      Int64.TryParse(pValue, out _result);

      return _result;
    }
    private Double StringToDouble(string pValue)
    {
      Double _result = 0.00;
      Double.TryParse(pValue, out _result);

      return _result;
    }
    private CustomersTbl GetDataFromForm()
    {
      CustomersTbl _CustomersTblData = new CustomersTbl();

      _CustomersTblData.CompanyName = CompanyNameTextBox.Text;
      _CustomersTblData.CustomerID = (String.IsNullOrWhiteSpace(CompanyIDLabel.Text)) ? 0 : StringToInt64(CompanyIDLabel.Text);  //0  for insert
      _CustomersTblData.ContactFirstName = ContactFirstNameTextBox.Text;
      _CustomersTblData.ContactLastName = ContactLastNameTextBox.Text;
      _CustomersTblData.ContactTitle = ContactTitleTextBox.Text;
      _CustomersTblData.ContactAltFirstName = ContactAltFirstNameTextBox.Text;
      _CustomersTblData.ContactAltLastName = ContactAltLastNameTextBox.Text;
      _CustomersTblData.BillingAddress = BillingAddressTextBox.Text;
      _CustomersTblData.Department = DepartmentTextBox.Text;
      _CustomersTblData.PostalCode = PostalCodeTextBox.Text;
      _CustomersTblData.City = StringToInt32(ddlCities.SelectedValue);
      _CustomersTblData.Province = ProvinceTextBox.Text;
      _CustomersTblData.PhoneNumber = PhoneNumberTextBox.Text;
      _CustomersTblData.CellNumber = CellNumberTextBox.Text;
      _CustomersTblData.FaxNumber = FaxNumberTextBox.Text;
      _CustomersTblData.EmailAddress = EmailAddressTextBox.Text;
      _CustomersTblData.AltEmailAddress = AltEmailAddressTextBox.Text;
      _CustomersTblData.CustomerTypeID = StringToInt32(ddlCustomerTypes.SelectedValue);
      _CustomersTblData.EquipType = StringToInt32(ddlEquipTypes.SelectedValue); // CoffeePreferenceTextBox.Text);
      _CustomersTblData.MachineSN = MachineSNTextBox.Text;
      _CustomersTblData.CoffeePreference = StringToInt32(ddlFirstPreference.SelectedValue); // CoffeePreferenceTextBox.Text);
      _CustomersTblData.PriPrefQty = String.IsNullOrWhiteSpace(PriPrefQtyTextBox.Text) ? 0 : StringToDouble(PriPrefQtyTextBox.Text);
      _CustomersTblData.PrefPackagingID = StringToInt32(ddlPackagingTypes.SelectedValue);
      _CustomersTblData.PreferedAgent = StringToInt32(ddlDeliveryBy.SelectedValue);
      _CustomersTblData.SalesAgentID = StringToInt32(ddlAgent.SelectedValue);
      _CustomersTblData.enabled = enabledCheckBox.Checked;
      _CustomersTblData.autofulfill = autofulfillCheckBox.Checked;
      _CustomersTblData.UsesFilter = UsesFilterCheckBox.Checked;
      _CustomersTblData.PredictionDisabled = PredictionDisabledCheckBox.Checked;
      _CustomersTblData.AlwaysSendChkUp = AlwaysSendChkUpCheckBox.Checked;
      _CustomersTblData.NormallyResponds = NormallyRespondsCheckBox.Checked;
      _CustomersTblData.Notes = NotesTextBox.Text;

      return _CustomersTblData;
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
    void UpdateRecord()
    {
      CustomersTbl _ctd = new CustomersTbl();
      string resultStr = (_ctd.UpdateCustomer(GetDataFromForm(), StringToInt64(CompanyIDLabel.Text)));
      if (String.IsNullOrWhiteSpace(resultStr))
        ltrlStatus.Text = "Record Updated";
      else
        ltrlStatus.Text = resultStr;
    }
    void ReturnToPrevPage() { ReturnToPrevPage(false); }

    void ReturnToPrevPage(bool GoToCustomers)
    {
      if ((GoToCustomers) || (String.IsNullOrWhiteSpace(prevPage)))
        Response.Redirect("~/Pages/Customers.aspx");
      else
        Response.Redirect(prevPage);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      UpdateRecord();
    }
    protected void btnUpdateAndReturn_Click(object sender, EventArgs e)
    {
      UpdateRecord();
      ReturnToPrevPage();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      ReturnToPrevPage();
    }

    protected CustomersAccInfoTbl CopyCompanyData2AccInfo(CustomersTbl pCustomer)
    {
      CustomersAccInfoTbl _CustomersAccInfo = new CustomersAccInfoTbl();

      _CustomersAccInfo.CustomerID = pCustomer.CustomerID;

      string[] _BillAddress = pCustomer.BillingAddress.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);

      _CustomersAccInfo.BillAddr1 = (_BillAddress.Length > 0) ? _BillAddress[0].Trim() : string.Empty;
      _CustomersAccInfo.BillAddr2 = (_BillAddress.Length > 1) ? _BillAddress[1].Trim() : string.Empty;
      _CustomersAccInfo.BillAddr3 = (_BillAddress.Length > 2) ? _BillAddress[2].Trim() : string.Empty;
      for (int i = 3; i < _BillAddress.Length; i++)
      {
        _CustomersAccInfo.BillAddr4 = _BillAddress[i].Trim();  //add all the rest to the last line
        if (i + 1 < _BillAddress.Length)
          _CustomersAccInfo.BillAddr4 += ";";
      }
      _CustomersAccInfo.BillAddr5 = pCustomer.PostalCode;

      _CustomersAccInfo.ShipAddr1 = _CustomersAccInfo.BillAddr1;
      _CustomersAccInfo.ShipAddr2 = _CustomersAccInfo.BillAddr2;
      _CustomersAccInfo.ShipAddr3 = _CustomersAccInfo.BillAddr3;
      _CustomersAccInfo.ShipAddr4 = _CustomersAccInfo.BillAddr4;
      _CustomersAccInfo.ShipAddr5 = _CustomersAccInfo.BillAddr5;

      _CustomersAccInfo.AccEmail = pCustomer.EmailAddress;
      _CustomersAccInfo.AltAccEmail = pCustomer.AltEmailAddress;
      _CustomersAccInfo.FullCoName = pCustomer.CompanyName;
      _CustomersAccInfo.AccFirstName = pCustomer.ContactFirstName;
      _CustomersAccInfo.AccLastName = pCustomer.ContactLastName;
      _CustomersAccInfo.AltAccFirstName = pCustomer.ContactAltFirstName;
      _CustomersAccInfo.AltAccLastName = pCustomer.ContactAltLastName;

      return _CustomersAccInfo;
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
      string _ErrorStr = String.Empty;
      CustomersTbl _customerData = GetDataFromForm();
      //CustomersTbl _ctd = new CustomersTbl();
      //      ClientScriptManager _csm = Page.ClientScript;

      if (_customerData.InsertCustomer(_customerData, ref _ErrorStr))
      {
        _customerData = _customerData.GetCustomerByName(_customerData.CompanyName);

        if (_customerData.CustomerID > 0)
        {
          CustomersAccInfoTbl _CustomersAccInfo = CopyCompanyData2AccInfo(_customerData);

          _CustomersAccInfo.Enabled = true;

          string _msg = _CustomersAccInfo.Insert(_CustomersAccInfo);
          if (string.IsNullOrEmpty(_msg))
          {
            showMessageBox _showMsg = new showMessageBox(this.Page, "Insert", "Customer account info added, please edit.");

            Response.Redirect(String.Format("{0}?{1}={2}&{3}=Y", Page.ResolveUrl("~/Pages/CustomerDetails.aspx"), CONST_URL_REQUEST_CUSTOMERID,
              _customerData.CustomerID, CONST_URL_REQUEST_CUSTOMERACCFOCUS));
            /*
                        SetButtonStatus(true);
                        tabcCustomer.Visible = true;
                        tabcCustomer.ActiveTabIndex = 0;
                        lblCustomerID.Text = _CustomersAccInfo.CustomerID.ToString();
                        PlaceAccDataOnForm(_CustomersAccInfo);
                        this.Page.SetFocus(accFullCoNameTextBox);
                        uppnlTabContainer.Update();
            */
          }
          else
          { showMessageBox _showErrMsg = new showMessageBox(this.Page, "Insert", "Error inserting customer account info"); }

          ltrlStatus.Text = "Customer Added";
        }
        else
        {
          string _ScriptToRun = "redirect('" + String.Format("{0}?CompanyName={1}", Page.ResolveUrl("~/Pages/Customers.aspx"), _customerData.CompanyName) + "');";
          ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerInserted", _ScriptToRun, true);
        }
      }
      else
      {
        //        _msgBox.ShowSuccess("Error " + _ErrorStr + ". Customer not added.");

        ltrlStatus.Text = "ERROR: " + _ErrorStr;
      }

    }
    protected void ServerButton_Click(object sender, EventArgs e)
    {
      ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
    }

    protected void btnAddLasOrder_Click(object sender, EventArgs e)
    {
      string _AddLastURL = string.Format("~/Pages/NewOrderDetail.aspx?" + NewOrderDetail.CONST_URL_REQUEST_CUSTOMERID + "={0}&LastOrder=Y", CompanyIDLabel.Text);

      Response.Redirect(_AddLastURL);
    }

    protected void btnForceNext_Click(object sender, EventArgs e)
    {
      TrackerDotNet.classes.TrackerTools _TTools = new TrackerDotNet.classes.TrackerTools();
      DateTime _dt = _TTools.GetClosestNextRoastDate(DateTime.Now.AddDays(14).Date); // add a fortnight;
      ClientUsageTbl _ClientUsage = new ClientUsageTbl();
      _ClientUsage.ForceNextCoffeeDate(_dt.AddDays(3), StringToInt64(CompanyIDLabel.Text));

      CustomersTbl _CustomerTbl = new CustomersTbl();
      _CustomerTbl.IncrementReminderCount(StringToInt64(CompanyIDLabel.Text));

      dgCustomerUsage.DataBind();

      string _ScriptToRun = "showMessage('" + String.Format("{0} force to skip a week of prediction", CompanyNameTextBox.Text) + "');";
      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerInserted", _ScriptToRun, true);
    }

    protected void btnRecalcAverage_Click(object sender, EventArgs e)
    {
      TrackerDotNet.classes.GeneralTrackerDbTools _TrackerDbTools = new GeneralTrackerDbTools();

      _TrackerDbTools.CalcAndSaveNextRequiredDates(StringToInt64(CompanyIDLabel.Text));
      dgCustomerUsage.DataBind();
      upnlNextItems.Update();

      string _ScriptToRun = "showMessage('" + String.Format("{0} average calculations updated", CompanyNameTextBox.Text) + "');";
      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerAverageCalcDone", _ScriptToRun, true);
    }

    protected void tabcCustomer_OnActiveTabChanged(Object sender, EventArgs e)
    {
      Session[CONST_LASTTABCONTER] = tabcCustomer.ActiveTabIndex;
    }
    protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      bool _forceUpdate = false;
      if (e.CommandName == "Delete")
      {
        ItemUsageTbl _ItemUsage = new ItemUsageTbl();
        _ItemUsage.ClientUsageLineNo = Convert.ToInt32(e.CommandArgument);
        _ItemUsage.DeleteItemLine(_ItemUsage.ClientUsageLineNo);
        _forceUpdate = true;
      }
      else if (e.CommandName == "Update")
      {
        int _Idx = Convert.ToInt32(e.CommandArgument);

        TextBox _tbxItemDate = (TextBox)gvItems.Rows[_Idx].FindControl("tbxItemDate");
        DropDownList _ddlItems = (DropDownList)gvItems.Rows[_Idx].FindControl("ddlItems");
        TextBox _tbxAmountProvided = (TextBox)gvItems.Rows[_Idx].FindControl("tbxAmountProvided");
        DropDownList _ddlPackaging = (DropDownList)gvItems.Rows[_Idx].FindControl("ddlPackaging");
        TextBox _tbxPrepTypeID = (TextBox)gvItems.Rows[_Idx].FindControl("tbxPrepTypeID");
        TextBox _tbxNotes = (TextBox)gvItems.Rows[_Idx].FindControl("tbxNotes");
        Label _lblClientUsageLineNo = (Label)gvItems.Rows[_Idx].FindControl("lblClientUsageLineNo");

        ItemUsageTbl _ItemUsage = new ItemUsageTbl();
        _ItemUsage.ItemDate = Convert.ToDateTime(_tbxItemDate.Text);
        _ItemUsage.ItemProvidedID = StringToInt32(_ddlItems.SelectedValue);
        _ItemUsage.AmountProvided = StringToDouble(_tbxAmountProvided.Text);
        _ItemUsage.PackagingID = StringToInt32(_ddlPackaging.SelectedValue);
        _ItemUsage.PrepTypeID = StringToInt32(_tbxPrepTypeID.Text);
        _ItemUsage.Notes = _tbxNotes.Text;
        _ItemUsage.CustomerID = StringToInt64(CompanyIDLabel.Text);
        _ItemUsage.ClientUsageLineNo = StringToInt64(_lblClientUsageLineNo.Text);

        _ItemUsage.UpdateItemsUsed(_ItemUsage);

        _forceUpdate = true;
      }

      if (_forceUpdate)
      {
        //bool _ForceBinds = true;
        //Session[CONST_FORCE_GVITEMS_BIND] = _ForceBinds;
        odsItems.Select();
        gvItems.DataBind();
        upnlItems.Update();
      }
    }
    protected void accPaymentTermsDropDownList_DataBound(object sender, EventArgs e)
    {
      DropDownList _list = (DropDownList)sender;

      if (_list != null)
      {
        if (string.IsNullOrEmpty(_list.SelectedValue))
        {
          long _CustID = GetCustomerIDFromRequest();
          if (_CustID > 0)
          {
            CustomersAccInfoTbl _CustomerPaymentTerms = new CustomersAccInfoTbl();
            _CustomerPaymentTerms.GetByPaymentTypeIDByCustomerID(_CustID);
          }
          else
            _list.SelectedIndex = 1;  // set it to first option
        }
        //if (_list.Items.FindByValue(_list.SelectedValue) == null)
        //{
        //  if (_list.Items.Count > 0)
        //    _list.SelectedValue = _list.Items[0].Value;
        //}

      }

    }
    protected void btnCopy2AccInfo_Click(object sender, EventArgs e)
    {
      CustomersTbl _customerData = GetDataFromForm();
      CustomersAccInfoTbl _CustomersAccInfo = CopyCompanyData2AccInfo(_customerData);

      PlaceAccDataOnForm(_CustomersAccInfo);
      uppnlTabContainer.Update();
      dvCustomersAccInfoUpdatePanel.Update();
    }
    protected void UpdateAccountInfo(CustomersAccInfoTbl pUpdateAccInfo)
    {

      string _err = pUpdateAccInfo.Update(pUpdateAccInfo);

      if (string.IsNullOrEmpty(_err))
      {
        showMessageBox _msg = new showMessageBox(this.Page, "Update", "Customer Account Info Updated");
      }
      else
      {
        showMessageBox _msg = new showMessageBox(this.Page, "Update", "Error updating: " + _err);
      }

    }
    protected void accUpdateButton_Click(object sender, EventArgs e)
    {
      /* was try to sort out update the accinfo id was not being used so we nede to reimport and deliver the data asn use new data.*/

      CustomersAccInfoTbl _UpdatedAccInfo = GetAccDataFromForm();
      UpdateAccountInfo(_UpdatedAccInfo);
    }

    protected void accAddDetailsButton_Click(object sender, EventArgs e)
    {
      CustomersAccInfoTbl _NewAccInfo = GetAccDataFromForm();

      if (_NewAccInfo.CustomersAccInfoID == 0)    // it should be otherwise this button should have been disabled
      {
        string _err = _NewAccInfo.Insert(_NewAccInfo);

        if (string.IsNullOrEmpty(_err))
        {
          showMessageBox _msg = new showMessageBox(this.Page, "Insert", "Customer Account Info Inserted");
          accAddDetailsButton.Enabled = false;
          accUpdateButton.Enabled = true;
          dvCustomersAccInfoUpdatePanel.Update();
        }
        else
        {
          // we got an error, have they been added by someone else?  The the Customer ID exists.
          CustomersAccInfoTbl _CustomersAccInfo = _NewAccInfo.GetByCustomerID(_NewAccInfo.CustomerID);
          if (!_CustomersAccInfo.CustomersAccInfoID.Equals(0))
          {
            _NewAccInfo.CustomersAccInfoID = _CustomersAccInfo.CustomersAccInfoID;
            UpdateAccountInfo(_NewAccInfo);
          }
          else
          {
            showMessageBox _msg = new showMessageBox(this.Page, "Insert", "Error inserting: " + _err);
          }

        }
      }
    }

  }
}