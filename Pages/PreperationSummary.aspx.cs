using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using QOnT.classes;

namespace QOnT.Pages
{
  public partial class PreperationSummary : System.Web.UI.Page
  {
    const string CONST_GROUPTTOTAL = "GroupTotal";
    const string CONST_LINENO = "LineNo";
    const string CONST_WEEKDESC = "WeekDesc";
    const int CONST_NUMWEEKS = 9;

    // creates a list of date of CONST_NUMWEEKS weeks before today and 2 weeks ahead
    protected List<DateTime> ListOfDatesOnDoW(DayOfWeek pDoW)
    {
      List<DateTime> dtList = new List<DateTime>();
      // get the closest date to the DayOfWeek past
      DateTime dtThis = DateTime.Now.AddDays((int)pDoW - (int)DateTime.Now.DayOfWeek).Date;
      // starting 7*4 weeks ago create a list of date starting on the Day of Week past
      for (int i = 0; i < CONST_NUMWEEKS+3; i++)
      {
        dtList.Add(dtThis.AddDays((7 * - CONST_NUMWEEKS) + (7 * i)));         
      }
      return dtList;
    }
    protected void ZeroViewStateVals()
    {
      ViewState[CONST_GROUPTTOTAL] = (double)0;  // zero group total
      ViewState[CONST_LINENO] = (int)1;  // Set line number to zero
    }
    protected DateTime GetFirstDoW(DateTime pDate)
    {
      int diff = pDate.DayOfWeek - DayOfWeek.Sunday;
      if (diff < 0)
        diff += 7;
        
      return pDate.AddDays(-1 * diff).Date;
    }
    protected DateTime GetLastDoW(DateTime pDate)
    {
      int diff = DayOfWeek.Saturday - pDate.DayOfWeek ;
      if (diff < 0)
        diff = 0;
        
      return pDate.AddDays(1 * diff).Date;
    }
    protected void ResetDates()
    {
      tbxDateFrom.Text = String.Format("{0:d}", GetFirstDoW(System.DateTime.Now));
      tbxDateTo.Text = String.Format("{0:d}", GetLastDoW(System.DateTime.Now));

      ZeroViewStateVals();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
/*
 * // get a list of dates and add them to the ddl
        List<DateTime> dtFrom = ListOfDatesOnDoW(DayOfWeek.Sunday);
        List<DateTime> dtTo = ListOfDatesOnDoW(DayOfWeek.Saturday);
        // now add From to the ddl
        foreach (DateTime dt in dtFrom)
        {
          ddlDateFrom.Items.Add(new ListItem(dt.ToLongDateString(), dt.ToShortDateString()));
        }
        // now add To to the ddl
        foreach (DateTime dt in dtTo)
        {
          ddlDateTo.Items.Add(new ListItem(dt.ToLongDateString(), dt.ToShortDateString()));
        }
        ddlDateFrom.SelectedIndex = CONST_NUMWEEKS;  // shoud be now
        ddlDateTo.SelectedIndex = CONST_NUMWEEKS;
*/
        ResetDates();

      }

    }

