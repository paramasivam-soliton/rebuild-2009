// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.PatientTestBrowser
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using PathMedical.AudiologyTest;
using PathMedical.Automaton;
using PathMedical.DataExchange;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.DetailView;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser;

[ToolboxItem(false)]
public sealed class PatientTestBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressMutliSelectionGridViewHelper<Patient> patientMultiSelectionGridHelper;
  private readonly DevExpressSingleSelectionGridViewHelper<AudiologyTestInformation> testSingleSelectionGridHelper;
  private BarEditItem dobSelector;
  private RepositoryItemComboBox repositoryItemDobButtonEdit;
  private BarEditItem findPatientEditor;
  private GalleryDropDown reportSelectionGalleryDropDown;
  private GalleryItem printAllBestBasicReport;
  private GalleryItem printBestTeoaeBasicReport;
  private GalleryItem printBestDpoaeBasicReport;
  private GalleryItem printBestAbrBasicReport;
  private GalleryItem printAllBestDetailReport;
  private GalleryItem printBestTeoaeDetailReport;
  private GalleryItem printBestDpoaeDetailReport;
  private GalleryItem printBestAbrDetailReport;
  private GalleryDropDown exportDataDropDown;
  private GalleryDropDown importDataDropDown;
  private GalleryDropDown importConfigurationDropDown;
  private GalleryDropDown exportConfigurationDropDown;
  private IContainer components;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutDetailViewer;
  private GridControl testGrid;
  private GridView testGridView;
  private LayoutControlItem layoutTestGrid;
  private GridControl patientGrid;
  private LayoutControlItem patientGridLayout;
  private RepositoryItemImageComboBox overallTestResultImage;
  private DevExpressTextEdit devExpressTextEdit1;
  private LayoutControlGroup advancedSearchGroupLayout;
  private LayoutControlItem layoutControlItem2;
  private GridColumn columnRightEarResult;
  private GridColumn columnLeftEarResult;
  private GridColumn columnTestTime;
  private GridColumn columnDuration;
  private PanelControl detailViewer;
  private LayoutControlItem layoutControlItem1;
  private SplitterItem splitterItem1;
  private SplitterItem splitterItem2;
  private GridColumn columnTestType;
  private RepositoryItemImageComboBox testResultImage;
  private GridColumn columnExaminer;
  private RepositoryItemImageComboBox riskIndicatorImage;
  private BandedGridView patientGridView;
  private BandedGridColumn columnPatientMedicalRecordNumber;
  private BandedGridColumn columnPatientFullName;
  private BandedGridColumn columnPatientDateOfBirth;
  private BandedGridColumn columnPatientHasRiskIndicator;
  private BandedGridColumn bestDpoaeLeft;
  private BandedGridColumn bestDpoaeRight;
  private BandedGridColumn bestTeoaeLeft;
  private BandedGridColumn bestTeoaeRight;
  private BandedGridColumn bestAbrLeft;
  private BandedGridColumn bestAbrRight;
  private GridColumn columnTestName;
  private BandedGridColumn columnPatientHasComments;
  private RepositoryItemImageComboBox commentImage;
  private LayoutControlGroup layoutGroupTestResults;
  private GridBand patientBand;
  private GridBand abrBand;
  private GridBand teoaeBand;
  private GridBand dpoaeBand;

  public PatientTestBrowser()
  {
    this.InitializeComponent();
    DevExpress.Utils.ImageCollection imageCollection1 = new DevExpress.Utils.ImageCollection();
    imageCollection1.AddImage((Image) (ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("GN_TestPass") as Bitmap));
    imageCollection1.AddImage((Image) (ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("GN_TestRefer") as Bitmap));
    imageCollection1.AddImage((Image) (ComponentResourceManagementBase<PathMedical.AudiologyTest.Properties.Resources>.Instance.ResourceManager.GetObject("GN_TestIncomplete") as Bitmap));
    this.overallTestResultImage.Items.Clear();
    ImageComboBoxItemCollection items1 = this.overallTestResultImage.Items;
    ImageComboBoxItem imageComboBoxItem1 = new ImageComboBoxItem();
    imageComboBoxItem1.ImageIndex = 0;
    imageComboBoxItem1.Value = (object) AudiologyTestResult.Pass;
    items1.Add(imageComboBoxItem1);
    ImageComboBoxItemCollection items2 = this.overallTestResultImage.Items;
    ImageComboBoxItem imageComboBoxItem2 = new ImageComboBoxItem();
    imageComboBoxItem2.ImageIndex = 1;
    imageComboBoxItem2.Value = (object) AudiologyTestResult.Refer;
    items2.Add(imageComboBoxItem2);
    ImageComboBoxItemCollection items3 = this.overallTestResultImage.Items;
    ImageComboBoxItem imageComboBoxItem3 = new ImageComboBoxItem();
    imageComboBoxItem3.ImageIndex = 2;
    imageComboBoxItem3.Value = (object) AudiologyTestResult.Incomplete;
    items3.Add(imageComboBoxItem3);
    this.overallTestResultImage.SmallImages = (object) imageCollection1;
    this.testResultImage.Items.Clear();
    ImageComboBoxItemCollection items4 = this.testResultImage.Items;
    ImageComboBoxItem imageComboBoxItem4 = new ImageComboBoxItem();
    imageComboBoxItem4.ImageIndex = 0;
    imageComboBoxItem4.Value = (object) AudiologyTestResult.Pass;
    items4.Add(imageComboBoxItem4);
    ImageComboBoxItemCollection items5 = this.testResultImage.Items;
    ImageComboBoxItem imageComboBoxItem5 = new ImageComboBoxItem();
    imageComboBoxItem5.ImageIndex = 1;
    imageComboBoxItem5.Value = (object) AudiologyTestResult.Refer;
    items5.Add(imageComboBoxItem5);
    ImageComboBoxItemCollection items6 = this.testResultImage.Items;
    ImageComboBoxItem imageComboBoxItem6 = new ImageComboBoxItem();
    imageComboBoxItem6.ImageIndex = 2;
    imageComboBoxItem6.Value = (object) AudiologyTestResult.Incomplete;
    items6.Add(imageComboBoxItem6);
    this.testResultImage.SmallImages = (object) imageCollection1;
    DevExpress.Utils.ImageCollection imageCollection2 = new DevExpress.Utils.ImageCollection();
    imageCollection2.AddImage((Image) (ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnknown") as Bitmap));
    imageCollection2.AddImage((Image) (ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorAssigned") as Bitmap));
    imageCollection2.AddImage((Image) (ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnassigned") as Bitmap));
    this.riskIndicatorImage.Items.Clear();
    ImageComboBoxItemCollection items7 = this.riskIndicatorImage.Items;
    ImageComboBoxItem imageComboBoxItem7 = new ImageComboBoxItem();
    imageComboBoxItem7.ImageIndex = 0;
    imageComboBoxItem7.Value = (object) RiskIndicatorValueType.Unknown;
    items7.Add(imageComboBoxItem7);
    ImageComboBoxItemCollection items8 = this.riskIndicatorImage.Items;
    ImageComboBoxItem imageComboBoxItem8 = new ImageComboBoxItem();
    imageComboBoxItem8.ImageIndex = 1;
    imageComboBoxItem8.Value = (object) RiskIndicatorValueType.Yes;
    items8.Add(imageComboBoxItem8);
    ImageComboBoxItemCollection items9 = this.riskIndicatorImage.Items;
    ImageComboBoxItem imageComboBoxItem9 = new ImageComboBoxItem();
    imageComboBoxItem9.ImageIndex = 2;
    imageComboBoxItem9.Value = (object) RiskIndicatorValueType.No;
    items9.Add(imageComboBoxItem9);
    this.riskIndicatorImage.SmallImages = (object) imageCollection2;
    DevExpress.Utils.ImageCollection imageCollection3 = new DevExpress.Utils.ImageCollection();
    imageCollection3.AddImage((Image) (ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_Commented") as Bitmap));
    this.commentImage.Items.Clear();
    ImageComboBoxItemCollection items10 = this.commentImage.Items;
    ImageComboBoxItem imageComboBoxItem10 = new ImageComboBoxItem();
    imageComboBoxItem10.ImageIndex = 0;
    imageComboBoxItem10.Value = (object) true;
    items10.Add(imageComboBoxItem10);
    this.commentImage.SmallImages = (object) imageCollection3;
    this.CreateRibbonBarCommands();
    this.Dock = DockStyle.Fill;
    this.advancedSearchGroupLayout.Visibility = LayoutVisibility.Never;
    this.SetParameters(true, false, 60);
    this.HelpMarker = "patients_tests_01.html";
  }

  public PatientTestBrowser(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.patientMultiSelectionGridHelper = new DevExpressMutliSelectionGridViewHelper<Patient>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, (GridView) this.patientGridView, model, Triggers.ChangeSelection);
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    this.testSingleSelectionGridHelper = new DevExpressSingleSelectionGridViewHelper<AudiologyTestInformation>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.testGridView, model, PatientManagementTriggers.SelectTest, "AudiologyTests");
    TestDetailViewer testDetailViewer = new TestDetailViewer(model);
    this.detailViewer.Controls.Clear();
    this.detailViewer.Controls.Add((Control) testDetailViewer);
    testDetailViewer.Dock = DockStyle.Fill;
    this.VisibleChanged += new EventHandler(this.PatientTestBrowser_VisibleChanged);
    if (!(model is PatientManager))
      return;
    this.DobSelectorInitialize((model as PatientManager).CreationTimeStampFilterType);
  }

  private void PatientTestBrowser_VisibleChanged(object sender, EventArgs e)
  {
    if (this.reportSelectionGalleryDropDown != null && this.reportSelectionGalleryDropDown.Ribbon == null)
      this.reportSelectionGalleryDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
    if (this.exportDataDropDown != null && this.exportDataDropDown.Ribbon == null)
      this.exportDataDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
    if (this.importDataDropDown != null && this.importDataDropDown.Ribbon == null)
      this.importDataDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
    if (this.importConfigurationDropDown != null && this.importConfigurationDropDown.Ribbon == null)
      this.importConfigurationDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
    if (this.exportConfigurationDropDown == null || this.exportConfigurationDropDown.Ribbon != null)
      return;
    this.exportConfigurationDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
  }

  public override void CleanUpView()
  {
    base.CleanUpView();
    if (this.findPatientEditor != null)
      this.findPatientEditor.EditValue = (object) null;
    if (UserInterfaceManager.Instance.Ribbon.Manager.ActiveEditItemLink == null)
      return;
    UserInterfaceManager.Instance.Ribbon.Manager.ActiveEditItemLink.CloseEditor();
  }

  private void CreateRibbonBarCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    RibbonPageGroup ribbonPageGroup1 = ribbonHelper.CreateRibbonPageGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPatientManagementGroup);
    ribbonPageGroup1.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonAddPatient, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("PatientAdd") as Bitmap, Triggers.Add));
    ribbonPageGroup1.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPatientDetails, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("PatientDetails") as Bitmap, Triggers.Edit));
    ribbonPageGroup1.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDeletePatient, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("PatientDelete") as Bitmap, Triggers.Delete));
    this.printAllBestBasicReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestTest_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestTest_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintAllBasic") as Bitmap, PatientManagementTriggers.PrintBasicOverallReport);
    this.printBestTeoaeBasicReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestTeoae_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestTeoae_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintTeoaeBasic") as Bitmap, PatientManagementTriggers.PrintBasicReportTeoae);
    this.printBestDpoaeBasicReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestDpoae_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestDpoae_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintDpoaeBasic") as Bitmap, PatientManagementTriggers.PrintBasicReportDpoae);
    this.printBestAbrBasicReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestAbr_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintBasicReport_BestAbrDescription, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintAbrBasic") as Bitmap, PatientManagementTriggers.PrintBasicReportAbr);
    GalleryItemGroup galleryGroup1 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonBasicReports);
    galleryGroup1.Items.AddRange(new GalleryItem[4]
    {
      this.printAllBestBasicReport,
      this.printBestTeoaeBasicReport,
      this.printBestDpoaeBasicReport,
      this.printBestAbrBasicReport
    });
    this.printAllBestDetailReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestTest_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestTest_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintAllDetails") as Bitmap, PatientManagementTriggers.PrintDetailOverallReport);
    this.printBestTeoaeDetailReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestTeoae_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestTeoae_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintTeoaeDetails") as Bitmap, PatientManagementTriggers.PrintDetailReportTeoae);
    this.printBestDpoaeDetailReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestDpoae_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestDpoae_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintDpoaeDetails") as Bitmap, PatientManagementTriggers.PrintDetailReportDpoae);
    this.printBestAbrDetailReport = ribbonHelper.CreateGalleryItem(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestAbr_Caption, PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintDetailReport_BestAbr_Description, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintAbrDetails") as Bitmap, PatientManagementTriggers.PrintDetailReportAbr);
    GalleryItemGroup galleryGroup2 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDetailReports);
    galleryGroup2.Items.AddRange(new GalleryItem[4]
    {
      this.printAllBestDetailReport,
      this.printBestTeoaeDetailReport,
      this.printBestDpoaeDetailReport,
      this.printBestAbrDetailReport
    });
    this.reportSelectionGalleryDropDown = ribbonHelper.CreateGalleryDropDown();
    this.reportSelectionGalleryDropDown.Gallery.RowCount = 8;
    this.reportSelectionGalleryDropDown.Gallery.ColumnCount = 1;
    this.reportSelectionGalleryDropDown.Gallery.Groups.AddRange(new GalleryItemGroup[2]
    {
      galleryGroup1,
      galleryGroup2
    });
    this.reportSelectionGalleryDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
    BarButtonItem largeDropDownButton1 = ribbonHelper.CreateLargeDropDownButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonPrintButton, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_PrintAllDetails") as Bitmap, this.reportSelectionGalleryDropDown);
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeDropDownButton1);
    this.ToolbarElements.Add((object) ribbonPageGroup1);
    RibbonPageGroup ribbonPageGroup2 = ribbonHelper.CreateRibbonPageGroup(ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetString("PatientTestBrowser_SearchGroupDescription"));
    this.ToolbarElements.Add((object) ribbonPageGroup2);
    this.dobSelector = new BarEditItem();
    this.repositoryItemDobButtonEdit = new RepositoryItemComboBox();
    this.repositoryItemDobButtonEdit.Buttons.Clear();
    this.repositoryItemDobButtonEdit.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemDobButtonEdit.SelectedValueChanged += new EventHandler(this.DoBFilterChanged);
    this.repositoryItemDobButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.dobSelector.Edit = (RepositoryItem) this.repositoryItemDobButtonEdit;
    this.dobSelector.Caption = string.Empty;
    this.dobSelector.Width = 150;
    this.dobSelector.Border = BorderStyles.NoBorder;
    ribbonPageGroup2.ItemLinks.Add((BarItem) this.dobSelector);
    this.findPatientEditor = new BarEditItem();
    RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
    repositoryItemButtonEdit.Buttons.Clear();
    repositoryItemButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Delete, string.Empty, -1, true, true, false, HorzAlignment.Default, (Image) null));
    repositoryItemButtonEdit.ButtonClick += new ButtonPressedEventHandler(this.PatientSearchTextButtonClick);
    repositoryItemButtonEdit.NullText = ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetString("SearchFieldNullText");
    repositoryItemButtonEdit.EndInit();
    this.findPatientEditor.Edit = (RepositoryItem) repositoryItemButtonEdit;
    this.findPatientEditor.Caption = string.Empty;
    this.findPatientEditor.Width = 150;
    this.findPatientEditor.EditValueChanged += new EventHandler(this.FilterPatientData);
    ribbonPageGroup2.ItemLinks.Add((BarItem) this.findPatientEditor);
    RibbonPageGroup ribbonPageGroup3 = ribbonHelper.CreateRibbonPageGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange);
    ISupportDataExchangeModules[] array = SystemConfigurationManager.Instance.Plugins.OfType<ISupportDataExchangeModules>().Where<ISupportDataExchangeModules>((Func<ISupportDataExchangeModules, bool>) (p => p.ExportModule != null || p.ImportModule != null || p.ConfigurationExportModule != null || p.ConfigurationImportModule != null)).OrderBy<ISupportDataExchangeModules, string>((Func<ISupportDataExchangeModules, string>) (p => p.Name)).ToArray<ISupportDataExchangeModules>();
    List<IApplicationComponentModule> source1 = new List<IApplicationComponentModule>();
    List<IApplicationComponentModule> source2 = new List<IApplicationComponentModule>();
    List<IApplicationComponentModule> source3 = new List<IApplicationComponentModule>();
    List<IApplicationComponentModule> source4 = new List<IApplicationComponentModule>();
    foreach (ISupportDataExchangeModules dataExchangeModules in array)
    {
      if (dataExchangeModules != null)
      {
        if (dataExchangeModules.ExportModule != null)
          source1.Add(dataExchangeModules.ExportModule);
        if (dataExchangeModules.ImportModule != null)
          source2.Add(dataExchangeModules.ImportModule);
        if (dataExchangeModules.ConfigurationImportModule != null)
          source3.Add(dataExchangeModules.ConfigurationImportModule);
        if (dataExchangeModules.ConfigurationExportModule != null)
          source4.Add(dataExchangeModules.ConfigurationExportModule);
      }
    }
    if (source1.Count > 0)
    {
      GalleryItemGroup galleryGroup3 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Export);
      foreach (IApplicationComponentModule module in (IEnumerable<IApplicationComponentModule>) source1.OrderBy<IApplicationComponentModule, string>((Func<IApplicationComponentModule, string>) (m => m.Name)))
      {
        GalleryItem galleryItem = ribbonHelper.CreateGalleryItem(module);
        galleryGroup3.Items.AddRange(new GalleryItem[1]
        {
          galleryItem
        });
      }
      this.exportDataDropDown = ribbonHelper.CreateGalleryDropDown();
      this.exportDataDropDown.Gallery.RowCount = source1.Count;
      this.exportDataDropDown.Gallery.ColumnCount = 1;
      this.exportDataDropDown.Gallery.Groups.AddRange(new GalleryItemGroup[1]
      {
        galleryGroup3
      });
      this.exportDataDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
      BarButtonItem largeDropDownButton2 = ribbonHelper.CreateLargeDropDownButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Export_Button, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_DataExport") as Bitmap, this.exportDataDropDown);
      ribbonPageGroup3.ItemLinks.Add((BarItem) largeDropDownButton2);
    }
    if (source2.Count > 0)
    {
      GalleryItemGroup galleryGroup4 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Import);
      foreach (IApplicationComponentModule module in (IEnumerable<IApplicationComponentModule>) source2.OrderBy<IApplicationComponentModule, string>((Func<IApplicationComponentModule, string>) (m => m.Name)))
      {
        GalleryItem galleryItem = ribbonHelper.CreateGalleryItem(module);
        galleryGroup4.Items.AddRange(new GalleryItem[1]
        {
          galleryItem
        });
      }
      this.importDataDropDown = ribbonHelper.CreateGalleryDropDown();
      this.importDataDropDown.Gallery.RowCount = source1.Count;
      this.importDataDropDown.Gallery.ColumnCount = 1;
      this.importDataDropDown.Gallery.Groups.AddRange(new GalleryItemGroup[1]
      {
        galleryGroup4
      });
      this.importDataDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
      BarButtonItem largeDropDownButton3 = ribbonHelper.CreateLargeDropDownButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Importt_Button, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_DataImport") as Bitmap, this.exportDataDropDown);
      ribbonPageGroup3.ItemLinks.Add((BarItem) largeDropDownButton3);
    }
    if (source3.Count > 0)
    {
      GalleryItemGroup galleryGroup5 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Configuration_Import);
      foreach (IApplicationComponentModule module in (IEnumerable<IApplicationComponentModule>) source3.OrderBy<IApplicationComponentModule, string>((Func<IApplicationComponentModule, string>) (m => m.Name)))
      {
        GalleryItem galleryItem = ribbonHelper.CreateGalleryItem(module);
        galleryGroup5.Items.AddRange(new GalleryItem[1]
        {
          galleryItem
        });
      }
      this.importConfigurationDropDown = ribbonHelper.CreateGalleryDropDown();
      this.importConfigurationDropDown.Gallery.RowCount = source1.Count;
      this.importConfigurationDropDown.Gallery.ColumnCount = 1;
      this.importConfigurationDropDown.Gallery.Groups.AddRange(new GalleryItemGroup[1]
      {
        galleryGroup5
      });
      this.importConfigurationDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
      BarButtonItem largeDropDownButton4 = ribbonHelper.CreateLargeDropDownButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Configuration_Import_Button, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_DataImport") as Bitmap, this.importConfigurationDropDown);
      ribbonPageGroup3.ItemLinks.Add((BarItem) largeDropDownButton4);
    }
    if (source4.Count > 0)
    {
      GalleryItemGroup galleryGroup6 = ribbonHelper.CreateGalleryGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Configuration_Export);
      foreach (IApplicationComponentModule module in (IEnumerable<IApplicationComponentModule>) source4.OrderBy<IApplicationComponentModule, string>((Func<IApplicationComponentModule, string>) (m => m.Name)))
      {
        GalleryItem galleryItem = ribbonHelper.CreateGalleryItem(module);
        galleryGroup6.Items.AddRange(new GalleryItem[1]
        {
          galleryItem
        });
      }
      this.exportConfigurationDropDown = ribbonHelper.CreateGalleryDropDown();
      this.exportConfigurationDropDown.Gallery.RowCount = source1.Count;
      this.exportConfigurationDropDown.Gallery.ColumnCount = 1;
      this.exportConfigurationDropDown.Gallery.Groups.AddRange(new GalleryItemGroup[1]
      {
        galleryGroup6
      });
      this.exportConfigurationDropDown.Ribbon = UserInterfaceManager.Instance.Ribbon;
      BarButtonItem largeDropDownButton5 = ribbonHelper.CreateLargeDropDownButton(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonDataExchange_Configuration_Export_Button, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("GN_DataExport") as Bitmap, this.exportConfigurationDropDown);
      ribbonPageGroup3.ItemLinks.Add((BarItem) largeDropDownButton5);
    }
    if (ribbonPageGroup3.ItemLinks.Count > 0)
      this.ToolbarElements.Add((object) ribbonPageGroup3);
    foreach (IInstrumentPlugin instrumentPlugin1 in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.DataDownloadModuleType != (Type) null || p.DataUploadModuleType != (Type) null || p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      IInstrumentPlugin instrumentPlugin2 = instrumentPlugin1;
      if (instrumentPlugin2 != null)
      {
        RibbonPageGroup ribbonPageGroup4 = ribbonHelper.CreateRibbonPageGroup(instrumentPlugin1.Name);
        if (instrumentPlugin2.DataUploadModuleType != (Type) null)
        {
          IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin2.DataUploadModuleType);
          ribbonPageGroup4.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(applicationComponentModule));
        }
        if (instrumentPlugin2.DataDownloadModuleType != (Type) null)
        {
          IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin2.DataDownloadModuleType);
          ribbonPageGroup4.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(applicationComponentModule));
        }
        if (instrumentPlugin2.ConfigurationSynchronizationModuleType != (Type) null)
        {
          IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin2.ConfigurationSynchronizationModuleType);
          ribbonPageGroup4.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(applicationComponentModule));
        }
        if (ribbonPageGroup4.ItemLinks.Count > 0)
          this.ToolbarElements.Add((object) ribbonPageGroup4);
      }
    }
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources.PatientTestBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs eventArgs)
  {
    if (eventArgs == null || !(eventArgs.ChangedObject is CreationTimeStampFilterType))
      return;
    object changedObject = (object) (CreationTimeStampFilterType) eventArgs.ChangedObject;
    if (UserInterfaceManager.Instance.Ribbon.InvokeRequired)
      UserInterfaceManager.Instance.Ribbon.BeginInvoke((Delegate) new PatientTestBrowser.UpdateRibbonBarDobSelectorCallBack(this.UpdateRibbonBarDobSelector), changedObject);
    else
      this.UpdateRibbonBarDobSelector(changedObject);
  }

  private void UpdateRibbonBarDobSelector(object value)
  {
    if (this.repositoryItemDobButtonEdit == null)
      return;
    foreach (object obj in (CollectionBase) this.repositoryItemDobButtonEdit.Items)
    {
      if (obj.Equals(value))
        this.dobSelector.EditValue = value;
    }
  }

  private void DoBFilterChanged(object sender, EventArgs e)
  {
    UserInterfaceManager.Instance.Ribbon.Manager.ActiveEditItemLink.CloseEditor();
    if (!(this.dobSelector.EditValue is ComboBoxEditItemWrapper editValue))
      return;
    SearchTriggerContext context = new SearchTriggerContext(editValue.Value);
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Search, (TriggerContext) context));
  }

  private void DobSelectorInitialize(
    CreationTimeStampFilterType creationTimeStampFilterType)
  {
    ComboBoxEditItemWrapper boxEditItemWrapper = (ComboBoxEditItemWrapper) null;
    if (this.repositoryItemDobButtonEdit != null)
    {
      try
      {
        this.repositoryItemDobButtonEdit.Items.BeginUpdate();
        this.repositoryItemDobButtonEdit.Items.Clear();
        ComboBoxEditItemWrapper[] array = Enum.GetValues(typeof (CreationTimeStampFilterType)).Cast<object>().Select<object, ComboBoxEditItemWrapper>((Func<object, ComboBoxEditItemWrapper>) (v => new ComboBoxEditItemWrapper((Enum) v))).ToArray<ComboBoxEditItemWrapper>();
        this.repositoryItemDobButtonEdit.Items.AddRange((object[]) array);
        boxEditItemWrapper = ((IEnumerable<ComboBoxEditItemWrapper>) array).FirstOrDefault<ComboBoxEditItemWrapper>((Func<ComboBoxEditItemWrapper, bool>) (i => i.Value.Equals((object) creationTimeStampFilterType)));
      }
      finally
      {
        this.repositoryItemDobButtonEdit.Items.EndUpdate();
      }
    }
    if (this.dobSelector == null || boxEditItemWrapper == null)
      return;
    this.dobSelector.EditValue = (object) boxEditItemWrapper.Name;
  }

  private void FilterPatientData(object sender, EventArgs e)
  {
    string editValue = this.findPatientEditor.EditValue as string;
    if (string.IsNullOrEmpty(editValue))
      return;
    SearchTriggerContext context = new SearchTriggerContext(editValue);
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Search, (TriggerContext) context));
  }

  private void PatientSearchTextButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (e == null || e.Button.Kind != ButtonPredefines.Delete)
      return;
    SearchTriggerContext context = new SearchTriggerContext(string.Empty);
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Search, (TriggerContext) context));
    this.findPatientEditor.EditValue = (object) null;
    UserInterfaceManager.Instance.Ribbon.Manager.ActiveEditItemLink.CloseEditor();
  }

  private void SetParameters(bool vertical, bool diagonal, int h)
  {
  }

  private void CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
  {
  }

  private Rectangle Transform(Graphics g, int degree, Rectangle r)
  {
    g.RotateTransform((float) degree);
    float num1 = (float) Math.Round(Math.Cos((double) degree * (Math.PI / 180.0)), 2);
    float num2 = (float) Math.Round(Math.Sin((double) degree * (Math.PI / 180.0)), 2);
    Rectangle rectangle = r with
    {
      X = (int) ((double) r.X * (double) num1 + (double) r.Y * (double) num2),
      Y = (int) ((double) r.X * -(double) num2 + (double) r.Y * (double) num1)
    };
    rectangle.Offset(7, 0);
    return rectangle;
  }

  private void OnPatientDoubleClick(object sender, EventArgs e)
  {
    if (!(sender is GridView clickedView))
      return;
    int clickedRow = this.patientMultiSelectionGridHelper.GetClickedRow(clickedView, this.patientGrid.PointToClient(Cursor.Position));
    if (!clickedView.IsDataRow(clickedRow))
      return;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Edit, (TriggerContext) null));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PatientTestBrowser));
    this.layoutControl1 = new LayoutControl();
    this.detailViewer = new PanelControl();
    this.devExpressTextEdit1 = new DevExpressTextEdit();
    this.patientGrid = new GridControl();
    this.patientGridView = new BandedGridView();
    this.patientBand = new GridBand();
    this.columnPatientMedicalRecordNumber = new BandedGridColumn();
    this.columnPatientFullName = new BandedGridColumn();
    this.columnPatientDateOfBirth = new BandedGridColumn();
    this.columnPatientHasRiskIndicator = new BandedGridColumn();
    this.riskIndicatorImage = new RepositoryItemImageComboBox();
    this.columnPatientHasComments = new BandedGridColumn();
    this.commentImage = new RepositoryItemImageComboBox();
    this.abrBand = new GridBand();
    this.bestAbrLeft = new BandedGridColumn();
    this.overallTestResultImage = new RepositoryItemImageComboBox();
    this.bestAbrRight = new BandedGridColumn();
    this.teoaeBand = new GridBand();
    this.bestTeoaeLeft = new BandedGridColumn();
    this.bestTeoaeRight = new BandedGridColumn();
    this.dpoaeBand = new GridBand();
    this.bestDpoaeLeft = new BandedGridColumn();
    this.bestDpoaeRight = new BandedGridColumn();
    this.testGrid = new GridControl();
    this.testGridView = new GridView();
    this.columnTestType = new GridColumn();
    this.columnTestTime = new GridColumn();
    this.columnRightEarResult = new GridColumn();
    this.testResultImage = new RepositoryItemImageComboBox();
    this.columnLeftEarResult = new GridColumn();
    this.columnDuration = new GridColumn();
    this.columnExaminer = new GridColumn();
    this.columnTestName = new GridColumn();
    this.layoutDetailViewer = new LayoutControlGroup();
    this.patientGridLayout = new LayoutControlItem();
    this.advancedSearchGroupLayout = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.splitterItem1 = new SplitterItem();
    this.splitterItem2 = new SplitterItem();
    this.layoutGroupTestResults = new LayoutControlGroup();
    this.layoutTestGrid = new LayoutControlItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.detailViewer.BeginInit();
    this.devExpressTextEdit1.Properties.BeginInit();
    this.patientGrid.BeginInit();
    this.patientGridView.BeginInit();
    this.riskIndicatorImage.BeginInit();
    this.commentImage.BeginInit();
    this.overallTestResultImage.BeginInit();
    this.testGrid.BeginInit();
    this.testGridView.BeginInit();
    this.testResultImage.BeginInit();
    this.layoutDetailViewer.BeginInit();
    this.patientGridLayout.BeginInit();
    this.advancedSearchGroupLayout.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.splitterItem1.BeginInit();
    this.splitterItem2.BeginInit();
    this.layoutGroupTestResults.BeginInit();
    this.layoutTestGrid.BeginInit();
    this.SuspendLayout();
    this.layoutControl1.Controls.Add((Control) this.detailViewer);
    this.layoutControl1.Controls.Add((Control) this.devExpressTextEdit1);
    this.layoutControl1.Controls.Add((Control) this.patientGrid);
    this.layoutControl1.Controls.Add((Control) this.testGrid);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutDetailViewer;
    componentResourceManager.ApplyResources((object) this.detailViewer, "detailViewer");
    this.detailViewer.Name = "detailViewer";
    componentResourceManager.ApplyResources((object) this.devExpressTextEdit1, "devExpressTextEdit1");
    this.devExpressTextEdit1.EnterMoveNextControl = true;
    this.devExpressTextEdit1.FormatString = (string) null;
    this.devExpressTextEdit1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.devExpressTextEdit1.IsReadOnly = false;
    this.devExpressTextEdit1.IsUndoing = false;
    this.devExpressTextEdit1.Name = "devExpressTextEdit1";
    this.devExpressTextEdit1.Properties.Appearance.BorderColor = Color.Yellow;
    this.devExpressTextEdit1.Properties.Appearance.Options.UseBorderColor = true;
    this.devExpressTextEdit1.Properties.BorderStyle = BorderStyles.Simple;
    this.devExpressTextEdit1.StyleController = (IStyleController) this.layoutControl1;
    this.devExpressTextEdit1.Validator = (ICustomValidator) null;
    this.devExpressTextEdit1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientGrid, "patientGrid");
    this.patientGrid.MainView = (BaseView) this.patientGridView;
    this.patientGrid.Name = "patientGrid";
    this.patientGrid.RepositoryItems.AddRange(new RepositoryItem[3]
    {
      (RepositoryItem) this.overallTestResultImage,
      (RepositoryItem) this.riskIndicatorImage,
      (RepositoryItem) this.commentImage
    });
    this.patientGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.patientGridView
    });
    this.patientGridView.Bands.AddRange(new GridBand[4]
    {
      this.patientBand,
      this.abrBand,
      this.teoaeBand,
      this.dpoaeBand
    });
    this.patientGridView.Columns.AddRange(new BandedGridColumn[11]
    {
      this.columnPatientMedicalRecordNumber,
      this.columnPatientFullName,
      this.columnPatientDateOfBirth,
      this.columnPatientHasRiskIndicator,
      this.columnPatientHasComments,
      this.bestAbrLeft,
      this.bestAbrRight,
      this.bestDpoaeLeft,
      this.bestDpoaeRight,
      this.bestTeoaeLeft,
      this.bestTeoaeRight
    });
    this.patientGridView.GridControl = this.patientGrid;
    this.patientGridView.Name = "patientGridView";
    this.patientGridView.OptionsBehavior.Editable = false;
    this.patientGridView.OptionsDetail.AllowZoomDetail = false;
    this.patientGridView.OptionsDetail.EnableMasterViewMode = false;
    this.patientGridView.OptionsDetail.ShowDetailTabs = false;
    this.patientGridView.OptionsDetail.SmartDetailExpand = false;
    this.patientGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.patientGridView.OptionsSelection.MultiSelect = true;
    this.patientGridView.OptionsView.ShowGroupPanel = false;
    this.patientGridView.DoubleClick += new EventHandler(this.OnPatientDoubleClick);
    componentResourceManager.ApplyResources((object) this.patientBand, "patientBand");
    this.patientBand.Columns.Add(this.columnPatientMedicalRecordNumber);
    this.patientBand.Columns.Add(this.columnPatientFullName);
    this.patientBand.Columns.Add(this.columnPatientDateOfBirth);
    this.patientBand.Columns.Add(this.columnPatientHasRiskIndicator);
    this.patientBand.Columns.Add(this.columnPatientHasComments);
    componentResourceManager.ApplyResources((object) this.columnPatientMedicalRecordNumber, "columnPatientMedicalRecordNumber");
    this.columnPatientMedicalRecordNumber.FieldName = "PatientRecordNumberOrHospitalId";
    this.columnPatientMedicalRecordNumber.Name = "columnPatientMedicalRecordNumber";
    componentResourceManager.ApplyResources((object) this.columnPatientFullName, "columnPatientFullName");
    this.columnPatientFullName.FieldName = "PatientContact.FullName";
    this.columnPatientFullName.Name = "columnPatientFullName";
    componentResourceManager.ApplyResources((object) this.columnPatientDateOfBirth, "columnPatientDateOfBirth");
    this.columnPatientDateOfBirth.FieldName = "PatientContact.DateOfBirth";
    this.columnPatientDateOfBirth.Name = "columnPatientDateOfBirth";
    this.columnPatientHasRiskIndicator.AppearanceHeader.Options.UseTextOptions = true;
    this.columnPatientHasRiskIndicator.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.columnPatientHasRiskIndicator, "columnPatientHasRiskIndicator");
    this.columnPatientHasRiskIndicator.ColumnEdit = (RepositoryItem) this.riskIndicatorImage;
    this.columnPatientHasRiskIndicator.FieldName = "HasActiveRiskIndicator";
    this.columnPatientHasRiskIndicator.Name = "columnPatientHasRiskIndicator";
    componentResourceManager.ApplyResources((object) this.riskIndicatorImage, "riskIndicatorImage");
    this.riskIndicatorImage.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("riskIndicatorImage.Buttons"))
    });
    this.riskIndicatorImage.Name = "riskIndicatorImage";
    this.riskIndicatorImage.ReadOnly = true;
    this.riskIndicatorImage.ShowDropDown = ShowDropDown.Never;
    this.riskIndicatorImage.EditValueChanged += new EventHandler(this.ColumnEdit_EditValueChanged);
    this.columnPatientHasComments.AppearanceHeader.Options.UseTextOptions = true;
    this.columnPatientHasComments.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.columnPatientHasComments, "columnPatientHasComments");
    this.columnPatientHasComments.ColumnEdit = (RepositoryItem) this.commentImage;
    this.columnPatientHasComments.FieldName = "HasComments";
    this.columnPatientHasComments.Name = "columnPatientHasComments";
    componentResourceManager.ApplyResources((object) this.commentImage, "commentImage");
    this.commentImage.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("commentImage.Buttons"))
    });
    this.commentImage.Name = "commentImage";
    this.commentImage.ReadOnly = true;
    this.commentImage.ShowDropDown = ShowDropDown.Never;
    this.abrBand.AppearanceHeader.Options.UseTextOptions = true;
    this.abrBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.abrBand, "abrBand");
    this.abrBand.Columns.Add(this.bestAbrLeft);
    this.abrBand.Columns.Add(this.bestAbrRight);
    this.bestAbrLeft.AppearanceCell.Options.UseTextOptions = true;
    this.bestAbrLeft.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestAbrLeft.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
    this.bestAbrLeft.AppearanceHeader.Options.UseTextOptions = true;
    this.bestAbrLeft.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestAbrLeft.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestAbrLeft, "bestAbrLeft");
    this.bestAbrLeft.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestAbrLeft.FieldName = "OverallResultLeftEarAbr.OverallTestResult";
    this.bestAbrLeft.MinWidth = 55;
    this.bestAbrLeft.Name = "bestAbrLeft";
    this.overallTestResultImage.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("overallTestResultImage.Buttons"))
    });
    componentResourceManager.ApplyResources((object) this.overallTestResultImage, "overallTestResultImage");
    this.overallTestResultImage.Name = "overallTestResultImage";
    this.overallTestResultImage.ReadOnly = true;
    this.overallTestResultImage.ShowDropDown = ShowDropDown.Never;
    this.bestAbrRight.AppearanceCell.Options.UseTextOptions = true;
    this.bestAbrRight.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestAbrRight.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
    this.bestAbrRight.AppearanceHeader.Options.UseTextOptions = true;
    this.bestAbrRight.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestAbrRight.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestAbrRight, "bestAbrRight");
    this.bestAbrRight.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestAbrRight.FieldName = "OverallResultRightEarAbr.OverallTestResult";
    this.bestAbrRight.MinWidth = 55;
    this.bestAbrRight.Name = "bestAbrRight";
    this.teoaeBand.AppearanceHeader.Options.UseTextOptions = true;
    this.teoaeBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.teoaeBand, "teoaeBand");
    this.teoaeBand.Columns.Add(this.bestTeoaeLeft);
    this.teoaeBand.Columns.Add(this.bestTeoaeRight);
    this.bestTeoaeLeft.AppearanceHeader.Options.UseTextOptions = true;
    this.bestTeoaeLeft.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestTeoaeLeft.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestTeoaeLeft, "bestTeoaeLeft");
    this.bestTeoaeLeft.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestTeoaeLeft.FieldName = "OverallResultLeftEarTeoae.OverallTestResult";
    this.bestTeoaeLeft.MinWidth = 55;
    this.bestTeoaeLeft.Name = "bestTeoaeLeft";
    this.bestTeoaeRight.AppearanceHeader.Options.UseTextOptions = true;
    this.bestTeoaeRight.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestTeoaeRight.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestTeoaeRight, "bestTeoaeRight");
    this.bestTeoaeRight.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestTeoaeRight.FieldName = "OverallResultRightEarTeoae.OverallTestResult";
    this.bestTeoaeRight.MinWidth = 55;
    this.bestTeoaeRight.Name = "bestTeoaeRight";
    this.dpoaeBand.AppearanceHeader.Options.UseTextOptions = true;
    this.dpoaeBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.dpoaeBand, "dpoaeBand");
    this.dpoaeBand.Columns.Add(this.bestDpoaeLeft);
    this.dpoaeBand.Columns.Add(this.bestDpoaeRight);
    this.bestDpoaeLeft.AppearanceHeader.Options.UseTextOptions = true;
    this.bestDpoaeLeft.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestDpoaeLeft.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestDpoaeLeft, "bestDpoaeLeft");
    this.bestDpoaeLeft.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestDpoaeLeft.FieldName = "OverallResultLeftEarDpoae.OverallTestResult";
    this.bestDpoaeLeft.MinWidth = 55;
    this.bestDpoaeLeft.Name = "bestDpoaeLeft";
    this.bestDpoaeRight.AppearanceHeader.Options.UseTextOptions = true;
    this.bestDpoaeRight.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    this.bestDpoaeRight.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.bestDpoaeRight, "bestDpoaeRight");
    this.bestDpoaeRight.ColumnEdit = (RepositoryItem) this.overallTestResultImage;
    this.bestDpoaeRight.FieldName = "OverallResultRightEarDpoae.OverallTestResult";
    this.bestDpoaeRight.MinWidth = 55;
    this.bestDpoaeRight.Name = "bestDpoaeRight";
    componentResourceManager.ApplyResources((object) this.testGrid, "testGrid");
    this.testGrid.MainView = (BaseView) this.testGridView;
    this.testGrid.Name = "testGrid";
    this.testGrid.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.testResultImage
    });
    this.testGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.testGridView
    });
    this.testGridView.BestFitMaxRowCount = 5;
    this.testGridView.Columns.AddRange(new GridColumn[7]
    {
      this.columnTestType,
      this.columnTestTime,
      this.columnRightEarResult,
      this.columnLeftEarResult,
      this.columnDuration,
      this.columnExaminer,
      this.columnTestName
    });
    this.testGridView.GridControl = this.testGrid;
    this.testGridView.Name = "testGridView";
    this.testGridView.OptionsBehavior.Editable = false;
    this.testGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.testGridView.OptionsView.ShowGroupPanel = false;
    this.columnTestType.AppearanceCell.Options.UseTextOptions = true;
    this.columnTestType.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
    componentResourceManager.ApplyResources((object) this.columnTestType, "columnTestType");
    this.columnTestType.FieldName = "TestType";
    this.columnTestType.Name = "columnTestType";
    this.columnTestTime.AppearanceCell.Options.UseTextOptions = true;
    this.columnTestTime.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.columnTestTime, "columnTestTime");
    this.columnTestTime.DisplayFormat.FormatType = FormatType.DateTime;
    this.columnTestTime.FieldName = "TestDate";
    this.columnTestTime.Name = "columnTestTime";
    this.columnRightEarResult.AppearanceCell.Options.UseTextOptions = true;
    this.columnRightEarResult.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
    this.columnRightEarResult.AppearanceHeader.Options.UseTextOptions = true;
    this.columnRightEarResult.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.columnRightEarResult, "columnRightEarResult");
    this.columnRightEarResult.ColumnEdit = (RepositoryItem) this.testResultImage;
    this.columnRightEarResult.FieldName = "RightEarTestResult";
    this.columnRightEarResult.Name = "columnRightEarResult";
    this.testResultImage.Appearance.Options.UseTextOptions = true;
    this.testResultImage.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
    this.testResultImage.AppearanceDisabled.Options.UseTextOptions = true;
    this.testResultImage.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Center;
    this.testResultImage.AppearanceDropDown.Options.UseTextOptions = true;
    this.testResultImage.AppearanceDropDown.TextOptions.HAlignment = HorzAlignment.Center;
    this.testResultImage.AppearanceFocused.Options.UseTextOptions = true;
    this.testResultImage.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Center;
    this.testResultImage.AppearanceReadOnly.Options.UseTextOptions = true;
    this.testResultImage.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.testResultImage, "testResultImage");
    this.testResultImage.Name = "testResultImage";
    this.testResultImage.ReadOnly = true;
    this.columnLeftEarResult.AppearanceCell.Options.UseTextOptions = true;
    this.columnLeftEarResult.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
    this.columnLeftEarResult.AppearanceHeader.Options.UseTextOptions = true;
    this.columnLeftEarResult.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.columnLeftEarResult, "columnLeftEarResult");
    this.columnLeftEarResult.ColumnEdit = (RepositoryItem) this.testResultImage;
    this.columnLeftEarResult.FieldName = "LeftEarTestResult";
    this.columnLeftEarResult.Name = "columnLeftEarResult";
    this.columnDuration.AppearanceCell.Options.UseTextOptions = true;
    this.columnDuration.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.columnDuration, "columnDuration");
    this.columnDuration.FieldName = "DurationFormatted";
    this.columnDuration.Name = "columnDuration";
    componentResourceManager.ApplyResources((object) this.columnExaminer, "columnExaminer");
    this.columnExaminer.FieldName = "Examiner";
    this.columnExaminer.Name = "columnExaminer";
    componentResourceManager.ApplyResources((object) this.columnTestName, "columnTestName");
    this.columnTestName.FieldName = "TestName";
    this.columnTestName.Name = "columnTestName";
    componentResourceManager.ApplyResources((object) this.layoutDetailViewer, "layoutDetailViewer");
    this.layoutDetailViewer.Items.AddRange(new BaseLayoutItem[6]
    {
      (BaseLayoutItem) this.patientGridLayout,
      (BaseLayoutItem) this.advancedSearchGroupLayout,
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.splitterItem1,
      (BaseLayoutItem) this.splitterItem2,
      (BaseLayoutItem) this.layoutGroupTestResults
    });
    this.layoutDetailViewer.Location = new Point(0, 0);
    this.layoutDetailViewer.Name = "Root";
    this.layoutDetailViewer.Size = new Size(1000, 700);
    this.layoutDetailViewer.TextVisible = false;
    this.patientGridLayout.Control = (Control) this.patientGrid;
    componentResourceManager.ApplyResources((object) this.patientGridLayout, "patientGridLayout");
    this.patientGridLayout.Location = new Point(0, 76);
    this.patientGridLayout.Name = "layoutControlItem1";
    this.patientGridLayout.Size = new Size(980, 241);
    this.patientGridLayout.TextSize = new Size(0, 0);
    this.patientGridLayout.TextToControlDistance = 0;
    this.patientGridLayout.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.advancedSearchGroupLayout, "advancedSearchGroupLayout");
    this.advancedSearchGroupLayout.ExpandButtonLocation = GroupElementLocation.AfterText;
    this.advancedSearchGroupLayout.ExpandButtonVisible = true;
    this.advancedSearchGroupLayout.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.advancedSearchGroupLayout.Location = new Point(0, 0);
    this.advancedSearchGroupLayout.Name = "advancedSearchGroupLayout";
    this.advancedSearchGroupLayout.Size = new Size(980, 76);
    this.advancedSearchGroupLayout.Spacing = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
    this.advancedSearchGroupLayout.Visibility = LayoutVisibility.Never;
    this.layoutControlItem2.Control = (Control) this.devExpressTextEdit1;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(948, 24);
    this.layoutControlItem2.TextSize = new Size(93, 13);
    this.layoutControlItem1.Control = (Control) this.detailViewer;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(541, 323);
    this.layoutControlItem1.Name = "item0";
    this.layoutControlItem1.Size = new Size(439, 357);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.splitterItem1.AllowHotTrack = true;
    componentResourceManager.ApplyResources((object) this.splitterItem1, "splitterItem1");
    this.splitterItem1.Location = new Point(535, 323);
    this.splitterItem1.Name = "splitterItem1";
    this.splitterItem1.Size = new Size(6, 357);
    this.splitterItem2.AllowHotTrack = true;
    componentResourceManager.ApplyResources((object) this.splitterItem2, "splitterItem2");
    this.splitterItem2.Location = new Point(0, 317);
    this.splitterItem2.Name = "splitterItem2";
    this.splitterItem2.Size = new Size(980, 6);
    componentResourceManager.ApplyResources((object) this.layoutGroupTestResults, "layoutGroupTestResults");
    this.layoutGroupTestResults.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutTestGrid
    });
    this.layoutGroupTestResults.Location = new Point(0, 323);
    this.layoutGroupTestResults.Name = "layoutGroupTestResults";
    this.layoutGroupTestResults.Size = new Size(535, 357);
    this.layoutTestGrid.AllowHide = false;
    this.layoutTestGrid.Control = (Control) this.testGrid;
    componentResourceManager.ApplyResources((object) this.layoutTestGrid, "layoutTestGrid");
    this.layoutTestGrid.Location = new Point(0, 0);
    this.layoutTestGrid.Name = "layoutTestGrid";
    this.layoutTestGrid.ShowInCustomizationForm = false;
    this.layoutTestGrid.Size = new Size(511 /*0x01FF*/, 313);
    this.layoutTestGrid.TextSize = new Size(0, 0);
    this.layoutTestGrid.TextToControlDistance = 0;
    this.layoutTestGrid.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (PatientTestBrowser);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.detailViewer.EndInit();
    this.devExpressTextEdit1.Properties.EndInit();
    this.patientGrid.EndInit();
    this.patientGridView.EndInit();
    this.riskIndicatorImage.EndInit();
    this.commentImage.EndInit();
    this.overallTestResultImage.EndInit();
    this.testGrid.EndInit();
    this.testGridView.EndInit();
    this.testResultImage.EndInit();
    this.layoutDetailViewer.EndInit();
    this.patientGridLayout.EndInit();
    this.advancedSearchGroupLayout.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem1.EndInit();
    this.splitterItem1.EndInit();
    this.splitterItem2.EndInit();
    this.layoutGroupTestResults.EndInit();
    this.layoutTestGrid.EndInit();
    this.ResumeLayout(false);
  }

  private void ColumnEdit_EditValueChanged(object sender, EventArgs e)
  {
  }

  private delegate void UpdateRibbonBarDobSelectorCallBack(object value);
}
