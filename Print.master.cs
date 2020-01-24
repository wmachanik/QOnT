using System;
// using System.Linq;

public partial class PrintMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
      Response.Write("  ");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}
