using System;
using System.Collections.Generic;
using System.Data;
using TrackerDotNet.classes;

namespace TrackerDotNet.control
{
  public class OrderDetailDAL
  {
    // connection String

    public OrderDetailDAL()
    {
    }
    public List<OrderDetailData> LoadOrderDetailData(Int64 CustomerId, DateTime DeliveryDate, String Notes, int MaximumRows, int StartRowIndex)
    {
      List<OrderDetailData> oDetails = new List<OrderDetailData>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = "SELECT [ItemTypeID], [QuantityOrdered], [PackagingID], [OrderID] FROM [OrdersTbl] WHERE ";
      if (CustomerId == CustomersTbl.CONST_CUSTOMERID_GENERALOROTHER)
      {
        _sqlCmd += "([CustomerId] = " + CustomersTbl.CONST_STR_CUSTOMERID_GENERALOROTHER + ") AND ([RequiredByDate] = ?) AND ([Notes] = ?)";

        _TDB.AddWhereParams(DeliveryDate, DbType.Date, "@RequiredByDate");
        _TDB.AddWhereParams(Notes, DbType.String, "@Notes");
      }
      else
      {
        _sqlCmd += "([CustomerId] = ?) AND ([RequiredByDate] = ?)";
        _TDB.AddWhereParams(CustomerId, DbType.Int64, "@CustomerId");
        _TDB.AddWhereParams(DeliveryDate, DbType.Date, "@RequiredByDate");
      }
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          OrderDetailData od = new OrderDetailData();

          od.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : (Int32)_DataReader["ItemTypeID"];
          od.PackagingID = (_DataReader["PackagingID"] == DBNull.Value) ? 0 : (Int32)_DataReader["PackagingID"];
          od.OrderID = (Int32)_DataReader["OrderId"];   // this is the PK cannot be null
          od.QuantityOrdered = (_DataReader["QuantityOrdered"] == DBNull.Value) ? 1 : Math.Round(Convert.ToDouble(_DataReader["QuantityOrdered"]),2);

          oDetails.Add(od);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return oDetails;
    }
/*
      OleDbConnection _conn = new OleDbConnection(_connectionString);
      // custoemr ZZname = 9 is a general customer name so condition must be to that
      string _sqlCmd = "SELECT [ItemTypeID], [QuantityOrdered], [PackagingID], [OrderID] FROM [OrdersTbl] WHERE ";
      if (CustomerId == 9)
        _sqlCmd += "([CustomerId] = 9) AND ([RequiredByDate] = ?) AND ([Notes] = ?)";
      else
        _sqlCmd += "([CustomerId] = ?) AND ([RequiredByDate] = ?)";
      // attach the command
      OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
      if (CustomerId == 9)
      {
        _cmd.Parameters.Add(new OleDbParameter { Value = DeliveryDate });
        _cmd.Parameters.Add(new OleDbParameter { Value = Notes });
      }
      else
      {
        _cmd.Parameters.Add(new OleDbParameter { Value = CustomerId });
        _cmd.Parameters.Add(new OleDbParameter { Value = DeliveryDate });
      }
      try
      {
        _conn.Open();
        OleDbDataReader _DataReader = _cmd.ExecuteReader();
        while (_DataReader.Read())
        {
          OrderDetailData od = new OrderDetailData();

          od.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : (Int32)_DataReader["ItemTypeID"];
          od.PackagingID = (_DataReader["PackagingID"] == DBNull.Value) ? 0 : (Int32)_DataReader["PackagingID"];
          od.OrderID = (Int32)_DataReader["OrderId"];   // this is the PK cannot be null
          od.QuantityOrdered = (_DataReader["QuantityOrdered"] == DBNull.Value) ? 1 : Math.Round(Convert.ToDouble(_DataReader["QuantityOrdered"]),2);

          oDetails.Add(od);
        }
        _DataReader.Close();
      }
      catch (Exception _ex)
      {
        lastError = _ex.Message;
      }
      finally
      {
        _conn.Close();
      }
      return oDetails;
    }
 */
    /// <summary>
    /// Update Order Details, using the orderID update the line info.
    /// </summary>
    /// <param name="ItemTypeID"></param>
    /// <param name="QuantityOrdered"></param>
    /// <param name="PackagingID"></param>
    /// <param name="OrderID"></param>
    /// <returns></returns>
    public bool UpdateOrderDetails(Int64 OrderID, Int64 CustomerID, Int32 ItemTypeID, DateTime DeliveryDate,  double QuantityOrdered, Int32 PackagingID)
    {
      bool _Success = false;
      string _sqlCmd = "UPDATE OrdersTbl SET ItemTypeID = ?, QuantityOrdered = ?, PackagingID = ? WHERE (OrderId = ?)";
      TrackerDb _TDB = new TrackerDb();

      // check if the item is a group item then get the next group.
      TrackerTools _TT = new TrackerTools();
      ItemTypeID = _TT.ChangeItemIfGroupToNextItemInGroup(CustomerID, ItemTypeID, DeliveryDate);

      _TDB.AddParams(ItemTypeID, DbType.Int32, "@ItemTypeID");
      _TDB.AddParams(Math.Round(QuantityOrdered,2), DbType.Double, "@QuantityOrdered");
      _TDB.AddParams(PackagingID, DbType.Int32, "@PackagingID");
      _TDB.AddWhereParams(OrderID, DbType.Int64, "@OrderID");

      _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(_sqlCmd));
      _TDB.Close();

