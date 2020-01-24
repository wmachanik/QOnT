using System;
using System.Collections.Generic;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class ActiveDeliveryData
  {
    // internal variable declarations
    DateTime _RequiredByDate;
    string _Person;
    int _PersonID;
    // class definition
    public ActiveDeliveryData()
    {
      _RequiredByDate = DateTime.MinValue;
      _Person = String.Empty;
      _PersonID = 0;
    }

    public DateTime RequiredByDate { get { return _RequiredByDate; } set { _RequiredByDate = value; } }
    public string Person { get { return _Person; } set { _Person = value; } }
    public int PersonID { get { return _PersonID; } set { _PersonID = value; } }


#region ConstantDeclarations
    const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_SQL_SELECT_ACTIVEDELIVERIES = "SELECT DISTINCT OrdersTbl.RequiredByDate, PersonsTbl.Person, PersonsTbl.PersonID " +
                                   " FROM (OrdersTbl LEFT OUTER JOIN PersonsTbl ON OrdersTbl.ToBeDeliveredBy = PersonsTbl.PersonID)" +
                                   " WHERE (OrdersTbl.Done = false)";
#endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<ActiveDeliveryData> GetActiveDeliveryDateWithDeliveryPerson(string SortBy)
    {
      List<ActiveDeliveryData> _DataItems = new List<ActiveDeliveryData>();

      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT_ACTIVEDELIVERIES;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);        // run the query we have built
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          ActiveDeliveryData _DataItem = new ActiveDeliveryData();

          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["RequiredByDate"]).Date;
          _DataItem.Person = (_DataReader["Person"] == DBNull.Value) ? string.Empty : _DataReader["Person"].ToString();
          _DataItem.PersonID = (_DataReader["PersonID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PersonID"]);
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItems;
    }

    /*       
     * using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_SELECT_ACTIVEDELIVERIES;
        if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          ActiveDeliveryData _DataItem = new ActiveDeliveryData();

          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["RequiredByDate"]);
          _DataItem.Person = (_DataReader["Person"] == DBNull.Value) ? string.Empty : _DataReader["Person"].ToString();
          _DataItem.PersonID = (_DataReader["PersonID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PersonID"]);
          _DataItems.Add(_DataItem);
        }
      }
*/

    public class ActiveDeliveryDate
    {
      DateTime _RequiredByDate;

      public ActiveDeliveryDate()
      {
        _RequiredByDate = DateTime.MinValue;
      }

      public DateTime RequiredByDate { get { return _RequiredByDate; } set { _RequiredByDate = value;} }
    }

    const string CONST_SQL_SELECT_DISTINTDELIVERYDATES = "SELECT DISTINCT OrdersTbl.RequiredByDate FROM OrdersTbl " +
                                                    "WHERE (OrdersTbl.Done = false) ORDER BY RequiredByDate";

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public List<ActiveDeliveryDate> GetActiveDeliveryDates()
    {
      List<ActiveDeliveryDate> _DataItems = new List<ActiveDeliveryDate>();
      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECT_DISTINTDELIVERYDATES);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          ActiveDeliveryDate _DataItem = new ActiveDeliveryDate();

          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["RequiredByDate"]).Date;
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItems;
    }

    /*
      string _connectionStr = ConfigurationManager.ConnectionStrings[TrackerDotNet.classes.TrackerDb.CONST_CONSTRING].ConnectionString; ;
      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_SELECT_DISTINTDELIVERYDATES;
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          ActiveDeliveryDate _DataItem = new ActiveDeliveryDate();

          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["RequiredByDate"]);
          _DataItems.Add(_DataItem);
        }
      }

     */
  }
  
}