using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QOnT.Pages
{
  public partial class SupportTables : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    void BindItemsTableToGrid()
    {
      //ObjectDataSource _ItemsDataSource = new ObjectDataSource();

      //_ItemsDataSource.ID = "odsItemTbl";
      //_ItemsDataSource.TypeName = "TrackerDotNet.control.ItemTypeTbl";
      //_ItemsDataSource.SelectMethod = "GetAll";
      //_ItemsDataSource.SortParameterName = "SortBy";

      gvSupporTable.AutoGenerateEditButton = true;
      gvSupporTable.DataSourceID = "odsItemTypeTbl";
      gvSupporTable.DataBind();
      upnlSupporTables.Update();
      //gvSupporTable.DataSourceObject = new 

      //        DataSourceID="odsItemTypeTbl">
      //<asp:ObjectDataSource ID="odsItemTypeTbl" runat="server" SelectMethod="GetAll" 
      //  TypeName="TrackerDotNet.control.ItemTypeTbl">
      //  <SelectParameters>
      //    <asp:ControlParameter ControlID="gvSupporTable" DefaultValue="" Name="SortBy" 
      //      PropertyName="SelectedValue" Type="String" />
      //  </SelectParameters>
      //</asp:ObjectDataSource>

      //gvSupporTable.DataSource = "TrackerDotNet.control.ItemTypeTbl";
      //gvSupporTable.DataSourceObject = "TrackerDotNet.control.ItemTypeTbl";
    }
    protected void ddlTables_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (ddlTables.SelectedValue)
      {
        case "Items":
          BindItemsTableToGrid();
          break;
        default:
          break;
      }
    }
  }
}