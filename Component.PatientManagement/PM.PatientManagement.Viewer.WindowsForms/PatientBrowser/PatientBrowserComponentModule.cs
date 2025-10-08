// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.PatientBrowserComponentModule
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser;

public class PatientBrowserComponentModule : 
  ApplicationComponentModuleBase<PatientManager, PatientTestBrowser>
{
  public PatientBrowserComponentModule()
  {
    this.Id = new Guid("A9FAAEF7-0076-438f-AED3-6DC078FFB1C5");
    this.Name = Resources.PatientBrowserComponentModule_ModuleName;
    this.Controller = (IController<PatientManager, PatientTestBrowser>) new PatientBrowserController((IApplicationComponentModule) this);
  }
}