      return _Success;
    }
 /*
      try
      {
        _conn.Open();
        if (_cmd.ExecuteNonQuery() > 0)


      OleDbConnection _conn = new OleDbConnection(_connectionString);

       // add parameters in the order they appear in the update command
      OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
      _cmd.Parameters.Add(new OleDbParameter { Value = ItemTypeID });
      _cmd.Parameters.Add(new OleDbParameter { Value = Math.Round(QuantityOrdered,2) });
      _cmd.Parameters.Add(new OleDbParameter { Value = PackagingID });
      _cmd.Parameters.Add(new OleDbParameter { Value = OrderID });

      try
      {
        _conn.Open();
        if (_cmd.ExecuteNonQuery() > 0)
          return false;
      }
      catch (OleDbException ex)
      {
        string _ErrStr = ex.Message;
        return _ErrStr == "";
      }
      finally
      {
        _conn.Close();
      }

      return true;
    }
 */
    public bool InsertOrderDetails(Int64 CustomerID, DateTime OrderDate, DateTime RoastDate, Int32 ToBeDeliveredBy, 
                                   DateTime RequiredByDate, Boolean Confirmed, Boolean Done, String Notes, 
                                   double QuantityOrdered, Int32 PackagingID, Int32 ItemTypeID)
    {
      string _sqlCmd = "INSERT INTO OrdersTbl (CustomerId, OrderDate, RoastDate, RequiredByDate, ToBeDeliveredBy, Confirmed, Done, Notes, " +
                                              " ItemTypeID, QuantityOrdered, PackagingID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                                                                                //1  2  3  4  5  6  7  8  9  10 11
      bool _Success = false;

      // check if the item is a group item then get the next group.
      TrackerTools _TT = new TrackerTools();
      ItemTypeID = _TT.ChangeItemIfGroupToNextItemInGroup(CustomerID, ItemTypeID, RequiredByDate);
      TrackerDb _TDB = new TrackerDb();
      // first summary data
      _TDB.AddParams(CustomerID, DbType.Int64, "@CustomerID");
      _TDB.AddParams(OrderDate, DbType.Date, "@OrderDate");
      _TDB.AddParams(RoastDate, DbType.Date, "@RoastDate");
      _TDB.AddParams(RequiredByDate, DbType.Date, "@RequiredByDate");
      _TDB.AddParams(ToBeDeliveredBy, DbType.Int32, "@ToBeDeliveredBy");
      _TDB.AddParams(Confirmed, DbType.Boolean, "@Confirmed");
      _TDB.AddParams(Done, DbType.Boolean, "@Done");
      _TDB.AddParams(Notes, DbType.String, "@Notes");

      // Now line data
      _TDB.AddParams(ItemTypeID, DbType.Int32, "@ItemTypeID");
      _TDB.AddParams(Math.Round(QuantityOrdered, 2), DbType.Double, "@QuantityOrdered");
      _TDB.AddParams(PackagingID, DbType.Int32, "@PackagingID");

      _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(_sqlCmd));
      _TDB.Close();
      return _Success;
    }

/*
      OleDbConnection _conn = new OleDbConnection(_connectionString);                           

      // add parameters in the order they appear in the update command
      OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);
      // first summary data
      _cmd.Parameters.Add(new OleDbParameter { Value = CustomerID });
      _cmd.Parameters.Add(new OleDbParameter { Value = OrderDate });
      _cmd.Parameters.Add(new OleDbParameter { Value = RoastDate });
      _cmd.Parameters.Add(new OleDbParameter { Value = RequiredByDate });
      _cmd.Parameters.Add(new OleDbParameter { Value = ToBeDeliveredBy });
      _cmd.Parameters.Add(new OleDbParameter { Value = Confirmed });
      _cmd.Parameters.Add(new OleDbParameter { Value = Done });
      _cmd.Parameters.Add(new OleDbParameter { Value = Notes });

      // Now line data
      _cmd.Parameters.Add(new OleDbParameter { Value = ItemTypeID });
      _cmd.Parameters.Add(new OleDbParameter { Value = Math.Round(QuantityOrdered,2) });
      _cmd.Parameters.Add(new OleDbParameter { Value = PackagingID });

      try
      {
        _conn.Open();
        if (_cmd.ExecuteNonQuery() > 0)
          return false;
      }
      catch (OleDbException ex)
      {
        return ex.Message == "";        // Handle exception.
      }
      finally
      {
        _conn.Close();
      }

      return true;
    }
*/
    public bool DeleteOrderDetails(string OrderID)
    {
      string _sqlDeleteCmd = "DELETE FROM OrdersTbl WHERE (OrderID = ?)";
      bool _Success = false;
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(OrderID, DbType.String, "@OrderID");

      _Success = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(_sqlDeleteCmd));
      _TDB.Close();

      return _Success;
    }

/*
      OleDbConnection _conn = new OleDbConnection(_connectionString);
     // add parameters in the order they appear in the update command
      OleDbCommand _cmd = new OleDbCommand(_sqlDeleteCmd, _conn);
      _cmd.Parameters.Add(new OleDbParameter { Value = OrderID });

      try
      {
        _conn.Open();
        _Success = (_cmd.ExecuteNonQuery() > 0);
      }
      catch (OleDbException ex)
      {
        return ex.Message == "";
      }
      finally
      {
        _conn.Close();
      }

      return _Success;
    }
 */
  }
}