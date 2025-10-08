// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewerComponentModule
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer;

public class TestViewerComponentModule : 
  ApplicationComponentModuleBase<WaveFormDataManager, PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer>
{
  public TestViewerComponentModule()
  {
    this.Id = new Guid("BC06BBBF-C4D1-406f-9052-E84054DFE880");
    this.Name = Resources.TestViewerComponentModule_ModuleName;
    this.Controller = (IController<WaveFormDataManager, PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer>) new TestViewerController((IApplicationComponentModule) this);
  }
}
