using System;

namespace TrackerDotNet.Pages
{
  public partial class ClientListForm : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      TrackerDotNet.classes.TrackerTools tt = new classes.TrackerTools();
      DateTime dt = tt.GetClosestNextRoastDate(DateTime.Now);

      lblRoastDate.Text = "<b>Next Roast date:</b> " + dt.ToShortDateString();

      // gvClients.EnableViewState = false;
    }

    protected void ddlClientsPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
      gvClients.PageSize = Convert.ToInt16(ddlClientsPerPage.SelectedValue);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

    }

    protected void btnSendBulkEmail_Click(object sender, EventArgs e)
    {

    }

    protected void btnSendIt_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
  }
}