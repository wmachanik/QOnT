using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrackerDotNet
{
  public partial class SiteMaster : System.Web.UI.MasterPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      // get page name for relative path
      string strThingPage = Page.AppRelativeVirtualPath;
      Int32 intSlashPos = strThingPage.IndexOf("/");
      string strPageName = "~"+strThingPage.Substring(intSlashPos, strThingPage.Length - intSlashPos).ToUpper();

      // Select menu item with matching NavigateUrl property
      foreach (MenuItem mnuiParent in NavigationMenu.Items)
      {
        if (mnuiParent.NavigateUrl.ToUpper() == strPageName)
        {
          mnuiParent.Selected = true;
          mnuiParent.Text = ">" + mnuiParent.Text + "<";
          mnuiParent.Enabled = false;
        }
        else
          foreach (MenuItem mnuiChild in mnuiParent.ChildItems)
          {
            if (mnuiChild.NavigateUrl.ToUpper() == strPageName)
            {
              mnuiChild.Selected = true;
              mnuiChild.Text = ">" + mnuiChild.Text + "<";
              mnuiChild.Enabled = false;
            }
          }
      }


      //  from http://www.maconstateit.net/tutorials/aspnet20/ASPNET12/aspnet12-02.aspx
  //      '-- Get page name from relative path
  //Dim ThisPage As String = Page.AppRelativeVirtualPath
  //Dim SlashPos As Integer = InStrRev(ThisPage,"/")
  //Dim PageName As String = Right(ThisPage, Len(ThisPage) - SlashPos)

  //'-- Select menu item with matching NavigateUrl property
  //Dim ParentMenu As MenuItem
  //Dim ChildMenu As MenuItem
  //For Each ParentMenu in NavigationMenu.Items
  //  If ParentMenu.NavigateUrl = PageName Then
  //    ParentMenu.Selected = True
  //  Else
  //    For Each ChildMenu in ParentMenu.ChildItems
  //      If ChildMenu.NavigateUrl = PageName Then
  //        ChildMenu.Selected = True
  //      End If
  //    Next
  //  End If
  //Next
    
    }
  }
}
