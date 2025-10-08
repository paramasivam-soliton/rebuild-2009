// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeConfigurator.ProbeConfiguratorComponentModule
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Type1077;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeConfigurator;

internal class ProbeConfiguratorComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, ProbeConfiguratorEditor>
{
  public ProbeConfiguratorComponentModule()
  {
    this.Id = new Guid("1C006CFB-0567-454c-8623-BFB1C8AAF2ED");
    this.Name = "Service Tool";
    this.ShortcutName = "Service";
    this.Controller = (IController<Type1077Manager, ProbeConfiguratorEditor>) new ProbeConfiguratorController((IApplicationComponentModule) this);
  }
}
