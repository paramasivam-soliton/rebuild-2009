// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.SelectionChangingEventArgs
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls;

public class SelectionChangingEventArgs : EventArgs
{
  public string OldDisplayText { get; private set; }

  public object OldValue { get; private set; }

  public object OldObject { get; private set; }

  public string NewDisplayText { get; private set; }

  public object NewValue { get; private set; }

  public object NewObject { get; private set; }

  public SelectionChangingEventArgs(
    string oldDisplayText,
    object oldValue,
    object oldObject,
    string newDisplayText,
    object newValue,
    object newObject)
  {
    this.OldDisplayText = oldDisplayText;
    this.OldValue = oldValue;
    this.OldObject = oldObject;
    this.NewDisplayText = newDisplayText;
    this.NewValue = newValue;
    this.NewObject = newObject;
  }
}
