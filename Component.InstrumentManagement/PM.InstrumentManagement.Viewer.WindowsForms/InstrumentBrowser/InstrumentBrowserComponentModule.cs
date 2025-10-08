// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser.InstrumentBrowserComponentModule
// Assembly: PM.InstrumentManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 377C3581-C34D-4673-97B7-CC091DEDB55A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Viewer.WindowsForms.dll

using PathMedical.InstrumentManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser;

public class InstrumentBrowserComponentModule : 
  ApplicationComponentModuleBase<InstrumentManager, InstrumentMasterDetailBrowser>
{
  public InstrumentBrowserComponentModule()
  {
    this.Id = new Guid("540EA042-4E79-4813-953B-FACC702955F1");
    this.Name = Resources.InstrumentBrowserComponentModule_ModuleName;
    this.Controller = (IController<InstrumentManager, InstrumentMasterDetailBrowser>) new InstrumentBrowserController((IApplicationComponentModule) this);
  }
}
