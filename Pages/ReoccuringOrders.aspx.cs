using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;

namespace QOnT.Pages
{
  public partial class ReoccuringOrders : System.Web.UI.Page
  {

    const string CONST_WHERECLAUSE_SESSIONVAR = "ReoccuringOrderSummaryWhereFilter";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if ((Request.QueryString["CompanyName"]) != null)
        {
          ddlFilterBy.SelectedValue = "CompanyName";

          Session[CONST_WHERECLAUSE_SESSIONVAR] = "CompanyName LIKE '%" + Request.QueryString["CompanyName"].ToString() + "%'"; 
        }
        if (Session[CONST_WHERECLAUSE_SESSIONVAR] != null)
        {
          string _FilterBy = (string)Session[CONST_WHERECLAUSE_SESSIONVAR];
          _FilterBy = _FilterBy.Remove(0,_FilterBy.IndexOf("%")+1);
          _FilterBy =   _FilterBy.Remove(_FilterBy.IndexOf("%"));
          tbxFilterBy.Text = _FilterBy;
        }
/*
else
          Session[CONST_WHERECLAUSE_SESSIONVAR] = "";
*/
        gvReoccuringOrders.Sort("CompanyName", SortDirection.Ascending);
      }
      else
        if (Session[CONST_WHERECLAUSE_SESSIONVAR]!=null)
          lblFilter.Text = Session[CONST_WHERECLAUSE_SESSIONVAR].ToString();

      GridViewHelper _gvHelper = new GridViewHelper(this.gvReoccuringOrders);
      _gvHelper.RegisterGroup("CompanyName", true, false);
      _gvHelper.ApplyGroupSort();
    }
    protected void btnGon_Click(object sender, EventArgs e)
    {
      if ((ddlFilterBy.SelectedValue != "0") && (!String.IsNullOrWhiteSpace (tbxFilterBy.Text)))
      {
        Session[CONST_WHERECLAUSE_SESSIONVAR] = (ddlFilterBy.SelectedValue + " LIKE '%" + tbxFilterBy.Text + "%'"); 

        odsReoccuringOrderSummarys.DataBind();
      }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
      Session[CONST_WHERECLAUSE_SESSIONVAR] = "";
      
      ddlFilterBy.SelectedIndex = 0;
      tbxFilterBy.Text = "";
      Session[CONST_WHERECLAUSE_SESSIONVAR] = null;
      odsReoccuringOrderSummarys.DataBind();
    }

    protected void tbxFilterBy_TextChanged(object sender, EventArgs e)
    {
      if ((!String.IsNullOrWhiteSpace(tbxFilterBy.Text)) && (ddlFilterBy.SelectedIndex == 0))
      {
        ddlFilterBy.SelectedIndex = 1;   // should be company
        upnlSelection.Update();
      }
    }

  }
}