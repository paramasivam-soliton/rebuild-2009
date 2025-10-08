// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Viewer.TestViewerComponentModule
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Viewer;

public class TestViewerComponentModule : ApplicationComponentModuleBase<AbrTestManager, TestViewer>
{
  public TestViewerComponentModule()
  {
    this.Id = new Guid("DE3913A2-D0F2-4869-AEC3-052384189549");
    this.Name = "ABR Detail Viewer";
    this.ShortcutName = "ABR";
    this.Controller = (IController<AbrTestManager, TestViewer>) new TestViewerController((IApplicationComponentModule) this);
    this.Image = (object) null;
  }
}
