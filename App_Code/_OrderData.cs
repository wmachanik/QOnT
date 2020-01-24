using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace TrackerDotNet.App_Code
{
  public class OrderData
  {
    const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_SELECTDISTINCTORDERS = "SELECT DISTINCT CustomersTbl.CompanyName, OrdersTbl.CustomerId As CustomerId, OrdersTbl.OrderDate, OrdersTbl.RoastDate, " +
                                              " OrdersTbl.RequiredByDate, PersonsTbl.Person, OrdersTbl.Confirmed, OrdersTbl.Done " +
                                              " FROM ((OrdersTbl LEFT OUTER JOIN PersonsTbl ON OrdersTbl.ToBeDeliveredBy = PersonsTbl.PersonID)" +
                                              " LEFT OUTER JOIN CustomersTbl ON OrdersTbl.CustomerId = CustomersTbl.CustomerID)" +
                                              " WHERE (OrdersTbl.Done = ?)";
    const string CONST_UPDATEORDERDATES = "UPDATE OrdersTbl SET RoastDate=? WHERE CustomerId=? AND OrderDate=?";

    private string _connectionString;

    public OrderData()
    {
      Initialize();
    }

    //////////
    // Verify that only valid columns are specified in the sort expression to avoid a SQL Injection attack.

    private void VerifySortColumns(string sortColumns)
    {
      if (sortColumns.ToLowerInvariant().EndsWith(" desc"))
        sortColumns = sortColumns.Substring(0, sortColumns.Length - 5);

      string[] columnNames = sortColumns.Split(',');

      foreach (string columnName in columnNames)
      {
        switch (columnName.Trim().ToLowerInvariant())
        {
          case "":
            break;
          default:
            throw new ArgumentException("SortColumns contains an invalid column name.");
            break;
        }
      }
    }


    public void Initialize()
    {
      // Initialize data source. Use "Tracker08" connection string from configuration.

      if (ConfigurationManager.ConnectionStrings[CONST_CONSTRING] == null ||
          ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString.Trim() == "")
      {
        throw new Exception("A connection string named "+ CONST_CONSTRING +" with a valid connection string " +
                            "must exist in the <connectionStrings> configuration section for the application.");
      }
      _connectionString =
        ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;
    }
    
    public DataTable GetDistinctOrders(bool OrderDone, string SearchFor, string SearchValue)
    {
      //VerifySortColumns(sortColumns);

      string sqlCmd = CONST_SELECTDISTINCTORDERS;
      
      // check the search for value, to see if we must add items to the where portion of the clause
      if ((SearchFor != "none") && (SearchFor.Length > 0))
      {
        switch (SearchFor) { 
          case "Company" :
            sqlCmd += " AND CustomersTbl.CompanyName LIKE '%" + SearchValue + "%'";
            break;
          case "PrepDate" :
            sqlCmd += " AND OrdersTbl.RoastDate= #" + SearchValue + "#";
            break;
          default:
            break;
        }
      }

      //if (sortColumns.Trim() == "")
      //  sqlCmd += "ORDER BY RoastDate";
      //else
      //  sqlCmd += "ORDER BY " + sortColumns;

      OleDbConnection conn = new OleDbConnection(_connectionString);
      OleDbDataAdapter da = new OleDbDataAdapter(sqlCmd, conn);
      da.SelectCommand.Parameters.Add("@Done", OleDbType.Boolean).Value = OrderDone;

      DataSet ds = new DataSet();

      try
      {
        conn.Open();

        da.Fill(ds, "Orders");
      }
      catch (OleDbException e)
      {
        // Handle exception.
      }
      finally
      {
        conn.Close();
      }

      return ds.Tables["Orders"];
    }

    public int UpdateOrderRoastDate(DateTime RoastDate)
    {
      OleDbConnection conn = new OleDbConnection(_connectionString);
      OleDbCommand cmd = new OleDbCommand(CONST_UPDATEORDERDATES, conn);
      cmd.Parameters.Add("@RoastDate", OleDbType.Date).Value = RoastDate;

      int result = 0;

      try
      {
        conn.Open();
        result = cmd.ExecuteNonQuery();
      }
      catch (OleDbException e)
      {
        // Handle exception.
      }
      finally
      {
        conn.Close();
      }

      return result;
    }

  }
}