// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditorComponentModule
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor;

public class PatientEditorComponentModule : 
  ApplicationComponentModuleBase<PatientManager, PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor>
{
  public PatientEditorComponentModule()
  {
    this.Id = new Guid("6BC869E2-A4AA-43d1-9589-A0ABBC67B83C");
    this.Name = Resources.PatientEditorComponentModule_ModuleName;
    this.Controller = (IController<PatientManager, PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor>) new PatientEditorController((IApplicationComponentModule) this);
  }
}
