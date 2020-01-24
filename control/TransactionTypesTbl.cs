/// --- auto generated class for table: TransactionTypesTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using QOnT.classes;      // TrackerDot classes used for DB access
using System.Data;               // for IDataReader

namespace QOnT.control
{
  public class TransactionTypesTbl
  {
    #region PublicConstants
    public const int CONST_INSERT_TRANSACTION_INT = 1;
    public const int CONST_UPDATE_TRANSACTION_INT = 2;
    public const int CONST_ENABLE_TRANSACTION_INT = 3;
    public const int CONST_DISABLE_TRANSACTION_INT = 4;
    public const int CONST_CLOSED_TRANSACTION_INT = 5;
    // future CONST_FUTURE1_TRANSACTION_INT  - 6/7/8 / 1 2 3
    public const int CONST_DELETE_TRANSACTION_INT = 9;
    #endregion
    // internal variable declarations
    private int _TransactionID;
    private string _TransactionType;
    private string _Notes;
    // class definition
    public TransactionTypesTbl()
    {
      _TransactionID = 0;
      _TransactionType = string.Empty;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int TransactionID { get { return _TransactionID;}  set { _TransactionID = value;} }
    public string TransactionType { get { return _TransactionType;}  set { _TransactionType = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT TransactionID, TransactionType, Notes FROM TransactionTypesTbl";
    const string CONST_SQL_SELECTTTANSACTIONBYID = "SELECT [TransactionType] FROM [TransactionTypesTbl] WHERE ([TransactionID] = ?) ";
    const string CONST_SQL_INSERT = "INSERT INTO TransactionTypesTbl (TransactionID, TransactionType, Notes) VALUES (?,?,?)";
    const string CONST_SQL_UPDATE = "UPDATE TransactionTypesTbl SET TransactionType = ? , Notes = ? WHERE (TransactionID = ?)";
  #endregion

    public bool InsertDefaultTransactions()
    {
      bool _Inserted = false;
      List<TransactionTypesTbl> _DataItems = new List<TransactionTypesTbl>();

      // create a list of data to insert
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_INSERT_TRANSACTION_INT, TransactionType = "Insert", Notes = "Insert Transaction" });
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_UPDATE_TRANSACTION_INT, TransactionType = "Update", Notes = "Update Transaction" });
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_ENABLE_TRANSACTION_INT, TransactionType = "Enable", Notes = "Enable Transaction" });
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_DISABLE_TRANSACTION_INT, TransactionType = "Disble ", Notes = "Disble that are sent Transaction" });
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_CLOSED_TRANSACTION_INT, TransactionType = "Closed ", Notes = "Closed Transaction" });
      _DataItems.Add(new TransactionTypesTbl { TransactionID = CONST_DELETE_TRANSACTION_INT, TransactionType = "Delete", Notes = "Delete Transaction" });

      foreach (TransactionTypesTbl _DataItem in _DataItems)
        _Inserted = (string.IsNullOrWhiteSpace(InsertTransactionType(_DataItem)) && _Inserted);

      _DataItems.Clear();

      return _Inserted;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<TransactionTypesTbl> GetAll(string SortBy)
    {
      List<TransactionTypesTbl> _DataItems = new List<TransactionTypesTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          TransactionTypesTbl _DataItem = new TransactionTypesTbl();

          _DataItem.TransactionID = (_DataReader["TransactionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["TransactionID"]);
          _DataItem.TransactionType = (_DataReader["TransactionType"] == DBNull.Value) ? string.Empty : _DataReader["TransactionType"].ToString();
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public string GetTransactionTypeByID(Int32 pTransactionID)
    {
      string _TransactionType = string.Empty;
      TrackerDb _TDB = new TrackerDb();
      // for some reason this is not working so have replaced with string
      //_TDB.AddWhereParams(pTransactionID, DbType.Int32);
      string _sql = CONST_SQL_SELECTTTANSACTIONBYID;

      _sql = _sql.Replace("?", pTransactionID.ToString());

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sql);
      if (_DataReader != null)
      {
        if (!_DataReader.Read())
          _TransactionType = (_DataReader["TransactionType"] == DBNull.Value) ? string.Empty : _DataReader["TransactionType"].ToString();
        else
          _TransactionType = pTransactionID.ToString();
        _DataReader.Close();
      }
      _TDB.Close();

      return _TransactionType;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string InsertTransactionType(TransactionTypesTbl pTransactionType)
    {
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pTransactionType.TransactionID, DbType.Int32);
      _TDB.AddParams(pTransactionType.TransactionType);
      _TDB.AddParams(pTransactionType.Notes);

      string _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();

      return _Result;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public static void UpdateTransaction(TransactionTypesTbl pTransactionType) { UpdateTransaction(pTransactionType, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public static void UpdateTransaction(TransactionTypesTbl pTransactionType, int pOrignal_TransactionID)
    {
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pTransactionType.TransactionType);
      _TDB.AddParams(pTransactionType.Notes);

      if (pOrignal_TransactionID > 0)
        _TDB.AddWhereParams(pOrignal_TransactionID, DbType.Int32);
      else
        _TDB.AddWhereParams(pTransactionType.TransactionID, DbType.Int32);

      string _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();

      // return _Result;
    }
  }
}
