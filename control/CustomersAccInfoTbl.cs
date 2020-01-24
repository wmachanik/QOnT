/// --- auto generated class for table: CustomersAccInfoTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Configuration;        // TrackerDot classes used for DB access
using System.Data;                  // for IDataReader and DbType
using System.Data.OleDb;
using QOnT.classes;

namespace QOnT.control
{
  public class CustomersAccInfoTbl
  {
    public const int CONST_DEFAULTPAYMENTTERMID = 5;  // should be on statement
    public const int CONST_DEFAULTPRICELEVEL = 1;  // should be standard


    #region InternalVariableDeclarations
    private int _CustomersAccInfoID;
    private long _CustomerID;
    private bool _RequiresPurchOrder;
    private string _CustomerVATNo;
    private string _BillAddr1;
    private string _BillAddr2;
    private string _BillAddr3;
    private string _BillAddr4;
    private string _BillAddr5;
    private string _ShipAddr1;
    private string _ShipAddr2;
    private string _ShipAddr3;
    private string _ShipAddr4;
    private string _ShipAddr5;
    private string _AccEmail;
    private string _AltAccEmail;
    private int _PaymentTermID;
    private double _Limit;
    private string _FullCoName;
    private string _AccFirstName;
    private string _AccLastName;
    private string _AltAccFirstName;
    private string _AltAccLastName;
    private int _PriceLevelID;
    private int _InvoiceTypeID;
    private string _RegNo;
    private string _BankAccNo;
    private string _BankBranch;
    private bool _Enabled;
    private string _Notes;
    #endregion

    // class definition
    public CustomersAccInfoTbl()
    {
      _CustomersAccInfoID = 0;
      _CustomerID = 0;
      _RequiresPurchOrder = false;
      _CustomerVATNo = string.Empty;
      _BillAddr1 = string.Empty;
      _BillAddr2 = string.Empty;
      _BillAddr3 = string.Empty;
      _BillAddr4 = string.Empty;
      _BillAddr5 = string.Empty;
      _ShipAddr1 = string.Empty;
      _ShipAddr2 = string.Empty;
      _ShipAddr3 = string.Empty;
      _ShipAddr4 = string.Empty;
      _ShipAddr5 = string.Empty;
      _AccEmail = string.Empty;
      _AltAccEmail = string.Empty;
      _PaymentTermID = PaymentTermsTbl.CONST_DEFAULT_PAYMENTTERMID;
      _Limit = 0.0;
      _FullCoName = string.Empty;
      _AccFirstName = string.Empty;
      _AccLastName = string.Empty;
      _AltAccFirstName = string.Empty;
      _AltAccLastName = string.Empty;
      _PriceLevelID = PriceLevelsTbl.CONST_DEAFULT_PRICELEVELID;
      _InvoiceTypeID = InvoiceTypeTbl.CONST_DEFAULT_INVOICETYPEID;
      _RegNo = string.Empty;
      _BankAccNo = string.Empty;
      _BankBranch = string.Empty;
      _Enabled = false;
      _Notes = string.Empty;
    }

