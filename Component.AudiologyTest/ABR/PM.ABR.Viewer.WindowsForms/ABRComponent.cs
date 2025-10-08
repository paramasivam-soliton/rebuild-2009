// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.AbrComponent
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using PathMedical.ABR.Viewer.WindowsForms.Configurator;
using PathMedical.ABR.Viewer.WindowsForms.Properties;
using PathMedical.ABR.Viewer.WindowsForms.Report;
using PathMedical.ABR.Viewer.WindowsForms.Viewer;
using PathMedical.AudiologyTest;
using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms;

public class AbrComponent : TestDetailPluginBase<AbrTestInformation, TestInformationAdapter>
{
  public static AbrComponent Instance => PathMedical.Singleton.Singleton<AbrComponent>.Instance;

  public UserControl HostControl { get; set; }

  private AbrComponent()
  {
    this.DetailViewerComponentModuleType = typeof (TestViewerComponentModule);
    this.DetailReportType = typeof (AbrDetailSubSubReport);
    this.ConfigurationModuleType = typeof (ConfigurationEditorAbrComponentModule);
    this.Name = Resources.ABRComponent_ComponentName;
    this.Description = Resources.ABRComponent_ComponentDescription;
    this.Fingerprint = new Guid("3804B10A-F42D-4c84-AB63-31A27465323A");
    this.InstrumentSignature = new Guid("BAC087B8-331E-4cd3-8F6E-12A9C13E7E61");
    this.TestTypeSignature = new AbrTestInformation().NativeTestTypeSignature;
    this.RecordDescriptionSets = AbrTestManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = AbrTestManager.Instance.RecordSetMaps;
  }

  public override void WriteConfiguration(Stream stream)
  {
    AbrConfigurationManager.Instance.WritePresets(stream);
  }

  protected override void ConfigureAdapterRelations(TestInformationAdapter adapter)
  {
  }

  protected override AudiologyTestInformation CreateAudiologyTestInfomation(
    AbrTestInformation abrTestInformation)
  {
    return AbrTestManager.CreateAudiologyTestInformation(abrTestInformation);
  }
}
