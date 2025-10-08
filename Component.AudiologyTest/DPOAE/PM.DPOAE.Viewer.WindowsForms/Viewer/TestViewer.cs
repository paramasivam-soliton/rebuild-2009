// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Viewer.TestViewer
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Viewer;

[ToolboxItem(false)]
public sealed class TestViewer : 
  PathMedical.UserInterface.WindowsForms.ModelViewController.View,
  ITestDetailView,
  IView,
  IDisposable,
  ISupportControllerAction,
  ISupportUserInterfaceManager
{
  private Series noiseSeries;
  private Series dpoaePassSeries;
  private Series dpoaeFailSeries;
  private Series calibration;
  private DpoaeTestInformation testDetailInformation;
  private CustomAxisLabel[] customAxisLabelCollection = new CustomAxisLabel[0];
  private ChartControl calibrationGraph;
  private ChartControl resultGraph;
  private SecondaryAxisX dpoaePassAxisX;
  private SecondaryAxisX dpoaeFailAxisX;
  private XYDiagram dpoaeDiagram;
  private SecondaryAxisY dpoaeAxisY;
  private Size chartSize;
  private IContainer components;
  private LayoutControl layoutDetailView;
  private LayoutControlGroup layoutControlGroup1;
  private PictureEdit LeftEarIcon;
  private LayoutControlItem layoutControlItem1;
  private LabelControl TestResultHeader;
  private LayoutControlItem layoutControlItem2;
  private PictureEdit RightEarIcon;
  private LayoutControlItem layoutControlItem3;
  private XtraTabControl xtraTabControl1;
  private XtraTabPage xtraTabTestResult;
  private XtraTabPage xtraTabTestComments;
  private GridControl gridControl1;
  private GridView TestCommentView;
  private GridColumn commentDate;
  private GridColumn TestComment;
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
  private LabelControl testFacilityIDValue;
  private LabelControl locationTypeValue;
  private LabelControl probeCalibrationDateValue;
  private LabelControl FirmwareNo;
  private LabelControl ProbeSerialNo;
  private LabelControl DeviceSerialNo;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutDeviceSerial;
  private LayoutControlItem layoutFirmwareBuild;
  private LayoutControlItem layoutProbeSerial;
  private LayoutControlItem layoutTestFacility;
  private LayoutControlItem layoutLocation;
  private LayoutControlItem layoutProbeCalibrationDate;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem2;
  private ChartControl chartControl1;
  private LayoutControlItem layoutChartControl;
  private LayoutControl layoutControl2;
  private LayoutControlGroup layoutControlGroup3;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlItem layoutControlItem10;

  public TestViewer()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.LeftEarIcon.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Ear_Left") as Image;
    this.RightEarIcon.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Ear_Right") as Image;
    this.chartSize = this.chartControl1.Size;
  }

  public TestViewer(DpoaeTestInformation testInformation)
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
    DevExpressSingleSelectionGridViewHelper<Comment> selectionGridViewHelper = new DevExpressSingleSelectionGridViewHelper<Comment>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.TestCommentView, model, Triggers.ChangeSelection, "Comments");
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
    if (eventArgs != null && eventArgs.ChangedObject is AudiologyTestInformation && eventArgs.ChangeType == ChangeType.SelectionChanged && DpoaeComponent.Instance.HostControl != null)
    {
      DpoaeComponent.Instance.HostControl.Controls.Clear();
      DpoaeComponent.Instance.HostControl.Controls.Add((Control) this);
    }
    if (eventArgs == null || !(eventArgs.ChangedObject is DpoaeTestInformation))
      return;
    this.testDetailInformation = eventArgs.ChangedObject as DpoaeTestInformation;
    this.FillFields(this.testDetailInformation);
  }

  private void FillFields(DpoaeTestInformation dpoaeTestInformation)
  {
    if (dpoaeTestInformation == null)
      return;
    int? ear = dpoaeTestInformation.Ear;
    if (!ear.HasValue)
      return;
    switch (ear.GetValueOrDefault())
    {
      case 7:
        this.LoadEar(dpoaeTestInformation, this.LeftEarIcon);
        break;
      case 112 /*0x70*/:
        this.LoadEar(dpoaeTestInformation, this.RightEarIcon);
        break;
    }
  }

  private void CreateChartControls()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TestViewer));
    this.dpoaeDiagram = new XYDiagram();
    this.dpoaeDiagram.PaneDistance = 0;
    this.dpoaePassAxisX = new SecondaryAxisX();
    this.dpoaeFailAxisX = new SecondaryAxisX();
    this.dpoaePassAxisX = TestViewer.CreateDpoaeResultAxisX();
    this.dpoaeFailAxisX = TestViewer.CreateDpoaeResultAxisX();
    this.noiseSeries = this.CreateDpoaeSeries("noiseSeries", Color.Black);
    this.dpoaePassSeries = this.CreateDpoaeSeries("dpoaePassSeries", Color.Green);
    this.dpoaeFailSeries = this.CreateDpoaeSeries("dpoaeFailSeries", Color.Gray);
    this.resultGraph = this.CreateDpoaeResultGraph(this.chartSize);
    try
    {
      this.layoutDetailView.SuspendLayout();
      this.layoutChartControl.BeginInit();
      this.layoutDetailView.BeginInit();
      if (this.layoutDetailView.Controls["chartControl1"] != null)
        this.layoutDetailView.Controls.RemoveByKey("chartControl1");
      if (this.layoutDetailView.Controls["ResultGraph"] != null)
        this.layoutDetailView.Controls.RemoveByKey("ResultGraph");
      if (this.layoutDetailView.Controls["CalibrationGraph"] != null)
        this.layoutDetailView.Controls.RemoveByKey("CalibrationGraph");
      this.layoutDetailView.Controls.Add((Control) this.resultGraph);
      this.layoutChartControl.Control = (Control) this.resultGraph;
    }
    finally
    {
      this.layoutDetailView.EndInit();
      this.layoutChartControl.EndInit();
      this.layoutDetailView.ResumeLayout();
    }
  }

  private Series CreateDpoaeSeries(string name, Color color)
  {
    Series dpoaeSeries = new Series();
    dpoaeSeries.Label.Visible = false;
    dpoaeSeries.Name = name;
    dpoaeSeries.View.Color = color;
    if (dpoaeSeries.View is SideBySideBarSeriesView view)
    {
      view.BarWidth = 0.2;
      view.FillStyle.FillMode = FillMode.Gradient;
    }
    new RectangleGradientFillOptions()
    {
      GradientMode = RectangleGradientMode.LeftToRight
    }.Color2 = Color.Gray;
    dpoaeSeries.ArgumentScaleType = ScaleType.Numerical;
    dpoaeSeries.PointOptions.ValueNumericOptions.Precision = 0;
    dpoaeSeries.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
    return dpoaeSeries;
  }

  private static SecondaryAxisX CreateDpoaeResultAxisX()
  {
    SecondaryAxisX dpoaeResultAxisX = new SecondaryAxisX();
    dpoaeResultAxisX.Label.Font = new Font("Microsoft Sans Serif", 7f);
    dpoaeResultAxisX.Range.Auto = false;
    dpoaeResultAxisX.Range.MaxValueSerializable = "9.9";
    dpoaeResultAxisX.Range.MinValueSerializable = "-0.1";
    dpoaeResultAxisX.Range.ScrollingRange.SideMarginsEnabled = true;
    dpoaeResultAxisX.Range.SideMarginsEnabled = true;
    dpoaeResultAxisX.Label.Visible = false;
    dpoaeResultAxisX.Tickmarks.Visible = false;
    dpoaeResultAxisX.Tickmarks.MinorVisible = false;
    dpoaeResultAxisX.Visible = false;
    dpoaeResultAxisX.VisibleInPanesSerializable = "-1";
    return dpoaeResultAxisX;
  }

  private static void CreateAxisY(AxisY axisY)
  {
    axisY.Range.Auto = false;
    axisY.Range.MaxValueSerializable = "80";
    axisY.Range.MinValueSerializable = "0";
    axisY.Range.ScrollingRange.SideMarginsEnabled = false;
    axisY.Range.SideMarginsEnabled = false;
    axisY.Tickmarks.Visible = false;
    axisY.Tickmarks.MinorVisible = false;
    axisY.Title.Alignment = StringAlignment.Far;
    axisY.Title.Antialiasing = false;
    axisY.Title.Font = new Font("Microsoft Sans Serif", 7f);
    axisY.Title.Text = "dB";
    axisY.Title.Visible = true;
    axisY.VisibleInPanesSerializable = "-1";
  }

  private static void CreateAxisX(AxisX axisX)
  {
    axisX.Alignment = AxisAlignment.Zero;
    axisX.Label.Font = new Font("Microsoft Sans Serif", 7f);
    axisX.Range.Auto = false;
    axisX.Range.MaxValue = (object) "10";
    axisX.Range.MinValue = (object) "0";
    axisX.Range.ScrollingRange.SideMarginsEnabled = true;
    axisX.Range.SideMarginsEnabled = true;
    axisX.Tickmarks.Visible = false;
    axisX.Tickmarks.MinorVisible = false;
    axisX.Title.Alignment = StringAlignment.Far;
    axisX.Title.Antialiasing = false;
    axisX.Title.Font = new Font("Microsoft Sans Serif", 7f);
    axisX.Title.Text = "kHz";
    axisX.Title.Visible = true;
  }

  private ChartControl CreateDpoaeResultGraph(Size chartSize)
  {
    ChartControl dpoaeResultGraph = new ChartControl();
    dpoaeResultGraph.Series.Clear();
    dpoaeResultGraph.BorderOptions.Visible = false;
    dpoaeResultGraph.Legend.Visible = false;
    dpoaeResultGraph.Location = new Point(12, 42);
    dpoaeResultGraph.Margin = new System.Windows.Forms.Padding(0);
    dpoaeResultGraph.MinimumSize = new Size(320, 80 /*0x50*/);
    dpoaeResultGraph.Name = "ResultGraph";
    dpoaeResultGraph.SideBySideBarDistanceFixed = 0;
    dpoaeResultGraph.SideBySideEqualBarWidth = true;
    dpoaeResultGraph.Size = chartSize;
    return dpoaeResultGraph;
  }

  private static CustomAxisLabel[] CreateAxisYLabels()
  {
    CustomAxisLabel customAxisLabel1 = new CustomAxisLabel();
    customAxisLabel1.AxisValue = (object) "10";
    customAxisLabel1.Name = "-30";
    CustomAxisLabel customAxisLabel2 = customAxisLabel1;
    CustomAxisLabel customAxisLabel3 = new CustomAxisLabel();
    customAxisLabel3.AxisValue = (object) "20";
    customAxisLabel3.Name = "-20";
    CustomAxisLabel customAxisLabel4 = customAxisLabel3;
    CustomAxisLabel customAxisLabel5 = new CustomAxisLabel();
    customAxisLabel5.AxisValue = (object) "30";
    customAxisLabel5.Name = "-10";
    CustomAxisLabel customAxisLabel6 = customAxisLabel5;
    CustomAxisLabel customAxisLabel7 = new CustomAxisLabel();
    customAxisLabel7.AxisValue = (object) "40";
    customAxisLabel7.Name = "0";
    CustomAxisLabel customAxisLabel8 = customAxisLabel7;
    CustomAxisLabel customAxisLabel9 = new CustomAxisLabel();
    customAxisLabel9.AxisValue = (object) "50";
    customAxisLabel9.Name = "10";
    CustomAxisLabel customAxisLabel10 = customAxisLabel9;
    CustomAxisLabel customAxisLabel11 = new CustomAxisLabel();
    customAxisLabel11.AxisValue = (object) "60";
    customAxisLabel11.Name = "20";
    CustomAxisLabel customAxisLabel12 = customAxisLabel11;
    CustomAxisLabel customAxisLabel13 = new CustomAxisLabel();
    customAxisLabel13.AxisValue = (object) "70";
    customAxisLabel13.Name = "30";
    CustomAxisLabel customAxisLabel14 = customAxisLabel13;
    return new CustomAxisLabel[7]
    {
      customAxisLabel2,
      customAxisLabel4,
      customAxisLabel6,
      customAxisLabel8,
      customAxisLabel10,
      customAxisLabel12,
      customAxisLabel14
    };
  }

  private void CreateCalibrationChart()
  {
    Series series = new Series();
    series.Label.Visible = false;
    series.Name = "calibrationSeries";
    series.View.Color = Color.Black;
    this.calibration = series;
    CustomAxisLabel customAxisLabel1 = new CustomAxisLabel();
    customAxisLabel1.Name = "1";
    customAxisLabel1.AxisValue = (object) "1000";
    CustomAxisLabel customAxisLabel2 = customAxisLabel1;
    CustomAxisLabel customAxisLabel3 = new CustomAxisLabel();
    customAxisLabel3.Name = "2";
    customAxisLabel3.AxisValue = (object) "2000";
    CustomAxisLabel customAxisLabel4 = customAxisLabel3;
    CustomAxisLabel customAxisLabel5 = new CustomAxisLabel();
    customAxisLabel5.Name = "";
    customAxisLabel5.AxisValue = (object) "3000";
    CustomAxisLabel customAxisLabel6 = customAxisLabel5;
    CustomAxisLabel customAxisLabel7 = new CustomAxisLabel();
    customAxisLabel7.Name = "";
    customAxisLabel7.AxisValue = (object) "4000";
    CustomAxisLabel customAxisLabel8 = customAxisLabel7;
    CustomAxisLabel customAxisLabel9 = new CustomAxisLabel();
    customAxisLabel9.Name = "5";
    customAxisLabel9.AxisValue = (object) "5000";
    CustomAxisLabel customAxisLabel10 = customAxisLabel9;
    CustomAxisLabel customAxisLabel11 = new CustomAxisLabel();
    customAxisLabel11.Name = "";
    customAxisLabel11.AxisValue = (object) "6000";
    CustomAxisLabel customAxisLabel12 = customAxisLabel11;
    CustomAxisLabel customAxisLabel13 = new CustomAxisLabel();
    customAxisLabel13.Name = "";
    customAxisLabel13.AxisValue = (object) "7000";
    CustomAxisLabel customAxisLabel14 = customAxisLabel13;
    CustomAxisLabel customAxisLabel15 = new CustomAxisLabel();
    customAxisLabel15.Name = "";
    customAxisLabel15.AxisValue = (object) "8000";
    CustomAxisLabel customAxisLabel16 = customAxisLabel15;
    this.customAxisLabelCollection = new CustomAxisLabel[8]
    {
      customAxisLabel2,
      customAxisLabel4,
      customAxisLabel6,
      customAxisLabel8,
      customAxisLabel10,
      customAxisLabel12,
      customAxisLabel14,
      customAxisLabel16
    };
    this.calibrationGraph = new ChartControl();
    this.calibrationGraph.Series.Clear();
    this.calibrationGraph.Legend.Visible = false;
    this.calibrationGraph.Location = new Point(12, 42);
    this.calibrationGraph.Margin = new System.Windows.Forms.Padding(0);
    this.calibrationGraph.MinimumSize = new Size(320, 80 /*0x50*/);
    this.calibrationGraph.Name = "CalibrationGraph";
    this.calibrationGraph.PaletteRepository.Add("CalibrationGraph", new Palette("TestGraph", PaletteScaleMode.Repeat, new PaletteEntry[2]
    {
      new PaletteEntry(Color.Black, Color.Black),
      new PaletteEntry(Color.Blue, Color.Blue)
    }));
    this.calibrationGraph.PaletteName = "CalibrationGraph";
    this.calibration.ArgumentScaleType = ScaleType.Numerical;
    SplineSeriesView splineSeriesView = new SplineSeriesView();
    splineSeriesView.LineMarkerOptions.Visible = false;
    this.calibration.View = (SeriesViewBase) splineSeriesView;
    ((LineSeriesView) this.calibration.View).LineStyle.Thickness = 1;
    ((LineSeriesView) this.calibration.View).LineMarkerOptions.Visible = false;
    this.calibrationGraph.Size = this.chartSize;
    this.layoutChartControl.BeginInit();
    this.layoutDetailView.BeginInit();
    if (this.layoutDetailView.Controls["chartControl1"] != null)
      this.layoutDetailView.Controls.RemoveByKey("chartControl1");
    if (this.layoutDetailView.Controls["CalibrationGraph"] != null)
      this.layoutDetailView.Controls.RemoveByKey("CalibrationGraph");
    this.layoutDetailView.Controls.Add((Control) this.calibrationGraph);
    if (this.layoutDetailView.Controls["ResultGraph"] != null)
      this.layoutDetailView.Controls.RemoveByKey("ResultGraph");
    this.layoutChartControl.Control = (Control) this.calibrationGraph;
    this.layoutDetailView.EndInit();
    this.layoutChartControl.EndInit();
  }

  private void LoadEar(DpoaeTestInformation testDetail, PictureEdit earPicture)
  {
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
          goto label_16;
        case 16706:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("MeasurementAbort");
          goto label_16;
        case 16707:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationAbort");
          goto label_16;
        case 17221:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationError");
          goto label_16;
        case 17236:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationTimeout");
          goto label_16;
        case 17740:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ElectrodeLoose");
          goto label_16;
        case 19526:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CalibrationLeaky");
          goto label_16;
        case 20035:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Noisy");
          goto label_16;
        case 29555:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("IncompleteTest");
          this.ErrorDetail.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ProbeError");
          goto label_16;
        case 30583 /*0x7777*/:
          this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_NoClearResponse");
          this.ResultText.Text = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ReferredResultText");
          this.ErrorDetail.Text = (string) null;
          goto label_16;
      }
    }
    this.ResultImage.Image = (Image) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Test_Incomplete");
    this.ResultText.Text = (string) null;
