using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackerDotNet.DataSets
{
  public class CustomersCls
  {
    private int m_CustomerID;
    private string m_CompanyName;
    private string m_ContactTitle;
    private string m_ContactFirstName;
    private string m_ContactLastName;
    private string m_ContactAltFirstName;
    private string m_ContactAltLastName;
    private string m_Department;
    private string m_BillingAddress;
    private string m_StateOrProvince;
    private string m_PostalCode;
    private string m_PhoneNumber;
    private string m_Extension;
    private string m_FaxNumber;
    private string m_CellNumber;
    private string m_EmailAddress;
    private string m_AltEmailAddress;
    private string m_CustomerType;
    private int m_EquipTypeName;
    private int m_CoffeePreference;
    private int m_City;
    private int m_PriPref;
    private int m_SecPref;
    private double m_PriPrefQty;
    private double m_SecPrefQty;
    private int m_Abreviation;
    private string m_MachineSN;
    private bool m_UsesFilter;
    private bool m_Autofulfill;
    private bool m_Enabled;
    private bool m_PredictionDisabled;
    private bool m_AlwaysSendChkUp;
    private bool m_NormallyResponds;
    private string m_Notes;

    public CustomersCls()
    {
      m_CustomerID = 0;
      m_CompanyName = m_ContactTitle = m_ContactFirstName = m_ContactLastName =
        m_ContactAltFirstName = m_ContactAltLastName = m_Department =
        m_BillingAddress = m_StateOrProvince = m_PostalCode =
        m_PhoneNumber = m_Extension = m_FaxNumber = m_CellNumber =
        m_EmailAddress = m_AltEmailAddress = m_CustomerType = "";
      m_EquipTypeName = m_CoffeePreference = m_City = m_PriPref = m_SecPref = 0;
      m_PriPrefQty = m_SecPrefQty = 0.0;
      m_Abreviation = 0;
      m_MachineSN = "";
      m_Enabled = true;
      m_UsesFilter = m_Autofulfill = m_PredictionDisabled = m_AlwaysSendChkUp = m_NormallyResponds = false;
      m_Notes = "";
    }

    public int CustomerID
    {
      get { return m_CustomerID; }
      set { m_CustomerID = value; }
    }

    public string CompanyName
    {
      get { return m_CompanyName; }
      set { m_CompanyName = value; }
    }

    public string ContactTitle
    {
      get { return m_ContactTitle; }
      set { m_ContactTitle = value; }
    }

    public string ContactFirstName
    {
      get { return m_ContactFirstName; }
      set { m_ContactFirstName = value; }
    }

    public string ContactLastName
    {
      get { return m_ContactLastName; }
      set { m_ContactLastName = value; }
    }
    public string ContactAltFirstName
    {
      get { return m_ContactAltFirstName; }
      set { m_ContactAltFirstName = value; }
    }

    public string ContactAltLastName
    {
      get { return m_ContactAltLastName; }
      set { m_ContactAltLastName = value; }
    }
    public string Department
    {
      get { return m_Department; }
      set { m_Department = value; }
    }

    public string BillingAddress
    {
      get { return m_BillingAddress; }
      set { m_BillingAddress = value; }
    }

    public string StateOrProvince
    {
      get { return m_StateOrProvince; }
      set { m_StateOrProvince = value; }
    }

    public string PostalCode
    {
      get { return m_PostalCode; }
      set { m_PostalCode = value; }
    }

    public string PhoneNumber
    {
      get { return m_PhoneNumber; }
      set { m_PhoneNumber = value; }
    }

    public string Extension
    {
      get { return m_Extension; }
      set { m_Extension = value; }
    }

    public string FaxNumber
    {
      get { return m_FaxNumber; }
      set { m_FaxNumber = value; }
    }

    public string CellNumber
    {
      get { return m_CellNumber; }
      set { m_CellNumber = value; }
    }

    public string EmailAddress
    {
      get { return m_EmailAddress; }
      set { m_EmailAddress = value; }
    }

    public string AltEmailAddress
    {
      get { return m_AltEmailAddress; }
      set { m_AltEmailAddress = value; }
    }

    public string CustomerType
    {
      get { return m_CustomerType; }
      set { m_CustomerType = value; }
    }

    public int EquipTypeName
    {
      get { return m_EquipTypeName; }
      set { m_EquipTypeName = value; }
    }

    public int CoffeePreference
    {
      get { return m_CoffeePreference; }
      set { m_CoffeePreference = value; }
    }

    public int City
    {
      get { return m_City; }
      set { m_City = value; }
    }

    public int PriPref
    {
      get { return m_PriPref; }
      set { m_PriPref = value; }
    }

    public int SecPref
    {
      get { return m_SecPref; }
      set { m_SecPref = value; }
    }

    public double PriPrefQty
    {
      get { return m_PriPrefQty; }
      set { m_PriPrefQty = value; }
    }

    public double SecPrefQty
    {
      get { return m_SecPrefQty; }
      set { m_SecPrefQty = value; }
    }
    public int Abreviation
    {
      get { return m_Abreviation; }
      set { m_Abreviation = value; }
    }
    public string MachineSN
    {
      get { return m_MachineSN; }
      set { m_MachineSN = value; }
    }
    public bool UsesFilter
    {
      get { return m_UsesFilter; }
      set { m_UsesFilter = value; }
    }
    public bool Autofulfill
    {
      get { return m_Autofulfill; }
      set { m_Autofulfill = value; }
    }
    public bool Enabled
    {
      get { return m_Enabled; }
      set { m_Enabled = value; }
    }
    public bool PredictionDisabled
    {
      get { return m_PredictionDisabled; }
      set { m_PredictionDisabled = value; }
    }
    public bool AlwaysSendChkUp
    {
      get { return m_AlwaysSendChkUp; }
      set { m_AlwaysSendChkUp = value; }
    }
    public bool NormallyResponds
    {
      get { return m_NormallyResponds; }
      set { m_NormallyResponds = value; }
    }
    public string Notes
    {
      get { return m_Notes; }
      set { m_Notes = value; }
    }
  }
}