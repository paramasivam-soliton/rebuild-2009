// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Report.AbrDetailSubSubReport
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PathMedical.AudiologyTest;
using PathMedical.AudiologyTest.Properties;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Report;

public class AbrDetailSubSubReport : XtraReport, ITestDetailSubReport
{
  private Series curve;
  private Series zeroPointLine;
  private Series plusSigmaLine;
  private Series minusSigmaLine;
  private Series leftMarker;
  private Series rightMarker;
  private Series calibrationCurve;
  private Series calibrationZeroPointLine;
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
  private XRLabel xrLabelEeg;
  private DevExpressProgressBar devExpressProgressBarEeg;
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
  private XRLabel xrLabel2;
  private XRChart calibrationGraph;
  private XRLabel xrLabel1;
  private XRLabel presetNameValue;
  private XRLabel presetName;
  private XRLabel Progress;
  private DevExpressProgressBar devExpressProgressBarProgress;
  private XRLabel abr;
  private DevExpressProgressBar devExpressProgressBarAbr;
  private XRLabel framesValue;
  private XRLabel frames;
  private XRLabel impedance;
  private XRPictureBox impedanceRed;
  private XRPictureBox impedanceWhite;
  private XRLabel impedanceRedValue;
  private XRLabel impedanceWhiteValue;
  private PageHeaderBand PageHeader;
  private XRLabel xrLabel3;
  private XRLabel energyValue;
  private XRLabel energy;
  private XRLabel deviceNameValue;
  private XRLabel deviceName;
  private XRLabel firmwareValue;
  private XRLabel probeCalibrationDateValue;
  private XRLabel firmwareBuild;
  private XRLabel probeCalibrationDate;

  public Guid? TestId { get; set; }

  public AbrDetailSubSubReport()
  {
    this.InitializeComponent();
    this.impedanceRed.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("impdance_red") as Image;
    this.impedanceWhite.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("impdance_white") as Image;
  }

  private void AbrDetailSubSubReport_BeforePrint(object sender, PrintEventArgs e)
  {
    if (!this.TestId.HasValue)
      return;
    AbrTestInformation abrTestInformation = AbrComponent.Instance.Get(this.TestId.Value);
    List<TestDataPrintElement> dataSource = new List<TestDataPrintElement>();
    dataSource.Add(new TestDataPrintElement()
    {
      Comment = AbrDetailSubSubReport.LoadComments(abrTestInformation)
    });
    this.BindTestResult(abrTestInformation);
    this.CreateGraph(abrTestInformation);
    this.CreateResultInformation(abrTestInformation);
    if (abrTestInformation.Comments.Count<Comment>() > 0)
    {
      this.BindTestComment(dataSource);
    }
    else
    {
      this.xrLabelComments.Visible = false;
      this.xrTableCommentHeader.Visible = false;
    }
  }

  private static List<Comment> LoadComments(AbrTestInformation abrTestInformation)
  {
    return abrTestInformation.Comments.ToList<Comment>();
  }

  private void BindTestComment(List<TestDataPrintElement> dataSource)
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

