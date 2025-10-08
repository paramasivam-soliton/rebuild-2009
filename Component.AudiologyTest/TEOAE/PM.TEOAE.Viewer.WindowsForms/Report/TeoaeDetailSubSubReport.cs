// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Viewer.WindowsForms.Report.TeoaeDetailSubSubReport
// Assembly: PM.TEOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83E8557F-96BD-4446-877A-A7FC2FA17A58
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.Viewer.WindowsForms.dll

using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PathMedical.AudiologyTest;
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
namespace PathMedical.TEOAE.Viewer.WindowsForms.Report;

public class TeoaeDetailSubSubReport : XtraReport, ITestDetailSubReport
{
  private Series curve;
  private Series zeroPointLine;
  private Series peaks;
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
  private XRChart calibrationGraph;
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
  private XRLabel xrLabelNoise;
  private DevExpressProgressBar devExpressProgressBarNoise;
  private XRLabel progress;
  private DevExpressProgressBar devExpressProgressBarProgress;
  private XRLabel teoae;
  private DevExpressProgressBar devExpressProgressBarTeoae;
  private XRLabel framesValue;
  private XRLabel frames;
  private XRLabel xrLabel2;
  private XRChart resultGraph;
  private XRLabel xrLabel1;
  private XRLabel energyValue;
  private XRLabel energy;
  private DetailReportBand testCommentOverview;
  private DetailBand testCommentDetails;
  private ReportHeaderBand testCommentHeader;
  private XRTable xrTableCommentHeader;
  private XRTableRow xrTableRow2;
  private XRTableCell xrTableCellCommentCreatedHeader;
  private XRTableCell xrTableCellCommentHeader;
  private XRTableCell xrTableCellCreatedByHeader;
  private XRLabel xrLabelComments;
  private XRTable xrTable3;
  private XRTableRow xrTableRow3;
  private XRTableCell xrTableCellCommentCreated;
  private XRTableCell xrTableCellComment;
  private XRTableCell xrTableCellCommenCreatedBy;
  private PageHeaderBand PageHeader;
  private XRLabel xrLabel3;
  private XRLabel deviceNameValue;
  private XRLabel deviceName;
  private XRLabel probeCalibrationDateValue;
  private XRLabel probeCalibrationDate;
  private XRLabel firmwareValue;
  private XRLabel firmwareBuild;

  public Guid? TestId { get; set; }

  public TeoaeDetailSubSubReport() => this.InitializeComponent();

  private void TeoaeDetailSubSubReport_BeforePrint(object sender, PrintEventArgs e)
  {
    if (!this.TestId.HasValue)
      return;
    TeoaeTestInformation teoaeTestInformation = TeoaeComponent.Instance.Get(this.TestId.Value);
    List<TestDataPrintElement> dataSource = new List<TestDataPrintElement>();
    dataSource.Add(new TestDataPrintElement()
    {
      Comment = TeoaeDetailSubSubReport.LoadComments(teoaeTestInformation)
    });
    this.BindTestResult(teoaeTestInformation);
    this.CreateGraph(teoaeTestInformation);
    this.CreateResultInformation(teoaeTestInformation);
    if (teoaeTestInformation.Comments.Count<Comment>() > 0)
    {
      this.BindTestComment(dataSource);
    }
    else
    {
      this.xrLabelComments.Visible = false;
      this.xrTableCommentHeader.Visible = false;
    }
  }

