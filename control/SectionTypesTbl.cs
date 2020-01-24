/// --- auto generated class for table: SectionTypesTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class SectionTypesTbl
  {
    #region PublicConstants
    public const int CONST_ORDERS_SECTION_INT = 1;
    public const int CONST_CUSTOMER_SECTION_INT = 2;
    public const int CONST_REMINDERS_SECTION_INT = 3;
    public const int CONST_REPAIRS_SECTION_INT = 4;
    public const int CONST_RECURRING_SECTION_INT = 5;
    // future CONST_FUTURE1_SECTION_INT  - 6/7/8 / 1 2 3
    public const int CONST_SYSTEM_SECTION_INT = 9;

    #endregion
    // internal variable declarations
    private int _SectionID;
    private string _SectionType;
    private string _Notes;
    // class definition
    public SectionTypesTbl()
    {
      _SectionID = 0;
      _SectionType = string.Empty;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int SectionID { get { return _SectionID;}  set { _SectionID = value;} }
    public string SectionType { get { return _SectionType;}  set { _SectionType = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT SectionID, SectionType, Notes FROM SectionTypesTbl";
    const string CONST_SQL_SELECTSECTIONBYID = "SELECT SectionType FROM SectionTypesTbl WHERE SectionID = ? ";
    const string CONST_SQL_INSERT = "INSERT INTO SectionTypesTbl (SectionID, SectionType, Notes) VALUES (?,?,?)";
    const string CONST_SQL_UPDATE = "UPDATE SectionTypesTbl SET SectionType = ? , Notes = ? WHERE (SectionID = ?)";
  #endregion

    public bool InsertDefaultSections()
    {
      bool _Inserted = false;
      List<SectionTypesTbl> _DataItems = new List<SectionTypesTbl>();

      // create a list of data to insert
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_ORDERS_SECTION_INT, SectionType = "Order", Notes = "Order section" });
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_CUSTOMER_SECTION_INT, SectionType = "Customer", Notes = "Customer section" });
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_REMINDERS_SECTION_INT, SectionType = "Reminders", Notes = "Reminders that are sent section" });
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_REPAIRS_SECTION_INT, SectionType = "Repairs", Notes = "Repairs section" });
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_RECURRING_SECTION_INT, SectionType = "Recurring", Notes = "Recurring Order section" });
      _DataItems.Add(new SectionTypesTbl { SectionID = CONST_SYSTEM_SECTION_INT, SectionType = "System", Notes = "System section" });

      foreach (SectionTypesTbl _DataItem in _DataItems)
        _Inserted = (string.IsNullOrWhiteSpace(InsertSectionType(_DataItem)) && _Inserted);

      _DataItems.Clear();

      return _Inserted;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<SectionTypesTbl> GetAll(string SortBy)
    {
      List<SectionTypesTbl> _DataItems = new List<SectionTypesTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);

      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          SectionTypesTbl _DataItem = new SectionTypesTbl();

          _DataItem.SectionID = (_DataReader["SectionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SectionID"]);
          _DataItem.SectionType = (_DataReader["SectionType"] == DBNull.Value) ? string.Empty : _DataReader["SectionType"].ToString();
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public string GetSectionTypeByID(int pSectionID)
    {
      string _SectionType = string.Empty;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pSectionID, DbType.Int32);

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTSECTIONBYID);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _SectionType = (_DataReader["SectionType"] == DBNull.Value) ? string.Empty : _DataReader["SectionType"].ToString();
        _DataReader.Close();
      }
      _TDB.Close();

      return _SectionType;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string InsertSectionType(SectionTypesTbl pSectionType)
    {
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pSectionType.SectionID, DbType.Int32);
      _TDB.AddParams(pSectionType.SectionType);
      _TDB.AddParams(pSectionType.Notes);

      string _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();

      return _Result;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public static void UpdateSection(SectionTypesTbl pSectionType) { UpdateSection(pSectionType, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public static void UpdateSection(SectionTypesTbl pSectionType, int pOrignal_SectionID)
    {
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pSectionType.SectionType);
      _TDB.AddParams(pSectionType.Notes);

      if (pOrignal_SectionID > 0)
        _TDB.AddWhereParams(pOrignal_SectionID, DbType.Int32);
      else
        _TDB.AddWhereParams(pSectionType.SectionID, DbType.Int32);

      string _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();

      // return _Result;
    }


  }
}
