/// --- auto generated class for table: SysDataTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class SysDataTbl
  {
    // internal variable declarations
    private int _ID;
    private DateTime _LastReoccurringDate;
    private bool _DoReoccuringOrders;
    private DateTime _DateLastPrepDateCalcd;
    private DateTime _MinReminderDate;
    private int _GroupItemTypeID;
    // class definition
    public SysDataTbl()
    {
      _ID = 0;
      _LastReoccurringDate = System.DateTime.Now.Date;
      _DoReoccuringOrders = false;
      _DateLastPrepDateCalcd = System.DateTime.Now.Date;
      _MinReminderDate = System.DateTime.Now.Date;
      _GroupItemTypeID = 0;
    }
    // get and sets of public
    public int ID { get { return _ID;}  set { _ID = value;} }
    public DateTime LastReoccurringDate { get { return _LastReoccurringDate;}  set { _LastReoccurringDate = value;} }
    public bool DoReoccuringOrders { get { return _DoReoccuringOrders;}  set { _DoReoccuringOrders = value;} }
    public DateTime DateLastPrepDateCalcd { get { return _DateLastPrepDateCalcd;}  set { _DateLastPrepDateCalcd = value;} }
    public DateTime MinReminderDate { get { return _MinReminderDate;}  set { _MinReminderDate = value;} }
    public int GroupItemTypeID { get { return _GroupItemTypeID; } set { _GroupItemTypeID = value; } }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT ID, LastReoccurringDate, DoReoccuringOrders, DateLastPrepDateCalcd, MinReminderDate, GroupItemTypeID FROM SysDataTbl";
    const string CONST_SQL_SELECTMINREMINDERDATE = "SELECT MinReminderDate FROM SysDataTbl WHERE ID = 1";
    const string CONST_SQL_SELECTGROUPSERVICETYPEID = "SELECT GroupItemTypeID FROM SysDataTbl WHERE ID = 1";
    const string CONST_SQL_UPDATEBYID = "UPDATE SysDataTbl " +
      "SET LastReoccurringDate = ?, DoReoccuringOrders = ?, DateLastPrepDateCalcd = ?, MinReminderDate = ?, GroupItemTypeID = ?" +
      "WHERE (SysDataTbl.ID = ?)";
  #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<SysDataTbl> GetAll()    //(string SortBy)
    {
      List<SysDataTbl> _DataItems = new List<SysDataTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
//      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);

      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          SysDataTbl _DataItem = new SysDataTbl();

          _DataItem.ID = (_DataReader["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ID"]);
          _DataItem.LastReoccurringDate = (_DataReader["LastReoccurringDate"] == DBNull.Value) ? System.DateTime.MinValue : Convert.ToDateTime(_DataReader["LastReoccurringDate"]).Date;
          _DataItem.DoReoccuringOrders = (_DataReader["DoReoccuringOrders"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["DoReoccuringOrders"]);
          _DataItem.DateLastPrepDateCalcd = (_DataReader["DateLastPrepDateCalcd"] == DBNull.Value) ? System.DateTime.MinValue : Convert.ToDateTime(_DataReader["DateLastPrepDateCalcd"]).Date;
          _DataItem.MinReminderDate = (_DataReader["MinReminderDate"] == DBNull.Value) ? System.DateTime.MinValue : Convert.ToDateTime(_DataReader["MinReminderDate"]).Date;
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? TrackerDb.CONST_INVALIDID : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public DateTime GetMinReminderDate()
    {
      DateTime _MinDate = DateTime.MinValue;
      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTMINREMINDERDATE);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _MinDate = (_DataReader["MinReminderDate"] == DBNull.Value) ? System.DateTime.MinValue : Convert.ToDateTime(_DataReader["MinReminderDate"]).Date;

        _DataReader.Close();
      }
      _TDB.Close();

      return _MinDate;
    }
    
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public string Update(SysDataTbl SysDataItem) { return Update(SysDataItem, SysDataItem.ID); }
    public string Update(SysDataTbl SysDataItem, int orig_ID)
    {
      string _Result = String.Empty;

      if (orig_ID > 0)
      {
        TrackerDb _TDB = new TrackerDb();
        // LastReoccurringDate = ?, DoReoccuringOrders = ?, DateLastPrepDateCalcd = ?, MinReminderDate = ?, GroupItemTypeID = ?" +
        _TDB.AddParams(SysDataItem.LastReoccurringDate, DbType.Date);
        _TDB.AddParams(SysDataItem.DoReoccuringOrders, DbType.Boolean);
        _TDB.AddParams(SysDataItem.DateLastPrepDateCalcd, DbType.Date);
        _TDB.AddParams(SysDataItem.MinReminderDate, DbType.Date);
        _TDB.AddParams(SysDataItem.GroupItemTypeID, DbType.Int32, "@GroupItemTypeID");
        // "WHERE (SysDataTbl.ID = ?)
        _TDB.AddWhereParams(orig_ID, DbType.Int32);
        _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATEBYID);

        _TDB.Close();
      }
      return _Result;
    }
    /// <summary>
    /// Return the item in the SysDataTbl that is a group Item
    /// </summary>
    /// <returns>TheID of the ServiceType that is a group</returns>
    public int GetGroupItemTypeID()
    {
      int _Result = 0;

      TrackerDb _TDB = new TrackerDb();
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTGROUPSERVICETYPEID);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _Result = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);

        _DataReader.Close();
      }
      _TDB.Close();

      return _Result;
    }
  }
}
