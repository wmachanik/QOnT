using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  // line items
  public class ItemContactRequires
  {
    #region InternalItems

    private long _TCIID;
    private int _ItemID;
    private long _CustomerID;
    private double _ItemQty;
    private int _ItemPrepID;
    private int _ItemPackagID;
    private bool _AutoFulfill;
    private bool _ReoccurOrder;      
    private long _ReoccurID;    /// used to set the reocuring item is done
                                     
//    DateTime _NextDateRequired;
    #endregion

    #region ClassInit
    public ItemContactRequires()
    {
      _TCIID = 0;
      _ItemID = 0;
      _CustomerID = 0;
      _ItemQty = 0.0;
      _ItemPrepID = _ItemPackagID = 0;
      _AutoFulfill = _ReoccurOrder = false;
      _ReoccurID = 0;
//      _NextDateRequired = DateTime.Now;
    }
    #endregion

    #region PublicItems
    public long TCIID { get { return _TCIID; } set { _TCIID = value; } }
    public int ItemID { get { return _ItemID; } set { _ItemID = value; } }
    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public double ItemQty { get { return _ItemQty; } set { _ItemQty = value; } }
    public int ItemPrepID { get { return _ItemPrepID; } set { _ItemPrepID = value; } }
    public int ItemPackagID { get { return _ItemPackagID; } set { _ItemPackagID = value; } }
    public bool AutoFulfill { get { return _AutoFulfill; } set { _AutoFulfill = value; } }
    public bool ReoccurOrder { get { return _ReoccurOrder; } set { _ReoccurOrder = value; } }
    public long ReoccurID { get { return _ReoccurID; } set { _ReoccurID = value; } }
    #endregion
  }

  public class ContactToRemindDetails
  {
    #region  InternalVars
    private long _TCCID;
    private long _CustomerID;
    private string _CompanyName;
    private string _ContactTitle;
    private string _ContactFirstName;
    private string _ContactAltFirstName;
    private int _CityID;
    private string _EmailAddress;
    private string _AltEmailAddress;
    private int _CustomerTypeID;
    private int _EquipTypeID;
    private bool _TypicallySecToo;
    private int _PreferedAgentID;
    private int _SalesAgentID;
    private bool _UsesFilter;
    private bool _autofulfill;
    private bool _enabled;
    private bool _AlwaysSendChkUp;
    private int _ReminderCount;
    private bool _RequiresPurchOrder;

    DateTime _LastDateSentReminder;
    DateTime _NextPrepDate;
    DateTime _NextDeliveryDate;
    DateTime _NextCoffee;
    DateTime _NextClean;
    DateTime _NextFilter;
    DateTime _NextDescal;
    DateTime _NextService;
    #endregion


    #region classdefinition

    // class definition
    public ContactToRemindDetails()
    {
      _TCCID = 0;
      _CustomerID = 0;
      _CompanyName = string.Empty;
      _ContactTitle = string.Empty;
      _ContactFirstName = string.Empty;
      _ContactAltFirstName = string.Empty;
      _CityID = 0;
      _EmailAddress = string.Empty;
      _AltEmailAddress = string.Empty;
      _CustomerTypeID = 0;
      _EquipTypeID = 0;
      _TypicallySecToo = false;
      _PreferedAgentID = 0;
      _SalesAgentID = 0;
      _UsesFilter = false;
      //_AutoFulfill = _ReoccurOrder = false;
      _enabled = _autofulfill = false;
      _AlwaysSendChkUp = _RequiresPurchOrder = false;
      _ReminderCount = 0;
      // Dates
      _NextPrepDate = _NextDeliveryDate = DateTime.Now.Date;
      _LastDateSentReminder = _NextCoffee = _NextClean = _NextFilter = _NextService = DateTime.MinValue;
    }

    #endregion

    #region public vars
    // get and sets of public
    public long TCCID { get { return _TCCID; } set { _TCCID = value; } }
    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public string CompanyName { get { return _CompanyName; } set { _CompanyName = value; } }
    public string ContactTitle { get { return _ContactTitle; } set { _ContactTitle = value; } } 
    public string ContactFirstName { get { return _ContactFirstName; } set { _ContactFirstName = value; } }    
    public string ContactAltFirstName { get { return _ContactAltFirstName; } set { _ContactAltFirstName = value; } }
    public int CityID { get { return _CityID; } set { _CityID = value; } }
    public string EmailAddress { get { return _EmailAddress; } set { _EmailAddress = value; } }
    public string AltEmailAddress { get { return _AltEmailAddress; } set { _AltEmailAddress = value; } }
    public int CustomerTypeID { get { return _CustomerTypeID; } set { _CustomerTypeID = value; } }
    public int EquipTypeID { get { return _EquipTypeID; } set { _EquipTypeID = value; } }
    public bool TypicallySecToo { get { return _TypicallySecToo; } set { _TypicallySecToo = value; } }
    public int PreferedAgentID { get { return _PreferedAgentID; } set { _PreferedAgentID = value; } }
    public int SalesAgentID { get { return _SalesAgentID; } set { _SalesAgentID = value; } }
    public bool UsesFilter { get { return _UsesFilter; } set { _UsesFilter = value; } }
    public bool autofulfill { get { return _autofulfill; } set { _autofulfill = value; } }
    public bool enabled { get { return _enabled; } set { _enabled = value; } }
    public bool AlwaysSendChkUp { get { return _AlwaysSendChkUp; } set { _AlwaysSendChkUp = value; } }
    public int ReminderCount { get { return _ReminderCount; } set { _ReminderCount = value; } }
    public bool RequiresPurchOrder { get { return _RequiresPurchOrder; } set { _RequiresPurchOrder = value; } }
    // dates
    public DateTime LastDateSentReminder { get { return _LastDateSentReminder; } set { _LastDateSentReminder = value; } }
    public DateTime NextPrepDate { get { return _NextPrepDate; } set { _NextPrepDate = value; } }
    public DateTime NextDeliveryDate { get { return _NextDeliveryDate; } set { _NextDeliveryDate = value; } }

    public DateTime NextCoffee { get { return _NextCoffee; } set { _NextCoffee = value; } }
    public DateTime NextClean { get { return _NextClean; } set { _NextClean = value; } }
    public DateTime NextFilter { get { return _NextFilter; } set { _NextFilter = value; } }
    public DateTime NextDescal { get { return _NextDescal; } set { _NextDescal = value; } }
    public DateTime NextService { get { return _NextService; } set { _NextService = value; } }

    #endregion
  }
  
  public class ContactToRemindWithItems : ContactToRemindDetails
  {
    // link to items
    List<ItemContactRequires> _ItemsContactRequires;

    public ContactToRemindWithItems()
    {
       // ContactToRemindDetails();
      _ItemsContactRequires = new List<ItemContactRequires>();
    }
    public virtual List<ItemContactRequires> ItemsContactRequires { get { return _ItemsContactRequires; } set { _ItemsContactRequires = value; } }

    // Const declarations now
    const string CONST_SQL_CUSTOMERSUSAGE_SELECT = "SELECT CustomersTbl.CustomerID, CustomersTbl.CompanyName, CustomersTbl.ContactTitle, " +
                        "  CustomersTbl.ContactFirstName, CustomersTbl.ContactAltFirstName, CustomersTbl.PostalCode, CustomersTbl.EmailAddress, " +
                        " CustomersTbl.AltEmailAddress, CustomersTbl.CustomerTypeID, CustomersTbl.PriPrefQty, CustomersTbl.TypicallySecToo, " +
                        " CustomersTbl.PreferedAgent, CustomersTbl.SalesAgentID, CustomersTbl.UsesFilter, CustomersTbl.autofulfill,  " +
                        " CustomersTbl.enabled, CustomersTbl.PredictionDisabled, CustomersTbl.AlwaysSendChkUp, CustomersTbl.ReminderCount, " +
                        " CustomersTbl.LastDateSentReminder, CustomersAccInfoTbl.RequiresPurchOrder, " +
                        " NextRoastDateByCityTbl.CityID, NextRoastDateByCityTbl.DeliveryDate, NextRoastDateByCityTbl.PreperationDate, " +
                        " ClientUsageTbl.NextCoffeeBy, ClientUsageTbl.NextCleanOn, ClientUsageTbl.NextFilterEst, ClientUsageTbl.NextDescaleEst, ClientUsageTbl.NextServiceEst" +
                      " FROM (((CustomersAccInfoTbl RIGHT OUTER JOIN" +
                        " CustomersTbl ON CustomersAccInfoTbl.CustomerID = CustomersTbl.CustomerID) LEFT OUTER JOIN" +
                        " ClientUsageTbl ON CustomersTbl.CustomerID = ClientUsageTbl.CustomerId) LEFT OUTER JOIN" +
                        " NextRoastDateByCityTbl ON CustomersTbl.City = NextRoastDateByCityTbl.CityID)" +
                      " WHERE (CustomersTbl.CustomerID = ?)";
/*      
      " SELECT CustomersTbl.CustomerID, CustomersTbl.CompanyName, CustomersTbl.ContactTitle, CustomersTbl.ContactFirstName, " +
                        " CustomersTbl.ContactAltFirstName, CustomersTbl.PostalCode, CustomersTbl.EmailAddress, CustomersTbl.AltEmailAddress, CustomersTbl.CustomerTypeID, " +
                        " CustomersTbl.PriPrefQty, CustomersTbl.TypicallySecToo, CustomersTbl.PreferedAgent, CustomersTbl.SalesAgentID, " +
                        " CustomersTbl.UsesFilter, CustomersTbl.autofulfill, CustomersTbl.enabled, CustomersTbl.PredictionDisabled, " +
                        " CustomersTbl.AlwaysSendChkUp, CustomersTbl.ReminderCount, CustomersTbl.LastDateSentReminder, " +
                        " NextRoastDateByCityTbl.CityID, NextRoastDateByCityTbl.DeliveryDate, NextRoastDateByCityTbl.PreperationDate, " +
                        " ClientUsageTbl.NextCoffeeBy, ClientUsageTbl.NextCleanOn, ClientUsageTbl.NextFilterEst, ClientUsageTbl.NextDescaleEst, ClientUsageTbl.NextServiceEst " +
                     " FROM ((ClientUsageTbl RIGHT OUTER JOIN CustomersTbl ON ClientUsageTbl.CustomerId = CustomersTbl.CustomerID) LEFT OUTER JOIN " +
                        " NextRoastDateByCityTbl ON CustomersTbl.City = NextRoastDateByCityTbl.CityID) " +
                     " WHERE (CustomersTbl.CustomerID = ?)";
*/ 
 
    public ContactToRemindWithItems GetCustomerDetails(long pCustomerID)
    {
      ContactToRemindWithItems _DataItem = null;

      TrackerDb _TrackerDB = new classes.TrackerDb();
      _TrackerDB.AddWhereParams(pCustomerID, DbType.Int64);

      IDataReader _DataReader = _TrackerDB.ExecuteSQLGetDataReader(CONST_SQL_CUSTOMERSUSAGE_SELECT);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _DataItem = new ContactToRemindWithItems();

          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? String.Empty : _DataReader["CompanyName"].ToString();
          _DataItem.ContactTitle = (_DataReader["ContactTitle"] == DBNull.Value) ? String.Empty : _DataReader["ContactTitle"].ToString();
          _DataItem.ContactFirstName = (_DataReader["ContactFirstName"] == DBNull.Value) ? String.Empty : _DataReader["ContactFirstName"].ToString();
          _DataItem.ContactAltFirstName = (_DataReader["ContactAltFirstName"] == DBNull.Value) ? String.Empty : _DataReader["ContactAltFirstName"].ToString();
          _DataItem.EmailAddress = (_DataReader["EmailAddress"] == DBNull.Value) ? String.Empty : _DataReader["EmailAddress"].ToString();
          _DataItem.AltEmailAddress = (_DataReader["AltEmailAddress"] == DBNull.Value) ? String.Empty : _DataReader["AltEmailAddress"].ToString();
          _DataItem.CustomerTypeID = (_DataReader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTypeID"]);
          _DataItem.TypicallySecToo = (_DataReader["TypicallySecToo"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TypicallySecToo"]);
          _DataItem.PreferedAgentID = (_DataReader["PreferedAgent"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PreferedAgent"]);
          _DataItem.SalesAgentID = (_DataReader["SalesAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SalesAgentID"]);
          _DataItem.UsesFilter = (_DataReader["UsesFilter"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UsesFilter"]);
          _DataItem.autofulfill = (_DataReader["autofulfill"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["autofulfill"]);
          _DataItem.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);
          _DataItem.AlwaysSendChkUp = (_DataReader["AlwaysSendChkUp"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AlwaysSendChkUp"]);
          _DataItem.ReminderCount = (_DataReader["ReminderCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderCount"]);
          _DataItem.LastDateSentReminder = (_DataReader["LastDateSentReminder"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["LastDateSentReminder"]).Date;
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          _DataItem.CityID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.NextDeliveryDate = (_DataReader["DeliveryDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["DeliveryDate"]).Date;
          _DataItem.NextPrepDate = (_DataReader["PreperationDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["PreperationDate"]).Date;
          //        _DataItem.LastCupCount = (_DataReader["LastCupCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastCupCount"]);
          _DataItem.NextCoffee = (_DataReader["NextCoffeeBy"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextCoffeeBy"]).Date;
          _DataItem.NextClean = (_DataReader["NextCleanOn"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextCleanOn"]).Date;
          _DataItem.NextFilter = (_DataReader["NextFilterEst"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextFilterEst"]).Date;
          _DataItem.NextDescal = (_DataReader["NextDescaleEst"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextDescaleEst"]).Date;
          _DataItem.NextService = (_DataReader["NextServiceEst"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["NextServiceEst"]).Date;
          //        _DataItem.DailyConsumption = (_DataReader["DailyConsumption"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["DailyConsumption"]);
          //        _DataItem.FilterAveCount = (_DataReader["FilterAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["FilterAveCount"]);
          //        _DataItem.ClientUsage.DescaleAveCount = (_DataReader["DescaleAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["DescaleAveCount"]);
          //        _DataItem.ClientUsage.ServiceAveCount = (_DataReader["ServiceAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["ServiceAveCount"]);
          //        _DataItem.ClientUsage.CleanAveCount = (_DataReader["CleanAveCount"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["CleanAveCount"]);
        }

        _DataReader.Close();
      }
      _TrackerDB.Close();

      return _DataItem;
    }

  }

  public class TempCoffeeCheckup
  {

    public TempCoffeeCheckup()
    {
    }

    #region ConstantDeclarations
    const string CONST_SQL_SELECTALLCONTACTS =
      "SELECT TCCID, CustomerID, CompanyName, ContactFirstName, ContactAltFirstName, CityID, EmailAddress, AltEmailAddress, CustomerTypeID, EquipTypeID, TypicallySecToo, " +
            " PreferedAgentID, SalesAgentID, UsesFilter, [enabled], AlwaysSendChkUp, RequiresPurchOrder, ReminderCount, NextPrepDate, NextDeliveryDate, NextCoffee, NextClean, NextFilter, " + 
            " NextDescal, NextService FROM TempCoffeecheckupCustomerTbl";
    const string CONST_SQL_SELECTCONTACTITEMSBYCUST = "SELECT TCIID, CustomerID, ItemID, ItemQty, ItemPrepID, ItemPackagID, AutoFulfill, ReoccurOrderId " +
                                                      " FROM TempCoffeecheckupItemsTbl WHERE CustomerID = ?";

    const string CONST_SQL_INSERTNEWCONTACTS = "INSERT INTO TempCoffeecheckupCustomerTbl" +
            " (CustomerID, CompanyName, ContactFirstName, ContactAltFirstName, CityID, EmailAddress, AltEmailAddress, CustomerTypeID, EquipTypeID, TypicallySecToo, " +
            "  PreferedAgentID, SalesAgentID, UsesFilter, [enabled], AlwaysSendChkUp, RequiresPurchOrder, ReminderCount, NextPrepDate, NextDeliveryDate, NextCoffee, NextClean, NextFilter, " +
            "  NextDescal, NextService) VALUES (?, ? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,?)";
    const string CONST_SQL_INSERTNEWITEMS = "INSERT INTO TempCoffeecheckupItemsTbl " +
                        " (CustomerID, ItemID, ItemQty, ItemPrepID, ItemPackagID, AutoFulfill, ReoccurOrderId) " +
                        " VALUES ( ?, ? ,? ,? ,? ,? ,? )";
    const string CONST_SQL_DELETEALLCONTACTS = "DELETE * FROM TempCoffeecheckupCustomerTbl";
    const string CONST_SQL_DELETEALLITEMS = "DELETE * FROM TempCoffeecheckupItemsTbl";
    #endregion

    public List<ContactToRemindDetails> GetAllContacts() { return GetAllContacts(String.Empty); }
    public List<ContactToRemindDetails> GetAllContacts(string SortBy)
    {
      List<ContactToRemindDetails> _DataItems = new List<ContactToRemindDetails>();

      string _sqlCmd = CONST_SQL_SELECTALLCONTACTS;
      _sqlCmd += (!String.IsNullOrEmpty(SortBy)) ? " ORDER BY " + SortBy : " ORDER BY CompanyName";   // add default order

      TrackerDb _TDB = new TrackerDb();
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {

        while (_DataReader.Read())
        {
          ContactToRemindDetails _DataItem = new ContactToRemindDetails();

          _DataItem.TCCID = (_DataReader["TCCID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["TCCID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? string.Empty : _DataReader["CompanyName"].ToString();
          _DataItem.ContactFirstName = (_DataReader["ContactFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactFirstName"].ToString();
          _DataItem.ContactAltFirstName = (_DataReader["ContactAltFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactAltFirstName"].ToString();
          _DataItem.CityID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.EmailAddress = (_DataReader["EmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["EmailAddress"].ToString();
          _DataItem.AltEmailAddress = (_DataReader["AltEmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["AltEmailAddress"].ToString();
          _DataItem.CustomerTypeID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.EquipTypeID = (_DataReader["EquipTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["EquipTypeID"]);
          _DataItem.TypicallySecToo = (_DataReader["TypicallySecToo"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TypicallySecToo"]);
          _DataItem.PreferedAgentID = (_DataReader["PreferedAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PreferedAgentID"]);
          _DataItem.SalesAgentID = (_DataReader["SalesAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SalesAgentID"]);
          _DataItem.UsesFilter = (_DataReader["UsesFilter"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UsesFilter"]);
          _DataItem.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);
          _DataItem.AlwaysSendChkUp = (_DataReader["AlwaysSendChkUp"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AlwaysSendChkUp"]);
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          _DataItem.ReminderCount = (_DataReader["ReminderCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderCount"]);
          _DataItem.NextPrepDate = (_DataReader["NextPrepDate"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextPrepDate"]).Date;
          _DataItem.NextDeliveryDate = (_DataReader["NextDeliveryDate"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextDeliveryDate"]).Date;
          _DataItem.NextCoffee = (_DataReader["NextCoffee"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextCoffee"]).Date;
          _DataItem.NextClean = (_DataReader["NextClean"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextClean"]).Date;
          _DataItem.NextFilter = (_DataReader["NextFilter"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextFilter"]).Date;
          _DataItem.NextDescal = (_DataReader["NextDescal"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextDescal"]).Date;
          _DataItem.NextService = (_DataReader["NextService"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextService"]).Date;

          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItems;
    }
    public List<ItemContactRequires> GetAllContactItems(long CustomerID, string SortBy)
    {
      List<ItemContactRequires> _DataItems = new List<ItemContactRequires>();

      if (CustomerID > 0)
      {
        string _sqlCmd = CONST_SQL_SELECTCONTACTITEMSBYCUST;
        _sqlCmd += (!String.IsNullOrEmpty(SortBy)) ? " ORDER BY " + SortBy : " ORDER BY ItemID";   // add default order
        TrackerDb _TDB = new TrackerDb();
        _TDB.AddWhereParams(CustomerID, DbType.Int64, "@CustomerID");

        IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
        if (_DataReader != null)
        { while (_DataReader.Read())
          {
            ItemContactRequires _DataItem = new ItemContactRequires();

            _DataItem.TCIID = (_DataReader["TCIID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["TCIID"]);
            _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
            _DataItem.ItemID = (_DataReader["ItemID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemID"]);
            _DataItem.ItemQty = (_DataReader["ItemQty"] == DBNull.Value) ? 0 : Convert.ToDouble(_DataReader["ItemQty"]);
            _DataItem.ItemPrepID = (_DataReader["ItemPrepID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemPrepID"]);
            _DataItem.ItemPackagID = (_DataReader["ItemPackagID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemPackagID"]);
            _DataItem.AutoFulfill = (_DataReader["AutoFulfill"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AutoFulfill"]);
            _DataItem.ReoccurID = (_DataReader["ReoccurOrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["ReoccurOrderID"]);
            _DataItem.ReoccurOrder = (_DataItem.ReoccurID > 0);
            //          _DataItem.NextDateRequired = (_DataReader["NextDateRequired"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextDateRequired"]);

            _DataItems.Add(_DataItem);
          }
          _DataReader.Close();
        }
        _TDB.Close();
      }
      return _DataItems;
    }

    public List<ContactToRemindWithItems> GetAllContactAndItems() { return GetAllContactAndItems(String.Empty); }
    public List<ContactToRemindWithItems> GetAllContactAndItems(string SortBy)
    {
      List<ContactToRemindWithItems> _DataItems = new List<ContactToRemindWithItems>();
      string _sqlCmd = CONST_SQL_SELECTALLCONTACTS;
      _sqlCmd += (!String.IsNullOrEmpty(SortBy)) ? " ORDER BY " + SortBy : " ORDER BY CompanyName";   // add default order
      TrackerDb _TDB = new TrackerDb();
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {

        while (_DataReader.Read())
        {
          ContactToRemindWithItems _DataItem = new ContactToRemindWithItems();

          _DataItem.TCCID = (_DataReader["TCCID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["TCCID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? string.Empty : _DataReader["CompanyName"].ToString();
          _DataItem.ContactFirstName = (_DataReader["ContactFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactFirstName"].ToString();
          _DataItem.ContactAltFirstName = (_DataReader["ContactAltFirstName"] == DBNull.Value) ? string.Empty : _DataReader["ContactAltFirstName"].ToString();
          _DataItem.CityID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.EmailAddress = (_DataReader["EmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["EmailAddress"].ToString();
          _DataItem.AltEmailAddress = (_DataReader["AltEmailAddress"] == DBNull.Value) ? string.Empty : _DataReader["AltEmailAddress"].ToString();
          _DataItem.CustomerTypeID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.EquipTypeID = (_DataReader["EquipTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["EquipTypeID"]);
          _DataItem.TypicallySecToo = (_DataReader["TypicallySecToo"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TypicallySecToo"]);
          _DataItem.PreferedAgentID = (_DataReader["PreferedAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PreferedAgentID"]);
          _DataItem.SalesAgentID = (_DataReader["SalesAgentID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SalesAgentID"]);
          _DataItem.UsesFilter = (_DataReader["UsesFilter"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UsesFilter"]);
          _DataItem.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);
          _DataItem.AlwaysSendChkUp = (_DataReader["AlwaysSendChkUp"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["AlwaysSendChkUp"]);
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          _DataItem.ReminderCount = (_DataReader["ReminderCount"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderCount"]);
          _DataItem.NextPrepDate = (_DataReader["NextPrepDate"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextPrepDate"]).Date;
          _DataItem.NextDeliveryDate = (_DataReader["NextDeliveryDate"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextDeliveryDate"]).Date;
          _DataItem.NextCoffee = (_DataReader["NextCoffee"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextCoffee"]).Date;
          _DataItem.NextClean = (_DataReader["NextClean"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextClean"]).Date;
          _DataItem.NextFilter = (_DataReader["NextFilter"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextFilter"]).Date;
          _DataItem.NextDescal = (_DataReader["NextDescal"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextDescal"]).Date;
          _DataItem.NextService = (_DataReader["NextService"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(_DataReader["NextService"]).Date;

          // now add the items for the customer
          _DataItem.ItemsContactRequires = GetAllContactItems(_DataItem.CustomerID, "");
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItems;
    }

    /// <summary>
    /// Insert Contact item into Temp Header
    /// </summary>
    /// <param name="pHeaderData">Contact Header Data to add</param>
    /// <returns>success or failure</returns>
    public bool InsertContacts(ContactToRemindDetails pHeaderData)
    {
      bool _Success = false;

      TrackerDb _TDB = new TrackerDb();
      #region Parameters
      // Add data sent CustomerId, OrderDate, RoastDate, RequiredByDate, ToBeDeliveredByID, Confirmed, Done, Notes
      _TDB.AddParams(pHeaderData.CustomerID, DbType.Int64, "@CustomerID");
      _TDB.AddParams(pHeaderData.CompanyName, DbType.String, "@CompanyName");
      _TDB.AddParams(pHeaderData.ContactFirstName, DbType.String, "@ContactFirstName");
      _TDB.AddParams(pHeaderData.ContactAltFirstName, DbType.String, "@ContactAltFirstName");
      _TDB.AddParams(pHeaderData.CityID, DbType.Int32, "@CityID");
      _TDB.AddParams(pHeaderData.EmailAddress, DbType.String, "@EmailAddress");
      _TDB.AddParams(pHeaderData.AltEmailAddress, DbType.String, "@AltEmailAddress");
      _TDB.AddParams(pHeaderData.CityID, DbType.Int32, "@CityID");
      _TDB.AddParams(pHeaderData.EquipTypeID, DbType.Int32, "@EquipTypeID");
      _TDB.AddParams(pHeaderData.TypicallySecToo, DbType.Boolean, "@TypicallySecToo");
      _TDB.AddParams(pHeaderData.PreferedAgentID, DbType.Int32, "@PreferedAgentID");
      _TDB.AddParams(pHeaderData.SalesAgentID, DbType.Int32, "@SalesAgentID");
      _TDB.AddParams(pHeaderData.UsesFilter, DbType.Boolean, "@UsesFilter");
      _TDB.AddParams(pHeaderData.enabled, DbType.Boolean, "@enabled");
      _TDB.AddParams(pHeaderData.AlwaysSendChkUp, DbType.Boolean, "@AlwaysSendChkUp");
      _TDB.AddParams(pHeaderData.RequiresPurchOrder, DbType.Boolean, "@RequiresPurchOrder");
      _TDB.AddParams(pHeaderData.ReminderCount, DbType.Int32, "@ReminderCount");
      _TDB.AddParams(pHeaderData.NextPrepDate, DbType.Date, "@NextPrepDate");
      _TDB.AddParams(pHeaderData.NextDeliveryDate, DbType.Date, "@NextDeliveryDate");
      _TDB.AddParams(pHeaderData.NextCoffee, DbType.Date, "@NextCoffee");
      _TDB.AddParams(pHeaderData.NextClean, DbType.Date, "@NextClean");
      _TDB.AddParams(pHeaderData.NextFilter, DbType.Date, "@NextFilter");
      _TDB.AddParams(pHeaderData.NextDescal, DbType.Date, "@NextDescal");
      _TDB.AddParams(pHeaderData.NextService, DbType.Date, "@NextService");
      #endregion

      _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(CONST_SQL_INSERTNEWCONTACTS));
      _TDB.Close();

      return _Success;
    }
    /// <summary>
    /// Insert Contact item into Temp Line
    /// </summary>
    /// <param name="pLineData">Contact Line Data to add</param>
    /// <returns>success or failure</returns>
    public bool InsertContactItems(ItemContactRequires pLineData)
    {
      bool _Success = false;
      TrackerDb _TDB = new TrackerDb();
      #region Parameters
      // Add data sent CustomerId, OrderDate, RoastDate, RequiredByDate, ToBeDeliveredByID, Confirmed, Done, Notes
      _TDB.AddParams(pLineData.CustomerID, DbType.Int64, "@CustomerID");
      _TDB.AddParams(pLineData.ItemID, DbType.Int32, "@ItemID");
      _TDB.AddParams(pLineData.ItemQty, DbType.Double, "@ItemQty");
      _TDB.AddParams(pLineData.ItemPrepID, DbType.Int32, "@ItemPrepID");
      _TDB.AddParams(pLineData.ItemPackagID, DbType.Int32, "@ItemPackagID");
      _TDB.AddParams(pLineData.AutoFulfill, DbType.Boolean, "@AutoFulfill");
      _TDB.AddParams(pLineData.ReoccurID, DbType.Int64, "@ReoccurID");
      #endregion

      _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(CONST_SQL_INSERTNEWITEMS));
      _TDB.Close();
      return _Success;
    }
/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[TrackerDotNet.classes.TrackerDb.CONST_CONSTRING].ConnectionString;
      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        OleDbCommand _cmd = new OleDbCommand(CONST_SQL_INSERTNEWITEMS, _conn);
        #region Parameters
        // Add data sent CustomerId, OrderDate, RoastDate, RequiredByDate, ToBeDeliveredByID, Confirmed, Done, Notes
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.CustomerID, DbType = System.Data.DbType.Int64 });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.ItemID, DbType = System.Data.DbType.Int32 });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.ItemQty, DbType = System.Data.DbType.Double });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.ItemPrepID, DbType = System.Data.DbType.Int32 });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.ItemPackagID, DbType = System.Data.DbType.Int32 });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.AutoFulfill, DbType = System.Data.DbType.Boolean });
        _cmd.Parameters.Add(new OleDbParameter { Value = pLineData.ReoccurID, DbType = System.Data.DbType.Int64});
        #endregion
        try
        {
          _conn.Open();
          _Success = (_cmd.ExecuteNonQuery() >= 0);
        }
        catch (OleDbException oleErr)
        { _Success = oleErr.ErrorCode != 0; }
        finally
        { _conn.Close(); }

        _cmd.Dispose();
      }
      return _Success;
    }
 */ 
    /// <summary>
    /// Delete all the records in the contact database
    /// </summary>
    public bool DeleteAllContactRecords()
    {
      TrackerDb _TDB = new TrackerDb();
      bool _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(CONST_SQL_DELETEALLCONTACTS));
      _TDB.Close();
      return _Success;
    }
/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[TrackerDotNet.classes.TrackerDb.CONST_CONSTRING].ConnectionString; ;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        OleDbCommand _cmd = new OleDbCommand(CONST_SQL_DELETEALLCONTACTS, _conn);                    // run the query we have built
        _conn.Open();
        _cmd.ExecuteNonQuery();
        _Success = _cmd.ExecuteNonQuery() >= 0;
        _conn.Close();
        _cmd.Dispose();
      }
      return _Success;
    }
*/
    /// <summary>
    /// Delete all the records in the contact database
    /// </summary>
    public bool DeleteAllContactItems()
    {
      TrackerDb _TDB = new TrackerDb();
      bool _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(CONST_SQL_DELETEALLITEMS));
      _TDB.Close();
      return _Success;
    }
/*
      bool _Success = false;
      string _connectionStr = ConfigurationManager.ConnectionStrings[TrackerDotNet.classes.TrackerDb.CONST_CONSTRING].ConnectionString; ;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        OleDbCommand _cmd = new OleDbCommand(CONST_SQL_DELETEALLITEMS, _conn);                    // run the query we have built
        _conn.Open();
        _cmd.ExecuteNonQuery();
        _Success = _cmd.ExecuteNonQuery() >= 0;
        _conn.Close();
        _cmd.Dispose();
      }
      return _Success;
    }
 */ 
  }
}