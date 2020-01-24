using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace QOnT.Administration
{
  public partial class ManageUsers : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        BindUserAccounts();
      }
    }

    private void BindUserAccounts()
    {
      gvUserAccounts.DataSource = Membership.GetAllUsers();
      gvUserAccounts.DataBind();
    }

  }
}