    #region PublicVariableDeclarations
    public int CustomersAccInfoID { get { return _CustomersAccInfoID;}  set { _CustomersAccInfoID = value;} }
    public long CustomerID { get { return _CustomerID;}  set { _CustomerID = value;} }
    public bool RequiresPurchOrder { get { return _RequiresPurchOrder;}  set { _RequiresPurchOrder = value;} }
    public string CustomerVATNo { get { return _CustomerVATNo;}  set { _CustomerVATNo = value;} }
    public string BillAddr1 { get { return _BillAddr1;}  set { _BillAddr1 = value;} }
    public string BillAddr2 { get { return _BillAddr2;}  set { _BillAddr2 = value;} }
    public string BillAddr3 { get { return _BillAddr3;}  set { _BillAddr3 = value;} }
    public string BillAddr4 { get { return _BillAddr4;}  set { _BillAddr4 = value;} }
    public string BillAddr5 { get { return _BillAddr5;}  set { _BillAddr5 = value;} }
    public string ShipAddr1 { get { return _ShipAddr1;}  set { _ShipAddr1 = value;} }
    public string ShipAddr2 { get { return _ShipAddr2;}  set { _ShipAddr2 = value;} }
    public string ShipAddr3 { get { return _ShipAddr3;}  set { _ShipAddr3 = value;} }
    public string ShipAddr4 { get { return _ShipAddr4;}  set { _ShipAddr4 = value;} }
    public string ShipAddr5 { get { return _ShipAddr5;}  set { _ShipAddr5 = value;} }
    public string AccEmail { get { return _AccEmail;}  set { _AccEmail = value;} }
    public string AltAccEmail { get { return _AltAccEmail;}  set { _AltAccEmail = value;} }
    public int PaymentTermID { get { return _PaymentTermID;}  set { _PaymentTermID = value;} }
    public double Limit { get { return _Limit;}  set { _Limit = value;} }
    public string FullCoName { get { return _FullCoName;}  set { _FullCoName = value;} }
    public string AccFirstName { get { return _AccFirstName;}  set { _AccFirstName = value;} }
    public string AccLastName { get { return _AccLastName;}  set { _AccLastName = value;} }
    public string AltAccFirstName { get { return _AltAccFirstName;}  set { _AltAccFirstName = value;} }
    public string AltAccLastName { get { return _AltAccLastName;}  set { _AltAccLastName = value;} }
    public int PriceLevelID { get { return _PriceLevelID;}  set { _PriceLevelID = value;} }
    public int InvoiceTypeID { get { return _InvoiceTypeID;}  set { _InvoiceTypeID = value;} }
    public string RegNo { get { return _RegNo;}  set { _RegNo = value;} }
    public string BankAccNo { get { return _BankAccNo;}  set { _BankAccNo = value;} }
    public string BankBranch { get { return _BankBranch;}  set { _BankBranch = value;} }
    public bool Enabled { get { return _Enabled;}  set { _Enabled = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }
    #endregion

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT CustomersAccInfoID, CustomerID, RequiresPurchOrder, CustomerVATNo, BillAddr1, BillAddr2, BillAddr3, BillAddr4, " + 
                            " BillAddr5, ShipAddr1, ShipAddr2, ShipAddr3, ShipAddr4, ShipAddr5, AccEmail, AltAccEmail, " + 
                            " PaymentTermID, Limit, FullCoName, AccFirstName, AccLastName, AltAccFirstName, AltAccLastName, PriceLevelID, " + 
                            " InvoiceTypeID, RegNo, BankAccNo, BankBranch, Enabled, Notes FROM CustomersAccInfoTbl";
    const string CONST_SQL_SELECTBYCUSTID = "SELECT CustomersAccInfoID, RequiresPurchOrder, CustomerVATNo, BillAddr1, BillAddr2, BillAddr3, BillAddr4, " +
                            " BillAddr5, ShipAddr1, ShipAddr2, ShipAddr3, ShipAddr4, ShipAddr5, AccEmail, AltAccEmail, " +
                            " PaymentTermID, Limit, FullCoName, AccFirstName, AccLastName, AltAccFirstName, AltAccLastName, PriceLevelID, " +
                            " InvoiceTypeID, RegNo, BankAccNo, BankBranch, Enabled, Notes FROM CustomersAccInfoTbl WHERE (CustomerID = ?)";
    const string CONST_SQL_SELECTPaymentTermID_BYCUSTID = "SELECT PaymentTermID FROM CustomersAccInfoTbl WHERE (CustomerID = ?)";
    const string CONST_SQL_INSERT = "INSERT INTO CustomersAccInfoTbl (CustomerID, RequiresPurchOrder, CustomerVATNo, BillAddr1, BillAddr2, BillAddr3, BillAddr4, BillAddr5, " + 
                            " ShipAddr1, ShipAddr2, ShipAddr3, ShipAddr4, ShipAddr5, AccEmail, AltAccEmail, PaymentTermID, " + 
                            " Limit, FullCoName, AccFirstName, AccLastName, AltAccFirstName, AltAccLastName, PriceLevelID, InvoiceTypeID, " + 
                            " RegNo, BankAccNo, BankBranch, Enabled, Notes)" + 
                          " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE CustomersAccInfoTbl SET CustomerID = ?, RequiresPurchOrder = ?, CustomerVATNo = ?, BillAddr1 = ?, BillAddr2 = ?, BillAddr3 = ?, BillAddr4 = ?, BillAddr5 = ?, " + 
                            " ShipAddr1 = ?, ShipAddr2 = ?, ShipAddr3 = ?, ShipAddr4 = ?, ShipAddr5 = ?, AccEmail = ?, AltAccEmail = ?, PaymentTermID = ?, " + 
                            " Limit = ?, FullCoName = ?, AccFirstName = ?, AccLastName = ?, AltAccFirstName = ?, AltAccLastName = ?, PriceLevelID = ?, InvoiceTypeID = ?, " + 
                            " RegNo = ?, BankAccNo = ?, BankBranch = ?, Enabled = ?, Notes = ?" + 
                           " WHERE (CustomersAccInfoID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM CustomersAccInfoTbl WHERE (CustomersAccInfoID = ?)";

    const string CONST_SQL_GETCUSTOMERSINVOICETYPE = "SELECT InvoiceTypeID FROM CustomersAccInfoTbl WHERE (CustomerID = ?)";

    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<CustomersAccInfoTbl> GetAll(string SortBy)
    {
      List<CustomersAccInfoTbl> _DataItems = new List<CustomersAccInfoTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          CustomersAccInfoTbl _DataItem = new CustomersAccInfoTbl();

          #region StoreThisDataItem
          _DataItem.CustomersAccInfoID = (_DataReader["CustomersAccInfoID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomersAccInfoID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomerID"]);
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          _DataItem.CustomerVATNo = (_DataReader["CustomerVATNo"] == DBNull.Value) ? string.Empty : _DataReader["CustomerVATNo"].ToString();
          _DataItem.BillAddr1 = (_DataReader["BillAddr1"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr1"].ToString();
          _DataItem.BillAddr2 = (_DataReader["BillAddr2"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr2"].ToString();
          _DataItem.BillAddr3 = (_DataReader["BillAddr3"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr3"].ToString();
          _DataItem.BillAddr4 = (_DataReader["BillAddr4"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr4"].ToString();
          _DataItem.BillAddr5 = (_DataReader["BillAddr5"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr5"].ToString();
          _DataItem.ShipAddr1 = (_DataReader["ShipAddr1"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr1"].ToString();
          _DataItem.ShipAddr2 = (_DataReader["ShipAddr2"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr2"].ToString();
          _DataItem.ShipAddr3 = (_DataReader["ShipAddr3"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr3"].ToString();
          _DataItem.ShipAddr4 = (_DataReader["ShipAddr4"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr4"].ToString();
          _DataItem.ShipAddr5 = (_DataReader["ShipAddr5"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr5"].ToString();
          _DataItem.AccEmail = (_DataReader["AccEmail"] == DBNull.Value) ? string.Empty : _DataReader["AccEmail"].ToString();
          _DataItem.AltAccEmail = (_DataReader["AltAccEmail"] == DBNull.Value) ? string.Empty : _DataReader["AltAccEmail"].ToString();
          _DataItem.PaymentTermID = (_DataReader["PaymentTermID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PaymentTermID"]);
          _DataItem.Limit = (_DataReader["Limit"] == DBNull.Value) ? 0.0 : Convert.ToDouble(_DataReader["Limit"]);
          _DataItem.FullCoName = (_DataReader["FullCoName"] == DBNull.Value) ? string.Empty : _DataReader["FullCoName"].ToString();
          _DataItem.AccFirstName = (_DataReader["AccFirstName"] == DBNull.Value) ? string.Empty : _DataReader["AccFirstName"].ToString();
          _DataItem.AccLastName = (_DataReader["AccLastName"] == DBNull.Value) ? string.Empty : _DataReader["AccLastName"].ToString();
          _DataItem.AltAccFirstName = (_DataReader["AltAccFirstName"] == DBNull.Value) ? string.Empty : _DataReader["AltAccFirstName"].ToString();
          _DataItem.AltAccLastName = (_DataReader["AltAccLastName"] == DBNull.Value) ? string.Empty : _DataReader["AltAccLastName"].ToString();
          _DataItem.PriceLevelID = (_DataReader["PriceLevelID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PriceLevelID"]);
          _DataItem.InvoiceTypeID = (_DataReader["InvoiceTypeID"] == DBNull.Value) ? InvoiceTypeTbl.CONST_DEFAULT_INVOICETYPEID : Convert.ToInt32(_DataReader["InvoiceTypeID"]);
          _DataItem.RegNo = (_DataReader["RegNo"] == DBNull.Value) ? string.Empty : _DataReader["RegNo"].ToString();
          _DataItem.BankAccNo = (_DataReader["BankAccNo"] == DBNull.Value) ? string.Empty : _DataReader["BankAccNo"].ToString();
          _DataItem.BankBranch = (_DataReader["BankBranch"] == DBNull.Value) ? string.Empty : _DataReader["BankBranch"].ToString();
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? true : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
    /// <summary>
    /// Get Account info using the cusotmer ID, if found return record if not found CustomerID - 0
    /// </summary>
    /// <param name="pCustomerID">cutomer ID </param>
    /// <returns>CustomersAccInfoTbl if found customerID > 0</returns>
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public CustomersAccInfoTbl GetByCustomerID(long pCustomerID)
    {
      CustomersAccInfoTbl _DataItem = new CustomersAccInfoTbl();
      _DataItem.CustomerID = pCustomerID;  // assing it to non zero so we can do error checking
      string _sql = CONST_SQL_SELECTBYCUSTID;
      TrackerDb _TDB = new TrackerDb();
      // params would go here if need
      _TDB.AddWhereParams(pCustomerID, DbType.Int64, "@CustomerID");
      // _sql = _sql.Replace("?", pCustomerID.ToString());
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sql);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          #region StoreThisDataItem
          _DataItem.CustomersAccInfoID = (_DataReader["CustomersAccInfoID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["CustomersAccInfoID"]);
          _DataItem.RequiresPurchOrder = (_DataReader["RequiresPurchOrder"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["RequiresPurchOrder"]);
          _DataItem.CustomerVATNo = (_DataReader["CustomerVATNo"] == DBNull.Value) ? string.Empty : _DataReader["CustomerVATNo"].ToString();
          _DataItem.BillAddr1 = (_DataReader["BillAddr1"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr1"].ToString();
          _DataItem.BillAddr2 = (_DataReader["BillAddr2"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr2"].ToString();
          _DataItem.BillAddr3 = (_DataReader["BillAddr3"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr3"].ToString();
          _DataItem.BillAddr4 = (_DataReader["BillAddr4"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr4"].ToString();
          _DataItem.BillAddr5 = (_DataReader["BillAddr5"] == DBNull.Value) ? string.Empty : _DataReader["BillAddr5"].ToString();
          _DataItem.ShipAddr1 = (_DataReader["ShipAddr1"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr1"].ToString();
          _DataItem.ShipAddr2 = (_DataReader["ShipAddr2"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr2"].ToString();
          _DataItem.ShipAddr3 = (_DataReader["ShipAddr3"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr3"].ToString();
          _DataItem.ShipAddr4 = (_DataReader["ShipAddr4"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr4"].ToString();
          _DataItem.ShipAddr5 = (_DataReader["ShipAddr5"] == DBNull.Value) ? string.Empty : _DataReader["ShipAddr5"].ToString();
          _DataItem.AccEmail = (_DataReader["AccEmail"] == DBNull.Value) ? string.Empty : _DataReader["AccEmail"].ToString();
          _DataItem.AltAccEmail = (_DataReader["AltAccEmail"] == DBNull.Value) ? string.Empty : _DataReader["AltAccEmail"].ToString();
          _DataItem.PaymentTermID = (_DataReader["PaymentTermID"] == DBNull.Value) ? _DataItem.PaymentTermID : Convert.ToInt32(_DataReader["PaymentTermID"]);
          _DataItem.Limit = (_DataReader["Limit"] == DBNull.Value) ? 0.0 : Convert.ToDouble(_DataReader["Limit"]);
          _DataItem.FullCoName = (_DataReader["FullCoName"] == DBNull.Value) ? string.Empty : _DataReader["FullCoName"].ToString();
          _DataItem.AccFirstName = (_DataReader["AccFirstName"] == DBNull.Value) ? string.Empty : _DataReader["AccFirstName"].ToString();
          _DataItem.AccLastName = (_DataReader["AccLastName"] == DBNull.Value) ? string.Empty : _DataReader["AccLastName"].ToString();
          _DataItem.AltAccFirstName = (_DataReader["AltAccFirstName"] == DBNull.Value) ? string.Empty : _DataReader["AltAccFirstName"].ToString();
          _DataItem.AltAccLastName = (_DataReader["AltAccLastName"] == DBNull.Value) ? string.Empty : _DataReader["AltAccLastName"].ToString();
          _DataItem.PriceLevelID = (_DataReader["PriceLevelID"] == DBNull.Value) ? _DataItem.PriceLevelID : Convert.ToInt32(_DataReader["PriceLevelID"]);
          _DataItem.InvoiceTypeID = (_DataReader["InvoiceTypeID"] == DBNull.Value) ? _DataItem.InvoiceTypeID : Convert.ToInt32(_DataReader["InvoiceTypeID"]);
          _DataItem.RegNo = (_DataReader["RegNo"] == DBNull.Value) ? string.Empty : _DataReader["RegNo"].ToString();
          _DataItem.BankAccNo = (_DataReader["BankAccNo"] == DBNull.Value) ? string.Empty : _DataReader["BankAccNo"].ToString();
          _DataItem.BankBranch = (_DataReader["BankBranch"] == DBNull.Value) ? string.Empty : _DataReader["BankBranch"].ToString();
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? true : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItem;
    }
    public int GetByPaymentTypeIDByCustomerID(long pCustomerID)
    {
      int _PaymentTermID = 0;
      TrackerDb _TDB = new TrackerDb();
      // params would go here if need
      _TDB.AddWhereParams(pCustomerID, DbType.Int64, "@CustomerID");
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_SELECTPaymentTermID_BYCUSTID);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _PaymentTermID = (_DataReader["PaymentTermID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["PaymentTermID"]);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _PaymentTermID;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string Insert(CustomersAccInfoTbl pCustomersAccInfoTbl)
    {
      string _result = string.Empty;

      if (pCustomersAccInfoTbl.CustomerID == 0)
        _result = "error: customer number cannot be 0";
      else
      {
        TrackerDb _TDB = new TrackerDb();

        #region InsertParameters
        _TDB.AddParams(pCustomersAccInfoTbl.CustomerID, DbType.Int64, "@CustomerID");
        _TDB.AddParams(pCustomersAccInfoTbl.RequiresPurchOrder, DbType.Int32, "@RequiresPurchOrder");
        _TDB.AddParams(pCustomersAccInfoTbl.CustomerVATNo, DbType.String, "@CustomerVATNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr1, DbType.String, "@BillAddr1");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr2, DbType.String, "@BillAddr2");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr3, DbType.String, "@BillAddr3");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr4, DbType.String, "@BillAddr4");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr5, DbType.String, "@BillAddr5");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr1, DbType.String, "@ShipAddr1");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr2, DbType.String, "@ShipAddr2");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr3, DbType.String, "@ShipAddr3");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr4, DbType.String, "@ShipAddr4");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr5, DbType.String, "@ShipAddr5");
        _TDB.AddParams(pCustomersAccInfoTbl.AccEmail, DbType.String, "@AccEmail");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccEmail, DbType.String, "@AltAccEmail");
        _TDB.AddParams(pCustomersAccInfoTbl.PaymentTermID, DbType.Int32, "@PaymentTermID");
        _TDB.AddParams(pCustomersAccInfoTbl.Limit, DbType.Currency, "@Limit");
        _TDB.AddParams(pCustomersAccInfoTbl.FullCoName, DbType.String, "@FullCoName");
        _TDB.AddParams(pCustomersAccInfoTbl.AccFirstName, DbType.String, "@AccFirstName");
        _TDB.AddParams(pCustomersAccInfoTbl.AccLastName, DbType.String, "@AccLastName");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccFirstName, DbType.String, "@AltAccFirstName");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccLastName, DbType.String, "@AltAccLastName");
        _TDB.AddParams(pCustomersAccInfoTbl.PriceLevelID, DbType.Int32, "@PriceLevelID");
        _TDB.AddParams(pCustomersAccInfoTbl.InvoiceTypeID, DbType.Int32, "@InvoiceTypeID");
        _TDB.AddParams(pCustomersAccInfoTbl.RegNo, DbType.String, "@RegNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BankAccNo, DbType.String, "@BankAccNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BankBranch, DbType.String, "@BankBranch");
        _TDB.AddParams(pCustomersAccInfoTbl.Enabled, DbType.Boolean, "@Enabled");
        _TDB.AddParams(pCustomersAccInfoTbl.Notes, DbType.String, "@Notes");
        #endregion
        // Now we have the parameters excute the SQL
        _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
        _TDB.Close();
      }
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string Update(CustomersAccInfoTbl pCustomersAccInfoTbl)
    { return Update(pCustomersAccInfoTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string Update(CustomersAccInfoTbl pCustomersAccInfoTbl, long pOrignal_CustomersAccInfoID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();
      if (pCustomersAccInfoTbl.CustomerID == 0)
        _result = "error: CustomerID cannot be 0";
      else
      {

        #region UpdateParameters

        if (pOrignal_CustomersAccInfoID > 0)
          _TDB.AddWhereParams(pOrignal_CustomersAccInfoID, DbType.Int64);  // check this line it assumes id field is int32
        else
          _TDB.AddWhereParams(pCustomersAccInfoTbl.CustomersAccInfoID, DbType.Int64, "@CustomersAccInfoID");

        _TDB.AddParams(pCustomersAccInfoTbl.CustomerID, DbType.Int64, "@CustomerID");
        _TDB.AddParams(pCustomersAccInfoTbl.RequiresPurchOrder, DbType.Int32, "@RequiresPurchOrder");
        _TDB.AddParams(pCustomersAccInfoTbl.CustomerVATNo, DbType.String, "@CustomerVATNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr1, DbType.String, "@BillAddr1");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr2, DbType.String, "@BillAddr2");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr3, DbType.String, "@BillAddr3");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr4, DbType.String, "@BillAddr4");
        _TDB.AddParams(pCustomersAccInfoTbl.BillAddr5, DbType.String, "@BillAddr5");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr1, DbType.String, "@ShipAddr1");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr2, DbType.String, "@ShipAddr2");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr3, DbType.String, "@ShipAddr3");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr4, DbType.String, "@ShipAddr4");
        _TDB.AddParams(pCustomersAccInfoTbl.ShipAddr5, DbType.String, "@ShipAddr5");
        _TDB.AddParams(pCustomersAccInfoTbl.AccEmail, DbType.String, "@AccEmail");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccEmail, DbType.String, "@AltAccEmail");
        _TDB.AddParams(pCustomersAccInfoTbl.PaymentTermID, DbType.Int32, "@PaymentTermID");
        _TDB.AddParams(pCustomersAccInfoTbl.Limit, DbType.Double, "@Limit");
        _TDB.AddParams(pCustomersAccInfoTbl.FullCoName, DbType.String, "@FullCoName");
        _TDB.AddParams(pCustomersAccInfoTbl.AccFirstName, DbType.String, "@AccFirstName");
        _TDB.AddParams(pCustomersAccInfoTbl.AccLastName, DbType.String, "@AccLastName");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccFirstName, DbType.String, "@AltAccFirstName");
        _TDB.AddParams(pCustomersAccInfoTbl.AltAccLastName, DbType.String, "@AltAccLastName");
        _TDB.AddParams(pCustomersAccInfoTbl.PriceLevelID, DbType.Int32, "@PriceLevelID");
        _TDB.AddParams(pCustomersAccInfoTbl.InvoiceTypeID, DbType.Int32, "@InvoiceTypeID");
        _TDB.AddParams(pCustomersAccInfoTbl.RegNo, DbType.String, "@RegNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BankAccNo, DbType.String, "@BankAccNo");
        _TDB.AddParams(pCustomersAccInfoTbl.BankBranch, DbType.String, "@BankBranch");
        _TDB.AddParams(pCustomersAccInfoTbl.Enabled, DbType.Boolean, "@Enabled");
        _TDB.AddParams(pCustomersAccInfoTbl.Notes, DbType.String, "@Notes");
        #endregion
        // Now we have the parameters excute the SQL
        _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
        _TDB.Close();
      }
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string Delete(CustomersAccInfoTbl pCustomersAccInfoTbl)
    { return Delete(pCustomersAccInfoTbl.CustomersAccInfoID); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string Delete(int pCustomersAccInfoID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pCustomersAccInfoID, DbType.Int32, "@CustomersAccInfoID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }

    public int GetCustomersInvoiceType(long pCustomerID)
    {
      int _InvoiceTypeID = InvoiceTypeTbl.CONST_DEFAULT_INVOICETYPEID;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pCustomerID, DbType.Int64);
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_GETCUSTOMERSINVOICETYPE);
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _InvoiceTypeID = (_DataReader["InvoiceTypeID"] == DBNull.Value) ? InvoiceTypeTbl.CONST_DEFAULT_INVOICETYPEID : Convert.ToInt32(_DataReader["InvoiceTypeID"]); 
        _DataReader.Close();
      }
      _TDB.Close();

      return _InvoiceTypeID;
    }

  }
}
