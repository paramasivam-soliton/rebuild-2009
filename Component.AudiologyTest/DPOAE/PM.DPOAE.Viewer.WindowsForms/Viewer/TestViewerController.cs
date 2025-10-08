// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Viewer.TestViewerController
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Viewer;

public class TestViewerController : Controller<DpoaeTestManager, TestViewer>
{
  public TestViewerController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = DpoaeTestManager.Instance;
    this.View = new TestViewer((IModel) this.Model);
  }
}
