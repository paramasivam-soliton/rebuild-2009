// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Configurator.ConfigurationEditorDpoaeComponentModule
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using PathMedical.DPOAE.Viewer.WindowsForms.Properties;
using PathMedical.InstrumentManagement;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Configurator;

public class ConfigurationEditorDpoaeComponentModule : 
  ApplicationComponentModuleBase<DpoaeConfigurationManager, ConfigurationEditorDpoaeMasterDetailBrowser>
{
  public ConfigurationEditorDpoaeComponentModule()
  {
    this.Id = new Guid("973E8BE1-68C1-46dd-97FA-491606536040");
    this.Name = Resources.ConfigurationEditorDpoaeComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationEditorDpoaeComponentModule_ModuelShortcutName;
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_DpoaeManage") as Bitmap);
    this.Controller = (IController<DpoaeConfigurationManager, ConfigurationEditorDpoaeMasterDetailBrowser>) new ConfigurationEditorDpoaeController((IApplicationComponentModule) this);
  }
}
