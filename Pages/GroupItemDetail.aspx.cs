using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;

namespace QOnT.Pages
{
  public partial class GroupItemDetail : System.Web.UI.Page
  {
    public const string CONST_QRYSTR_GROUPITEMID = "ItemTypeID";
    const string CONST_SESSION_RETURNURL = "ReturnItemGroupURL";
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // check if name passed in and populate.
        Session[CONST_SESSION_RETURNURL] = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.OriginalString.ToString();
        if (Request.QueryString[CONST_QRYSTR_GROUPITEMID] != null)
        {
          // this is an edit not a add
          ItemTypeTbl _Item = new ItemTypeTbl();
          _Item = _Item.GetItemTypeFromID(Convert.ToInt32(Request.QueryString[CONST_QRYSTR_GROUPITEMID].ToString()));

          if (!_Item.ItemTypeID.Equals(TrackerDb.CONST_INVALIDID))
          {
            lblGroupItemID.Visible = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            lblGroupItemID.Text = _Item.ItemTypeID.ToString();
            tbxGroupItem.Text = _Item.ItemDesc;
            tbxGroupDesc.Text = _Item.ItemDetail;
            tbxGroupShortName.Text = _Item.ItemShortName;

            // upnlGroupDetail.Update();
          }
        }
      }
    }
    protected void ReturnToPrevPage()
    {
      string returnURL = Session[CONST_SESSION_RETURNURL].ToString();
      if (returnURL.Length > 0)
        Response.Redirect(returnURL);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (tbxGroupItem.Text.Equals(string.Empty))
      {
        showMessageBox _MsgBox = new showMessageBox(this.Page, "Error no item", "Please enter a Group Item Name");
      }
      else
      {
        TrackerDotNet.control.ItemTypeTbl _ITT = new ItemTypeTbl();

        if (_ITT.GroupOfThisNameExists(tbxGroupItem.Text))
        {
          showMessageBox _MsgBox = new showMessageBox(this.Page, "name exists", "Group Name: "+tbxGroupItem.Text+" Exists. Please enter a different Group Item Name");
        }
        else
        {
          SysDataTbl _SysData = new SysDataTbl();

          _ITT.ItemDesc = tbxGroupItem.Text;
          _ITT.ItemDetail = tbxGroupDesc.Text;
          _ITT.ItemEnabled = true;
          _ITT.ItemsCharacteritics = "Group Item";
          _ITT.ItemShortName = tbxGroupShortName.Text;
          _ITT.ServiceTypeID = _SysData.GetGroupItemTypeID();
          _ITT.SortOrder = ItemTypeTbl.CONST_NEEDDESCRIPTION_SORT_ORDER + 5;

          bool _success = _ITT.InsertItem(_ITT);
          showMessageBox _MsgBox = new showMessageBox(this.Page, "Status", _success ? "Group item added" : "Error adding group item");
          ReturnToPrevPage();
        }
      }
    }
// - code for update
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      if (tbxGroupItem.Text.Equals(string.Empty))
      {
        showMessageBox _MsgBox = new showMessageBox(this.Page, "Error no item", "Please enter a Group Item Name");
      }
      else
      {
        TrackerDotNet.control.ItemTypeTbl _ITT = new ItemTypeTbl();

          SysDataTbl _SysData = new SysDataTbl();

          _ITT.ItemDesc = tbxGroupItem.Text;
          _ITT.ItemDetail = tbxGroupDesc.Text;
          _ITT.ItemEnabled = true;
          _ITT.ItemsCharacteritics = "Group Item-update";
          _ITT.ItemShortName = tbxGroupShortName.Text;
          _ITT.ServiceTypeID = _SysData.GetGroupItemTypeID();
          _ITT.SortOrder = ItemTypeTbl.CONST_NEEDDESCRIPTION_SORT_ORDER + 5;
          _ITT.ItemTypeID = Convert.ToInt32(lblGroupItemID.Text);
          bool _success = _ITT.UpdateItem(_ITT);
          showMessageBox _MsgBox = new showMessageBox(this.Page, "Status", _success ? "Group item update" : "Error updating group item");
          ReturnToPrevPage();  
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      ReturnToPrevPage();
    }
  }
}