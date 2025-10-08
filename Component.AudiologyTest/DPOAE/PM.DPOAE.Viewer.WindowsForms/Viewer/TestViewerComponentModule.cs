// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Viewer.TestViewerComponentModule
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Viewer;

public class TestViewerComponentModule : ApplicationComponentModuleBase<DpoaeTestManager, TestViewer>
{
  public TestViewerComponentModule()
  {
    this.Id = new Guid("D077211C-6AE5-4692-B10A-BD8BF1AD7537");
    this.Name = "DPOAE Detail Viewer";
    this.ShortcutName = "DPOAE";
    this.Controller = (IController<DpoaeTestManager, TestViewer>) new TestViewerController((IApplicationComponentModule) this);
    this.Image = (object) null;
  }
}
