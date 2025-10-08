// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Viewer.WindowsForms.Viewer.TestViewerComponentModule
// Assembly: PM.TEOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83E8557F-96BD-4446-877A-A7FC2FA17A58
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.TEOAE.Viewer.WindowsForms.Viewer;

public class TestViewerComponentModule : ApplicationComponentModuleBase<TeoaeTestManager, TestViewer>
{
  public TestViewerComponentModule()
  {
    this.Id = new Guid("073BB09F-02CE-47DA-A19F-3EF95DE08D2C");
    this.Name = "TEOAE Detail Viewer";
    this.ShortcutName = "TEOAE";
    this.Controller = (IController<TeoaeTestManager, TestViewer>) new TestViewerController((IApplicationComponentModule) this);
    this.Image = (object) null;
  }
}
