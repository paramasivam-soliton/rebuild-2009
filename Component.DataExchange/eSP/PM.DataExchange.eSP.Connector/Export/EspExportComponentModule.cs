// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.Export.EspExportComponentModule
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.DataExchange.eSP.Connector.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.Export;

public class EspExportComponentModule : 
  ApplicationComponentModuleBase<EspManager, EspExportAssistant>
{
  public EspExportComponentModule()
  {
    this.Id = new Guid("8A3D3DEC-7B6F-48e7-A6DC-41FA18075E09");
    this.Name = "Synchronize with eSP";
    this.ShortcutName = "eSP";
    this.Description = "Synchronize data with eSP";
    this.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_EspExport");
    this.Controller = (IController<EspManager, EspExportAssistant>) new EspExportController((IApplicationComponentModule) this);
  }
}
