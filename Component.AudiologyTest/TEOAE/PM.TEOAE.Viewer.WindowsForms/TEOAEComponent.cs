// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Viewer.WindowsForms.TeoaeComponent
// Assembly: PM.TEOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83E8557F-96BD-4446-877A-A7FC2FA17A58
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.Viewer.WindowsForms.dll

using PathMedical.AudiologyTest;
using PathMedical.TEOAE.Viewer.WindowsForms.Report;
using PathMedical.TEOAE.Viewer.WindowsForms.Viewer;
using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.TEOAE.Viewer.WindowsForms;

public class TeoaeComponent : TestDetailPluginBase<TeoaeTestInformation, TestInformationAdapter>
{
  public static TeoaeComponent Instance => PathMedical.Singleton.Singleton<TeoaeComponent>.Instance;

  public UserControl HostControl { get; set; }

  private TeoaeComponent()
  {
    this.DetailViewerComponentModuleType = typeof (TestViewerComponentModule);
    this.DetailReportType = typeof (TeoaeDetailSubSubReport);
    this.Name = "TEOAE";
    this.Description = "Plugin to manage TEOAE data.";
    this.Fingerprint = new Guid("8A8448C4-E7EF-494d-BBB1-80EF8800A3BC");
    this.InstrumentSignature = new Guid("BAC087B8-331E-4cd3-8F6E-12A9C13E7E61");
    this.TestTypeSignature = new TeoaeTestInformation().NativeTestTypeSignature;
    this.RecordDescriptionSets = TeoaeTestManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = TeoaeTestManager.Instance.RecordSetMaps;
  }

  public override void WriteConfiguration(Stream stream)
  {
  }

  protected override void ConfigureAdapterRelations(TestInformationAdapter adapter)
  {
  }

  protected override AudiologyTestInformation CreateAudiologyTestInfomation(
    TeoaeTestInformation testInformation)
  {
    return TeoaeTestManager.CreateAudiologyTestInformation(testInformation);
  }
}
