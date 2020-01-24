using System;
using System.Collections.Generic;      // for data stuff
using System.Data;
using QOnT.classes;

namespace QOnT.control
{

  public class ContactsThayMayNeedData
  {
    private CustomersTbl _CustomerData;
    private bool _RequiresPurchOrder;
    private ClientUsageTbl _ClientUsageData;
    private NextRoastDateByCityTbl _NextRoastDateByCityData;

    public ContactsThayMayNeedData()
    {
      _CustomerData = new CustomersTbl();
      _RequiresPurchOrder = false;
      _ClientUsageData = new ClientUsageTbl();
      _NextRoastDateByCityData = new NextRoastDateByCityTbl();
    }

    public CustomersTbl CustomerData {  get { return _CustomerData; } set { _CustomerData = value; } }
    public bool RequiresPurchOrder { get { return _RequiresPurchOrder; } set { _RequiresPurchOrder = value; } }
    public ClientUsageTbl ClientUsageData {  get { return _ClientUsageData; } set { _ClientUsageData = value; } }
    public NextRoastDateByCityTbl NextRoastDateByCityData { get { return _NextRoastDateByCityData; } set { _NextRoastDateByCityData = value; } }

  }
  public class ContactsThatMayNeedNextWeek
  {
    #region Constants
    const string CONST_SELECT_CONTACTSTHATMAYNEEDNEXTWEEK =
      "SELECT CustomersTbl.CustomerID AS ContactID, CustomersTbl.CompanyName, CustomersTbl.ContactTitle, CustomersTbl.ContactFirstName, " +
              " CustomersTbl.ContactLastName, CustomersTbl.ContactAltFirstName, CustomersTbl.ContactAltLastName, CustomersTbl.Department," +
              " CustomersTbl.BillingAddress, CustomersTbl.City, CustomersTbl.PostalCode, CustomersTbl.PreferedAgent, CustomersTbl.SalesAgentID," +
              " CustomersTbl.PhoneNumber, CustomersTbl.Extension, CustomersTbl.FaxNumber, CustomersTbl.CellNumber, CustomersTbl.EmailAddress, " +
              " CustomersTbl.AltEmailAddress, CustomersTbl.UsesFilter, CustomersTbl.EquipType, CustomersTbl.CustomerTypeID," +
              " CustomersTbl.TypicallySecToo, CustomersTbl.PriPrefQty, CustomersTbl.SecPrefQty, CustomersTbl.ReminderCount, " +
              " CustomersTbl.autofulfill, CustomersTbl.AlwaysSendChkUp, CustomersAccInfoTbl.RequiresPurchOrder, CustomersTbl.enabled, CustomersTbl.Notes, " +
              " ClientUsageTbl.LastCupCount, ClientUsageTbl.NextCoffeeBy, ClientUsageTbl.NextCleanOn, ClientUsageTbl.NextFilterEst, " +
              " ClientUsageTbl.NextDescaleEst, ClientUsageTbl.NextServiceEst, ClientUsageTbl.DailyConsumption, " +
              " NextRoastDateByCityTbl.PreperationDate, NextRoastDateByCityTbl.DeliveryDate, NextRoastDateByCityTbl.NextPreperationDate, NextRoastDateByCityTbl.NextDeliveryDate" +
        " FROM ((((CustomersTbl INNER JOIN" +
              " ClientUsageTbl ON CustomersTbl.CustomerID = ClientUsageTbl.CustomerId) LEFT OUTER JOIN" +
              " CustomersAccInfoTbl ON CustomersTbl.CustomerID = CustomersAccInfoTbl.CustomerID) LEFT OUTER JOIN" +
              " NextRoastDateByCityTbl ON CustomersTbl.City = NextRoastDateByCityTbl.CityID) LEFT OUTER JOIN" +
              " ItemNoStockItemQry ON CustomersTbl.CoffeePreference = ItemNoStockItemQry.ItemTypeID) "+
        " WHERE ((LastDateSentReminder IS Null) OR (LastDateSentReminder <> ?)) AND (CustomersTbl.enabled=True) " +
              " AND (CustomersTbl.PredictionDisabled=False) " +
              " AND ((ClientUsageTbl.NextCoffeeBy > ?) AND (" +
                    "(NextRoastDateByCityTbl.NextDeliveryDate<=DateAdd('d', 9, ClientUsageTbl.NextCoffeeBy)) OR " +
                    "CustomersTbl.AlwaysSendChkUp=True) )" +
              " AND (NOT Exists  (SELECT  OrdersTbl.CustomerId FROM OrdersTbl  " +
                   " WHERE (OrdersTbl.CustomerId=CustomersTbl.CustomerId) AND (OrdersTbl.RoastDate>=Date() AND  " +
                           " OrdersTbl.RoastDate<=DateAdd('d',9,Date()))	 )) " +
         " ORDER BY CustomersTbl.CompanyName";
/*
   "SELECT CustomersTbl.CustomerID AS ContactID, CustomersTbl.CompanyName, CustomersTbl.ContactTitle, CustomersTbl.ContactFirstName, " +
             "CustomersTbl.ContactLastName, CustomersTbl.ContactAltFirstName, CustomersTbl.ContactAltLastName, CustomersTbl.Department, " +
             "CustomersTbl.BillingAddress, CustomersTbl.City, CustomersTbl.PostalCode, CustomersTbl.PreferedAgent, CustomersTbl.SalesAgentID, " +
             "CustomersTbl.PhoneNumber, CustomersTbl.Extension, CustomersTbl.FaxNumber, CustomersTbl.CellNumber, CustomersTbl.EmailAddress,  " +
             "CustomersTbl.AltEmailAddress, CustomersTbl.UsesFilter, CustomersTbl.EquipType, CustomersTbl.CustomerTypeID, CustomersTbl.TypicallySecToo," +
             "CustomersTbl.PriPrefQty, CustomersTbl.SecPrefQty, CustomersTbl.ReminderCount, " + 
             "CustomersTbl.autofulfill, CustomersTbl.AlwaysSendChkUp, CustomersTbl.enabled, CustomersTbl.Notes, " +
             "ClientUsageTbl.LastCupCount, ClientUsageTbl.NextCoffeeBy, ClientUsageTbl.NextCleanOn, ClientUsageTbl.NextFilterEst, ClientUsageTbl.NextDescaleEst, "+
             "ClientUsageTbl.NextServiceEst, ClientUsageTbl.DailyConsumption,  " +
             "NextRoastDateByCityTbl.PreperationDate, NextRoastDateByCityTbl.DeliveryDate, NextRoastDateByCityTbl.NextPreperationDate, NextRoastDateByCityTbl.NextDeliveryDate " +
        "FROM ((CustomersTbl INNER JOIN ClientUsageTbl ON CustomersTbl.CustomerID = ClientUsageTbl.CustomerId) " +
              " LEFT JOIN NextRoastDateByCityTbl ON CustomersTbl.City = NextRoastDateByCityTbl.CityID) " +
              " LEFT JOIN ItemNoStockItemQry ON CustomersTbl.CoffeePreference = ItemNoStockItemQry.ItemTypeID  " +
        " WHERE ((LastDateSentReminder IS Null) OR (LastDateSentReminder <> ?)) AND (CustomersTbl.enabled=True)  " +
              " AND (CustomersTbl.PredictionDisabled=False) " +
              " AND ((ClientUsageTbl.NextCoffeeBy < ?) OR " +
                    "(NextRoastDateByCityTbl.NextDeliveryDate<=DateAdd('d', 8, ClientUsageTbl.NextCoffeeBy)) OR " +
                    "CustomersTbl.AlwaysSendChkUp=True) " +
              " AND (NOT Exists  (SELECT  OrdersTbl.CustomerId FROM OrdersTbl  " +
                   " WHERE (OrdersTbl.CustomerId=CustomersTbl.CustomerId) AND (OrdersTbl.RoastDate>=Date() AND  " +
                           " OrdersTbl.RoastDate<=DateAdd('d',9,Date()))	 )) " +
         " ORDER BY CustomersTbl.CompanyName";
*/
//              "                                                     (OrdersTbl.RoastDate<=DateAdd('d',10,NextRoastDateByCityTbl.NextDeliveryDate)) ))))" +

//              "                                                    ((OrdersTbl.RoastDate>DateAdd('ww',-1,NextRoastDateByCityTbl.PreperationDate)) AND " +
//              "                                                     (OrdersTbl.RoastDate<=DateAdd('d',3,NextRoastDateByCityTbl.DeliveryDate)) ))))" +


