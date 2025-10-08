// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Viewer.WindowsForms.Viewer.TestViewerController
// Assembly: PM.TEOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83E8557F-96BD-4446-877A-A7FC2FA17A58
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.TEOAE.Viewer.WindowsForms.Viewer;

public class TestViewerController : Controller<TeoaeTestManager, TestViewer>
{
  public TestViewerController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = TeoaeTestManager.Instance;
    this.View = new TestViewer((IModel) this.Model);
  }
}
