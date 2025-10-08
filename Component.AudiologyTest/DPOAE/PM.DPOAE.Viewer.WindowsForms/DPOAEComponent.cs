// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.DpoaeComponent
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using PathMedical.AudiologyTest;
using PathMedical.DPOAE.Properties;
using PathMedical.DPOAE.Viewer.WindowsForms.Configurator;
using PathMedical.DPOAE.Viewer.WindowsForms.Report;
using PathMedical.DPOAE.Viewer.WindowsForms.Viewer;
using PathMedical.InstrumentManagement;
using PathMedical.ResourceManager;
using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms;

public class DpoaeComponent : TestDetailPluginBase<DpoaeTestInformation, TestInformationAdapter>
{
  public static DpoaeComponent Instance => PathMedical.Singleton.Singleton<DpoaeComponent>.Instance;

  public UserControl HostControl { get; set; }

  private DpoaeComponent()
  {
    this.Name = "DPOAE";
    this.Description = "Application for managing DPOAE data";
    this.Fingerprint = new Guid("01F99DB8-63C5-496f-AB05-58882657C4F1");
    this.InstrumentSignature = new Guid("BAC087B8-331E-4cd3-8F6E-12A9C13E7E61");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<Resources>.Instance.ResourceManager);
    this.DetailViewerComponentModuleType = typeof (TestViewerComponentModule);
    this.ConfigurationModuleType = typeof (ConfigurationEditorDpoaeComponentModule);
    this.DetailReportType = typeof (DpoaeDetailSubSubReport);
    this.TestTypeSignature = new DpoaeTestInformation().NativeTestTypeSignature;
    this.RecordDescriptionSets = DpoaeTestManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = DpoaeTestManager.Instance.RecordSetMaps;
  }

  public override void WriteConfiguration(Stream stream)
  {
    DpoaeConfigurationManager.Instance.WriteConfiguration(stream);
  }

  protected override void ConfigureAdapterRelations(TestInformationAdapter adapter)
  {
  }

  protected override AudiologyTestInformation CreateAudiologyTestInfomation(
    DpoaeTestInformation dpoaeTestInformation)
  {
    return DpoaeTestManager.CreateAudiologyTestInformation(dpoaeTestInformation);
  }
}
