// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ComboBoxEditItemWrapper
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.ResourceManager;
using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class ComboBoxEditItemWrapper
{
  public ComboBoxEditItemWrapper([Localizable(true)] string displayName, object item)
  {
    this.Name = displayName;
    this.Value = item;
  }

  public ComboBoxEditItemWrapper(Enum enumObject)
  {
    string str = GlobalResourceEnquirer.Instance.GetResourceByName(enumObject) as string;
    if (string.IsNullOrEmpty(str))
      str = Enum.GetName(enumObject.GetType(), (object) enumObject);
    this.Name = str;
    this.Value = (object) enumObject;
  }

  public ComboBoxEditItemWrapper(bool value)
  {
    string resourceByName = GlobalResourceEnquirer.Instance.GetResourceByName($"{typeof (bool).FullName}.{value}".Replace(".", "__")) as string;
    if (string.IsNullOrEmpty(resourceByName))
      resourceByName = value.ToString();
    this.Name = resourceByName;
    this.Value = (object) value;
  }

  public string Name { get; protected set; }

  public object Value { get; protected set; }

  public override string ToString() => this.Name;
}
