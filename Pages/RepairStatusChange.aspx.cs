using System;
using QOnT.classes;
using QOnT.control;

namespace QOnT.Pages
{
  public partial class RepairStatusChange : System.Web.UI.Page
  {
    const string CONST_SESSION_REPAIRDATA = "RepairDataUsed";
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
        if ((Request.QueryString[RepairDetail.CONST_URL_REQUEST_REPAIRID]) != null)
        {
          lblRepairID.Text = Request.QueryString[RepairDetail.CONST_URL_REQUEST_REPAIRID].ToString();
          PutDataFromForm(Convert.ToInt32(lblRepairID.Text));
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
    private void PutDataFromForm(int pRepairID)
    {
      RepairsTbl _Repair = new RepairsTbl();
      _Repair = _Repair.GetRepairById(pRepairID);
      if (_Repair != null)
      {
        lblRepairID.Text = _Repair.RepairID.ToString();
        // make sure the data is bound to the drop down lists.
        ltrlComapny.Text = GetCompanyName(_Repair.CustomerID);
        ltrlMachine.Text = GetMachineDesc(_Repair.MachineTypeID);
        ddlRepairStatuses.DataBind();
        // now assign data
        ltrlMachineSerialNumber.Text = _Repair.MachineSerialNumber;
        ddlRepairStatuses.SelectedValue = _Repair.RepairStatusID.ToString();
        // now the static

        Session[CONST_SESSION_REPAIRDATA] = _Repair;
      }
    }
    void ReturnToPrevPage()
    {
      if (String.IsNullOrWhiteSpace(prevPage))
        Response.Redirect("~/Pages/Repairs.aspx");
      else
        Response.Redirect(prevPage);
    }
    void UpdateRecord()
    {
      RepairsTbl _Repair = (RepairsTbl)Session[CONST_SESSION_REPAIRDATA];

      int _NewStatusId = Convert.ToInt32(ddlRepairStatuses.SelectedValue);

      if (_Repair.RepairStatusID != _NewStatusId)
      {
        // if the status has changed then update and send an email

        //string resultStr = _Repair.UpdateRepairStatus(_NewStatusId, _Repair.RepairID);
        _Repair.RepairStatusID = _NewStatusId; 
        _Repair.HandleAndUpdateRepairStatusChange(_Repair);
      }
    }
    protected void btnUpdateAndReturn_Click(object sender, EventArgs e)
    {
      UpdateRecord();
      ReturnToPrevPage();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      ReturnToPrevPage();
    }
  }
}