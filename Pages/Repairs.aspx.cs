using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;
using System.Drawing;

namespace QOnT.Pages
{
  public partial class Repairs : System.Web.UI.Page
  {
    const string CONST_WHERECLAUSE_SESSIONVAR = "CustomerRepairWhereFilter";
    const int CONST_GVCOL_CONTACTNAME = 4;
    const int CONST_GVCOL_JOBCARD = 5;
    const int CONST_GVCOL_EQUIPMENT = 6;
    const int CONST_GVCOL_MACHINESN = 7;
    const int CONST_GVCOL_FAULT = 8;
    const int CONST_GVCOL_FAULTDESC = 9;
    const int CONST_GVCOL_ROID = 10;

    protected void Page_PreInit(object sender, EventArgs e)
    {
      CheckBrowser _CheckBrowser = new CheckBrowser();
      bool _RunningOnMobile = _CheckBrowser.fBrowserIsMobile();
      Session[CheckBrowser.CONST_SESSION_RUNNINGONMOBILE] = _RunningOnMobile;

      if (_RunningOnMobile)
      {
        this.MasterPageFile = "~/MobileSite.master";
      }
      else
      {
        this.MasterPageFile = "~/Site.master";
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        bool _RunningOnMobile = (bool)Session[CheckBrowser.CONST_SESSION_RUNNINGONMOBILE];
        if (_RunningOnMobile)
        {
          // TextBox _tbxFilterBy = (TextBox)this.Page.FindControl("tbxFilterBy");
          tbxFilterBy.Width = new Unit(8, UnitType.Em);
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
    public string GetMachineDesc(int pEquipID)
    {
      if (pEquipID > 0)
      {
        EquipTypeTbl _EquipType = new EquipTypeTbl();
        return _EquipType.GetEquipName(pEquipID);
      }
      else
        return String.Empty;
    }
    public string GetRepairFaultDesc(int pRepairFaultID)
    {
      if (pRepairFaultID > 0)
      {
        RepairFaultsTbl _RepairFault = new RepairFaultsTbl();
        return _RepairFault.GetRepairFaultDesc(pRepairFaultID);
      }
      else
        return String.Empty;
    }
    public string GetRepairStatusDesc(int pRepairStatusID)
    {
      if (pRepairStatusID > 0)
      {
        RepairStatusesTbl _RepairStatus = new RepairStatusesTbl();
        return _RepairStatus.GetRepairStatusDesc(pRepairStatusID);
      }
      else
        return String.Empty;
    }
    protected void gvRepairs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      bool _RunningOnMobile = (bool)Session[CheckBrowser.CONST_SESSION_RUNNINGONMOBILE];
      if (_RunningOnMobile)
      {
        e.Row.Cells[CONST_GVCOL_CONTACTNAME].Visible = false;
        e.Row.Cells[CONST_GVCOL_EQUIPMENT].Visible = false;
        // e.Row.Cells[CONST_GVCOL_MACHINESN].Visible = false;
        e.Row.Cells[CONST_GVCOL_FAULT].Visible = false;
        e.Row.Cells[CONST_GVCOL_FAULTDESC].Visible = false;
        e.Row.Cells[CONST_GVCOL_ROID].Visible = false;
        //      e.Row.Cells[CONST_GVCOL_JOBCARD].Visible = false;
      }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
      if ((ddlFilterBy.SelectedValue != "0") && (!String.IsNullOrWhiteSpace(tbxFilterBy.Text)))
      {
//        Session[CONST_WHERECLAUSE_SESSIONVAR] = (ddlFilterBy.SelectedValue + " LIKE '%" + tbxFilterBy.Text + "%'");

//      tbxFilterBy.Text = "";
//        odsRepairs.DataBind();
      }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
      Session[CONST_WHERECLAUSE_SESSIONVAR] = "";

      ddlFilterBy.SelectedIndex = 0;
      tbxFilterBy.Text = "";
      odsRepairs.DataBind();
    }

    protected void tbxFilterBy_TextChanged(object sender, EventArgs e)
    {
      if ((!String.IsNullOrWhiteSpace(tbxFilterBy.Text)) && (ddlFilterBy.SelectedIndex == 0))
      {
        ddlFilterBy.SelectedIndex = 1;   // should be company
        upnlSelection.Update();
      }
    }

    protected void ddlRepairStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      odsRepairs.DataBind();
      upnlRepairsSummary.Update();
    }
  }
}