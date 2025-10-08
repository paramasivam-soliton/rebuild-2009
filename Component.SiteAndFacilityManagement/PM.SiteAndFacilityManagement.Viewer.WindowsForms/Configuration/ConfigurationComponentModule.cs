// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration.ConfigurationComponentModule
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationComponentModule : 
  ApplicationComponentModuleBase<ConfigurationManager, ConfigurationEditor>
{
  public ConfigurationComponentModule()
  {
    this.Id = new Guid("F3B13B79-2822-4202-8BFB-75204E63CAD0");
    this.Name = Resources.ConfigurationComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationComponentModule_ModuleShortcutName;
    this.Description = Resources.ConfigurationComponentModule_ModuleDescription;
    this.Controller = (IController<ConfigurationManager, ConfigurationEditor>) new ConfigurationController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SiteManage") as Bitmap);
  }
}
