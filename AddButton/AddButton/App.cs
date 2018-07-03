#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using adWin = Autodesk.Windows;
using System.Diagnostics;
using System.IO;
#endregion

namespace AddButton
{
  class App : IExternalApplication
  {
    public Result OnStartup( UIControlledApplication a )
    {
      adWin.RibbonControl ribbon
        = adWin.ComponentManager.Ribbon;

      foreach( adWin.RibbonTab tab in ribbon.Tabs )
      {
        if( tab.Id == "Analyze" )
        {
          foreach( adWin.RibbonPanel panel
            in tab.Panels )
          {
            if( panel.Source.Id == "cea_shr" )
            {
              adWin.RibbonButton button
                = new adWin.RibbonButton();

              button.Name = "TbcButtonName";
              //button.Image = image;
              //button.LargeImage = image;
              button.Id = "ID_TBC_BUTTON";
              button.AllowInStatusBar = true;
              button.AllowInToolBar = true;
              button.GroupLocation = Autodesk.Private
                .Windows.RibbonItemGroupLocation.Middle;
              button.IsEnabled = true;
              button.IsToolTipEnabled = true;
              button.IsVisible = true;
              button.ShowImage = false; //  true;
              button.ShowText = true;
              button.ShowToolTipOnDisabled = true;
              button.Text = "The Building Coder";
              button.ToolTip = "Open The Building "
                + "Coder blog on the Revit API";
              button.MinHeight = 0;
              button.MinWidth = 0;
              button.Size = adWin.RibbonItemSize.Large;
              button.ResizeStyle = adWin
                .RibbonItemResizeStyles.HideText;
              button.IsCheckable = true;
              button.Orientation = System.Windows
                .Controls.Orientation.Vertical;
              button.KeyTip = "TBC";

              adWin.ComponentManager.UIElementActivated
                += new EventHandler<
                  adWin.UIElementActivatedEventArgs>(
                  ComponentManager_UIElementActivated );

              panel.Source.Items.Add( button );

              return Result.Succeeded;
            }
          }
        }
      }
      return Result.Succeeded;
    }

    void ComponentManager_UIElementActivated(
      object sender,
      adWin.UIElementActivatedEventArgs e )
    {
      if( e != null
        && e.Item != null
        && e.Item.Id != null
        && e.Item.Id == "ID_TBC_BUTTON" )
      {
        // Perform the button action

        // Local file

        string path = System.Reflection.Assembly
          .GetExecutingAssembly().Location;

        path = Path.Combine(
          Path.GetDirectoryName( path ),
          "test.html" );

        // Internet URL

        path = "http://thebuildingcoder.typepad.com";

        Process.Start( path );
      }
    }

    public Result OnShutdown( UIControlledApplication a )
    {
      return Result.Succeeded;
    }
  }
}
