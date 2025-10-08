// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourceManagerSetter
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

[DesignerSerializer(typeof (ResourceManagerSetterSerializer), typeof (CodeDomSerializer))]
public class ResourceManagerSetter : Component
{
  private IContainer components;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
