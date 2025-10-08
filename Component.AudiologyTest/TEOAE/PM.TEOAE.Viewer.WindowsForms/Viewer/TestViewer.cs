// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Viewer.WindowsForms.Viewer.TestViewer
// Assembly: PM.TEOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83E8557F-96BD-4446-877A-A7FC2FA17A58
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using PathMedical.AudiologyTest;
using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.TEOAE.Viewer.WindowsForms.Viewer;

[ToolboxItem(false)]
public sealed class TestViewer : 
  PathMedical.UserInterface.WindowsForms.ModelViewController.View,
  ITestDetailView,
  IView,
  IDisposable,
  ISupportControllerAction,
  ISupportUserInterfaceManager
{
  private Series curve;
  private Series zeroPointLine;
  private Series peaks;
  private Series plusSigmaLine;
  private Series minusSigmaLine;
  private Series leftMarker;
  private Series rightMarker;
  private TeoaeTestInformation testDetailInformation;
  private DevExpressSingleSelectionGridViewHelper<Comment> commentGridViewHelper;
  private IContainer components;
  private LayoutControl TestLayout;
  private LayoutControlGroup layoutControlGroup1;
  private PictureEdit LeftEarIcon;
  private LayoutControlItem layoutLeftEarIcon;
  private LabelControl TestResultHeader;
  private LayoutControlItem layoutControlItem2;
  private PictureEdit RightEarIcon;
  private LayoutControlItem layoutRightEarIcon;
  private SimpleButton PeakCounter;
  private LayoutControlItem layoutControlItem5;
  private EmptySpaceItem emptySpaceItem3;
  private EmptySpaceItem emptySpaceItem1;
  private ChartControl ResultGraph;
  private LayoutControlItem layoutControlItem4;
  private XtraTabControl TestInformation;
  private XtraTabPage xtraTabTestResult;
  private XtraTabPage xtraTabTestComments;
  private GridControl TestComments;
  private GridView TestCommentView;
  private GridColumn commentDate;
  private GridColumn TestComment;
  private PictureEdit ResultImage;
  private LayoutControlItem layoutControlItem6;
  private LabelControl TimeStamp;
  private LabelControl ErrorDetail;
  private LabelControl ResultText;
  private LayoutControlItem layoutControlItem7;
  private LayoutControlItem layoutControlItem8;
  private LayoutControlItem layoutControlItem9;
  private XtraTabPage xtraTabDeviceInformation;
  private LabelControl ProbeSerialNo;
  private LabelControl DeviceSerialNo;
  private LabelControl FirmwareNo;
  private GridColumn TestCreator;
  private SimpleButton addComment;
  private DevExpressComboBoxEdit commentAssignment;
  private LabelControl testFacilityIDValue;
  private LabelControl locationTypeValue;
  private LabelControl probeCalibrationDateValue;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutDeviceSerialNumber;
  private LayoutControlItem layoutFirmwareBuild;
  private LayoutControlItem layoutProbeSerialNumber;
  private LayoutControlItem layoutTestFacility;
  private LayoutControlItem layoutTestLocation;
  private LayoutControlItem layoutProbeCalibrationDate;
  private EmptySpaceItem emptySpaceItem2;
  private EmptySpaceItem emptySpaceItem4;
  private LayoutControl layoutControl2;
  private LayoutControlGroup layoutControlGroup3;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem10;

  public TestViewer()
  {
    this.InitializeComponent();
    this.PeakCounter.StyleController = (IStyleController) null;
    this.PeakCounter.ButtonStyle = BorderStyles.UltraFlat;
    this.Dock = DockStyle.Fill;
    this.AutoScroll = true;
    this.AutoScrollMinSize = this.MinimumSize;
    this.LeftEarIcon.Image = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Ear_Left") as Image;
    this.RightEarIcon.Image = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Ear_Right") as Image;
    this.PeakCounter.BackgroundImage = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject(nameof (PeakCounter)) as Image;
  }

  public TestViewer(TeoaeTestInformation testInformation)
    : this()
  {
    this.Dock = DockStyle.None;
    this.FillFields(testInformation);
    this.addComment.Visible = false;
    this.TestComments.Dock = DockStyle.Fill;
  }

  public TestViewer(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.commentGridViewHelper = new DevExpressSingleSelectionGridViewHelper<Comment>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.TestCommentView, model, Triggers.ChangeSelection, "Comments");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    this.addComment.Click += new EventHandler(this.AddCommentClick);
    this.InitializeSelectionValues();
  }

  private void InitializeSelectionValues()
  {
    this.commentAssignment.DataSource = (object) CommentManager.Instance.CommentList.Select<PredefinedComment, ComboBoxEditItemWrapper>((Func<PredefinedComment, ComboBoxEditItemWrapper>) (i => new ComboBoxEditItemWrapper(i.Text, (object) i))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs eventArgs)
  {
    if (eventArgs.ChangedObject is AudiologyTestInformation && eventArgs.ChangeType == ChangeType.SelectionChanged && TeoaeComponent.Instance.HostControl != null)
    {
      TeoaeComponent.Instance.HostControl.Controls.Clear();
      TeoaeComponent.Instance.HostControl.Controls.Add((Control) this);
    }
    if (!(eventArgs.ChangedObject is TeoaeTestInformation))
      return;
    this.testDetailInformation = eventArgs.ChangedObject as TeoaeTestInformation;
    this.FillFields(this.testDetailInformation);
  }

  private void FillFields(TeoaeTestInformation teoaeTestInformation)
  {
    if (teoaeTestInformation == null)
      return;
    int? ear = teoaeTestInformation.Ear;
    if (!ear.HasValue)
      return;
    switch (ear.GetValueOrDefault())
    {
      case 7:
        this.LoadEar(teoaeTestInformation, this.LeftEarIcon);
        break;
      case 112 /*0x70*/:
        this.LoadEar(teoaeTestInformation, this.RightEarIcon);
        break;
    }
  }

  private void CreateChartControls()
  {
    this.ResultGraph.Series.Clear();
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
    Series series7 = new Series("Peaks", ViewType.Point);
    series7.View.Color = Color.FromArgb(102, 168, 9);
    this.peaks = series7;
    ((PointSeriesView) this.peaks.View).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid;
  }

  private void LoadEar(TeoaeTestInformation testDetail, PictureEdit earPicture)
  {
    if (testDetail == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (testDetail));
    if (earPicture == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (earPicture));
    if (earPicture == this.LeftEarIcon)
    {
      this.layoutRightEarIcon.ContentVisible = false;
      this.layoutLeftEarIcon.ContentVisible = true;
    }
    else
    {
      this.layoutRightEarIcon.ContentVisible = true;
      this.layoutLeftEarIcon.ContentVisible = false;
    }
    int? nullable1 = testDetail.TestResult;
    if (nullable1.HasValue)
    {
      switch (nullable1.GetValueOrDefault())
      {
        case 13175:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_ClearResponse");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("PassResultText");
          this.ErrorDetail.Text = (string) null;
          goto label_20;
        case 16706:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("MeasurementAbort");
          goto label_20;
        case 16707:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("CalibrationAbort");
          goto label_20;
        case 17221:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("CalibrationError");
          goto label_20;
        case 17236:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("CalibrationTimeout");
          goto label_20;
        case 17740:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("ElectrodeLoose");
          goto label_20;
        case 19526:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("CalibrationLeaky");
          goto label_20;
        case 20035:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("Noisy");
          goto label_20;
        case 29555:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("ProbeError");
          goto label_20;
        case 30583 /*0x7777*/:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_NoClearResponse");
          this.ResultText.Text = ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetString("ReferredResultText");
          this.ErrorDetail.Text = (string) null;
          goto label_20;
      }
    }
    this.ResultImage.Image = (Image) ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
    this.ResultText.Text = (string) null;
label_20:
    int index1 = 0;
    this.CreateChartControls();
    nullable1 = testDetail.Frames;
    if (nullable1.HasValue)
    {
      nullable1 = testDetail.Frames;
      int num1 = 0;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      {
        double? nullable2;
        double? nullable3;
        for (int index2 = 0; index2 < testDetail.Graph.Length - 64 /*0x40*/; ++index2)
        {
          SeriesPointCollection points = this.curve.Points;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (ValueType) index2;
          object[] objArray = new object[1];
          nullable2 = testDetail.GraphScale;
          double num2 = (double) testDetail.Graph[index2 + 64 /*0x40*/];
          nullable3 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num2) : new double?();
          double num3 = -1.0;
          double? nullable4;
          if (!nullable3.HasValue)
          {
            nullable2 = new double?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new double?(nullable3.GetValueOrDefault() * num3);
          objArray[0] = (object) nullable4;
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
          this.rightMarker.Points.Add(new SeriesPoint((double) (testDetail.Graph.Length - 65), new double[1]
          {
            (double) index4
          }));
        for (; testDetail.PeakIndex[index1] != (byte) 0; ++index1)
        {
          int num4 = (int) testDetail.PeakIndex[index1] - 64 /*0x40*/;
          SeriesPointCollection points = this.peaks.Points;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (ValueType) num4;
          object[] objArray = new object[1];
          nullable2 = testDetail.GraphScale;
          double num5 = (double) testDetail.Graph[num4 + 64 /*0x40*/];
          nullable3 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num5) : new double?();
          double num6 = -1.0;
          double? nullable5;
          if (!nullable3.HasValue)
          {
            nullable2 = new double?();
            nullable5 = nullable2;
          }
          else
            nullable5 = new double?(nullable3.GetValueOrDefault() * num6);
          objArray[0] = (object) nullable5;
          SeriesPoint point = new SeriesPoint((object) local, objArray);
          points.Add(point);
        }
        this.peaks.Label.Visible = false;
        goto label_49;
      }
    }
    if (testDetail.CalibrationGraph != null)
    {
      for (int index5 = 0; index5 < testDetail.CalibrationGraph.Length; ++index5)
      {
        SeriesPointCollection points = this.curve.Points;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) index5;
        object[] objArray = new object[1];
        double? calibrationGraphScale = testDetail.CalibrationGraphScale;
        double num7 = (double) testDetail.CalibrationGraph[index5];
        double? nullable6 = calibrationGraphScale.HasValue ? new double?(calibrationGraphScale.GetValueOrDefault() * num7) : new double?();
        double num8 = 256.0;
        double? nullable7 = nullable6.HasValue ? new double?(nullable6.GetValueOrDefault() / num8) : new double?();
        double num9 = -1.0;
        double? nullable8;
        if (!nullable7.HasValue)
        {
          nullable6 = new double?();
          nullable8 = nullable6;
        }
        else
          nullable8 = new double?(nullable7.GetValueOrDefault() * num9);
        objArray[0] = (object) nullable8;
        SeriesPoint point = new SeriesPoint((object) local, objArray);
        points.Add(point);
        this.zeroPointLine.Points.Add(new SeriesPoint((double) index5, new double[1]));
      }
    }
label_49:
    this.ResultGraph.Legend.Visible = false;
    this.ResultGraph.Series.Add(this.zeroPointLine);
    this.ResultGraph.Series.Add(this.curve);
    this.ResultGraph.Series.Add(this.plusSigmaLine);
    this.ResultGraph.Series.Add(this.minusSigmaLine);
    this.ResultGraph.Series.Add(this.leftMarker);
    this.ResultGraph.Series.Add(this.rightMarker);
    this.ResultGraph.Series.Add(this.peaks);
    if (testDetail.InstrumentSerialNumber.HasValue)
      this.DeviceSerialNo.Text = testDetail.InstrumentSerialNumber.ToString();
    nullable1 = testDetail.FirmwareVersion;
    if (nullable1.HasValue)
    {
      LabelControl firmwareNo = this.FirmwareNo;
      nullable1 = testDetail.FirmwareVersion;
      string str = nullable1.ToString();
      firmwareNo.Text = str;
    }
    long? probeSerialNumber = testDetail.ProbeSerialNumber;
    if (probeSerialNumber.HasValue)
    {
      LabelControl probeSerialNo = this.ProbeSerialNo;
      probeSerialNumber = testDetail.ProbeSerialNumber;
      string str = probeSerialNumber.ToString();
      probeSerialNo.Text = str;
    }
    DateTime? nullable9 = testDetail.ProbeCalibrationDate;
    if (nullable9.HasValue)
    {
      LabelControl calibrationDateValue = this.probeCalibrationDateValue;
      nullable9 = testDetail.ProbeCalibrationDate;
      string str = nullable9.ToString();
      calibrationDateValue.Text = str;
    }
    Guid? nullable10 = testDetail.FacilityId;
    if (nullable10.HasValue)
      this.testFacilityIDValue.Text = FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
      {
        Guid id = f.Id;
        Guid? facilityId = testDetail.FacilityId;
        return facilityId.HasValue && id == facilityId.GetValueOrDefault();
      })).Select<Facility, string>((Func<Facility, string>) (f => f.Code)).FirstOrDefault<string>();
    else
      this.testFacilityIDValue.Text = PathMedical.AudiologyTest.Properties.Resources.TestViewer_NoValueFound;
    nullable10 = testDetail.LocationId;
    if (nullable10.HasValue)
      this.locationTypeValue.Text = LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l =>
      {
        Guid id = l.Id;
        Guid? locationId = testDetail.LocationId;
        return locationId.HasValue && id == locationId.GetValueOrDefault();
      })).Select<LocationType, string>((Func<LocationType, string>) (l => l.Name)).FirstOrDefault<string>();
    else
      this.locationTypeValue.Text = PathMedical.AudiologyTest.Properties.Resources.TestViewer_NoValueFound;
    LabelControl timeStamp = this.TimeStamp;
    nullable9 = testDetail.TestTimeStamp;
    string str1 = nullable9.ToString();
    timeStamp.Text = str1;
    this.PeakCounter.Text = index1 > 0 ? $"{index1}/8" : "0/8";
  }

  private void AddCommentClick(object sender, EventArgs e)
  {
    object obj = this.commentAssignment.Value;
    if (obj == null)
      return;
    if (obj is string)
    {
      FreeTextComment freeTextComment = new FreeTextComment();
      freeTextComment.Text = obj as string;
      TeoaeTestManager.Instance.AddComment((object) freeTextComment as FreeTextComment);
    }
    if (obj is PredefinedComment)
      TeoaeTestManager.Instance.AddPredefinedComment(((PredefinedComment) obj).Clone() as PredefinedComment);
    this.commentAssignment.SelectedIndex = -1;
    CommandManager.Instance.SetSaved();
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool flag = this.ViewMode != 0;
    this.addComment.Enabled = flag;
    this.commentAssignment.Enabled = flag;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TestViewer));
    XYDiagram xyDiagram = new XYDiagram();
    Series series = new Series();
    PointSeriesLabel pointSeriesLabel1 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView1 = new SplineSeriesView();
    PointSeriesLabel pointSeriesLabel2 = new PointSeriesLabel();
    SplineSeriesView splineSeriesView2 = new SplineSeriesView();
    this.TestLayout = new LayoutControl();
    this.TimeStamp = new LabelControl();
    this.ErrorDetail = new LabelControl();
    this.ResultText = new LabelControl();
    this.ResultImage = new PictureEdit();
    this.RightEarIcon = new PictureEdit();
    this.TestResultHeader = new LabelControl();
    this.LeftEarIcon = new PictureEdit();
    this.ResultGraph = new ChartControl();
    this.PeakCounter = new SimpleButton();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutLeftEarIcon = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutRightEarIcon = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem7 = new LayoutControlItem();
    this.layoutControlItem8 = new LayoutControlItem();
    this.layoutControlItem9 = new LayoutControlItem();
    this.TestInformation = new XtraTabControl();
    this.xtraTabTestResult = new XtraTabPage();
    this.xtraTabTestComments = new XtraTabPage();
    this.layoutControl2 = new LayoutControl();
    this.TestComments = new GridControl();
    this.TestCommentView = new GridView();
    this.commentDate = new GridColumn();
    this.TestComment = new GridColumn();
    this.TestCreator = new GridColumn();
    this.addComment = new SimpleButton();
    this.commentAssignment = new DevExpressComboBoxEdit();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem10 = new LayoutControlItem();
    this.xtraTabDeviceInformation = new XtraTabPage();
    this.layoutControl1 = new LayoutControl();
    this.DeviceSerialNo = new LabelControl();
    this.probeCalibrationDateValue = new LabelControl();
    this.FirmwareNo = new LabelControl();
    this.locationTypeValue = new LabelControl();
    this.testFacilityIDValue = new LabelControl();
    this.ProbeSerialNo = new LabelControl();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutDeviceSerialNumber = new LayoutControlItem();
    this.layoutFirmwareBuild = new LayoutControlItem();
    this.layoutTestFacility = new LayoutControlItem();
    this.layoutTestLocation = new LayoutControlItem();
    this.layoutProbeCalibrationDate = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutProbeSerialNumber = new LayoutControlItem();
    this.TestLayout.BeginInit();
    this.TestLayout.SuspendLayout();
    this.ResultImage.Properties.BeginInit();
    this.RightEarIcon.Properties.BeginInit();
    this.LeftEarIcon.Properties.BeginInit();
    this.ResultGraph.BeginInit();
    ((ISupportInitialize) xyDiagram).BeginInit();
    ((ISupportInitialize) series).BeginInit();
    ((ISupportInitialize) pointSeriesLabel1).BeginInit();
    ((ISupportInitialize) splineSeriesView1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel2).BeginInit();
    ((ISupportInitialize) splineSeriesView2).BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutLeftEarIcon.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutRightEarIcon.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem7.BeginInit();
    this.layoutControlItem8.BeginInit();
    this.layoutControlItem9.BeginInit();
    this.TestInformation.BeginInit();
    this.TestInformation.SuspendLayout();
    this.xtraTabTestResult.SuspendLayout();
    this.xtraTabTestComments.SuspendLayout();
    this.layoutControl2.BeginInit();
    this.layoutControl2.SuspendLayout();
    this.TestComments.BeginInit();
    this.TestCommentView.BeginInit();
    this.commentAssignment.Properties.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem10.BeginInit();
    this.xtraTabDeviceInformation.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.layoutControlGroup2.BeginInit();
    this.layoutDeviceSerialNumber.BeginInit();
    this.layoutFirmwareBuild.BeginInit();
    this.layoutTestFacility.BeginInit();
    this.layoutTestLocation.BeginInit();
    this.layoutProbeCalibrationDate.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.layoutProbeSerialNumber.BeginInit();
    this.SuspendLayout();
    this.TestLayout.Controls.Add((Control) this.TimeStamp);
    this.TestLayout.Controls.Add((Control) this.ErrorDetail);
    this.TestLayout.Controls.Add((Control) this.ResultText);
    this.TestLayout.Controls.Add((Control) this.ResultImage);
    this.TestLayout.Controls.Add((Control) this.RightEarIcon);
    this.TestLayout.Controls.Add((Control) this.TestResultHeader);
    this.TestLayout.Controls.Add((Control) this.LeftEarIcon);
    this.TestLayout.Controls.Add((Control) this.ResultGraph);
    this.TestLayout.Controls.Add((Control) this.PeakCounter);
    componentResourceManager.ApplyResources((object) this.TestLayout, "TestLayout");
    this.TestLayout.Name = "TestLayout";
    this.TestLayout.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.TimeStamp, "TimeStamp");
    this.TimeStamp.Name = "TimeStamp";
    this.TimeStamp.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.ErrorDetail, "ErrorDetail");
    this.ErrorDetail.Name = "ErrorDetail";
    this.ErrorDetail.StyleController = (IStyleController) this.TestLayout;
    this.ResultText.Appearance.Font = new Font("Arial Unicode MS", 8.25f, FontStyle.Bold);
    this.ResultText.Appearance.Options.UseFont = true;
    componentResourceManager.ApplyResources((object) this.ResultText, "ResultText");
    this.ResultText.Name = "ResultText";
    this.ResultText.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.ResultImage, "ResultImage");
    this.ResultImage.Name = "ResultImage";
    this.ResultImage.Properties.Appearance.BackColor = Color.Transparent;
    this.ResultImage.Properties.Appearance.Options.UseBackColor = true;
    this.ResultImage.Properties.BorderStyle = BorderStyles.NoBorder;
    this.ResultImage.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.RightEarIcon, "RightEarIcon");
    this.RightEarIcon.MinimumSize = new Size(28, 26);
    this.RightEarIcon.Name = "RightEarIcon";
    this.RightEarIcon.Properties.Appearance.BackColor = Color.Transparent;
    this.RightEarIcon.Properties.Appearance.Options.UseBackColor = true;
    this.RightEarIcon.Properties.BorderStyle = BorderStyles.NoBorder;
    this.RightEarIcon.StyleController = (IStyleController) this.TestLayout;
    this.TestResultHeader.Appearance.Font = new Font("Arial Unicode MS", 12f);
    this.TestResultHeader.Appearance.Options.UseFont = true;
    this.TestResultHeader.Appearance.Options.UseTextOptions = true;
    this.TestResultHeader.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
    this.TestResultHeader.Appearance.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.TestResultHeader, "TestResultHeader");
    this.TestResultHeader.MinimumSize = new Size(267, 26);
    this.TestResultHeader.Name = "TestResultHeader";
    this.TestResultHeader.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.LeftEarIcon, "LeftEarIcon");
    this.LeftEarIcon.MinimumSize = new Size(28, 26);
    this.LeftEarIcon.Name = "LeftEarIcon";
    this.LeftEarIcon.Properties.Appearance.BackColor = Color.Transparent;
    this.LeftEarIcon.Properties.Appearance.Options.UseBackColor = true;
    this.LeftEarIcon.Properties.BorderStyle = BorderStyles.NoBorder;
    this.LeftEarIcon.StyleController = (IStyleController) this.TestLayout;
    this.ResultGraph.BorderOptions.Visible = false;
    xyDiagram.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
    xyDiagram.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram.AxisX.Visible = false;
    xyDiagram.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram.AxisY.GridLines.Visible = false;
    xyDiagram.AxisY.Range.Auto = false;
    xyDiagram.AxisY.Range.MaxValueSerializable = "10";
    xyDiagram.AxisY.Range.MinValueSerializable = "-10";
    xyDiagram.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
    xyDiagram.AxisY.Range.SideMarginsEnabled = true;
    xyDiagram.AxisY.Visible = false;
    xyDiagram.AxisY.VisibleInPanesSerializable = "-1";
    xyDiagram.DefaultPane.BorderVisible = false;
    this.ResultGraph.Diagram = (Diagram) xyDiagram;
    this.ResultGraph.Legend.Visible = false;
    componentResourceManager.ApplyResources((object) this.ResultGraph, "ResultGraph");
    this.ResultGraph.MinimumSize = new Size(320, 80 /*0x50*/);
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
    series.Label = (SeriesLabelBase) pointSeriesLabel1;
    splineSeriesView1.LineMarkerOptions.Visible = false;
    series.View = (SeriesViewBase) splineSeriesView1;
    this.ResultGraph.SeriesSerializable = new Series[1]
    {
      series
    };
    pointSeriesLabel2.LineVisible = true;
    this.ResultGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel2;
    this.ResultGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView2;
    this.PeakCounter.AccessibleRole = AccessibleRole.None;
    this.PeakCounter.AllowFocus = false;
    this.PeakCounter.Appearance.BackColor = Color.Transparent;
    this.PeakCounter.Appearance.BackColor2 = Color.Transparent;
    this.PeakCounter.Appearance.BorderColor = Color.Transparent;
    this.PeakCounter.Appearance.Font = new Font("Arial Unicode MS", 18f, FontStyle.Bold);
    this.PeakCounter.Appearance.ForeColor = Color.Black;
    this.PeakCounter.Appearance.Options.UseBackColor = true;
    this.PeakCounter.Appearance.Options.UseBorderColor = true;
    this.PeakCounter.Appearance.Options.UseFont = true;
    this.PeakCounter.Appearance.Options.UseForeColor = true;
    this.PeakCounter.AutoSizeInLayoutControl = false;
    componentResourceManager.ApplyResources((object) this.PeakCounter, "PeakCounter");
    this.PeakCounter.Cursor = Cursors.Default;
    this.PeakCounter.MinimumSize = new Size(52, 27);
    this.PeakCounter.Name = "PeakCounter";
    this.PeakCounter.StyleController = (IStyleController) this.TestLayout;
    this.layoutControlGroup1.AppearanceGroup.BackColor = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.BackColor2 = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[11]
    {
      (BaseLayoutItem) this.layoutLeftEarIcon,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutRightEarIcon,
      (BaseLayoutItem) this.layoutControlItem6,
      (BaseLayoutItem) this.layoutControlItem7,
      (BaseLayoutItem) this.layoutControlItem8,
      (BaseLayoutItem) this.layoutControlItem9
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(348, 288);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    this.layoutLeftEarIcon.Control = (Control) this.LeftEarIcon;
    componentResourceManager.ApplyResources((object) this.layoutLeftEarIcon, "layoutLeftEarIcon");
    this.layoutLeftEarIcon.Location = new Point(0, 0);
    this.layoutLeftEarIcon.Name = "layoutLeftEarIcon";
    this.layoutLeftEarIcon.Size = new Size(32 /*0x20*/, 30);
    this.layoutLeftEarIcon.TextSize = new Size(0, 0);
    this.layoutLeftEarIcon.TextToControlDistance = 0;
    this.layoutLeftEarIcon.TextVisible = false;
    this.layoutControlItem2.Control = (Control) this.TestResultHeader;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(32 /*0x20*/, 0);
    this.layoutControlItem2.MaxSize = new Size(0, 30);
    this.layoutControlItem2.MinSize = new Size(14, 30);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(264, 30);
    this.layoutControlItem2.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    this.layoutControlItem4.Control = (Control) this.ResultGraph;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 30);
    this.layoutControlItem4.MinSize = new Size(324, 84);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(328, 147);
    this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    this.layoutControlItem5.Control = (Control) this.PeakCounter;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(132, 177);
    this.layoutControlItem5.MinSize = new Size(56, 31 /*0x1F*/);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(56, 31 /*0x1F*/);
    this.layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(0, 177);
    this.emptySpaceItem3.MinSize = new Size(104, 24);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(132, 31 /*0x1F*/);
    this.emptySpaceItem3.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(188, 177);
    this.emptySpaceItem1.MinSize = new Size(104, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(140, 31 /*0x1F*/);
    this.emptySpaceItem1.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutRightEarIcon.Control = (Control) this.RightEarIcon;
    componentResourceManager.ApplyResources((object) this.layoutRightEarIcon, "layoutRightEarIcon");
    this.layoutRightEarIcon.Location = new Point(296, 0);
    this.layoutRightEarIcon.Name = "layoutRightEarIcon";
    this.layoutRightEarIcon.Size = new Size(32 /*0x20*/, 30);
    this.layoutRightEarIcon.TextSize = new Size(0, 0);
    this.layoutRightEarIcon.TextToControlDistance = 0;
    this.layoutRightEarIcon.TextVisible = false;
    this.layoutControlItem6.Control = (Control) this.ResultImage;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(0, 208 /*0xD0*/);
    this.layoutControlItem6.MinSize = new Size(55, 60);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(55, 60);
    this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.layoutControlItem7.Control = (Control) this.ResultText;
    componentResourceManager.ApplyResources((object) this.layoutControlItem7, "layoutControlItem7");
    this.layoutControlItem7.Location = new Point(55, 208 /*0xD0*/);
    this.layoutControlItem7.MinSize = new Size(67, 17);
    this.layoutControlItem7.Name = "layoutControlItem7";
    this.layoutControlItem7.Size = new Size(273, 17);
    this.layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem7.TextSize = new Size(0, 0);
    this.layoutControlItem7.TextToControlDistance = 0;
    this.layoutControlItem7.TextVisible = false;
    this.layoutControlItem8.Control = (Control) this.ErrorDetail;
    componentResourceManager.ApplyResources((object) this.layoutControlItem8, "layoutControlItem8");
    this.layoutControlItem8.Location = new Point(55, 225);
    this.layoutControlItem8.MinSize = new Size(67, 17);
    this.layoutControlItem8.Name = "layoutControlItem8";
    this.layoutControlItem8.Size = new Size(273, 26);
    this.layoutControlItem8.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem8.TextSize = new Size(0, 0);
    this.layoutControlItem8.TextToControlDistance = 0;
    this.layoutControlItem8.TextVisible = false;
    this.layoutControlItem9.Control = (Control) this.TimeStamp;
    componentResourceManager.ApplyResources((object) this.layoutControlItem9, "layoutControlItem9");
    this.layoutControlItem9.Location = new Point(55, 251);
    this.layoutControlItem9.MinSize = new Size(67, 17);
    this.layoutControlItem9.Name = "layoutControlItem9";
    this.layoutControlItem9.Size = new Size(273, 17);
    this.layoutControlItem9.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem9.TextSize = new Size(0, 0);
    this.layoutControlItem9.TextToControlDistance = 0;
    this.layoutControlItem9.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.TestInformation, "TestInformation");
    this.TestInformation.Name = "TestInformation";
    this.TestInformation.SelectedTabPage = this.xtraTabTestResult;
    this.TestInformation.TabPages.AddRange(new XtraTabPage[3]
    {
      this.xtraTabTestResult,
      this.xtraTabTestComments,
      this.xtraTabDeviceInformation
    });
    this.xtraTabTestResult.Controls.Add((Control) this.TestLayout);
    this.xtraTabTestResult.Name = "xtraTabTestResult";
    componentResourceManager.ApplyResources((object) this.xtraTabTestResult, "xtraTabTestResult");
    this.xtraTabTestComments.Controls.Add((Control) this.layoutControl2);
    this.xtraTabTestComments.Name = "xtraTabTestComments";
    componentResourceManager.ApplyResources((object) this.xtraTabTestComments, "xtraTabTestComments");
    this.layoutControl2.Controls.Add((Control) this.TestComments);
    this.layoutControl2.Controls.Add((Control) this.commentAssignment);
    this.layoutControl2.Controls.Add((Control) this.addComment);
    componentResourceManager.ApplyResources((object) this.layoutControl2, "layoutControl2");
    this.layoutControl2.Name = "layoutControl2";
    this.layoutControl2.Root = this.layoutControlGroup3;
    componentResourceManager.ApplyResources((object) this.TestComments, "TestComments");
    this.TestComments.MainView = (BaseView) this.TestCommentView;
    this.TestComments.Name = "TestComments";
    this.TestComments.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.TestCommentView
    });
    this.TestCommentView.Columns.AddRange(new GridColumn[3]
    {
      this.commentDate,
      this.TestComment,
      this.TestCreator
    });
    this.TestCommentView.GridControl = this.TestComments;
    this.TestCommentView.Name = "TestCommentView";
    this.TestCommentView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.commentDate, "commentDate");
    this.commentDate.DisplayFormat.FormatType = FormatType.DateTime;
    this.commentDate.FieldName = "CreationDate";
    this.commentDate.Name = "commentDate";
    this.commentDate.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.TestComment, "TestComment");
    this.TestComment.FieldName = "Text";
    this.TestComment.Name = "TestComment";
    this.TestComment.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.TestCreator, "TestCreator");
    this.TestCreator.FieldName = "Examiner";
    this.TestCreator.Name = "TestCreator";
    this.TestCreator.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.addComment, "addComment");
    this.addComment.Name = "addComment";
    this.addComment.StyleController = (IStyleController) this.layoutControl2;
    this.commentAssignment.EnterMoveNextControl = true;
    this.commentAssignment.FormatString = (string) null;
    this.commentAssignment.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.commentAssignment.IsReadOnly = false;
    this.commentAssignment.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.commentAssignment, "commentAssignment");
    this.commentAssignment.Name = "commentAssignment";
    this.commentAssignment.Properties.Appearance.BorderColor = Color.LightGray;
    this.commentAssignment.Properties.Appearance.Options.UseBorderColor = true;
    this.commentAssignment.Properties.BorderStyle = BorderStyles.Simple;
    this.commentAssignment.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("commentAssignment.Properties.Buttons"))
    });
    this.commentAssignment.ShowEmptyElement = true;
    this.commentAssignment.StyleController = (IStyleController) this.layoutControl2;
    this.commentAssignment.Validator = (ICustomValidator) null;
    this.commentAssignment.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup3.GroupBordersVisible = false;
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem10
    });
    this.layoutControlGroup3.Location = new Point(0, 0);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(348, 288);
    this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup3.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.TestComments;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(328, 242);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.layoutControlItem3.Control = (Control) this.commentAssignment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 242);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(226, 26);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutControlItem10.Control = (Control) this.addComment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem10, "layoutControlItem10");
    this.layoutControlItem10.Location = new Point(226, 242);
    this.layoutControlItem10.Name = "layoutControlItem10";
    this.layoutControlItem10.Size = new Size(102, 26);
    this.layoutControlItem10.TextSize = new Size(0, 0);
    this.layoutControlItem10.TextToControlDistance = 0;
    this.layoutControlItem10.TextVisible = false;
    this.xtraTabDeviceInformation.Controls.Add((Control) this.layoutControl1);
    this.xtraTabDeviceInformation.Name = "xtraTabDeviceInformation";
    componentResourceManager.ApplyResources((object) this.xtraTabDeviceInformation, "xtraTabDeviceInformation");
    this.layoutControl1.Controls.Add((Control) this.DeviceSerialNo);
    this.layoutControl1.Controls.Add((Control) this.probeCalibrationDateValue);
    this.layoutControl1.Controls.Add((Control) this.FirmwareNo);
    this.layoutControl1.Controls.Add((Control) this.locationTypeValue);
    this.layoutControl1.Controls.Add((Control) this.testFacilityIDValue);
    this.layoutControl1.Controls.Add((Control) this.ProbeSerialNo);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.DeviceSerialNo, "DeviceSerialNo");
    this.DeviceSerialNo.Name = "DeviceSerialNo";
    this.DeviceSerialNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDateValue, "probeCalibrationDateValue");
    this.probeCalibrationDateValue.Name = "probeCalibrationDateValue";
    this.probeCalibrationDateValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.FirmwareNo, "FirmwareNo");
    this.FirmwareNo.Name = "FirmwareNo";
    this.FirmwareNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.locationTypeValue, "locationTypeValue");
    this.locationTypeValue.Name = "locationTypeValue";
    this.locationTypeValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.testFacilityIDValue, "testFacilityIDValue");
    this.testFacilityIDValue.Name = "testFacilityIDValue";
    this.testFacilityIDValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.ProbeSerialNo, "ProbeSerialNo");
    this.ProbeSerialNo.Name = "ProbeSerialNo";
    this.ProbeSerialNo.StyleController = (IStyleController) this.layoutControl1;
    this.layoutControlGroup2.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup2.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[8]
    {
      (BaseLayoutItem) this.layoutDeviceSerialNumber,
      (BaseLayoutItem) this.layoutFirmwareBuild,
      (BaseLayoutItem) this.layoutTestFacility,
      (BaseLayoutItem) this.layoutTestLocation,
      (BaseLayoutItem) this.layoutProbeCalibrationDate,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.emptySpaceItem4,
      (BaseLayoutItem) this.layoutProbeSerialNumber
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(348, 288);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    this.layoutDeviceSerialNumber.Control = (Control) this.DeviceSerialNo;
    componentResourceManager.ApplyResources((object) this.layoutDeviceSerialNumber, "layoutDeviceSerialNumber");
    this.layoutDeviceSerialNumber.Location = new Point(0, 30);
    this.layoutDeviceSerialNumber.Name = "layoutDeviceSerialNumber";
    this.layoutDeviceSerialNumber.Size = new Size(328, 17);
    this.layoutDeviceSerialNumber.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutFirmwareBuild.Control = (Control) this.FirmwareNo;
    componentResourceManager.ApplyResources((object) this.layoutFirmwareBuild, "layoutFirmwareBuild");
    this.layoutFirmwareBuild.Location = new Point(0, 47);
    this.layoutFirmwareBuild.Name = "layoutFirmwareBuild";
    this.layoutFirmwareBuild.Size = new Size(328, 17);
    this.layoutFirmwareBuild.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutTestFacility.Control = (Control) this.testFacilityIDValue;
    componentResourceManager.ApplyResources((object) this.layoutTestFacility, "layoutTestFacility");
    this.layoutTestFacility.Location = new Point(0, 64 /*0x40*/);
    this.layoutTestFacility.Name = "layoutTestFacility";
    this.layoutTestFacility.Size = new Size(328, 17);
    this.layoutTestFacility.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutTestLocation.Control = (Control) this.locationTypeValue;
    componentResourceManager.ApplyResources((object) this.layoutTestLocation, "layoutTestLocation");
    this.layoutTestLocation.Location = new Point(0, 81);
    this.layoutTestLocation.Name = "layoutTestLocation";
    this.layoutTestLocation.Size = new Size(328, 17);
    this.layoutTestLocation.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutProbeCalibrationDate.Control = (Control) this.probeCalibrationDateValue;
    componentResourceManager.ApplyResources((object) this.layoutProbeCalibrationDate, "layoutProbeCalibrationDate");
    this.layoutProbeCalibrationDate.Location = new Point(0, 115);
    this.layoutProbeCalibrationDate.Name = "layoutProbeCalibrationDate";
    this.layoutProbeCalibrationDate.Size = new Size(328, 17);
    this.layoutProbeCalibrationDate.TextSize = new Size(112 /*0x70*/, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 132);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(328, 136);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(0, 0);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(328, 30);
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    this.layoutProbeSerialNumber.Control = (Control) this.ProbeSerialNo;
    componentResourceManager.ApplyResources((object) this.layoutProbeSerialNumber, "layoutProbeSerialNumber");
    this.layoutProbeSerialNumber.Location = new Point(0, 98);
    this.layoutProbeSerialNumber.Name = "layoutProbeSerialNumber";
    this.layoutProbeSerialNumber.Size = new Size(328, 17);
    this.layoutProbeSerialNumber.TextSize = new Size(112 /*0x70*/, 13);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.TestInformation);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(355, 317);
    this.Name = nameof (TestViewer);
    this.TestLayout.EndInit();
    this.TestLayout.ResumeLayout(false);
    this.ResultImage.Properties.EndInit();
    this.RightEarIcon.Properties.EndInit();
    this.LeftEarIcon.Properties.EndInit();
    ((ISupportInitialize) xyDiagram).EndInit();
    ((ISupportInitialize) pointSeriesLabel1).EndInit();
    ((ISupportInitialize) splineSeriesView1).EndInit();
    ((ISupportInitialize) series).EndInit();
    ((ISupportInitialize) pointSeriesLabel2).EndInit();
    ((ISupportInitialize) splineSeriesView2).EndInit();
    this.ResultGraph.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutLeftEarIcon.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutRightEarIcon.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem7.EndInit();
    this.layoutControlItem8.EndInit();
    this.layoutControlItem9.EndInit();
    this.TestInformation.EndInit();
    this.TestInformation.ResumeLayout(false);
    this.xtraTabTestResult.ResumeLayout(false);
    this.xtraTabTestComments.ResumeLayout(false);
    this.layoutControl2.EndInit();
    this.layoutControl2.ResumeLayout(false);
    this.TestComments.EndInit();
    this.TestCommentView.EndInit();
    this.commentAssignment.Properties.EndInit();
    this.layoutControlGroup3.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem10.EndInit();
    this.xtraTabDeviceInformation.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.layoutControlGroup2.EndInit();
    this.layoutDeviceSerialNumber.EndInit();
    this.layoutFirmwareBuild.EndInit();
    this.layoutTestFacility.EndInit();
    this.layoutTestLocation.EndInit();
    this.layoutProbeCalibrationDate.EndInit();
    this.emptySpaceItem2.EndInit();
    this.emptySpaceItem4.EndInit();
    this.layoutProbeSerialNumber.EndInit();
    this.ResumeLayout(false);
  }
}
