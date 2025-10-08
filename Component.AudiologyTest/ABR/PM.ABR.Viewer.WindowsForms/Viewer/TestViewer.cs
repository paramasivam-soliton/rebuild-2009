// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Viewer.TestViewer
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using DevExpress.LookAndFeel;
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
using PathMedical.AudiologyTest.Properties;
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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Viewer;

[ToolboxItem(false)]
public sealed class TestViewer : 
  PathMedical.UserInterface.WindowsForms.ModelViewController.View,
  ITestDetailView,
  IView,
  IDisposable,
  ISupportControllerAction,
  ISupportUserInterfaceManager
{
  private Series zeroPointLine;
  private Series curve;
  private Series minusSigmaLine;
  private Series plusSigmaLine;
  private Series leftMarker;
  private Series rightMarker;
  private byte counter;
  private AbrTestInformation testDetailInformation;
  private DevExpressSingleSelectionGridViewHelper<Comment> commentGridViewHelper;
  private IContainer components;
  private LayoutControl TestLayout;
  private LayoutControlGroup layoutControlGroup1;
  private PictureEdit LeftEarIcon;
  private LayoutControlItem layoutControlItem1;
  private LabelControl TestResultHeader;
  private LayoutControlItem layoutControlItem2;
  private PictureEdit RightEarIcon;
  private LayoutControlItem layoutControlItem3;
  private ChartControl ResultGraph;
  private LayoutControlItem layoutControlItem4;
  private XtraTabControl xtraTabControl1;
  private XtraTabPage xtraTabTestResult;
  private XtraTabPage xtraTabTestComments;
  private GridControl gridControl1;
  private GridView TestCommentView;
  private GridColumn commentDate;
  private GridColumn TestComment;
  private SimpleButton redElectrode;
  private SimpleButton whiteElectrode;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlItem layoutControlItem10;
  private ProgressBarControl abrEeg;
  private LabelControl abrEegText;
  private LayoutControlItem layoutControlItem12;
  private LayoutControlItem layoutControlItem13;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem2;
  private EmptySpaceItem emptySpaceItem3;
  private PictureEdit impedanceRed;
  private LayoutControlItem layoutControlItem11;
  private PictureEdit impdanceWhite;
  private LayoutControlItem layoutControlItem14;
  private PictureEdit ResultImage;
  private LayoutControlItem layoutControlItem6;
  private LabelControl ErrorDetail;
  private LabelControl ResultText;
  private LayoutControlItem layoutControlItem7;
  private LayoutControlItem layoutControlItem8;
  private LabelControl TimeStamp;
  private LayoutControlItem layoutControlItem9;
  private XtraTabPage xtraTabDeviceInformation;
  private GridColumn Commentator;
  private SimpleButton addComment;
  private DevExpressComboBoxEdit commentAssignment;
  private LabelControl probeCalibrationDateValue;
  private LabelControl locationTypeValue;
  private LabelControl testFacilityIDValue;
  private LabelControl FirmwareNo;
  private LabelControl ProbeSerialNo;
  private LabelControl DeviceSerialNo;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutDeviceInformation;
  private LayoutControlItem layoutLocation;
  private LayoutControlItem layoutDeviceSerial;
  private LayoutControlItem layoutFirmwareBuild;
  private LayoutControlItem layoutProbeSerial;
  private LayoutControlItem layoutTestFacility;
  private LayoutControlItem layoutProbeCalibrationDate;
  private EmptySpaceItem emptySpaceItem4;
  private EmptySpaceItem emptySpaceItem5;
  private LayoutControl layoutControl2;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutControlItem15;
  private LayoutControlItem layoutControlItem16;
  private LayoutControlItem layoutControlItem17;

  public TestViewer()
  {
    this.InitializeComponent();
    this.whiteElectrode.StyleController = (IStyleController) null;
    this.whiteElectrode.ButtonStyle = BorderStyles.UltraFlat;
    this.redElectrode.StyleController = (IStyleController) null;
    this.redElectrode.ButtonStyle = BorderStyles.UltraFlat;
    this.Dock = DockStyle.Fill;
    this.LeftEarIcon.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Ear_Left") as Image;
    this.RightEarIcon.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Ear_Right") as Image;
    this.impedanceRed.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("impdance_red") as Image;
    this.impdanceWhite.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("impdance_white") as Image;
  }

  public TestViewer(AbrTestInformation testInformation)
    : this()
  {
    this.Dock = DockStyle.None;
    this.FillFields(testInformation);
  }

  public TestViewer(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
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
    if (eventArgs != null && eventArgs.ChangedObject is AudiologyTestInformation && eventArgs.ChangeType == ChangeType.SelectionChanged && AbrComponent.Instance.HostControl != null)
    {
      AbrComponent.Instance.HostControl.Controls.Clear();
      AbrComponent.Instance.HostControl.Controls.Add((Control) this);
    }
    if (eventArgs == null || !(eventArgs.ChangedObject is AbrTestInformation))
      return;
    this.testDetailInformation = eventArgs.ChangedObject as AbrTestInformation;
    this.FillFields(this.testDetailInformation);
  }

  private void FillFields(AbrTestInformation abrTestInformation)
  {
    if (abrTestInformation == null)
      return;
    int? ear = abrTestInformation.Ear;
    if (!ear.HasValue)
      return;
    switch (ear.GetValueOrDefault())
    {
      case 7:
        this.LoadEar(abrTestInformation, this.LeftEarIcon);
        break;
      case 112 /*0x70*/:
        this.LoadEar(abrTestInformation, this.RightEarIcon);
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
  }

  private void LoadEar(AbrTestInformation testDetail, PictureEdit earPicture)
  {
    if (testDetail == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("TestDetail");
    if (earPicture == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (earPicture));
    if (earPicture == this.LeftEarIcon)
    {
      this.layoutControlItem3.ContentVisible = false;
      this.layoutControlItem1.ContentVisible = true;
    }
    else
    {
      this.layoutControlItem3.ContentVisible = true;
      this.layoutControlItem1.ContentVisible = false;
    }
    int? nullable1 = testDetail.TestResult;
    if (nullable1.HasValue)
    {
      switch (nullable1.GetValueOrDefault())
      {
        case 13175:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_ClearResponse");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("PassResultText");
          this.ErrorDetail.Text = (string) null;
          goto label_20;
        case 16706:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("MeasurementAbort");
          goto label_20;
        case 16707:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationAbort");
          goto label_20;
        case 17221:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationError");
          goto label_20;
        case 17236:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationTimeout");
          goto label_20;
        case 17740:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ElectrodeLoose");
          goto label_20;
        case 19526:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationLeaky");
          goto label_20;
        case 20035:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Noisy");
          goto label_20;
        case 29555:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ProbeError");
          goto label_20;
        case 30583 /*0x7777*/:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_NoClearResponse");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ReferredResultText");
          this.ErrorDetail.Text = (string) null;
          goto label_20;
      }
    }
    this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
    this.ResultText.Text = (string) null;
label_20:
    this.CreateChartControls();
    nullable1 = testDetail.Frames;
    int num1 = 0;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
    {
      if (testDetail.Graph != null)
      {
        for (int index = 0; index < testDetail.Graph.Length; ++index)
        {
          SeriesPointCollection points = this.curve.Points;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (ValueType) index;
          object[] objArray = new object[1];
          double? graphScale = testDetail.GraphScale;
          double num2 = (double) testDetail.Graph[index];
          double? nullable2 = graphScale.HasValue ? new double?(graphScale.GetValueOrDefault() * num2) : new double?();
          double num3 = 7.0;
          objArray[0] = (object) (nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num3) : new double?());
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
          this.rightMarker.Points.Add(new SeriesPoint((double) (testDetail.Graph.Length - 1), new double[1]
          {
            (double) index
          }));
      }
    }
    else if (testDetail.CalibrationGraph != null)
    {
      for (int index = 0; index < testDetail.CalibrationGraph.Length; ++index)
      {
        SeriesPointCollection points = this.curve.Points;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) index;
        object[] objArray = new object[1];
        double? calibrationGraphScale = testDetail.CalibrationGraphScale;
        double num4 = (double) testDetail.CalibrationGraph[index];
        double? nullable3 = calibrationGraphScale.HasValue ? new double?(calibrationGraphScale.GetValueOrDefault() * num4) : new double?();
        double num5 = 256.0;
        double? nullable4 = nullable3.HasValue ? new double?(nullable3.GetValueOrDefault() / num5) : new double?();
        double num6 = 2.0;
        double? nullable5;
        if (!nullable4.HasValue)
        {
          nullable3 = new double?();
          nullable5 = nullable3;
        }
        else
          nullable5 = new double?(nullable4.GetValueOrDefault() * num6);
        objArray[0] = (object) nullable5;
        SeriesPoint point = new SeriesPoint((object) local, objArray);
        points.Add(point);
        this.zeroPointLine.Points.Add(new SeriesPoint((double) index, new double[1]));
      }
    }
    nullable1 = testDetail.Eeg;
    if (nullable1.HasValue)
    {
      ProgressBarControl abrEeg = this.abrEeg;
      nullable1 = testDetail.Eeg;
      int num7 = nullable1.Value;
      abrEeg.Position = num7;
    }
    this.abrEeg.StyleController = (IStyleController) null;
    this.abrEeg.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
    this.abrEeg.Properties.LookAndFeel.Style = LookAndFeelStyle.UltraFlat;
    nullable1 = testDetail.Version;
    int num8 = 20;
    if (nullable1.GetValueOrDefault() > num8 & nullable1.HasValue)
    {
      if (Math.Round((double) testDetail.Impedances[0] / 100.0) <= 4.0)
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_green") as Image;
      else if (Math.Round((double) testDetail.Impedances[0] / 100.0) <= 12.0)
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_yellow") as Image;
      else
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_red") as Image;
      if (Math.Round((double) testDetail.Impedances[1] / 100.0) <= 4.0)
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_green") as Image;
      else if (Math.Round((double) testDetail.Impedances[1] / 100.0) <= 12.0)
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_yellow") as Image;
      else
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_red") as Image;
      this.whiteElectrode.Text = $"{Math.Round((double) testDetail.Impedances[0] / 100.0)}kΩ";
      this.redElectrode.Text = $"{Math.Round((double) testDetail.Impedances[1] / 100.0)}kΩ";
    }
    else
    {
      if (Math.Round((double) testDetail.Impedances[0] / 1000.0) <= 4.0)
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_green") as Image;
      else if (Math.Round((double) testDetail.Impedances[0] / 1000.0) <= 12.0)
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_yellow") as Image;
      else
        this.whiteElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_red") as Image;
      if (Math.Round((double) testDetail.Impedances[1] / 1000.0) <= 4.0)
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_green") as Image;
      else if (Math.Round((double) testDetail.Impedances[1] / 1000.0) <= 12.0)
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_yellow") as Image;
      else
        this.redElectrode.BackgroundImage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("OhmField_red") as Image;
      this.whiteElectrode.Text = $"{Math.Round((double) testDetail.Impedances[0] / 1000.0)}kΩ";
      this.redElectrode.Text = $"{Math.Round((double) testDetail.Impedances[1] / 1000.0)}kΩ";
    }
    long? nullable6 = testDetail.InstrumentSerialNumber;
    if (nullable6.HasValue)
    {
      LabelControl deviceSerialNo = this.DeviceSerialNo;
      nullable6 = testDetail.InstrumentSerialNumber;
      string str = nullable6.ToString();
      deviceSerialNo.Text = str;
    }
    nullable1 = testDetail.FirmwareVersion;
    if (nullable1.HasValue)
    {
      LabelControl firmwareNo = this.FirmwareNo;
      nullable1 = testDetail.FirmwareVersion;
      string str = nullable1.ToString();
      firmwareNo.Text = str;
    }
    nullable6 = testDetail.ProbeSerialNumber;
    if (nullable6.HasValue)
    {
      LabelControl probeSerialNo = this.ProbeSerialNo;
      nullable6 = testDetail.ProbeSerialNumber;
      string str = nullable6.ToString();
      probeSerialNo.Text = str;
    }
    DateTime? nullable7 = testDetail.ProbeCalibrationDate;
    if (nullable7.HasValue)
    {
      LabelControl calibrationDateValue = this.probeCalibrationDateValue;
      nullable7 = testDetail.ProbeCalibrationDate;
      string str = nullable7.ToString();
      calibrationDateValue.Text = str;
    }
    Guid? nullable8 = testDetail.FacilityId;
    if (nullable8.HasValue)
      this.testFacilityIDValue.Text = FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
      {
        Guid id = f.Id;
        Guid? facilityId = testDetail.FacilityId;
        return facilityId.HasValue && id == facilityId.GetValueOrDefault();
      })).Select<Facility, string>((Func<Facility, string>) (f => f.Code)).FirstOrDefault<string>();
    else
      this.testFacilityIDValue.Text = Resources.TestViewer_NoValueFound;
    nullable8 = testDetail.LocationId;
    if (nullable8.HasValue)
      this.locationTypeValue.Text = LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l =>
      {
        Guid id = l.Id;
        Guid? locationId = testDetail.LocationId;
        return locationId.HasValue && id == locationId.GetValueOrDefault();
      })).Select<LocationType, string>((Func<LocationType, string>) (l => l.Name)).FirstOrDefault<string>();
    else
      this.locationTypeValue.Text = Resources.TestViewer_NoValueFound;
    this.ResultGraph.Legend.Visible = false;
    this.ResultGraph.Series.Add(this.zeroPointLine);
    this.ResultGraph.Series.Add(this.leftMarker);
    this.ResultGraph.Series.Add(this.plusSigmaLine);
    this.ResultGraph.Series.Add(this.minusSigmaLine);
    this.ResultGraph.Series.Add(this.rightMarker);
    this.ResultGraph.Series.Add(this.curve);
    LabelControl timeStamp = this.TimeStamp;
    nullable7 = testDetail.TestTimeStamp;
    string str1 = nullable7.ToString();
    timeStamp.Text = str1;
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
      AbrTestManager.Instance.AddComment((object) freeTextComment as FreeTextComment);
    }
    if (obj is PredefinedComment)
      AbrTestManager.Instance.AddPredefinedComment(((PredefinedComment) obj).Clone() as PredefinedComment);
    this.commentAssignment.SelectedIndex = -1;
    CommandManager.Instance.SetSaved();
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool flag = this.ViewMode != 0;
    this.addComment.Enabled = flag;
    this.commentAssignment.Enabled = flag;
  }

  private void xtraTabDeviceInformation_Paint(object sender, PaintEventArgs e)
  {
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
    this.impdanceWhite = new PictureEdit();
    this.impedanceRed = new PictureEdit();
    this.abrEeg = new ProgressBarControl();
    this.abrEegText = new LabelControl();
    this.whiteElectrode = new SimpleButton();
    this.redElectrode = new SimpleButton();
    this.ResultGraph = new ChartControl();
    this.RightEarIcon = new PictureEdit();
    this.TestResultHeader = new LabelControl();
    this.LeftEarIcon = new PictureEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutControlItem10 = new LayoutControlItem();
    this.layoutControlItem12 = new LayoutControlItem();
    this.layoutControlItem13 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.layoutControlItem11 = new LayoutControlItem();
    this.layoutControlItem14 = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem7 = new LayoutControlItem();
    this.layoutControlItem8 = new LayoutControlItem();
    this.layoutControlItem9 = new LayoutControlItem();
    this.xtraTabControl1 = new XtraTabControl();
    this.xtraTabTestResult = new XtraTabPage();
    this.xtraTabTestComments = new XtraTabPage();
    this.commentAssignment = new DevExpressComboBoxEdit();
    this.addComment = new SimpleButton();
    this.gridControl1 = new GridControl();
    this.TestCommentView = new GridView();
    this.commentDate = new GridColumn();
    this.TestComment = new GridColumn();
    this.Commentator = new GridColumn();
    this.xtraTabDeviceInformation = new XtraTabPage();
    this.layoutControl1 = new LayoutControl();
    this.locationTypeValue = new LabelControl();
    this.probeCalibrationDateValue = new LabelControl();
    this.DeviceSerialNo = new LabelControl();
    this.FirmwareNo = new LabelControl();
    this.testFacilityIDValue = new LabelControl();
    this.ProbeSerialNo = new LabelControl();
    this.layoutDeviceInformation = new LayoutControlGroup();
    this.layoutLocation = new LayoutControlItem();
    this.layoutDeviceSerial = new LayoutControlItem();
    this.layoutFirmwareBuild = new LayoutControlItem();
    this.layoutTestFacility = new LayoutControlItem();
    this.layoutProbeCalibrationDate = new LayoutControlItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.emptySpaceItem5 = new EmptySpaceItem();
    this.layoutProbeSerial = new LayoutControlItem();
    this.layoutControl2 = new LayoutControl();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem15 = new LayoutControlItem();
    this.layoutControlItem16 = new LayoutControlItem();
    this.layoutControlItem17 = new LayoutControlItem();
    this.TestLayout.BeginInit();
    this.TestLayout.SuspendLayout();
    this.ResultImage.Properties.BeginInit();
    this.impdanceWhite.Properties.BeginInit();
    this.impedanceRed.Properties.BeginInit();
    this.abrEeg.Properties.BeginInit();
    this.ResultGraph.BeginInit();
    ((ISupportInitialize) xyDiagram).BeginInit();
    ((ISupportInitialize) series).BeginInit();
    ((ISupportInitialize) pointSeriesLabel1).BeginInit();
    ((ISupportInitialize) splineSeriesView1).BeginInit();
    ((ISupportInitialize) pointSeriesLabel2).BeginInit();
    ((ISupportInitialize) splineSeriesView2).BeginInit();
    this.RightEarIcon.Properties.BeginInit();
    this.LeftEarIcon.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutControlItem10.BeginInit();
    this.layoutControlItem12.BeginInit();
    this.layoutControlItem13.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.layoutControlItem11.BeginInit();
    this.layoutControlItem14.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem7.BeginInit();
    this.layoutControlItem8.BeginInit();
    this.layoutControlItem9.BeginInit();
    this.xtraTabControl1.BeginInit();
    this.xtraTabControl1.SuspendLayout();
    this.xtraTabTestResult.SuspendLayout();
    this.xtraTabTestComments.SuspendLayout();
    this.commentAssignment.Properties.BeginInit();
    this.gridControl1.BeginInit();
    this.TestCommentView.BeginInit();
    this.xtraTabDeviceInformation.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.layoutDeviceInformation.BeginInit();
    this.layoutLocation.BeginInit();
    this.layoutDeviceSerial.BeginInit();
    this.layoutFirmwareBuild.BeginInit();
    this.layoutTestFacility.BeginInit();
    this.layoutProbeCalibrationDate.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.emptySpaceItem5.BeginInit();
    this.layoutProbeSerial.BeginInit();
    this.layoutControl2.BeginInit();
    this.layoutControl2.SuspendLayout();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem15.BeginInit();
    this.layoutControlItem16.BeginInit();
    this.layoutControlItem17.BeginInit();
    this.SuspendLayout();
    this.TestLayout.Controls.Add((Control) this.TimeStamp);
    this.TestLayout.Controls.Add((Control) this.ErrorDetail);
    this.TestLayout.Controls.Add((Control) this.ResultText);
    this.TestLayout.Controls.Add((Control) this.ResultImage);
    this.TestLayout.Controls.Add((Control) this.impdanceWhite);
    this.TestLayout.Controls.Add((Control) this.impedanceRed);
    this.TestLayout.Controls.Add((Control) this.abrEeg);
    this.TestLayout.Controls.Add((Control) this.abrEegText);
    this.TestLayout.Controls.Add((Control) this.whiteElectrode);
    this.TestLayout.Controls.Add((Control) this.redElectrode);
    this.TestLayout.Controls.Add((Control) this.ResultGraph);
    this.TestLayout.Controls.Add((Control) this.RightEarIcon);
    this.TestLayout.Controls.Add((Control) this.TestResultHeader);
    this.TestLayout.Controls.Add((Control) this.LeftEarIcon);
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
    componentResourceManager.ApplyResources((object) this.impdanceWhite, "impdanceWhite");
    this.impdanceWhite.MinimumSize = new Size(12, 22);
    this.impdanceWhite.Name = "impdanceWhite";
    this.impdanceWhite.Properties.Appearance.BackColor = Color.Transparent;
    this.impdanceWhite.Properties.Appearance.Options.UseBackColor = true;
    this.impdanceWhite.Properties.BorderStyle = BorderStyles.NoBorder;
    this.impdanceWhite.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.impedanceRed, "impedanceRed");
    this.impedanceRed.MinimumSize = new Size(12, 22);
    this.impedanceRed.Name = "impedanceRed";
    this.impedanceRed.Properties.Appearance.BackColor = Color.Transparent;
    this.impedanceRed.Properties.Appearance.Options.UseBackColor = true;
    this.impedanceRed.Properties.BorderStyle = BorderStyles.NoBorder;
    this.impedanceRed.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.abrEeg, "abrEeg");
    this.abrEeg.Name = "abrEeg";
    this.abrEeg.Properties.Appearance.BackColor = Color.Gray;
    this.abrEeg.Properties.Appearance.BackColor2 = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
    this.abrEeg.Properties.Appearance.BorderColor = Color.FromArgb(64 /*0x40*/, 64 /*0x40*/, 64 /*0x40*/);
    this.abrEeg.Properties.Appearance.ForeColor = Color.Black;
    this.abrEeg.Properties.Appearance.ForeColor2 = Color.Black;
    this.abrEeg.Properties.Appearance.GradientMode = LinearGradientMode.Vertical;
    this.abrEeg.Properties.BorderStyle = BorderStyles.Simple;
    this.abrEeg.Properties.EndColor = Color.Black;
    this.abrEeg.Properties.PercentView = false;
    this.abrEeg.Properties.ProgressViewStyle = ProgressViewStyle.Solid;
    this.abrEeg.Properties.StartColor = Color.Black;
    this.abrEeg.StyleController = (IStyleController) this.TestLayout;
    componentResourceManager.ApplyResources((object) this.abrEegText, "abrEegText");
    this.abrEegText.Name = "abrEegText";
    this.abrEegText.StyleController = (IStyleController) this.TestLayout;
    this.whiteElectrode.Appearance.BackColor = Color.Transparent;
    this.whiteElectrode.Appearance.BackColor2 = Color.Transparent;
    this.whiteElectrode.Appearance.BorderColor = Color.Transparent;
    this.whiteElectrode.Appearance.Font = new Font("Arial Unicode MS", 13f, FontStyle.Bold);
    this.whiteElectrode.Appearance.ForeColor = Color.Black;
    this.whiteElectrode.Appearance.Options.UseBackColor = true;
    this.whiteElectrode.Appearance.Options.UseBorderColor = true;
    this.whiteElectrode.Appearance.Options.UseFont = true;
    this.whiteElectrode.Appearance.Options.UseForeColor = true;
    this.whiteElectrode.AutoSizeInLayoutControl = false;
    componentResourceManager.ApplyResources((object) this.whiteElectrode, "whiteElectrode");
    this.whiteElectrode.MinimumSize = new Size(52, 22);
    this.whiteElectrode.Name = "whiteElectrode";
    this.whiteElectrode.StyleController = (IStyleController) this.TestLayout;
    this.redElectrode.AccessibleRole = AccessibleRole.None;
    this.redElectrode.AllowFocus = false;
    this.redElectrode.Appearance.BackColor = Color.Transparent;
    this.redElectrode.Appearance.BackColor2 = Color.Transparent;
    this.redElectrode.Appearance.BorderColor = Color.Transparent;
    this.redElectrode.Appearance.Font = new Font("Arial Unicode MS", 13f, FontStyle.Bold);
    this.redElectrode.Appearance.ForeColor = Color.Black;
    this.redElectrode.Appearance.Options.UseBackColor = true;
    this.redElectrode.Appearance.Options.UseBorderColor = true;
    this.redElectrode.Appearance.Options.UseFont = true;
    this.redElectrode.Appearance.Options.UseForeColor = true;
    this.redElectrode.AutoSizeInLayoutControl = false;
    componentResourceManager.ApplyResources((object) this.redElectrode, "redElectrode");
    this.redElectrode.Cursor = Cursors.Default;
    this.redElectrode.MinimumSize = new Size(52, 22);
    this.redElectrode.Name = "redElectrode";
    this.redElectrode.StyleController = (IStyleController) this.TestLayout;
    this.ResultGraph.BorderOptions.Visible = false;
    xyDiagram.AxisX.Range.Auto = false;
    xyDiagram.AxisX.Range.MaxValueSerializable = "200";
    xyDiagram.AxisX.Range.MinValueSerializable = "0";
    xyDiagram.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
    xyDiagram.AxisX.Range.SideMarginsEnabled = true;
    xyDiagram.AxisX.Visible = false;
    xyDiagram.AxisX.VisibleInPanesSerializable = "-1";
    xyDiagram.AxisY.GridLines.Visible = false;
    xyDiagram.AxisY.Range.Auto = false;
    xyDiagram.AxisY.Range.MaxValueSerializable = "30";
    xyDiagram.AxisY.Range.MinValueSerializable = "-30";
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
    componentResourceManager.ApplyResources((object) series, "series5");
    splineSeriesView1.LineMarkerOptions.Visible = false;
    series.View = (SeriesViewBase) splineSeriesView1;
    this.ResultGraph.SeriesSerializable = new Series[1]
    {
      series
    };
    pointSeriesLabel2.LineVisible = true;
    this.ResultGraph.SeriesTemplate.Label = (SeriesLabelBase) pointSeriesLabel2;
    this.ResultGraph.SeriesTemplate.View = (SeriesViewBase) splineSeriesView2;
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
    this.layoutControlGroup1.AppearanceGroup.BackColor = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.BackColor2 = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[17]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.layoutControlItem10,
      (BaseLayoutItem) this.layoutControlItem12,
      (BaseLayoutItem) this.layoutControlItem13,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.layoutControlItem11,
      (BaseLayoutItem) this.layoutControlItem14,
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
    this.layoutControlItem1.Control = (Control) this.LeftEarIcon;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(32 /*0x20*/, 30);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
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
    this.layoutControlItem4.Size = new Size(328, 124);
    this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    this.layoutControlItem3.Control = (Control) this.RightEarIcon;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(296, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(32 /*0x20*/, 30);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutControlItem5.Control = (Control) this.whiteElectrode;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(16 /*0x10*/, 178);
    this.layoutControlItem5.MinSize = new Size(40, 26);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(60, 26);
    this.layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    this.layoutControlItem10.Control = (Control) this.redElectrode;
    componentResourceManager.ApplyResources((object) this.layoutControlItem10, "layoutControlItem10");
    this.layoutControlItem10.Location = new Point(90, 178);
    this.layoutControlItem10.MinSize = new Size(40, 26);
    this.layoutControlItem10.Name = "layoutControlItem10";
    this.layoutControlItem10.Size = new Size(60, 26);
    this.layoutControlItem10.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem10.TextSize = new Size(0, 0);
    this.layoutControlItem10.TextToControlDistance = 0;
    this.layoutControlItem10.TextVisible = false;
    this.layoutControlItem12.Control = (Control) this.abrEegText;
    componentResourceManager.ApplyResources((object) this.layoutControlItem12, "layoutControlItem12");
    this.layoutControlItem12.Location = new Point(256 /*0x0100*/, 154);
    this.layoutControlItem12.MinSize = new Size(67, 17);
    this.layoutControlItem12.Name = "layoutControlItem12";
    this.layoutControlItem12.Size = new Size(72, 32 /*0x20*/);
    this.layoutControlItem12.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem12.TextSize = new Size(0, 0);
    this.layoutControlItem12.TextToControlDistance = 0;
    this.layoutControlItem12.TextVisible = false;
    this.layoutControlItem13.Control = (Control) this.abrEeg;
    componentResourceManager.ApplyResources((object) this.layoutControlItem13, "layoutControlItem13");
    this.layoutControlItem13.Location = new Point(256 /*0x0100*/, 186);
    this.layoutControlItem13.MinSize = new Size(54, 16 /*0x10*/);
    this.layoutControlItem13.Name = "layoutControlItem13";
    this.layoutControlItem13.Size = new Size(72, 18);
    this.layoutControlItem13.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem13.TextSize = new Size(0, 0);
    this.layoutControlItem13.TextToControlDistance = 0;
    this.layoutControlItem13.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(150, 154);
    this.emptySpaceItem1.MinSize = new Size(104, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(106, 50);
    this.emptySpaceItem1.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(76, 154);
    this.emptySpaceItem2.MinSize = new Size(1, 24);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(74, 24);
    this.emptySpaceItem2.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(0, 154);
    this.emptySpaceItem3.MinSize = new Size(1, 24);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(76, 24);
    this.emptySpaceItem3.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    this.layoutControlItem11.Control = (Control) this.impedanceRed;
    componentResourceManager.ApplyResources((object) this.layoutControlItem11, "layoutControlItem11");
    this.layoutControlItem11.Location = new Point(76, 178);
    this.layoutControlItem11.MinSize = new Size(12, 12);
    this.layoutControlItem11.Name = "layoutControlItem11";
    this.layoutControlItem11.Size = new Size(14, 26);
    this.layoutControlItem11.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem11.TextSize = new Size(0, 0);
    this.layoutControlItem11.TextToControlDistance = 0;
    this.layoutControlItem11.TextVisible = false;
    this.layoutControlItem14.Control = (Control) this.impdanceWhite;
    componentResourceManager.ApplyResources((object) this.layoutControlItem14, "layoutControlItem14");
    this.layoutControlItem14.Location = new Point(0, 178);
    this.layoutControlItem14.MinSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.layoutControlItem14.Name = "layoutControlItem14";
    this.layoutControlItem14.Size = new Size(16 /*0x10*/, 26);
    this.layoutControlItem14.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem14.TextSize = new Size(0, 0);
    this.layoutControlItem14.TextToControlDistance = 0;
    this.layoutControlItem14.TextVisible = false;
    this.layoutControlItem6.Control = (Control) this.ResultImage;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(0, 204);
    this.layoutControlItem6.MinSize = new Size(24, 24);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(59, 64 /*0x40*/);
    this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.layoutControlItem7.Control = (Control) this.ResultText;
    componentResourceManager.ApplyResources((object) this.layoutControlItem7, "layoutControlItem7");
    this.layoutControlItem7.Location = new Point(59, 204);
    this.layoutControlItem7.MinSize = new Size(67, 17);
    this.layoutControlItem7.Name = "layoutControlItem7";
    this.layoutControlItem7.Size = new Size(269, 22);
    this.layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem7.TextSize = new Size(0, 0);
    this.layoutControlItem7.TextToControlDistance = 0;
    this.layoutControlItem7.TextVisible = false;
    this.layoutControlItem8.Control = (Control) this.ErrorDetail;
    componentResourceManager.ApplyResources((object) this.layoutControlItem8, "layoutControlItem8");
    this.layoutControlItem8.Location = new Point(59, 226);
    this.layoutControlItem8.MinSize = new Size(67, 17);
    this.layoutControlItem8.Name = "layoutControlItem8";
    this.layoutControlItem8.Size = new Size(269, 21);
    this.layoutControlItem8.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem8.TextSize = new Size(0, 0);
    this.layoutControlItem8.TextToControlDistance = 0;
    this.layoutControlItem8.TextVisible = false;
    this.layoutControlItem9.Control = (Control) this.TimeStamp;
    componentResourceManager.ApplyResources((object) this.layoutControlItem9, "layoutControlItem9");
    this.layoutControlItem9.Location = new Point(59, 247);
    this.layoutControlItem9.MinSize = new Size(67, 17);
    this.layoutControlItem9.Name = "layoutControlItem9";
    this.layoutControlItem9.Size = new Size(269, 21);
    this.layoutControlItem9.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem9.TextSize = new Size(0, 0);
    this.layoutControlItem9.TextToControlDistance = 0;
    this.layoutControlItem9.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.xtraTabControl1, "xtraTabControl1");
    this.xtraTabControl1.Name = "xtraTabControl1";
    this.xtraTabControl1.SelectedTabPage = this.xtraTabTestResult;
    this.xtraTabControl1.TabPages.AddRange(new XtraTabPage[3]
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
    componentResourceManager.ApplyResources((object) this.addComment, "addComment");
    this.addComment.Name = "addComment";
    this.addComment.StyleController = (IStyleController) this.layoutControl2;
    componentResourceManager.ApplyResources((object) this.gridControl1, "gridControl1");
    this.gridControl1.MainView = (BaseView) this.TestCommentView;
    this.gridControl1.Name = "gridControl1";
    this.gridControl1.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.TestCommentView
    });
    this.TestCommentView.Columns.AddRange(new GridColumn[3]
    {
      this.commentDate,
      this.TestComment,
      this.Commentator
    });
    this.TestCommentView.GridControl = this.gridControl1;
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
    componentResourceManager.ApplyResources((object) this.Commentator, "Commentator");
    this.Commentator.FieldName = "Examiner";
    this.Commentator.Name = "Commentator";
    this.Commentator.OptionsColumn.AllowEdit = false;
    this.xtraTabDeviceInformation.Controls.Add((Control) this.layoutControl1);
    this.xtraTabDeviceInformation.Name = "xtraTabDeviceInformation";
    componentResourceManager.ApplyResources((object) this.xtraTabDeviceInformation, "xtraTabDeviceInformation");
    this.xtraTabDeviceInformation.Paint += new PaintEventHandler(this.xtraTabDeviceInformation_Paint);
    this.layoutControl1.Controls.Add((Control) this.locationTypeValue);
    this.layoutControl1.Controls.Add((Control) this.probeCalibrationDateValue);
    this.layoutControl1.Controls.Add((Control) this.DeviceSerialNo);
    this.layoutControl1.Controls.Add((Control) this.FirmwareNo);
    this.layoutControl1.Controls.Add((Control) this.testFacilityIDValue);
    this.layoutControl1.Controls.Add((Control) this.ProbeSerialNo);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutDeviceInformation;
    componentResourceManager.ApplyResources((object) this.locationTypeValue, "locationTypeValue");
    this.locationTypeValue.Name = "locationTypeValue";
    this.locationTypeValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDateValue, "probeCalibrationDateValue");
    this.probeCalibrationDateValue.Name = "probeCalibrationDateValue";
    this.probeCalibrationDateValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.DeviceSerialNo, "DeviceSerialNo");
    this.DeviceSerialNo.Name = "DeviceSerialNo";
    this.DeviceSerialNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.FirmwareNo, "FirmwareNo");
    this.FirmwareNo.Name = "FirmwareNo";
    this.FirmwareNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.testFacilityIDValue, "testFacilityIDValue");
    this.testFacilityIDValue.Name = "testFacilityIDValue";
    this.testFacilityIDValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.ProbeSerialNo, "ProbeSerialNo");
    this.ProbeSerialNo.Name = "ProbeSerialNo";
    this.ProbeSerialNo.StyleController = (IStyleController) this.layoutControl1;
    this.layoutDeviceInformation.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutDeviceInformation.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutDeviceInformation, "layoutDeviceInformation");
    this.layoutDeviceInformation.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutDeviceInformation.GroupBordersVisible = false;
    this.layoutDeviceInformation.Items.AddRange(new BaseLayoutItem[8]
    {
      (BaseLayoutItem) this.layoutLocation,
      (BaseLayoutItem) this.layoutDeviceSerial,
      (BaseLayoutItem) this.layoutFirmwareBuild,
      (BaseLayoutItem) this.layoutTestFacility,
      (BaseLayoutItem) this.layoutProbeCalibrationDate,
      (BaseLayoutItem) this.emptySpaceItem4,
      (BaseLayoutItem) this.emptySpaceItem5,
      (BaseLayoutItem) this.layoutProbeSerial
    });
    this.layoutDeviceInformation.Location = new Point(0, 0);
    this.layoutDeviceInformation.Name = "layoutDeviceInformation";
    this.layoutDeviceInformation.Size = new Size(348, 288);
    this.layoutDeviceInformation.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutDeviceInformation.TextVisible = false;
    this.layoutLocation.Control = (Control) this.locationTypeValue;
    componentResourceManager.ApplyResources((object) this.layoutLocation, "layoutLocation");
    this.layoutLocation.Location = new Point(0, 81);
    this.layoutLocation.Name = "layoutLocation";
    this.layoutLocation.Size = new Size(328, 17);
    this.layoutLocation.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutDeviceSerial.Control = (Control) this.DeviceSerialNo;
    componentResourceManager.ApplyResources((object) this.layoutDeviceSerial, "layoutDeviceSerial");
    this.layoutDeviceSerial.Location = new Point(0, 30);
    this.layoutDeviceSerial.Name = "layoutDeviceSerial";
    this.layoutDeviceSerial.Size = new Size(328, 17);
    this.layoutDeviceSerial.TextSize = new Size(112 /*0x70*/, 13);
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
    this.layoutProbeCalibrationDate.Control = (Control) this.probeCalibrationDateValue;
    componentResourceManager.ApplyResources((object) this.layoutProbeCalibrationDate, "layoutProbeCalibrationDate");
    this.layoutProbeCalibrationDate.Location = new Point(0, 115);
    this.layoutProbeCalibrationDate.Name = "layoutProbeCalibrationDate";
    this.layoutProbeCalibrationDate.Size = new Size(328, 17);
    this.layoutProbeCalibrationDate.TextSize = new Size(112 /*0x70*/, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(0, 132);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(328, 136);
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem5, "emptySpaceItem5");
    this.emptySpaceItem5.Location = new Point(0, 0);
    this.emptySpaceItem5.Name = "emptySpaceItem5";
    this.emptySpaceItem5.Size = new Size(328, 30);
    this.emptySpaceItem5.TextSize = new Size(0, 0);
    this.layoutProbeSerial.Control = (Control) this.ProbeSerialNo;
    componentResourceManager.ApplyResources((object) this.layoutProbeSerial, "layoutProbeSerial");
    this.layoutProbeSerial.Location = new Point(0, 98);
    this.layoutProbeSerial.Name = "layoutProbeSerial";
    this.layoutProbeSerial.Size = new Size(328, 17);
    this.layoutProbeSerial.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutControl2.Controls.Add((Control) this.gridControl1);
    this.layoutControl2.Controls.Add((Control) this.addComment);
    this.layoutControl2.Controls.Add((Control) this.commentAssignment);
    componentResourceManager.ApplyResources((object) this.layoutControl2, "layoutControl2");
    this.layoutControl2.Name = "layoutControl2";
    this.layoutControl2.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlItem15,
      (BaseLayoutItem) this.layoutControlItem16,
      (BaseLayoutItem) this.layoutControlItem17
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(348, 288);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    this.layoutControlItem15.Control = (Control) this.gridControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlItem15, "layoutControlItem15");
    this.layoutControlItem15.Location = new Point(0, 0);
    this.layoutControlItem15.Name = "layoutControlItem15";
    this.layoutControlItem15.Size = new Size(328, 242);
    this.layoutControlItem15.TextSize = new Size(0, 0);
    this.layoutControlItem15.TextToControlDistance = 0;
    this.layoutControlItem15.TextVisible = false;
    this.layoutControlItem16.Control = (Control) this.commentAssignment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem16, "layoutControlItem16");
    this.layoutControlItem16.Location = new Point(0, 242);
    this.layoutControlItem16.Name = "layoutControlItem16";
    this.layoutControlItem16.Size = new Size(226, 26);
    this.layoutControlItem16.TextSize = new Size(0, 0);
    this.layoutControlItem16.TextToControlDistance = 0;
    this.layoutControlItem16.TextVisible = false;
    this.layoutControlItem17.Control = (Control) this.addComment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem17, "layoutControlItem17");
    this.layoutControlItem17.Location = new Point(226, 242);
    this.layoutControlItem17.Name = "layoutControlItem17";
    this.layoutControlItem17.Size = new Size(102, 26);
    this.layoutControlItem17.TextSize = new Size(0, 0);
    this.layoutControlItem17.TextToControlDistance = 0;
    this.layoutControlItem17.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.xtraTabControl1);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(355, 317);
    this.Name = nameof (TestViewer);
    this.TestLayout.EndInit();
    this.TestLayout.ResumeLayout(false);
    this.ResultImage.Properties.EndInit();
    this.impdanceWhite.Properties.EndInit();
    this.impedanceRed.Properties.EndInit();
    this.abrEeg.Properties.EndInit();
    ((ISupportInitialize) xyDiagram).EndInit();
    ((ISupportInitialize) pointSeriesLabel1).EndInit();
    ((ISupportInitialize) splineSeriesView1).EndInit();
    ((ISupportInitialize) series).EndInit();
    ((ISupportInitialize) pointSeriesLabel2).EndInit();
    ((ISupportInitialize) splineSeriesView2).EndInit();
    this.ResultGraph.EndInit();
    this.RightEarIcon.Properties.EndInit();
    this.LeftEarIcon.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutControlItem10.EndInit();
    this.layoutControlItem12.EndInit();
    this.layoutControlItem13.EndInit();
    this.emptySpaceItem1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.emptySpaceItem3.EndInit();
    this.layoutControlItem11.EndInit();
    this.layoutControlItem14.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem7.EndInit();
    this.layoutControlItem8.EndInit();
    this.layoutControlItem9.EndInit();
    this.xtraTabControl1.EndInit();
    this.xtraTabControl1.ResumeLayout(false);
    this.xtraTabTestResult.ResumeLayout(false);
    this.xtraTabTestComments.ResumeLayout(false);
    this.commentAssignment.Properties.EndInit();
    this.gridControl1.EndInit();
    this.TestCommentView.EndInit();
    this.xtraTabDeviceInformation.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.layoutDeviceInformation.EndInit();
    this.layoutLocation.EndInit();
    this.layoutDeviceSerial.EndInit();
    this.layoutFirmwareBuild.EndInit();
    this.layoutTestFacility.EndInit();
    this.layoutProbeCalibrationDate.EndInit();
    this.emptySpaceItem4.EndInit();
    this.emptySpaceItem5.EndInit();
    this.layoutProbeSerial.EndInit();
    this.layoutControl2.EndInit();
    this.layoutControl2.ResumeLayout(false);
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem15.EndInit();
    this.layoutControlItem16.EndInit();
    this.layoutControlItem17.EndInit();
    this.ResumeLayout(false);
  }
}
