/// --- auto generated class for table: RepairsTbl
using System;   // for DateTime variables
using System.Collections.Generic;      // for data stuff
using System.Data;               // for IDataReader
using QOnT.classes;      // TrackerDot classes used for DB access

namespace QOnT.control
{

  public class RepairsTbl
  {

    public const int CONST_REPAIRSTATUS_LOGGED = 1; //	logged
    public const int CONST_REPAIRSTATUS_COLLECTED = 2; //	collected
    public const int CONST_REPAIRSTATUS_WORKSHOP = 3;	// workshop
    public const int CONST_REPAIRSTATUS_WAITING = 4;  //	waiting for part
    public const int CONST_REPAIRSTATUS_ESTIMATE = 5;  //	estimate
    public const int CONST_REPAIRSTATUS_READY = 6;   //	ready
    public const int CONST_REPAIRSTATUS_DONE = 7;  /// the status for done
    const string CONST_REPAIR_DONESTR = "7";  /// the status for done

    // internal variable declarations
    private int _RepairID;
    private long _CustomerID;
    private string _ContactName;
    private string _ContactEmail;
    private string _JobCardNumber;
    private DateTime _DateLogged;
    private DateTime _LastStatusChange;
    private int _MachineTypeID;
    private string _MachineSerialNumber;
    private int _SwopOutMachineID;
    private int _MachineConditionID;
    private bool _TakenFrother;
    private bool _TakenBeanLid;
    private bool _TakenWaterLid;
    private bool _BrokenFrother;
    private bool _BrokenBeanLid;
    private bool _BrokenWaterLid;
    private int _RepairFaultID;
    private string _RepairFaultDesc;
    private int _RepairStatusID;
    private long _RelatedOrderID;
    private string _Notes;
    // class definition
    public RepairsTbl()
    {
      _RepairID = 0;
      _CustomerID = 0;
      _ContactName = string.Empty;
      _ContactEmail = string.Empty;
      _JobCardNumber = string.Empty;
      _DateLogged = System.DateTime.MinValue;
      _LastStatusChange = System.DateTime.Now;
      _MachineTypeID = 0;
      _MachineSerialNumber = string.Empty;
      _SwopOutMachineID = 0;
      _MachineConditionID = 0;
      _TakenFrother = false;
      _TakenBeanLid = true;
      _TakenWaterLid = true;
      _BrokenFrother = false;
      _BrokenBeanLid = false;
      _BrokenWaterLid = false;
      _RepairFaultID = 0;
      _RepairFaultDesc = string.Empty;
      _RepairStatusID = 0;
      _RelatedOrderID = 0;
      _Notes = string.Empty;
    }
    // get and sets of public
    public int RepairID { get { return _RepairID; } set { _RepairID = value; } }
    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public string ContactName { get { return _ContactName; } set { _ContactName = value; } }
    public string ContactEmail { get { return _ContactEmail; } set { _ContactEmail = value; } }
    public string JobCardNumber { get { return _JobCardNumber; } set { _JobCardNumber = value; } }
    public DateTime DateLogged { get { return _DateLogged; } set { _DateLogged = value; } }
    public DateTime LastStatusChange { get { return _LastStatusChange; } set { _LastStatusChange = value; } }
    public int MachineTypeID { get { return _MachineTypeID; } set { _MachineTypeID = value; } }
    public string MachineSerialNumber { get { return _MachineSerialNumber; } set { _MachineSerialNumber = value; } }
    public int SwopOutMachineID { get { return _SwopOutMachineID; } set { _SwopOutMachineID = value; } }
    public int MachineConditionID { get { return _MachineConditionID; } set { _MachineConditionID = value; } }
    public bool TakenFrother { get { return _TakenFrother; } set { _TakenFrother = value; } }
    public bool TakenBeanLid { get { return _TakenBeanLid; } set { _TakenBeanLid = value; } }
    public bool TakenWaterLid { get { return _TakenWaterLid; } set { _TakenWaterLid = value; } }
    public bool BrokenFrother { get { return _BrokenFrother; } set { _BrokenFrother = value; } }
    public bool BrokenBeanLid { get { return _BrokenBeanLid; } set { _BrokenBeanLid = value; } }
    public bool BrokenWaterLid { get { return _BrokenWaterLid; } set { _BrokenWaterLid = value; } }
    public int RepairFaultID { get { return _RepairFaultID; } set { _RepairFaultID = value; } }
    public string RepairFaultDesc { get { return _RepairFaultDesc; } set { _RepairFaultDesc = value; } }
    public int RepairStatusID { get { return _RepairStatusID; } set { _RepairStatusID = value; } }
    public long RelatedOrderID { get { return _RelatedOrderID; } set { _RelatedOrderID = value; } }
    public string Notes { get { return _Notes; } set { _Notes = value; } }

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT RepairID, CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, " +
                                           "MachineTypeID, MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, " +
                                           "TakenBeanLid, TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, " +
                                           "RepairFaultDesc, RepairsTbl.RepairStatusID, RelatedOrderID, Notes FROM RepairsTbl";
    const string CONST_SQL_SELECTNOTDONE = "SELECT RepairID, CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, " +
                                           "MachineTypeID, MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, " +
                                           "TakenBeanLid, TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, " +
                                           "RepairFaultDesc, RepairsTbl.RepairStatusID, RelatedOrderID, RepairsTbl.Notes " +
                                       "FROM RepairsTbl WHERE (RepairsTbl.RepairStatusID <> 7)";
    const string CONST_SQL_SELECTONSTATUS = "SELECT RepairID, CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, " +
                                           "MachineTypeID, MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, " +
                                           "TakenBeanLid, TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, " +
                                           "RepairFaultDesc, RepairsTbl.RepairStatusID, RelatedOrderID, RepairsTbl.Notes " +
                                           "FROM RepairsTbl WHERE (RepairsTbl.RepairStatusID = ?)";

