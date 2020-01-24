/// --- auto generated class for table: MachineConditionsTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class MachineConditionsTbl
  {
    // internal variable declarations
    private int _MachineConditionID;
    private string _ConditionDesc;
    private int _SortOrder;
    private string _Notes;
    // class definition
    public MachineConditionsTbl()
    {
      _MachineConditionID = 0;
      _ConditionDesc = string.Empty;
      _SortOrder = 0;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int MachineConditionID { get { return _MachineConditionID;}  set { _MachineConditionID = value;} }
    public string ConditionDesc { get { return _ConditionDesc;}  set { _ConditionDesc = value;} }
    public int SortOrder { get { return _SortOrder;}  set { _SortOrder = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT MachineConditionID, ConditionDesc, SortOrder, Notes FROM MachineConditionsTbl";
  #endregion

    [System.ComponentModel.DataObjectMethod (System.ComponentModel.DataObjectMethodType.Select, true)] 
    public List<MachineConditionsTbl> GetAll(string SortBy)
    {
      List<MachineConditionsTbl> _DataItems = new List<MachineConditionsTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      _sqlCmd += " ORDER BY " + (String.IsNullOrEmpty(SortBy) ? "SortOrder" : SortBy);
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          MachineConditionsTbl _DataItem = new MachineConditionsTbl();

          _DataItem.MachineConditionID = (_DataReader["MachineConditionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineConditionID"]);
          _DataItem.ConditionDesc = (_DataReader["ConditionDesc"] == DBNull.Value) ? string.Empty : _DataReader["ConditionDesc"].ToString();
          _DataItem.SortOrder = (_DataReader["SortOrder"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SortOrder"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
  }
}