label_16:
    int num1 = ((IEnumerable<int>) testDetail.SingleResultList).Any<int>((Func<int, bool>) (t => t != 0)) ? 1 : 0;
    byte num2 = 1;
    byte num3 = 0;
    byte num4 = 0;
    if (num1 != 0)
    {
      this.CreateChartControls();
      if (testDetail.Frequency2List != null)
      {
        for (int index = 0; index < testDetail.Frequency2List.Length; ++index)
        {
          if (testDetail.Frequency2List[index] != 0)
          {
            if (testDetail.Frequency1List[index] != 0 && num4 == (byte) 0)
              num4 = (byte) (testDetail.Frequency2List[index] / 1000);
            this.noiseSeries.Points.Add(new SeriesPoint((double) (testDetail.Frequency2List[index] / 1000), new double[1]
            {
              testDetail.NoiseList[index]
            }));
            if (testDetail.SingleResultList[index] == 13175)
              this.dpoaePassSeries.Points.Add(new SeriesPoint((double) (testDetail.Frequency2List[index] / 1000), new double[1]
              {
                testDetail.DPOAEList[index]
              }));
            else
              this.dpoaeFailSeries.Points.Add(new SeriesPoint((double) (testDetail.Frequency2List[index] / 1000), new double[1]
              {
                testDetail.DPOAEList[index]
              }));
            ++num2;
            num3 = (byte) (testDetail.Frequency2List[index] / 1000);
          }
        }
      }
      if ((int) num3 == (int) num2)
        ++num2;
      this.resultGraph.Series.AddRange(new Series[3]
      {
        this.dpoaePassSeries,
        this.dpoaeFailSeries,
        this.noiseSeries
      });
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Range.MinValue = (object) "0";
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Range.MaxValue = (object) "80";
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Title.Text = "kHz";
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Title.Alignment = StringAlignment.Far;
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Title.Antialiasing = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Title.Font = new Font("Microsoft Sans Serif", 7f);
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Title.Visible = true;
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Range.MinValue = (object) ((int) num4 - 1).ToString();
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Range.MaxValue = (object) num2.ToString();
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
      ((XYDiagram) this.resultGraph.Diagram).AxisX.Range.SideMarginsEnabled = true;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.CustomLabels.AddRange(TestViewer.CreateAxisYLabels());
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Title.Text = "dB";
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Title.Alignment = StringAlignment.Far;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Title.Antialiasing = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Title.Font = new Font("Microsoft Sans Serif", 7f);
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Title.Visible = true;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Tickmarks.Visible = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Tickmarks.MinorVisible = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Tickmarks.Visible = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Tickmarks.MinorVisible = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Range.ScrollingRange.SideMarginsEnabled = false;
      ((XYDiagram) this.resultGraph.Diagram).AxisY.Range.SideMarginsEnabled = false;
      AxisRange range1 = this.dpoaePassAxisX.Range;
      double num5 = (double) num2 - 0.1;
      string str1 = num5.ToString();
      range1.MaxValue = (object) str1;
      AxisRange range2 = this.dpoaePassAxisX.Range;
      num5 = (double) num4 - 1.1;
      string str2 = num5.ToString();
      range2.MinValue = (object) str2;
      AxisRange range3 = this.dpoaeFailAxisX.Range;
      num5 = (double) num2 - 0.1;
      string str3 = num5.ToString();
      range3.MaxValue = (object) str3;
      AxisRange range4 = this.dpoaeFailAxisX.Range;
      num5 = (double) num4 - 1.1;
      string str4 = num5.ToString();
      range4.MinValue = (object) str4;
      ((XYDiagram) this.resultGraph.Diagram).SecondaryAxesX.AddRange(new SecondaryAxisX[2]
      {
        this.dpoaePassAxisX,
        this.dpoaeFailAxisX
      });
      ((XYDiagramSeriesViewBase) this.dpoaePassSeries.View).AxisX = (AxisXBase) this.dpoaePassAxisX;
      ((XYDiagramSeriesViewBase) this.dpoaeFailSeries.View).AxisX = (AxisXBase) this.dpoaeFailAxisX;
      ((RectangleGradientFillOptions) ((BarSeriesView) this.resultGraph.Series[0].View).FillStyle.Options).GradientMode = RectangleGradientMode.LeftToRight;
      ((FillOptionsColor2Base) ((BarSeriesView) this.resultGraph.Series[0].View).FillStyle.Options).Color2 = Color.LightGreen;
      ((RectangleGradientFillOptions) ((BarSeriesView) this.resultGraph.Series[1].View).FillStyle.Options).GradientMode = RectangleGradientMode.LeftToRight;
      ((FillOptionsColor2Base) ((BarSeriesView) this.resultGraph.Series[1].View).FillStyle.Options).Color2 = Color.Gray;
      ((RectangleGradientFillOptions) ((BarSeriesView) this.resultGraph.Series[2].View).FillStyle.Options).GradientMode = RectangleGradientMode.LeftToRight;
      ((FillOptionsColor2Base) ((BarSeriesView) this.resultGraph.Series[2].View).FillStyle.Options).Color2 = Color.Gray;
      this.resultGraph.SideBySideBarDistanceFixed = 0;
      this.resultGraph.SideBySideEqualBarWidth = true;
    }
    else
    {
      this.CreateCalibrationChart();
      for (int index = 0; index < testDetail.CalibrationGraph.Length; ++index)
        this.calibration.Points.Add(new SeriesPoint((double) (index + 11) * 46.875, new double[1]
        {
          (double) testDetail.CalibrationGraph[index]
        }));
      this.calibrationGraph.Series.Add(this.calibration);
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.CustomLabels.AddRange(this.customAxisLabelCollection);
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.GridLines.Visible = true;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Logarithmic = true;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.LogarithmicBase = 10.0;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Range.Auto = false;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Range.MaxValue = (object) "10000";
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Range.MinValue = (object) "500";
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Title.Text = "kHz";
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Title.Alignment = StringAlignment.Far;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Title.Antialiasing = false;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Title.Font = new Font("Microsoft Sans Serif", 7f);
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Title.Visible = true;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Range.SideMarginsEnabled = false;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisX.Tickmarks.Visible = false;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisY.Visible = false;
      ((XYDiagram) this.calibrationGraph.Diagram).AxisY.Range.SideMarginsEnabled = true;
      ((XYDiagram2D) this.calibrationGraph.Diagram).DefaultPane.BorderVisible = false;
      this.calibrationGraph.BorderOptions.Visible = false;
    }
    long? nullable2 = testDetail.InstrumentSerialNumber;
    if (nullable2.HasValue)
    {
      LabelControl deviceSerialNo = this.DeviceSerialNo;
      nullable2 = testDetail.InstrumentSerialNumber;
      string str = nullable2.ToString();
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
    nullable2 = testDetail.ProbeSerialNumber;
    if (nullable2.HasValue)
    {
      LabelControl probeSerialNo = this.ProbeSerialNo;
      nullable2 = testDetail.ProbeSerialNumber;
      string str = nullable2.ToString();
      probeSerialNo.Text = str;
    }
    DateTime? nullable3 = testDetail.ProbeCalibrationDate;
    if (nullable3.HasValue)
    {
      LabelControl calibrationDateValue = this.probeCalibrationDateValue;
      nullable3 = testDetail.ProbeCalibrationDate;
      string str = nullable3.ToString();
      calibrationDateValue.Text = str;
    }
    Guid? nullable4 = testDetail.FacilityId;
    if (nullable4.HasValue)
      this.testFacilityIDValue.Text = FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
      {
        Guid id = f.Id;
        Guid? facilityId = testDetail.FacilityId;
        return facilityId.HasValue && id == facilityId.GetValueOrDefault();
      })).Select<Facility, string>((Func<Facility, string>) (f => f.Code)).FirstOrDefault<string>();
    else
      this.testFacilityIDValue.Text = Resources.TestViewer_NoValueFound;
    nullable4 = testDetail.LocationId;
    if (nullable4.HasValue)
      this.locationTypeValue.Text = LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l =>
      {
        Guid id = l.Id;
        Guid? locationId = testDetail.LocationId;
        return locationId.HasValue && id == locationId.GetValueOrDefault();
      })).Select<LocationType, string>((Func<LocationType, string>) (l => l.Name)).FirstOrDefault<string>();
    else
      this.locationTypeValue.Text = Resources.TestViewer_NoValueFound;
    LabelControl timeStamp = this.TimeStamp;
    nullable3 = testDetail.TestTimeStamp;
    string str5 = nullable3.ToString();
    timeStamp.Text = str5;
  }

  private void ApplySideBySideSeries(Series series, float correctValueMin, float correctValueMax)
  {
    if (!(series.View is SideBySideBarSeriesView view))
      return;
    view.BarWidth = 0.5;
    view.FillStyle.FillMode = FillMode.Solid;
    view.AxisX.Range.MaxValue = (object) Convert.ToString(correctValueMax);
    view.AxisX.Range.MinValue = (object) Convert.ToString(correctValueMin);
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
      DpoaeTestManager.Instance.AddComment((object) freeTextComment as FreeTextComment);
    }
    if (obj is PredefinedComment)
      DpoaeTestManager.Instance.AddPredefinedComment(((PredefinedComment) obj).Clone() as PredefinedComment);
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
    SideBySideBarSeriesLabel sideBarSeriesLabel = new SideBySideBarSeriesLabel();
    this.layoutDetailView = new LayoutControl();
    this.chartControl1 = new ChartControl();
    this.TimeStamp = new LabelControl();
    this.ErrorDetail = new LabelControl();
    this.ResultText = new LabelControl();
    this.ResultImage = new PictureEdit();
    this.RightEarIcon = new PictureEdit();
    this.TestResultHeader = new LabelControl();
    this.LeftEarIcon = new PictureEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem7 = new LayoutControlItem();
    this.layoutControlItem8 = new LayoutControlItem();
    this.layoutControlItem9 = new LayoutControlItem();
    this.layoutChartControl = new LayoutControlItem();
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
    this.DeviceSerialNo = new LabelControl();
    this.FirmwareNo = new LabelControl();
    this.testFacilityIDValue = new LabelControl();
    this.locationTypeValue = new LabelControl();
    this.probeCalibrationDateValue = new LabelControl();
    this.ProbeSerialNo = new LabelControl();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutDeviceSerial = new LayoutControlItem();
    this.layoutFirmwareBuild = new LayoutControlItem();
    this.layoutTestFacility = new LayoutControlItem();
    this.layoutLocation = new LayoutControlItem();
    this.layoutProbeCalibrationDate = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutProbeSerial = new LayoutControlItem();
    this.layoutControl2 = new LayoutControl();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutControlItem10 = new LayoutControlItem();
    this.layoutDetailView.BeginInit();
    this.layoutDetailView.SuspendLayout();
    this.chartControl1.BeginInit();
    ((ISupportInitialize) sideBarSeriesLabel).BeginInit();
    this.ResultImage.Properties.BeginInit();
    this.RightEarIcon.Properties.BeginInit();
    this.LeftEarIcon.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem7.BeginInit();
    this.layoutControlItem8.BeginInit();
    this.layoutControlItem9.BeginInit();
    this.layoutChartControl.BeginInit();
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
    this.layoutControlGroup2.BeginInit();
    this.layoutDeviceSerial.BeginInit();
    this.layoutFirmwareBuild.BeginInit();
    this.layoutTestFacility.BeginInit();
    this.layoutLocation.BeginInit();
    this.layoutProbeCalibrationDate.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutProbeSerial.BeginInit();
    this.layoutControl2.BeginInit();
    this.layoutControl2.SuspendLayout();
    this.layoutControlGroup3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutControlItem10.BeginInit();
    this.SuspendLayout();
    this.layoutDetailView.Controls.Add((Control) this.chartControl1);
    this.layoutDetailView.Controls.Add((Control) this.TimeStamp);
    this.layoutDetailView.Controls.Add((Control) this.ErrorDetail);
    this.layoutDetailView.Controls.Add((Control) this.ResultText);
    this.layoutDetailView.Controls.Add((Control) this.ResultImage);
    this.layoutDetailView.Controls.Add((Control) this.RightEarIcon);
    this.layoutDetailView.Controls.Add((Control) this.TestResultHeader);
    this.layoutDetailView.Controls.Add((Control) this.LeftEarIcon);
    componentResourceManager.ApplyResources((object) this.layoutDetailView, "layoutDetailView");
    this.layoutDetailView.Name = "layoutDetailView";
    this.layoutDetailView.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.chartControl1, "chartControl1");
    this.chartControl1.Name = "chartControl1";
    this.chartControl1.SeriesSerializable = new Series[0];
    sideBarSeriesLabel.LineVisible = true;
    this.chartControl1.SeriesTemplate.Label = (SeriesLabelBase) sideBarSeriesLabel;
    componentResourceManager.ApplyResources((object) this.TimeStamp, "TimeStamp");
    this.TimeStamp.Name = "TimeStamp";
    this.TimeStamp.StyleController = (IStyleController) this.layoutDetailView;
    componentResourceManager.ApplyResources((object) this.ErrorDetail, "ErrorDetail");
    this.ErrorDetail.Name = "ErrorDetail";
    this.ErrorDetail.StyleController = (IStyleController) this.layoutDetailView;
    this.ResultText.Appearance.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.ResultText.Appearance.Options.UseFont = true;
    componentResourceManager.ApplyResources((object) this.ResultText, "ResultText");
    this.ResultText.Name = "ResultText";
    this.ResultText.StyleController = (IStyleController) this.layoutDetailView;
    componentResourceManager.ApplyResources((object) this.ResultImage, "ResultImage");
    this.ResultImage.Name = "ResultImage";
    this.ResultImage.Properties.Appearance.BackColor = Color.Transparent;
    this.ResultImage.Properties.Appearance.Options.UseBackColor = true;
    this.ResultImage.Properties.BorderStyle = BorderStyles.NoBorder;
    this.ResultImage.StyleController = (IStyleController) this.layoutDetailView;
    componentResourceManager.ApplyResources((object) this.RightEarIcon, "RightEarIcon");
    this.RightEarIcon.MinimumSize = new Size(28, 26);
    this.RightEarIcon.Name = "RightEarIcon";
    this.RightEarIcon.Properties.Appearance.BackColor = Color.Transparent;
    this.RightEarIcon.Properties.Appearance.Options.UseBackColor = true;
    this.RightEarIcon.Properties.BorderStyle = BorderStyles.NoBorder;
    this.RightEarIcon.StyleController = (IStyleController) this.layoutDetailView;
    this.TestResultHeader.Appearance.Font = new Font("Microsoft Sans Serif", 12f);
    this.TestResultHeader.Appearance.Options.UseFont = true;
    this.TestResultHeader.Appearance.Options.UseTextOptions = true;
    this.TestResultHeader.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
    this.TestResultHeader.Appearance.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.TestResultHeader, "TestResultHeader");
    this.TestResultHeader.MinimumSize = new Size(267, 26);
    this.TestResultHeader.Name = "TestResultHeader";
    this.TestResultHeader.StyleController = (IStyleController) this.layoutDetailView;
    componentResourceManager.ApplyResources((object) this.LeftEarIcon, "LeftEarIcon");
    this.LeftEarIcon.MinimumSize = new Size(28, 26);
    this.LeftEarIcon.Name = "LeftEarIcon";
    this.LeftEarIcon.Properties.Appearance.BackColor = Color.Transparent;
    this.LeftEarIcon.Properties.Appearance.Options.UseBackColor = true;
    this.LeftEarIcon.Properties.BorderStyle = BorderStyles.NoBorder;
    this.LeftEarIcon.StyleController = (IStyleController) this.layoutDetailView;
    this.layoutControlGroup1.AppearanceGroup.BackColor = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.BackColor2 = Color.Transparent;
    this.layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[8]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem6,
      (BaseLayoutItem) this.layoutControlItem7,
      (BaseLayoutItem) this.layoutControlItem8,
      (BaseLayoutItem) this.layoutControlItem9,
      (BaseLayoutItem) this.layoutChartControl
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
    this.layoutControlItem3.Control = (Control) this.RightEarIcon;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(296, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(32 /*0x20*/, 30);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutControlItem6.Control = (Control) this.ResultImage;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(0, 154);
    this.layoutControlItem6.MinSize = new Size(24, 24);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(59, 114);
    this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.layoutControlItem7.Control = (Control) this.ResultText;
    componentResourceManager.ApplyResources((object) this.layoutControlItem7, "layoutControlItem7");
    this.layoutControlItem7.Location = new Point(59, 154);
    this.layoutControlItem7.MinSize = new Size(67, 17);
    this.layoutControlItem7.Name = "layoutControlItem7";
    this.layoutControlItem7.Size = new Size(269, 27);
    this.layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem7.TextSize = new Size(0, 0);
    this.layoutControlItem7.TextToControlDistance = 0;
    this.layoutControlItem7.TextVisible = false;
    this.layoutControlItem8.Control = (Control) this.ErrorDetail;
    componentResourceManager.ApplyResources((object) this.layoutControlItem8, "layoutControlItem8");
    this.layoutControlItem8.Location = new Point(59, 181);
    this.layoutControlItem8.MinSize = new Size(67, 17);
    this.layoutControlItem8.Name = "layoutControlItem8";
    this.layoutControlItem8.Size = new Size(269, 17);
    this.layoutControlItem8.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem8.TextSize = new Size(0, 0);
    this.layoutControlItem8.TextToControlDistance = 0;
    this.layoutControlItem8.TextVisible = false;
    this.layoutControlItem9.Control = (Control) this.TimeStamp;
    componentResourceManager.ApplyResources((object) this.layoutControlItem9, "layoutControlItem9");
    this.layoutControlItem9.Location = new Point(59, 198);
    this.layoutControlItem9.MinSize = new Size(67, 17);
    this.layoutControlItem9.Name = "layoutControlItem9";
    this.layoutControlItem9.Size = new Size(269, 70);
    this.layoutControlItem9.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem9.TextSize = new Size(0, 0);
    this.layoutControlItem9.TextToControlDistance = 0;
    this.layoutControlItem9.TextVisible = false;
    this.layoutChartControl.Control = (Control) this.chartControl1;
    componentResourceManager.ApplyResources((object) this.layoutChartControl, "layoutChartControl");
    this.layoutChartControl.Location = new Point(0, 30);
    this.layoutChartControl.Name = "layoutControlItem4";
    this.layoutChartControl.Size = new Size(328, 124);
    this.layoutChartControl.TextSize = new Size(0, 0);
    this.layoutChartControl.TextToControlDistance = 0;
    this.layoutChartControl.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.xtraTabControl1, "xtraTabControl1");
    this.xtraTabControl1.Name = "xtraTabControl1";
    this.xtraTabControl1.SelectedTabPage = this.xtraTabTestResult;
    this.xtraTabControl1.TabPages.AddRange(new XtraTabPage[3]
    {
      this.xtraTabTestResult,
      this.xtraTabTestComments,
      this.xtraTabDeviceInformation
    });
    this.xtraTabTestResult.Controls.Add((Control) this.layoutDetailView);
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
    this.layoutControl1.Controls.Add((Control) this.DeviceSerialNo);
    this.layoutControl1.Controls.Add((Control) this.FirmwareNo);
    this.layoutControl1.Controls.Add((Control) this.testFacilityIDValue);
    this.layoutControl1.Controls.Add((Control) this.locationTypeValue);
    this.layoutControl1.Controls.Add((Control) this.probeCalibrationDateValue);
    this.layoutControl1.Controls.Add((Control) this.ProbeSerialNo);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.DeviceSerialNo, "DeviceSerialNo");
    this.DeviceSerialNo.Name = "DeviceSerialNo";
    this.DeviceSerialNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.FirmwareNo, "FirmwareNo");
    this.FirmwareNo.Name = "FirmwareNo";
    this.FirmwareNo.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.testFacilityIDValue, "testFacilityIDValue");
    this.testFacilityIDValue.Name = "testFacilityIDValue";
    this.testFacilityIDValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.locationTypeValue, "locationTypeValue");
    this.locationTypeValue.Name = "locationTypeValue";
    this.locationTypeValue.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.probeCalibrationDateValue, "probeCalibrationDateValue");
    this.probeCalibrationDateValue.Name = "probeCalibrationDateValue";
    this.probeCalibrationDateValue.StyleController = (IStyleController) this.layoutControl1;
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
      (BaseLayoutItem) this.layoutDeviceSerial,
      (BaseLayoutItem) this.layoutFirmwareBuild,
      (BaseLayoutItem) this.layoutTestFacility,
      (BaseLayoutItem) this.layoutLocation,
      (BaseLayoutItem) this.layoutProbeCalibrationDate,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutProbeSerial
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(348, 288);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
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
    this.layoutLocation.Control = (Control) this.locationTypeValue;
    componentResourceManager.ApplyResources((object) this.layoutLocation, "layoutLocation");
    this.layoutLocation.Location = new Point(0, 81);
    this.layoutLocation.Name = "layoutLocation";
    this.layoutLocation.Size = new Size(328, 17);
    this.layoutLocation.TextSize = new Size(112 /*0x70*/, 13);
    this.layoutProbeCalibrationDate.Control = (Control) this.probeCalibrationDateValue;
    componentResourceManager.ApplyResources((object) this.layoutProbeCalibrationDate, "layoutProbeCalibrationDate");
    this.layoutProbeCalibrationDate.Location = new Point(0, 115);
    this.layoutProbeCalibrationDate.Name = "layoutProbeCalibrationDate";
    this.layoutProbeCalibrationDate.Size = new Size(328, 17);
    this.layoutProbeCalibrationDate.TextSize = new Size(112 /*0x70*/, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 0);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(328, 30);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 132);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(328, 136);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
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
    this.layoutControl2.Root = this.layoutControlGroup3;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup3.GroupBordersVisible = false;
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.layoutControlItem10
    });
    this.layoutControlGroup3.Location = new Point(0, 0);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(348, 288);
    this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup3.TextVisible = false;
    this.layoutControlItem4.Control = (Control) this.gridControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 0);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(328, 242);
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    this.layoutControlItem5.Control = (Control) this.commentAssignment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(0, 242);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(226, 26);
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    this.layoutControlItem10.Control = (Control) this.addComment;
    componentResourceManager.ApplyResources((object) this.layoutControlItem10, "layoutControlItem10");
    this.layoutControlItem10.Location = new Point(226, 242);
    this.layoutControlItem10.Name = "layoutControlItem10";
    this.layoutControlItem10.Size = new Size(102, 26);
    this.layoutControlItem10.TextSize = new Size(0, 0);
    this.layoutControlItem10.TextToControlDistance = 0;
    this.layoutControlItem10.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.xtraTabControl1);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(355, 317);
    this.Name = nameof (TestViewer);
    this.layoutDetailView.EndInit();
    this.layoutDetailView.ResumeLayout(false);
    ((ISupportInitialize) sideBarSeriesLabel).EndInit();
    this.chartControl1.EndInit();
    this.ResultImage.Properties.EndInit();
    this.RightEarIcon.Properties.EndInit();
    this.LeftEarIcon.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem7.EndInit();
    this.layoutControlItem8.EndInit();
    this.layoutControlItem9.EndInit();
    this.layoutChartControl.EndInit();
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
    this.layoutControlGroup2.EndInit();
    this.layoutDeviceSerial.EndInit();
    this.layoutFirmwareBuild.EndInit();
    this.layoutTestFacility.EndInit();
    this.layoutLocation.EndInit();
    this.layoutProbeCalibrationDate.EndInit();
    this.emptySpaceItem1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutProbeSerial.EndInit();
    this.layoutControl2.EndInit();
    this.layoutControl2.ResumeLayout(false);
    this.layoutControlGroup3.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutControlItem10.EndInit();
    this.ResumeLayout(false);
  }
}
