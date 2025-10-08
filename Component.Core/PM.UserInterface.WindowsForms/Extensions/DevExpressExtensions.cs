// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Extensions.DevExpressExtensions
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Extensions;

public static class DevExpressExtensions
{
  public static string GetCaption(this BaseEdit control)
  {
    string caption = (string) null;
    if (control != null && control.StyleController != null && control.StyleController is LayoutControl)
    {
      LayoutControlItem layoutControlItem = (control.StyleController as LayoutControl).Items.OfType<LayoutControlItem>().FirstOrDefault<LayoutControlItem>((Func<LayoutControlItem, bool>) (i => i.Control == control));
      if (layoutControlItem != null)
        caption = layoutControlItem.Text;
    }
    return caption;
  }

  public static string SetCaption(this BaseEdit control, string caption)
  {
    // ISSUE: variable of the null type
    __Null local = null;
    if (control == null)
      return (string) local;
    if (control.StyleController == null)
      return (string) local;
    if (!(control.StyleController is LayoutControl))
      return (string) local;
    LayoutControlItem layoutControlItem = (control.StyleController as LayoutControl).Items.OfType<LayoutControlItem>().FirstOrDefault<LayoutControlItem>((Func<LayoutControlItem, bool>) (i => i.Control == control));
    if (layoutControlItem == null)
      return (string) local;
    layoutControlItem.Text = caption;
    return (string) local;
  }

  public static bool GetIsActive(this BaseEdit control)
  {
    bool isActive = false;
    if (control != null && control.StyleController != null && control.StyleController is LayoutControl)
    {
      LayoutControlItem layoutControlItem = (control.StyleController as LayoutControl).Items.OfType<LayoutControlItem>().FirstOrDefault<LayoutControlItem>((Func<LayoutControlItem, bool>) (i => i.Control == control));
      if (layoutControlItem != null)
      {
        switch (layoutControlItem.Visibility)
        {
          case LayoutVisibility.Always:
            isActive = true;
            break;
          case LayoutVisibility.Never:
            isActive = false;
            break;
          case LayoutVisibility.OnlyInCustomization:
            isActive = true;
            break;
          case LayoutVisibility.OnlyInRuntime:
            isActive = true;
            break;
          default:
            isActive = true;
            break;
        }
      }
    }
    return isActive;
  }

  public static bool SetIsActive(this BaseEdit control, bool isActive)
  {
    int num = 0;
    if (control == null)
      return num != 0;
    if (control.StyleController == null)
      return num != 0;
    if (!(control.StyleController is LayoutControl))
      return num != 0;
    LayoutControlItem layoutControlItem = (control.StyleController as LayoutControl).Items.OfType<LayoutControlItem>().FirstOrDefault<LayoutControlItem>((Func<LayoutControlItem, bool>) (i => i.Control == control));
    if (isActive)
    {
      layoutControlItem.Visibility = LayoutVisibility.Always;
      layoutControlItem.ShowInCustomizationForm = true;
      return num != 0;
    }
    layoutControlItem.Visibility = LayoutVisibility.Never;
    layoutControlItem.ShowInCustomizationForm = false;
    return num != 0;
  }
}
