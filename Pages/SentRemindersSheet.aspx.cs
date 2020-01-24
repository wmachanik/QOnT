using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.control;

namespace QOnT.Pages
{
  public partial class SentRemindersSheet : System.Web.UI.Page
  {

    const string CONST_URL_REQUEST_LASTSENTDATE = "LastSentDate";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)  //Complete
    {
      if (!IsPostBack)
      {
        // Look for Date if sent, and set .
        if (Request.QueryString.Count > 0)
        {
          if (Request.QueryString[CONST_URL_REQUEST_LASTSENTDATE] != null)
          {
            string _LastDateString = String.Format("{0:d}", Convert.ToDateTime(Request.QueryString[CONST_URL_REQUEST_LASTSENTDATE]));

            if (ddlFilterByDate.Items.FindByValue(_LastDateString) != null)
            {
              ddlFilterByDate.SelectedValue = _LastDateString;

              gvSentReminders.DataBind();
              upnlSentRemindersList.Update();
            }
          }
        }
      }
    }

    public string GetCompanyName(long pCompanyID)
    {
      if (pCompanyID > 0)
      {
        CompanyNames _Companys = new CompanyNames();
        return _Companys.GetCompanyNameByCompanyID(pCompanyID);
      }
      else
        return String.Empty;
    }

  }
}