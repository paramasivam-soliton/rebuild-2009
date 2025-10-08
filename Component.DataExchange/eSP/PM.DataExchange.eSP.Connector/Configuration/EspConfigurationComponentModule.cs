// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration.EspConfigurationComponentModule
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.DataExchange.eSP.Connector.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration;

public class EspConfigurationComponentModule : 
  ApplicationComponentModuleBase<EspConfigurationManager, EspConfigurationEditor>
{
  public EspConfigurationComponentModule()
  {
    this.Id = new Guid("5291AAB0-3DCE-44cf-BA34-55B8ADE5C7F3");
    this.Name = "Configure eSP";
    this.ShortcutName = "eSP";
    this.Description = "Configure eSP synchronization";
    this.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_EspExport");
    this.Controller = (IController<EspConfigurationManager, EspConfigurationEditor>) new EspConfigurationController((IApplicationComponentModule) this);
  }
}
