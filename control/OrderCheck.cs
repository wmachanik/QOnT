using System;
using System.Collections.Generic;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
 
  public class OrderCheckData
  {
    long _OrderID;
    long _CustomerID;
    int _ItemTypeID;
    DateTime _RequiredByDate;

    public OrderCheckData()
    {
      _OrderID = _CustomerID = 0;
      _ItemTypeID = 0;
      _RequiredByDate = DateTime.MinValue;
    }

    public long OrderID { get { return _OrderID; } set { _OrderID = value; } }
    public long CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
    public int ItemTypeID { get { return _ItemTypeID; } set { _ItemTypeID = value; } }
    public DateTime RequiredByDate { get { return _RequiredByDate; } set { _RequiredByDate = value; } }
  }

  public class OrderCheck
  {

    #region ConstantDeclarations
    public const string CONST_SELECT_ORDERITEMSWITHSAMESERVICETYPEEXISTS =
       "SELECT  OrdersTbl.OrderID, OrdersTbl.CustomerID, ItemTypeTbl.ItemTypeID,OrdersTbl.RequiredByDate " +
       " FROM (OrdersTbl INNER JOIN ItemTypeTbl ON OrdersTbl.ItemTypeID = ItemTypeTbl.ItemTypeID) " +
       " WHERE (OrdersTbl.CustomerID = ?) AND (OrdersTbl.Done = false) AND ((OrdersTbl.RequiredByDate > ?) AND (OrdersTbl.RequiredByDate < ?)) AND " +
             " (ItemTypeTbl.ServiceTypeId = (SELECT ServiceTypesTbl.ServiceTypeId " +
             " FROM (ServiceTypesTbl INNER JOIN ItemTypeTbl ItemTypeTblChkd ON ServiceTypesTbl.ServiceTypeId = ItemTypeTblChkd.ServiceTypeId) " +
             " WHERE (ItemTypeTblChkd.ItemTypeID = ?))) ";
    #endregion

    public List<OrderCheckData> GetSimilarItemInOrders(long pCustomerID, int pItemTypeID, DateTime pStartDate, DateTime pEndDate)
    {
      List<OrderCheckData> _DataItems = null;
      TrackerDb _TDB = new TrackerDb();
      /// 
      _TDB.AddWhereParams(pCustomerID, DbType.Int64, "@CustomerID");
      _TDB.AddWhereParams(pStartDate, DbType.Date, "@RequiredStartDate");
      _TDB.AddWhereParams(pEndDate, DbType.Date, "@RequiredEndDate");
      _TDB.AddWhereParams(pItemTypeID, DbType.Int32, "@ItemTypeID");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_ORDERITEMSWITHSAMESERVICETYPEEXISTS);
      if ((_DataReader != null))
      {
        while (_DataReader.Read())
        {
          OrderCheckData _DataItem = new OrderCheckData();

          _DataItem.OrderID = (_DataReader["OrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["OrderID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now.Date : Convert.ToDateTime(_DataReader["RequiredByDate"]).Date;

          if (_DataItems == null)
            _DataItems = new List<OrderCheckData>();

          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
 /*  
  string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;
      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))
      {
        string _sqlCmd = CONST_SELECT_ORDERITEMSWITHSAMESERVICETYPEEXISTS;
        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
        /// 
        _cmd.Parameters.Add(new OleDbParameter { Value = pCustomerID });
        _cmd.Parameters.Add(new OleDbParameter { Value = pStartDate });
        _cmd.Parameters.Add(new OleDbParameter { Value = pEndDate });
        _cmd.Parameters.Add(new OleDbParameter { Value = pItemTypeID });
        _conn.Open();

        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          OrderCheckData _DataItem = new OrderCheckData();

          _DataItem.OrderID = (_DataReader["OrderID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["OrderID"]);
          _DataItem.CustomerID = (_DataReader["CustomerID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["CustomerID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.RequiredByDate = (_DataReader["RequiredByDate"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["RequiredByDate"]);

          if (_DataItems == null)
            _DataItems = new List<OrderCheckData>();

          _DataItems.Add(_DataItem);
        }
      }
      return _DataItems;

    }

  */
  }
}