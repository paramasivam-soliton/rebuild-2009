// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport.EspConfigurationImportComponentModule
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.DataExchange.eSP.Connector.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport;

public class EspConfigurationImportComponentModule : 
  ApplicationComponentModuleBase<EspManager, EspImportConfigurationAssistant>
{
  public EspConfigurationImportComponentModule()
  {
    this.Id = new Guid("AD34EA02-8D5D-4b52-BCC6-61F337D731B6");
    this.Name = "Import eSP configuration";
    this.ShortcutName = "eSP";
    this.Description = "Import configuration from eSP";
    this.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_EspImport");
    this.Controller = (IController<EspManager, EspImportConfigurationAssistant>) new EspConfigurationImportController((IApplicationComponentModule) this);
  }
}
