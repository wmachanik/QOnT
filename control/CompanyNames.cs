using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QOnT.classes;
using System.Data;

namespace QOnT.control
{
  public class CompanyNames
  {
    private long _CustomerID;
    private string _CompanyName;
    private bool _enabled;

    public CompanyNames()
    {
      _CustomerID = 0;
      _CompanyName = string.Empty;
      _enabled = false;
    }

    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public string CompanyName { get { return _CompanyName; } set { _CompanyName = value; } }
    public bool enabled { get { return _enabled; } set { _enabled = value; } }

    const string CONST_SQL_SELECT = "SELECT [CustomerID], [CompanyName], [enabled] FROM [CustomersTbl] ORDER BY [enabled], [CompanyName]";
    const string CONST_SQL_SELECTDEMOS = "SELECT [CustomerID], [CompanyName], [enabled] FROM [CustomersTbl] WHERE [CompanyName] LIKE 'DEMO%' ORDER BY [enabled], [CompanyName]";
    const string CONST_SQL_CUSTOMERNAME_SELECT = "SELECT CompanyName FROM CustomersTbl WHERE (CustomerID = ?)";

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<CompanyNames> GetAll()
    {
      List<CompanyNames> _CompanyNames = new List<CompanyNames>();

      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECT);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CompanyNames _Company = new CompanyNames();

          _Company.CustomerID = Convert.ToInt64(_DataReader["CustomerID"]);
          _Company.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? "" : _DataReader["CompanyName"].ToString();
          _Company.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);

          _CompanyNames.Add(_Company);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _CompanyNames;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<CompanyNames> GetAllDemo()
    {
      List<CompanyNames> _CompanyNames = new List<CompanyNames>();

      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTDEMOS);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CompanyNames _Company = new CompanyNames();

          _Company.CustomerID = Convert.ToInt64(_DataReader["CustomerID"]);
          _Company.CompanyName = (_DataReader["CompanyName"] == DBNull.Value) ? "" : _DataReader["CompanyName"].ToString();
          _Company.enabled = (_DataReader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["enabled"]);

          _CompanyNames.Add(_Company);
        }

        _DataReader.Close();
      }
      _TDB.Close();

      return _CompanyNames;
    }
    public string GetCompanyNameByCompanyID(long pCustomerID)
    {
      string _CustomerName = String.Empty;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pCustomerID, DbType.Int64);

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_CUSTOMERNAME_SELECT);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _CustomerName = (_DataReader["CompanyName"] == DBNull.Value) ? String.Empty : _DataReader["CompanyName"].ToString();

        _DataReader.Dispose();
      }
      _TDB.Close();
      return _CustomerName;
    }


  }
}