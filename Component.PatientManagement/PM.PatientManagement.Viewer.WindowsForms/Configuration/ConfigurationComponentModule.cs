// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.Configuration.ConfigurationComponentModule
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationComponentModule : 
  ApplicationComponentModuleBase<PatientConfigManager, ConfigurationEditor>
{
  public ConfigurationComponentModule()
  {
    this.Id = new Guid("32F74D6A-3783-4611-B570-830C5B425B10");
    this.Name = Resources.ConfigurationComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationComponentModule_ShortcutName;
    this.Controller = (IController<PatientConfigManager, ConfigurationEditor>) new ConfigurationController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_PatientManage") as Bitmap);
  }
}
