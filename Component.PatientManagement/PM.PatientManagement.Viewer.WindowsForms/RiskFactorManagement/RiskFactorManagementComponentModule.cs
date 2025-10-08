// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.RiskFactorManagement.RiskFactorManagementComponentModule
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.RiskFactorManagement;

public class RiskFactorManagementComponentModule : 
  ApplicationComponentModuleBase<RiskIndicatorManager, RiskFactorMasterDetailBrowser>
{
  public RiskFactorManagementComponentModule()
  {
    this.Id = new Guid("D330A4E3-F6CF-4e8b-A925-F156A50D5153");
    this.Name = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskFactorManagementComponentModule_ModuleName");
    this.ShortcutName = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskFactorManagementComponentModule_ShortcutName");
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskManage") as Bitmap);
    this.Controller = (IController<RiskIndicatorManager, RiskFactorMasterDetailBrowser>) new RiskFactorManagementController((IApplicationComponentModule) this);
  }
}
