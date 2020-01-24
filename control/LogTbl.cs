/// --- auto generated class for table: Log
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class LogTbl
  {
    // internal variable declarations
    private int _LogID;
    private DateTime _DateAdded;
    private int _UserID;
    private int _SectionID;
    private int _TranactionTypeID;
    private long _CustomerID;
    private string _Details;
    private string _Notes;
    // class definition
    public LogTbl()
    {
      _LogID = 0;
      _DateAdded = DateTime.MinValue;
      _UserID = 0;
      _SectionID = 0;
      _TranactionTypeID = 0;
      _CustomerID = 0;
      _Details = string.Empty;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int LogID { get { return _LogID; } set { _LogID = value; } }
    public DateTime DateAdded  { get { return _DateAdded; } set { _DateAdded = value; } }
    public int UserID { get { return _UserID; } set { _UserID = value; } }
    public int SectionID { get { return _SectionID; } set { _SectionID = value; } }
    public int TranactionTypeID { get { return _TranactionTypeID; } set { _TranactionTypeID = value; } }
    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public string Details { get { return _Details; } set { _Details = value; } }
    public string Notes { get { return _Notes; } set { _Notes = (value == null) ? string.Empty : value; } }

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT LogID, DateAdded, UserID, SectionID, TranactionTypeID, CustomerID, Details, Notes FROM LogTbl";
    const string CONST_SQL_INSERT = "INSERT INTO LogTbl (DateAdded, UserID, SectionID, TranactionTypeID, CustomerID, Details, Notes) VALUES (?,?,?,?,?,?,?)";
    #endregion

    public List<LogTbl> GetAll(string SortBy)
    {
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      _sqlCmd += (!String.IsNullOrEmpty(SortBy)) ? " ORDER BY " + SortBy : " ORDER BY DateAdded";   // add default order\

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      List<LogTbl> _DataItems = new List<LogTbl>();
      if (_DataReader != null)
      {

        while (_DataReader.Read())
        {
          LogTbl _DataItem = new LogTbl();

          _DataItem.LogID = (_DataReader["LogID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LogID"]);
          _DataItem.DateAdded = (_DataReader["DateAdded"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_DataReader["DateAdded"]).Date;
          _DataItem.UserID = (_DataReader["UserID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["UserID"]);
          _DataItem.SectionID = (_DataReader["SectionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SectionID"]);
          _DataItem.TranactionTypeID = (_DataReader["TranactionTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["TranactionTypeID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.Details = (_DataReader["Details"] == DBNull.Value) ? string.Empty : _DataReader["Details"].ToString();
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }

    public bool InsertLogItem(LogTbl objLog)
    {
      bool _inserted = false;

      TrackerDb _TDB = new TrackerDb();

      _TDB.AddParams(DateTime.Now, DbType.DateTime);   // other insert DateTime does not work only date
      _TDB.AddParams(objLog.UserID, DbType.Int32);
      _TDB.AddParams(objLog.SectionID, DbType.Int32);
      _TDB.AddParams(objLog.TranactionTypeID, DbType.Int32);
      _TDB.AddParams(objLog.CustomerID, DbType.Int64);
      _TDB.AddParams(objLog.Details);
      _TDB.AddParams(objLog.Notes);
      
      _inserted  = string.IsNullOrWhiteSpace(_TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT));
      _TDB.Close();

      return _inserted;
    }
    /// <summary>
    /// Insert an item into the log using the securityusername
    /// </summary>
    /// <param name="pSecurityUserName">security user name as per the Members table</param>
    /// <param name="pSectionID">Section's ID</param>
    /// <param name="pTranasctionTypeID">Transaction's Type ID</param>
    /// <param name="pDetails">Detai of what was done</param>
    /// <param name="pNotes">Notes of what was done</param>
    /// <returns>if the item was insertted</returns>
    public bool InsertLogItem(string pSecurityUserName, int pSectionID,int pTransactionTypeID,
      long pCustomerID, string pDetails, string pNotes)
    {
      bool _inserted = false;

      PersonsTbl _Persons = new PersonsTbl();
      int _SecurityUserID = _Persons.PersonsIDoFSecurityUsers(pSecurityUserName);

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(DateTime.Now, DbType.Date);
      _TDB.AddParams(_SecurityUserID, DbType.Int32);
      _TDB.AddParams(pSectionID, DbType.Int32);
      _TDB.AddParams(pTransactionTypeID, DbType.Int32);
      _TDB.AddParams(pCustomerID, DbType.Int64);
      _TDB.AddParams(pDetails);
      _TDB.AddParams(pNotes);

      _inserted = string.IsNullOrWhiteSpace(_TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT));
      _TDB.Close();

      return _inserted;
    }

    public void AddToWhatsChanged(string pItemName, string pOrig, string pNew, ref string pWhatChanged)
    {
      if (!pOrig.Equals(pNew))
      {
        if (!string.IsNullOrWhiteSpace(pWhatChanged))
          pWhatChanged += " - ";

        pWhatChanged += pItemName + " changed from: " + pOrig + " to " + pNew;
      }
    }

  }
}
