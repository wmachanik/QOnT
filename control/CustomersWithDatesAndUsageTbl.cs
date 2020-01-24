using System;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class CustomersWithDatesAndUsageTbl
  {
    private CustomersTbl _Customer;
    private NextRoastDateByCityTbl _NextRoastDateByCity;
    private ClientUsageTbl _ClientUsage;

    public CustomersWithDatesAndUsageTbl()
    {
      _Customer = new CustomersTbl();
      _NextRoastDateByCity = new NextRoastDateByCityTbl();
      _ClientUsage = new ClientUsageTbl();
    }

    public CustomersTbl Customer { get { return _Customer; } set { _Customer = value; } }
    public NextRoastDateByCityTbl NextRoastDateByCity { get { return _NextRoastDateByCity; } set { _NextRoastDateByCity = value; } }
    public ClientUsageTbl ClientUsage { get { return _ClientUsage; } set { _ClientUsage = value; } }

    // Const declarations now
    const string CONST_SQL_CUSTOMERSUSAGE_SELECT = "SELECT CustomersTbl.CustomerID, CustomersTbl.CompanyName, CustomersTbl.ContactTitle, CustomersTbl.ContactFirstName, CustomersTbl.ContactLastName, " +
                        " CustomersTbl.ContactAltFirstName, CustomersTbl.ContactAltLastName, CustomersTbl.Department, CustomersTbl.BillingAddress, CustomersTbl.City, " +
                        " CustomersTbl.StateOrProvince AS Province, CustomersTbl.PostalCode, CustomersTbl.[Country/Region] AS Region, CustomersTbl.PhoneNumber, " +
                        " CustomersTbl.Extension, CustomersTbl.FaxNumber, CustomersTbl.CellNumber, CustomersTbl.EmailAddress, CustomersTbl.AltEmailAddress, " +
                        " CustomersTbl.ContractNo, CustomersTbl.CustomerTypeID, CustomersTbl.EquipType, CustomersTbl.CoffeePreference, CustomersTbl.PriPrefQty," +
                        " CustomersTbl.PrefPrepTypeID, CustomersTbl.PrefPackagingID, CustomersTbl.SecondaryPreference, CustomersTbl.SecPrefQty, CustomersTbl.TypicallySecToo, " +
                        " CustomersTbl.PreferedAgent, CustomersTbl.SalesAgentID, CustomersTbl.MachineSN, CustomersTbl.UsesFilter, CustomersTbl.autofulfill, CustomersTbl.enabled," +
                        " CustomersTbl.PredictionDisabled, CustomersTbl.AlwaysSendChkUp, CustomersTbl.NormallyResponds, CustomersTbl.ReminderCount, " +
                        " CustomersTbl.LastDateSentReminder, CustomersTbl.Notes, NextRoastDateByCityTbl.CityID, NextRoastDateByCityTbl.DeliveryDate, " +
                        " NextRoastDateByCityTbl.PreperationDate, NextRoastDateByCityTbl.DeliveryOrder," +
                        " LastCupCount, NextCoffeeBy, NextCleanOn, NextFilterEst, NextDescaleEst, NextServiceEst, DailyConsumption, FilterAveCount, DescaleAveCount, ServiceAveCount, CleanAveCount " +
                     " FROM  (NextRoastDateByCityTbl RIGHT OUTER JOIN CustomersTbl ON NextRoastDateByCityTbl.CityID = CustomersTbl.City), ClientUsageTbl.CustomerID = CustomersTbl.CustomerID " +
                     " WHERE (CustomersTbl.CustomerID = ?)";

    public CustomersWithDatesAndUsageTbl GetCustomerWithDatesAndUsage(long pCustomerID)
    {
      CustomersWithDatesAndUsageTbl _DataItem = null;

      TrackerDb _TDB = new classes.TrackerDb();
      _TDB.AddWhereParams(pCustomerID, DbType.Int64);

//      OleDbDataReader _DataReader = _TrackerDB.ReturnDataReader(CONST_SQL_CUSTOMERSUSAGE_SELECT, _WhereParameters);
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_CUSTOMERSUSAGE_SELECT);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _DataItem = new CustomersWithDatesAndUsageTbl();

          _DataItem.Customer.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.Customer.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? String.Empty : _DataReader["CompanyName"].ToString();
          _DataItem.Customer.ContactTitle = (_DataReader["ContactTitle"] == DBNull.Value) ? String.Empty : _DataReader["ContactTitle"].ToString();
          _DataItem.Customer.ContactFirstName = (_DataReader["ContactFirstName"] == DBNull.Value) ? String.Empty : _DataReader["ContactFirstName"].ToString();
          _DataItem.Customer.ContactLastName = (_DataReader["ContactLastName"] == DBNull.Value) ? String.Empty : _DataReader["ContactLastName"].ToString();
          _DataItem.Customer.ContactAltFirstName = (_DataReader["ContactAltFirstName"] == DBNull.Value) ? String.Empty : _DataReader["ContactAltFirstName"].ToString();
          _DataItem.Customer.ContactAltLastName = (_DataReader["ContactAltLastName"] == DBNull.Value) ? String.Empty : _DataReader["ContactAltLastName"].ToString();
          _DataItem.Customer.Department = (_DataReader["Department"] == DBNull.Value) ? String.Empty : _DataReader["Department"].ToString();
          _DataItem.Customer.BillingAddress = (_DataReader["BillingAddress"] == DBNull.Value) ? String.Empty : _DataReader["BillingAddress"].ToString();
          _DataItem.Customer.City = (_DataReader["City"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["City"]);
          _DataItem.Customer.Province = (_DataReader["Province"] == DBNull.Value) ? String.Empty : _DataReader["Province"].ToString();
          _DataItem.Customer.PostalCode = (_DataReader["PostalCode"] == DBNull.Value) ? String.Empty : _DataReader["PostalCode"].ToString();
          _DataItem.Customer.Region = (_DataReader["Region"] == DBNull.Value) ? String.Empty : _DataReader["Region"].ToString();
          _DataItem.Customer.PhoneNumber = (_DataReader["PhoneNumber"] == DBNull.Value) ? String.Empty : _DataReader["PhoneNumber"].ToString();
          _DataItem.Customer.Extension = (_DataReader["Extension"] == DBNull.Value) ? String.Empty : _DataReader["Extension"].ToString();
          _DataItem.Customer.FaxNumber = (_DataReader["FaxNumber"] == DBNull.Value) ? String.Empty : _DataReader["FaxNumber"].ToString();
          _DataItem.Customer.CellNumber = (_DataReader["CellNumber"] == DBNull.Value) ? String.Empty : _DataReader["CellNumber"].ToString();
          _DataItem.Customer.EmailAddress = (_DataReader["EmailAddress"] == DBNull.Value) ? String.Empty : _DataReader["EmailAddress"].ToString();
          _DataItem.Customer.AltEmailAddress = (_DataReader["AltEmailAddress"] == DBNull.Value) ? String.Empty : _DataReader["AltEmailAddress"].ToString();
          _DataItem.Customer.ContractNo = (_DataReader["ContractNo"] == DBNull.Value) ? String.Empty : _DataReader["ContractNo"].ToString();
          _DataItem.Customer.CustomerTypeID = (_DataReader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTypeID"]);
          _DataItem.Customer.EquipType = (_DataReader["EquipType"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["EquipType"]);
          _DataItem.Customer.CoffeePreference = (_DataReader["CoffeePreference"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CoffeePreference"]);
          _DataItem.Customer.PriPrefQty = (_DataReader["PriPrefQty"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["PriPrefQty"]);
          _DataItem.Customer.PrefPrepTypeID = (_DataReader["PrefPrepTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PrefPrepTypeID"]);
          _DataItem.Customer.PrefPackagingID = (_DataReader["PrefPackagingID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PrefPackagingID"]);
          _DataItem.Customer.SecondaryPreference = (_DataReader["SecondaryPreference"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SecondaryPreference"]);
          _DataItem.Customer.SecPrefQty = (_DataReader["SecPrefQty"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["SecPrefQty"]);
          _DataItem.Customer.TypicallySecToo = (_DataReader["TypicallySecToo"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TypicallySecToo"]);
          _DataItem.Customer.PreferedAgent = (_DataReader["PreferedAgent"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PreferedAgent"]);
          _DataItem.Customer.SalesAgentID = (_DataReader["SalesAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SalesAgentID"]);
          _DataItem.Customer.MachineSN = (_DataReader["MachineSN"] == DBNull.Value) ? String.Empty : _DataReader["MachineSN"].ToString();
          _DataItem.Customer.UsesFilter = (_DataReader["UsesFilter"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UsesFilter"]);
          _DataItem.Customer.autofulfill = (_DataReader["autofulfill"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["autofulfill"]);
          _DataItem.Customer.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);
          _DataItem.Customer.AlwaysSendChkUp = (_DataReader["AlwaysSendChkUp"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AlwaysSendChkUp"]);
          _DataItem.Customer.NormallyResponds = (_DataReader["NormallyResponds"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["NormallyResponds"]);
          _DataItem.Customer.ReminderCount = (_DataReader["xxx"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["xxx"]);
          _DataItem.Customer.LastDateSentReminder = (_DataReader["LastDateSentReminder"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["LastDateSentReminder"]);
          _DataItem.Customer.Notes = (_DataReader["Notes"] == DBNull.Value) ? String.Empty : _DataReader["Notes"].ToString();
          _DataItem.NextRoastDateByCity.CityID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.NextRoastDateByCity.DeliveryDate = (_DataReader["DeliveryDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["DeliveryDate"]).Date;
          _DataItem.NextRoastDateByCity.PrepDate = (_DataReader["PreperationDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["PreperationDate"]).Date;
          _DataItem.NextRoastDateByCity.DeliveryOrder = (_DataReader["DeliveryOrder"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["DeliveryOrder"]);
          _DataItem.ClientUsage.LastCupCount = (_DataReader["LastCupCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastCupCount"]);
          _DataItem.ClientUsage.NextCoffeeBy = (_DataReader["NextCoffeeBy"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextCoffeeBy"]).Date;
          _DataItem.ClientUsage.NextCleanOn = (_DataReader["NextCleanOn"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextCleanOn"]).Date;
          _DataItem.ClientUsage.NextFilterEst = (_DataReader["NextFilterEst "] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextFilterEst"]).Date;
          _DataItem.ClientUsage.NextDescaleEst = (_DataReader["NextDescaleEst"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextDescaleEst"]).Date;
          _DataItem.ClientUsage.NextServiceEst = (_DataReader["NextServiceEst"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextServiceEst"]).Date;
          _DataItem.ClientUsage.DailyConsumption = (_DataReader["DailyConsumption"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["DailyConsumption"]);
          _DataItem.ClientUsage.FilterAveCount = (_DataReader["FilterAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["FilterAveCount"]);
          _DataItem.ClientUsage.DescaleAveCount = (_DataReader["DescaleAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["DescaleAveCount"]);
          _DataItem.ClientUsage.ServiceAveCount = (_DataReader["ServiceAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["ServiceAveCount"]);
          _DataItem.ClientUsage.CleanAveCount = (_DataReader["CleanAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["CleanAveCount"]);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      
      return _DataItem;
    }
  }
}