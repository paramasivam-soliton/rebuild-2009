// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.MultipleTestViewerComponent
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms;

public class MultipleTestViewerComponent : ApplicationComponentBase
{
  public static MultipleTestViewerComponent Instance
  {
    get => PathMedical.Singleton.Singleton<MultipleTestViewerComponent>.Instance;
  }

  private MultipleTestViewerComponent()
  {
    this.Name = Resources.MultipleTestViewerComponent_ComponentName;
    this.Description = Resources.MultipleTestViewerComponent_ComponentDescription;
    this.Fingerprint = new Guid("D1C72D61-1CD3-467e-9F41-0343B4AAFA76");
    this.ActiveModuleType = typeof (TestViewerComponentModule);
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<Resources>.Instance.ResourceManager);
  }
}
