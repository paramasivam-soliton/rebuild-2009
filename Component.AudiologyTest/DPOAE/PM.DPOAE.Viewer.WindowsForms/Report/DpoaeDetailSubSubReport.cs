// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Report.DpoaeDetailSubSubReport
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PathMedical.AudiologyTest;
using PathMedical.InstrumentManagement;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.ResourceManager;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Report;

public class DpoaeDetailSubSubReport : XtraReport, ITestDetailSubReport
{
  private Series noise;
  private Series dpoae;
  private Series calibration;
  private Series dpoaeFail;
  private List<DpoaeDetailSubSubReport.Frequencies> frequenceList;
  private IContainer components;
  private DetailBand testDetailOverviewDetails;
  private TopMarginBand topMarginBand1;
  private BottomMarginBand bottomMarginBand1;
  private XRChart ResultGraph;
  private GroupHeaderBand testDetailOverviewHeader;
  private XRTable xrTable1;
  private XRTableRow xrTableRow1;
  private XRTableCell xrTableCell1;
  private XRTableCell xrTableCell2;
  private XRTableCell xrTableCell3;
  private XRTableCell xrTableCell4;
  private XRTableCell xrTableCell5;
  private XRTableCell xrTableCell6;
  private XRTableCell xrTableCell7;
  private XRTableCell xrTableCell8;
  private XRTable bestTestOverviewTable;
  private XRTableRow bestTestOverviewDetailRow;
  private XRTableCell bestTestOverviewTestType;
  private XRTableCell bestTestOverviewTestEar;
  private XRTableCell bestTestOverviewTestDateAndTime;
  private XRTableCell bestTestOverviewTestResult;
  private XRTableCell bestTestOverviewDuration;
  private XRTableCell bestTestOverviewInstrument;
  private XRTableCell bestTestOverviewProbe;
  private XRTableCell bestTestOverviewExaminer;
  private XRLabel xrLabelComments;
  private XRTable xrTableCommentHeader;
  private XRTableRow xrTableRow2;
  private XRTableCell xrTableCellCommentCreatedHeader;
  private XRTableCell xrTableCellCommentHeader;
  private XRTableCell xrTableCellCreatedByHeader;
  private XRTable xrTable3;
  private XRTableRow xrTableRow3;
  private XRTableCell xrTableCellCommentCreated;
  private XRTableCell xrTableCellComment;
  private XRTableCell xrTableCellCommenCreatedBy;
  private DetailReportBand testCommentOverview;
  private DetailBand testCommentDetails;
  private GroupHeaderBand testCommentHeader;
  public SecondaryAxisX dpoaePassAxisX;
  public SecondaryAxisX dpoaeFailAxisX;
  public XYDiagram xyDiagram1;
  public XYDiagram xyDiagram2;
  private PageHeaderBand PageHeader;
  private XRLabel xrLabel1;
  private XRLabel xrLabel2;
  private XRLabel presetName;
  private XRLabel presetNameValue;
  private XRLabel testCount;
  private XRLabel testCountValue;
  private DetailReportBand frequencies;
  private DetailBand Detail;
  private GroupHeaderBand frequenciesHeader;
  private XRTable xrTable2;
  private XRTableRow xrTableRow4;
  private XRTableCell xrTableCell9;
  private XRTableCell xrTableCell10;
  private XRTableCell xrTableCell12;
  private XRTableCell xrTableCell13;
  private XRTableCell xrTableCell14;
  private XRTableCell xrTableCell15;
  private XRTable xrTable4;
  private XRTableRow xrTableRow5;
  private XRTableCell frequence;
  private XRTableCell normalizedFrequence;
  private XRTableCell frequenceResult;
  private XRTableCell frequenceNoise;
  private XRTableCell frequenceEnergy;
  private XRTableCell frequenceFrames;
  private XRChart calibrationGraph;
  private XRLabel xrLabel3;
  private XRTableCell xrTableCell16;
  private XRTableCell xrTableCell11;
  private XRLabel deviceNameValue;
  private XRLabel deviceName;
  private XRLabel firmwareValue;
  private XRLabel firmwareBuild;
  private XRLabel probeCalibrationDateValue;
  private XRLabel probeCalibrationDate;

  public Guid? TestId { get; set; }

  public DpoaeDetailSubSubReport() => this.InitializeComponent();

  private void DpoaeDetailSubSubReport_BeforePrint(object sender, PrintEventArgs e)
  {
    if (!this.TestId.HasValue)
      return;
    DpoaeTestInformation dpoaeTestInformation = DpoaeComponent.Instance.Get(this.TestId.Value);
    this.frequenceList = new List<DpoaeDetailSubSubReport.Frequencies>();
    this.LoadFrequencies(dpoaeTestInformation);
    List<DpoaeDetailSubSubReport.TestDataPrintElement> dataSource = new List<DpoaeDetailSubSubReport.TestDataPrintElement>();
    dataSource.Add(new DpoaeDetailSubSubReport.TestDataPrintElement()
    {
      Comment = DpoaeDetailSubSubReport.LoadComments(dpoaeTestInformation)
    });
    this.BindTestResult(dpoaeTestInformation);
    this.CreateGraph(dpoaeTestInformation);
    this.CreateResultInformation(dpoaeTestInformation);
    this.CreateFrequenceResultInformation(this.frequenceList);
    if (dpoaeTestInformation.Comments.Count<Comment>() > 0)
    {
      this.BindTestComment(dataSource);
    }
    else
    {
      this.xrLabelComments.Visible = false;
      this.xrTableCommentHeader.Visible = false;
    }
  }

