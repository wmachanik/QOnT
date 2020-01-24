using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace QOnT.Administration
{
  public partial class UserInformation : System.Web.UI.Page
  {
    private void Page_PreRender()
    {
      // Load the User Roles into checkboxes.
      UserRolesCheckBoxList.DataSource = Roles.GetAllRoles();
      UserRolesCheckBoxList.DataBind();

      //// Disable checkboxes if appropriate:
      //if (UserInfo.CurrentMode != DetailsViewMode.Edit)
      //{
      //  foreach (ListItem checkbox in UserRoles.Items)
      //  {
      //    checkbox.Enabled = false;
      //  }
      //}

      // Bind these checkboxes to the User's own set of roles.
      string _userName = Request.QueryString["user"];
      string[] _userRoles = Roles.GetRolesForUser(_userName);
      foreach (string role in _userRoles)
      {
        ListItem checkbox = UserRolesCheckBoxList.Items.FindByValue(role);
        checkbox.Selected = true;
      }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        // If querystring value is missing, send the user to ManageUsers.aspx
        string userName = Request.QueryString["user"];
        if (string.IsNullOrEmpty(userName))
          Response.Redirect("ManageUsers.aspx");

        // Get information about this user
        MembershipUser _usr = Membership.GetUser(userName);
        if (_usr == null)
          Response.Redirect("ManageUsers.aspx");

        lblUserName.Text = _usr.UserName;
        cbxUserIsApproved.Checked = _usr.IsApproved;
        if (_usr.LastLockoutDate.Year < 2000)
          lblUserLockedOut.Text = "no"; // string.Empty;
        else
          lblUserLockedOut.Text = _usr.LastLockoutDate.ToShortDateString();

        btnUnlockUser.Visible = _usr.IsLockedOut;

        OnlineLabel.Text = (_usr.IsOnline) ? "online" : "offline";
        LastLoginDateLabel.Text = string.Format("{0:d}", _usr.LastLoginDate);

        EmailLabel.Text = _usr.Email;
      }
    }

    protected void cbxUserIsApproved_CheckedChanged(object sender, EventArgs e)
    {
      // Toggle the user's approved status
      string userName = Request.QueryString["user"];
      MembershipUser _usr = Membership.GetUser(userName);
      _usr.IsApproved = cbxUserIsApproved.Checked;
      Membership.UpdateUser(_usr);
      lblStatusMessage.Text = "The user's approved status has been updated.";
    }

    protected void btnUnlockUser_Click(object sender, EventArgs e)
    {
      // Unlock the user account
      string userName = Request.QueryString["user"];
      MembershipUser _usr = Membership.GetUser(userName);

      _usr.UnlockUser();
      Membership.UpdateUser(_usr);
      btnUnlockUser.Enabled = false;
      lblStatusMessage.Text = "The user account has been unlocked.";
    }
    private void UpdateUserRoles()
    {
      string _userName = lblUserName.Text;
      foreach (ListItem rolebox in UserRolesCheckBoxList.Items)
      {
        if (rolebox.Selected)
        {
          if (!Roles.IsUserInRole(_userName, rolebox.Text))
          {
            Roles.AddUserToRole(_userName, rolebox.Text);
          }
        }
        else
        {
          if (Roles.IsUserInRole(_userName, rolebox.Text))
          {
            Roles.RemoveUserFromRole(_userName, rolebox.Text);
          }
        }
      }
    }
    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {
      UpdateUserRoles();
      Response.Redirect("~/Administration/ManageUsers.aspx");
    }
    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
      // Unlock the user account
      string userName = Request.QueryString["user"];

      Membership.DeleteUser(userName, true);
      lblStatusMessage.Text = "The user account deleted.";

    }
  }
}