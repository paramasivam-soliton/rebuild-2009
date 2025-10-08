// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Viewer.WindowsForms.SystemConfigurationComponent
// Assembly: PM.SystemConfiguration.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D6114BC-3807-4057-97EB-FB0AA393F8AD
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Viewer.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser;
using PathMedical.SystemConfiguration.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration.Viewer.WindowsForms;

public class SystemConfigurationComponent : ApplicationComponentBase
{
  public static SystemConfigurationComponent Instance
  {
    get => PathMedical.Singleton.Singleton<SystemConfigurationComponent>.Instance;
  }

  public new static int LoadPriority { get; set; }

  private SystemConfigurationComponent()
  {
    this.Name = Resources.SystemConfigurationComponent_ComponentName;
    this.Description = Resources.SystemConfigurationComponent_ComponentDescription;
    this.Fingerprint = new Guid("2FD02C7C-E616-4a89-AAF0-E455C5928C74");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<Resources>.Instance.ResourceManager);
    this.ActiveModuleType = typeof (ComponentBrowserComponentModule);
    SystemConfigurationComponent.LoadPriority = int.MaxValue;
  }
}
