using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QOnT.classes;
using System.Data;

namespace QOnT.control
{
  public class ContactType
  {
    long _CustomerID;
    string _CompanyName;
    int _CustomerTypeID;
    bool _IsEnabled;
    bool _PredictionDisabled;

    public ContactType()
    {
      _CustomerID = 0;
      _CompanyName = string.Empty;
      _CustomerTypeID = 0;
      _IsEnabled = false;
      _PredictionDisabled = false;
    }

    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; }  }
    public string CompanyName { get { return _CompanyName; } set { _CompanyName = value; }  }
    public int CustomerTypeID { get { return _CustomerTypeID; } set { _CustomerTypeID = value; } }
    public bool IsEnabled { get { return _IsEnabled; } set { _IsEnabled = value; } }
    public bool PredictionDisabled { get { return _PredictionDisabled; } set { _PredictionDisabled = value; } }

    const string CONST_SQL_SELECT = "SELECT CustomerID, CompanyName, CustomerTypeID, enabled, PredictionDisabled FROM CustomersTbl";
    const string CONST_SQL_UPDATE = "UPDATE CustomersTbl SET CustomerTypeID = ?, PredictionDisabled = ? WHERE CustomerID = ?";
    const string CONST_SQL_UPDATETYPEONLY = "UPDATE CustomersTbl SET CustomerTypeID = ? WHERE CustomerID = ? AND CustomerTypeID = ?";

    public List<ContactType> GetAllContacts(string SortBy)
    {
      List<ContactType> _DataItems = new List<ContactType>();

      string _sql = CONST_SQL_SELECT;

      TrackerDb _TDB = new TrackerDb();
      if (!String.IsNullOrEmpty(SortBy))
        _sql += " ORDER BY " + SortBy;

      IDataReader _Reader = _TDB.ExecuteSQLGetDataReader(_sql);
      if (_Reader != null)
      {
        while (_Reader.Read())
        {
          ContactType _DataItem = new ContactType();

          _DataItem.CustomerID = Convert.ToInt64(_Reader["CustomerID"]);
          _DataItem.CompanyName = (_Reader["CompanyName"] == DBNull.Value) ? "" : _Reader["CompanyName"].ToString();
          _DataItem.CustomerTypeID = (_Reader["CustomerTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_Reader["CustomerTypeID"]);
          _DataItem.IsEnabled = (_Reader["enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_Reader["enabled"]);
          _DataItem.PredictionDisabled = (_Reader["PredictionDisabled"] == DBNull.Value) ? false : Convert.ToBoolean(_Reader["PredictionDisabled"]);

          _DataItems.Add(_DataItem);
        }
        _Reader.Close();
      }
      _TDB.Close();

      return _DataItems;
    }
    public string UpdateContact(ContactType pContact)
    {
      string _Err =  String.Empty;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pContact.CustomerTypeID,DbType.Int32);
      _TDB.AddParams(pContact.PredictionDisabled,DbType.Boolean);
      _TDB.AddWhereParams(pContact.CustomerID,DbType.Int64);
      // CONST_SQL_UPDATE

      _Err = _TDB.ExecuteNonQuerySQLWithParams(CONST_SQL_UPDATE, _TDB.Params, _TDB.WhereParams);
      _TDB.Close();
      return _Err;
    }
    /// <summary>
    /// Update only Contact Type, usese pContact.CustType and pContact.CustID
    /// </summary>
    /// <param name="pContact">cus</param>
    /// <returns></returns>
    public string UpdateContactTypeIfInfoOnly(long pCustomerID, int pCustomerTypeID)
    {
      string _Err = String.Empty;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pCustomerTypeID, DbType.Int32);
      _TDB.AddWhereParams(pCustomerID, DbType.Int64);
      _TDB.AddWhereParams(CustomerTypeTbl.CONST_INFO_ONLY , DbType.Int32);
      // CONST_SQL_UPDATE

      _Err = _TDB.ExecuteNonQuerySQLWithParams(CONST_SQL_UPDATETYPEONLY, _TDB.Params, _TDB.WhereParams);
      _TDB.Close();
      return _Err;
    }
  }
}