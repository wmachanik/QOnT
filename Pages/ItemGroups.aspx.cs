using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;

namespace QOnT.Pages
{
  public partial class ItemGroups : System.Web.UI.Page
  {
    const string CONST_SESSION_LASTIDSELECTED = "LastGroupIDSelected";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session[CONST_SESSION_LASTIDSELECTED] != null)
        {
          ddlGroupItems.DataBind(); // bind here so we can change the index
          string _IDSelected = (string)Session[CONST_SESSION_LASTIDSELECTED];
          if (ddlGroupItems.Items.FindByValue(_IDSelected) != null)
          {
            ddlGroupItems.SelectedValue = _IDSelected;
          }
        }
      }
    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)  // once page is Complete
    {
     
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
      Response.Redirect("GroupItemDetail.aspx");
      ddlGroupItems.DataBind();
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
      // for each item that is selected remove from list
      foreach (GridViewRow _row in gvItemsNotInGroup.Rows)
      {
        // Access the CheckBox
        CheckBox _cbxAddItem = (CheckBox)_row.FindControl("cbxAddItem");
        if (_cbxAddItem != null && _cbxAddItem.Checked)
        {
          DropDownList _ddlItem = (DropDownList)_row.FindControl("ddlItemTypeDesc");

          TrackerDotNet.control.ItemGroupTbl _ITG = new control.ItemGroupTbl();
          _ITG.GroupItemTypeID = Convert.ToInt32(ddlGroupItems.SelectedValue);
          _ITG.ItemTypeID = Convert.ToInt32(_ddlItem.SelectedValue);
          _ITG.ItemTypeSortPos = _ITG.GetLastGroupItemSortPos(_ITG.GroupItemTypeID) + 1;
          _ITG.Enabled = true;
          _ITG.Notes = "added on ItemGroup form";
          _ITG.InsertItemGroup(_ITG);
        }
      }
      gvItemsInList.DataBind();
      gvItemsNotInGroup.DataBind();
      updtPnlItemsInList.Update();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
      // for each item that is selected remove from list
      foreach (GridViewRow row in gvItemsInList.Rows)
      {
        // Access the CheckBox
        CheckBox _cbxRemoveItem = (CheckBox)row.FindControl("cbxRemoveItem");
        if (_cbxRemoveItem != null && _cbxRemoveItem.Checked)
        {
          DropDownList _ddlItem = (DropDownList)row.FindControl("ddlItemDesc");

          TrackerDotNet.control.ItemGroupTbl _ITG = new control.ItemGroupTbl();
          _ITG.DeleteGroupItemFromGroup(Convert.ToInt32(ddlGroupItems.SelectedValue), Convert.ToInt32(_ddlItem.SelectedValue));
        }
      }
      gvItemsInList.DataBind();
      gvItemsNotInGroup.DataBind();
    }

    protected void ddlGroupItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!ddlGroupItems.SelectedValue.Equals(TrackerDb.CONST_INVALIDIDSTR))
      {
        DropDownList _ddlGroupItems = (DropDownList)sender;
        if (_ddlGroupItems != null)
        {
          string _IDSelected = (string)_ddlGroupItems.SelectedValue;
          Session[CONST_SESSION_LASTIDSELECTED] = _IDSelected;
        }
        gvItemsInList.DataBind();
        gvItemsNotInGroup.DataBind();

        // updtPnlItems.Update();
        updtPnlItemsInList.Update();
      }
    }
    protected string GiveInStatus()
    {
      return (ddlGroupItems.SelectedValue == TrackerDb.CONST_INVALIDIDSTR) ? "Please select a group" : "Please add an item to the group";
    }

    protected void gvItemsInList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if(e.CommandName.Equals("MoveDown") || e.CommandName.Equals("MoveUp"))
      {
        // Convert the row index stored in the CommandArgument
        // property to an Integer.
        int _index = Convert.ToInt32(e.CommandArgument);   
        
        // Get the last name of the sele  cted author from the appropriate
        // cell in the GridView control.
        GridViewRow _row = gvItemsInList.Rows[_index];
        DropDownList _ddlItem = (DropDownList)_row.FindControl("ddlItemDesc");
        Label _lblItemSortPos = (Label)_row.FindControl("lblItemSortPos");
        
        TrackerDotNet.control.ItemGroupTbl _ITG = new control.ItemGroupTbl();
        _ITG.GroupItemTypeID = Convert.ToInt32(ddlGroupItems.SelectedValue);
        _ITG.ItemTypeID = Convert.ToInt32(_ddlItem.SelectedValue);
        _ITG.ItemTypeSortPos = Convert.ToInt32(_lblItemSortPos.Text); 
                
        if(e.CommandName.Equals("MoveUp"))
        {
          _ITG.DecItemSortPos(_ITG);
        }
        else
          _ITG.IncItemSortPos(_ITG);

        gvItemsInList.DataBind();
      }
    }

    protected void btnEditGroup_Click(object sender, EventArgs e)
    {
      if (!ddlGroupItems.SelectedValue.Equals(TrackerDb.CONST_INVALIDIDSTR))
      {
        Response.Redirect("GroupItemDetail.aspx?" + GroupItemDetail.CONST_QRYSTR_GROUPITEMID + "=" + ddlGroupItems.SelectedValue);
        ddlGroupItems.DataBind();
      }
    }
  }
}