/// --- auto generated class for table: PaymentTermsTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Data;                  // for IDataReader and DbType
using QOnT.classes;        // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class PaymentTermsTbl
  {
    public const int CONST_DEFAULT_PAYMENTTERMID = 1; // should be Due on receipt

    #region InternalVariableDeclarations
    private int _PaymentTermID;
    private string _PaymentTermDesc;
    private int _PaymentDays;
    private int _DayOfMonth;
    private bool _UseDays;
    private bool _Enabled;
    private string _Notes;
    #endregion

    // class definition
    public PaymentTermsTbl()
    {
      _PaymentTermID = 0;
      _PaymentTermDesc = string.Empty;
      _PaymentDays = 0;
      _DayOfMonth = 0;
      _UseDays = false;
      _Enabled = false;
      _Notes = string.Empty;
    }
    #region PublicVariableDeclarations
    public int PaymentTermID { get { return _PaymentTermID;}  set { _PaymentTermID = value;} }
    public string PaymentTermDesc { get { return _PaymentTermDesc;}  set { _PaymentTermDesc = value;} }
    public int PaymentDays { get { return _PaymentDays;}  set { _PaymentDays = value;} }
    public int DayOfMonth { get { return _DayOfMonth;}  set { _DayOfMonth = value;} }
    public bool UseDays { get { return _UseDays;}  set { _UseDays = value;} }
    public bool Enabled { get { return _Enabled;}  set { _Enabled = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }
    #endregion

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT PaymentTermID, PaymentTermDesc, PaymentDays, DayOfMonth, UseDays, Enabled, Notes FROM PaymentTermsTbl";
    const string CONST_SQL_SELECTBYDESC = "SELECT PaymentTermID FROM PaymentTermsTbl WHERE PaymentTermDesc = ?";
    const string CONST_SQL_INSERT = "INSERT INTO PaymentTermsTbl (PaymentTermDesc, PaymentDays, DayOfMonth, UseDays, Enabled, Notes)" + 
                          " VALUES ( ?, ?, ?, ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE PaymentTermsTbl SET PaymentTermDesc = ?, PaymentDays = ?, DayOfMonth = ?, UseDays = ?, Enabled = ?, Notes = ?" + 
                           " WHERE (PaymentTermID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM PaymentTermsTbl WHERE (PaymentTermID = ?)";
    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<PaymentTermsTbl> GetAll(string SortBy)
    {
      List<PaymentTermsTbl> _DataItems = new List<PaymentTermsTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);

      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          PaymentTermsTbl _DataItem = new PaymentTermsTbl();

          #region StoreThisDataItem
          _DataItem.PaymentTermID = (_DataReader["PaymentTermID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PaymentTermID"]);
          _DataItem.PaymentTermDesc = (_DataReader["PaymentTermDesc"] == DBNull.Value) ? string.Empty : _DataReader["PaymentTermDesc"].ToString();
          _DataItem.PaymentDays = (_DataReader["PaymentDays"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PaymentDays"]);
          _DataItem.DayOfMonth = (_DataReader["DayOfMonth"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["DayOfMonth"]);
          _DataItem.UseDays = (_DataReader["UseDays"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["UseDays"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public int GetPaymentTermIDByDesc(string pPaymentTermDesc)
    {
      int _PaymentTermID = 0;
      TrackerDb _TDB = new TrackerDb();
      // params would go here if need
      _TDB.AddWhereParams(pPaymentTermDesc, DbType.String, "@PaymentTermDesc");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTBYDESC);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _PaymentTermID = (_DataReader["PaymentTermID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PaymentTermID"]);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _PaymentTermID;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string Insert(PaymentTermsTbl pPaymentTermsTbl)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region InsertParameters
      _TDB.AddParams(pPaymentTermsTbl.PaymentTermDesc, DbType.String, "@PaymentTermDesc");
      _TDB.AddParams(pPaymentTermsTbl.PaymentDays, DbType.Int32, "@PaymentDays");
      _TDB.AddParams(pPaymentTermsTbl.DayOfMonth, DbType.Int32, "@DayOfMonth");
      _TDB.AddParams(pPaymentTermsTbl.UseDays, DbType.Boolean, "@UseDays");
      _TDB.AddParams(pPaymentTermsTbl.Enabled, DbType.Boolean, "@Enabled");
      _TDB.AddParams(pPaymentTermsTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string Update(PaymentTermsTbl pPaymentTermsTbl)
    { return Update(pPaymentTermsTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string Update(PaymentTermsTbl pPaymentTermsTbl, int pOrignal_PaymentTermID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region UpdateParameters
      if (pOrignal_PaymentTermID > 0)
        _TDB.AddWhereParams(pOrignal_PaymentTermID, DbType.Int32);  // check this line it assumes id field is int32
      else
        _TDB.AddWhereParams(pPaymentTermsTbl.PaymentTermID, DbType.Boolean, "@PaymentTermID");

      _TDB.AddParams(pPaymentTermsTbl.PaymentTermDesc, DbType.String, "@PaymentTermDesc" );
      _TDB.AddParams(pPaymentTermsTbl.PaymentDays, DbType.Int32, "@PaymentDays");
      _TDB.AddParams(pPaymentTermsTbl.DayOfMonth, DbType.Int32, "@DayOfMonth");
      _TDB.AddParams(pPaymentTermsTbl.UseDays, DbType.Boolean, "@UseDays");
      _TDB.AddParams(pPaymentTermsTbl.Enabled, DbType.Boolean, "@Enabled");
      _TDB.AddParams(pPaymentTermsTbl.Notes, DbType.String, "@Notes" );
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string Delete(PaymentTermsTbl pPaymentTermsTbl)
    { return Delete(pPaymentTermsTbl.PaymentTermID); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string Delete(int pPaymentTermID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pPaymentTermID, DbType.Int32, "@PaymentTermID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }

  }
}
