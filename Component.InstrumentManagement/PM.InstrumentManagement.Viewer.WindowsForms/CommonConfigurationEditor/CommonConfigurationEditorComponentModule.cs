// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Viewer.WindowsForms.CommonConfigurationEditor.ConfigurationComponentModule
// Assembly: PM.InstrumentManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 377C3581-C34D-4673-97B7-CC091DEDB55A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Viewer.WindowsForms.dll

using PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser;
using PathMedical.InstrumentManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.InstrumentManagement.Viewer.WindowsForms.CommonConfigurationEditor;

public class ConfigurationComponentModule : 
  ApplicationComponentModuleBase<InstrumentManager, InstrumentMasterDetailBrowser>
{
  public ConfigurationComponentModule()
  {
    this.Id = new Guid("D323F8C9-4108-4461-A01C-A0B1391BC3C9");
    this.Name = Resources.ConfigurationComponentModule_ModuleName;
    this.Controller = (IController<InstrumentManager, InstrumentMasterDetailBrowser>) new InstrumentBrowserController((IApplicationComponentModule) this);
  }
}