  private void LoadFrequencies(DpoaeTestInformation dpoaeTestInformation)
  {
    for (int index = 0; index < ((IEnumerable<int>) dpoaeTestInformation.Frequency2List).Count<int>(); ++index)
    {
      if (dpoaeTestInformation.Frequency2List[index] > 0)
        this.frequenceList.Add(new DpoaeDetailSubSubReport.Frequencies()
        {
          Frequence = dpoaeTestInformation.Frequency2List[index],
          Energy = Math.Round(dpoaeTestInformation.EnergyList[index], 2),
          Frames = dpoaeTestInformation.FrameCounterList[index],
          Noise = Math.Round(dpoaeTestInformation.NoiseList[index], 2),
          NormalizedFrequence = Math.Round((double) (dpoaeTestInformation.NormalizedFrequency2List[index] * 48 /*0x30*/) / 2.048, 2),
          Result = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) (T1077AudiologyTestResult) dpoaeTestInformation.SingleResultList[index]) as string
        });
    }
  }

  private static List<Comment> LoadComments(DpoaeTestInformation dpoaeTestInformation)
  {
    return dpoaeTestInformation.Comments.ToList<Comment>();
  }

  private void BindTestComment(
    List<DpoaeDetailSubSubReport.TestDataPrintElement> dataSource)
  {
    XRBinding binding1 = new XRBinding("Text", (object) dataSource, "Comment.Text");
    XRBinding binding2 = new XRBinding("Text", (object) dataSource, "Comment.CreationDate");
    XRBinding binding3 = new XRBinding("Text", (object) dataSource, "Comment.Examiner");
    this.testCommentOverview.DataSource = (object) dataSource;
    this.testCommentOverview.DataMember = "Comment";
    this.xrTableCellCommentCreated.DataBindings.Add(binding2);
    this.xrTableCellComment.DataBindings.Add(binding1);
    this.xrTableCellCommenCreatedBy.DataBindings.Add(binding3);
  }

  private void CreateResultInformation(DpoaeTestInformation dpoaeTestInformation)
  {
    XRBinding binding1 = new XRBinding("Text", (object) dpoaeTestInformation, "TestName");
    XRBinding binding2 = new XRBinding("Text", (object) dpoaeTestInformation, "NumberOfTests");
    this.presetNameValue.DataBindings.Add(binding1);
    this.testCountValue.DataBindings.Add(binding2);
    if (!dpoaeTestInformation.InstrumentSerialNumber.HasValue)
      return;
    Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == dpoaeTestInformation.InstrumentSerialNumber.ToString()));
    if (instrument == null)
      return;
    this.deviceNameValue.Text = instrument.Name;
  }

  private void CreateFrequenceResultInformation(
    List<DpoaeDetailSubSubReport.Frequencies> resultFrequenceList)
  {
    XRBinding binding1 = new XRBinding("Text", (object) resultFrequenceList, "frequence");
    XRBinding binding2 = new XRBinding("Text", (object) resultFrequenceList, "normalizedFrequence");
    XRBinding binding3 = new XRBinding("Text", (object) resultFrequenceList, "result");
    XRBinding binding4 = new XRBinding("Text", (object) resultFrequenceList, "noise");
    XRBinding binding5 = new XRBinding("Text", (object) resultFrequenceList, "energy");
    XRBinding binding6 = new XRBinding("Text", (object) resultFrequenceList, "frames");
    this.frequencies.DataSource = (object) resultFrequenceList;
    this.frequence.DataBindings.Add(binding1);
    this.normalizedFrequence.DataBindings.Add(binding2);
    this.frequenceResult.DataBindings.Add(binding3);
    this.frequenceNoise.DataBindings.Add(binding4);
    this.frequenceEnergy.DataBindings.Add(binding5);
    this.frequenceFrames.DataBindings.Add(binding6);
  }

  private void CreateGraph(DpoaeTestInformation dpoaeTestInformation)
  {
    int num1 = ((IEnumerable<int>) dpoaeTestInformation.SingleResultList).Any<int>((Func<int, bool>) (t => t != 0)) ? 1 : 0;
    this.CreateChartControls();
    byte num2 = 1;
    byte num3 = 0;
    byte num4 = 0;
    if (num1 != 0)
    {
      if (dpoaeTestInformation.Frequency2List != null)
      {
        for (int index = 0; index < dpoaeTestInformation.Frequency2List.Length; ++index)
        {
          if (dpoaeTestInformation.Frequency2List[index] != 0)
          {
            if (dpoaeTestInformation.Frequency1List[index] != 0 && num4 == (byte) 0)
              num4 = (byte) (dpoaeTestInformation.Frequency2List[index] / 1000);
            this.noise.Points.Add(new SeriesPoint((double) (dpoaeTestInformation.Frequency2List[index] / 1000), new double[1]
            {
              dpoaeTestInformation.NoiseList[index]
            }));
            if (dpoaeTestInformation.SingleResultList[index] == 13175)
              this.dpoae.Points.Add(new SeriesPoint((double) (dpoaeTestInformation.Frequency2List[index] / 1000), new double[1]
              {
                dpoaeTestInformation.DPOAEList[index]
              }));
            else
              this.dpoaeFail.Points.Add(new SeriesPoint((double) (dpoaeTestInformation.Frequency2List[index] / 1000), new double[1]
              {
                dpoaeTestInformation.DPOAEList[index]
              }));
            ++num2;
            num3 = (byte) (dpoaeTestInformation.Frequency2List[index] / 1000);
          }
        }
      }
      if ((int) num3 == (int) num2)
        ++num2;
      this.xyDiagram1.AxisX.GridSpacing = 1.0;
      this.dpoaePassAxisX.GridSpacing = 1.0;
      this.dpoaeFailAxisX.GridSpacing = 1.0;
      this.xyDiagram1.AxisX.Range.MinValue = (object) ((int) num4 - 1).ToString();
      this.xyDiagram1.AxisX.Range.MaxValue = (object) num2.ToString();
      this.dpoaePassAxisX.Range.MaxValue = (object) ((double) num2 - 0.2).ToString();
      AxisRange range1 = this.dpoaePassAxisX.Range;
      double num5 = (double) num4 - 1.1;
      string str1 = num5.ToString();
      range1.MinValue = (object) str1;
      AxisRange range2 = this.dpoaeFailAxisX.Range;
      num5 = (double) num2 - 0.2;
      string str2 = num5.ToString();
      range2.MaxValue = (object) str2;
      this.dpoaeFailAxisX.Range.MinValue = (object) ((double) num4 - 1.1).ToString();
      ((XYDiagramSeriesViewBase) this.dpoae.View).AxisX = (AxisXBase) this.dpoaePassAxisX;
      ((XYDiagramSeriesViewBase) this.dpoaeFail.View).AxisX = (AxisXBase) this.dpoaeFailAxisX;
    }
    this.calibration.ArgumentScaleType = ScaleType.Numerical;
    for (int index = 0; index < dpoaeTestInformation.CalibrationGraph.Length; ++index)
      this.calibration.Points.Add(new SeriesPoint((double) (index + 11) * 46.875, new double[1]
      {
        (double) dpoaeTestInformation.CalibrationGraph[index]
      }));
    this.ResultGraph.Series.Add(this.dpoaeFail);
    this.ResultGraph.Series.Add(this.dpoae);
    this.ResultGraph.Series.Add(this.noise);
    this.calibrationGraph.Series.Add(this.calibration);
  }

  private void CreateChartControls()
  {
    this.ResultGraph.Series.Clear();
    Series series1 = new Series();
    series1.Label.Visible = false;
    series1.Name = "noiseSeries";
    series1.View.Color = Color.Black;
    this.noise = series1;
    ((BarSeriesView) this.noise.View).BarWidth = 0.2;
    ((BarSeriesView) this.noise.View).FillStyle.FillMode = FillMode.Solid;
    this.noise.ArgumentScaleType = ScaleType.Numerical;
    this.noise.PointOptions.ValueNumericOptions.Precision = 0;
    this.noise.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
    Series series2 = new Series();
    series2.Label.Visible = false;
    series2.Name = "dpoaePassSeries";
    series2.View.Color = Color.Green;
    this.dpoae = series2;
    ((BarSeriesView) this.dpoae.View).BarWidth = 0.2;
    ((BarSeriesView) this.dpoae.View).FillStyle.FillMode = FillMode.Solid;
    ((BarSeriesView) this.dpoae.View).Border.Visible = false;
    this.dpoae.ArgumentScaleType = ScaleType.Numerical;
    this.dpoae.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
    this.dpoae.PointOptions.ValueNumericOptions.Precision = 0;
    Series series3 = new Series();
    series3.Label.Visible = false;
    series3.Name = "dpoaeFailSeries";
    series3.View.Color = Color.Gray;
    this.dpoaeFail = series3;
    ((BarSeriesView) this.dpoaeFail.View).BarWidth = 0.2;
    ((BarSeriesView) this.dpoaeFail.View).FillStyle.FillMode = FillMode.Solid;
    ((BarSeriesView) this.dpoae.View).Border.Visible = false;
    this.dpoaeFail.ArgumentScaleType = ScaleType.Numerical;
    this.dpoaeFail.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
    this.dpoaeFail.PointOptions.ValueNumericOptions.Precision = 0;
    this.ResultGraph.SideBySideBarDistanceFixed = 0;
    this.ResultGraph.SideBySideEqualBarWidth = true;
    this.calibrationGraph.Series.Clear();
    Series series4 = new Series("Calibration", ViewType.Spline);
    series4.Label.Visible = false;
    this.calibration = series4;
    ((LineSeriesView) this.calibration.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.calibration.View).LineStyle.Thickness = 1;
    this.calibration.View.Color = Color.Blue;
  }

  private void BindTestResult(DpoaeTestInformation dpoaeTestInformation)
  {
    this.bestTestOverviewTestType.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestType.DPOAE) as string;
    int? nullable1;
    if (dpoaeTestInformation.Ear.HasValue)
    {
      nullable1 = dpoaeTestInformation.Ear;
      if (nullable1.GetValueOrDefault() == 7)
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.LeftEar) as string;
      else
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.RightEar) as string;
    }
    DateTime? nullable2 = dpoaeTestInformation.TestTimeStamp;
    if (nullable2.HasValue)
    {
      XRTableCell overviewTestDateAndTime = this.bestTestOverviewTestDateAndTime;
      nullable2 = dpoaeTestInformation.TestTimeStamp;
      string str = nullable2.ToString();
      overviewTestDateAndTime.Text = str;
    }
    nullable1 = dpoaeTestInformation.TestResult;
    if (nullable1.HasValue)
    {
      XRTableCell overviewTestResult = this.bestTestOverviewTestResult;
      GlobalResourceEnquirer instance = GlobalResourceEnquirer.Instance;
      nullable1 = dpoaeTestInformation.TestResult;
      // ISSUE: variable of a boxed type
      __Boxed<T1077AudiologyTestResult> resourceName = (Enum) (T1077AudiologyTestResult) nullable1.Value;
      string resourceByName = instance.GetResourceByName((Enum) resourceName) as string;
      overviewTestResult.Text = resourceByName;
    }
    nullable1 = dpoaeTestInformation.Duration;
    if (nullable1.HasValue)
    {
      XRTableCell overviewDuration = this.bestTestOverviewDuration;
      DateTime dateTime = new DateTime();
      nullable1 = dpoaeTestInformation.Duration;
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) nullable1.Value);
      string str = $"{dateTime + timeSpan:T}";
      overviewDuration.Text = str;
    }
    long? nullable3 = dpoaeTestInformation.InstrumentSerialNumber;
    if (nullable3.HasValue)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == dpoaeTestInformation.InstrumentSerialNumber.ToString()));
      if (instrument != null)
        this.bestTestOverviewInstrument.Text = instrument.SerialNumber;
    }
    nullable3 = dpoaeTestInformation.ProbeSerialNumber;
    if (nullable3.HasValue)
    {
      XRTableCell testOverviewProbe = this.bestTestOverviewProbe;
      nullable3 = dpoaeTestInformation.ProbeSerialNumber;
      string str = nullable3.ToString();
      testOverviewProbe.Text = str;
    }
    if (dpoaeTestInformation.UserAccountId.HasValue)
    {
      User user = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
      {
        Guid id = u.Id;
        Guid? userAccountId = dpoaeTestInformation.UserAccountId;
        return userAccountId.HasValue && id == userAccountId.GetValueOrDefault();
      }));
      if (user != null)
        this.bestTestOverviewExaminer.Text = user.LoginName;
    }
    nullable1 = dpoaeTestInformation.FirmwareVersion;
    if (nullable1.HasValue)
    {
      XRLabel firmwareValue = this.firmwareValue;
      nullable1 = dpoaeTestInformation.FirmwareVersion;
      string str = nullable1.ToString();
      firmwareValue.Text = str;
    }
    nullable2 = dpoaeTestInformation.ProbeCalibrationDate;
    if (!nullable2.HasValue)
      return;
    XRLabel calibrationDateValue = this.probeCalibrationDateValue;
    nullable2 = dpoaeTestInformation.ProbeCalibrationDate;
    string str1 = nullable2.ToString();
    calibrationDateValue.Text = str1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DpoaeDetailSubSubReport));
    this.xyDiagram1 = new XYDiagram();
    CustomAxisLabel customAxisLabel1 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel2 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel3 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel4 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel5 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel6 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel7 = new CustomAxisLabel();
    this.dpoaePassAxisX = new SecondaryAxisX();
    this.dpoaeFailAxisX = new SecondaryAxisX();
    SecondaryAxisY secondaryAxisY = new SecondaryAxisY();
    Series series1 = new Series();
    SideBySideBarSeriesLabel sideBarSeriesLabel1 = new SideBySideBarSeriesLabel();
    PointOptions pointOptions1 = new PointOptions();
    SideBySideBarSeriesView sideBarSeriesView1 = new SideBySideBarSeriesView();
    Series series2 = new Series();
    SideBySideBarSeriesLabel sideBarSeriesLabel2 = new SideBySideBarSeriesLabel();
    PointOptions pointOptions2 = new PointOptions();
    SideBySideBarSeriesView sideBarSeriesView2 = new SideBySideBarSeriesView();
    Series series3 = new Series();
    SideBySideBarSeriesLabel sideBarSeriesLabel3 = new SideBySideBarSeriesLabel();
    PointOptions pointOptions3 = new PointOptions();
    SideBySideBarSeriesView sideBarSeriesView3 = new SideBySideBarSeriesView();
    SideBySideBarSeriesLabel sideBarSeriesLabel4 = new SideBySideBarSeriesLabel();
    XYDiagram xyDiagram = new XYDiagram();
    CustomAxisLabel customAxisLabel8 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel9 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel10 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel11 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel12 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel13 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel14 = new CustomAxisLabel();
    CustomAxisLabel customAxisLabel15 = new CustomAxisLabel();
    Series series4 = new Series();
    PointSeriesLabel pointSeriesLabel1 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView1 = new SplineSeriesView();
    PointSeriesLabel pointSeriesLabel2 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView2 = new SplineSeriesView();
    this.ResultGraph = new XRChart();
    this.testDetailOverviewDetails = new DetailBand();
    this.probeCalibrationDateValue = new XRLabel();
    this.probeCalibrationDate = new XRLabel();
    this.firmwareValue = new XRLabel();
    this.firmwareBuild = new XRLabel();
    this.deviceNameValue = new XRLabel();
    this.deviceName = new XRLabel();
    this.xrLabel3 = new XRLabel();
    this.calibrationGraph = new XRChart();
    this.testCount = new XRLabel();
    this.testCountValue = new XRLabel();
    this.presetName = new XRLabel();
    this.presetNameValue = new XRLabel();
    this.xrLabel2 = new XRLabel();
    this.bestTestOverviewTable = new XRTable();
    this.bestTestOverviewDetailRow = new XRTableRow();
    this.bestTestOverviewTestType = new XRTableCell();
    this.bestTestOverviewTestEar = new XRTableCell();
    this.bestTestOverviewTestDateAndTime = new XRTableCell();
    this.xrTableCell16 = new XRTableCell();
    this.bestTestOverviewTestResult = new XRTableCell();
    this.bestTestOverviewDuration = new XRTableCell();
    this.bestTestOverviewInstrument = new XRTableCell();
    this.bestTestOverviewProbe = new XRTableCell();
    this.bestTestOverviewExaminer = new XRTableCell();
    this.topMarginBand1 = new TopMarginBand();
    this.bottomMarginBand1 = new BottomMarginBand();
    this.testDetailOverviewHeader = new GroupHeaderBand();
    this.xrTable1 = new XRTable();
    this.xrTableRow1 = new XRTableRow();
    this.xrTableCell1 = new XRTableCell();
    this.xrTableCell2 = new XRTableCell();
    this.xrTableCell3 = new XRTableCell();
    this.xrTableCell11 = new XRTableCell();
    this.xrTableCell4 = new XRTableCell();
    this.xrTableCell5 = new XRTableCell();
    this.xrTableCell6 = new XRTableCell();
    this.xrTableCell7 = new XRTableCell();
    this.xrTableCell8 = new XRTableCell();
    this.xrLabelComments = new XRLabel();
    this.xrTableCommentHeader = new XRTable();
    this.xrTableRow2 = new XRTableRow();
    this.xrTableCellCommentCreatedHeader = new XRTableCell();
    this.xrTableCellCommentHeader = new XRTableCell();
    this.xrTableCellCreatedByHeader = new XRTableCell();
    this.xrTable3 = new XRTable();
    this.xrTableRow3 = new XRTableRow();
    this.xrTableCellCommentCreated = new XRTableCell();
    this.xrTableCellComment = new XRTableCell();
    this.xrTableCellCommenCreatedBy = new XRTableCell();
    this.testCommentOverview = new DetailReportBand();
    this.testCommentDetails = new DetailBand();
    this.testCommentHeader = new GroupHeaderBand();
    this.PageHeader = new PageHeaderBand();
    this.xrLabel1 = new XRLabel();
    this.frequencies = new DetailReportBand();
    this.Detail = new DetailBand();
    this.xrTable4 = new XRTable();
    this.xrTableRow5 = new XRTableRow();
    this.frequence = new XRTableCell();
    this.normalizedFrequence = new XRTableCell();
    this.frequenceResult = new XRTableCell();
    this.frequenceNoise = new XRTableCell();
    this.frequenceEnergy = new XRTableCell();
    this.frequenceFrames = new XRTableCell();
    this.frequenciesHeader = new GroupHeaderBand();
    this.xrTable2 = new XRTable();
    this.xrTableRow4 = new XRTableRow();
    this.xrTableCell9 = new XRTableCell();
    this.xrTableCell10 = new XRTableCell();
    this.xrTableCell12 = new XRTableCell();
    this.xrTableCell13 = new XRTableCell();
    this.xrTableCell14 = new XRTableCell();
    this.xrTableCell15 = new XRTableCell();
    this.ResultGraph.BeginInit();
    ((ISupportInitialize) this.xyDiagram1).BeginInit();
    ((ISupportInitialize) this.dpoaePassAxisX).BeginInit();
    ((ISupportInitialize) this.dpoaeFailAxisX).BeginInit();
    ((ISupportInitialize) secondaryAxisY).BeginInit();
    ((ISupportInitialize) series1).BeginInit();
    ((ISupportInitialize) sideBarSeriesLabel1).BeginInit();
    ((ISupportInitialize) sideBarSeriesView1).BeginInit();
    ((ISupportInitialize) series2).BeginInit();
    ((ISupportInitialize) sideBarSeriesLabel2).BeginInit();
    ((ISupportInitialize) sideBarSeriesView2).BeginInit();
    ((ISupportInitialize) series3).BeginInit();
    ((ISupportInitialize) sideBarSeriesLabel3).BeginInit();
    ((ISupportInitialize) sideBarSeriesView3).BeginInit();
    ((ISupportInitialize) sideBarSeriesLabel4).BeginInit();
    this.calibrationGraph.BeginInit();
    ((ISupportInitialize) xyDiagram).BeginInit();
    ((ISupportInitialize) series4).BeginInit();
    ((ISupportInitialize) pointSeriesLabel1).BeginInit();
    ((ISupportInitialize) splineSeriesView1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel2).BeginInit();
    ((ISupportInitialize) splineSeriesView2).BeginInit();
    this.bestTestOverviewTable.BeginInit();
    this.xrTable1.BeginInit();
    this.xrTableCommentHeader.BeginInit();
    this.xrTable3.BeginInit();
    this.xrTable4.BeginInit();
    this.xrTable2.BeginInit();
    this.BeginInit();
    componentResourceManager.ApplyResources((object) this.ResultGraph, "ResultGraph");
    this.ResultGraph.Borders = BorderSide.None;
    this.xyDiagram1.AxisX.Alignment = (AxisAlignment) componentResourceManager.GetObject("resource.Alignment");
    this.xyDiagram1.AxisX.Label.Font = (Font) componentResourceManager.GetObject("resource.Font");
    this.xyDiagram1.AxisX.Range.Auto = false;
    this.xyDiagram1.AxisX.Range.MaxValueSerializable = "10";
    this.xyDiagram1.AxisX.Range.MinValueSerializable = "0";
    this.xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
    this.xyDiagram1.AxisX.Tickmarks.Visible = false;
    this.xyDiagram1.AxisX.Title.Alignment = StringAlignment.Far;
    this.xyDiagram1.AxisX.Title.Antialiasing = false;
    this.xyDiagram1.AxisX.Title.Font = (Font) componentResourceManager.GetObject("resource.Font1");
    this.xyDiagram1.AxisX.Title.Text = componentResourceManager.GetString("resource.Text");
    this.xyDiagram1.AxisX.Title.Visible = true;
    this.xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
    customAxisLabel1.AxisValueSerializable = "10";
    componentResourceManager.ApplyResources((object) customAxisLabel1, "customAxisLabel1");
    customAxisLabel2.AxisValueSerializable = "20";
    componentResourceManager.ApplyResources((object) customAxisLabel2, "customAxisLabel2");
    customAxisLabel3.AxisValueSerializable = "30";
    componentResourceManager.ApplyResources((object) customAxisLabel3, "customAxisLabel3");
    customAxisLabel4.AxisValueSerializable = "40";
    componentResourceManager.ApplyResources((object) customAxisLabel4, "customAxisLabel4");
    customAxisLabel5.AxisValueSerializable = "50";
    componentResourceManager.ApplyResources((object) customAxisLabel5, "customAxisLabel5");
    customAxisLabel6.AxisValueSerializable = "60";
    componentResourceManager.ApplyResources((object) customAxisLabel6, "customAxisLabel6");
    customAxisLabel7.AxisValueSerializable = "70";
    componentResourceManager.ApplyResources((object) customAxisLabel7, "customAxisLabel7");
    this.xyDiagram1.AxisY.CustomLabels.AddRange(new CustomAxisLabel[7]
    {
      customAxisLabel1,
      customAxisLabel2,
      customAxisLabel3,
      customAxisLabel4,
      customAxisLabel5,
      customAxisLabel6,
      customAxisLabel7
    });
    this.xyDiagram1.AxisY.Range.Auto = false;
    this.xyDiagram1.AxisY.Range.MaxValueSerializable = "80";
    this.xyDiagram1.AxisY.Range.MinValueSerializable = "0";
    this.xyDiagram1.AxisY.Range.SideMarginsEnabled = false;
    this.xyDiagram1.AxisY.Tickmarks.Visible = false;
    this.xyDiagram1.AxisY.Title.Alignment = StringAlignment.Far;
    this.xyDiagram1.AxisY.Title.Antialiasing = false;
    this.xyDiagram1.AxisY.Title.Font = (Font) componentResourceManager.GetObject("resource.Font2");
    this.xyDiagram1.AxisY.Title.Text = componentResourceManager.GetString("resource.Text1");
    this.xyDiagram1.AxisY.Title.Visible = true;
    this.xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
    this.xyDiagram1.PaneDistance = 0;
    this.dpoaePassAxisX.AxisID = 0;
    this.dpoaePassAxisX.Label.Visible = false;
    componentResourceManager.ApplyResources((object) this.dpoaePassAxisX, "dpoaePassAxisX");
    this.dpoaePassAxisX.Range.Auto = false;
    this.dpoaePassAxisX.Range.MaxValueSerializable = "9.9";
    this.dpoaePassAxisX.Range.MinValueSerializable = "-0.1";
    this.dpoaePassAxisX.Range.SideMarginsEnabled = true;
    this.dpoaePassAxisX.Visible = false;
    this.dpoaePassAxisX.VisibleInPanesSerializable = "-1";
    this.dpoaeFailAxisX.AxisID = 1;
    componentResourceManager.ApplyResources((object) this.dpoaeFailAxisX, "dpoaeFailAxisX");
    this.dpoaeFailAxisX.Range.Auto = false;
    this.dpoaeFailAxisX.Range.MaxValueSerializable = "9.9";
    this.dpoaeFailAxisX.Range.MinValueSerializable = "-0.1";
    this.dpoaeFailAxisX.Range.SideMarginsEnabled = true;
    this.dpoaeFailAxisX.Visible = false;
    this.dpoaeFailAxisX.VisibleInPanesSerializable = "-1";
    this.xyDiagram1.SecondaryAxesX.AddRange(new SecondaryAxisX[2]
    {
      this.dpoaePassAxisX,
      this.dpoaeFailAxisX
    });
    secondaryAxisY.AxisID = 0;
    componentResourceManager.ApplyResources((object) secondaryAxisY, "secondaryAxisY1");
    secondaryAxisY.Range.SideMarginsEnabled = true;
    secondaryAxisY.Visible = false;
    secondaryAxisY.VisibleInPanesSerializable = "-1";
    this.xyDiagram1.SecondaryAxesY.AddRange(new SecondaryAxisY[1]
    {
      secondaryAxisY
    });
    this.ResultGraph.Diagram = (Diagram) this.xyDiagram1;
    this.ResultGraph.Legend.Visible = false;
    this.ResultGraph.Name = "ResultGraph";
    this.ResultGraph.PaletteName = "Palette 1";
    this.ResultGraph.PaletteRepository.Add("Palette 1", new Palette("Palette 1", PaletteScaleMode.Repeat, new PaletteEntry[12]
    {
      new PaletteEntry(Color.FromArgb(206, 146, 30), Color.FromArgb(239, 188, 88)),
      new PaletteEntry(Color.FromArgb(102, 168, 9), Color.FromArgb(102, 168, 9)),
      new PaletteEntry(Color.FromArgb(200, 76, 27), Color.FromArgb(234, 118, 72)),
      new PaletteEntry(Color.FromArgb(200, 173, 58), Color.FromArgb(246, 213, 76)),
      new PaletteEntry(Color.FromArgb(119, 199, 70), Color.FromArgb(144 /*0x90*/, 238, 82)),
      new PaletteEntry(Color.FromArgb(152, 58, 23), Color.FromArgb(187, 86, 49)),
      new PaletteEntry(Color.FromArgb(0, 136, 0), Color.FromArgb(30, 171, 27)),
      new PaletteEntry(Color.FromArgb(158, 161, 0), Color.FromArgb(196, 194, 0)),
      new PaletteEntry(Color.FromArgb(55, 159, 113), Color.FromArgb(72, 194, 141)),
      new PaletteEntry(Color.FromArgb(206, 172, 104), Color.FromArgb(236, 202, 134)),
      new PaletteEntry(Color.FromArgb(115, 192 /*0xC0*/, 192 /*0xC0*/), Color.FromArgb(132, 227, 181)),
      new PaletteEntry(Color.FromArgb(234, 147, 115), Color.FromArgb((int) byte.MaxValue, 175, 149))
    }));
    this.ResultGraph.PaletteRepository.Add("TestGraph", new Palette("TestGraph", PaletteScaleMode.Repeat, new PaletteEntry[2]
    {
      new PaletteEntry(Color.Black, Color.Black),
      new PaletteEntry(Color.Blue, Color.Blue)
    }));
    series1.ArgumentScaleType = ScaleType.Numerical;
    sideBarSeriesLabel1.LineVisible = true;
    sideBarSeriesLabel1.Visible = false;
    series1.Label = (SeriesLabelBase) sideBarSeriesLabel1;
    componentResourceManager.ApplyResources((object) series1, "series1");
    pointOptions1.ValueNumericOptions.Format = NumericFormat.Number;
    pointOptions1.ValueNumericOptions.Precision = 0;
    series1.PointOptions = pointOptions1;
    series1.ShowInLegend = false;
    sideBarSeriesView1.BarWidth = 0.2;
    sideBarSeriesView1.FillStyle.FillMode = FillMode.Solid;
    series1.View = (SeriesViewBase) sideBarSeriesView1;
    series2.ArgumentScaleType = ScaleType.Numerical;
    sideBarSeriesLabel2.LineVisible = true;
    sideBarSeriesLabel2.Visible = false;
    series2.Label = (SeriesLabelBase) sideBarSeriesLabel2;
    componentResourceManager.ApplyResources((object) series2, "series2");
    pointOptions2.ValueNumericOptions.Format = NumericFormat.Number;
    pointOptions2.ValueNumericOptions.Precision = 0;
    series2.PointOptions = pointOptions2;
    series2.ShowInLegend = false;
    sideBarSeriesView2.BarWidth = 0.2;
    sideBarSeriesView2.Border.Visible = false;
    sideBarSeriesView2.FillStyle.FillMode = FillMode.Solid;
    series2.View = (SeriesViewBase) sideBarSeriesView2;
    series3.ArgumentScaleType = ScaleType.Numerical;
    sideBarSeriesLabel3.LineVisible = true;
    sideBarSeriesLabel3.Visible = false;
    series3.Label = (SeriesLabelBase) sideBarSeriesLabel3;
    componentResourceManager.ApplyResources((object) series3, "series3");
    pointOptions3.ValueNumericOptions.Format = NumericFormat.Number;
    pointOptions3.ValueNumericOptions.Precision = 0;
    series3.PointOptions = pointOptions3;
    series3.ShowInLegend = false;
    sideBarSeriesView3.BarWidth = 0.2;
    sideBarSeriesView3.Border.Visible = false;
    sideBarSeriesView3.FillStyle.FillMode = FillMode.Solid;
    series3.View = (SeriesViewBase) sideBarSeriesView3;
    this.ResultGraph.SeriesSerializable = new Series[3]
    {
      series1,
      series2,
      series3
    };
    sideBarSeriesLabel4.LineVisible = true;
    this.ResultGraph.SeriesTemplate.Label = (SeriesLabelBase) sideBarSeriesLabel4;
    this.ResultGraph.SideBySideBarDistanceFixed = 0;
    this.ResultGraph.SideBySideEqualBarWidth = true;
    this.testDetailOverviewDetails.Controls.AddRange(new XRControl[15]
    {
      (XRControl) this.probeCalibrationDateValue,
      (XRControl) this.probeCalibrationDate,
      (XRControl) this.firmwareValue,
      (XRControl) this.firmwareBuild,
      (XRControl) this.deviceNameValue,
      (XRControl) this.deviceName,
      (XRControl) this.xrLabel3,
      (XRControl) this.calibrationGraph,
      (XRControl) this.testCount,
      (XRControl) this.testCountValue,
      (XRControl) this.presetName,
      (XRControl) this.presetNameValue,
      (XRControl) this.xrLabel2,
      (XRControl) this.bestTestOverviewTable,
      (XRControl) this.ResultGraph
    });
    componentResourceManager.ApplyResources((object) this.testDetailOverviewDetails, "testDetailOverviewDetails");
    this.testDetailOverviewDetails.HeightF = 400f;
    this.testDetailOverviewDetails.Name = "testDetailOverviewDetails";
    this.testDetailOverviewDetails.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.testDetailOverviewDetails.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDateValue, "probeCalibrationDateValue");
    this.probeCalibrationDateValue.Name = "probeCalibrationDateValue";
    this.probeCalibrationDateValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.probeCalibrationDateValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDate, "probeCalibrationDate");
    this.probeCalibrationDate.Name = "probeCalibrationDate";
    this.probeCalibrationDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.probeCalibrationDate.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.firmwareValue, "firmwareValue");
    this.firmwareValue.Name = "firmwareValue";
    this.firmwareValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.firmwareValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.firmwareBuild, "firmwareBuild");
    this.firmwareBuild.Name = "firmwareBuild";
    this.firmwareBuild.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.firmwareBuild.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.deviceNameValue, "deviceNameValue");
    this.deviceNameValue.Name = "deviceNameValue";
    this.deviceNameValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.deviceNameValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.deviceName, "deviceName");
    this.deviceName.Name = "deviceName";
    this.deviceName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.deviceName.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabel3, "xrLabel3");
    this.xrLabel3.Name = "xrLabel3";
    this.xrLabel3.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel3.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.calibrationGraph, "calibrationGraph");
    this.calibrationGraph.Borders = BorderSide.None;
    customAxisLabel8.AxisValueSerializable = "1000";
    componentResourceManager.ApplyResources((object) customAxisLabel8, "customAxisLabel8");
    customAxisLabel9.AxisValueSerializable = "2000";
    componentResourceManager.ApplyResources((object) customAxisLabel9, "customAxisLabel9");
    customAxisLabel10.AxisValueSerializable = "3000";
    componentResourceManager.ApplyResources((object) customAxisLabel10, "customAxisLabel10");
    customAxisLabel11.AxisValueSerializable = "4000";
    componentResourceManager.ApplyResources((object) customAxisLabel11, "customAxisLabel11");
    customAxisLabel12.AxisValueSerializable = "5000";
    componentResourceManager.ApplyResources((object) customAxisLabel12, "customAxisLabel12");
    customAxisLabel13.AxisValueSerializable = "6000";
    componentResourceManager.ApplyResources((object) customAxisLabel13, "customAxisLabel13");
    customAxisLabel14.AxisValueSerializable = "7000";
    componentResourceManager.ApplyResources((object) customAxisLabel14, "customAxisLabel14");
    customAxisLabel15.AxisValueSerializable = "8000";
    componentResourceManager.ApplyResources((object) customAxisLabel15, "customAxisLabel15");
    xyDiagram.AxisX.CustomLabels.AddRange(new CustomAxisLabel[8]
    {
      customAxisLabel8,
      customAxisLabel9,
      customAxisLabel10,
      customAxisLabel11,
      customAxisLabel12,
      customAxisLabel13,
      customAxisLabel14,
      customAxisLabel15
    });
    xyDiagram.AxisX.GridLines.Visible = true;
    xyDiagram.AxisX.Logarithmic = true;
    xyDiagram.AxisX.Range.Auto = false;
    xyDiagram.AxisX.Range.MaxValueSerializable = "10000";
    xyDiagram.AxisX.Range.MinValueSerializable = "500";
    xyDiagram.AxisX.Range.SideMarginsEnabled = false;
    xyDiagram.AxisX.Tickmarks.Visible = false;
    xyDiagram.AxisX.Title.Alignment = StringAlignment.Far;
    xyDiagram.AxisX.Title.Font = (Font) componentResourceManager.GetObject("resource.Font3");
    xyDiagram.AxisX.Title.Text = componentResourceManager.GetString("resource.Text2");
    xyDiagram.AxisX.Title.Visible = true;
    xyDiagram.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram.AxisY.Visible = false;
    xyDiagram.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram.DefaultPane.BorderVisible = false;
    this.calibrationGraph.Diagram = (Diagram) xyDiagram;
    this.calibrationGraph.Legend.Visible = false;
    this.calibrationGraph.Name = "calibrationGraph";
    this.calibrationGraph.PaletteName = "Palette 1";
    this.calibrationGraph.PaletteRepository.Add("Palette 1", new Palette("Palette 1", PaletteScaleMode.Repeat, new PaletteEntry[12]
    {
      new PaletteEntry(Color.FromArgb(206, 146, 30), Color.FromArgb(239, 188, 88)),
      new PaletteEntry(Color.FromArgb(102, 168, 9), Color.FromArgb(102, 168, 9)),
      new PaletteEntry(Color.FromArgb(200, 76, 27), Color.FromArgb(234, 118, 72)),
      new PaletteEntry(Color.FromArgb(200, 173, 58), Color.FromArgb(246, 213, 76)),
      new PaletteEntry(Color.FromArgb(119, 199, 70), Color.FromArgb(144 /*0x90*/, 238, 82)),
      new PaletteEntry(Color.FromArgb(152, 58, 23), Color.FromArgb(187, 86, 49)),
      new PaletteEntry(Color.FromArgb(0, 136, 0), Color.FromArgb(30, 171, 27)),
      new PaletteEntry(Color.FromArgb(158, 161, 0), Color.FromArgb(196, 194, 0)),
      new PaletteEntry(Color.FromArgb(55, 159, 113), Color.FromArgb(72, 194, 141)),
      new PaletteEntry(Color.FromArgb(206, 172, 104), Color.FromArgb(236, 202, 134)),
      new PaletteEntry(Color.FromArgb(115, 192 /*0xC0*/, 192 /*0xC0*/), Color.FromArgb(132, 227, 181)),
      new PaletteEntry(Color.FromArgb(234, 147, 115), Color.FromArgb((int) byte.MaxValue, 175, 149))
    }));
    this.calibrationGraph.PaletteRepository.Add("TestGraph", new Palette("TestGraph", PaletteScaleMode.Repeat, new PaletteEntry[2]
    {
      new PaletteEntry(Color.Black, Color.Black),
      new PaletteEntry(Color.Blue, Color.Blue)
    }));
    series4.ArgumentScaleType = ScaleType.Numerical;
    pointSeriesLabel1.LineVisible = true;
    pointSeriesLabel1.Visible = false;
    series4.Label = (SeriesLabelBase) pointSeriesLabel1;
    componentResourceManager.ApplyResources((object) series4, "series4");
    splineSeriesView1.LineMarkerOptions.Visible = false;
    series4.View = (SeriesViewBase) splineSeriesView1;
    this.calibrationGraph.SeriesSerializable = new Series[1]
    {
      series4
    };
    pointSeriesLabel2.LineVisible = true;
    this.calibrationGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel2;
    this.calibrationGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView2;
    componentResourceManager.ApplyResources((object) this.testCount, "testCount");
    this.testCount.Name = "testCount";
    this.testCount.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.testCount.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.testCountValue, "testCountValue");
    this.testCountValue.Name = "testCountValue";
    this.testCountValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.testCountValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.presetName, "presetName");
    this.presetName.Name = "presetName";
    this.presetName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.presetName.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.presetNameValue, "presetNameValue");
    this.presetNameValue.Name = "presetNameValue";
    this.presetNameValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.presetNameValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabel2, "xrLabel2");
    this.xrLabel2.Name = "xrLabel2";
    this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel2.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTable, "bestTestOverviewTable");
    this.bestTestOverviewTable.Name = "bestTestOverviewTable";
    this.bestTestOverviewTable.Rows.AddRange(new XRTableRow[1]
    {
      this.bestTestOverviewDetailRow
    });
    this.bestTestOverviewDetailRow.Cells.AddRange(new XRTableCell[9]
    {
      this.bestTestOverviewTestType,
      this.bestTestOverviewTestEar,
      this.bestTestOverviewTestDateAndTime,
      this.xrTableCell16,
      this.bestTestOverviewTestResult,
      this.bestTestOverviewDuration,
      this.bestTestOverviewInstrument,
      this.bestTestOverviewProbe,
      this.bestTestOverviewExaminer
    });
    this.bestTestOverviewDetailRow.Name = "bestTestOverviewDetailRow";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDetailRow, "bestTestOverviewDetailRow");
    this.bestTestOverviewDetailRow.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestType, "bestTestOverviewTestType");
    this.bestTestOverviewTestType.Name = "bestTestOverviewTestType";
    this.bestTestOverviewTestType.StylePriority.UseFont = false;
    this.bestTestOverviewTestType.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestType.Weight = 101.0 / 329.0;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestEar, "bestTestOverviewTestEar");
    this.bestTestOverviewTestEar.Name = "bestTestOverviewTestEar";
    this.bestTestOverviewTestEar.StylePriority.UseFont = false;
    this.bestTestOverviewTestEar.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestEar.Weight = 0.21302221528181792;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestDateAndTime, "bestTestOverviewTestDateAndTime");
    this.bestTestOverviewTestDateAndTime.Name = "bestTestOverviewTestDateAndTime";
    this.bestTestOverviewTestDateAndTime.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewTestDateAndTime.StylePriority.UseFont = false;
    this.bestTestOverviewTestDateAndTime.StylePriority.UsePadding = false;
    this.bestTestOverviewTestDateAndTime.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestDateAndTime.Weight = 0.54367567014066165;
    this.xrTableCell16.Name = "xrTableCell16";
    componentResourceManager.ApplyResources((object) this.xrTableCell16, "xrTableCell16");
    this.xrTableCell16.Weight = 0.044963715388057462;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestResult, "bestTestOverviewTestResult");
    this.bestTestOverviewTestResult.Name = "bestTestOverviewTestResult";
    this.bestTestOverviewTestResult.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewTestResult.StylePriority.UseFont = false;
    this.bestTestOverviewTestResult.StylePriority.UsePadding = false;
    this.bestTestOverviewTestResult.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestResult.Weight = 0.44276971509753749;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDuration, "bestTestOverviewDuration");
    this.bestTestOverviewDuration.Name = "bestTestOverviewDuration";
    this.bestTestOverviewDuration.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewDuration.StylePriority.UseFont = false;
    this.bestTestOverviewDuration.StylePriority.UsePadding = false;
    this.bestTestOverviewDuration.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewDuration.Weight = 0.24316947352117238;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewInstrument, "bestTestOverviewInstrument");
    this.bestTestOverviewInstrument.Name = "bestTestOverviewInstrument";
    this.bestTestOverviewInstrument.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewInstrument.StylePriority.UseFont = false;
    this.bestTestOverviewInstrument.StylePriority.UsePadding = false;
    this.bestTestOverviewInstrument.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewInstrument.Weight = 0.44849625551068506;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewProbe, "bestTestOverviewProbe");
    this.bestTestOverviewProbe.Name = "bestTestOverviewProbe";
    this.bestTestOverviewProbe.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewProbe.StylePriority.UseFont = false;
    this.bestTestOverviewProbe.StylePriority.UsePadding = false;
    this.bestTestOverviewProbe.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewProbe.Weight = 0.2916666640385065;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewExaminer, "bestTestOverviewExaminer");
    this.bestTestOverviewExaminer.Name = "bestTestOverviewExaminer";
    this.bestTestOverviewExaminer.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewExaminer.StylePriority.UseFont = false;
    this.bestTestOverviewExaminer.StylePriority.UsePadding = false;
    this.bestTestOverviewExaminer.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewExaminer.Weight = 0.42524540956259504;
    this.topMarginBand1.HeightF = 100f;
    this.topMarginBand1.Name = "topMarginBand1";
    componentResourceManager.ApplyResources((object) this.topMarginBand1, "topMarginBand1");
    this.bottomMarginBand1.HeightF = 100f;
    this.bottomMarginBand1.Name = "bottomMarginBand1";
    componentResourceManager.ApplyResources((object) this.bottomMarginBand1, "bottomMarginBand1");
    this.testDetailOverviewHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable1
    });
    this.testDetailOverviewHeader.HeightF = 27f;
    this.testDetailOverviewHeader.Name = "testDetailOverviewHeader";
    componentResourceManager.ApplyResources((object) this.testDetailOverviewHeader, "testDetailOverviewHeader");
    componentResourceManager.ApplyResources((object) this.xrTable1, "xrTable1");
    this.xrTable1.Name = "xrTable1";
    this.xrTable1.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow1
    });
    this.xrTableRow1.Borders = BorderSide.Bottom;
    this.xrTableRow1.Cells.AddRange(new XRTableCell[9]
    {
      this.xrTableCell1,
      this.xrTableCell2,
      this.xrTableCell3,
      this.xrTableCell11,
      this.xrTableCell4,
      this.xrTableCell5,
      this.xrTableCell6,
      this.xrTableCell7,
      this.xrTableCell8
    });
    this.xrTableRow1.Name = "xrTableRow1";
    this.xrTableRow1.StylePriority.UseBorders = false;
    componentResourceManager.ApplyResources((object) this.xrTableRow1, "xrTableRow1");
    this.xrTableRow1.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCell1, "xrTableCell1");
    this.xrTableCell1.Name = "xrTableCell1";
    this.xrTableCell1.StylePriority.UseFont = false;
    this.xrTableCell1.StylePriority.UseTextAlignment = false;
    this.xrTableCell1.Weight = 101.0 / 329.0;
    componentResourceManager.ApplyResources((object) this.xrTableCell2, "xrTableCell2");
    this.xrTableCell2.Name = "xrTableCell2";
    this.xrTableCell2.StylePriority.UseFont = false;
    this.xrTableCell2.StylePriority.UseTextAlignment = false;
    this.xrTableCell2.Weight = 0.21302221528181792;
    componentResourceManager.ApplyResources((object) this.xrTableCell3, "xrTableCell3");
    this.xrTableCell3.Name = "xrTableCell3";
    this.xrTableCell3.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell3.StylePriority.UseFont = false;
    this.xrTableCell3.StylePriority.UsePadding = false;
    this.xrTableCell3.StylePriority.UseTextAlignment = false;
    this.xrTableCell3.Weight = 0.54367567014066165;
    this.xrTableCell11.Name = "xrTableCell11";
    componentResourceManager.ApplyResources((object) this.xrTableCell11, "xrTableCell11");
    this.xrTableCell11.Weight = 0.044963715388057462;
    componentResourceManager.ApplyResources((object) this.xrTableCell4, "xrTableCell4");
    this.xrTableCell4.Name = "xrTableCell4";
    this.xrTableCell4.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell4.StylePriority.UseFont = false;
    this.xrTableCell4.StylePriority.UsePadding = false;
    this.xrTableCell4.StylePriority.UseTextAlignment = false;
    this.xrTableCell4.Weight = 0.44276971509753749;
    componentResourceManager.ApplyResources((object) this.xrTableCell5, "xrTableCell5");
    this.xrTableCell5.Name = "xrTableCell5";
    this.xrTableCell5.StylePriority.UseFont = false;
    this.xrTableCell5.StylePriority.UseTextAlignment = false;
    this.xrTableCell5.Weight = 0.24316947352117238;
    componentResourceManager.ApplyResources((object) this.xrTableCell6, "xrTableCell6");
    this.xrTableCell6.Name = "xrTableCell6";
    this.xrTableCell6.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell6.StylePriority.UseFont = false;
    this.xrTableCell6.StylePriority.UsePadding = false;
    this.xrTableCell6.StylePriority.UseTextAlignment = false;
    this.xrTableCell6.Weight = 0.44849625551068506;
    componentResourceManager.ApplyResources((object) this.xrTableCell7, "xrTableCell7");
    this.xrTableCell7.Name = "xrTableCell7";
    this.xrTableCell7.StylePriority.UseFont = false;
    this.xrTableCell7.StylePriority.UseTextAlignment = false;
    this.xrTableCell7.Weight = 0.2916666640385065;
    componentResourceManager.ApplyResources((object) this.xrTableCell8, "xrTableCell8");
    this.xrTableCell8.Name = "xrTableCell8";
    this.xrTableCell8.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell8.StylePriority.UseFont = false;
    this.xrTableCell8.StylePriority.UsePadding = false;
    this.xrTableCell8.StylePriority.UseTextAlignment = false;
    this.xrTableCell8.Weight = 0.42524540956259504;
    componentResourceManager.ApplyResources((object) this.xrLabelComments, "xrLabelComments");
    this.xrLabelComments.Name = "xrLabelComments";
    this.xrLabelComments.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelComments.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrTableCommentHeader, "xrTableCommentHeader");
    this.xrTableCommentHeader.Name = "xrTableCommentHeader";
    this.xrTableCommentHeader.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow2
    });
    this.xrTableRow2.Cells.AddRange(new XRTableCell[3]
    {
      this.xrTableCellCommentCreatedHeader,
      this.xrTableCellCommentHeader,
      this.xrTableCellCreatedByHeader
    });
    this.xrTableRow2.Name = "xrTableRow2";
    componentResourceManager.ApplyResources((object) this.xrTableRow2, "xrTableRow2");
    this.xrTableRow2.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentCreatedHeader, "xrTableCellCommentCreatedHeader");
    this.xrTableCellCommentCreatedHeader.Name = "xrTableCellCommentCreatedHeader";
    this.xrTableCellCommentCreatedHeader.StylePriority.UseFont = false;
    this.xrTableCellCommentCreatedHeader.Weight = 0.84473948435740431;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentHeader, "xrTableCellCommentHeader");
    this.xrTableCellCommentHeader.Name = "xrTableCellCommentHeader";
    this.xrTableCellCommentHeader.StylePriority.UseFont = false;
    this.xrTableCellCommentHeader.Weight = 1.7242687328441724;
    componentResourceManager.ApplyResources((object) this.xrTableCellCreatedByHeader, "xrTableCellCreatedByHeader");
    this.xrTableCellCreatedByHeader.Name = "xrTableCellCreatedByHeader";
    this.xrTableCellCreatedByHeader.StylePriority.UseFont = false;
    this.xrTableCellCreatedByHeader.Weight = 0.43099178279842337;
    componentResourceManager.ApplyResources((object) this.xrTable3, "xrTable3");
    this.xrTable3.Name = "xrTable3";
    this.xrTable3.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow3
    });
    this.xrTableRow3.Cells.AddRange(new XRTableCell[3]
    {
      this.xrTableCellCommentCreated,
      this.xrTableCellComment,
      this.xrTableCellCommenCreatedBy
    });
    this.xrTableRow3.Name = "xrTableRow3";
    componentResourceManager.ApplyResources((object) this.xrTableRow3, "xrTableRow3");
    this.xrTableRow3.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentCreated, "xrTableCellCommentCreated");
    this.xrTableCellCommentCreated.Name = "xrTableCellCommentCreated";
    this.xrTableCellCommentCreated.StylePriority.UseFont = false;
    this.xrTableCellCommentCreated.Weight = 0.84473948435740431;
    componentResourceManager.ApplyResources((object) this.xrTableCellComment, "xrTableCellComment");
    this.xrTableCellComment.Name = "xrTableCellComment";
    this.xrTableCellComment.StylePriority.UseFont = false;
    this.xrTableCellComment.Weight = 1.7242687328441724;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommenCreatedBy, "xrTableCellCommenCreatedBy");
    this.xrTableCellCommenCreatedBy.Name = "xrTableCellCommenCreatedBy";
    this.xrTableCellCommenCreatedBy.StylePriority.UseFont = false;
    this.xrTableCellCommenCreatedBy.Weight = 0.43099178279842337;
    this.testCommentOverview.Bands.AddRange(new Band[2]
    {
      (Band) this.testCommentDetails,
      (Band) this.testCommentHeader
    });
    this.testCommentOverview.Level = 1;
    this.testCommentOverview.Name = "testCommentOverview";
    componentResourceManager.ApplyResources((object) this.testCommentOverview, "testCommentOverview");
    this.testCommentDetails.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable3
    });
    this.testCommentDetails.HeightF = 28f;
    this.testCommentDetails.Name = "testCommentDetails";
    componentResourceManager.ApplyResources((object) this.testCommentDetails, "testCommentDetails");
    this.testCommentHeader.Controls.AddRange(new XRControl[2]
    {
      (XRControl) this.xrTableCommentHeader,
      (XRControl) this.xrLabelComments
    });
    this.testCommentHeader.HeightF = 54f;
    this.testCommentHeader.Name = "testCommentHeader";
    componentResourceManager.ApplyResources((object) this.testCommentHeader, "testCommentHeader");
    this.PageHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrLabel1
    });
    this.PageHeader.HeightF = 38f;
    this.PageHeader.Name = "PageHeader";
    componentResourceManager.ApplyResources((object) this.PageHeader, "PageHeader");
    componentResourceManager.ApplyResources((object) this.xrLabel1, "xrLabel1");
    this.xrLabel1.Name = "xrLabel1";
    this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel1.StylePriority.UseFont = false;
    this.frequencies.Bands.AddRange(new Band[2]
    {
      (Band) this.Detail,
      (Band) this.frequenciesHeader
    });
    this.frequencies.Level = 0;
    this.frequencies.Name = "frequencies";
    componentResourceManager.ApplyResources((object) this.frequencies, "frequencies");
    this.Detail.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable4
    });
    this.Detail.HeightF = 32f;
    this.Detail.Name = "Detail";
    componentResourceManager.ApplyResources((object) this.Detail, "Detail");
    componentResourceManager.ApplyResources((object) this.xrTable4, "xrTable4");
    this.xrTable4.Name = "xrTable4";
    this.xrTable4.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow5
    });
    this.xrTableRow5.Borders = BorderSide.Bottom;
    this.xrTableRow5.Cells.AddRange(new XRTableCell[6]
    {
      this.frequence,
      this.normalizedFrequence,
      this.frequenceResult,
      this.frequenceNoise,
      this.frequenceEnergy,
      this.frequenceFrames
    });
    this.xrTableRow5.Name = "xrTableRow5";
    this.xrTableRow5.StylePriority.UseBorders = false;
    componentResourceManager.ApplyResources((object) this.xrTableRow5, "xrTableRow5");
    this.xrTableRow5.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.frequence, "frequence");
    this.frequence.Name = "frequence";
    this.frequence.StylePriority.UseFont = false;
    this.frequence.StylePriority.UseTextAlignment = false;
    this.frequence.Weight = 0.36254643701452216;
    componentResourceManager.ApplyResources((object) this.normalizedFrequence, "normalizedFrequence");
    this.normalizedFrequence.Name = "normalizedFrequence";
    this.normalizedFrequence.StylePriority.UseFont = false;
    this.normalizedFrequence.StylePriority.UseTextAlignment = false;
    this.normalizedFrequence.Weight = 0.70114245114878837;
    componentResourceManager.ApplyResources((object) this.frequenceResult, "frequenceResult");
    this.frequenceResult.Name = "frequenceResult";
    this.frequenceResult.StylePriority.UseFont = false;
    this.frequenceResult.StylePriority.UseTextAlignment = false;
    this.frequenceResult.Weight = 0.62903963108958127;
    componentResourceManager.ApplyResources((object) this.frequenceNoise, "frequenceNoise");
    this.frequenceNoise.Name = "frequenceNoise";
    this.frequenceNoise.StylePriority.UseFont = false;
    this.frequenceNoise.StylePriority.UseTextAlignment = false;
    this.frequenceNoise.Weight = 0.36111065597559944;
    componentResourceManager.ApplyResources((object) this.frequenceEnergy, "frequenceEnergy");
    this.frequenceEnergy.Name = "frequenceEnergy";
    this.frequenceEnergy.StylePriority.UseFont = false;
    this.frequenceEnergy.StylePriority.UseTextAlignment = false;
    this.frequenceEnergy.Weight = 0.38832330561376677;
    componentResourceManager.ApplyResources((object) this.frequenceFrames, "frequenceFrames");
    this.frequenceFrames.Name = "frequenceFrames";
    this.frequenceFrames.StylePriority.UseFont = false;
    this.frequenceFrames.StylePriority.UseTextAlignment = false;
    this.frequenceFrames.Weight = 0.51783751915774212;
    this.frequenciesHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable2
    });
    this.frequenciesHeader.HeightF = 26f;
    this.frequenciesHeader.Name = "frequenciesHeader";
    componentResourceManager.ApplyResources((object) this.frequenciesHeader, "frequenciesHeader");
    componentResourceManager.ApplyResources((object) this.xrTable2, "xrTable2");
    this.xrTable2.Name = "xrTable2";
    this.xrTable2.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow4
    });
    this.xrTableRow4.Borders = BorderSide.Bottom;
    this.xrTableRow4.Cells.AddRange(new XRTableCell[6]
    {
      this.xrTableCell9,
      this.xrTableCell10,
      this.xrTableCell12,
      this.xrTableCell13,
      this.xrTableCell14,
      this.xrTableCell15
    });
    this.xrTableRow4.Name = "xrTableRow4";
    this.xrTableRow4.StylePriority.UseBorders = false;
    componentResourceManager.ApplyResources((object) this.xrTableRow4, "xrTableRow4");
    this.xrTableRow4.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCell9, "xrTableCell9");
    this.xrTableCell9.Name = "xrTableCell9";
    this.xrTableCell9.StylePriority.UseFont = false;
    this.xrTableCell9.StylePriority.UseTextAlignment = false;
    this.xrTableCell9.Weight = 0.36254643701452216;
    componentResourceManager.ApplyResources((object) this.xrTableCell10, "xrTableCell10");
    this.xrTableCell10.Name = "xrTableCell10";
    this.xrTableCell10.StylePriority.UseFont = false;
    this.xrTableCell10.StylePriority.UseTextAlignment = false;
    this.xrTableCell10.Weight = 0.70114245114878837;
    componentResourceManager.ApplyResources((object) this.xrTableCell12, "xrTableCell12");
    this.xrTableCell12.Name = "xrTableCell12";
    this.xrTableCell12.StylePriority.UseFont = false;
    this.xrTableCell12.StylePriority.UseTextAlignment = false;
    this.xrTableCell12.Weight = 0.62903963108958127;
    componentResourceManager.ApplyResources((object) this.xrTableCell13, "xrTableCell13");
    this.xrTableCell13.Name = "xrTableCell13";
    this.xrTableCell13.StylePriority.UseFont = false;
    this.xrTableCell13.StylePriority.UseTextAlignment = false;
    this.xrTableCell13.Weight = 0.36111065597559944;
    componentResourceManager.ApplyResources((object) this.xrTableCell14, "xrTableCell14");
    this.xrTableCell14.Name = "xrTableCell14";
    this.xrTableCell14.StylePriority.UseFont = false;
    this.xrTableCell14.StylePriority.UseTextAlignment = false;
    this.xrTableCell14.Weight = 0.38832330561376677;
    componentResourceManager.ApplyResources((object) this.xrTableCell15, "xrTableCell15");
    this.xrTableCell15.Name = "xrTableCell15";
    this.xrTableCell15.StylePriority.UseFont = false;
    this.xrTableCell15.StylePriority.UseTextAlignment = false;
    this.xrTableCell15.Weight = 0.51783751915774212;
    this.Bands.AddRange(new Band[7]
    {
      (Band) this.testDetailOverviewDetails,
      (Band) this.topMarginBand1,
      (Band) this.bottomMarginBand1,
      (Band) this.testDetailOverviewHeader,
      (Band) this.testCommentOverview,
      (Band) this.PageHeader,
      (Band) this.frequencies
    });
    this.Margins = new System.Drawing.Printing.Margins(100, 76, 100, 100);
    this.Version = "9.3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BeforePrint += new PrintEventHandler(this.DpoaeDetailSubSubReport_BeforePrint);
    ((ISupportInitialize) this.dpoaePassAxisX).EndInit();
    ((ISupportInitialize) this.dpoaeFailAxisX).EndInit();
    ((ISupportInitialize) secondaryAxisY).EndInit();
    ((ISupportInitialize) this.xyDiagram1).EndInit();
    ((ISupportInitialize) sideBarSeriesLabel1).EndInit();
    ((ISupportInitialize) sideBarSeriesView1).EndInit();
    ((ISupportInitialize) series1).EndInit();
    ((ISupportInitialize) sideBarSeriesLabel2).EndInit();
    ((ISupportInitialize) sideBarSeriesView2).EndInit();
    ((ISupportInitialize) series2).EndInit();
    ((ISupportInitialize) sideBarSeriesLabel3).EndInit();
    ((ISupportInitialize) sideBarSeriesView3).EndInit();
    ((ISupportInitialize) series3).EndInit();
    ((ISupportInitialize) sideBarSeriesLabel4).EndInit();
    this.ResultGraph.EndInit();
    ((ISupportInitialize) xyDiagram).EndInit();
    ((ISupportInitialize) pointSeriesLabel1).EndInit();
    ((ISupportInitialize) splineSeriesView1).EndInit();
    ((ISupportInitialize) series4).EndInit();
    ((ISupportInitialize) pointSeriesLabel2).EndInit();
    ((ISupportInitialize) splineSeriesView2).EndInit();
    this.calibrationGraph.EndInit();
    this.bestTestOverviewTable.EndInit();
    this.xrTable1.EndInit();
    this.xrTableCommentHeader.EndInit();
    this.xrTable3.EndInit();
    this.xrTable4.EndInit();
    this.xrTable2.EndInit();
    this.EndInit();
  }

  internal class Frequencies
  {
    public int Frequence { get; set; }

    public double NormalizedFrequence { get; set; }

    public string Result { get; set; }

    public double Noise { get; set; }

    public double Energy { get; set; }

    public int Frames { get; set; }
  }

  internal class TestDataPrintElement
  {
    public List<Comment> Comment { get; set; }
  }
}
