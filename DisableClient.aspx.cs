using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QOnT.classes;
using QOnT.control;
using System.Configuration;

namespace QOnT
{
  public partial class DisableClient : System.Web.UI.Page
  {
    private void DisableCustomerTracking(string pCustID)
    {
      int _CustID;
      if (Int32.TryParse(pCustID, out _CustID ))
      {
        string _CCEmail = (ConfigurationManager.AppSettings[EmailCls.CONST_APPSETTING_FROMEMAILKEY] == null) ? 
          "orders@quaffee.co.za" : ConfigurationManager.AppSettings[EmailCls.CONST_APPSETTING_FROMEMAILKEY];

        TrackerDotNet.control.CustomersTbl _Customers = new control.CustomersTbl();
        _Customers.DisableCustomer(_CustID);

        _Customers = _Customers.GetCustomersByCustomerID(_CustID);

        CompanyNameLabel.Text = _Customers.CompanyName;
        string _Name = String.Empty;
        _Name = _Customers.ContactAltFirstName;
        if (!String.IsNullOrEmpty(_Name))
        {
          if (!String.IsNullOrEmpty(_Customers.ContactAltFirstName))
            _Name += " & " + _Customers.ContactAltFirstName;
        }
        else if (!String.IsNullOrEmpty(_Customers.ContactAltFirstName))
          _Name = _Customers.ContactAltFirstName;
        else
          _Name = "X Coffee lover";
         
        EmailCls _Email = new EmailCls();
        if (!String.IsNullOrEmpty(_Customers.EmailAddress))
          _Email.SetEmailTo(_Customers.EmailAddress);
        if (!String.IsNullOrEmpty(_Customers.AltEmailAddress))
          _Email.SetEmailTo(_Customers.AltEmailAddress);

        _Email.SetEmailCC(_CCEmail);
        _Email.SetEmailSubject(_Customers.CompanyName+ " request to be disabled in Coffee Tracker");
        _Email.AddFormatToBody("Dear {0}, <br /><br />", _Name);
        _Email.AddFormatToBody("As requested we have disabled: {0} in Quaffee's Coffee Tracker.<br /><br />",_Customers.CompanyName);
        _Email.AddFormatToBody("We wish you the best in the future. Should you require anything else from us please email {0}.<br /><br />",_CCEmail);
        _Email.AddStrAndNewLineToBody("The Quaffee Orders Team");
        _Email.AddStrAndNewLineToBody("web: <a href='http://www.quaffee.co.za'>quaffee.co.za</a>");

        _Email.SendEmail();

      }
      else
        CompanyNameLabel.Text = "Company not found";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // hand query string if sent
        // Look for first Customer Name, and then ann stock items sent, also allow or Last Order.
        if (Request.QueryString.Count > 0)
        {
          if (Request.QueryString[TrackerDotNet.Pages.NewOrderDetail.CONST_URL_REQUEST_CUSTOMERID] != null)
            DisableCustomerTracking(Request.QueryString[TrackerDotNet.Pages.NewOrderDetail.CONST_URL_REQUEST_CUSTOMERID]);
        }
      }

    }
  }
}