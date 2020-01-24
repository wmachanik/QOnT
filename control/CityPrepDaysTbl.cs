/// --- auto generated class for table: CityPrepDaysTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class CityPrepDaysTbl
  {
    // internal variable declarations
    private int _CityPrepDaysID;
    private int _CityID;
    private byte _PrepDayOfWeekID;
    private int _DeliveryDelayDays;
    private int _DeliveryOrder;
    // class definition
    public CityPrepDaysTbl()
    {
      _CityPrepDaysID = 0;
      _CityID = 0;
      _PrepDayOfWeekID = 0;
      _DeliveryDelayDays = 0;
      _DeliveryOrder = 0;
    }
    // get and sets of public
    public int CityPrepDaysID { get { return _CityPrepDaysID;}  set { _CityPrepDaysID = value;} }
    public int CityID { get { return _CityID;}  set { _CityID = value;} }
    public byte PrepDayOfWeekID { get { return _PrepDayOfWeekID;}  set { _PrepDayOfWeekID = value;} }
    public int DeliveryDelayDays { get { return _DeliveryDelayDays;}  set { _DeliveryDelayDays = value;} }
    public int DeliveryOrder { get { return _DeliveryOrder;}  set { _DeliveryOrder = value;} }

  #region ConstantDeclarations
    // const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_SQL_SELECT = "SELECT CityPrepDaysID, CityID, PrepDayOfWeekID, DeliveryDelayDays, DeliveryOrder FROM CityPrepDaysTbl";
    const string CONST_SQL_SELECTBYCITYID = "SELECT CityPrepDaysID, PrepDayOfWeekID, DeliveryDelayDays, DeliveryOrder FROM CityPrepDaysTbl WHERE CityID = ? ORDER BY PrepDayOfWeekID";
    const string CONST_SQL_INSERT = "INSERT INTO CityPrepDaysTbl (CityID, PrepDayOfWeekID, DeliveryDelayDays, DeliveryOrder)" +
                                                        " VALUES (  ?   ,      ?         ,        ?         ,      ?)";
    const string CONST_SQL_UPDATEBYID = "UPDATE CityPrepDaysTbl SET CityID = ? , PrepDayOfWeekID = ?, DeliveryDelayDays = ?," +
                                                  " DeliveryOrder = ? WHERE (CityPrepDaysID = ?)";

    const string CONST_SQL_DELETEBYID = "DELETE FROM CityPrepDaysTbl WHERE (CityPrepDaysID = ?)";
  #endregion

    public List<CityPrepDaysTbl> GetAll(string SortBy)
    {
      TrackerDb _TrackerDb = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) 
        _sqlCmd += " ORDER BY " + SortBy;

      List<CityPrepDaysTbl> _DataItems = new List<CityPrepDaysTbl>();
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);

      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CityPrepDaysTbl _DataItem = new CityPrepDaysTbl();

          _DataItem.CityPrepDaysID = (_DataReader["CityPrepDaysID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityPrepDaysID"]);
          _DataItem.CityID = (_DataReader["CityID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityID"]);
          _DataItem.PrepDayOfWeekID = (_DataReader["PrepDayOfWeekID"] == DBNull.Value) ? byte.MinValue : Convert.ToByte(_DataReader["PrepDayOfWeekID"]);
          _DataItem.DeliveryDelayDays = (_DataReader["DeliveryDelayDays"] == DBNull.Value) ? 0 : Convert.ToInt16(_DataReader["DeliveryDelayDays"]);
          _DataItem.DeliveryOrder = (_DataReader["DeliveryOrder"] == DBNull.Value) ? 0 : Convert.ToInt16(_DataReader["DeliveryOrder"]);

          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }

    public List<CityPrepDaysTbl> GetAllByCityId(int pCityID)
    {
      string _sqlCmd = CONST_SQL_SELECTBYCITYID;
      //if (!String.IsNullOrEmpty(SortBy))
      //  _sqlCmd += " ORDER BY " + SortBy;

      TrackerDb _TrackerDb = new TrackerDb();
      _TrackerDb.AddWhereParams(pCityID, DbType.Int32);

      List<CityPrepDaysTbl> _DataItems = new List<CityPrepDaysTbl>();
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(_sqlCmd);

      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CityPrepDaysTbl _DataItem = new CityPrepDaysTbl();

          _DataItem.CityID = pCityID;
          _DataItem.CityPrepDaysID = (_DataReader["CityPrepDaysID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CityPrepDaysID"]);
          _DataItem.PrepDayOfWeekID = (_DataReader["PrepDayOfWeekID"] == DBNull.Value) ? byte.MinValue : Convert.ToByte(_DataReader["PrepDayOfWeekID"]);
          _DataItem.DeliveryDelayDays = (_DataReader["DeliveryDelayDays"] == DBNull.Value) ? 0 : Convert.ToInt16(_DataReader["DeliveryDelayDays"]);
          _DataItem.DeliveryOrder = (_DataReader["DeliveryOrder"] == DBNull.Value) ? 0 : Convert.ToInt16(_DataReader["DeliveryOrder"]);

          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    public string InsertCityPrepDay(CityPrepDaysTbl objCityPrepDaysTbl)
    {
      string _Result = String.Empty;

      TrackerDb _TDB = new TrackerDb();
      /// CityID, PrepDayOfWeekID, DeliveryDelayDays, DeliveryOrder
      _TDB.AddParams(objCityPrepDaysTbl.CityID, System.Data.DbType.Int32);
      _TDB.AddParams(objCityPrepDaysTbl.PrepDayOfWeekID, System.Data.DbType.Byte);
      _TDB.AddParams(objCityPrepDaysTbl.DeliveryDelayDays, System.Data.DbType.Int32);
      _TDB.AddParams(objCityPrepDaysTbl.DeliveryOrder, System.Data.DbType.Int32);

      _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);

      _TDB.Close();

      return _Result;
    }
    public string UpdateCityPrepDay(CityPrepDaysTbl objCityPrepDaysTbl)
    { return UpdateCityPrepDay(objCityPrepDaysTbl, objCityPrepDaysTbl.CityPrepDaysID); }
    public string UpdateCityPrepDay(CityPrepDaysTbl objCityPrepDaysTbl, int origCityPrepDaysID)
    {
      string _Result = String.Empty;

      if (origCityPrepDaysID > 0)
      {
        TrackerDb _TDB = new TrackerDb();
        /// CityID, PrepDayOfWeekID, DeliveryDelayDays, DeliveryOrder
        _TDB.AddParams(objCityPrepDaysTbl.CityID, System.Data.DbType.Int32);
        _TDB.AddParams(objCityPrepDaysTbl.PrepDayOfWeekID, System.Data.DbType.Byte);
        _TDB.AddParams(objCityPrepDaysTbl.DeliveryDelayDays, System.Data.DbType.Int32);
        _TDB.AddParams(objCityPrepDaysTbl.DeliveryOrder, System.Data.DbType.Int32);
        /// Where CityPrepDayID
        _TDB.AddWhereParams(origCityPrepDaysID, DbType.Int32);

        _Result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATEBYID);

        _TDB.Close();
      }
      return _Result;
    }
    public string DeleteByCityPrepDayID(int pCityPrepDayID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pCityPrepDayID, DbType.Int32, "@CityPrepDayID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETEBYID);
      _TDB.Close();
      return _result;
    }

  }
}
