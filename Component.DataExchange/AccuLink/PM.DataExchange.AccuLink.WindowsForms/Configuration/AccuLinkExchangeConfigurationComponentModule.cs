// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.WindowsForms.Configuration.AccuLinkExchangeConfigurationComponentModule
// Assembly: PM.DataExchange.AccuLink.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B9FC5AD-6EE7-4FA1-8083-412FCFB9EB4F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.WindowsForms.dll

using PathMedical.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DataExchange.AccuLink.WindowsForms.Configuration;

internal class AccuLinkExchangeConfigurationComponentModule : 
  ApplicationComponentModuleBase<AccuLinkDataExchangeConfigurationManager, AccuLinkDataExchangeConfigurationEditor>
{
  public AccuLinkExchangeConfigurationComponentModule()
  {
    this.Id = new Guid("CCA603A1-70C2-49cc-8AD8-FA43340BEC3E");
    this.Name = "AccuLink Data Exchange";
    this.ShortcutName = "AccuLink";
    this.Description = "Configure AccuLink Data Exchange";
    this.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("???");
    this.Controller = (IController<AccuLinkDataExchangeConfigurationManager, AccuLinkDataExchangeConfigurationEditor>) new AccuLinkDataExchangeConfigurationController((IApplicationComponentModule) this);
  }
}
