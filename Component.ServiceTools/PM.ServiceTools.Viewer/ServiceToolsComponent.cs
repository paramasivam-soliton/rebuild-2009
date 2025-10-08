// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ServiceToolsComponent
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.ResourceManager;
using PathMedical.ServiceTools.WindowsForms.ProbeConfigurator;
using PathMedical.ServiceTools.WindowsForms.Properties;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms;

internal class ServiceToolsComponent : ApplicationComponentBase
{
  public static ServiceToolsComponent Instance => PathMedical.Singleton.Singleton<ServiceToolsComponent>.Instance;

  private ServiceToolsComponent()
  {
    this.Name = "Service Tools";
    this.Description = "Service tools to manage probes and instruments";
    this.Fingerprint = new Guid("0C138E27-B9D5-4a7a-AF02-7E9AB34CEEDB");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<Resources>.Instance.ResourceManager);
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<Resources>.Instance.ResourceManager);
    this.ConfigurationModuleTypes = (Type[]) null;
    this.ActiveModuleType = typeof (ProbeConfiguratorComponentModule);
  }
}