    protected void GoBtn_Click(object sender, EventArgs e)
    {
      // construct the string with the where clause as per above parameters.
      string _strSQL = "SELECT ItemTypeTbl.ItemDesc, ROUND(SUM(OrdersTbl.QuantityOrdered),2) AS Quantity" +
                       " FROM (OrdersTbl INNER JOIN ItemTypeTbl ON OrdersTbl.ItemTypeID = ItemTypeTbl.ItemTypeID)";

      bool _ByRoastDate = ddlFilterByRoastDate.SelectedValue.Equals("RoastDate");

      if (_ByRoastDate)
        _strSQL += " WHERE (OrdersTbl.RoastDate >= ?) AND (OrdersTbl.RoastDate <= ?) ";
      else
        _strSQL += "  WHERE (OrdersTbl.RequiredByDate >= ?) AND (OrdersTbl.RequiredByDate <= ?) ";
      _strSQL += " AND (ItemTypeTbl.ServiceTypeID = 2) GROUP BY ItemTypeTbl.ItemDesc";

      TrackerDb _TDB = new TrackerDb();
      DateTime _dtFrom = Convert.ToDateTime(tbxDateFrom.Text);    // (ddlDateFrom.SelectedValue);
      _TDB.AddWhereParams(_dtFrom, DbType.Date, "@RoastDateFrom");
      _TDB.AddWhereParams(Convert.ToDateTime(tbxDateTo.Text), DbType.Date, "@RoastDateTo"); // ddlDateTo.SelectedValue), DbType.Date, "@RoastDateTo");
      double _Dbl = _dtFrom.DayOfYear / 7;
      ViewState[CONST_WEEKDESC] = "Y" + _dtFrom.Year.ToString() + " Wk " + Convert.ToString(Math.Ceiling(_Dbl) + 1);
      ltrlDates.Text = String.Format("{0} - From: {1} to {2}", ((_ByRoastDate) ? "Roast Date" : "Prep Date"), tbxDateFrom.Text, tbxDateTo.Text); // ddlDateFrom.SelectedValue + " " + ddlDateTo.SelectedValue;
      // zero values
      ZeroViewStateVals();
      ;
      DataSet _ds = _TDB.ReturnDataSet(_strSQL);
      gvPreperationSummary.DataSource = _ds;
      gvPreperationSummary.DataBind();     
    }
    protected void gvPreperationSummary_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.Header)
      {
        Label _DescHdrLabel = (Label)e.Row.FindControl("lblDescHdr");
        _DescHdrLabel.Text = ViewState[CONST_WEEKDESC].ToString() + ":Ln1";
      }
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get all the values from the labels we have
        Label _ItemLabel = (Label)e.Row.FindControl("lblItemDesc");
        Label _QtyLabel = (Label)e.Row.FindControl("lblQty");
        Label _DescLabel = (Label)e.Row.FindControl("lblDescItem");
        // now calculate totals
        double _dblQty = Convert.ToDouble(_QtyLabel.Text);
        double _dblGroupTotal = (ViewState["GroupTotal"] == null) ? 0 : (double)ViewState["GroupTotal"];
        _dblGroupTotal += _dblQty;
        ViewState[CONST_GROUPTTOTAL] = _dblGroupTotal;
        // now populate the descriptiomn
        int _LineNo = (int)ViewState[CONST_LINENO];
        _DescLabel.Text = String.Format("{0}-Ln:{1}>{2}kgs of {3}", ViewState[CONST_WEEKDESC].ToString(), _LineNo, _dblQty, _ItemLabel.Text);
        _LineNo++;
        ViewState[CONST_LINENO] = _LineNo;
      }
      else if (e.Row.RowType == DataControlRowType.Footer)
      {
        double _dblGroupTotal = (ViewState["GroupTotal"] == null) ? 0 : (double)ViewState["GroupTotal"];
        Label _TotalQtyLabel = (Label)e.Row.FindControl("lblTotalQty");
        _TotalQtyLabel.Text = _dblGroupTotal.ToString();
      }
    }

    protected void ddlDateFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
      ZeroViewStateVals();  // zero totals
    }

    protected void ddlDateTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      ZeroViewStateVals();  // zero totals
    }

    protected void BackBtn_Click(object sender, EventArgs e)
    {
      ZeroViewStateVals();  // zero totals
      DateTime dtFrom = Convert.ToDateTime(tbxDateFrom.Text).AddDays(-7);
      DateTime dtTo = Convert.ToDateTime(tbxDateTo.Text).AddDays(-7);

      tbxDateFrom.Text = String.Format("{0:d}", dtFrom);
      tbxDateTo.Text = String.Format("{0:d}", dtTo);
      /*
      if (ddlDateFrom.SelectedIndex >= 1)
      {
        ZeroViewStateVals();  // zero totals
        ddlDateFrom.SelectedIndex--;
        ddlDateTo.SelectedIndex = ddlDateFrom.SelectedIndex;
        ddlDateFrom.DataBind();
        ddlDateTo.DataBind();
      }
       */
    }

    protected void ForwardBtn_Click(object sender, EventArgs e)
    {
      ZeroViewStateVals();  // zero totals
      DateTime dtFrom = Convert.ToDateTime(tbxDateFrom.Text).AddDays(7);
      DateTime dtTo = Convert.ToDateTime(tbxDateTo.Text).AddDays(7);

      tbxDateFrom.Text = String.Format("{0:d}", dtFrom);
      tbxDateTo.Text = String.Format("{0:d}", dtTo);
 
/*    if (ddlDateTo.SelectedIndex < (ddlDateTo.Items.Count - 1))
      {
        ZeroViewStateVals();  // zero totals
        ddlDateTo.SelectedIndex++;
        ddlDateFrom.SelectedIndex = ddlDateTo.SelectedIndex;
        ddlDateFrom.DataBind();
        ddlDateTo.DataBind();
      }
 */
    }

    protected void ResetBtn_Click(object sender, EventArgs e)
    {
      ResetDates();
     // udtpnlPrepSummary.Update();
    }
  }
}