    const string CONST_SQL_SELECTBYREPAIRID = "SELECT CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, " +
                                           "MachineTypeID, MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, " +
                                           "TakenBeanLid, TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, " +
                                           "RepairFaultDesc, RepairsTbl.RepairStatusID, RelatedOrderID, Notes " +
                                           "FROM RepairsTbl WHERE (RepairID = ?)";

    const string CONST_SQL_SELECTLAST = "SELECT TOP 1 RepairID FROM RepairsTbl WHERE (CustomerID = ?) ORDER BY DateLogged DESC";

    const string CONST_SQL_INSERT = "INSERT INTO RepairsTbl " +
                                      "(CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, MachineTypeID, " +
                                           "MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, TakenBeanLid, " +
                                           "TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, RepairFaultDesc, " +
                                           "RepairStatusID, RelatedOrderID, Notes)" +
                                      "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    const string CONST_SQL_UPDATE = "UPDATE RepairsTbl " +
                                      "SET CustomerID = ?, ContactName = ?, ContactEmail = ?, JobCardNumber = ?, DateLogged = ?, " +
                                           "LastStatusChange = ?, MachineTypeID = ?, MachineSerialNumber = ?, SwopOutMachineID = ?,  " +
                                           "MachineConditionID = ?, TakenFrother = ?, TakenBeanLid = ?, TakenWaterLid = ?, BrokenFrother = ?, " +
                                           " BrokenBeanLid = ?, BrokenWaterLid = ?, RepairFaultID = ?, RepairFaultDesc = ?, RepairStatusID = ?, " +
                                           " RelatedOrderID = ?, Notes = ? " +
                                      "WHERE (RepairsTbl.RepairID = ?)";
    const string CONST_SQL_UPDATESTATUS = "UPDATE RepairsTbl SET RepairStatusID = ? WHERE (RepairsTbl.RepairID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM RepairsTbl WHERE (RepairsTbl.RepairID = ?)";

