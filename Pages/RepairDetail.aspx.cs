using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;
using System.Web.Security;

namespace QOnT.Pages
{
  public partial class RepairDetail : System.Web.UI.Page
  {
    public const string CONST_URL_REQUEST_REPAIRID = "RepairID";
    const string CONST_SESSION_REPAIRSTATUSID = "RepairStatusID";

    static string prevPage = String.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
      CheckBrowser _CheckBrowser = new CheckBrowser();

      if (_CheckBrowser.fBrowserIsMobile())
      {
        this.MasterPageFile = "~/MobileSite.master";
      }
      else
        this.MasterPageFile = "~/Site.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // get referring page
        if ((Request.UrlReferrer == null))
          prevPage = String.Empty;
        else
          prevPage = Request.UrlReferrer.ToString();
        // if the id is past and is not null then set it
        if ((Request.QueryString[CONST_URL_REQUEST_REPAIRID]) != null)
        {
          pnlNewRepair.Visible = false;
          pnlRepairDetail.Visible = true;
          lblRepairID.Text = Request.QueryString[CONST_URL_REQUEST_REPAIRID].ToString();
          PutDataFromForm(Convert.ToInt32(lblRepairID.Text));
          upnlRepairDetail.Update();

          MembershipUser _currMember = Membership.GetUser();

          btnDelete.Enabled = (_currMember.UserName.ToLower() == "warren");

        }
        else
        {
          pnlNewRepair.Visible = true;
          pnlRepairDetail.Visible = false;
          upnlRepairDetail.Update();
        }
      }
    }

    private void PutDataFromForm(int pRepairID)
    {
      RepairsTbl _Repair = new RepairsTbl();
      _Repair = _Repair.GetRepairById(pRepairID);
      if (_Repair != null)
      {
        lblRepairID.Text = _Repair.RepairID.ToString();
        // make sure the data is bound to the drop down lists.
        ddlCompany.DataBind();
        ddlEquipTypes.DataBind();
        ddlMachineCondtion.DataBind();
        ddlRepairFault.DataBind();
        ddlRepairStatuses.DataBind();
        ddlSwopOutMachine.DataBind();

        // now assign data
        ddlCompany.SelectedValue = _Repair.CustomerID.ToString();
        tbxContactName.Text = _Repair.ContactName;
        tbxContactEmail.Text = _Repair.ContactEmail;
        tbxJobCardNumber.Text = _Repair.JobCardNumber;
        ddlEquipTypes.SelectedValue = _Repair.MachineTypeID.ToString();
        tbxMachineSerialNumber.Text = _Repair.MachineSerialNumber;
        ddlSwopOutMachine.SelectedValue = _Repair.SwopOutMachineID.ToString();
        ddlMachineCondtion.SelectedValue = _Repair.MachineConditionID.ToString();
        cbxTakenFrother.Checked = _Repair.TakenFrother;
        cbxTakenBeanLid.Checked = _Repair.TakenBeanLid;
        cbxTakenWaterLid.Checked = _Repair.TakenWaterLid;
        cbxBrokenFrother.Checked = _Repair.BrokenFrother;
        cbxBrokenBeanLid.Checked = _Repair.BrokenBeanLid;
        cbxBrokenWaterLid.Checked = _Repair.BrokenWaterLid;
        ddlRepairFault.SelectedValue = _Repair.RepairFaultID.ToString();
        tbxRepairFaultDesc.Text = _Repair.RepairFaultDesc;
        ddlRepairStatuses.SelectedValue = _Repair.RepairStatusID.ToString();
        tbxNotes.Text = _Repair.Notes;
        // now the static
        lblDateLogged.Text = String.Format("{0:d}", _Repair.DateLogged);
        lblLastChanged.Text = String.Format("{0:d}", _Repair.LastStatusChange);
        lblRelatedOrderID.Text = _Repair.RelatedOrderID.ToString();
        Session[CONST_SESSION_REPAIRSTATUSID] = _Repair.RepairStatusID;

      }
    }
    private RepairsTbl GetDataFromForm()
    {
      RepairsTbl _Repair = new RepairsTbl();
      
      _Repair.RepairID = Convert.ToInt32(lblRepairID.Text);
      _Repair.CustomerID = Convert.ToInt64(ddlCompany.SelectedValue);
      _Repair.ContactName = tbxContactName.Text;
      _Repair.ContactEmail = tbxContactEmail.Text;
      _Repair.JobCardNumber = tbxJobCardNumber.Text;
      _Repair.MachineTypeID = Convert.ToInt32(ddlEquipTypes.SelectedValue);
      _Repair.MachineSerialNumber = tbxMachineSerialNumber.Text;
      _Repair.SwopOutMachineID = Convert.ToInt32(ddlSwopOutMachine.SelectedValue);
      _Repair.MachineConditionID = Convert.ToInt32(ddlMachineCondtion.SelectedValue);
      _Repair.TakenFrother = cbxTakenFrother.Checked;
      _Repair.TakenBeanLid = cbxTakenBeanLid.Checked;
      _Repair.TakenWaterLid = cbxTakenWaterLid.Checked;
      _Repair.BrokenFrother = cbxBrokenFrother.Checked;
      _Repair.BrokenBeanLid = cbxBrokenBeanLid.Checked;
      _Repair.BrokenWaterLid = cbxBrokenWaterLid.Checked;
      _Repair.RepairFaultID = Convert.ToInt32(ddlRepairFault.SelectedValue);
      _Repair.RepairFaultDesc = tbxRepairFaultDesc.Text;
      _Repair.RepairStatusID = Convert.ToInt32(ddlRepairStatuses.SelectedValue);
      _Repair.Notes = tbxNotes.Text;
      // now the static
      _Repair.DateLogged = Convert.ToDateTime(lblDateLogged.Text).Date;
      _Repair.LastStatusChange = Convert.ToDateTime(lblLastChanged.Text).Date;
      _Repair.RelatedOrderID = Convert.ToInt32(lblRelatedOrderID.Text);

      return _Repair;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
      // isnert a record and redirect to the same pacge, since this is only called if no data.
      RepairsTbl _Repair = new control.RepairsTbl();

      if (ddlNewCompany.SelectedIndex > 0)
      {
        _Repair.CustomerID = Convert.ToInt64(ddlNewCompany.SelectedValue);
        _Repair.DateLogged = DateTime.Now.Date;

        // get contact, machine and serial number defaults
        CustomersTbl _CustomerData = new CustomersTbl();
        _CustomerData = _CustomerData.GetCustomersByCustomerID(_Repair.CustomerID);

        _Repair.ContactName = _CustomerData.ContactFirstName;
        _Repair.ContactEmail = _CustomerData.EmailAddress;
        _Repair.MachineTypeID = _CustomerData.EquipType;
        _Repair.MachineSerialNumber = _CustomerData.MachineSN;
        _Repair.DateLogged = DateTime.Now.Date;

        _Repair.InsertRepair(_Repair);

        pnlNewRepair.Visible = false;
        pnlRepairDetail.Visible = true;
        _Repair.RepairID = _Repair.GetLastIDInserted(_Repair.CustomerID);
        PutDataFromForm(_Repair.RepairID);
        upnlRepairDetail.Update();
      }
    }

    void UpdateRecord()
    {
      RepairsTbl _Repair = GetDataFromForm();
      string resultStr = _Repair.UpdateRepair(_Repair, _Repair.RepairID);

      int _OldStatusId = (Session[CONST_SESSION_REPAIRSTATUSID] != null) ? (int)Session[CONST_SESSION_REPAIRSTATUSID] : 0;

      if (String.IsNullOrWhiteSpace(resultStr))
      {
        ltrlStatus.Text = "Record Updated";
        // if the status has changed then send an email
        if (_Repair.RepairStatusID != _OldStatusId)
        {
          _Repair.HandleAndUpdateRepairStatusChange(_Repair);
          Session[CONST_SESSION_REPAIRSTATUSID] = _OldStatusId;
        }
      }
      else
      {
        ltrlStatus.Text = resultStr;
        upnlRepairDetail.Update();
      }
    }
    void ReturnToPrevPage() { ReturnToPrevPage(false); }

    void ReturnToPrevPage(bool pGoToRepairs)
    {
      if ((pGoToRepairs) || (String.IsNullOrWhiteSpace(prevPage)))
        Response.Redirect("~/Pages/Repairs.aspx");
      else
        Response.Redirect(prevPage);
    }
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //  UpdateRecord();
    //}
    protected void btnUpdateAndReturn_Click(object sender, EventArgs e)
    {
      UpdateRecord();
      ReturnToPrevPage();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      ReturnToPrevPage();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
      RepairsTbl _Repair = new RepairsTbl();

      _Repair.DeleteRepair(Convert.ToInt32(lblRepairID.Text));
      ReturnToPrevPage();
    }

    public void RepairUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {
      //RepairsTbl _Repair = new RepairsTbl();

      //_Repair.RepairID = Convert.ToInt32(lblThusRepairID.Text);
      //e.InputParameters["orig_RepairID"] = _Repair.RepairID;
      //_Repair.LastStatusChange = DateTime.Now.Date;

      //DropDownList _ddlCompany = (DropDownList)dvRepairDetail.FindControl("ddlCompany");
      //_Repair.CustomerID = Convert.ToInt64(_ddlCompany.SelectedValue);
      //TextBox _tbxContactName = (TextBox)dvRepairDetail.FindControl("tbxContactName");
      //_Repair.ContactName = _tbxContactName.Text;
      //TextBox _tbxContactEmail = (TextBox)dvRepairDetail.FindControl("tbxContactEmail");
      //_Repair.ContactEmail = _tbxContactEmail.Text;
      //TextBox _tbxJobCardNumber = (TextBox)dvRepairDetail.FindControl("tbxJobCardNumber");
      //_Repair.JobCardNumber = _tbxJobCardNumber.Text;
      //e.InputParameters["RepairItem"] = _Repair;
      //bool _syncd = e.InputParameters.IsSynchronized;

      //if (_syncd)
      //  e.InputParameters.AsParallel();
      ///// --- add other items once working

      /// this code below was just added to see if the data routing works:
      // if I call this the data updates, but the mode does not change, so I use a redirect to check and that works fine
      // _Repair.UpdateRepair(_Repair.RepairID, _Repair);
      //      Response.Redirect(String.Format("~/Pages/RepairDetail.aspx?{0}={1}",CONST_URL_REQUEST_REPAIRID,_Repair.RepairID));

    }

    public void RowUpdated(object source, ObjectDataSourceStatusEventArgs e)
    {
      if (e.AffectedRows == 0)
      {
        classes.showMessageBox _smb = new classes.showMessageBox(this.Page, "nothing updated", "no records updated");
      }
    }

  }
}