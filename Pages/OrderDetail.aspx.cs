using System;
using System.Configuration;
using System.Data;  //.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
using QOnT.classes;
using QOnT.control;

namespace QOnT.Pages
{
  public partial class OrderDetail : System.Web.UI.Page
  {
    public const string CONST_EMAILDELIMITERSTART = "[#";
    public const string CONST_EMAILDELIMITEREND = "#]";
    public const string CONST_QRYSTR_CUSTOMERID = "CustomerID";
    public const string CONST_QRYSTR_DELIVERYDATE = "DeliveryDate";
    public const string CONST_QRYSTR_NOTES = "Notes";
    public const string CONST_QRYSTR_DELIVERED = "Delivered";
    public const string CONST_QRYSTR_INVOICED = "Invoiced";

    const string CONST_FROMEMAIL = "orders@quaffee.co.za";
    const string CONST_DELIVERYTYPEISCOLLECTION = "Cllct";
    const string CONST_DELIVERYTYPEISCOURIER = "Cour";
    const string CONST_ORDERHEADERVALUES = "OrderHeaderValues";
    
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        long _BoundCustomerId = 1;
        DateTime _BoundDeliveryDate = DateTime.Now.Date;
        String _BoundNotes = String.Empty;

        if (Request.QueryString[CONST_QRYSTR_CUSTOMERID] != null)
          _BoundCustomerId = Convert.ToInt32(Request.QueryString[CONST_QRYSTR_CUSTOMERID].ToString());
        if (Request.QueryString[CONST_QRYSTR_DELIVERYDATE] != null)
          _BoundDeliveryDate = Convert.ToDateTime(Request.QueryString[CONST_QRYSTR_DELIVERYDATE]).Date;
        if (Request.QueryString[CONST_QRYSTR_NOTES ] != null)
          _BoundNotes = Request.QueryString[CONST_QRYSTR_NOTES ].ToString();

        Session[OrderHeaderData.CONST_BOUNDCUSTOMERID] = _BoundCustomerId;
        Session[OrderHeaderData.CONST_BOUNDDELIVERYDATE] = _BoundDeliveryDate.Date;
        Session[OrderHeaderData.CONST_BOUNDNOTES] = _BoundNotes;
        Session[CONST_ORDERHEADERVALUES] = null;

        OrderItemTbl mine_ = new OrderItemTbl();

        // set the permissions
        MembershipUser _currMember = Membership.GetUser();
        btnOrderCancelled.Enabled = (_currMember.UserName.ToLower() == "warren");

        bool _EnableNew =  User.IsInRole("Administrators") || User.IsInRole("AgentManager") || User.IsInRole("Agents");

        btnNewItem.Enabled = _EnableNew;
        TrackerTools _TT = new TrackerTools();

        _TT.SetTrackerSessionErrorString(string.Empty);
        if (Request.QueryString[CONST_QRYSTR_INVOICED] != null)
        {
          if (Request.QueryString[CONST_QRYSTR_INVOICED].Equals("Y"))
            MarkItemAsInvoiced();
        }
        else if (Request.QueryString[CONST_QRYSTR_DELIVERED] != null)
        {
          if (Request.QueryString[CONST_QRYSTR_DELIVERED].Equals("Y"))
            btnOrderDelivered_Click(sender, e);
        }
      }
      else
      {
        TrackerTools _TT = new TrackerTools();
        string _ErrorStr = _TT.GetTrackerSessionErrorString();
        if (!string.IsNullOrEmpty(_ErrorStr))
        {
          showMessageBox _MsgBox = new showMessageBox(this.Page, "Tracker Error", "ERROR: " + _ErrorStr);
          _TT.SetTrackerSessionErrorString(string.Empty);
        }
      }
    }

