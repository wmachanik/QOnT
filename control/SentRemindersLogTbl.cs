//--- auto generated class for table: SentRemindersLogTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class SentRemindersLogTbl
  {
    // internal variable declarations
    private int _ReminderID;
    private long _CustomerID;
    private DateTime _DateSentReminder;
    private DateTime _NextPrepDate;
    private bool _ReminderSent;
    private bool _HadAutoFulfilItem;
    private bool _HadReoccurItems;
    // class definition
    public SentRemindersLogTbl()
    {
      _ReminderID = 0;
      _CustomerID = 0;
      _DateSentReminder = System.DateTime.Now;
      _NextPrepDate = System.DateTime.Now.Date;
      _ReminderSent = false;
      _HadAutoFulfilItem = false;
      _HadReoccurItems = false;
    }
    // get and sets of public
    public int ReminderID { get { return _ReminderID;}  set { _ReminderID = value;} }
    public long CustomerID { get { return _CustomerID;}  set { _CustomerID = value;} }
    public DateTime DateSentReminder { get { return _DateSentReminder;}  set { _DateSentReminder = value;} }
    public DateTime NextPrepDate { get { return _NextPrepDate;}  set { _NextPrepDate = value;} }
    public bool ReminderSent { get { return _ReminderSent;}  set { _ReminderSent = value;} }
    public bool HadAutoFulfilItem { get { return _HadAutoFulfilItem;}  set { _HadAutoFulfilItem = value;} }
    public bool HadReoccurItems { get { return _HadReoccurItems;}  set { _HadReoccurItems = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT ReminderID, CustomerID, DateSentReminder, NextPrepDate, ReminderSent, HadAutoFulfilItem, HadReoccurItems FROM SentRemindersLogTbl";
    const string CONST_SQL_SELECT_BYDATESENT = "SELECT ReminderID, CustomerID, DateSentReminder, NextPrepDate, ReminderSent, HadAutoFulfilItem, HadReoccurItems " +
      " FROM SentRemindersLogTbl WHERE DateSentReminder = ?";
    const string CONST_SQL_SELECT_REMINDERDATESSENT = "SELECT DISTINCT TOP 20 DateSentReminder FROM SentRemindersLogTbl ORDER BY DateSentReminder DESC";
    const string CONST_SQL_UPDATE = "UPDATE SentRemindersLogTbl SET CustomerID = ?, DateSentReminder = ?, NextPrepDate = ?, ReminderSent = ?, HadAutoFulfilItem = ?, HadReoccurItems = ? " +
      " WHERE (SentRemindersLogTbl.ReminderID = ?)";
    const string CONST_SQL_INSERT = "INSERT INTO SentRemindersLogTbl (CustomerID, DateSentReminder, NextPrepDate, ReminderSent, HadAutoFulfilItem, HadReoccurItems) " +
      "VALUES (?, ?, ?, ?, ?, ?)";
  #endregion

    public List<SentRemindersLogTbl> GetAll(string SortBy)
    {
      List<SentRemindersLogTbl> _DataItems = new List<SentRemindersLogTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          SentRemindersLogTbl _DataItem = new SentRemindersLogTbl();

          _DataItem.ReminderID = (_DataReader["ReminderID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.DateSentReminder = (_DataReader["DateSentReminder"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateSentReminder"]).Date;
          _DataItem.NextPrepDate = (_DataReader["NextPrepDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextPrepDate"]).Date;
          _DataItem.ReminderSent = (_DataReader["ReminderSent"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["ReminderSent"]);
          _DataItem.HadAutoFulfilItem = (_DataReader["HadAutoFulfilItem"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["HadAutoFulfilItem"]);
          _DataItem.HadReoccurItems = (_DataReader["HadReoccurItems"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["HadReoccurItems"]);
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    

    public List<SentRemindersLogTbl> GetAllByDate(DateTime pDateSent, string SortBy)
    {
      List<SentRemindersLogTbl> _DataItems = new List<SentRemindersLogTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT_BYDATESENT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      _TrackerDb.AddWhereParams(pDateSent.Date, DbType.Date);
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          SentRemindersLogTbl _DataItem = new SentRemindersLogTbl();

          _DataItem.ReminderID = (_DataReader["ReminderID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ReminderID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.DateSentReminder = (_DataReader["DateSentReminder"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateSentReminder"]).Date;
          _DataItem.NextPrepDate = (_DataReader["NextPrepDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["NextPrepDate"]).Date;
          _DataItem.ReminderSent = (_DataReader["ReminderSent"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["ReminderSent"]);
          _DataItem.HadAutoFulfilItem = (_DataReader["HadAutoFulfilItem"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["HadAutoFulfilItem"]);
          _DataItem.HadReoccurItems = (_DataReader["HadReoccurItems"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["HadReoccurItems"]);
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public List<DateTime> GetLast20DatesReminderSent()
    {
      List<DateTime> _DataItems = new List<DateTime>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT_REMINDERDATESSENT;
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          DateTime _DataItem = new DateTime();

          _DataItem = (_DataReader["DateSentReminder"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateSentReminder"]).Date;
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }

    public string UpdateLogItem(SentRemindersLogTbl pSentRemindersLog)
    {
      string _ErrString = String.Empty;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pSentRemindersLog.CustomerID, DbType.Int64);
      _TDB.AddParams(pSentRemindersLog.DateSentReminder, DbType.Date);
      _TDB.AddParams(pSentRemindersLog.NextPrepDate, DbType.Date);
      _TDB.AddParams(pSentRemindersLog.ReminderSent, DbType.Boolean);
      _TDB.AddParams(pSentRemindersLog.HadAutoFulfilItem, DbType.Boolean);
      _TDB.AddParams(pSentRemindersLog.HadReoccurItems, DbType.Boolean);
      _TDB.AddWhereParams(pSentRemindersLog.ReminderID, DbType.Int32);
      _ErrString = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();

      return _ErrString;
    }
    public string InsertLogItem(SentRemindersLogTbl pSentRemindersLog)
    {
      string _ErrString = String.Empty;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pSentRemindersLog.CustomerID, DbType.Int64);
      _TDB.AddParams(pSentRemindersLog.DateSentReminder, DbType.Date);
      _TDB.AddParams(pSentRemindersLog.NextPrepDate, DbType.Date);
      _TDB.AddParams(pSentRemindersLog.ReminderSent, DbType.Boolean);
      _TDB.AddParams(pSentRemindersLog.HadAutoFulfilItem, DbType.Boolean);
      _TDB.AddParams(pSentRemindersLog.HadReoccurItems, DbType.Boolean);
      _ErrString = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();

      return _ErrString;
    }
  }
}
