// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels.PatientRiskIndicatorEditor
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels;

[ToolboxItem(false)]
public class PatientRiskIndicatorEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<RiskIndicator> modelMapper;
  private DevExpressSingleSelectionGridViewHelper<RiskIndicator> riskIndicatorGridViewHelper;
  private IContainer components;
  private LayoutControl layoutControl1;
  private GridControl riskIndicatorGrid;
  private GridView riskIndicatorGridView;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutRiskIndicatorGrid;
  private DevExpressMemoEdit riskIndicatorDescription;
  private DevExpressTextEdit riskIndicatorName;
  private LayoutControlItem layoutRiskIndicatorName;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlItem layoutRiskIndicatorDescription;
  private DevExpressComboBoxEdit riskIndicatorPreventScreening;
  private LayoutControlItem layoutRiskIndicatorPreventScreening;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlGroup layoutControlGroup3;
  private GridColumn columnLocalizedName;
  private GridColumn columnPatientRiskIndicator;
  private RepositoryItemImageComboBox patientRiskIndicatorAssignmentImage;
  private SimpleSeparator simpleSeparator1;
  private GridColumn columnOrderNumber;
  private DevExpressComboBoxEdit riskIndicatorPatientAssignment;
  private LayoutControlItem layoutRiskIndicatorPatientAssignment;
  private LayoutControlGroup layoutGroupRiskFactors;

  public RibbonPageGroup QuickChangeRiskIndicatorGroup { get; protected set; }

  public BarButtonItem AssignUnknownRiskToNoRiskElement { get; protected set; }

  public PatientRiskIndicatorEditor()
  {
    this.InitializeComponent();
    DevExpress.Utils.ImageCollection imageCollection = new DevExpress.Utils.ImageCollection();
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnknown") as Bitmap));
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorAssigned") as Bitmap));
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnassigned") as Bitmap));
    this.patientRiskIndicatorAssignmentImage.Items.Clear();
    ImageComboBoxItemCollection items1 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem1 = new ImageComboBoxItem();
    imageComboBoxItem1.ImageIndex = 0;
    imageComboBoxItem1.Value = (object) RiskIndicatorValueType.Unknown;
    items1.Add(imageComboBoxItem1);
    ImageComboBoxItemCollection items2 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem2 = new ImageComboBoxItem();
    imageComboBoxItem2.ImageIndex = 1;
    imageComboBoxItem2.Value = (object) RiskIndicatorValueType.Yes;
    items2.Add(imageComboBoxItem2);
    ImageComboBoxItemCollection items3 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem3 = new ImageComboBoxItem();
    imageComboBoxItem3.ImageIndex = 2;
    imageComboBoxItem3.Value = (object) RiskIndicatorValueType.No;
    items3.Add(imageComboBoxItem3);
    this.InitializeToolBarElements();
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
    this.HelpMarker = "patients_details_riskfactors_01.html";
  }

  public PatientRiskIndicatorEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.riskIndicatorGridViewHelper = new DevExpressSingleSelectionGridViewHelper<RiskIndicator>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.riskIndicatorGridView, model, PatientManagementTriggers.SelectRiskFactor, "RiskIndicators");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void InitializeToolBarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.QuickChangeRiskIndicatorGroup = ribbonHelper.CreateRibbonPageGroup(Resources.PatientRiskIndicatorEditor_RibbonQuickChangeRiskIndicatorGroup);
    this.AssignUnknownRiskToNoRiskElement = ribbonHelper.CreateLargeImageButton(Resources.PatientRiskIndicatorEditor_RibbonAssignUnknownRiskToNoRisk_Caption, Resources.PatientRiskIndicatorEditor_RibbonAssignUnknownRiskToNoRisk_Description, string.Empty, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("SetUnkownToNo") as Bitmap, PatientManagementTriggers.AssignUnknownRiskToNoRisk);
    this.AssignUnknownRiskToNoRiskElement.Enabled = false;
    this.QuickChangeRiskIndicatorGroup.ItemLinks.Add((BarItem) this.AssignUnknownRiskToNoRiskElement);
    this.ToolbarElements.Add((object) this.QuickChangeRiskIndicatorGroup);
  }

  private void InitializeSelectionValues()
  {
    this.riskIndicatorPatientAssignment.DataSource = (object) new RiskIndicatorValueType[3]
    {
      RiskIndicatorValueType.Yes,
      RiskIndicatorValueType.No,
      RiskIndicatorValueType.Unknown
    };
    DevExpress.Utils.ImageCollection imageCollection = new DevExpress.Utils.ImageCollection();
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorAssigned") as Bitmap));
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnassigned") as Bitmap));
    imageCollection.AddImage((Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnknown") as Bitmap));
    this.patientRiskIndicatorAssignmentImage.Items.Clear();
    this.patientRiskIndicatorAssignmentImage.SmallImages = (object) imageCollection;
    ImageComboBoxItemCollection items1 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem1 = new ImageComboBoxItem();
    imageComboBoxItem1.Value = (object) RiskIndicatorValueType.Yes;
    imageComboBoxItem1.Description = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskIndicatorValueType.Yes");
    imageComboBoxItem1.ImageIndex = 0;
    items1.Add(imageComboBoxItem1);
    ImageComboBoxItemCollection items2 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem2 = new ImageComboBoxItem();
    imageComboBoxItem2.Value = (object) RiskIndicatorValueType.No;
    imageComboBoxItem2.Description = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskIndicatorValueType.No");
    imageComboBoxItem2.ImageIndex = 1;
    items2.Add(imageComboBoxItem2);
    ImageComboBoxItemCollection items3 = this.patientRiskIndicatorAssignmentImage.Items;
    ImageComboBoxItem imageComboBoxItem3 = new ImageComboBoxItem();
    imageComboBoxItem3.Value = (object) RiskIndicatorValueType.Unknown;
    imageComboBoxItem3.Description = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskIndicatorValueType.Unknown");
    imageComboBoxItem3.ImageIndex = 2;
    items3.Add(imageComboBoxItem3);
  }

  private void InitializeModelMapper()
  {
    ModelMapper<RiskIndicator> modelMapper = new ModelMapper<RiskIndicator>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (ri => ri.SafeName), (object) this.riskIndicatorName);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (ri => ri.SafeDescription), (object) this.riskIndicatorDescription);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (ri => (object) ri.PreventScreening), (object) this.riskIndicatorPreventScreening);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (ri => (object) ri.PatientRiskIndicatorValue), (object) this.riskIndicatorPatientAssignment);
    this.modelMapper = modelMapper;
    this.modelMapper.SetUIEnabledForced(false, (object) this.riskIndicatorName, (object) this.riskIndicatorDescription, (object) this.riskIndicatorPreventScreening);
  }

  private void FillFields(RiskIndicator riskIndicator)
  {
    this.modelMapper.SetUIEnabled(riskIndicator != null && this.ViewMode != 0);
    this.modelMapper.CopyModelToUI(riskIndicator);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited || !(e.Type == typeof (RiskIndicator)) || e.IsList)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PatientRiskIndicatorEditor.UpdateRiskIndicatorDataCallBack(this.FillFields), (object) (e.ChangedObject as RiskIndicator));
    else
      this.FillFields(e.ChangedObject as RiskIndicator);
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void PatientRiskIndicatorAssignmentChanged(object sender, EventArgs e)
  {
    if (this.riskIndicatorPatientAssignment.IsValueSetting || this.riskIndicatorPatientAssignment.IsUndoing)
      return;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.RefreshDataFromForm));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PatientRiskIndicatorEditor));
    this.layoutControl1 = new LayoutControl();
    this.riskIndicatorPatientAssignment = new DevExpressComboBoxEdit();
    this.riskIndicatorPreventScreening = new DevExpressComboBoxEdit();
    this.riskIndicatorDescription = new DevExpressMemoEdit();
    this.riskIndicatorGrid = new GridControl();
    this.riskIndicatorGridView = new GridView();
    this.columnLocalizedName = new GridColumn();
    this.columnPatientRiskIndicator = new GridColumn();
    this.patientRiskIndicatorAssignmentImage = new RepositoryItemImageComboBox();
    this.columnOrderNumber = new GridColumn();
    this.riskIndicatorName = new DevExpressTextEdit();
    this.layoutRiskIndicatorPreventScreening = new LayoutControlItem();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutRiskIndicatorName = new LayoutControlItem();
    this.layoutRiskIndicatorDescription = new LayoutControlItem();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutRiskIndicatorPatientAssignment = new LayoutControlItem();
    this.simpleSeparator1 = new SimpleSeparator();
    this.layoutGroupRiskFactors = new LayoutControlGroup();
    this.layoutRiskIndicatorGrid = new LayoutControlItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.riskIndicatorPatientAssignment.Properties.BeginInit();
    this.riskIndicatorPreventScreening.Properties.BeginInit();
    this.riskIndicatorDescription.Properties.BeginInit();
    this.riskIndicatorGrid.BeginInit();
    this.riskIndicatorGridView.BeginInit();
    this.patientRiskIndicatorAssignmentImage.BeginInit();
    this.riskIndicatorName.Properties.BeginInit();
    this.layoutRiskIndicatorPreventScreening.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutRiskIndicatorName.BeginInit();
    this.layoutRiskIndicatorDescription.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.layoutRiskIndicatorPatientAssignment.BeginInit();
    this.simpleSeparator1.BeginInit();
    this.layoutGroupRiskFactors.BeginInit();
    this.layoutRiskIndicatorGrid.BeginInit();
    this.SuspendLayout();
    this.layoutControl1.Controls.Add((Control) this.riskIndicatorPatientAssignment);
    this.layoutControl1.Controls.Add((Control) this.riskIndicatorPreventScreening);
    this.layoutControl1.Controls.Add((Control) this.riskIndicatorDescription);
    this.layoutControl1.Controls.Add((Control) this.riskIndicatorGrid);
    this.layoutControl1.Controls.Add((Control) this.riskIndicatorName);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorPreventScreening
    });
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    this.riskIndicatorPatientAssignment.EnterMoveNextControl = true;
    this.riskIndicatorPatientAssignment.FormatString = (string) null;
    this.riskIndicatorPatientAssignment.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorPatientAssignment.IsReadOnly = false;
    this.riskIndicatorPatientAssignment.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.riskIndicatorPatientAssignment, "riskIndicatorPatientAssignment");
    this.riskIndicatorPatientAssignment.Name = "riskIndicatorPatientAssignment";
    this.riskIndicatorPatientAssignment.Properties.Appearance.BorderColor = Color.LightGray;
    this.riskIndicatorPatientAssignment.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorPatientAssignment.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorPatientAssignment.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("riskIndicatorPatientAssignment.Properties.Buttons"))
    });
    this.riskIndicatorPatientAssignment.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riskIndicatorPatientAssignment.ShowEmptyElement = false;
    this.riskIndicatorPatientAssignment.StyleController = (IStyleController) this.layoutControl1;
    this.riskIndicatorPatientAssignment.Validator = (ICustomValidator) null;
    this.riskIndicatorPatientAssignment.Value = (object) null;
    this.riskIndicatorPatientAssignment.SelectedIndexChanged += new EventHandler(this.PatientRiskIndicatorAssignmentChanged);
    this.riskIndicatorPreventScreening.EnterMoveNextControl = true;
    this.riskIndicatorPreventScreening.FormatString = (string) null;
    this.riskIndicatorPreventScreening.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorPreventScreening.IsActive = false;
    this.riskIndicatorPreventScreening.IsReadOnly = false;
    this.riskIndicatorPreventScreening.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.riskIndicatorPreventScreening, "riskIndicatorPreventScreening");
    this.riskIndicatorPreventScreening.Name = "riskIndicatorPreventScreening";
    this.riskIndicatorPreventScreening.Properties.Appearance.BorderColor = Color.LightGray;
    this.riskIndicatorPreventScreening.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorPreventScreening.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorPreventScreening.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("riskIndicatorPreventScreening.Properties.Buttons"))
    });
    this.riskIndicatorPreventScreening.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riskIndicatorPreventScreening.ShowEmptyElement = true;
    this.riskIndicatorPreventScreening.StyleController = (IStyleController) this.layoutControl1;
    this.riskIndicatorPreventScreening.Validator = (ICustomValidator) null;
    this.riskIndicatorPreventScreening.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.riskIndicatorDescription, "riskIndicatorDescription");
    this.riskIndicatorDescription.EnterMoveNextControl = true;
    this.riskIndicatorDescription.FormatString = (string) null;
    this.riskIndicatorDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorDescription.IsReadOnly = false;
    this.riskIndicatorDescription.IsUndoing = false;
    this.riskIndicatorDescription.Name = "riskIndicatorDescription";
    this.riskIndicatorDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.riskIndicatorDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorDescription.Properties.LinesCount = 4;
    this.riskIndicatorDescription.StyleController = (IStyleController) this.layoutControl1;
    this.riskIndicatorDescription.Validator = (ICustomValidator) null;
    this.riskIndicatorDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.riskIndicatorGrid, "riskIndicatorGrid");
    this.riskIndicatorGrid.MainView = (BaseView) this.riskIndicatorGridView;
    this.riskIndicatorGrid.Name = "riskIndicatorGrid";
    this.riskIndicatorGrid.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.patientRiskIndicatorAssignmentImage
    });
    this.riskIndicatorGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.riskIndicatorGridView
    });
    this.riskIndicatorGridView.Columns.AddRange(new GridColumn[3]
    {
      this.columnLocalizedName,
      this.columnPatientRiskIndicator,
      this.columnOrderNumber
    });
    this.riskIndicatorGridView.GridControl = this.riskIndicatorGrid;
    this.riskIndicatorGridView.Name = "riskIndicatorGridView";
    this.riskIndicatorGridView.OptionsBehavior.Editable = false;
    this.riskIndicatorGridView.OptionsCustomization.AllowFilter = false;
    this.riskIndicatorGridView.OptionsCustomization.AllowSort = false;
    this.riskIndicatorGridView.OptionsFilter.AllowFilterEditor = false;
    this.riskIndicatorGridView.OptionsMenu.EnableColumnMenu = false;
    this.riskIndicatorGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.riskIndicatorGridView.OptionsView.ShowDetailButtons = false;
    this.riskIndicatorGridView.OptionsView.ShowGroupPanel = false;
    this.riskIndicatorGridView.SortInfo.AddRange(new GridColumnSortInfo[1]
    {
      new GridColumnSortInfo(this.columnOrderNumber, ColumnSortOrder.Ascending)
    });
    this.columnLocalizedName.AppearanceCell.Options.UseTextOptions = true;
    this.columnLocalizedName.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
    componentResourceManager.ApplyResources((object) this.columnLocalizedName, "columnLocalizedName");
    this.columnLocalizedName.FieldName = "SafeName";
    this.columnLocalizedName.Name = "columnLocalizedName";
    this.columnLocalizedName.OptionsColumn.ReadOnly = true;
    this.columnPatientRiskIndicator.AppearanceHeader.Options.UseTextOptions = true;
    this.columnPatientRiskIndicator.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
    componentResourceManager.ApplyResources((object) this.columnPatientRiskIndicator, "columnPatientRiskIndicator");
    this.columnPatientRiskIndicator.ColumnEdit = (RepositoryItem) this.patientRiskIndicatorAssignmentImage;
    this.columnPatientRiskIndicator.FieldName = "PatientRiskIndicatorValue";
    this.columnPatientRiskIndicator.Name = "columnPatientRiskIndicator";
    componentResourceManager.ApplyResources((object) this.patientRiskIndicatorAssignmentImage, "patientRiskIndicatorAssignmentImage");
    this.patientRiskIndicatorAssignmentImage.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientRiskIndicatorAssignmentImage.Buttons"))
    });
    this.patientRiskIndicatorAssignmentImage.Items.AddRange(new ImageComboBoxItem[1]
    {
      new ImageComboBoxItem(componentResourceManager.GetString("patientRiskIndicatorAssignmentImage.Items"), componentResourceManager.GetObject("patientRiskIndicatorAssignmentImage.Items1"), (int) componentResourceManager.GetObject("patientRiskIndicatorAssignmentImage.Items2"))
    });
    this.patientRiskIndicatorAssignmentImage.Name = "patientRiskIndicatorAssignmentImage";
    componentResourceManager.ApplyResources((object) this.columnOrderNumber, "columnOrderNumber");
    this.columnOrderNumber.FieldName = "OrderNumber";
    this.columnOrderNumber.Name = "columnOrderNumber";
    this.columnOrderNumber.SortMode = ColumnSortMode.Value;
    componentResourceManager.ApplyResources((object) this.riskIndicatorName, "riskIndicatorName");
    this.riskIndicatorName.EnterMoveNextControl = true;
    this.riskIndicatorName.FormatString = (string) null;
    this.riskIndicatorName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorName.IsReadOnly = false;
    this.riskIndicatorName.IsUndoing = false;
    this.riskIndicatorName.Name = "riskIndicatorName";
    this.riskIndicatorName.Properties.Appearance.BorderColor = Color.Yellow;
    this.riskIndicatorName.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorName.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorName.StyleController = (IStyleController) this.layoutControl1;
    this.riskIndicatorName.Validator = (ICustomValidator) null;
    this.riskIndicatorName.Value = (object) "";
    this.layoutRiskIndicatorPreventScreening.Control = (Control) this.riskIndicatorPreventScreening;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorPreventScreening, "layoutRiskIndicatorPreventScreening");
    this.layoutRiskIndicatorPreventScreening.Location = new Point(0, (int) sbyte.MaxValue);
    this.layoutRiskIndicatorPreventScreening.Name = "layoutRiskIndicatorPreventScreening";
    this.layoutRiskIndicatorPreventScreening.Size = new Size(380, 24);
    this.layoutRiskIndicatorPreventScreening.TextSize = new Size(88, 13);
    this.layoutRiskIndicatorPreventScreening.TextToControlDistance = 5;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutControlGroup2,
      (BaseLayoutItem) this.layoutControlGroup3,
      (BaseLayoutItem) this.simpleSeparator1,
      (BaseLayoutItem) this.layoutGroupRiskFactors
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(840, 665);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(416, 263);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(404, 382);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorName,
      (BaseLayoutItem) this.layoutRiskIndicatorDescription
    });
    this.layoutControlGroup2.Location = new Point(416, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(404, 195);
    this.layoutRiskIndicatorName.Control = (Control) this.riskIndicatorName;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorName, "layoutRiskIndicatorName");
    this.layoutRiskIndicatorName.Location = new Point(0, 0);
    this.layoutRiskIndicatorName.Name = "layoutRiskIndicatorName";
    this.layoutRiskIndicatorName.Size = new Size(380, 24);
    this.layoutRiskIndicatorName.TextSize = new Size(53, 13);
    this.layoutRiskIndicatorDescription.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutRiskIndicatorDescription.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutRiskIndicatorDescription.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutRiskIndicatorDescription.Control = (Control) this.riskIndicatorDescription;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorDescription, "layoutRiskIndicatorDescription");
    this.layoutRiskIndicatorDescription.Location = new Point(0, 24);
    this.layoutRiskIndicatorDescription.Name = "layoutRiskIndicatorDescription";
    this.layoutRiskIndicatorDescription.Size = new Size(380, (int) sbyte.MaxValue);
    this.layoutRiskIndicatorDescription.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorPatientAssignment
    });
    this.layoutControlGroup3.Location = new Point(416, 195);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(404, 68);
    this.layoutRiskIndicatorPatientAssignment.Control = (Control) this.riskIndicatorPatientAssignment;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorPatientAssignment, "layoutRiskIndicatorPatientAssignment");
    this.layoutRiskIndicatorPatientAssignment.Location = new Point(0, 0);
    this.layoutRiskIndicatorPatientAssignment.Name = "layoutRiskIndicatorPatientAssignment";
    this.layoutRiskIndicatorPatientAssignment.Size = new Size(380, 24);
    this.layoutRiskIndicatorPatientAssignment.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.simpleSeparator1, "simpleSeparator1");
    this.simpleSeparator1.Location = new Point(414, 0);
    this.simpleSeparator1.Name = "simpleSeparator1";
    this.simpleSeparator1.Size = new Size(2, 645);
    componentResourceManager.ApplyResources((object) this.layoutGroupRiskFactors, "layoutGroupRiskFactors");
    this.layoutGroupRiskFactors.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorGrid
    });
    this.layoutGroupRiskFactors.Location = new Point(0, 0);
    this.layoutGroupRiskFactors.Name = "layoutGroupRiskFactors";
    this.layoutGroupRiskFactors.Size = new Size(414, 645);
    this.layoutRiskIndicatorGrid.Control = (Control) this.riskIndicatorGrid;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorGrid, "layoutRiskIndicatorGrid");
    this.layoutRiskIndicatorGrid.Location = new Point(0, 0);
    this.layoutRiskIndicatorGrid.Name = "layoutRiskIndicatorGrid";
    this.layoutRiskIndicatorGrid.Size = new Size(390, 601);
    this.layoutRiskIndicatorGrid.TextSize = new Size(0, 0);
    this.layoutRiskIndicatorGrid.TextToControlDistance = 0;
    this.layoutRiskIndicatorGrid.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (PatientRiskIndicatorEditor);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.riskIndicatorPatientAssignment.Properties.EndInit();
    this.riskIndicatorPreventScreening.Properties.EndInit();
    this.riskIndicatorDescription.Properties.EndInit();
    this.riskIndicatorGrid.EndInit();
    this.riskIndicatorGridView.EndInit();
    this.patientRiskIndicatorAssignmentImage.EndInit();
    this.riskIndicatorName.Properties.EndInit();
    this.layoutRiskIndicatorPreventScreening.EndInit();
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutRiskIndicatorName.EndInit();
    this.layoutRiskIndicatorDescription.EndInit();
    this.layoutControlGroup3.EndInit();
    this.layoutRiskIndicatorPatientAssignment.EndInit();
    this.simpleSeparator1.EndInit();
    this.layoutGroupRiskFactors.EndInit();
    this.layoutRiskIndicatorGrid.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateRiskIndicatorDataCallBack(RiskIndicator riskIndicator);
}