  private void CreateResultInformation(AbrTestInformation abrTestInformation)
  {
    XRBinding binding1 = abrTestInformation != null ? new XRBinding("Text", (object) abrTestInformation, "TestName") : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("TestInformation");
    XRBinding binding2 = new XRBinding("Text", (object) abrTestInformation, "Frames");
    XRBinding binding3 = new XRBinding("Text", (object) abrTestInformation, "Energy");
    this.impedanceWhiteValue.Text = $"{Math.Round((double) abrTestInformation.Impedances[0] / 100.0)}kΩ";
    this.impedanceRedValue.Text = $"{Math.Round((double) abrTestInformation.Impedances[1] / 100.0)}kΩ";
    this.presetNameValue.DataBindings.Add(binding1);
    if (abrTestInformation.Eeg.HasValue)
      this.devExpressProgressBarEeg.Position = (float) abrTestInformation.Eeg.Value;
    int? nullable = abrTestInformation.Progress;
    if (nullable.HasValue)
    {
      DevExpressProgressBar progressBarProgress = this.devExpressProgressBarProgress;
      nullable = abrTestInformation.Progress;
      double num = (double) nullable.Value;
      progressBarProgress.Position = (float) num;
    }
    nullable = abrTestInformation.Abr;
    int num1 = 100;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      this.devExpressProgressBarAbr.Position = 100f;
    }
    else
    {
      nullable = abrTestInformation.Abr;
      if (nullable.HasValue)
      {
        DevExpressProgressBar expressProgressBarAbr = this.devExpressProgressBarAbr;
        nullable = abrTestInformation.Abr;
        double num2 = (double) nullable.Value;
        expressProgressBarAbr.Position = (float) num2;
      }
    }
    this.framesValue.DataBindings.Add(binding2);
    this.energyValue.DataBindings.Add(binding3);
    if (abrTestInformation.InstrumentSerialNumber.HasValue)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == abrTestInformation.InstrumentSerialNumber.ToString()));
      if (instrument != null)
        this.deviceNameValue.Text = instrument.Name;
    }
    nullable = abrTestInformation.FirmwareVersion;
    if (nullable.HasValue)
    {
      XRLabel firmwareValue = this.firmwareValue;
      nullable = abrTestInformation.FirmwareVersion;
      string str = nullable.ToString();
      firmwareValue.Text = str;
    }
    DateTime? probeCalibrationDate = abrTestInformation.ProbeCalibrationDate;
    if (!probeCalibrationDate.HasValue)
      return;
    XRLabel calibrationDateValue = this.probeCalibrationDateValue;
    probeCalibrationDate = abrTestInformation.ProbeCalibrationDate;
    string str1 = probeCalibrationDate.ToString();
    calibrationDateValue.Text = str1;
  }

  private void CreateGraph(AbrTestInformation abrTestInformation)
  {
    if (abrTestInformation == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (abrTestInformation));
    this.CreateChartControls();
    int? frames = abrTestInformation.Frames;
    double? nullable1;
    if (frames.HasValue)
    {
      frames = abrTestInformation.Frames;
      int num1 = 0;
      if (frames.GetValueOrDefault() > num1 & frames.HasValue && abrTestInformation.Graph != null)
      {
        for (int index = 0; index < abrTestInformation.Graph.Length; ++index)
        {
          SeriesPointCollection points = this.curve.Points;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (ValueType) index;
          object[] objArray = new object[1];
          nullable1 = abrTestInformation.GraphScale;
          double num2 = (double) abrTestInformation.Graph[index];
          double? nullable2 = nullable1.HasValue ? new double?(nullable1.GetValueOrDefault() * num2) : new double?();
          double num3 = 7.0;
          double? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new double?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new double?(nullable2.GetValueOrDefault() * num3);
          objArray[0] = (object) nullable3;
          SeriesPoint point = new SeriesPoint((object) local, objArray);
          points.Add(point);
          this.zeroPointLine.Points.Add(new SeriesPoint((double) index, new double[1]));
          if (index >= 75)
          {
            this.plusSigmaLine.Points.Add(new SeriesPoint((double) index, new double[1]
            {
              21.0
            }));
            this.minusSigmaLine.Points.Add(new SeriesPoint((double) index, new double[1]
            {
              -21.0
            }));
          }
        }
        for (int index = -21; index <= 21; ++index)
          this.leftMarker.Points.Add(new SeriesPoint(75.0, new double[1]
          {
            (double) index
          }));
        for (int index = -21; index <= 21; ++index)
          this.rightMarker.Points.Add(new SeriesPoint((double) (abrTestInformation.Graph.Length - 1), new double[1]
          {
            (double) index
          }));
      }
    }
    if (abrTestInformation.CalibrationGraph != null)
    {
      for (int index = 0; index < abrTestInformation.CalibrationGraph.Length; ++index)
      {
        SeriesPointCollection points = this.calibrationCurve.Points;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) index;
        object[] objArray = new object[1];
        double? nullable4 = abrTestInformation.CalibrationGraphScale;
        double num4 = (double) abrTestInformation.CalibrationGraph[index];
        nullable1 = nullable4.HasValue ? new double?(nullable4.GetValueOrDefault() * num4) : new double?();
        double num5 = 256.0;
        double? nullable5;
        if (!nullable1.HasValue)
        {
          nullable4 = new double?();
          nullable5 = nullable4;
        }
        else
          nullable5 = new double?(nullable1.GetValueOrDefault() / num5);
        double? nullable6 = nullable5;
        double num6 = 2.0;
        double? nullable7;
        if (!nullable6.HasValue)
        {
          nullable1 = new double?();
          nullable7 = nullable1;
        }
        else
          nullable7 = new double?(nullable6.GetValueOrDefault() * num6);
        objArray[0] = (object) nullable7;
        SeriesPoint point = new SeriesPoint((object) local, objArray);
        points.Add(point);
        this.calibrationZeroPointLine.Points.Add(new SeriesPoint((double) index, new double[1]));
      }
    }
    this.ResultGraph.Legend.Visible = false;
    this.ResultGraph.Series.Add(this.zeroPointLine);
    this.ResultGraph.Series.Add(this.leftMarker);
    this.ResultGraph.Series.Add(this.plusSigmaLine);
    this.ResultGraph.Series.Add(this.minusSigmaLine);
    this.ResultGraph.Series.Add(this.rightMarker);
    this.ResultGraph.Series.Add(this.curve);
    this.calibrationGraph.Legend.Visible = false;
    this.calibrationGraph.Series.Add(this.calibrationZeroPointLine);
    this.calibrationGraph.Series.Add(this.calibrationCurve);
  }

  private void CreateChartControls()
  {
    this.ResultGraph.Series.Clear();
    this.calibrationGraph.Series.Clear();
    Series series1 = new Series("Measurement", ViewType.Spline);
    series1.Label.Visible = false;
    this.curve = series1;
    ((LineSeriesView) this.curve.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.curve.View).LineStyle.Thickness = 1;
    this.curve.View.Color = Color.Blue;
    Series series2 = new Series("ZeroPoint", ViewType.Line);
    series2.Label.Visible = false;
    this.zeroPointLine = series2;
    ((LineSeriesView) this.zeroPointLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.zeroPointLine.View).LineStyle.Thickness = 1;
    this.zeroPointLine.View.Color = Color.Black;
    Series series3 = new Series("plusSigmaLine", ViewType.Line);
    series3.Label.Visible = false;
    this.plusSigmaLine = series3;
    ((LineSeriesView) this.plusSigmaLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.plusSigmaLine.View).LineStyle.Thickness = 1;
    this.plusSigmaLine.View.Color = Color.LightGreen;
    Series series4 = new Series("minusSigmaLine", ViewType.Line);
    series4.Label.Visible = false;
    this.minusSigmaLine = series4;
    ((LineSeriesView) this.minusSigmaLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.minusSigmaLine.View).LineStyle.Thickness = 1;
    this.minusSigmaLine.View.Color = Color.LightGreen;
    Series series5 = new Series("leftMarker", ViewType.Line);
    series5.Label.Visible = false;
    this.leftMarker = series5;
    ((LineSeriesView) this.leftMarker.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.leftMarker.View).LineStyle.Thickness = 1;
    this.leftMarker.View.Color = Color.LightGreen;
    Series series6 = new Series("rightMarker", ViewType.Line);
    series6.Label.Visible = false;
    this.rightMarker = series6;
    ((LineSeriesView) this.rightMarker.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.rightMarker.View).LineStyle.Thickness = 1;
    this.rightMarker.View.Color = Color.LightGreen;
    Series series7 = new Series("Calibration", ViewType.Spline);
    series7.Label.Visible = false;
    this.calibrationCurve = series7;
    ((LineSeriesView) this.calibrationCurve.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.calibrationCurve.View).LineStyle.Thickness = 1;
    this.calibrationCurve.View.Color = Color.Blue;
    Series series8 = new Series("ZeroPoint", ViewType.Line);
    series8.Label.Visible = false;
    this.calibrationZeroPointLine = series8;
    ((LineSeriesView) this.calibrationZeroPointLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.calibrationZeroPointLine.View).LineStyle.Thickness = 1;
    this.calibrationZeroPointLine.View.Color = Color.Black;
  }

  private void BindTestResult(AbrTestInformation abrTestInformation)
  {
    this.bestTestOverviewTestType.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestType.ABR) as string;
    int? nullable1;
    if (abrTestInformation.Ear.HasValue)
    {
      nullable1 = abrTestInformation.Ear;
      if (nullable1.GetValueOrDefault() == 7)
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.LeftEar) as string;
      else
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.RightEar) as string;
    }
    DateTime? testTimeStamp = abrTestInformation.TestTimeStamp;
    if (testTimeStamp.HasValue)
    {
      XRTableCell overviewTestDateAndTime = this.bestTestOverviewTestDateAndTime;
      testTimeStamp = abrTestInformation.TestTimeStamp;
      string str = testTimeStamp.ToString();
      overviewTestDateAndTime.Text = str;
    }
    nullable1 = abrTestInformation.TestResult;
    if (nullable1.HasValue)
    {
      XRTableCell overviewTestResult = this.bestTestOverviewTestResult;
      GlobalResourceEnquirer instance = GlobalResourceEnquirer.Instance;
      nullable1 = abrTestInformation.TestResult;
      // ISSUE: variable of a boxed type
      __Boxed<T1077AudiologyTestResult> resourceName = (Enum) (T1077AudiologyTestResult) nullable1.Value;
      string resourceByName = instance.GetResourceByName((Enum) resourceName) as string;
      overviewTestResult.Text = resourceByName;
    }
    nullable1 = abrTestInformation.Duration;
    if (nullable1.HasValue)
    {
      XRTableCell overviewDuration = this.bestTestOverviewDuration;
      DateTime dateTime = new DateTime();
      nullable1 = abrTestInformation.Duration;
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) nullable1.Value);
      string str = $"{dateTime + timeSpan:T}";
      overviewDuration.Text = str;
    }
    long? nullable2 = abrTestInformation.InstrumentSerialNumber;
    if (nullable2.HasValue)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == abrTestInformation.InstrumentSerialNumber.ToString()));
      if (instrument != null)
        this.bestTestOverviewInstrument.Text = instrument.SerialNumber;
    }
    nullable2 = abrTestInformation.ProbeSerialNumber;
    if (nullable2.HasValue)
    {
      XRTableCell testOverviewProbe = this.bestTestOverviewProbe;
      nullable2 = abrTestInformation.ProbeSerialNumber;
      string str = nullable2.ToString();
      testOverviewProbe.Text = str;
    }
    if (!abrTestInformation.UserAccountId.HasValue)
      return;
    User user = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
    {
      Guid id = u.Id;
      Guid? userAccountId = abrTestInformation.UserAccountId;
      return userAccountId.HasValue && id == userAccountId.GetValueOrDefault();
    }));
    if (user == null)
      return;
    this.bestTestOverviewExaminer.Text = user.LoginName;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AbrDetailSubSubReport));
    XYDiagram xyDiagram1 = new XYDiagram();
    Series series1 = new Series();
    PointSeriesLabel pointSeriesLabel1 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView1 = new SplineSeriesView();
    PointSeriesLabel pointSeriesLabel2 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView2 = new SplineSeriesView();
    XYDiagram xyDiagram2 = new XYDiagram();
    Series series2 = new Series();
    PointSeriesLabel pointSeriesLabel3 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView3 = new SplineSeriesView();
    PointSeriesLabel pointSeriesLabel4 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView4 = new SplineSeriesView();
    this.ResultGraph = new XRChart();
    this.testDetailOverviewDetails = new DetailBand();
    this.firmwareValue = new XRLabel();
    this.probeCalibrationDateValue = new XRLabel();
    this.firmwareBuild = new XRLabel();
    this.probeCalibrationDate = new XRLabel();
    this.deviceNameValue = new XRLabel();
    this.deviceName = new XRLabel();
    this.energyValue = new XRLabel();
    this.energy = new XRLabel();
    this.impedanceWhiteValue = new XRLabel();
    this.impedanceRedValue = new XRLabel();
    this.impedanceRed = new XRPictureBox();
    this.impedance = new XRLabel();
    this.framesValue = new XRLabel();
    this.frames = new XRLabel();
    this.abr = new XRLabel();
    this.devExpressProgressBarAbr = new DevExpressProgressBar();
    this.Progress = new XRLabel();
    this.devExpressProgressBarProgress = new DevExpressProgressBar();
    this.presetNameValue = new XRLabel();
    this.presetName = new XRLabel();
    this.xrLabel2 = new XRLabel();
    this.calibrationGraph = new XRChart();
    this.bestTestOverviewTable = new XRTable();
    this.bestTestOverviewDetailRow = new XRTableRow();
    this.bestTestOverviewTestType = new XRTableCell();
    this.bestTestOverviewTestEar = new XRTableCell();
    this.bestTestOverviewTestDateAndTime = new XRTableCell();
    this.bestTestOverviewTestResult = new XRTableCell();
    this.bestTestOverviewDuration = new XRTableCell();
    this.bestTestOverviewInstrument = new XRTableCell();
    this.bestTestOverviewProbe = new XRTableCell();
    this.bestTestOverviewExaminer = new XRTableCell();
    this.xrLabelEeg = new XRLabel();
    this.devExpressProgressBarEeg = new DevExpressProgressBar();
    this.xrLabel1 = new XRLabel();
    this.impedanceWhite = new XRPictureBox();
    this.topMarginBand1 = new TopMarginBand();
    this.bottomMarginBand1 = new BottomMarginBand();
    this.testDetailOverviewHeader = new GroupHeaderBand();
    this.xrTable1 = new XRTable();
    this.xrTableRow1 = new XRTableRow();
    this.xrTableCell1 = new XRTableCell();
    this.xrTableCell2 = new XRTableCell();
    this.xrTableCell3 = new XRTableCell();
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
    this.xrLabel3 = new XRLabel();
    this.ResultGraph.BeginInit();
    ((ISupportInitialize) xyDiagram1).BeginInit();
    ((ISupportInitialize) series1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel1).BeginInit();
    ((ISupportInitialize) splineSeriesView1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel2).BeginInit();
    ((ISupportInitialize) splineSeriesView2).BeginInit();
    this.calibrationGraph.BeginInit();
    ((ISupportInitialize) xyDiagram2).BeginInit();
    ((ISupportInitialize) series2).BeginInit();
    ((ISupportInitialize) pointSeriesLabel3).BeginInit();
    ((ISupportInitialize) splineSeriesView3).BeginInit();
    ((ISupportInitialize) pointSeriesLabel4).BeginInit();
    ((ISupportInitialize) splineSeriesView4).BeginInit();
    this.bestTestOverviewTable.BeginInit();
    this.xrTable1.BeginInit();
    this.xrTableCommentHeader.BeginInit();
    this.xrTable3.BeginInit();
    this.BeginInit();
    componentResourceManager.ApplyResources((object) this.ResultGraph, "ResultGraph");
    this.ResultGraph.Borders = BorderSide.None;
    xyDiagram1.AxisX.Range.Auto = false;
    xyDiagram1.AxisX.Range.MaxValueSerializable = "200";
    xyDiagram1.AxisX.Range.MinValueSerializable = "0";
    xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram1.AxisX.Visible = false;
    xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram1.AxisY.GridLines.Visible = false;
    xyDiagram1.AxisY.Range.Auto = false;
    xyDiagram1.AxisY.Range.MaxValueSerializable = "30";
    xyDiagram1.AxisY.Range.MinValueSerializable = "-30";
    xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram1.AxisY.Visible = false;
    xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram1.DefaultPane.BorderVisible = false;
    this.ResultGraph.Diagram = (Diagram) xyDiagram1;
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
    pointSeriesLabel1.LineVisible = true;
    pointSeriesLabel1.Visible = false;
    series1.Label = (SeriesLabelBase) pointSeriesLabel1;
    componentResourceManager.ApplyResources((object) series1, "series1");
    splineSeriesView1.LineMarkerOptions.Visible = false;
    series1.View = (SeriesViewBase) splineSeriesView1;
    this.ResultGraph.SeriesSerializable = new Series[1]
    {
      series1
    };
    pointSeriesLabel2.LineVisible = true;
    this.ResultGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel2;
    this.ResultGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView2;
    this.testDetailOverviewDetails.Controls.AddRange(new XRControl[28]
    {
      (XRControl) this.firmwareValue,
      (XRControl) this.probeCalibrationDateValue,
      (XRControl) this.firmwareBuild,
      (XRControl) this.probeCalibrationDate,
      (XRControl) this.deviceNameValue,
      (XRControl) this.deviceName,
      (XRControl) this.energyValue,
      (XRControl) this.energy,
      (XRControl) this.impedanceWhiteValue,
      (XRControl) this.impedanceRedValue,
      (XRControl) this.impedanceRed,
      (XRControl) this.impedance,
      (XRControl) this.framesValue,
      (XRControl) this.frames,
      (XRControl) this.abr,
      (XRControl) this.devExpressProgressBarAbr,
      (XRControl) this.Progress,
      (XRControl) this.devExpressProgressBarProgress,
      (XRControl) this.presetNameValue,
      (XRControl) this.presetName,
      (XRControl) this.xrLabel2,
      (XRControl) this.calibrationGraph,
      (XRControl) this.bestTestOverviewTable,
      (XRControl) this.ResultGraph,
      (XRControl) this.xrLabelEeg,
      (XRControl) this.devExpressProgressBarEeg,
      (XRControl) this.xrLabel1,
      (XRControl) this.impedanceWhite
    });
    componentResourceManager.ApplyResources((object) this.testDetailOverviewDetails, "testDetailOverviewDetails");
    this.testDetailOverviewDetails.HeightF = 532f;
    this.testDetailOverviewDetails.Name = "testDetailOverviewDetails";
    this.testDetailOverviewDetails.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.testDetailOverviewDetails.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.firmwareValue, "firmwareValue");
    this.firmwareValue.Name = "firmwareValue";
    this.firmwareValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.firmwareValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDateValue, "probeCalibrationDateValue");
    this.probeCalibrationDateValue.Name = "probeCalibrationDateValue";
    this.probeCalibrationDateValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.probeCalibrationDateValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.firmwareBuild, "firmwareBuild");
    this.firmwareBuild.Name = "firmwareBuild";
    this.firmwareBuild.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.firmwareBuild.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDate, "probeCalibrationDate");
    this.probeCalibrationDate.Name = "probeCalibrationDate";
    this.probeCalibrationDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.probeCalibrationDate.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.deviceNameValue, "deviceNameValue");
    this.deviceNameValue.Name = "deviceNameValue";
    this.deviceNameValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.deviceNameValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.deviceName, "deviceName");
    this.deviceName.Name = "deviceName";
    this.deviceName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.deviceName.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.energyValue, "energyValue");
    this.energyValue.Name = "energyValue";
    this.energyValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.energyValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.energy, "energy");
    this.energy.Name = "energy";
    this.energy.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.energy.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.impedanceWhiteValue, "impedanceWhiteValue");
    this.impedanceWhiteValue.Name = "impedanceWhiteValue";
    this.impedanceWhiteValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.impedanceWhiteValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.impedanceRedValue, "impedanceRedValue");
    this.impedanceRedValue.Name = "impedanceRedValue";
    this.impedanceRedValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.impedanceRedValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.impedanceRed, "impedanceRed");
    this.impedanceRed.Name = "impedanceRed";
    this.impedanceRed.Sizing = ImageSizeMode.ZoomImage;
    componentResourceManager.ApplyResources((object) this.impedance, "impedance");
    this.impedance.Name = "impedance";
    this.impedance.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.impedance.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.framesValue, "framesValue");
    this.framesValue.Name = "framesValue";
    this.framesValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.framesValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.frames, "frames");
    this.frames.Name = "frames";
    this.frames.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.frames.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.abr, "abr");
    this.abr.Name = "abr";
    this.abr.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.abr.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarAbr, "devExpressProgressBarAbr");
    this.devExpressProgressBarAbr.Borders = BorderSide.None;
    this.devExpressProgressBarAbr.MaxValue = 100f;
    this.devExpressProgressBarAbr.Name = "devExpressProgressBarAbr";
    this.devExpressProgressBarAbr.Position = 0.0f;
    this.devExpressProgressBarAbr.StylePriority.UseBackColor = false;
    this.devExpressProgressBarAbr.StylePriority.UseBorders = false;
    this.devExpressProgressBarAbr.StylePriority.UseForeColor = false;
    this.devExpressProgressBarAbr.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.Progress, "Progress");
    this.Progress.Name = "Progress";
    this.Progress.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.Progress.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarProgress, "devExpressProgressBarProgress");
    this.devExpressProgressBarProgress.Borders = BorderSide.None;
    this.devExpressProgressBarProgress.MaxValue = 100f;
    this.devExpressProgressBarProgress.Name = "devExpressProgressBarProgress";
    this.devExpressProgressBarProgress.Position = 0.0f;
    this.devExpressProgressBarProgress.StylePriority.UseBackColor = false;
    this.devExpressProgressBarProgress.StylePriority.UseBorders = false;
    this.devExpressProgressBarProgress.StylePriority.UseForeColor = false;
    this.devExpressProgressBarProgress.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.presetNameValue, "presetNameValue");
    this.presetNameValue.Name = "presetNameValue";
    this.presetNameValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.presetNameValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.presetName, "presetName");
    this.presetName.Name = "presetName";
    this.presetName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.presetName.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabel2, "xrLabel2");
    this.xrLabel2.Name = "xrLabel2";
    this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel2.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.calibrationGraph, "calibrationGraph");
    this.calibrationGraph.Borders = BorderSide.None;
    xyDiagram2.AxisX.Range.Auto = false;
    xyDiagram2.AxisX.Range.MaxValueSerializable = "200";
    xyDiagram2.AxisX.Range.MinValueSerializable = "0";
    xyDiagram2.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram2.AxisX.Visible = false;
    xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram2.AxisY.GridLines.Visible = false;
    xyDiagram2.AxisY.Range.Auto = false;
    xyDiagram2.AxisY.Range.MaxValueSerializable = "30";
    xyDiagram2.AxisY.Range.MinValueSerializable = "-30";
    xyDiagram2.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram2.AxisY.Visible = false;
    xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram2.DefaultPane.BorderVisible = false;
    this.calibrationGraph.Diagram = (Diagram) xyDiagram2;
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
    pointSeriesLabel3.LineVisible = true;
    pointSeriesLabel3.Visible = false;
    series2.Label = (SeriesLabelBase) pointSeriesLabel3;
    componentResourceManager.ApplyResources((object) series2, "series2");
    splineSeriesView3.LineMarkerOptions.Visible = false;
    series2.View = (SeriesViewBase) splineSeriesView3;
    this.calibrationGraph.SeriesSerializable = new Series[1]
    {
      series2
    };
    pointSeriesLabel4.LineVisible = true;
    this.calibrationGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel4;
    this.calibrationGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView4;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTable, "bestTestOverviewTable");
    this.bestTestOverviewTable.Name = "bestTestOverviewTable";
    this.bestTestOverviewTable.Rows.AddRange(new XRTableRow[1]
    {
      this.bestTestOverviewDetailRow
    });
    this.bestTestOverviewDetailRow.Cells.AddRange(new XRTableCell[8]
    {
      this.bestTestOverviewTestType,
      this.bestTestOverviewTestEar,
      this.bestTestOverviewTestDateAndTime,
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
    this.bestTestOverviewTestDateAndTime.Weight = 0.58863938552871908;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestResult, "bestTestOverviewTestResult");
    this.bestTestOverviewTestResult.Name = "bestTestOverviewTestResult";
    this.bestTestOverviewTestResult.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewTestResult.StylePriority.UseFont = false;
    this.bestTestOverviewTestResult.StylePriority.UsePadding = false;
    this.bestTestOverviewTestResult.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestResult.Weight = 0.40801424566936906;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDuration, "bestTestOverviewDuration");
    this.bestTestOverviewDuration.Name = "bestTestOverviewDuration";
    this.bestTestOverviewDuration.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewDuration.StylePriority.UseFont = false;
    this.bestTestOverviewDuration.StylePriority.UsePadding = false;
    this.bestTestOverviewDuration.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewDuration.Weight = 0.27792494294934078;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewInstrument, "bestTestOverviewInstrument");
    this.bestTestOverviewInstrument.Name = "bestTestOverviewInstrument";
    this.bestTestOverviewInstrument.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewInstrument.StylePriority.UseFont = false;
    this.bestTestOverviewInstrument.StylePriority.UsePadding = false;
    this.bestTestOverviewInstrument.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewInstrument.Weight = 0.42997773134076323;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewProbe, "bestTestOverviewProbe");
    this.bestTestOverviewProbe.Name = "bestTestOverviewProbe";
    this.bestTestOverviewProbe.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewProbe.StylePriority.UseFont = false;
    this.bestTestOverviewProbe.StylePriority.UsePadding = false;
    this.bestTestOverviewProbe.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewProbe.Weight = 0.30975224550009506;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewExaminer, "bestTestOverviewExaminer");
    this.bestTestOverviewExaminer.Name = "bestTestOverviewExaminer";
    this.bestTestOverviewExaminer.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewExaminer.StylePriority.UseFont = false;
    this.bestTestOverviewExaminer.StylePriority.UsePadding = false;
    this.bestTestOverviewExaminer.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewExaminer.Weight = 0.42567835227092832;
    componentResourceManager.ApplyResources((object) this.xrLabelEeg, "xrLabelEeg");
    this.xrLabelEeg.Name = "xrLabelEeg";
    this.xrLabelEeg.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelEeg.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarEeg, "devExpressProgressBarEeg");
    this.devExpressProgressBarEeg.Borders = BorderSide.None;
    this.devExpressProgressBarEeg.MaxValue = 100f;
    this.devExpressProgressBarEeg.Name = "devExpressProgressBarEeg";
    this.devExpressProgressBarEeg.Position = 0.0f;
    this.devExpressProgressBarEeg.StylePriority.UseBackColor = false;
    this.devExpressProgressBarEeg.StylePriority.UseBorders = false;
    this.devExpressProgressBarEeg.StylePriority.UseForeColor = false;
    this.devExpressProgressBarEeg.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrLabel1, "xrLabel1");
    this.xrLabel1.Name = "xrLabel1";
    this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel1.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.impedanceWhite, "impedanceWhite");
    this.impedanceWhite.Name = "impedanceWhite";
    this.impedanceWhite.Sizing = ImageSizeMode.ZoomImage;
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
    this.xrTableRow1.Cells.AddRange(new XRTableCell[8]
    {
      this.xrTableCell1,
      this.xrTableCell2,
      this.xrTableCell3,
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
    this.xrTableCell3.Weight = 0.58863938552871908;
    componentResourceManager.ApplyResources((object) this.xrTableCell4, "xrTableCell4");
    this.xrTableCell4.Name = "xrTableCell4";
    this.xrTableCell4.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell4.StylePriority.UseFont = false;
    this.xrTableCell4.StylePriority.UsePadding = false;
    this.xrTableCell4.StylePriority.UseTextAlignment = false;
    this.xrTableCell4.Weight = 0.40801424566936906;
    componentResourceManager.ApplyResources((object) this.xrTableCell5, "xrTableCell5");
    this.xrTableCell5.Name = "xrTableCell5";
    this.xrTableCell5.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell5.StylePriority.UseFont = false;
    this.xrTableCell5.StylePriority.UsePadding = false;
    this.xrTableCell5.StylePriority.UseTextAlignment = false;
    this.xrTableCell5.Weight = 0.27792494294934078;
    componentResourceManager.ApplyResources((object) this.xrTableCell6, "xrTableCell6");
    this.xrTableCell6.Name = "xrTableCell6";
    this.xrTableCell6.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell6.StylePriority.UseFont = false;
    this.xrTableCell6.StylePriority.UsePadding = false;
    this.xrTableCell6.StylePriority.UseTextAlignment = false;
    this.xrTableCell6.Weight = 0.42997773134076323;
    componentResourceManager.ApplyResources((object) this.xrTableCell7, "xrTableCell7");
    this.xrTableCell7.Name = "xrTableCell7";
    this.xrTableCell7.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell7.StylePriority.UseFont = false;
    this.xrTableCell7.StylePriority.UsePadding = false;
    this.xrTableCell7.StylePriority.UseTextAlignment = false;
    this.xrTableCell7.Weight = 0.30975224550009506;
    componentResourceManager.ApplyResources((object) this.xrTableCell8, "xrTableCell8");
    this.xrTableCell8.Name = "xrTableCell8";
    this.xrTableCell8.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell8.StylePriority.UseFont = false;
    this.xrTableCell8.StylePriority.UsePadding = false;
    this.xrTableCell8.StylePriority.UseTextAlignment = false;
    this.xrTableCell8.Weight = 0.42567835227092832;
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
    this.testCommentOverview.Level = 0;
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
      (XRControl) this.xrLabel3
    });
    this.PageHeader.HeightF = 48f;
    this.PageHeader.Name = "PageHeader";
    componentResourceManager.ApplyResources((object) this.PageHeader, "PageHeader");
    componentResourceManager.ApplyResources((object) this.xrLabel3, "xrLabel3");
    this.xrLabel3.Name = "xrLabel3";
    this.xrLabel3.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel3.StylePriority.UseFont = false;
    this.Bands.AddRange(new Band[6]
    {
      (Band) this.testDetailOverviewDetails,
      (Band) this.topMarginBand1,
      (Band) this.bottomMarginBand1,
      (Band) this.testDetailOverviewHeader,
      (Band) this.testCommentOverview,
      (Band) this.PageHeader
    });
    this.Margins = new System.Drawing.Printing.Margins(100, 76, 100, 100);
    this.Version = "9.3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BeforePrint += new PrintEventHandler(this.AbrDetailSubSubReport_BeforePrint);
    ((ISupportInitialize) xyDiagram1).EndInit();
    ((ISupportInitialize) pointSeriesLabel1).EndInit();
    ((ISupportInitialize) splineSeriesView1).EndInit();
    ((ISupportInitialize) series1).EndInit();
    ((ISupportInitialize) pointSeriesLabel2).EndInit();
    ((ISupportInitialize) splineSeriesView2).EndInit();
    this.ResultGraph.EndInit();
    ((ISupportInitialize) xyDiagram2).EndInit();
    ((ISupportInitialize) pointSeriesLabel3).EndInit();
    ((ISupportInitialize) splineSeriesView3).EndInit();
    ((ISupportInitialize) series2).EndInit();
    ((ISupportInitialize) pointSeriesLabel4).EndInit();
    ((ISupportInitialize) splineSeriesView4).EndInit();
    this.calibrationGraph.EndInit();
    this.bestTestOverviewTable.EndInit();
    this.xrTable1.EndInit();
    this.xrTableCommentHeader.EndInit();
    this.xrTable3.EndInit();
    this.EndInit();
  }
}
