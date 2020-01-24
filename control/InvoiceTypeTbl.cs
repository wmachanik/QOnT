/// --- auto generated class for table: InvoiceTypeTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Data;                  // for IDataReader and DbType
using QOnT.classes;        // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class InvoiceTypeTbl
  {
    public const int CONST_DEFAULT_INVOICETYPEID = 1;  //standard
    #region InternalVariableDeclarations
    private int _InvoiceTypeID;
    private string _InvoiceTypeDesc;
    private bool _Enabled;
    private string _Notes;
    #endregion

    // class definition
    public InvoiceTypeTbl()
    {
      _InvoiceTypeID = 0;
      _InvoiceTypeDesc = string.Empty;
      _Enabled = false;
      _Notes = string.Empty;
    }
    #region PublicVariableDeclarations
    public int InvoiceTypeID { get { return _InvoiceTypeID;}  set { _InvoiceTypeID = value;} }
    public string InvoiceTypeDesc { get { return _InvoiceTypeDesc;}  set { _InvoiceTypeDesc = value;} }
    public bool Enabled { get { return _Enabled;}  set { _Enabled = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }
    #endregion

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT InvoiceTypeID, InvoiceTypeDesc, Enabled, Notes FROM InvoiceTypeTbl";
    const string CONST_SQL_INSERT = "INSERT INTO InvoiceTypeTbl (InvoiceTypeDesc, Enabled, Notes)" + 
                          " VALUES ( ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE InvoiceTypeTbl SET InvoiceTypeDesc = ?, Enabled = ?, Notes = ?" + 
                           " WHERE (InvoiceTypeID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM InvoiceTypeTbl WHERE (InvoiceTypeID = ?)";
    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<InvoiceTypeTbl> GetAll(string SortBy)
    {
      List<InvoiceTypeTbl> _DataItems = new List<InvoiceTypeTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          InvoiceTypeTbl _DataItem = new InvoiceTypeTbl();

          #region StoreThisDataItem
          _DataItem.InvoiceTypeID = (_DataReader["InvoiceTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["InvoiceTypeID"]);
          _DataItem.InvoiceTypeDesc = (_DataReader["InvoiceTypeDesc"] == DBNull.Value) ? string.Empty : _DataReader["InvoiceTypeDesc"].ToString();
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
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string Insert(InvoiceTypeTbl pInvoiceTypeTbl)
    {
      string _result = string.Empty;
      if (pInvoiceTypeTbl.InvoiceTypeID > 0)
      {
        // they have called insert by mistake ( or the environment has)
        _result = Update(pInvoiceTypeTbl, 0);
      }
      else
      {
        TrackerDb _TDB = new TrackerDb();

        #region InsertParameters
        _TDB.AddParams(pInvoiceTypeTbl.InvoiceTypeDesc, DbType.String, "@InvoiceTypeDesc");
        _TDB.AddParams(pInvoiceTypeTbl.Enabled, DbType.Boolean, "@Enabled");
        _TDB.AddParams(pInvoiceTypeTbl.Notes, DbType.String, "@Notes");
        #endregion
        // Now we have the parameters excute the SQL
        _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
        _TDB.Close();
      }
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string Update(InvoiceTypeTbl pInvoiceTypeTbl)
    { return Update(pInvoiceTypeTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string Update(InvoiceTypeTbl pInvoiceTypeTbl, int pOrignal_InvoiceTypeID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region UpdateParameters
      if (pOrignal_InvoiceTypeID > 0)
        _TDB.AddWhereParams(pOrignal_InvoiceTypeID, DbType.Int32);  // check this line it assumes id field is int32
      else
        _TDB.AddWhereParams(pInvoiceTypeTbl.InvoiceTypeID, DbType.Boolean, "@InvoiceTypeID");

      _TDB.AddParams(pInvoiceTypeTbl.InvoiceTypeDesc, DbType.String, "@InvoiceTypeDesc" );
      _TDB.AddParams(pInvoiceTypeTbl.Enabled, DbType.Boolean, "@Enabled" );
      _TDB.AddParams(pInvoiceTypeTbl.Notes, DbType.String, "@Notes" );
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string Delete(InvoiceTypeTbl pInvoiceType) { return Delete(pInvoiceType.InvoiceTypeID);  }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string Delete(int pInvoiceTypeID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pInvoiceTypeID, DbType.Int32, "@InvoiceTypeID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }

  }
}
