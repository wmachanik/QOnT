/// --- auto generated class for table: RepairFaultsTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class RepairFaultsTbl
  {
    // internal variable declarations
    private int _RepairFaultID;
    private string _RepairFaultDesc;
    private int _SortOrder;
    private string _Notes;
    // class definition
    public RepairFaultsTbl()
    {
      _RepairFaultID = 0;
      _RepairFaultDesc = string.Empty;
      _SortOrder = 0;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int RepairFaultID { get { return _RepairFaultID;}  set { _RepairFaultID = value;} }
    public string RepairFaultDesc { get { return _RepairFaultDesc;}  set { _RepairFaultDesc = value;} }
    public int SortOrder { get { return _SortOrder;}  set { _SortOrder = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT RepairFaultID, RepairFaultDesc, SortOrder, Notes FROM RepairFaultsTbl";
    const string CONST_SQL_SELECTFAULTDESC = "SELECT RepairFaultDesc FROM RepairFaultsTbl WHERE (RepairFaultID = ?)";
  #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public List<RepairFaultsTbl> GetAll(string SortBy)
    {
      List<RepairFaultsTbl> _DataItems = new List<RepairFaultsTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      
      _sqlCmd += " ORDER BY " + (String.IsNullOrEmpty(SortBy) ? "SortOrder" : SortBy);
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          RepairFaultsTbl _DataItem = new RepairFaultsTbl();

          _DataItem.RepairFaultID = (_DataReader["RepairFaultID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairFaultID"]);
          _DataItem.RepairFaultDesc = (_DataReader["RepairFaultDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairFaultDesc"].ToString();
          _DataItem.SortOrder = (_DataReader["SortOrder"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SortOrder"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public string GetRepairFaultDesc(int RepairFaultID)
    {
      string _RepairFaultDesc = String.Empty;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(RepairFaultID, System.Data.DbType.Int32);

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTFAULTDESC);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _RepairFaultDesc = (_DataReader["RepairFaultDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairFaultDesc"].ToString();

        _DataReader.Close();
      }
      _TDB.Close();

      return _RepairFaultDesc;
    }


  }
}