  private static List<Comment> LoadComments(TeoaeTestInformation teoaeTestInformation)
  {
    return teoaeTestInformation.Comments.ToList<Comment>();
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

  private void CreateResultInformation(TeoaeTestInformation teoaeTestInformation)
  {
    XRBinding binding1 = teoaeTestInformation != null ? new XRBinding("Text", (object) teoaeTestInformation, "Frames") : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("TestInformation");
    XRBinding binding2 = new XRBinding("Text", (object) teoaeTestInformation, "Energy");
    if (teoaeTestInformation.Noise.HasValue)
      this.devExpressProgressBarNoise.Position = (float) teoaeTestInformation.Noise.Value;
    int? nullable = teoaeTestInformation.Progress;
    if (nullable.HasValue)
    {
      DevExpressProgressBar progressBarProgress = this.devExpressProgressBarProgress;
      nullable = teoaeTestInformation.Progress;
      double num = (double) nullable.Value;
      progressBarProgress.Position = (float) num;
    }
    nullable = teoaeTestInformation.Teoae;
    if (nullable.HasValue)
    {
      DevExpressProgressBar progressBarTeoae = this.devExpressProgressBarTeoae;
      nullable = teoaeTestInformation.Teoae;
      double num = (double) nullable.Value;
      progressBarTeoae.Position = (float) num;
    }
    this.framesValue.DataBindings.Add(binding1);
    this.energyValue.DataBindings.Add(binding2);
    if (teoaeTestInformation.InstrumentSerialNumber.HasValue)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == teoaeTestInformation.InstrumentSerialNumber.ToString()));
      if (instrument != null)
        this.deviceNameValue.Text = instrument.Name;
    }
    nullable = teoaeTestInformation.FirmwareVersion;
    if (nullable.HasValue)
    {
      XRLabel firmwareValue = this.firmwareValue;
      nullable = teoaeTestInformation.FirmwareVersion;
      string str = nullable.ToString();
      firmwareValue.Text = str;
    }
    DateTime? probeCalibrationDate = teoaeTestInformation.ProbeCalibrationDate;
    if (!probeCalibrationDate.HasValue)
      return;
    XRLabel calibrationDateValue = this.probeCalibrationDateValue;
    probeCalibrationDate = teoaeTestInformation.ProbeCalibrationDate;
    string str1 = probeCalibrationDate.ToString();
    calibrationDateValue.Text = str1;
  }

  private void CreateGraph(TeoaeTestInformation teoaeTestInformation)
  {
    if (teoaeTestInformation == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("TestInformation");
    this.CreateChartControls();
    int index1 = 0;
    int? frames = teoaeTestInformation.Frames;
    double? nullable1;
    double? nullable2;
    if (frames.HasValue)
    {
      frames = teoaeTestInformation.Frames;
      int num1 = 0;
      if (frames.GetValueOrDefault() > num1 & frames.HasValue && teoaeTestInformation.Graph != null && teoaeTestInformation.GraphScale.HasValue)
      {
        nullable1 = teoaeTestInformation.GraphScale;
        double num2 = 0.0;
        if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
        {
          for (int index2 = 0; index2 < teoaeTestInformation.Graph.Length - 64 /*0x40*/; ++index2)
          {
            SeriesPointCollection points = this.curve.Points;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) index2;
            object[] objArray = new object[1];
            nullable2 = teoaeTestInformation.GraphScale;
            double num3 = (double) teoaeTestInformation.Graph[index2 + 64 /*0x40*/];
            nullable1 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num3) : new double?();
            double num4 = -1.0;
            double? nullable3;
            if (!nullable1.HasValue)
            {
              nullable2 = new double?();
              nullable3 = nullable2;
            }
            else
              nullable3 = new double?(nullable1.GetValueOrDefault() * num4);
            objArray[0] = (object) nullable3;
            SeriesPoint point = new SeriesPoint((object) local, objArray);
            points.Add(point);
            this.zeroPointLine.Points.Add(new SeriesPoint((double) index2, new double[1]));
            if (index2 > 15)
            {
              this.plusSigmaLine.Points.Add(new SeriesPoint((double) index2, new double[1]
              {
                3.0
              }));
              this.minusSigmaLine.Points.Add(new SeriesPoint((double) index2, new double[1]
              {
                -3.0
              }));
            }
          }
          for (int index3 = -3; index3 <= 3; ++index3)
            this.leftMarker.Points.Add(new SeriesPoint(16.0, new double[1]
            {
              (double) index3
            }));
          for (int index4 = -3; index4 <= 3; ++index4)
            this.rightMarker.Points.Add(new SeriesPoint((double) (teoaeTestInformation.Graph.Length - 65), new double[1]
            {
              (double) index4
            }));
          if (teoaeTestInformation.PeakIndex != null)
          {
            for (; teoaeTestInformation.PeakIndex[index1] != (byte) 0; ++index1)
            {
              int num5 = (int) teoaeTestInformation.PeakIndex[index1] - 64 /*0x40*/;
              SeriesPointCollection points = this.peaks.Points;
              // ISSUE: variable of a boxed type
              __Boxed<int> local = (ValueType) num5;
              object[] objArray = new object[1];
              nullable2 = teoaeTestInformation.GraphScale;
              double num6 = (double) teoaeTestInformation.Graph[num5 + 64 /*0x40*/];
              nullable1 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num6) : new double?();
              double num7 = -1.0;
              double? nullable4;
              if (!nullable1.HasValue)
              {
                nullable2 = new double?();
                nullable4 = nullable2;
              }
              else
                nullable4 = new double?(nullable1.GetValueOrDefault() * num7);
              objArray[0] = (object) nullable4;
              SeriesPoint point = new SeriesPoint((object) local, objArray);
              points.Add(point);
            }
            this.peaks.Label.Visible = false;
          }
        }
      }
    }
    if (teoaeTestInformation.CalibrationGraph != null)
    {
      nullable1 = teoaeTestInformation.CalibrationGraphScale;
      if (nullable1.HasValue)
      {
        nullable1 = teoaeTestInformation.CalibrationGraphScale;
        double num8 = 0.0;
        if (nullable1.GetValueOrDefault() > num8 & nullable1.HasValue)
        {
          for (int index5 = 0; index5 < teoaeTestInformation.CalibrationGraph.Length; ++index5)
          {
            SeriesPointCollection points = this.calibrationCurve.Points;
            // ISSUE: variable of a boxed type
            __Boxed<int> local = (ValueType) index5;
            object[] objArray = new object[1];
            double? nullable5 = teoaeTestInformation.CalibrationGraphScale;
            double num9 = (double) teoaeTestInformation.CalibrationGraph[index5];
            nullable2 = nullable5.HasValue ? new double?(nullable5.GetValueOrDefault() * num9) : new double?();
            double num10 = 256.0;
            double? nullable6;
            if (!nullable2.HasValue)
            {
              nullable5 = new double?();
              nullable6 = nullable5;
            }
            else
              nullable6 = new double?(nullable2.GetValueOrDefault() / num10);
            nullable1 = nullable6;
            double num11 = -1.0;
            double? nullable7;
            if (!nullable1.HasValue)
            {
              nullable2 = new double?();
              nullable7 = nullable2;
            }
            else
              nullable7 = new double?(nullable1.GetValueOrDefault() * num11);
            objArray[0] = (object) nullable7;
            SeriesPoint point = new SeriesPoint((object) local, objArray);
            points.Add(point);
            this.calibrationZeroPointLine.Points.Add(new SeriesPoint((double) index5, new double[1]));
          }
        }
      }
    }
    this.resultGraph.Legend.Visible = false;
    this.resultGraph.Series.Add(this.zeroPointLine);
    this.resultGraph.Series.Add(this.curve);
    this.resultGraph.Series.Add(this.plusSigmaLine);
    this.resultGraph.Series.Add(this.minusSigmaLine);
    this.resultGraph.Series.Add(this.leftMarker);
    this.resultGraph.Series.Add(this.rightMarker);
    this.resultGraph.Series.Add(this.peaks);
    this.calibrationGraph.Legend.Visible = false;
    this.calibrationGraph.Series.Add(this.calibrationZeroPointLine);
    this.calibrationGraph.Series.Add(this.calibrationCurve);
  }

  private void CreateChartControls()
  {
    this.calibrationGraph.Series.Clear();
    this.resultGraph.Series.Clear();
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
    this.plusSigmaLine.View.Color = Color.Gray;
    Series series4 = new Series("minusSigmaLine", ViewType.Line);
    series4.Label.Visible = false;
    this.minusSigmaLine = series4;
    ((LineSeriesView) this.minusSigmaLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.minusSigmaLine.View).LineStyle.Thickness = 1;
    this.minusSigmaLine.View.Color = Color.Gray;
    Series series5 = new Series("leftMarker", ViewType.Line);
    series5.Label.Visible = false;
    this.leftMarker = series5;
    ((LineSeriesView) this.leftMarker.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.leftMarker.View).LineStyle.Thickness = 1;
    this.leftMarker.View.Color = Color.Gray;
    Series series6 = new Series("rightMarker", ViewType.Line);
    series6.Label.Visible = false;
    this.rightMarker = series6;
    ((LineSeriesView) this.rightMarker.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.rightMarker.View).LineStyle.Thickness = 1;
    this.rightMarker.View.Color = Color.Gray;
    this.peaks = new Series("Peaks", ViewType.Point);
    this.peaks.View.Color = Color.FromArgb(102, 168, 9);
    ((PointSeriesView) this.peaks.View).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid;
    Series series7 = new Series("Calibraion", ViewType.Spline);
    series7.Label.Visible = false;
    this.calibrationCurve = series7;
    ((LineSeriesView) this.calibrationCurve.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.calibrationCurve.View).LineStyle.Thickness = 1;
    this.calibrationCurve.View.Color = Color.Blue;
    Series series8 = new Series("CalibrationZeroPoint", ViewType.Line);
    series8.Label.Visible = false;
    this.calibrationZeroPointLine = series8;
    ((LineSeriesView) this.calibrationZeroPointLine.View).LineMarkerOptions.Visible = false;
    ((LineSeriesView) this.calibrationZeroPointLine.View).LineStyle.Thickness = 1;
    this.calibrationZeroPointLine.View.Color = Color.Black;
  }

  private void BindTestResult(TeoaeTestInformation teoaeTestInformation)
  {
    if (teoaeTestInformation == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("TestInformation");
    this.bestTestOverviewTestType.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestType.TEOAE) as string;
    int? nullable1 = teoaeTestInformation.Ear;
    if (nullable1.HasValue)
    {
      nullable1 = teoaeTestInformation.Ear;
      if (nullable1.GetValueOrDefault() == 7)
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.LeftEar) as string;
      else
        this.bestTestOverviewTestEar.Text = GlobalResourceEnquirer.Instance.GetResourceByName((Enum) TestObject.RightEar) as string;
    }
    DateTime? testTimeStamp = teoaeTestInformation.TestTimeStamp;
    if (testTimeStamp.HasValue)
    {
      XRTableCell overviewTestDateAndTime = this.bestTestOverviewTestDateAndTime;
      testTimeStamp = teoaeTestInformation.TestTimeStamp;
      string str = testTimeStamp.ToString();
      overviewTestDateAndTime.Text = str;
    }
    nullable1 = teoaeTestInformation.TestResult;
    if (nullable1.HasValue)
    {
      XRTableCell overviewTestResult = this.bestTestOverviewTestResult;
      GlobalResourceEnquirer instance = GlobalResourceEnquirer.Instance;
      nullable1 = teoaeTestInformation.TestResult;
      // ISSUE: variable of a boxed type
      __Boxed<T1077AudiologyTestResult> resourceName = (Enum) (T1077AudiologyTestResult) nullable1.Value;
      string resourceByName = instance.GetResourceByName((Enum) resourceName) as string;
      overviewTestResult.Text = resourceByName;
    }
    nullable1 = teoaeTestInformation.Duration;
    if (nullable1.HasValue)
    {
      XRTableCell overviewDuration = this.bestTestOverviewDuration;
      DateTime dateTime = new DateTime();
      nullable1 = teoaeTestInformation.Duration;
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) nullable1.Value);
      string str = $"{dateTime + timeSpan:T}";
      overviewDuration.Text = str;
    }
    long? nullable2 = teoaeTestInformation.InstrumentSerialNumber;
    if (nullable2.HasValue)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => i.SerialNumber == Convert.ToString((object) teoaeTestInformation.InstrumentSerialNumber)));
      if (instrument != null)
        this.bestTestOverviewInstrument.Text = instrument.SerialNumber;
    }
    nullable2 = teoaeTestInformation.ProbeSerialNumber;
    if (nullable2.HasValue)
    {
      XRTableCell testOverviewProbe = this.bestTestOverviewProbe;
      nullable2 = teoaeTestInformation.ProbeSerialNumber;
      string str = nullable2.ToString();
      testOverviewProbe.Text = str;
    }
    if (UserManager.Instance == null)
      return;
    User user = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
    {
      Guid id = u.Id;
      Guid? userAccountId = teoaeTestInformation.UserAccountId;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TeoaeDetailSubSubReport));
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
    this.calibrationGraph = new XRChart();
    this.testDetailOverviewDetails = new DetailBand();
    this.probeCalibrationDateValue = new XRLabel();
    this.probeCalibrationDate = new XRLabel();
    this.firmwareValue = new XRLabel();
    this.firmwareBuild = new XRLabel();
    this.deviceNameValue = new XRLabel();
    this.deviceName = new XRLabel();
    this.xrLabel2 = new XRLabel();
    this.resultGraph = new XRChart();
    this.framesValue = new XRLabel();
    this.frames = new XRLabel();
    this.teoae = new XRLabel();
    this.devExpressProgressBarTeoae = new DevExpressProgressBar();
    this.progress = new XRLabel();
    this.devExpressProgressBarProgress = new DevExpressProgressBar();
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
    this.xrLabelNoise = new XRLabel();
    this.devExpressProgressBarNoise = new DevExpressProgressBar();
    this.xrLabel1 = new XRLabel();
    this.energyValue = new XRLabel();
    this.energy = new XRLabel();
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
    this.testCommentOverview = new DetailReportBand();
    this.testCommentDetails = new DetailBand();
    this.xrTable3 = new XRTable();
    this.xrTableRow3 = new XRTableRow();
    this.xrTableCellCommentCreated = new XRTableCell();
    this.xrTableCellComment = new XRTableCell();
    this.xrTableCellCommenCreatedBy = new XRTableCell();
    this.testCommentHeader = new ReportHeaderBand();
    this.xrTableCommentHeader = new XRTable();
    this.xrTableRow2 = new XRTableRow();
    this.xrTableCellCommentCreatedHeader = new XRTableCell();
    this.xrTableCellCommentHeader = new XRTableCell();
    this.xrTableCellCreatedByHeader = new XRTableCell();
    this.xrLabelComments = new XRLabel();
    this.PageHeader = new PageHeaderBand();
    this.xrLabel3 = new XRLabel();
    this.calibrationGraph.BeginInit();
    ((ISupportInitialize) xyDiagram1).BeginInit();
    ((ISupportInitialize) series1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel1).BeginInit();
    ((ISupportInitialize) splineSeriesView1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel2).BeginInit();
    ((ISupportInitialize) splineSeriesView2).BeginInit();
    this.resultGraph.BeginInit();
    ((ISupportInitialize) xyDiagram2).BeginInit();
    ((ISupportInitialize) series2).BeginInit();
    ((ISupportInitialize) pointSeriesLabel3).BeginInit();
    ((ISupportInitialize) splineSeriesView3).BeginInit();
    ((ISupportInitialize) pointSeriesLabel4).BeginInit();
    ((ISupportInitialize) splineSeriesView4).BeginInit();
    this.bestTestOverviewTable.BeginInit();
    this.xrTable1.BeginInit();
    this.xrTable3.BeginInit();
    this.xrTableCommentHeader.BeginInit();
    this.BeginInit();
    componentResourceManager.ApplyResources((object) this.calibrationGraph, "calibrationGraph");
    this.calibrationGraph.Borders = BorderSide.None;
    xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram1.AxisX.Visible = false;
    xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram1.AxisY.GridLines.Visible = false;
    xyDiagram1.AxisY.Range.Auto = false;
    xyDiagram1.AxisY.Range.MaxValueSerializable = "10";
    xyDiagram1.AxisY.Range.MinValueSerializable = "-10";
    xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram1.AxisY.Visible = false;
    xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram1.DefaultPane.BorderVisible = false;
    this.calibrationGraph.Diagram = (Diagram) xyDiagram1;
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
    pointSeriesLabel1.LineVisible = true;
    pointSeriesLabel1.Visible = false;
    series1.Label = (SeriesLabelBase) pointSeriesLabel1;
    componentResourceManager.ApplyResources((object) series1, "series1");
    splineSeriesView1.LineMarkerOptions.Visible = false;
    series1.View = (SeriesViewBase) splineSeriesView1;
    this.calibrationGraph.SeriesSerializable = new Series[1]
    {
      series1
    };
    pointSeriesLabel2.LineVisible = true;
    this.calibrationGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel2;
    this.calibrationGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView2;
    this.testDetailOverviewDetails.Controls.AddRange(new XRControl[21]
    {
      (XRControl) this.probeCalibrationDateValue,
      (XRControl) this.probeCalibrationDate,
      (XRControl) this.firmwareValue,
      (XRControl) this.firmwareBuild,
      (XRControl) this.deviceNameValue,
      (XRControl) this.deviceName,
      (XRControl) this.xrLabel2,
      (XRControl) this.resultGraph,
      (XRControl) this.framesValue,
      (XRControl) this.frames,
      (XRControl) this.teoae,
      (XRControl) this.devExpressProgressBarTeoae,
      (XRControl) this.progress,
      (XRControl) this.devExpressProgressBarProgress,
      (XRControl) this.bestTestOverviewTable,
      (XRControl) this.calibrationGraph,
      (XRControl) this.xrLabelNoise,
      (XRControl) this.devExpressProgressBarNoise,
      (XRControl) this.xrLabel1,
      (XRControl) this.energyValue,
      (XRControl) this.energy
    });
    componentResourceManager.ApplyResources((object) this.testDetailOverviewDetails, "testDetailOverviewDetails");
    this.testDetailOverviewDetails.HeightF = 503f;
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
    componentResourceManager.ApplyResources((object) this.xrLabel2, "xrLabel2");
    this.xrLabel2.Name = "xrLabel2";
    this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel2.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.resultGraph, "resultGraph");
    this.resultGraph.Borders = BorderSide.None;
    xyDiagram2.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram2.AxisX.Visible = false;
    xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram2.AxisY.GridLines.Visible = false;
    xyDiagram2.AxisY.Range.Auto = false;
    xyDiagram2.AxisY.Range.MaxValueSerializable = "10";
    xyDiagram2.AxisY.Range.MinValueSerializable = "-10";
    xyDiagram2.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram2.AxisY.Visible = false;
    xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram2.DefaultPane.BorderVisible = false;
    this.resultGraph.Diagram = (Diagram) xyDiagram2;
    this.resultGraph.Legend.Visible = false;
    this.resultGraph.Name = "resultGraph";
    this.resultGraph.PaletteName = "Palette 1";
    this.resultGraph.PaletteRepository.Add("Palette 1", new Palette("Palette 1", PaletteScaleMode.Repeat, new PaletteEntry[12]
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
    this.resultGraph.PaletteRepository.Add("TestGraph", new Palette("TestGraph", PaletteScaleMode.Repeat, new PaletteEntry[2]
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
    this.resultGraph.SeriesSerializable = new Series[1]
    {
      series2
    };
    pointSeriesLabel4.LineVisible = true;
    this.resultGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel4;
    this.resultGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView4;
    componentResourceManager.ApplyResources((object) this.framesValue, "framesValue");
    this.framesValue.Name = "framesValue";
    this.framesValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.framesValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.frames, "frames");
    this.frames.Name = "frames";
    this.frames.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.frames.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.teoae, "teoae");
    this.teoae.Name = "teoae";
    this.teoae.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.teoae.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarTeoae, "devExpressProgressBarTeoae");
    this.devExpressProgressBarTeoae.Borders = BorderSide.None;
    this.devExpressProgressBarTeoae.MaxValue = 8f;
    this.devExpressProgressBarTeoae.Name = "devExpressProgressBarTeoae";
    this.devExpressProgressBarTeoae.Position = 0.0f;
    this.devExpressProgressBarTeoae.StylePriority.UseBackColor = false;
    this.devExpressProgressBarTeoae.StylePriority.UseBorders = false;
    this.devExpressProgressBarTeoae.StylePriority.UseForeColor = false;
    this.devExpressProgressBarTeoae.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.progress, "progress");
    this.progress.Name = "progress";
    this.progress.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.progress.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarProgress, "devExpressProgressBarProgress");
    this.devExpressProgressBarProgress.Borders = BorderSide.None;
    this.devExpressProgressBarProgress.MaxValue = 100f;
    this.devExpressProgressBarProgress.Name = "devExpressProgressBarProgress";
    this.devExpressProgressBarProgress.Position = 0.0f;
    this.devExpressProgressBarProgress.StylePriority.UseBackColor = false;
    this.devExpressProgressBarProgress.StylePriority.UseBorders = false;
    this.devExpressProgressBarProgress.StylePriority.UseForeColor = false;
    this.devExpressProgressBarProgress.StylePriority.UseTextAlignment = false;
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
    this.bestTestOverviewTestResult.Weight = 0.41228100999228573;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDuration, "bestTestOverviewDuration");
    this.bestTestOverviewDuration.Name = "bestTestOverviewDuration";
    this.bestTestOverviewDuration.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewDuration.StylePriority.UseFont = false;
    this.bestTestOverviewDuration.StylePriority.UsePadding = false;
    this.bestTestOverviewDuration.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewDuration.Weight = 0.27365817862642411;
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
    this.bestTestOverviewProbe.Weight = 0.3236411343889839;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewExaminer, "bestTestOverviewExaminer");
    this.bestTestOverviewExaminer.Name = "bestTestOverviewExaminer";
    this.bestTestOverviewExaminer.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewExaminer.StylePriority.UseFont = false;
    this.bestTestOverviewExaminer.StylePriority.UsePadding = false;
    this.bestTestOverviewExaminer.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewExaminer.Weight = 0.41178946338203948;
    componentResourceManager.ApplyResources((object) this.xrLabelNoise, "xrLabelNoise");
    this.xrLabelNoise.Name = "xrLabelNoise";
    this.xrLabelNoise.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelNoise.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.devExpressProgressBarNoise, "devExpressProgressBarNoise");
    this.devExpressProgressBarNoise.Borders = BorderSide.None;
    this.devExpressProgressBarNoise.MaxValue = 100f;
    this.devExpressProgressBarNoise.Name = "devExpressProgressBarNoise";
    this.devExpressProgressBarNoise.Position = 0.0f;
    this.devExpressProgressBarNoise.StylePriority.UseBackColor = false;
    this.devExpressProgressBarNoise.StylePriority.UseBorders = false;
    this.devExpressProgressBarNoise.StylePriority.UseForeColor = false;
    this.devExpressProgressBarNoise.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrLabel1, "xrLabel1");
    this.xrLabel1.Name = "xrLabel1";
    this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel1.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.energyValue, "energyValue");
    this.energyValue.Name = "energyValue";
    this.energyValue.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.energyValue.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.energy, "energy");
    this.energy.Name = "energy";
    this.energy.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.energy.StylePriority.UseFont = false;
    this.topMarginBand1.HeightF = 50f;
    this.topMarginBand1.Name = "topMarginBand1";
    componentResourceManager.ApplyResources((object) this.topMarginBand1, "topMarginBand1");
    this.bottomMarginBand1.HeightF = 100f;
    this.bottomMarginBand1.Name = "bottomMarginBand1";
    componentResourceManager.ApplyResources((object) this.bottomMarginBand1, "bottomMarginBand1");
    this.testDetailOverviewHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable1
    });
    this.testDetailOverviewHeader.HeightF = 25f;
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
    this.xrTableCell4.Weight = 0.41228100999228573;
    componentResourceManager.ApplyResources((object) this.xrTableCell5, "xrTableCell5");
    this.xrTableCell5.Name = "xrTableCell5";
    this.xrTableCell5.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell5.StylePriority.UseFont = false;
    this.xrTableCell5.StylePriority.UsePadding = false;
    this.xrTableCell5.StylePriority.UseTextAlignment = false;
    this.xrTableCell5.Weight = 0.27365817862642411;
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
    this.xrTableCell7.Weight = 0.3236411343889839;
    componentResourceManager.ApplyResources((object) this.xrTableCell8, "xrTableCell8");
    this.xrTableCell8.Name = "xrTableCell8";
    this.xrTableCell8.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell8.StylePriority.UseFont = false;
    this.xrTableCell8.StylePriority.UsePadding = false;
    this.xrTableCell8.StylePriority.UseTextAlignment = false;
    this.xrTableCell8.Weight = 0.41178946338203948;
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
    this.testCommentHeader.Controls.AddRange(new XRControl[2]
    {
      (XRControl) this.xrTableCommentHeader,
      (XRControl) this.xrLabelComments
    });
    this.testCommentHeader.HeightF = 52f;
    this.testCommentHeader.Name = "testCommentHeader";
    componentResourceManager.ApplyResources((object) this.testCommentHeader, "testCommentHeader");
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
    componentResourceManager.ApplyResources((object) this.xrLabelComments, "xrLabelComments");
    this.xrLabelComments.Name = "xrLabelComments";
    this.xrLabelComments.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelComments.StylePriority.UseFont = false;
    this.PageHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrLabel3
    });
    this.PageHeader.HeightF = 49f;
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
    this.Margins = new System.Drawing.Printing.Margins(100, 76, 50, 100);
    this.Version = "9.3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BeforePrint += new PrintEventHandler(this.TeoaeDetailSubSubReport_BeforePrint);
    ((ISupportInitialize) xyDiagram1).EndInit();
    ((ISupportInitialize) pointSeriesLabel1).EndInit();
    ((ISupportInitialize) splineSeriesView1).EndInit();
    ((ISupportInitialize) series1).EndInit();
    ((ISupportInitialize) pointSeriesLabel2).EndInit();
    ((ISupportInitialize) splineSeriesView2).EndInit();
    this.calibrationGraph.EndInit();
    ((ISupportInitialize) xyDiagram2).EndInit();
    ((ISupportInitialize) pointSeriesLabel3).EndInit();
    ((ISupportInitialize) splineSeriesView3).EndInit();
    ((ISupportInitialize) series2).EndInit();
    ((ISupportInitialize) pointSeriesLabel4).EndInit();
    ((ISupportInitialize) splineSeriesView4).EndInit();
    this.resultGraph.EndInit();
    this.bestTestOverviewTable.EndInit();
    this.xrTable1.EndInit();
    this.xrTable3.EndInit();
    this.xrTableCommentHeader.EndInit();
    this.EndInit();
  }
}
