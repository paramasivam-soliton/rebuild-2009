// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Viewer.TestViewerController
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Viewer;

public class TestViewerController : Controller<AbrTestManager, TestViewer>
{
  public TestViewerController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = AbrTestManager.Instance;
    this.View = new TestViewer((IModel) this.Model);
  }
}
