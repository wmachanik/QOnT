using System;
using System.Collections.Generic;
using System.Data;
using QOnT.classes;

/// --- auto generated class for table: CityTbl
namespace QOnT.control
{
  public class CityTblData
  {
    // internal variable declarations
    private int _ID;
    private string _City;
    // not used //    private int _RoastingDay; //    private int _DeliveryDelay;
    // class definition
    public CityTblData()
    {
      _ID = 0;
      _City = string.Empty;
      // not used //      _RoastingDay = 0;   _DeliveryDelay = 0;
    }
    // get and sets of public
    public int ID { get { return _ID;}  set { _ID = value;} }
    public string City { get { return _City;}  set { _City = value;} }

 }


  public class CityTblDAL
  {
    const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_SQL_SUMMARYDATA = "SELECT ID, City FROM CityTbl";
    const string CONST_SQL_SELECTCITYBYID = "SELECT City FROM CityTbl WHERE ID = ?";
    const string CONST_SQL_SELECTIDBYCITYBY = "SELECT ID FROM CityTbl WHERE City Like '?'";
    const string CONST_SQL_INSERT = "INSERT INTO CityTbl (ID, City) VALUES (?, ?)";

    public const int CONST_DEFAULT_CITYID = 1;  // cape town

    public static List<CityTblData> GetAllCityTblData(string SortBy)
    {
      List<CityTblData> _ListCitys = new List<CityTblData>();
      TrackerDb _TDB = new TrackerDb();
      
      string _sqlCmd = CONST_SQL_SUMMARYDATA;
      // Add order by string
      _sqlCmd += " ORDER BY " + (!String.IsNullOrEmpty(SortBy) ? SortBy : " City");
      // run the query we have built
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CityTblData _CityTblItem = new CityTblData();

          _CityTblItem.ID = Convert.ToInt32(_DataReader["ID"]);
          _CityTblItem.City = (_DataReader["City"] == DBNull.Value) ? "" : _DataReader["City"].ToString();

          _ListCitys.Add(_CityTblItem);
        }
      }
      _DataReader.Close();
      _TDB.Close();

      return _ListCitys;
    }

    public string GetCityName(int pCityID)
    {
      string _ItemDesc = String.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pCityID, DbType.Int32, "@ID");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTCITYBYID);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _ItemDesc = (_DataReader["City"] == DBNull.Value) ? "" : _DataReader["City"].ToString();
        _DataReader.Close();
      }
      _TDB.Close();
      return _ItemDesc;
    }
    public int GetCityID(string pCityName)
    {
      int _ID = 0;
      TrackerDb _TDB = new TrackerDb();
      if (!pCityName.Contains("%"))
        pCityName = "%" + pCityName + "%";
      _TDB.AddWhereParams(pCityName, DbType.String, "@City");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTIDBYCITYBY);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _ID = (_DataReader["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ID"].ToString());
        _DataReader.Close();
      }
      _TDB.Close();
      return _ID;
    }

/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[TrackerDotNet.classes.TrackerDb.CONST_CONSTRING].ConnectionString;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _ItemDesc = String.Empty;
        OleDbCommand _cmd = new OleDbCommand(CONST_SQL_SELECTCITYBYID, _conn);                    // run the query we have built
        _cmd.Parameters.Add(new OleDbParameter { Value = pCityID });
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        if (_DataReader.Read())
        {
          _ItemDesc = (_DataReader["City"] == DBNull.Value) ? "" : _DataReader["City"].ToString();
        }

*/

  }

}
