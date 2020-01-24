using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace QOnT.Administration
{
  public partial class ManageRoles : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        BindRoles();
      }
    }

    private void BindRoles()
    {
      // Create a DataTable and define its columns
      DataTable _RoleList = new DataTable();
      _RoleList.Columns.Add("Role Name");
      _RoleList.Columns.Add("User Count");

      // Get the list of roles in the system and how many users belong to each role
      string[] _allRoles = Roles.GetAllRoles();

      foreach (string _roleName in _allRoles)
      {
        int _numberOfUsersInRole = Roles.GetUsersInRole(_roleName).Length;
        string[] roleRow = { _roleName, _numberOfUsersInRole.ToString() };
        _RoleList.Rows.Add(roleRow);
      }
      // Bind the DataTable to the GridView
      gvRolesManagement.DataSource = _RoleList;
      gvRolesManagement.DataBind();
    }

    public void CreateRole_OnClick(object sender, EventArgs args)
    {
      string createRole = RoleTextBox.Text;

      try
      {
        if (Roles.RoleExists(createRole))
        {
          MsgLabel.Text = "Role '" + Server.HtmlEncode(createRole) + "' already exists. Please specify a different role name.";
          return;
        }

        Roles.CreateRole(createRole);

        MsgLabel.Text = "Role '" + Server.HtmlEncode(createRole) + "' created.";

        // Re-bind roles to GridView.
        BindRoles();
      }
      catch (Exception e)
      {
        MsgLabel.Text = "Role '" + Server.HtmlEncode(createRole) + "' <u>not</u> created.";
        Response.Write(e.ToString());
      }

    }
    public void RenameRoleAndUsers(string OldRoleName, string NewRoleName)
    {
      string[] users = Roles.GetUsersInRole(OldRoleName);
      Roles.CreateRole(NewRoleName);
      Roles.AddUsersToRole(users, NewRoleName);
      Roles.RemoveUsersFromRole(users, OldRoleName);
      Roles.DeleteRole(OldRoleName);
    }
    public void UpdateRole_OnClick(object sender, EventArgs args)
    {
      // RenameRoleAndUsers(_roles[gvRolesManagement.EditIndex], _UpdateRole);

      BindRoles();
    }
    protected virtual void gvRolesManagement_OnSelectedIndexChanged(object sender, EventArgs e)
    {
    }
  }
}