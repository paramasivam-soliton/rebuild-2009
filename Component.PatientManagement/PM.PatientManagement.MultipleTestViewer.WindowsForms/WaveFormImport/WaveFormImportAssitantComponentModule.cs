// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport.WaveFormImportAssitantComponentModule
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport;

[Obsolete]
public class WaveFormImportAssitantComponentModule : 
  ApplicationComponentModuleBase<WaveFormDataManager, WaveFormImportAssistent>
{
  public WaveFormImportAssitantComponentModule()
  {
    this.Id = new Guid("50C7F0BF-5DD0-4da9-96A8-D5D0B82645D2");
    this.Name = Resources.WaveFormImportAssitantComponentModule_ModuleName;
    this.Controller = (IController<WaveFormDataManager, WaveFormImportAssistent>) new WaveFormImportController((IApplicationComponentModule) this);
  }
}