    #endregion

    public List<ContactsThayMayNeedData> GetContactsThatMayNeedNextWeek()
    {
      List<ContactsThayMayNeedData> _DataItems = new List<ContactsThayMayNeedData>();
      TrackerDb _TDB = new classes.TrackerDb();
      _TDB.AddWhereParams(System.DateTime.Now.Date, System.Data.DbType.Date);
      SysDataTbl _SysData = new SysDataTbl();
      _TDB.AddWhereParams(_SysData.GetMinReminderDate().Date, DbType.Date);
/* this looks wrong the nextcoffee by should be date + 9
      _TDB.AddWhereParams(System.DateTime.Now.Date.AddDays(9), DbType.Date);
      */

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_CONTACTSTHATMAYNEEDNEXTWEEK);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          ContactsThayMayNeedData _DataItem = new ContactsThayMayNeedData();
          #region GetDataFromQuery
          // first Customer Contact DATA
          _DataItem.CustomerData.CustomerID = (_DataReader["ContactID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["ContactID"]);
          _DataItem.CustomerData.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? string.Empty : _DataReader["CompanyName"].ToString();
          _DataItem.CustomerData.ContactTitle = (_DataReader["ContactTitle"] == DBNull.Value) ? string.Empty : _DataReader["ContactTitle"].ToString();
          _DataItem.CustomerData.ContactFirstName = (_DataReader["ContactFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactFirstName"].ToString();
          _DataItem.CustomerData.ContactLastName = (_DataReader["ContactLastName"] == DBNull.Value) ? string.Empty : _DataReader["ContactLastName"].ToString();
          _DataItem.CustomerData.ContactAltFirstName = (_DataReader["ContactAltFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactAltFirstName"].ToString();
          _DataItem.CustomerData.ContactAltLastName = (_DataReader["ContactAltLastName"] == DBNull.Value) ? string.Empty : _DataReader["ContactAltLastName"].ToString();
          _DataItem.CustomerData.Department = (_DataReader["Department"] == DBNull.Value) ? string.Empty : _DataReader["Department"].ToString();
          _DataItem.CustomerData.BillingAddress = (_DataReader["BillingAddress"] == DBNull.Value) ? string.Empty : _DataReader["BillingAddress"].ToString();
          _DataItem.CustomerData.City = (_DataReader["City"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["City"]);
          _DataItem.CustomerData.PostalCode = (_DataReader["PostalCode"] == DBNull.Value) ? string.Empty : _DataReader["PostalCode"].ToString();
          _DataItem.CustomerData.PreferedAgent = (_DataReader["PreferedAgent"] == DBNull.Value) ? TrackerTools.CONST_DEFAULT_DELIVERYBYID : Convert.ToInt32(_DataReader["PreferedAgent"]);
          _DataItem.CustomerData.SalesAgentID = (_DataReader["SalesAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SalesAgentID"]);
          _DataItem.CustomerData.PhoneNumber = (_DataReader["PhoneNumber"] == DBNull.Value) ? string.Empty : _DataReader["PhoneNumber"].ToString();
          _DataItem.CustomerData.Extension = (_DataReader["Extension"] == DBNull.Value) ? string.Empty : _DataReader["Extension"].ToString();
          _DataItem.CustomerData.FaxNumber = (_DataReader["FaxNumber"] == DBNull.Value) ? string.Empty : _DataReader["FaxNumber"].ToString();
          _DataItem.CustomerData.CellNumber = (_DataReader["CellNumber"] == DBNull.Value) ? string.Empty : _DataReader["CellNumber"].ToString();
          _DataItem.CustomerData.EmailAddress = (_DataReader["EmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["EmailAddress"].ToString();
          _DataItem.CustomerData.AltEmailAddress = (_DataReader["AltEmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["AltEmailAddress"].ToString();
          _DataItem.CustomerData.EquipType = (_DataReader["EquipType"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["EquipType"]);
          _DataItem.CustomerData.CustomerTypeID = (_DataReader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTypeID"]);
          _DataItem.CustomerData.PriPrefQty = (_DataReader["PriPrefQty"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["PriPrefQty"]);
          _DataItem.CustomerData.SecPrefQty = (_DataReader["SecPrefQty"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["SecPrefQty"]);
          _DataItem.CustomerData.ReminderCount = (_DataReader["ReminderCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderCount"]);
          _DataItem.CustomerData.autofulfill = (_DataReader["autofulfill"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["autofulfill"]);
          _DataItem.CustomerData.UsesFilter = (_DataReader["UsesFilter"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UsesFilter"]);
          _DataItem.CustomerData.TypicallySecToo = (_DataReader["TypicallySecToo"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TypicallySecToo"]);
          _DataItem.CustomerData.AlwaysSendChkUp = (_DataReader["AlwaysSendChkUp"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AlwaysSendChkUp"]);
          _DataItem.CustomerData.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);
          _DataItem.CustomerData.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          // now Customer usage DATA
          _DataItem.ClientUsageData.LastCupCount = (_DataReader["LastCupCount"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["LastCupCount"]);
          _DataItem.ClientUsageData.NextCoffeeBy = (_DataReader["NextCoffeeBy"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextCoffeeBy"]).Date;
          _DataItem.ClientUsageData.NextCleanOn = (_DataReader["NextCleanOn"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextCleanOn"]).Date;
          _DataItem.ClientUsageData.NextFilterEst = (_DataReader["NextFilterEst"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextFilterEst"]).Date;
          _DataItem.ClientUsageData.NextDescaleEst = (_DataReader["NextDescaleEst"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextDescaleEst"]).Date;
          _DataItem.ClientUsageData.NextServiceEst = (_DataReader["NextServiceEst"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextServiceEst"]).Date;
          _DataItem.ClientUsageData.DailyConsumption = (_DataReader["DailyConsumption"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["DailyConsumption"]);
          // now next date
          _DataItem.NextRoastDateByCityData.PrepDate = (_DataReader["PreperationDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["PreperationDate"]).Date;
          _DataItem.NextRoastDateByCityData.DeliveryDate = (_DataReader["DeliveryDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DeliveryDate"]).Date;
          _DataItem.NextRoastDateByCityData.NextPrepDate = (_DataReader["NextPreperationDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextPreperationDate"]).Date;
          _DataItem.NextRoastDateByCityData.NextDeliveryDate = (_DataReader["NextDeliveryDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextDeliveryDate"]).Date;
          #endregion

          _DataItems.Add(_DataItem);
        }
        _DataReader.Dispose();
      }
      _TDB.Close();
      return _DataItems;
    }

  }
}