/// --- auto generated class for table: PriceLevelsTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Data;                  // for IDataReader and DbType
using QOnT.classes;        // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class PriceLevelsTbl
  {
    public const int CONST_DEAFULT_PRICELEVELID = 1;  //standard

    #region InternalVariableDeclarations
    private int _PriceLevelID;
    private string _PriceLevelDesc;
    private double _PricingFactor;
    private bool _Enabled;
    private string _Notes;
    #endregion

    // class definition
    public PriceLevelsTbl()
    {
      _PriceLevelID = 0;
      _PriceLevelDesc = string.Empty;
      _PricingFactor = 0.0;
      _Enabled = false;
      _Notes = string.Empty;
    }
    #region PublicVariableDeclarations
    public int PriceLevelID { get { return _PriceLevelID;}  set { _PriceLevelID = value;} }
    public string PriceLevelDesc { get { return _PriceLevelDesc;}  set { _PriceLevelDesc = value;} }
    public double PricingFactor { get { return _PricingFactor;}  set { _PricingFactor = value;} }
    public bool Enabled { get { return _Enabled;}  set { _Enabled = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }
    #endregion

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT PriceLevelID, PriceLevelDesc, PricingFactor, Enabled, Notes FROM PriceLevelsTbl";
    const string CONST_SQL_SELECTBYDESC = "SELECT PriceLevelID FROM PriceLevelsTbl WHERE (PriceLevelDesc = ?)";
    const string CONST_SQL_INSERT = "INSERT INTO PriceLevelsTbl (PriceLevelDesc, PricingFactor, Enabled, Notes)" + 
                          " VALUES ( ?, ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE PriceLevelsTbl SET PriceLevelDesc = ?, PricingFactor = ?, Enabled = ?, Notes = ?" + 
                           " WHERE (PriceLevelID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM PriceLevelsTbl WHERE (PriceLevelID = ?)";
    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<PriceLevelsTbl> GetAll(string SortBy)
    {
      List<PriceLevelsTbl> _DataItems = new List<PriceLevelsTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          PriceLevelsTbl _DataItem = new PriceLevelsTbl();

          #region StoreThisDataItem
          _DataItem.PriceLevelID = (_DataReader["PriceLevelID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PriceLevelID"]);
          _DataItem.PriceLevelDesc = (_DataReader["PriceLevelDesc"] == DBNull.Value) ? string.Empty : _DataReader["PriceLevelDesc"].ToString();
          _DataItem.PricingFactor = (_DataReader["PricingFactor"] == DBNull.Value) ? 0.0 : Math.Round(Convert.ToDouble(_DataReader["PricingFactor"]), 3);
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
    public int GetPriceLevelIDByDesc(string pPriceLevelDesc)
    {
      int _PriceLevelID = 0;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pPriceLevelDesc, DbType.String, "@PriceLevelDesc");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTBYDESC);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _PriceLevelID = (_DataReader["PriceLevelID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PriceLevelID"]);
        } 
        _DataReader.Close();
      }
      _TDB.Close();
      return _PriceLevelID;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string Insert(PriceLevelsTbl pPriceLevelsTbl)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region InsertParameters
      _TDB.AddParams(pPriceLevelsTbl.PriceLevelDesc, DbType.String, "@PriceLevelDesc");
      _TDB.AddParams(Math.Round(pPriceLevelsTbl.PricingFactor,3), DbType.Single, "@PricingFactor");
      _TDB.AddParams(pPriceLevelsTbl.Enabled, DbType.Int32, "@Enabled");
      _TDB.AddParams(pPriceLevelsTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string Update(PriceLevelsTbl pPriceLevelsTbl)
    { return Update(pPriceLevelsTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string Update(PriceLevelsTbl pPriceLevelsTbl, int pOrignal_PriceLevelID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region UpdateParameters
      if (pOrignal_PriceLevelID > 0)
        _TDB.AddWhereParams(pOrignal_PriceLevelID, DbType.Int32);  // check this line it assumes id field is int32
      else
        _TDB.AddWhereParams(pPriceLevelsTbl.PriceLevelID, DbType.Boolean, "@PriceLevelID");

      _TDB.AddParams(pPriceLevelsTbl.PriceLevelDesc, DbType.String, "@PriceLevelDesc" );
      _TDB.AddParams(Math.Round(pPriceLevelsTbl.PricingFactor,3), DbType.Double, "@PricingFactor");
      _TDB.AddParams(pPriceLevelsTbl.Enabled, DbType.Int32, "@Enabled" );
      _TDB.AddParams(pPriceLevelsTbl.Notes, DbType.String, "@Notes" );
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string Delete(PriceLevelsTbl pPriceLevelsTbl)
    { return Delete(pPriceLevelsTbl.PriceLevelID); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string Delete(int pPriceLevelID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pPriceLevelID, DbType.Int32, "@PriceLevelID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }

  }
}
