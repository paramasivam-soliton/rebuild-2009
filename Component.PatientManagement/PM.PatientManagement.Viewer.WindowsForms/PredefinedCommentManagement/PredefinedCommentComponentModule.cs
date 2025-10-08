// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement.PredefinedCommentComponentModule
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement;

public class PredefinedCommentComponentModule : 
  ApplicationComponentModuleBase<CommentManager, PredefinedCommentMasterDetailBrowser>
{
  public PredefinedCommentComponentModule()
  {
    this.Id = new Guid("24C5098D-46DF-4485-BCC7-B068C41B0272");
    this.Name = Resources.PredefinedCommentComponentModule_ModuleName;
    this.ShortcutName = Resources.PredefinedCommentComponentModule_ShortcutName;
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_CommentConfiguration") as Bitmap);
    this.Controller = (IController<CommentManager, PredefinedCommentMasterDetailBrowser>) new PredefinedCommentMaintenanceController((IApplicationComponentModule) this);
  }
}
