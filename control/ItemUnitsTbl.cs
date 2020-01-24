/// --- auto generated class for table: ItemUnitsTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class ItemUnitsTbl
  {
    // internal variable declarations
    private int _ItemUnitID;
    private string _UnitOfMeasure;
    private string _UnitDescription;
    // class definition
    public ItemUnitsTbl()
    {
      _ItemUnitID = 0;
      _UnitOfMeasure = string.Empty;
      _UnitDescription = string.Empty;
    }
    // get and sets of public
    public int ItemUnitID { get { return _ItemUnitID;}  set { _ItemUnitID = value;} }
    public string UnitOfMeasure { get { return _UnitOfMeasure;}  set { _UnitOfMeasure = value;} }
    public string UnitDescription { get { return _UnitDescription;}  set { _UnitDescription = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT ItemUnitID, UnitOfMeasure, UnitDescription FROM ItemUnitsTbl";
  #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<ItemUnitsTbl> GetAll(string SortBy)
    {
      List<ItemUnitsTbl> _DataItems = new List<ItemUnitsTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          ItemUnitsTbl _DataItem = new ItemUnitsTbl();

          _DataItem.ItemUnitID = (_DataReader["ItemUnitID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemUnitID"]);
          _DataItem.UnitOfMeasure = (_DataReader["UnitOfMeasure"] == DBNull.Value) ? string.Empty : _DataReader["UnitOfMeasure"].ToString();
          _DataItem.UnitDescription = (_DataReader["UnitDescription"] == DBNull.Value) ? string.Empty : _DataReader["UnitDescription"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
  }
}
