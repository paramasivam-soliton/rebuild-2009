// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Configurator.ConfigurationEditorAbrComponentModule
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using PathMedical.ABR.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Configurator;

public class ConfigurationEditorAbrComponentModule : 
  ApplicationComponentModuleBase<AbrConfigurationManager, ConfigurationEditorAbrMasterDetailBrowser>
{
  public ConfigurationEditorAbrComponentModule()
  {
    this.Id = new Guid("E2C2D5CB-FA3E-4dbf-9E71-201B4F1D29D8");
    this.Name = Resources.ConfigurationEditorAbrComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationEditorAbrComponentModule_ModuleShortcutName;
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AbrConfigure") as Bitmap);
    this.Controller = (IController<AbrConfigurationManager, ConfigurationEditorAbrMasterDetailBrowser>) new ConfigurationEditorAbrController((IApplicationComponentModule) this);
  }
}