    const string CONST_SQL_SELECTITEMWITHANORDER = "SELECT  RepairID, CustomerID, ContactName, ContactEmail, JobCardNumber, DateLogged, LastStatusChange, " +
                                           "MachineTypeID, MachineSerialNumber, SwopOutMachineID, MachineConditionID, TakenFrother, " +
                                           "TakenBeanLid, TakenWaterLid, BrokenFrother, BrokenBeanLid, BrokenWaterLid, RepairFaultID, " +
                                           "RepairFaultDesc, RepairsTbl.RepairStatusID, RelatedOrderID, Notes " +
                                           "FROM (RepairsTbl INNER JOIN TempOrdersLinesTbl ON RepairsTbl.RelatedOrderID = TempOrdersLinesTbl.OriginalOrderID)";
    const string CONST_SQL_SETDONEBYID = "UPDATE RepairsTbl SET RepairStatusID = ? WHERE (RelatedOrderID = ?)";

    //const string CONST_SQL_SETDONEBYTEMPORDERS = "UPDATE RepairsTbl SET RepairStatusID = ? " +
    //                              "WHERE (RepairStatusID >= ?) AND (RelatedOrderID = (SELECT OriginalOrderID FROM TempOrdersLinesTbl " +
    //                                      "WHERE (OriginalOrderID = RepairsTbl.RelatedOrderID)))";

    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public List<RepairsTbl> GetAll(string SortBy) { return GetAllRepairs(SortBy, CONST_SQL_SELECT, null); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<RepairsTbl> GetAllNotDone(string SortBy) { return GetAllRepairs(SortBy, CONST_SQL_SELECTNOTDONE, null); }
    /// <summary>
    /// GetAllRepairsOfStatus - gets all the repairs of a particular status if the status is "OPEN" then get all not done
    /// </summary>
    /// <param name="SortBy">the sorty by paramter</param>
    /// <param name="pRepairStatus">the status "OPEN" for only that that are not open</param>
    /// <returns></returns>
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<RepairsTbl> GetAllRepairsOfStatus(string SortBy, string pRepairStatus)
    {
      int _Status;
      bool _AllOpen = !(Int32.TryParse(pRepairStatus, out _Status));

      if ((pRepairStatus.Equals("OPEN")) || (_AllOpen))
        return GetAllRepairs(SortBy, CONST_SQL_SELECTNOTDONE, null);
      else
      {
        List<DBParameter> _WhereParams = new List<DBParameter>();
        _WhereParams.Add(new DBParameter { DataValue = _Status, DataDbType = DbType.Int32 } );

        return GetAllRepairs(SortBy, CONST_SQL_SELECTONSTATUS, _WhereParams);
      }
    
    }
    private List<RepairsTbl> GetAllRepairs(string SortBy, string pSQL, List<DBParameter> pWhereParams)
    {
      List<RepairsTbl> _DataItems = new List<RepairsTbl>();
      TrackerDb _TrackerDb = new TrackerDb();
      pSQL += " ORDER BY " + (String.IsNullOrEmpty(SortBy) ? "DateLogged DESC" : SortBy);     // Add order by string
      // params would go here if need
      if (pWhereParams != null)
        _TrackerDb.WhereParams = pWhereParams;
      IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(pSQL);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          RepairsTbl _DataItem = new RepairsTbl();

          _DataItem.RepairID = (_DataReader["RepairID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.ContactName = (_DataReader["ContactName"] == DBNull.Value) ? string.Empty : _DataReader["ContactName"].ToString();
          _DataItem.ContactEmail = (_DataReader["ContactEmail"] == DBNull.Value) ? string.Empty : _DataReader["ContactEmail"].ToString();
          _DataItem.JobCardNumber = (_DataReader["JobCardNumber"] == DBNull.Value) ? string.Empty : _DataReader["JobCardNumber"].ToString();
          _DataItem.DateLogged = (_DataReader["DateLogged"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateLogged"]).Date;
          _DataItem.LastStatusChange = (_DataReader["LastStatusChange"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["LastStatusChange"]).Date;
          _DataItem.MachineTypeID = (_DataReader["MachineTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineTypeID"]);
          _DataItem.MachineSerialNumber = (_DataReader["MachineSerialNumber"] == DBNull.Value) ? string.Empty : _DataReader["MachineSerialNumber"].ToString();
          _DataItem.SwopOutMachineID = (_DataReader["SwopOutMachineID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SwopOutMachineID"]);
          _DataItem.MachineConditionID = (_DataReader["MachineConditionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineConditionID"]);
          _DataItem.TakenFrother = (_DataReader["TakenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenFrother"]);
          _DataItem.TakenBeanLid = (_DataReader["TakenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenBeanLid"]);
          _DataItem.TakenWaterLid = (_DataReader["TakenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenWaterLid"]);
          _DataItem.BrokenFrother = (_DataReader["BrokenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenFrother"]);
          _DataItem.BrokenBeanLid = (_DataReader["BrokenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenBeanLid"]);
          _DataItem.BrokenWaterLid = (_DataReader["BrokenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenWaterLid"]);
          _DataItem.RepairFaultID = (_DataReader["RepairFaultID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairFaultID"]);
          _DataItem.RepairFaultDesc = (_DataReader["RepairFaultDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairFaultDesc"].ToString();
          _DataItem.RepairStatusID = (_DataReader["RepairStatusID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairStatusID"]);
          _DataItem.RelatedOrderID = (_DataReader["RelatedOrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["RelatedOrderID"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TrackerDb.Close();
      return _DataItems;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public RepairsTbl GetRepairById(int pRepairID)
    {
      RepairsTbl _DataItem = null;

      if (pRepairID > 0)
      {
        TrackerDb _TrackerDb = new TrackerDb();
        // params would go here if need
        _TrackerDb.AddWhereParams(pRepairID, DbType.Int32);
        IDataReader _DataReader = _TrackerDb.ExecuteSQLGetDataReader(CONST_SQL_SELECTBYREPAIRID);

        if (_DataReader != null)
        {
          if (_DataReader.Read())
          {
            _DataItem = new RepairsTbl();

            _DataItem.RepairID = pRepairID;
            _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
            _DataItem.ContactName = (_DataReader["ContactName"] == DBNull.Value) ? string.Empty : _DataReader["ContactName"].ToString();
            _DataItem.ContactEmail = (_DataReader["ContactEmail"] == DBNull.Value) ? string.Empty : _DataReader["ContactEmail"].ToString();
            _DataItem.JobCardNumber = (_DataReader["JobCardNumber"] == DBNull.Value) ? string.Empty : _DataReader["JobCardNumber"].ToString();
            _DataItem.DateLogged = (_DataReader["DateLogged"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateLogged"]).Date;
            _DataItem.LastStatusChange = (_DataReader["LastStatusChange"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["LastStatusChange"]).Date;
            _DataItem.MachineTypeID = (_DataReader["MachineTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineTypeID"]);
            _DataItem.MachineSerialNumber = (_DataReader["MachineSerialNumber"] == DBNull.Value) ? string.Empty : _DataReader["MachineSerialNumber"].ToString();
            _DataItem.SwopOutMachineID = (_DataReader["SwopOutMachineID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SwopOutMachineID"]);
            _DataItem.MachineConditionID = (_DataReader["MachineConditionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineConditionID"]);
            _DataItem.TakenFrother = (_DataReader["TakenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenFrother"]);
            _DataItem.TakenBeanLid = (_DataReader["TakenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenBeanLid"]);
            _DataItem.TakenWaterLid = (_DataReader["TakenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenWaterLid"]);
            _DataItem.BrokenFrother = (_DataReader["BrokenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenFrother"]);
            _DataItem.BrokenBeanLid = (_DataReader["BrokenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenBeanLid"]);
            _DataItem.BrokenWaterLid = (_DataReader["BrokenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenWaterLid"]);
            _DataItem.RepairFaultID = (_DataReader["RepairFaultID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairFaultID"]);
            _DataItem.RepairFaultDesc = (_DataReader["RepairFaultDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairFaultDesc"].ToString();
            _DataItem.RepairStatusID = (_DataReader["RepairStatusID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairStatusID"]);
            _DataItem.RelatedOrderID = (_DataReader["RelatedOrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["RelatedOrderID"]);
            _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          }
          _DataReader.Close();
        }
        _TrackerDb.Close();
      }
      return _DataItem;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool InsertRepair(RepairsTbl DataItem)
    {
      bool _result = false;
      TrackerDb _TrackerDb = new TrackerDb();

      _TrackerDb.AddParams(DataItem.CustomerID, DbType.Int64);
      _TrackerDb.AddParams(DataItem.ContactName, DbType.String);
      _TrackerDb.AddParams(DataItem.ContactEmail, DbType.String);
      _TrackerDb.AddParams(DataItem.JobCardNumber, DbType.String);
      _TrackerDb.AddParams(DataItem.DateLogged, DbType.Date);
      _TrackerDb.AddParams(DataItem.LastStatusChange, DbType.Date);
      _TrackerDb.AddParams(DataItem.MachineTypeID, DbType.Int32);
      _TrackerDb.AddParams(DataItem.MachineSerialNumber, DbType.String);
      _TrackerDb.AddParams(DataItem.SwopOutMachineID, DbType.Int32);
      _TrackerDb.AddParams(DataItem.MachineConditionID, DbType.Int32);
      _TrackerDb.AddParams(DataItem.TakenFrother, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.TakenBeanLid, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.TakenWaterLid, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.BrokenFrother, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.BrokenBeanLid, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.BrokenWaterLid, DbType.Boolean);
      _TrackerDb.AddParams(DataItem.RepairFaultID, DbType.Int32);
      _TrackerDb.AddParams(DataItem.RepairFaultDesc, DbType.String);
      _TrackerDb.AddParams(DataItem.RepairStatusID, DbType.Int32);
      _TrackerDb.AddParams(DataItem.RelatedOrderID, DbType.Int64);
      _TrackerDb.AddParams(DataItem.Notes, DbType.String);

      _result = String.IsNullOrEmpty(_TrackerDb.ExecuteNonQuerySQL(CONST_SQL_INSERT));
      _TrackerDb.Close();

      return _result;
    }
    public int GetLastIDInserted(long pCustomerID)
    {
      int _LastID = 0;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pCustomerID, DbType.Int64);

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTLAST);

      if (_DataReader != null)
        if (_DataReader.Read())
          _LastID = (_DataReader["RepairID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairID"]);
        else
          _DataReader.Close();

      _TDB.Close();

      return _LastID;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string UpdateRepair(TrackerDotNet.control.RepairsTbl RepairItem) { return UpdateRepair(RepairItem, RepairItem.RepairID); }
    public string UpdateRepair(TrackerDotNet.control.RepairsTbl RepairItem, int orig_RepairID)
    {
      string _result = String.Empty;
      TrackerDb _TrackerDb = new TrackerDb();

      _TrackerDb.AddParams(RepairItem.CustomerID, DbType.Int64);
      _TrackerDb.AddParams(RepairItem.ContactName, DbType.String);
      _TrackerDb.AddParams(RepairItem.ContactEmail, DbType.String);
      _TrackerDb.AddParams(RepairItem.JobCardNumber, DbType.String);
      _TrackerDb.AddParams(RepairItem.DateLogged, DbType.Date);
      _TrackerDb.AddParams(RepairItem.LastStatusChange, DbType.Date);
      _TrackerDb.AddParams(RepairItem.MachineTypeID, DbType.Int32);
      _TrackerDb.AddParams(RepairItem.MachineSerialNumber, DbType.String);
      _TrackerDb.AddParams(RepairItem.SwopOutMachineID, DbType.Int32);
      _TrackerDb.AddParams(RepairItem.MachineConditionID, DbType.Int32);
      _TrackerDb.AddParams(RepairItem.TakenFrother, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.TakenBeanLid, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.TakenWaterLid, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.BrokenFrother, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.BrokenBeanLid, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.BrokenWaterLid, DbType.Boolean);
      _TrackerDb.AddParams(RepairItem.RepairFaultID, DbType.Int32);
      _TrackerDb.AddParams(RepairItem.RepairFaultDesc, DbType.String);
      _TrackerDb.AddParams(RepairItem.RepairStatusID, DbType.Int32);
      _TrackerDb.AddParams(RepairItem.RelatedOrderID, DbType.Int64);
      _TrackerDb.AddParams(RepairItem.Notes, DbType.String);

      _TrackerDb.AddWhereParams(orig_RepairID, DbType.Int32);

      _result = _TrackerDb.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TrackerDb.Close();

      return _result;
    }
    //public string UpdateRepairStatus(int pNewRepairStatusID, int pRepairID)
    //{
    //  string _result = String.Empty;
    //  TrackerDb _TrackerDb = new TrackerDb();

    //  _TrackerDb.AddParams(pNewRepairStatusID, DbType.Int32);
    //  _TrackerDb.AddWhereParams(pRepairID, DbType.Int32);

    //  _result = _TrackerDb.ExecuteNonQuerySQL(CONST_SQL_UPDATESTATUS);
    //  _TrackerDb.Close();

    //  return _result;
    //}
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string DeleteRepair(int RepairID)
    {
      string _result = String.Empty;
      TrackerDb _TrackerDb = new TrackerDb();
      _TrackerDb.AddParams(RepairID, DbType.Int32);

      _result = _TrackerDb.ExecuteNonQuerySQL(CONST_SQL_DELETE);

      return _result;
    }

    private bool SendStatusChangeEmail(RepairsTbl pRepair)
    {
      EmailCls _Email = new EmailCls();
      EquipTypeTbl _EquipTypes = new EquipTypeTbl();
      RepairStatusesTbl _RepairStatuses = new RepairStatusesTbl();
      _Email.SetEmailTo(pRepair.ContactEmail);
      _Email.SetEmailSubject("Change to repair status - notification.");
      _Email.AddStrAndNewLineToBody("<b>Repair Status Change Notification</b>");
      _Email.AddFormatToBody("Dear {0}, <br /><br />", pRepair.ContactName);
      _Email.AddFormatToBody("Please note that repair status of the {0}, serial number: {1}, has changed to <b>{2}</b>.<br /><br />",
        _EquipTypes.GetEquipName(pRepair._MachineTypeID),
        pRepair.MachineSerialNumber,_RepairStatuses.GetRepairStatusDesc(pRepair.RepairStatusID));
      _Email.AddStrAndNewLineToBody("You are receiving this status update since your email address was assigned to the tracking of this repair.<br />");
      _Email.AddStrAndNewLineToBody("Please note that Quaffee DOES NOT run a coffee machine repair workshop. As a service to our current coffee "+
        "purchasing clients we will take the coffee machine to the workshop, give a swop out machine to the client, if available, and " +
        "charge what we are charged by the workshop plus a small admin fee. Please note: we are not able to quote these repairs, if you would like " +
        "to contact the workshop directly, please ask us for their details.<br />");
      _Email.AddStrAndNewLineToBody("For clients that are not currently using our coffee, or have not used our coffee consistently over" +
        " the last 3 months, we may at our discretion offer a swop out machine, at a fee, and also change a collection and delivery fee.<br />");
      _Email.AddStrAndNewLineToBody("Any warantee on the repairs is carried by the workshop not by Quaffee.<br />");
      _Email.AddStrAndNewLineToBody("The Quaffee Team");
      _Email.AddStrAndNewLineToBody("web: <a href='http://www.quaffee.co.za'>quaffee.co.za</a>");

      return _Email.SendEmail();
    }
    private bool LogARepair(RepairsTbl pRepair, bool pCalcOrderData)
    {
      bool _Success = false;
      // create a new order for delivery [RoastDate]
      OrderTblData _OrderData = new OrderTblData();
      DateTime _RequiredByDate = DateTime.Now.Date.AddDays(7);

      // add the default data to order
      _OrderData.CustomerId = pRepair.CustomerID;
      _OrderData.ItemTypeID = ItemTypeTbl.CONST_SERVICEITEMID;
      _OrderData.QuantityOrdered = 1;
      _OrderData.Notes = "Collect/Swop out Machine for Service";

      // Calculate the Data from the customer details
      if (pCalcOrderData)
      {
        TrackerTools _TT = new TrackerTools();
        _OrderData.RoastDate = _TT.GetNextRoastDateByCustomerID(pRepair.CustomerID, ref _RequiredByDate);
        TrackerTools.ContactPreferedItems _ContactPreferedItems = _TT.RetrieveCustomerPrefs(pRepair.CustomerID);

        _OrderData.OrderDate = DateTime.Now.Date;
        _OrderData.RequiredByDate = _RequiredByDate;
        if (_ContactPreferedItems.RequiresPurchOrder)
          _OrderData.PurchaseOrder = TrackerTools.CONST_POREQUIRED;
        _OrderData.ToBeDeliveredBy = _ContactPreferedItems.DeliveryByID;
      }
      else
      {
        _OrderData.RoastDate = _OrderData.OrderDate = DateTime.Now.Date;
        _OrderData.RequiredByDate = _RequiredByDate;
      }
      // save the data to the orders 
      OrderTbl _Order = new OrderTbl();

      _Order.InsertNewOrderLine(_OrderData);
      pRepair.RelatedOrderID = _Order.GetLastOrderAdded(_OrderData.CustomerId, _OrderData.OrderDate, ItemTypeTbl.CONST_SERVICEITEMID);

      return _Success;
    }
    public bool HandleAndUpdateRepairStatusChange(RepairsTbl pRepair)
    {
      // send email and handle status change
      bool _Success = true;

      switch (pRepair.RepairStatusID)
      {
        case CONST_REPAIRSTATUS_LOGGED:
          {
            _Success = LogARepair(pRepair, true);
            break;
          }
        case CONST_REPAIRSTATUS_COLLECTED:
          {
            if (pRepair.RelatedOrderID.Equals(0))
            {
              _Success = LogARepair(pRepair, true);
            }
            else
            {
              OrderTbl _Order = new OrderTbl();
              _Order.UpdateIncDeliveryDateBy7(pRepair.RelatedOrderID);
            }
            break;
          }
        case CONST_REPAIRSTATUS_WORKSHOP:
          {
            if (pRepair.RelatedOrderID.Equals(0))
            {
              _Success = LogARepair(pRepair, false);
            }
            else
            {
              OrderTbl _Order = new OrderTbl();
              _Order.UpdateIncDeliveryDateBy7(pRepair.RelatedOrderID);
            }
            if (!String.IsNullOrEmpty(pRepair.MachineSerialNumber))
            {
              // if we have no serial number in the current Customer Tbl then update
              CustomersTbl _Customers = new CustomersTbl();
              _Customers.SetEquipDetailsIfEmpty(pRepair.MachineTypeID, pRepair.MachineSerialNumber, pRepair.CustomerID);
            }
            break;
          }
        case CONST_REPAIRSTATUS_READY:
          {
            if (pRepair.RelatedOrderID > 0)
            {
              OrderTbl _Order = new OrderTbl();
              NextRoastDateByCityTbl _NextDates = new NextRoastDateByCityTbl();
              _Order.UpdateOrderDeliveryDate(_NextDates.GetNextDeliveryDate(pRepair.CustomerID), pRepair.RelatedOrderID);
            }

            break;
          }
        case CONST_REPAIRSTATUS_DONE:
          {
            if (pRepair.RelatedOrderID > 0)
            {
              OrderTbl _Order = new OrderTbl();
              _Order.UpdateSetDoneByID(true, pRepair.RelatedOrderID);
              // ??? should we do this since it interupts with the Preiction Calculations              
            }
            break;
          }
        default:
          {
            break;
          }
      }
      _Success = String.IsNullOrEmpty(UpdateRepair(pRepair));

      _Success = _Success && SendStatusChangeEmail(pRepair);

      return _Success;
    }
    public List<RepairsTbl> GetListOfRelatedTempOrders()
    {
      List<RepairsTbl> _RelatedRepairs = new List<RepairsTbl>();   // should only be one but check
      TrackerDb _TDB = new TrackerDb();

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTITEMWITHANORDER);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          RepairsTbl _RelatedRepair = new RepairsTbl();

          _RelatedRepair.RepairID = (_DataReader["RepairID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairID"]);
          _RelatedRepair.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _RelatedRepair.ContactName = (_DataReader["ContactName"] == DBNull.Value) ? string.Empty : _DataReader["ContactName"].ToString();
          _RelatedRepair.ContactEmail = (_DataReader["ContactEmail"] == DBNull.Value) ? string.Empty : _DataReader["ContactEmail"].ToString();
          _RelatedRepair.JobCardNumber = (_DataReader["JobCardNumber"] == DBNull.Value) ? string.Empty : _DataReader["JobCardNumber"].ToString();
          _RelatedRepair.DateLogged = (_DataReader["DateLogged"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["DateLogged"]).Date;
          _RelatedRepair.LastStatusChange = (_DataReader["LastStatusChange"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["LastStatusChange"]).Date;
          _RelatedRepair.MachineTypeID = (_DataReader["MachineTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineTypeID"]);
          _RelatedRepair.MachineSerialNumber = (_DataReader["MachineSerialNumber"] == DBNull.Value) ? string.Empty : _DataReader["MachineSerialNumber"].ToString();
          _RelatedRepair.SwopOutMachineID = (_DataReader["SwopOutMachineID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["SwopOutMachineID"]);
          _RelatedRepair.MachineConditionID = (_DataReader["MachineConditionID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["MachineConditionID"]);
          _RelatedRepair.TakenFrother = (_DataReader["TakenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenFrother"]);
          _RelatedRepair.TakenBeanLid = (_DataReader["TakenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenBeanLid"]);
          _RelatedRepair.TakenWaterLid = (_DataReader["TakenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["TakenWaterLid"]);
          _RelatedRepair.BrokenFrother = (_DataReader["BrokenFrother"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenFrother"]);
          _RelatedRepair.BrokenBeanLid = (_DataReader["BrokenBeanLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenBeanLid"]);
          _RelatedRepair.BrokenWaterLid = (_DataReader["BrokenWaterLid"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["BrokenWaterLid"]);
          _RelatedRepair.RepairFaultID = (_DataReader["RepairFaultID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairFaultID"]);
          _RelatedRepair.RepairFaultDesc = (_DataReader["RepairFaultDesc"] == DBNull.Value) ? string.Empty : _DataReader["RepairFaultDesc"].ToString();
          _RelatedRepair.RepairStatusID = (_DataReader["RepairStatusID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["RepairStatusID"]);
          _RelatedRepair.RelatedOrderID = (_DataReader["RelatedOrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["RelatedOrderID"]);
          _RelatedRepair.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();

          _RelatedRepairs.Add(_RelatedRepair);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _RelatedRepairs;

    }
    public string SetStatusDoneByTempOrder()
    {
      string _Result = String.Empty;
      List<RepairsTbl> _RelatedRepairs = GetListOfRelatedTempOrders();
      if (_RelatedRepairs.Count > 0)
      {
        TempOrdersLinesTbl _TOLines = new TempOrdersLinesTbl();

        for (int i = 0; i < _RelatedRepairs.Count; i++)
        {
          if (_RelatedRepairs[i].RepairStatusID <= CONST_REPAIRSTATUS_WORKSHOP)
          {
            _TOLines.DeleteByOriginalID(_RelatedRepairs[i].RelatedOrderID);
            OrderTbl _Order = new OrderTbl();
            _Order.UpdateIncDeliveryDateBy7(_RelatedRepairs[i].RelatedOrderID);
          }
          else
          {
            TrackerDb _TDB = new TrackerDb();
            _TDB.AddParams(CONST_REPAIRSTATUS_DONE, DbType.Int32);
            _Result += _TDB.ExecuteNonQuerySQL(CONST_SQL_SETDONEBYID);
            _TDB.Close();
          }

        }
      }
      return _Result;
    }

  }
}