/*
  void RedirectToOrderDetail()
    {
      string _CustID = dvOrderHeaderGetDDLControlSelectedValue("ddlContacts");
      string _DeliveryDate = dvOrderHeaderGetLabelValue("lblRequiredByDate");
      string _tbxNotes = dvOrderHeaderGetLabelValue("lblNotes");

      if (_CustID != "0")
        Response.Redirect(ResolveUrl("~/Pages/OrderDetail.aspx") + "?" +
                                     String.Format(CONST_QRYSTR_CUSTOMERID+"={0}&"+CONST_QRYSTR_DELIVERYDATE+"={1:d}&"+CONST_QRYSTR_NOTES+"={2}",
                                                   HttpContext.Current.Server.UrlEncode(_CustID),
                                                   Convert.ToDateTime(_DeliveryDate).Date,
                                                   HttpContext.Current.Server.UrlEncode(_tbxNotes)));
    }
    */
    private string GetOrderHeaderRequiredByDateStr()
    {
      string _DeliveryDate = string.Empty;
      if (dvOrderHeader.CurrentMode==DetailsViewMode.Edit)
        _DeliveryDate = dvOrderHeaderGetTextBoxValue("tbxRequiredByDate");
      else 
        _DeliveryDate = dvOrderHeaderGetLabelValue("lblRequiredByDate");
      return _DeliveryDate;
    }

    private string GetOrderHeaderNotes()
    {
      string _Notes = string.Empty;
      if (dvOrderHeader.CurrentMode==DetailsViewMode.Edit)
        _Notes = dvOrderHeaderGetTextBoxValue("tbxNotes");
      else
        _Notes = dvOrderHeaderGetLabelValue("lblNotes");
      return _Notes;
    }
    private void BindRowQueryParameters()
    {
      string _CustID = dvOrderHeaderGetDDLControlSelectedValue("ddlContacts");
      
      Int64 _BoundCustomerId = Convert.ToInt64(_CustID);
      Session[OrderHeaderData.CONST_BOUNDCUSTOMERID] = _BoundCustomerId;
      DateTime _BoundDeliveryDate = Convert.ToDateTime(GetOrderHeaderRequiredByDateStr()).Date;
      Session[OrderHeaderData.CONST_BOUNDDELIVERYDATE] = _BoundDeliveryDate.Date;
      string _BoundNotes = GetOrderHeaderNotes();
      Session[OrderHeaderData.CONST_BOUNDNOTES] = _BoundNotes;

//      System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
      // make collection editable
//      isreadonly.SetValue(this.Request.QueryString, false, null);
      // set url vars
      UriBuilder uriBuilder = new UriBuilder(Request.Url);
      System.Collections.Specialized.NameValueCollection _qs = HttpUtility.ParseQueryString(uriBuilder.Query); 
      _qs.Set(CONST_QRYSTR_CUSTOMERID, _CustID);
      _qs.Set(CONST_QRYSTR_DELIVERYDATE, string.Format("{0:yyyy-MM-dd}", _BoundDeliveryDate));
      _qs.Set(CONST_QRYSTR_NOTES,_BoundNotes);
      uriBuilder.Query = _qs.ToString();

      string _ScriptToRun = "ChangeUrl('OrderDetail','"+uriBuilder.Uri.ToString()+"')";

      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Order Detail", _ScriptToRun, true);     
    }
    protected void btnNewItem_Click(object sender, EventArgs e)
    {
      // show all buttons and text entry
      btnAdd.Visible = true;
      btnCancel.Visible = true;
      pnlNewItem.Visible = true;
      // now hide new button
      btnNewItem.Visible = false;
//      ddlNewItemDesc.DataBind();
      upnlNewOrderItem.Update();
//      ddlNewPackaging.DataBind();

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
      odsOrderDetail.DataBind();
      gvOrderLines.DataBind();
      upnlOrderLines.Update();
    }
    
    private string dvOrderHeaderGetDDLControlSelectedValue(string pDDLControlName)
    {
      DropDownList thisDDL = (DropDownList)dvOrderHeader.FindControl(pDDLControlName);

      return (thisDDL.SelectedValue != null) ? thisDDL.SelectedValue : "0"; 
    }
    private string dvOrderHeaderGetTextBoxValue(string pTextBoxControlName)
    {
      TextBox thisTextBox = (TextBox)dvOrderHeader.FindControl(pTextBoxControlName);

      return (thisTextBox == null) ? String.Empty : thisTextBox.Text;
    }
    private string dvOrderHeaderGetLabelValue(string pTextBoxControlName)
    {
      Label thisLabel = (Label)dvOrderHeader.FindControl(pTextBoxControlName);

      return (thisLabel == null) ? String.Empty : thisLabel.Text;
    }
    private bool dvOrderHeaderGetCheckBoxValue(string pCheckBoxControlName)
    {
      CheckBox thisCheckBox = (CheckBox)dvOrderHeader.FindControl(pCheckBoxControlName);

      return (thisCheckBox ==null) ? false : thisCheckBox.Checked;
    }

    private OrderHeaderData Get_dvOrderHeaderData(bool pInEditMode)
    {
      TrackerDotNet.control.OrderHeaderData _OrderHeader = new OrderHeaderData();

      _OrderHeader.CustomerID = Convert.ToInt64(dvOrderHeaderGetDDLControlSelectedValue("ddlContacts"));
      _OrderHeader.ToBeDeliveredBy = Convert.ToInt64(dvOrderHeaderGetDDLControlSelectedValue("ddlToBeDeliveredBy"));
      _OrderHeader.Confirmed = dvOrderHeaderGetCheckBoxValue("cbxConfirmed");
      _OrderHeader.Done = dvOrderHeaderGetCheckBoxValue("cbxDone");

      string _RequiredByDate = string.Empty;
      string _OrderDate = string.Empty;
      string _RoastDate = string.Empty;
      if (pInEditMode)
      {
        _OrderDate = dvOrderHeaderGetTextBoxValue("tbxOrderDate");
        _RoastDate = dvOrderHeaderGetTextBoxValue("tbxRoastDate");
        _RequiredByDate = dvOrderHeaderGetTextBoxValue("tbxRequiredByDate");
        _OrderHeader.Notes = dvOrderHeaderGetTextBoxValue("tbxNotes");
      }
      else
      {
        
        _RequiredByDate = dvOrderHeaderGetLabelValue("lblRequiredByDate");
        _OrderDate = dvOrderHeaderGetLabelValue("lblOrderDate");
        _RoastDate = dvOrderHeaderGetLabelValue("lblRoastDate");
        _OrderHeader.Notes = dvOrderHeaderGetLabelValue("lblNotes");
      }

      _OrderHeader.RequiredByDate = (string.IsNullOrEmpty(_RequiredByDate) ? DateTime.MinValue : Convert.ToDateTime(_RequiredByDate).Date);
      _OrderHeader.OrderDate = (string.IsNullOrEmpty(_OrderDate) ? DateTime.MinValue : Convert.ToDateTime(_OrderDate).Date);
      _OrderHeader.RoastDate = (string.IsNullOrEmpty(_RoastDate) ? DateTime.MinValue : Convert.ToDateTime(_RoastDate).Date);

      return _OrderHeader;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      string _ErrorStr = "Connection not openned";

      OrderTbl _OrderTbl = new OrderTbl();

      OrderHeaderData _OrderHeaderData = Get_dvOrderHeaderData(false);
      OrderTblData _OrderData = new OrderTblData();

        // first summary data
      _OrderData.CustomerId = _OrderHeaderData.CustomerID;
      _OrderData.OrderDate = _OrderHeaderData.OrderDate;
      _OrderData.RoastDate = _OrderHeaderData.RoastDate;
      _OrderData.RequiredByDate = _OrderHeaderData.RequiredByDate;
      _OrderData.ToBeDeliveredBy = Convert.ToInt32(_OrderHeaderData.ToBeDeliveredBy);
      _OrderData.PurchaseOrder = _OrderHeaderData.PurchaseOrder;
      _OrderData.Confirmed = _OrderHeaderData.Confirmed;
      _OrderData.InvoiceDone = _OrderHeaderData.InvoiceDone;
      _OrderData.Done = _OrderHeaderData.Done;
      _OrderData.PurchaseOrder = _OrderHeaderData.PurchaseOrder;
      _OrderData.Notes = _OrderHeaderData.Notes;

        // Now line data
      TrackerTools _TT = new TrackerTools();
      _OrderData.ItemTypeID = Convert.ToInt32(ddlNewItemDesc.SelectedValue);
      _OrderData.ItemTypeID = _TT.ChangeItemIfGroupToNextItemInGroup(_OrderData.CustomerId, _OrderData.ItemTypeID, _OrderData.RequiredByDate);
      _OrderData.QuantityOrdered = Convert.ToDouble(tbxNewQuantityOrdered.Text);
      _OrderData.PackagingID= Convert.ToInt32(ddlNewPackaging.SelectedValue);

      _ErrorStr = _OrderTbl.InsertNewOrderLine(_OrderData);
      ltrlStatus.Text = (String.IsNullOrWhiteSpace(_ErrorStr) ? "Item Added" : "Error adding item: " + _ErrorStr);
      HideNewOrderItemPanel();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      HideNewOrderItemPanel();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
    }

    protected void gvOrderLines_OnItemDelete(object sender, EventArgs e)
    {
      CommandEventArgs cea = (CommandEventArgs)e;
      string _OrderId = cea.CommandArgument.ToString();
      string _sqlDeleteCmd = "DELETE FROM OrdersTbl WHERE (OrderID = ?)";
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(_OrderId, DbType.String, "@OrderID");
      string _ErrorStr = _TDB.ExecuteNonQuerySQL(_sqlDeleteCmd);
      _TDB.Close();
      ltrlStatus.Text = (string.IsNullOrEmpty(_ErrorStr) ? "Item Deleted" : "Error deleting item: " + _ErrorStr);
    }

    protected void gvOrderLines_RowUpdated(Object sender, GridViewUpdatedEventArgs e)
    {
//      GridViewRow _gvRow = gvOrderLines.Rows[e.AffectedRows];
//
//      DropDownList _ddlContacts = (DropDownList)_gvRow.FindControl("ddlContacts");
//      TextBox _tbxQuantityOrdered = (TextBox)_gvRow.FindControl("tbxQuantityOrdered");

      string _WhatsChanged = String.Empty;

      LogTbl _Log = new LogTbl();
      _Log.AddToWhatsChanged("ItemTypeID",e.OldValues["ItemTypeID"].ToString(),e.NewValues["ItemTypeID"].ToString(),ref _WhatsChanged);
      _Log.AddToWhatsChanged("Qty", e.OldValues["QuantityOrdered"].ToString(), e.NewValues["QuantityOrdered"].ToString(),ref _WhatsChanged);
      _Log.AddToWhatsChanged("PackagingID", e.OldValues["PackagingID"].ToString(), e.NewValues["PackagingID"].ToString(),ref _WhatsChanged);

      _Log.InsertLogItem(Membership.GetUser().UserName, 
        SectionTypesTbl.CONST_ORDERS_SECTION_INT, TransactionTypesTbl.CONST_UPDATE_TRANSACTION_INT,
        Convert.ToInt64(dvOrderHeaderGetDDLControlSelectedValue("ddlContacts")), _WhatsChanged, "Order Detail" );

      odsOrderDetail.DataBind();
      gvOrderLines.DataBind();
      upnlOrderLines.Update();
    }
    
    protected virtual void dvOrderHeader_OnModeChanging(Object sender, DetailsViewModeEventArgs e)
    {
      if (e.NewMode == DetailsViewMode.Edit)
      {
        TrackerDotNet.control.OrderHeaderData _OrderHeader = Get_dvOrderHeaderData(false);
        Session[CONST_ORDERHEADERVALUES] = _OrderHeader; 
      }
      else if (e.NewMode == DetailsViewMode.ReadOnly)
      {
        if (Session[CONST_ORDERHEADERVALUES] != null)  // it has been editted
          dvOrderHeader.DataBind();
        //if (Session[CONST_ORDERHEADERVALUES] != null)
        //{
        //  TrackerDotNet.control.OrderHeaderData _OldOrderHeader = (OrderHeaderData)Session[CONST_ORDERHEADERVALUES];
        //  TrackerDotNet.control.OrderHeaderData _NewOrderHeader = Get_dvOrderHeaderData(true);

        //  LogTbl _Log = new LogTbl();
          
        //  string _WhatsChanged = string.Empty;
      
        //  _Log.AddToWhatsChanged("CustomderID", _OldOrderHeader.CustomerID.ToString(),_NewOrderHeader.CustomerID.ToString(), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("Order Date", string.Format("{0:d}", _OldOrderHeader.OrderDate), string.Format("{0:d}", _NewOrderHeader.OrderDate), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("Roast Date", string.Format("{0:d}", _OldOrderHeader.RoastDate), string.Format("{0:d}", _NewOrderHeader.RoastDate), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("ToBeDeliveredBy", _OldOrderHeader.ToBeDeliveredBy.ToString(), _NewOrderHeader.ToBeDeliveredBy.ToString(), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("RequiredBy Date", string.Format("{0:d}", _OldOrderHeader.RequiredByDate), string.Format("{0:d}", _NewOrderHeader.RequiredByDate), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("Confirmed", _OldOrderHeader.Confirmed.ToString(), _NewOrderHeader.Confirmed.ToString(), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("Done", _OldOrderHeader.Done.ToString(), _NewOrderHeader.Done.ToString(), ref _WhatsChanged);
        //  _Log.AddToWhatsChanged("Notes", _OldOrderHeader.Notes.ToString(), _NewOrderHeader.Notes.ToString(), ref _WhatsChanged);

        //  _Log.InsertLogItem(Membership.GetUser().UserName,
        //    SectionTypesTbl.CONST_ORDERS_SECTION_INT, TransactionTypesTbl.CONST_UPDATE_TRANSACTION_INT,
        //    _NewOrderHeader.CustomerID, _WhatsChanged, "Order Header Detail");

        //}
      }
    }
    
    protected virtual void dvOrderHeader_OnItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
    {
/*
 * if (Session[CONST_ORDERHEADERVALUES] != null)
      {
        TrackerDotNet.control.OrderHeaderData _OldOrderHeader = (OrderHeaderData)Session[CONST_ORDERHEADERVALUES];


// order data that was stored in session not saving RequiredByDate .        
        TrackerDotNet.control.OrderHeaderData _NewOrderHeader = Get_dvOrderHeaderData(dvOrderHeader.CurrentMode == DetailsViewMode.Edit);

        LogTbl _Log = new LogTbl();

        string _WhatsChanged = string.Empty;

        _Log.AddToWhatsChanged("CustomderID", _OldOrderHeader.CustomerID.ToString(), _NewOrderHeader.CustomerID.ToString(), ref _WhatsChanged);
        _Log.AddToWhatsChanged("Order Date", string.Format("{0:d}", _OldOrderHeader.OrderDate), string.Format("{0:d}", _NewOrderHeader.OrderDate), ref _WhatsChanged);
        _Log.AddToWhatsChanged("Roast Date", string.Format("{0:d}", _OldOrderHeader.RoastDate), string.Format("{0:d}", _NewOrderHeader.RoastDate), ref _WhatsChanged);
        _Log.AddToWhatsChanged("ToBeDeliveredBy", _OldOrderHeader.ToBeDeliveredBy.ToString(), _NewOrderHeader.ToBeDeliveredBy.ToString(), ref _WhatsChanged);
        _Log.AddToWhatsChanged("RequiredBy Date", string.Format("{0:d}", _OldOrderHeader.RequiredByDate), string.Format("{0:d}", _NewOrderHeader.RequiredByDate), ref _WhatsChanged);
        _Log.AddToWhatsChanged("Confirmed", _OldOrderHeader.Confirmed.ToString(), _NewOrderHeader.Confirmed.ToString(), ref _WhatsChanged);
        _Log.AddToWhatsChanged("Done", _OldOrderHeader.Done.ToString(), _NewOrderHeader.Done.ToString(), ref _WhatsChanged);
        _Log.AddToWhatsChanged("Notes", _OldOrderHeader.Notes.ToString(), _NewOrderHeader.Notes.ToString(), ref _WhatsChanged);

        _Log.InsertLogItem(Membership.GetUser().UserName,
          SectionTypesTbl.CONST_ORDERS_SECTION_INT, TransactionTypesTbl.CONST_UPDATE_TRANSACTION_INT,
          _NewOrderHeader.CustomerID, _WhatsChanged, "Order Header Detail");

      }
      odsOrderDetail.DataBind();
      gvOrderLines.DataBind(); */
      upnlOrderLines.Update();
 /* HERE- problem with the update of the order -seems to update the session var but still the view does not update even with a bind? */
    }
    protected virtual void dvOrderHeader_OnDataBound(Object sender, EventArgs e)
    {
      if (dvOrderHeader.CurrentMode == DetailsViewMode.ReadOnly)
      {
        if (Session[CONST_ORDERHEADERVALUES] != null)
        {
          // this is a update of the header items 
          TrackerDotNet.control.OrderHeaderData _OldOrderHeader = (OrderHeaderData)Session[CONST_ORDERHEADERVALUES];
          string _NewDateStr = GetOrderHeaderRequiredByDateStr();
          if (!_NewDateStr.Equals(string.Empty))
          {
            DateTime _NewRequireDate = Convert.ToDateTime(GetOrderHeaderRequiredByDateStr()).Date;
            if (_OldOrderHeader.OrderDate.Date != _NewRequireDate.Date)
            {
              UsedItemGroupTbl _UsedItems = new UsedItemGroupTbl();
              DropDownList _gvItemDLL = null;
              // chjeck in each item of the grid if the item was a group item
              foreach (GridViewRow _gvr in gvOrderLines.Rows)
              {
                _gvItemDLL = (DropDownList)_gvr.FindControl("ddlItemDesc");

                _UsedItems.UpdateIfGroupItem(_OldOrderHeader.CustomerID, Convert.ToInt32(_gvItemDLL.SelectedValue), _OldOrderHeader.RequiredByDate, _NewRequireDate);
              }
            }
          }
        }

        Label lblPurchaseOrder = (Label)dvOrderHeader.FindControl("lblPurchaseOrder");
        if ((lblPurchaseOrder != null) && (lblPurchaseOrder.Text.Equals(TrackerTools.CONST_POREQUIRED)))
        {
          lblPurchaseOrder.BackColor = System.Drawing.Color.Red;
          lblPurchaseOrder.ForeColor = System.Drawing.Color.White;
        }
      }
      CheckBox thisCheckBox = (CheckBox)dvOrderHeader.FindControl("cbxDone");
      if (thisCheckBox != null)
        btnOrderDelivered.Enabled = btnOrderCancelled.Enabled = !thisCheckBox.Checked;

      updtButtonPanel.Update();
    }

    public void DeleteOrderItem(string pOrderID)
    {
      OrderTbl _Order = new OrderTbl();
      string _ErrorStr = _Order.DeleteOrderById(Convert.ToInt64(pOrderID));
      ltrlStatus.Text = (_ErrorStr.Length == 0) ? "Item deleted" : _ErrorStr;
    }

    private string UnDoneOrderItem(string pOrderID)
    {
      long _OrderID = Convert.ToInt64(pOrderID);

      OrderTbl _Order = new OrderTbl();

      return _Order.UpdateSetDoneByID(false, _OrderID);
    }
    protected void btnCancelled_Click(object sender, EventArgs e)
    {
      MembershipUser _currMember = Membership.GetUser();

      if (_currMember.UserName.ToLower() == "warren")
      {
        // only if it is warren allow this
        foreach (GridViewRow gvr in gvOrderLines.Rows) 
        {
          Label _OrderLabel = (Label)gvr.Cells[4].FindControl("lblOrderID");
          DeleteOrderItem(_OrderLabel .Text);
        }
        Response.Redirect("DeliverySheet.aspx");
      }
    }
    private ContactEmailDetails GetEmailAddressFromNote()
    {
      ContactEmailDetails _contactEmailDetails = null;

      string _Notes = dvOrderHeaderGetLabelValue("lblNotes");

      int _Start = _Notes.IndexOf(CONST_EMAILDELIMITERSTART);
      if (_Start >= 0)
      {
        _contactEmailDetails = new ContactEmailDetails();
        _contactEmailDetails.EmailAddress = _Notes.Substring(_Start + 2, _Notes.IndexOf(CONST_EMAILDELIMITEREND) - _Start - 2);
      }

      return _contactEmailDetails;
    }
    private  ContactEmailDetails GetEmailDetails(string pContactsID)
    {
      ContactEmailDetails _contactEmailDetails = new ContactEmailDetails();
      if (pContactsID.Equals(NewOrderDetail.CONST_ZZNAME_DEFAULTID))
        _contactEmailDetails = GetEmailAddressFromNote();
      else
        _contactEmailDetails = _contactEmailDetails.GetContactsEmailDetails(Convert.ToInt64(pContactsID));
      return _contactEmailDetails;
    }
    string AddUnitsToQty(string pItemTypeID, string pQty)
    {
      string _UoMString = GetItemUoM(Convert.ToInt32(pItemTypeID));

      if (String.IsNullOrEmpty(_UoMString))
        return pQty;
      else
      {
        double _Qty = Convert.ToDouble(pQty);
        return pQty + " " + ((_Qty == 1 ) ? _UoMString : _UoMString+"s");
      }
    }
    int GetItemSortOrderID(string pItemTypeID)
    {
      ItemTypeTbl _ITT = new ItemTypeTbl();

      return _ITT.GetItemSortOrder(Convert.ToInt32(pItemTypeID));
    }
    private string UpCaseFirstLetter(string pString)
    {
      if (string.IsNullOrEmpty(pString))
        return string.Empty;
      else 
        return char.ToUpper(pString[0]) + pString.Substring(1);
    }
    /// <summary>
    /// Send email to the current client to confirm the items in the current order, and the deliver date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirmOrder_Click(object sender, EventArgs e)
    {
      DropDownList _ddlThisContact = (DropDownList)dvOrderHeader.FindControl("ddlContacts");

       ContactEmailDetails _thisContact = GetEmailDetails(_ddlThisContact.SelectedItem.Value);
      if (_thisContact != null)
      {
        EmailCls _email = new EmailCls();
        DropDownList _ddlDeliveryBy = (DropDownList)dvOrderHeader.FindControl("ddlToBeDeliveredBy");  // who is delivering this

        string _DeliveryDate = dvOrderHeaderGetLabelValue("lblRequiredByDate");  // date it will be dispatched / delivered"
        string _PurchaseOrder = dvOrderHeaderGetLabelValue("lblPurchaseOrder");
        string _Notes = dvOrderHeaderGetLabelValue("lblNotes");

        if (_thisContact.EmailAddress != "")
        {
          _email.SetEmailFromTo(CONST_FROMEMAIL, _thisContact.EmailAddress);
          if (_thisContact.altEmailAddress != "")
            _email.SetEmailCC(_thisContact.altEmailAddress);
        }
        else if (_thisContact.altEmailAddress != "")
          _email.SetEmailFromTo(CONST_FROMEMAIL, _thisContact.altEmailAddress);
        else
        {
          ltrlStatus.Text = "no email address found";
          TrackerDotNet.classes.showMessageBox _NoMsgBx = new classes.showMessageBox(this.Page, "Email FAILED: ", ltrlStatus.Text);
          upnlNewOrderItem.Update();
          return;  // no email address so quit
        }
        // send a BCC to orders to confirm
        _email.SetEmailBCC(CONST_FROMEMAIL);
        // set subject and body
        _email.SetEmailSubject("Order Confirmation");

        string _AddressedTo = "Coffee Lover";
        if (_thisContact.FirstName != "")
        {
          _AddressedTo = _thisContact.FirstName.Trim();
          if (_thisContact.altFirstName != "")
            _AddressedTo += " and " + _thisContact.altFirstName.Trim();
        }
        else if (_thisContact.altFirstName != "")
          _AddressedTo = _thisContact.altFirstName.Trim();

        _email.AddStrAndNewLineToBody("Dear " + _AddressedTo + ",<br />");
        if (_ddlThisContact.SelectedValue.Equals(NewOrderDetail.CONST_ZZNAME_DEFAULTID))
          _email.AddStrAndNewLineToBody("We confirm you order below:");
        else
          _email.AddStrAndNewLineToBody("We confirm the following order for "+_ddlThisContact.SelectedItem.Text+":");
        _email.AddToBody("<ul>");
        foreach (GridViewRow _gv in gvOrderLines.Rows)
        {
          DropDownList _gvItemDLL = (DropDownList)_gv.FindControl("ddlItemDesc");
          Label _gvItemQty = (Label)_gv.FindControl("lblQuantityOrdered");
          DropDownList _gvItemPackaging = (DropDownList)_gv.FindControl("ddlPackaging");
          // need to check for serivce / note and add the note using the same logic as we have for the delivery sheet
          if (GetItemSortOrderID(_gvItemDLL.SelectedValue) == ItemTypeTbl.CONST_NEEDDESCRIPTION_SORT_ORDER)
          {
            // if we are already use the notes field for name, check if there is a ":" seperator, and then only use what is after
            if (_Notes.Contains(":"))
              _Notes = _Notes.Substring(_Notes.IndexOf(":") + 1).Trim();

            int _Start = _Notes.IndexOf(OrderDetail.CONST_EMAILDELIMITERSTART);
            if (_Start >= 0)
            {
              int _End = _Notes.IndexOf(OrderDetail.CONST_EMAILDELIMITEREND);
              if (_End >= 0)
                _Notes = String.Concat(_Notes.Substring(0, _Start), ";", _Notes.Substring(_End + 2));
            }

            _email.AddFormatToBody("<li>{0}</li>", _Notes);
          }
          else
          {
            string _UnitsAndQty = AddUnitsToQty(_gvItemDLL.SelectedValue, _gvItemQty.Text);
            if (_gvItemPackaging.SelectedIndex == 0)
              _email.AddFormatToBody("<li>{0} of {1}</li>", _UnitsAndQty, _gvItemDLL.SelectedItem.Text);
            else
              _email.AddFormatToBody("<li>{0} of {1} - Preperation note: {2}</li>", _UnitsAndQty, _gvItemDLL.SelectedItem.Text, _gvItemPackaging.SelectedItem.Text);
          }
        }
        _email.AddStrAndNewLineToBody("</ul>");

        if (!string.IsNullOrEmpty(_PurchaseOrder))
        {
          if (_PurchaseOrder.EndsWith(TrackerTools.CONST_POREQUIRED))
            _email.AddStrAndNewLineToBody("<b>NOTE</b>: We are still waiting for a Purchase Order number from you.<br />");
          else
            _email.AddStrAndNewLineToBody(string.Format("This order has purchase order: {0}, allocated to it.<br />", _PurchaseOrder));
        }
        if (_ddlDeliveryBy.SelectedItem.Text == CONST_DELIVERYTYPEISCOLLECTION)
          _email.AddStrAndNewLineToBody("The order will be ready for collection on: " + _DeliveryDate);
        else if (_ddlDeliveryBy.SelectedItem.Text == CONST_DELIVERYTYPEISCOURIER)
          _email.AddStrAndNewLineToBody("The order will be dispatched on: " + _DeliveryDate + ".");
        else
          _email.AddStrAndNewLineToBody("The order will be delivered on: " + _DeliveryDate + ".");

        // Add a footer
        MembershipUser _currMember = Membership.GetUser();
        string _from = string.IsNullOrEmpty(_currMember.UserName) ? "the Quaffee Team" : " from the Quaffee Team (" + UpCaseFirstLetter(_currMember.UserName) + ")";
        
        _email.AddStrAndNewLineToBody("<br />Sent automatically by Quaffee's order and tracking System.<br /><br />Sincerely "+ _from +" (orders@quaffee.co.za)");
        if (_email.SendEmail())
          ltrlStatus.Text = "Email Sent to: " + _AddressedTo;
        else
        {
          showMessageBox _msg = new showMessageBox(this.Page, "error", "error sending email: " + _email.myResults);
          ltrlStatus.Text = "Email was not sent!";
        }

        TrackerDotNet.classes.showMessageBox _MsgBx = new classes.showMessageBox(this.Page,"Email Confirmation", ltrlStatus.Text);
        upnlNewOrderItem.Update();
      }
    }

    protected void OnDataBinding_ddlToBeDeliveredBy(object sender, EventArgs e)
    {
      DropDownList ddl = (DropDownList)sender; 
      if (ddl != null)
      {
      }

    }
    public string GetToBeDeliveredBy(object pToBeDeliveredBy)
    {
      if (pToBeDeliveredBy != null)
      {
/*
DropDownList _DDL = (DropDownList) dvOrderHeader.FindControl("ddlToBeDeliveredBy");
 */
        string _Value = pToBeDeliveredBy.ToString();
/*
 * if (Convert.ToInt32(_Value) > 100)
          _Value = "0";
 */
        return _Value;
      }
      else
        return "0";
    }
    public string GetItemUoMObj(object pItemID)
    {
      return (pItemID != null) ? GetItemUoM(Convert.ToInt32(pItemID.ToString())): string.Empty;
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
    protected void odsOrderSummary_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {

      ////// stuck here trying to get the project to run, trying to update the session variables so that on update the record does not dissappear.

      if ((bool)e.ReturnValue == true)
      {
        
        DropDownList _ddlContacts = (DropDownList)dvOrderHeader.FindControl("ddlContacts");
        TextBox _tbxRequiredByDate = (TextBox)dvOrderHeader.FindControl("tbxRequiredByDate");
        TextBox _tbxNotes = (TextBox)dvOrderHeader.FindControl("tbxNotes");
        // records where updated so update the session variables.
        if ((_ddlContacts != null) && (_tbxRequiredByDate != null) && (_tbxNotes != null))
        {
          BindRowQueryParameters();
          /*
           * // redirect website using query string ?
            string _OrderDetailURL = String.Format("{0}?"+CONST_QRYSTR_CUSTOMERID+"={1}&"+CONST_QRYSTR_DELIVERYDATE+"={2:d}&"+CONST_QRYSTR_NOTES+"={3}",
                                                   ResolveUrl("~/Pages/OrderDetail.aspx"), 
                                                    HttpContext.Current.Server.UrlEncode(_ddlContacts.SelectedValue),
                                                    Convert.ToDateTime(_tbxRequiredByDate.Text).Date,
                                                    HttpContext.Current.Server.UrlEncode(_tbxNotes.Text));
            Response.Redirect(_OrderDetailURL);
           */
        }
      }
      // get data stored on the forw
      //  ViewState["dvOrderHeader"]

      gvOrderLines.DataBind();
      upnlOrderLines.Update();
    }
    protected void MarkItemAsInvoiced()
    {
      OrderTbl _Orders =  new OrderTbl();
      long _CustomerId = (long)Session[OrderHeaderData.CONST_BOUNDCUSTOMERID];
      DateTime _RequiredByDate = ((DateTime) Session[OrderHeaderData.CONST_BOUNDDELIVERYDATE]).Date;
      string _Notes =  (string) Session[OrderHeaderData.CONST_BOUNDNOTES];

      _Orders.UpdateSetInvoiced(true, _CustomerId, _RequiredByDate, _Notes);
/*      odsOrderSummary.Update();*/
      pnlOrderHeader.Update();
     
    }

    /// <summary>
    /// From the client selected create a temporary table from the items in the order
    /// Then diosplay another for for the user to select how to close the order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOrderDelivered_Click(object sender, EventArgs e)
    {
      // General VARs
      OrderHeaderData _OrderHeaderData = Get_dvOrderHeaderData(false);
      TempOrdersData _TempOrdersData = new TempOrdersData();
      TempOrdersDAL _TempOrdersDAL  = new TempOrdersDAL();
      // DELETE data from TempOrdersHeaderTblData
      if (!_TempOrdersDAL.KillTempOrdersData())
      { ltrlStatus.Text = "Error deleting Temp Table"; }
        
      // add parameters in the order they appear in the update command
      // first summary / header data

      _TempOrdersData.HeaderData.CustomerID = _OrderHeaderData.CustomerID;
      _TempOrdersData.HeaderData.OrderDate = _OrderHeaderData.OrderDate;
      _TempOrdersData.HeaderData.RoastDate = _OrderHeaderData.RoastDate;
      _TempOrdersData.HeaderData.RequiredByDate = _OrderHeaderData.RequiredByDate;
      _TempOrdersData.HeaderData.ToBeDeliveredByID = Convert.ToInt32(_OrderHeaderData.ToBeDeliveredBy);
      _TempOrdersData.HeaderData.Confirmed = _OrderHeaderData.Confirmed;
      _TempOrdersData.HeaderData.Done = _OrderHeaderData.Done;
      _TempOrdersData.HeaderData.Notes = _OrderHeaderData.Notes;
      //_TempOrdersData.HeaderData.CustomerID = Convert.ToInt32(dvOrderHeaderGetDDLControlSelectedValue("ddlContacts"));
      //_TempOrdersData.HeaderData.OrderDate = Convert.ToDateTime(dvOrderHeaderGetLabelValue("lblOrderDate"));
      //_TempOrdersData.HeaderData.RoastDate = Convert.ToDateTime(dvOrderHeaderGetLabelValue("lblRoastDate"));
      //_TempOrdersData.HeaderData.RequiredByDate = Convert.ToDateTime(dvOrderHeaderGetLabelValue("lblRequiredByDate"));
      //_TempOrdersData.HeaderData.ToBeDeliveredByID = Convert.ToInt32(dvOrderHeaderGetDDLControlSelectedValue("ddlToBeDeliveredBy"));
      //_TempOrdersData.HeaderData.Confirmed = dvOrderHeaderGetCheckBoxValue("cbxConfirmed");
      //_TempOrdersData.HeaderData.Done = dvOrderHeaderGetCheckBoxValue("cbxDone");
      //_TempOrdersData.HeaderData.Notes = dvOrderHeaderGetLabelValue("lblNotes");

      // now the line data (the TO header is set when we add both using the one class TempOrders
      ItemTypeTbl _ItemType = new ItemTypeTbl();
      foreach (GridViewRow _gv in gvOrderLines.Rows)
      {
        TempOrdersLinesTbl _LineData = new TempOrdersLinesTbl();

        DropDownList _gvItemDesc = (DropDownList)_gv.FindControl("ddlItemDesc");
        Label _gvItemQty = (Label)_gv.FindControl("lblQuantityOrdered");
        DropDownList _gvItemPackaging = (DropDownList)_gv.FindControl("ddlPackaging");
        Label _gvOrderID = (Label)_gv.FindControl("lblOrderID");

        _LineData.ItemID = Convert.ToInt32(_gvItemDesc.SelectedValue);
        _LineData.Qty = Convert.ToDouble(_gvItemQty.Text);
        _LineData.PackagingID = Convert.ToInt32(_gvItemPackaging.SelectedValue);
        _LineData.ServiceTypeID = _ItemType.GetServiceID(_LineData.ItemID);
        _LineData.OriginalOrderID = Convert.ToInt64(_gvOrderID.Text);
           
        _TempOrdersData.OrdersLines.Add(_LineData);
      }

      // now add all the data to the database
      _TempOrdersDAL.Insert(_TempOrdersData);
      // open new form with database, p 
      Response.Redirect("OrderDone.aspx");
    }

    protected void btnUnDoDone_Click(object sender, EventArgs e)
    {
      Label _OrderLabel;
      string _Result = String.Empty;
      foreach (GridViewRow gvr in gvOrderLines.Rows)
      {
        _OrderLabel = (Label)gvr.Cells[4].FindControl("lblOrderID");
        _Result += UnDoneOrderItem(_OrderLabel.Text);
      }
      ltrlStatus.Text = _Result;

      dvOrderHeader.DataBind();
      pnlOrderHeader.Update();
      gvOrderLines.DataBind();
      upnlOrderLines.Update();
    }

    protected void gvOrderLines_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      bool _UpdateGrids = false;
      if (e.CommandName == "MoveOneDayOn")
      {
        _UpdateGrids = true;
        int index = Convert.ToInt32(e.CommandArgument);
        Label _OrderLabel = (Label)gvOrderLines.Rows[index].FindControl("lblOrderID");
        string _DeliveryDateStr = dvOrderHeaderGetLabelValue("lblRequiredByDate");
        DateTime _DeliveryDate = Convert.ToDateTime(_DeliveryDateStr).Date;

        if (_DeliveryDate.DayOfWeek < DayOfWeek.Friday)
          _DeliveryDate = _DeliveryDate.AddDays(1);
        else
        {
          int _daysUntilMonday = ((int) DayOfWeek.Monday - (int) _DeliveryDate.DayOfWeek + 7) % 7;
          _DeliveryDate = _DeliveryDate.AddDays(_daysUntilMonday);
        }

        OrderTbl _OrderTbl = new OrderTbl();
        _OrderTbl.UpdateOrderDeliveryDate(_DeliveryDate, Convert.ToInt64(_OrderLabel.Text));
      }
      else if (e.CommandName == "DeleteOrder")
      {
        _UpdateGrids = true;
        DeleteOrderItem(e.CommandArgument.ToString());
      }
      if (_UpdateGrids)
      {
        dvOrderHeader.DataBind();
        pnlOrderHeader.Update();
        gvOrderLines.DataBind();
        upnlOrderLines.Update();
      }
    }

    protected void ddlItemDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
  }
}