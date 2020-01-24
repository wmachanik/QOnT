using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackerDotNet.classes;

namespace TrackerDotNet.Pages
{
  public partial class ItemGroups : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gvItemGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow _row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
      if (_row != null)
      {
        TextBox _GroupItemTypeIDTextBox = (TextBox)_row.FindControl("GroupItemTypeIDTextBox");
        if ((_GroupItemTypeIDTextBox != null) && (!String.IsNullOrEmpty(_GroupItemTypeIDTextBox.Text)))
        {
          control.ItemGroupTbl _ItemGroup = new control.ItemGroupTbl();
          Literal _ItemGroupIDLiteral = (Literal)_row.FindControl("ItemGroupIDLiteral");
          _ItemGroup.ItemGroupID = (_ItemGroupIDLiteral != null) ? Convert.ToInt32(_ItemGroupIDLiteral.Text) : 0;
          if (e.CommandName.Equals("Delete"))
          {
            _ItemGroup.DeleteItemGroup(_ItemGroup.ItemGroupID);
          }
          else
          {
            TextBox _ItemTypeIDTextBox = (TextBox)_row.FindControl("ItemTypeIDTextBox");
            CheckBox _Enabled = (CheckBox)_row.FindControl("EnabledCheckBox");
            if (e.CommandName.Equals("Add") || e.CommandName.Equals("Insert"))
            {
              _ItemGroup.InsertItemGroup(_ItemGroup);
            }
            else if (e.CommandName.Equals("Update"))
            {
              _ItemGroup.UpdateItemGroup(_ItemGroup, _ItemGroup.ItemGroupID);
            }
          }
        }
        //gvItemGroup.DataBind();
      }
    }

    protected void ddlGroupItems_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {

      string _ScriptToRun;
      _ScriptToRun = "<script>" + "window.open('GroupItemDetail.aspx',null,null);</script>";
      // Page.RegisterStartupScript("JavaScript", _ScriptToRun);
      ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Add Group Item", _ScriptToRun, true);
      ddlGroupItems.DataBind();

    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {

    }
  }
}