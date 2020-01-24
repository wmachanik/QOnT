<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="_ItemGroups.aspx.cs"
  Inherits="TrackerDotNet.Pages.ItemGroups" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="cntItemGroupsHdr" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="cntItemGroupsBdy" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="scmLookup" runat="server">
  </ajaxToolkit:ToolkitScriptManager>
  <asp:UpdateProgress runat="server" ID="updtPrglItems" AssociatedUpdatePanelID="updtPnllItems">
    <ProgressTemplate>
      Please Wait&nbsp;<img src="../images/animi/QuaffeeProgress.gif" alt="Please Wait..." />&nbsp;...
    </ProgressTemplate>
  </asp:UpdateProgress>
  <h2>Item Group Tables...</h2>
  <asp:UpdatePanel ID="updtPnllItems" runat="server">
    <ContentTemplate>
      Item Group: <asp:DropDownList ID="ddlGroupItems" runat="server" DataSourceID="odsItemGroups" DataTextField="ItemDesc"
         DataValueField="ItemTypeID" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupItems_SelectedIndexChanged">
        <asp:ListItem Text="--Please add a group--" Value="-1" />
      </asp:DropDownList>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Button ID="btnAddGroup" runat="server" Text="Add a group" OnClick="btnAddGroup_Click" />
      <br /><br />
      <table class="TblWhite">
        <thead>
          <tr>
            <th>Items to add</th>
            <th>&nbsp;</th>
            <th>Items in group</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>gridview of items no in group</td>
            <td class="rowC">
              <asp:Button ID="btnAddItem" runat="server" Text="Add->" OnClick="btnAddItem_Click" /><br />
              <asp:Button ID="btnRemove" runat="server" Text="<-Remove" />
            </td>
            <td>
              list of items in group
            </td>
          </tr>
        </tbody>
      </table>
      
      <br />
      <br />

      
    </ContentTemplate>
  </asp:UpdatePanel>
  <asp:ObjectDataSource ID="odsItemGroups" runat="server" TypeName="TrackerDotNet.control.ItemTypeTbl" 
    SelectMethod="GetAllGroupTypeItems" OldValuesParameterFormatString="original_{0}">
  </asp:ObjectDataSource>
  <%--
    <asp:ObjectDataSource ID="odsItemGroup" runat="server" TypeName="TrackerDotNet.control.ItemGroupTbl"
    DataObjectTypeName="TrackerDotNet.control.ItemGroupTbl" SelectMethod="GetAll" SortParameterName="SortBy"
    UpdateMethod="UpdateItemGroup" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertItemGroup" DeleteMethod="DeleteItemGroup">
    <DeleteParameters>
      <asp:Parameter Name="pItemGroupID" Type="Int32" />
    </DeleteParameters>
    <SelectParameters>
      <asp:Parameter DefaultValue="GroupItemTypeID" Name="SortBy" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
    --%>
</asp:Content>
