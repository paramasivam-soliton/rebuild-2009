// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration.ConfigurationComponentModule
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationComponentModule : 
  ApplicationComponentModuleBase<ConfigurationManager, ConfigurationEditor>
{
  public ConfigurationComponentModule()
  {
    this.Id = new Guid("DD93D1F6-4FDB-4673-88BC-E8AC59E61493");
    this.Name = Resources.ConfigurationComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationComponentModule_ModuleShortcutName;
    this.Description = Resources.ConfigurationComponentModule_ModuleDescription;
    this.Controller = (IController<ConfigurationManager, ConfigurationEditor>) new ConfigurationController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Profiles") as Bitmap);
  }
}
