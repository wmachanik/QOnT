using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.control;
using QOnT.classes;

namespace QOnT.Pages
{
  public partial class ReoccuringOrderDetails : System.Web.UI.Page
  {
    static string prevPage = String.Empty;

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
        if ((Request.QueryString["ID"]) != null)
        {
          PutDataFromForm(Convert.ToInt64(Request.QueryString["ID"]));
        }
        else
        {
          btnUpdate.Enabled = false;
          btnUpdateAndReturn.Enabled = false;
          btnInsert.Enabled = true;
          btnDelete.Enabled = false;
          EnabledCheckBox.Checked = true;   // if it is an insert enable the ReoccuringOrder
        }

      }
    }
    private void PutDataFromForm(long pReoccuringOrderID)
    {
      // place data on form using ID as a Key
      ReoccuringOrderDAL _DAL = new ReoccuringOrderDAL();
      ReoccuringOrderTbl _ReoccuringOrderData = _DAL.GetByReoccuringOrderByID(pReoccuringOrderID);

      if (_ReoccuringOrderData != null)
      {
        ReoccuringOrderIDLabel.Text = _ReoccuringOrderData.ReoccuringOrderID.ToString();
        // if (ddlCompanyName.Items.FindByValue(_ReoccuringOrderData.CustomerID.ToString()) != null)
          ddlCompanyName.SelectedValue = _ReoccuringOrderData.CustomerID.ToString();

        ValueTextBox.Text = _ReoccuringOrderData.ReoccuranceValue.ToString();
        // if (ddlReoccuranceType.Items.FindByValue(_ReoccuringOrderData.ReoccuranceTypeID.ToString()) != null)
          ddlReoccuranceType.SelectedValue = _ReoccuringOrderData.ReoccuranceTypeID.ToString();
        
        // if (ddlItemType.Items.FindByValue(_ReoccuringOrderData.ItemRequiredID.ToString()) != null)
          ddlItemType.SelectedValue = _ReoccuringOrderData.ItemRequiredID.ToString();

        QuantityTextBox.Text = _ReoccuringOrderData.QtyRequired.ToString();
        UntilDateTextBox.Text = String.Format("{0:d}", _ReoccuringOrderData.RequireUntilDate);
        LastDateTextBox.Text = String.Format("{0:d}", _ReoccuringOrderData.DateLastDone);
        NextDateLabel.Text = String.Format("{0:d}", _ReoccuringOrderData.NextDateRequired);
        ddlPackagingTypes.SelectedValue = _ReoccuringOrderData.PackagingID.ToString(); 
        EnabledCheckBox.Checked = _ReoccuringOrderData.Enabled;
        NotesTextBox.Text = _ReoccuringOrderData.Notes;
      }

    }

    private ReoccuringOrderTbl GetDataFromForm()
    {
      ReoccuringOrderTbl _ReoccuringOrderData = new ReoccuringOrderTbl();
      if (!String.IsNullOrEmpty(ReoccuringOrderIDLabel.Text))
        _ReoccuringOrderData.ReoccuringOrderID = Convert.ToInt64(ReoccuringOrderIDLabel.Text);
      
      _ReoccuringOrderData.CustomerID = Convert.ToInt64(ddlCompanyName.SelectedValue);
      _ReoccuringOrderData.ReoccuranceValue = Convert.ToInt32(ValueTextBox.Text);
      _ReoccuringOrderData.ReoccuranceTypeID = Convert.ToInt32(ddlReoccuranceType.SelectedValue);
      _ReoccuringOrderData.ItemRequiredID = Convert.ToInt32(ddlItemType.SelectedValue);
      _ReoccuringOrderData.QtyRequired = Convert.ToDouble(QuantityTextBox.Text);
      _ReoccuringOrderData.RequireUntilDate = String.IsNullOrWhiteSpace(UntilDateTextBox.Text) ? TrackerTools.STATIC_TrackerMinDate : Convert.ToDateTime(UntilDateTextBox.Text);
      _ReoccuringOrderData.DateLastDone = String.IsNullOrWhiteSpace(LastDateTextBox.Text) ? TrackerTools.STATIC_TrackerMinDate : Convert.ToDateTime(LastDateTextBox.Text);
      _ReoccuringOrderData.NextDateRequired = String.IsNullOrWhiteSpace(NextDateLabel.Text) ? TrackerTools.STATIC_TrackerMinDate : Convert.ToDateTime(NextDateLabel.Text);
      _ReoccuringOrderData.PackagingID = Convert.ToInt32(ddlPackagingTypes.SelectedValue);
      _ReoccuringOrderData.Enabled = EnabledCheckBox.Checked;
      _ReoccuringOrderData.Notes = NotesTextBox.Text;

      return _ReoccuringOrderData;
    }

    void UpdateRecord()
    {
      ReoccuringOrderDAL _DAL = new ReoccuringOrderDAL();
      
      string _resultStr = (_DAL.UpdateReoccuringOrder(GetDataFromForm(), Convert.ToInt64(ReoccuringOrderIDLabel.Text)));

      ltrlStatus.Text = (_resultStr == "") ? "Reoccuring Item Updated" : _resultStr;
      showMessageBox _smb = new showMessageBox(this.Page, "Reoccurring Order Update", ltrlStatus.Text);
    }

    void ReturnToPrevPage() { ReturnToPrevPage(false); }
    void ReturnToPrevPage(bool GoToReoccuringOrders)
    {
      if ((GoToReoccuringOrders) || (String.IsNullOrWhiteSpace(prevPage)))
        Response.Redirect("~/Pages/ReoccuringOrders.aspx");
      else
        Response.Redirect(prevPage);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      UpdateRecord();
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

    protected void btnInsert_Click(object sender, EventArgs e)
    {
      ReoccuringOrderDAL _DAL = new ReoccuringOrderDAL();

      string _resultStr = _DAL.InsertReoccuringOrder(GetDataFromForm());

      ltrlStatus.Text = (_resultStr == "") ? "Reoccuring Item Inserted" : "Error inserting: " + _resultStr;
      showMessageBox _smb = new showMessageBox(this.Page, "Reoccurring Order Insert", ltrlStatus.Text);
      ReturnToPrevPage(true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      ReoccuringOrderDAL _DAL = new ReoccuringOrderDAL();

      string _resultStr = _DAL.DeleteReoccuringOrder(Convert.ToInt64(ReoccuringOrderIDLabel.Text));

      ltrlStatus.Text = (_resultStr == "") ? "Reoccuring Item Deleted" : _resultStr;
      showMessageBox _smb = new showMessageBox(this.Page, "Reoccurring Order Deleted", ltrlStatus.Text);
      ReturnToPrevPage(true);
    }

  }
}