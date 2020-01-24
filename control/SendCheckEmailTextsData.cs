/// --- auto generated class for table: SendCheckEmailTextsTbl
using System;   // for DateTime variables
using System.Configuration;
// for data stuff
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class SendCheckEmailTextsData
  {
    // internal variable declarations
    private int _SCEMTID;
    private string _Header;
    private string _Body;
    private string _Footer;
    private DateTime _DateLastChange;
    private string _Notes;
    // class definition
    public SendCheckEmailTextsData()
    {
      _SCEMTID = 0;
      _Header = string.Empty;
      _Body = string.Empty;
      _Footer = string.Empty;
      _DateLastChange = System.DateTime.Now.Date;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int SCEMTID { get { return _SCEMTID;}  set { _SCEMTID = value;} }
    public string Header { get { return _Header;}  set { _Header = value;} }
    public string Body { get { return _Body;}  set { _Body = value;} }
    public string Footer { get { return _Footer;}  set { _Footer = value;} }
    public DateTime DateLastChange { get { return _DateLastChange;}  set { _DateLastChange = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }

  #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT SCEMTID, Header, Body, Footer, DateLastChange, Notes FROM SendCheckEmailTextsTbl";
    const string CONST_SQL_UPDATE = "UPDATE SendCheckEmailTextsTbl SET Header = ?, Body = ?, Footer = ?, DateLastChange = ?, Notes = ? WHERE SCEMTID = ?";
  #endregion

    public SendCheckEmailTextsData GetTexts()
    {
      SendCheckEmailTextsData _DataItem = new SendCheckEmailTextsData();
      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECT);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _DataItem.SCEMTID = (_DataReader["SCEMTID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SCEMTID"]);
          _DataItem.Header = (_DataReader["Header"] == DBNull.Value) ? string.Empty : _DataReader["Header"].ToString();
          _DataItem.Body = (_DataReader["Body"] == DBNull.Value) ? string.Empty : _DataReader["Body"].ToString();
          _DataItem.Footer = (_DataReader["Footer"] == DBNull.Value) ? string.Empty : _DataReader["Footer"].ToString();
          _DataItem.DateLastChange = (_DataReader["DateLastChange"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateLastChange"]).Date;
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }

/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_SELECT;
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        if (_DataReader.Read())
        {
          _DataItem.SCEMTID = (_DataReader["SCEMTID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SCEMTID"]);
          _DataItem.Header = (_DataReader["Header"] == DBNull.Value) ? string.Empty : _DataReader["Header"].ToString();
          _DataItem.Body = (_DataReader["Body"] == DBNull.Value) ? string.Empty : _DataReader["Body"].ToString();
          _DataItem.Footer = (_DataReader["Footer"] == DBNull.Value) ? string.Empty : _DataReader["Footer"].ToString();
          _DataItem.DateLastChange = (_DataReader["DateLastChange"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["DateLastChange"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
        }
      }
      return _DataItem;
    }
*/
    public string UpdateTexts(SendCheckEmailTextsData pEmailTextsData, int pOriginalID )
    {
      string _errString = "";
      TrackerDb _TDB = new TrackerDb();
      #region Parameters
      // Add data sent
      _TDB.AddParams(pEmailTextsData.Header, DbType.String, "@Header");
      _TDB.AddParams(pEmailTextsData.Body, DbType.String, "@Body");
      _TDB.AddParams(pEmailTextsData.Footer, DbType.String, "@Footer");
      _TDB.AddParams(DateTime.Now.Date, DbType.Date, "@DateLastChange");
      _TDB.AddParams(pEmailTextsData.Notes, DbType.String, "@Notes");
      //                                     " WHERE SCEMTID = ?)";
      _TDB.AddWhereParams(pOriginalID, DbType.Int32, "@SCEMTID");
      #endregion

      _errString = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();
      return _errString;
    }

/*
      string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;

      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SQL_UPDATE;

        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
        #region Parameters
        // Add data sent
        _cmd.Parameters.Add(new OleDbParameter { Value = pEmailTextsData.Header, DbType = System.Data.DbType.String });
        _cmd.Parameters.Add(new OleDbParameter { Value = pEmailTextsData.Body, DbType = System.Data.DbType.String });
        _cmd.Parameters.Add(new OleDbParameter { Value = pEmailTextsData.Footer, DbType = System.Data.DbType.String });
        _cmd.Parameters.Add(new OleDbParameter { Value = DateTime.Now, DbType = System.Data.DbType.Date });
        _cmd.Parameters.Add(new OleDbParameter { Value = pEmailTextsData.Notes, DbType = System.Data.DbType.String });
        //                                     " WHERE SCEMTID = ?)";
        _cmd.Parameters.Add(new OleDbParameter { Value = pOriginalID, DbType = System.Data.DbType.Int32 });
        ///// others here
        #endregion
        try
        {
          _conn.Open();
          if (_cmd.ExecuteNonQuery() > 0)
            errString = "";
          else
            errString += " - error ";
        }
        catch (OleDbException oleErr)
        {
          // Handle exception.
          errString += " ERROR: " + oleErr.Message;
        }
        finally
        {
          _conn.Close();
        }
        return errString;
      }
    }
*/
  }
}
