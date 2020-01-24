using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QOnT.control
{
  public class OrderDetailData
  {
    // ItemTypeID, QuantityOrdered, PrepTypeID and OrderID 
    public OrderDetailData()
    {
      _otItemTypeID = 0;
      _otOrderID = 0;
      _otPackagingID = 0;
      _otQuantityOrdered = 0.00;
    }

    private int _otItemTypeID;
    private long _otOrderID;
    private int _otPackagingID;
    private double _otQuantityOrdered;

    public int ItemTypeID { get { return _otItemTypeID; } set { _otItemTypeID = value; } }
    public int PackagingID { get { return _otPackagingID; } set { _otPackagingID = value; } }
    public long OrderID { get { return _otOrderID; } set { _otOrderID = value; } }
    public double QuantityOrdered { get { return _otQuantityOrdered; } set { _otQuantityOrdered = value; } }

    // not used
    //public long CustomerID { get ; set ; }
    //public DateTime RoastDate { get; set; } }
  }
}