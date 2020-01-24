using System;
using System.Collections.Generic;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class OrderDataControl
  {

//      "RequiredByDate= ?, ToBeDeliveredBy= ?, Confirmed= ?, Done= ?, InvoiceDone = ?, PurchaseOrder = ?, Notes = ?";  // Where added later WHERE (OrderId = ?)";
    // constants
    public const string CONST_ORDERUPDATEHEADER_SQL = "UPDATE OrdersTbl SET CustomerId = ?, OrderDate= ?, RoastDate= ?, ToBeDeliveredBy= ?," +
      " RequiredByDate = ?, Confirmed= ?, Done= ?, InvoiceDone = ?, PurchaseOrder = ?, Notes = ?";  // Where added later WHERE (OrderId = ?)";
    const string CONST_ORDERUPDATEITEMS_SQL = "UPDATE OrdersTbl SET ItemTypeID = ?, QuantityOrdered = ?, PackagingID = ? WHERE (OrderId = ?)";
    const string CONST_ORDERUPDATEALL_SQL = "UPDATE OrdersTbl SET CustomerId = ?, OrderDate= ?, RoastDate= ?, RequiredByDate= ?, ToBeDeliveredBy= ?, Confirmed= ?, Done= ?, InvoiceDone = ?, PurchaseOrder = ?, Notes = ?, ItemTypeID = ?, QuantityOrdered = ?, PackagingID = ? WHERE (OrderId = ?)";


    public OrderDataControl()
    {
    }

    /// <summary>
    /// Execute the SQL statement does not return results, such as: delete, update, insert operation 
    /// </summary>
    /// <param name="strSQL">SQL String of a non Query Type</param>
    /// <returns>success or failure</returns>
    public bool UpdateOrderHeader(OrderHeaderData pOrderHeader, List<string> pOrders) 
    {
      bool _resultState = false;
      string _strSQL = CONST_ORDERUPDATEHEADER_SQL + " WHERE ";

      // for all the OrderIds passed create a where clause
      for (int i = 0; i < pOrders.Count-1; i++)
      {
        _strSQL += " OrderID = "+pOrders[i] + " OR";
      }
      _strSQL += " OrderID = " + pOrders[pOrders.Count-1];

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddParams(pOrderHeader.CustomerID, DbType.Int64, "@CustomerID");
      _TDB.AddParams(pOrderHeader.OrderDate, DbType.Date, "@OrderDate");
      _TDB.AddParams(pOrderHeader.RoastDate, DbType.Date, "@RoastDate");
      _TDB.AddParams(pOrderHeader.ToBeDeliveredBy, DbType.Int64, "@ToBeDeliveredBy");
      _TDB.AddParams(pOrderHeader.RequiredByDate, DbType.Date, "@RequiredByDate");
      _TDB.AddParams(pOrderHeader.Confirmed, DbType.Boolean, "@Confirmed");
      _TDB.AddParams(pOrderHeader.Done, DbType.Boolean, "@Done");
      _TDB.AddParams(pOrderHeader.InvoiceDone, DbType.Boolean, "@InvoiceDone");
      _TDB.AddParams(pOrderHeader.PurchaseOrder, DbType.String, "@PurchaseOrder");
      _TDB.AddParams(pOrderHeader.Notes, DbType.String, "@Notes");

      _resultState = String.IsNullOrEmpty(_TDB.ExecuteNonQuerySQL(_strSQL));
      
      _TDB.Close();


      return _resultState;
    }
  
/*
      _TrackerDbConn.Open();
      OleDbTransaction _myTrans = _TrackerDbConn.BeginTransaction();
      // UPDATE order CustomerId = ?, OrderDate= ?, RoastDate= ?, RequiredByDate= ?, ToBeDeliveredBy= ?, Confirmed= ?, Done= ?, Notes = ? WHERE (OrderId = ?)";
      OleDbCommand _command = new OleDbCommand(_strSQL, _TrackerDbConn, _myTrans);
      // add parameters in the order of the SQL command
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.CustomerID });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.OrderDate });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.RoastDate });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.RequiredByDate });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.ToBeDeliveredBy });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.Confirmed });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.Done });
      _command.Parameters.Add(new OleDbParameter { Value = pOrderHeader.Notes });
//      _command.Parameters.Add(new OleDbParameter { Value =pOrderId});

      try 
      { 
        _command.ExecuteNonQuery (); 
        _myTrans.Commit (); 
        _resultState = true; 
      } 
      catch 
      { 
        _myTrans.Rollback (); 
        _resultState = false; 
      } 
      finally 
      { 
        _TrackerDbConn.Close (); 
      } 
      return _resultState; 
    }
*/
  }
}