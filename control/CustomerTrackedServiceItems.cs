/// --- auto generated class for table: CustomerTrackedServiceItemsData
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using QOnT.classes;
using System.Data;

namespace QOnT.control
{
  public class CustomerTrackedServiceItems
  {
    public class CustomerTrackedServiceItemsData
    {
      // internal variable declarations
      private int _CustomerTrackedServiceItemsID;
      private int _CustomerTypeID;
      private int _ServiceTypeID;
      private string _Notes;
      // class definition
      public CustomerTrackedServiceItemsData()
      {
        _CustomerTrackedServiceItemsID = 0;
        _CustomerTypeID = 0;
        _ServiceTypeID = 0;
        _Notes = string.Empty;
      }
      // get and sets of public
      public int CustomerTrackedServiceItemsID { get { return _CustomerTrackedServiceItemsID; } set { _CustomerTrackedServiceItemsID = value; } }
      public int CustomerTypeID { get { return _CustomerTypeID; } set { _CustomerTypeID = value; } }
      public int ServiceTypeID { get { return _ServiceTypeID; } set { _ServiceTypeID = value; } }
      public string Notes { get { return _Notes; } set { _Notes = value; } }
    }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT CustomerTrackedServiceItemsID, CustomerTypeID, ServiceTypeID, Notes FROM CustomerTrackedServiceItemsTbl";
    const string CONST_SQL_SELECT_FORCUSTOMERTYPE = "SELECT CustomerTrackedServiceItemsID,  ServiceTypeID, Notes FROM CustomerTrackedServiceItemsTbl WHERE CustomerTypeID = ?";
  #endregion

    public List<CustomerTrackedServiceItemsData> GetAll(string SortBy)
    {
      List<CustomerTrackedServiceItemsData> _DataItems = new List<CustomerTrackedServiceItemsData>();
   
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CustomerTrackedServiceItemsData _DataItem = new CustomerTrackedServiceItemsData();

          _DataItem.CustomerTrackedServiceItemsID = (_DataReader["CustomerTrackedServiceItemsID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTrackedServiceItemsID"]);
          _DataItem.CustomerTypeID = (_DataReader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTypeID"]);
          _DataItem.ServiceTypeID = (_DataReader["ServiceTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ServiceTypeID"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
    
      return _DataItems;
    }
      
/*      
      string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_SELECT;
        if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          CustomerTrackedServiceItemsData _DataItem = new CustomerTrackedServiceItemsData();

          _DataItem.CustomerTrackedServiceItemsID = (_DataReader["CustomerTrackedServiceItemsID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTrackedServiceItemsID"]);
          _DataItem.CustomerTypeID = (_DataReader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTypeID"]);
          _DataItem.ServiceTypeID = (_DataReader["ServiceTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ServiceTypeID"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
      }
      return _DataItems;
    }
*/
    public List<CustomerTrackedServiceItemsData> GetAllByCustomerTypeID(int pCustomerTypeID)
    {
      List<CustomerTrackedServiceItemsData> _DataItems = new List<CustomerTrackedServiceItemsData>();
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pCustomerTypeID, DbType.Int32, "@CustomerTypeID");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECT_FORCUSTOMERTYPE);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CustomerTrackedServiceItemsData _DataItem = new CustomerTrackedServiceItemsData();

          _DataItem.CustomerTrackedServiceItemsID = (_DataReader["CustomerTrackedServiceItemsID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTrackedServiceItemsID"]);
          _DataItem.CustomerTypeID = pCustomerTypeID;
          _DataItem.ServiceTypeID = (_DataReader["ServiceTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ServiceTypeID"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();

          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString; ;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_SELECT_FORCUSTOMERTYPE;
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built
        _cmd.Parameters.Add(new OleDbParameter { Value = pCustomerTypeID });
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          CustomerTrackedServiceItemsData _DataItem = new CustomerTrackedServiceItemsData();

          _DataItem.CustomerTrackedServiceItemsID = (_DataReader["CustomerTrackedServiceItemsID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerTrackedServiceItemsID"]);
          _DataItem.CustomerTypeID = pCustomerTypeID;
          _DataItem.ServiceTypeID = (_DataReader["ServiceTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ServiceTypeID"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();

          _DataItems.Add(_DataItem);
        }
      }
      return _DataItems;
 */
    }
}
