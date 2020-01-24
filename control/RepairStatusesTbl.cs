/// --- auto generated class for table: RepairStatusesTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class RepairStatusesTbl
  {
    // internal variable declarations
    private int _RepairStatusID;
    private string _RepairStatusDesc;
    private bool _EmailClient;
    private int _SortOrder;
    private string _Notes;
    // class definition
    public RepairStatusesTbl()
    {
      _RepairStatusID = 0;
      _RepairStatusDesc = string.Empty;
      _EmailClient = false;
      _SortOrder = 0;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int RepairStatusID { get { return _RepairStatusID;}  set { _RepairStatusID = value;} }
    public string RepairStatusDesc { get { return _RepairStatusDesc;}  set { _RepairStatusDesc = value;} }
    public bool EmailClient { get { return _EmailClient;}  set { _EmailClient = value;} }
    public int SortOrder { get { return _SortOrder;}  set { _SortOrder = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT RepairStatusID, RepairStatusDesc, EmailClient, SortOrder, Notes FROM RepairStatusesTbl";
    const string CONST_SQL_SELECTSTATUSDESC = "SELECT RepairStatusDesc FROM RepairStatusesTbl WHERE (RepairStatusID = ?)";
  #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public List<RepairStatusesTbl> GetAll(string SortBy)
    {
      List<RepairStatusesTbl> _DataItems = new List<RepairStatusesTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      _sqlCmd += " ORDER BY " + (String.IsNullOrEmpty(SortBy) ? "SortOrder" : SortBy);
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          RepairStatusesTbl _DataItem = new RepairStatusesTbl();

          _DataItem.RepairStatusID = (_DataReader["RepairStatusID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairStatusID"]);
          _DataItem.RepairStatusDesc = (_DataReader["RepairStatusDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairStatusDesc"].ToString();
          _DataItem.EmailClient = (_DataReader["EmailClient"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["EmailClient"]);
          _DataItem.SortOrder = (_DataReader["SortOrder"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SortOrder"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public string GetRepairStatusDesc(int RepairStatusID)
    {
      string _RepairStatusDesc = String.Empty;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(RepairStatusID, System.Data.DbType.Int32);

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTSTATUSDESC);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _RepairStatusDesc = (_DataReader["RepairStatusDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairStatusDesc"].ToString();

        _DataReader.Close();
      }
      _TDB.Close();

      return _RepairStatusDesc;
    }
  }
}
