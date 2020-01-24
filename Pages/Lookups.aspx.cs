using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;


namespace QOnT.Pages
{
  public partial class Lookups : System.Web.UI.Page
  {

    const string CONST_ITEMSEARCHSESIONVAR = "SearchItemContains";
    const int CONST_BGCOLOURCOL = 4;
   // const string CONST_PAGEGVCITYDAYSCONTROL = "ctl00$MainContent$tabcLookup$tabpnlCities$gvCityDays$ctl01$";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        tabcLookup.ActiveTabIndex = 0;  // items
        gvCityDays.SelectedIndex = 1;
      }
      //{
      //  MembershipUser _usr = Membership.GetUser();
      //  if ((_usr.UserName == "warren") || (_usr.UserName == "admin") 
      //  {
      //    //        
      //  }
      //}

    }

    protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("AddItem"))
      {
        try
        {
          TextBox _tbxItem = (TextBox)gvItems.FooterRow.FindControl("tbxItem");
          TextBox _tbxSKU = (TextBox)gvItems.FooterRow.FindControl("tbxSKU");
          CheckBox _cbxItemEnabled = (CheckBox)gvItems.FooterRow.FindControl("cbxItemEnabled");
          TextBox _tbxItemCharacteristics = (TextBox)gvItems.FooterRow.FindControl("tbxItemCharacteristics");
          TextBox _tbxItemDetail = (TextBox)gvItems.FooterRow.FindControl("tbxItemDetail");
          DropDownList _ddlServiceType = (DropDownList)gvItems.FooterRow.FindControl("ddlServiceType");
          DropDownList _ddlReplacement = (DropDownList)gvItems.FooterRow.FindControl("ddlReplacement");
          TextBox _tbxItemShortName = (TextBox)gvItems.FooterRow.FindControl("tbxItemShortName");
          TextBox _tbxSortOrder = (TextBox)gvItems.FooterRow.FindControl("tbxSortOrder");
          // add qty and units
          TextBox _tbxUnitsPerQty = (TextBox)gvItems.FooterRow.FindControl("tbxUnitsPerQty");
          DropDownList _ddlUnits = (DropDownList)gvItems.FooterRow.FindControl("ddlUnits");

          // set values depending on if the item is null
          // "INSERT INTO [ItemTypeTbl] ([ItemDesc], [ItemEnabled], [ItemsCharacteritics], [ItemDetail], [ServiceTypeID], [ReplacementID], [ItemShortName], [SortOrder]) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"
          sdsItems.InsertParameters.Clear();
          sdsItems.InsertParameters.Add("ItemDesc", System.Data.DbType.String, _tbxItem.Text);
          sdsItems.InsertParameters.Add("SKU", System.Data.DbType.String, _tbxSKU.Text);
          sdsItems.InsertParameters.Add("ItemEnabled", System.Data.DbType.Boolean, _cbxItemEnabled.Enabled.ToString());
          sdsItems.InsertParameters.Add("ItemCharacteristics", System.Data.DbType.String, _tbxItemCharacteristics.Text);
          sdsItems.InsertParameters.Add("ItemDetail", System.Data.DbType.String, _tbxItemDetail.Text);
          sdsItems.InsertParameters.Add("ServiceTypeID", System.Data.DbType.Int32, _ddlServiceType.SelectedValue.ToString());
          sdsItems.InsertParameters.Add("ReplacementID", System.Data.DbType.Int32, _ddlReplacement.SelectedValue);   // none
          sdsItems.InsertParameters.Add("ItemShortName", System.Data.DbType.String, _tbxItemShortName.Text);
          sdsItems.InsertParameters.Add("SortOrder", System.Data.DbType.Int32, _tbxSortOrder.Text);  // order to display
          sdsItems.InsertParameters.Add("UnitsPerQty", System.Data.DbType.Single, _tbxUnitsPerQty.Text);  // Qty per unit
          sdsItems.InsertParameters.Add("ItemUnitID", System.Data.DbType.Int32, _ddlUnits.SelectedValue);  // unit of this type
          sdsItems.Insert();

          //          gvItems.DataSource = sdsItems;
          gvItems.DataBind();
        }
        catch (Exception ex)
        {
          lblStatus.Text = "Error adding record: " + ex.Message;
        }


        // from http://www.aspdotnetcodes.com/GridView_Insert_Edit_Update_Delete.aspx
      }
    }

    protected void gvPeople_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("AddItem"))
      {
        try
        {
          TextBox _tbxPerson = (TextBox)gvPeople.FooterRow.FindControl("tbxPerson");
          TextBox _tbxAbreviation = (TextBox)gvPeople.FooterRow.FindControl("tbxAbreviation");
          CheckBox _cbxEnabled = (CheckBox)gvPeople.FooterRow.FindControl("cbxEnabled");
          DropDownList _ddlDayOfWeek = (DropDownList)gvPeople.FooterRow.FindControl("ddlDayOfWeek");
          DropDownList _ddlSecurityNames = (DropDownList)gvPeople.FooterRow.FindControl("ddlSecurityNames");

          // set values depending on if the item is null
          control.PersonsTbl _Person = new control.PersonsTbl();

          _Person.Person = _tbxPerson.Text;
          _Person.Abreviation = _tbxAbreviation.Text;
          _Person.Enabled = _cbxEnabled.Checked;
          _Person.NormalDeliveryDoW = Convert.ToInt32(_ddlDayOfWeek.SelectedValue);
          _Person.SecurityUsername = _ddlSecurityNames.SelectedValue;
          _Person.InsertPerson(_Person);

          gvPeople.DataBind();
        }
        catch (Exception ex)
        {
          lblStatus.Text = "Error adding record: " + ex.Message;
        }


      }
    }

    protected void dvItems_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      gvItems.FooterRow.Enabled = false;
      gvItems.DataBind();

      //      gvItems.DataSourceID = "sdsItems";
      //      gvItems.DataBind();
    }

    protected void InsertItemButton_Click(object sender, EventArgs e)
    {
      gvItems.FooterRow.Enabled = true;
      gvItems.DataBind();

      //      gvItems.DataSourceID = "";
      //      gvItems.DataBind();

      // "INSERT INTO [ItemTypeTbl] ([ItemDesc], [ItemEnabled], [ItemsCharacteritics], [ItemDetail], [ServiceTypeID], [ReplacementID], [ItemShortName], [SortOrder]) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"
      //sdsItems.InsertParameters.Clear();
      //sdsItems.InsertParameters.Add("ItemDec",System.Data.DbType.String,"NewItem");
      //sdsItems.InsertParameters.Add("ItemEnabled", System.Data.DbType.Boolean, "false");
      //sdsItems.InsertParameters.Add("ItemCharacteristics", System.Data.DbType.String, "some notes");
      //sdsItems.InsertParameters.Add("ItemDetail",System.Data.DbType.String,"detailed notes");
      //sdsItems.InsertParameters.Add("ServiceTypeID",System.Data.DbType.Int32,"2");  // coffee
      //sdsItems.InsertParameters.Add("ReplacementID",System.Data.DbType.Int32,"0");   // none
      //sdsItems.InsertParameters.Add("ItemShortName",System.Data.DbType.String,"New");
      //sdsItems.InsertParameters.Add("SortOrder",System.Data.DbType.Int32,"5");  // roaster coffee
      //sdsItems.Insert();
      //gvItems.DataBind();
    }
    //    protected void gvEquipment_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //    {
    //      TrackerDotNet.classes.EquipTypeTbl _EquipType = new EquipTypeTbl();
    //      _EquipType.EquipTypeId = Convert.ToInt32(((Label)gvEquipment.Rows[e.RowIndex].FindControl("EquipTypeIdLabel")).Text);
    //      _EquipType.EquipTypeName = ((TextBox)gvEquipment.Rows[e.RowIndex].FindControl("EquipTypeNameTextBox")).Text;
    //      _EquipType.EquipTypeDesc = ((TextBox)gvEquipment.Rows[e.RowIndex].FindControl("EquipTypeDescTextBox")).Text;

    ////      odsEquipTypes.UpdateParameters.Add("objEquipType", _EquipType);
    //      //odsEquipTypes.UpdateParameters.Add("EquipTypeName", _EquipType.EquipTypeName.ToString());
    //      //odsEquipTypes.UpdateParameters.Add("EquipTypeDesc", _EquipType.EquipTypeName.ToString());
    //      // odsEquipTypes.Update();
    //    }
    //    protected void gvEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
    //    {
    //      switch (e.CommandName) 
    //      {
    //        case "Add" :
    //          // Do add stuff
    //          break;
    //        case "Edit":
    //          // Do edit stuff
    //          break;
    //        case "Update" :
    //          int i = 1;
    //          i += 1;
    //          break;
    //        default:
    //          break;
    //      }
    //    }
    protected void gvEquipment_UpdateButton_Click(EventArgs e)
    {
      //
      Response.Write("Do update");
    }
    protected void sdsCities_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    protected void gvEquipment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("Insert"))
      {
        try
        {
          TextBox _tbxEquipTypeName = (TextBox)gvEquipment.FooterRow.FindControl("EquipTypeNameTextBox");
          TextBox _tbxEquipTypeDesc = (TextBox)gvEquipment.FooterRow.FindControl("EquipTypeDescTextBox");

          TrackerDotNet.control.EquipTypeTbl _et = new TrackerDotNet.control.EquipTypeTbl();
          _et.EquipTypeName = _tbxEquipTypeName.Text;
          _et.EquipTypeDesc = _tbxEquipTypeDesc.Text;

          _et.InsertEquipObj(_et);

          //          odsEquipTypes.InsertParameters.Clear();
          ////          odsEquipTypes.InsertParameters["pEquipTypeName"].DefaultValue = _tbxEquipTypeName.Text;
          ////          odsEquipTypes.InsertParameters["pEquipTypeDesc"].DefaultValue = _tbxEquipTypeDesc.Text;
          //         odsEquipTypes.Insert();

          gvEquipment.DataBind();
        }
        catch (Exception ex)
        {
          lblStatus.Text = "Error adding record: " + ex.Message;
        }
      }
    }
    protected void odsEquipTypes_OnInserting(object source, ObjectDataSourceMethodEventArgs e)
    {
      System.Collections.IDictionary paramsFromPage = e.InputParameters;
      TrackerDotNet.control.EquipTypeTbl _et = new TrackerDotNet.control.EquipTypeTbl();

      _et.EquipTypeName = paramsFromPage["EquipTypeName"].ToString();
      _et.EquipTypeDesc = paramsFromPage["EquipTypeDesc"].ToString();

      paramsFromPage.Clear();
      paramsFromPage.Add("objEquipType", _et);
    }
    void DoItemSearch()
    {
      string _ItemSearchSessionVar = tbxItemSearch.Text;

      if (String.IsNullOrEmpty(_ItemSearchSessionVar))
        _ItemSearchSessionVar = "%";
      else
        _ItemSearchSessionVar = "%" + _ItemSearchSessionVar + "%";

      Session[CONST_ITEMSEARCHSESIONVAR] = _ItemSearchSessionVar;

      sdsItems.DataBind();
      gvItems.DataBind();
      upnlItems.Update();
    }

    protected void tbxItemSearch_TextChanged(object sender, EventArgs e)
    {
      DoItemSearch();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
      DoItemSearch();
    }
    protected void gvPackaging_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType.Equals(DataControlRowType.DataRow))
      {
        control.PackagingTbl _pt = (control.PackagingTbl)e.Row.DataItem;

        if (!String.IsNullOrEmpty(_pt.BGColour))
        {
          try
          {
            System.Drawing.Color _col = System.Drawing.ColorTranslator.FromHtml(_pt.BGColour);
            e.Row.Cells[CONST_BGCOLOURCOL].BackColor = _col;
          }
          catch (Exception _ex)
          {
            lblStatus.Text = _ex.Message;
          }
        }
      }
    }
    protected void gvPackaging_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("Insert"))
      {
        try
        {
          TextBox _tbxPackagingDesc = (TextBox)gvPackaging.FooterRow.FindControl("TextBoxDescription");
          TextBox _tbxAdditionalNotes = (TextBox)gvPackaging.FooterRow.FindControl("TextBoxAdditionalNotes");
          TextBox _tbxBGColour = (TextBox)gvPackaging.FooterRow.FindControl("TextBoxBGColour");
          TextBox _tbxColour = (TextBox)gvPackaging.FooterRow.FindControl("TextBoxColour");
          TextBox _tbxSymbol = (TextBox)gvPackaging.FooterRow.FindControl("TextBoxSymbol");

          TrackerDotNet.control.PackagingTbl _pt = new TrackerDotNet.control.PackagingTbl();
          _pt.Description = _tbxPackagingDesc.Text;
          _pt.AdditionalNotes = _tbxAdditionalNotes.Text;
          _pt.BGColour = _tbxBGColour.Text;
          _pt.Colour = String.IsNullOrEmpty(_tbxColour.Text) ? 0 : Convert.ToInt32(_tbxColour.Text);
          _pt.Symbol = _tbxColour.Text;

          _pt.InsertPackaging(_pt);

          gvPackaging.DataBind();
        }
        catch (Exception ex)
        {
          lblStatus.Text = "Error adding record: " + ex.Message;
        }


        // from http://www.aspdotnetcodes.com/GridView_Insert_Edit_Update_Delete.aspx
      }
    }
    protected void ColorPickerExtBGColour_OnClientColorSelectionChanged(object sender, EventArgs e)
    {
      TextBox _tbxBGColour = (TextBox)gvPackaging.FindControl("TextBoxBGColour");
      _tbxBGColour.Text = "#" + _tbxBGColour.Text;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
      tbxItemSearch.Text = String.Empty;
      DoItemSearch();
    }

    protected void gvCities_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("AddCity"))
      {
        try
        {
          TextBox _tbxCity = (TextBox)gvCities.FooterRow.FindControl("tbxCity");
          sdsCities.InsertParameters.Clear();
          sdsCities.InsertParameters.Add("City", System.Data.DbType.String, _tbxCity.Text);
          sdsCities.Insert();

          //          gvItems.DataSource = sdsItems;
          gvItems.DataBind();
        }
        catch (Exception ex)
        {
          lblStatus.Text = "Error adding record: " + ex.Message;
        }


        // from http://www.aspdotnetcodes.com/GridView_Insert_Edit_Update_Delete.aspx
      }
    }
    protected void gvCities_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      if (gvCities.SelectedDataKey.Values.Count > 0)
      {
        gvCityDays.Visible = true;
      }
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
      DropDownList _ddlPreperationDoW = (DropDownList)this.gvCityDays.Controls[0].Controls[0].Controls[0].FindControl("ddlPreperationDoW");;
      TextBox _tbxDeliveryDelay = (TextBox)this.gvCityDays.Controls[0].Controls[0].Controls[0].FindControl("tbxDeliveryDelay");
      TextBox _tbxDeliveryOrder = (TextBox)this.gvCityDays.Controls[0].Controls[0].Controls[0].FindControl("tbxDeliveryOrder");
      int _CityID = Convert.ToInt32(gvCities.SelectedDataKey.Value);

      control.CityPrepDaysTbl _CityPrepDays = new control.CityPrepDaysTbl();
      _CityPrepDays.CityID = _CityID;
      _CityPrepDays.PrepDayOfWeekID = Convert.ToByte(_ddlPreperationDoW.SelectedValue);
      _CityPrepDays.DeliveryDelayDays = Convert.ToInt32(_tbxDeliveryDelay.Text);
      _CityPrepDays.DeliveryOrder = Convert.ToInt32(_tbxDeliveryOrder.Text);

      _CityPrepDays.InsertCityPrepDay(_CityPrepDays);
      gvCityDays.DataBind();

    }

    //  protected void gvCities_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //  {
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //      //int _CityID = Convert.ToInt32(gvCities.DataKeys[e.Row.RowIndex].Value.ToString());
    //      //// odsCityPrepDays.SelectParameters["pCityID"].DefaultValue = _CityID.ToString();

    //      //GridView _gvCityDays = (GridView)e.Row.FindControl("gvCityDays");


    //      int _CityID = Convert.ToInt32(gvCities.DataKeys[e.Row.RowIndex].Value.ToString());
    //      GridView _gvCityDays = e.Row.FindControl("gvCityDays") as GridView;

    //      control.CityPrepDaysTbl _CityPrepDays = new control.CityPrepDaysTbl();
    //      _gvCityDays.DataSource = _CityPrepDays.GetAllByCityId(_CityID);
    //      _gvCityDays.DataBind();
    //    }
    //  }

    protected void gvCityDays_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      //int _EdittedRowIdx = 0;

      //while ((_EdittedRowIdx < gvCityDays.Rows.Count) && (gvCityDays.Rows[_EdittedRowIdx].RowState != DataControlRowState.Edit))
      //  _EdittedRowIdx++;

      //if (_EdittedRowIdx < gvCityDays.Rows.Count)
      //{
      //  DropDownList _ddlPreperationDoW = (DropDownList)gvCityDays.Rows[_EdittedRowIdx].FindControl("ddlPreperationDoW");
      //  TextBox _tbxDeliveryDelay = (TextBox)gvCityDays.Rows[_EdittedRowIdx].FindControl("tbxDeliveryDelay");
      //  TextBox _tbxDeliveryOrder = (TextBox)gvCityDays.Rows[_EdittedRowIdx].FindControl("tbxDeliveryOrder");
      //  Label _lblCityPrepDayID = (Label)gvCityDays.Rows[_EdittedRowIdx].FindControl("CityPrepDaysIDLabel");
      //  int _CityID = Convert.ToInt32(gvCities.SelectedDataKey.Value);

      //  control.CityPrepDaysTbl _CityPrepDays = new control.CityPrepDaysTbl();
      //  _CityPrepDays.CityID = _CityID;
      //  _CityPrepDays.CityPrepDaysID = Convert.ToByte(_ddlPreperationDoW.SelectedValue);
      //  _CityPrepDays.DeliveryDelayDays = Convert.ToInt32(_tbxDeliveryDelay.Text);
      //  _CityPrepDays.CityPrepDaysID = Convert.ToInt32(_lblCityPrepDayID.Text);
      //  _CityPrepDays.DeliveryOrder = Convert.ToInt32(_tbxDeliveryOrder.Text);

      //  _CityPrepDays.UpdateCityPrepDay(_CityPrepDays);
      //  gvCityDays.DataBind();

      //}
      //// odsEquipTypes.Update();
    }
    protected void gvCityDays_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("Update") || e.CommandName.Equals("AddCityDays"))
      {
        GridViewRow _EdittedRow = (e.CommandName.Equals("Update")) ? gvCityDays.Rows[gvCityDays.EditIndex] : gvCityDays.FooterRow;

        DropDownList _ddlPreperationDoW = (DropDownList)_EdittedRow.FindControl("ddlPreperationDoW");
        TextBox _tbxDeliveryDelay = (TextBox)_EdittedRow.FindControl("tbxDeliveryDelay");
        TextBox _tbxDeliveryOrder = (TextBox)_EdittedRow.FindControl("tbxDeliveryOrder");
        Label _lblCityPrepDayID = (Label)_EdittedRow.FindControl("CityPrepDaysIDLabel");
        int _CityID = Convert.ToInt32(gvCities.SelectedDataKey.Value);

        control.CityPrepDaysTbl _CityPrepDays = new control.CityPrepDaysTbl();
        _CityPrepDays.CityID = _CityID;
        _CityPrepDays.PrepDayOfWeekID = Convert.ToByte(_ddlPreperationDoW.SelectedValue);
        _CityPrepDays.DeliveryDelayDays = Convert.ToInt32(_tbxDeliveryDelay.Text);
        _CityPrepDays.CityPrepDaysID = Convert.ToInt32(_lblCityPrepDayID.Text);
        _CityPrepDays.DeliveryOrder = Convert.ToInt32(_tbxDeliveryOrder.Text);

        if (e.CommandName.Equals("Update"))
          _CityPrepDays.UpdateCityPrepDay(_CityPrepDays);
        else
          _CityPrepDays.InsertCityPrepDay(_CityPrepDays);

        gvCityDays.DataBind();
        upnlCities.Update();
      }
      else if (e.CommandName.Equals("Delete"))
      {
        GridViewRow _row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        Label _lblCityPrepDayID = (Label)_row.FindControl("CityPrepDaysIDLabel");
        control.CityPrepDaysTbl _CityPrepDays = new control.CityPrepDaysTbl();
        _CityPrepDays.CityPrepDaysID = Convert.ToInt32(_lblCityPrepDayID.Text);

        _CityPrepDays.DeleteByCityPrepDayID(_CityPrepDays.CityPrepDaysID);

        gvCityDays.DataBind();

        upnlCities.Update();

      }
    }

    public string GetDeliveryDay(string pPredDoW, string pDeliveryDelay)
    {
      int _PrepDoW = 0;
      int _Delay = 0;
      if (!Int32.TryParse(pPredDoW, out _PrepDoW))
        _PrepDoW = 1;
      if (!Int32.TryParse(pDeliveryDelay, out _Delay))
        _Delay = 1;

      int _WhichDoW = _PrepDoW + _Delay;

      string[] _DaysOfWeek = new string[7] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

      if (_WhichDoW > 7)
        _WhichDoW = _WhichDoW - 7;

      return _DaysOfWeek[_WhichDoW - 1];
    }


    //          OnRowEditing="gvPeople_RowEditing" OnRowCancelingEdit="gvPeople_RowCancelingEdit"
    //          OnRowDataBound="gvPeople_OnRowDataBound" OnRowUpdating="gvPeople_OnRowUpdating" >
    //protected void gvPeople_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //  gvPeople.EditIndex = e.NewEditIndex;
    //  gvPeople.DataBind();
    //}
    //protected void gvPeople_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //  gvPeople.EditIndex = -1;
    //  gvPeople.DataBind();
    //}
    //protected void gvPeople_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //  if (e.Row.RowType == DataControlRowType.DataRow && gvPeople.EditIndex == e.Row.RowIndex)
    //  {
    //    DropDownList _ddlPeople = (DropDownList)e.Row.FindControl("ddlPeople");
    //    // loop through the membership and add names

    //  }
    //}

    protected void gvInvoiceTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow _row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
      if (_row != null)
      {
        TextBox _InvoiceTypeDescTextBox = (TextBox)_row.FindControl("InvoiceTypeDescTextBox");
        if ((_InvoiceTypeDescTextBox != null) && (!String.IsNullOrEmpty(_InvoiceTypeDescTextBox.Text)))
        {
          control.InvoiceTypeTbl _InvoiceType = new control.InvoiceTypeTbl();
          Literal _InvoiceTypeIDLiteral = (Literal)_row.FindControl("InvoiceTypeIDLiteral");
          _InvoiceType.InvoiceTypeID = (_InvoiceTypeIDLiteral != null) ? Convert.ToInt32(_InvoiceTypeIDLiteral.Text) : 0;
          if (e.CommandName.Equals("Delete"))
          {
            _InvoiceType.Delete(_InvoiceType.InvoiceTypeID);
          }
          else
          {
            CheckBox _EnabledCheckBox = (CheckBox)_row.FindControl("EnabledCheckBox");
            TextBox _NotesTextBox = (TextBox)_row.FindControl("NotesTextBox");

            _InvoiceType.InvoiceTypeDesc = _InvoiceTypeDescTextBox.Text;
            _InvoiceType.Enabled = (_EnabledCheckBox != null) ? _EnabledCheckBox.Checked : false;
            _InvoiceType.Notes = (_NotesTextBox != null) ? _NotesTextBox.Text : string.Empty;

            if (e.CommandName.Equals("Add") || e.CommandName.Equals("Insert"))
            {
              _InvoiceType.Insert(_InvoiceType);
            }
            else if (e.CommandName.Equals("Update"))
            {
              _InvoiceType.Update(_InvoiceType, _InvoiceType.InvoiceTypeID);
            }
          }

          gvInvoiceTypes.DataBind();

        }
      }


    }

    protected void gvPriceLevels_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow _row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
      if (_row != null)
      {
        TextBox _PriceLevelDescTextBox = (TextBox)_row.FindControl("PriceLevelDescTextBox");
        if ((_PriceLevelDescTextBox != null) && (!String.IsNullOrEmpty(_PriceLevelDescTextBox.Text)))
        {

          TextBox _PricingFactorTextBox = (TextBox)_row.FindControl("PricingFactorTextBox");
          CheckBox _EnabledCheckBox = (CheckBox)_row.FindControl("EnabledCheckBox");
          TextBox _NotesTextBox = (TextBox)_row.FindControl("NotesTextBox");
          Literal _PriceLevelIDLiteral = (Literal)_row.FindControl("PriceLevelIDLiteral");
          control.PriceLevelsTbl _PriceLevel = new control.PriceLevelsTbl();

          _PriceLevel.PriceLevelDesc = _PriceLevelDescTextBox.Text;
          _PriceLevel.PricingFactor = (_PricingFactorTextBox != null) ? Convert.ToSingle(_PricingFactorTextBox.Text) : 1;
          _PriceLevel.Enabled = (_EnabledCheckBox != null) ? _EnabledCheckBox.Checked : false;
          _PriceLevel.Notes = (_NotesTextBox != null) ? _NotesTextBox.Text : string.Empty;
          _PriceLevel.PriceLevelID = (_PriceLevelIDLiteral != null) ? Convert.ToInt32(_PriceLevelIDLiteral.Text) : 0;

          if (e.CommandName.Equals("Add") || e.CommandName.Equals("Insert"))
          {
            _PriceLevel.Insert(_PriceLevel);
          }
          else if (e.CommandName.Equals("Update"))
          {
            _PriceLevel.Update(_PriceLevel, _PriceLevel.PriceLevelID);
          }
          else if (e.CommandName.Equals("Delete"))
          {
            _PriceLevel.Delete(_PriceLevel.PriceLevelID);
          }
          gvPriceLevels.DataBind();

        }
      }
    }

    protected void gvPaymentTerms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow _row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
      if (_row != null)
      {
        TextBox _PaymentTermDescTextBox = (TextBox)_row.FindControl("PaymentTermDescTextBox");
        if ((_PaymentTermDescTextBox != null) && (!String.IsNullOrEmpty(_PaymentTermDescTextBox.Text)))
        {

          TextBox _PaymentDaysTextBox = (TextBox)_row.FindControl("PaymentDaysTextBox");
          TextBox _DayOfMonthTextBox = (TextBox)_row.FindControl("DayOfMonthTextBox");
          CheckBox _UseDaysCheckBox = (CheckBox)_row.FindControl("UseDaysCheckBox");
          CheckBox _EnabledCheckBox = (CheckBox)_row.FindControl("EnabledCheckBox");
          TextBox _NotesTextBox = (TextBox)_row.FindControl("NotesTextBox");
          Literal _PaymentTermIDLiteral = (Literal)_row.FindControl("PaymentTermIDLiteral");
          control.PaymentTermsTbl _PaymentTerms = new control.PaymentTermsTbl();

          _PaymentTerms.PaymentTermDesc = _PaymentTermDescTextBox.Text;
          _PaymentTerms.PaymentDays = (_PaymentDaysTextBox != null) ? Convert.ToInt32(_PaymentDaysTextBox.Text) : 0;
          _PaymentTerms.DayOfMonth = (_DayOfMonthTextBox != null) ? Convert.ToInt32(_DayOfMonthTextBox.Text) : 0;
          _PaymentTerms.UseDays = (_UseDaysCheckBox != null) ? _UseDaysCheckBox.Checked : false;
          _PaymentTerms.Enabled = (_EnabledCheckBox != null) ? _EnabledCheckBox.Checked : true;
          _PaymentTerms.Notes = (_NotesTextBox != null) ? _NotesTextBox.Text : string.Empty;
          _PaymentTerms.PaymentTermID = (_PaymentTermIDLiteral != null) ? Convert.ToInt32(_PaymentTermIDLiteral.Text) : 0;

          if (e.CommandName.Equals("Add") || e.CommandName.Equals("Insert"))
          {
            _PaymentTerms.Insert(_PaymentTerms);
          }
          else if (e.CommandName.Equals("Update"))
          {
            _PaymentTerms.Update(_PaymentTerms, _PaymentTerms.PaymentTermID);
          }
          else if (e.CommandName.Equals("Delete"))
          {
            _PaymentTerms.Delete(_PaymentTerms.PaymentTermID);
          }
          gvPaymentTerms.DataBind();

        }
      }

    }

  }
}