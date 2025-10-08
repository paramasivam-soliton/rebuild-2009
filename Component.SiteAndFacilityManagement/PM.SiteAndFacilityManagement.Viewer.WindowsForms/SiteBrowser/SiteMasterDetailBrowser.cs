// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser.SiteMasterDetailBrowser
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;

[ToolboxItem(false)]
public sealed class SiteMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressSingleSelectionGridViewHelper<PathMedical.SiteAndFacilityManagement.Site> siteMvcHelper;
  private ModelMapper<PathMedical.SiteAndFacilityManagement.Site> modelMapper;
  private IContainer components;
  private GridControl siteGrid;
  private GridView siteGridView;
  private DevExpressTextEdit siteNameTextEdit;
  private LayoutControl layoutControl;
  private DevExpressTextEdit siteCodeTextEdit;
  private DevExpressTextEdit siteDescriptionTextEdit;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutControlItem3;
  private GridColumn nameColumn;
  private GridColumn descriptionColumn;
  private GridColumn codeColumn;
  private LayoutControlGroup layoutControlGroup2;
  private GridView facilityGridView;
  private GridColumn facilityNameColumn;
  private GridColumn facilityDescriptionColumn;
  private GridColumn facilityCodeColumn;
  private LayoutControlGroup layoutGroupSites;

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new SiteMasterDetailBrowser.OnUpdateModelCallBack(this.OnUpdateModelData), (object) e);
    else
      this.OnUpdateModelData(e);
  }

  private void OnUpdateModelData(ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && e.ChangedObject is PathMedical.SiteAndFacilityManagement.Site)
      this.FillFields(e.ChangedObject as PathMedical.SiteAndFacilityManagement.Site);
    if (e.ChangeType != ChangeType.ItemDeleted || e.ChangedObject != null)
      return;
    this.FillFields(e.ChangedObject as PathMedical.SiteAndFacilityManagement.Site);
  }

  public SiteMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.BuildUI();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.HelpMarker = "site_mgmt_01.html";
  }

  public SiteMasterDetailBrowser(IModel model)
    : this()
  {
    this.siteMvcHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<PathMedical.SiteAndFacilityManagement.Site>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.siteGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void BuildUI()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.SiteMasterDetailBrowser_RibbonMaintenanceGroup, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SiteAdd"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SiteEdit"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SiteDelete")));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.SiteMasterDetailBrowser_RibbonModificationGroup));
    RibbonPageGroup ribbonPageGroup1 = ribbonHelper.CreateRibbonPageGroup(Resources.SiteMasterDetailBrowser_RibbonConfigurationGroup);
    BarButtonItem largeImageButton1 = ribbonHelper.CreateLargeImageButton(Resources.SiteMasterDetailBrowser_ManageFacilitiesButtonName, string.Empty, string.Empty, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_FacilityManage") as Bitmap, SiteFacilityManagementTriggers.SwitchToFacilityBrowser);
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton1);
    this.ToolbarElements.Add((object) ribbonPageGroup1);
    BarButtonItem largeImageButton2 = ribbonHelper.CreateLargeImageButton(Resources.SiteMasterDetailBrowser_ManageLocationsButtonName, string.Empty, string.Empty, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_LocationManage") as Bitmap, SiteFacilityManagementTriggers.SwitchToLocationTypeBrowser);
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton2);
    foreach (IInstrumentPlugin instrumentPlugin1 in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      IInstrumentPlugin instrumentPlugin2 = instrumentPlugin1;
      if (instrumentPlugin2 != null)
      {
        RibbonPageGroup ribbonPageGroup2 = ribbonHelper.CreateRibbonPageGroup(instrumentPlugin1.Name);
        if (instrumentPlugin2.ConfigurationSynchronizationModuleType != (Type) null)
        {
          IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin2.ConfigurationSynchronizationModuleType);
          ribbonPageGroup2.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(applicationComponentModule));
        }
        if (ribbonPageGroup2.ItemLinks.Count > 0)
          this.ToolbarElements.Add((object) ribbonPageGroup2);
      }
    }
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.SiteMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void FillFields(PathMedical.SiteAndFacilityManagement.Site site)
  {
    this.modelMapper.CopyModelToUI(site);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeModelMapper()
  {
    ModelMapper<PathMedical.SiteAndFacilityManagement.Site> modelMapper = new ModelMapper<PathMedical.SiteAndFacilityManagement.Site>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<PathMedical.SiteAndFacilityManagement.Site, object>>) (s => s.Name), (object) this.siteNameTextEdit);
    modelMapper.Add((Expression<Func<PathMedical.SiteAndFacilityManagement.Site, object>>) (s => s.Description), (object) this.siteDescriptionTextEdit);
    modelMapper.Add((Expression<Func<PathMedical.SiteAndFacilityManagement.Site, object>>) (s => s.Code), (object) this.siteCodeTextEdit);
    this.modelMapper = modelMapper;
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SiteMasterDetailBrowser));
    GridLevelNode gridLevelNode = new GridLevelNode();
    this.facilityGridView = new GridView();
    this.facilityNameColumn = new GridColumn();
    this.facilityDescriptionColumn = new GridColumn();
    this.facilityCodeColumn = new GridColumn();
    this.siteGrid = new GridControl();
    this.siteGridView = new GridView();
    this.nameColumn = new GridColumn();
    this.descriptionColumn = new GridColumn();
    this.codeColumn = new GridColumn();
    this.siteNameTextEdit = new DevExpressTextEdit();
    this.layoutControl = new LayoutControl();
    this.siteCodeTextEdit = new DevExpressTextEdit();
    this.siteDescriptionTextEdit = new DevExpressTextEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutGroupSites = new LayoutControlGroup();
    this.layoutControlItem4 = new LayoutControlItem();
    this.facilityGridView.BeginInit();
    this.siteGrid.BeginInit();
    this.siteGridView.BeginInit();
    this.siteNameTextEdit.Properties.BeginInit();
    this.layoutControl.BeginInit();
    this.layoutControl.SuspendLayout();
    this.siteCodeTextEdit.Properties.BeginInit();
    this.siteDescriptionTextEdit.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutGroupSites.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.facilityGridView, "facilityGridView");
    this.facilityGridView.Columns.AddRange(new GridColumn[3]
    {
      this.facilityNameColumn,
      this.facilityDescriptionColumn,
      this.facilityCodeColumn
    });
    this.facilityGridView.GridControl = this.siteGrid;
    this.facilityGridView.Name = "facilityGridView";
    this.facilityGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.facilityGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.facilityGridView.OptionsBehavior.Editable = false;
    this.facilityGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.facilityGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.facilityGridView.OptionsView.EnableAppearanceOddRow = true;
    this.facilityGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.facilityNameColumn, "facilityNameColumn");
    this.facilityNameColumn.FieldName = "Name";
    this.facilityNameColumn.Name = "facilityNameColumn";
    componentResourceManager.ApplyResources((object) this.facilityDescriptionColumn, "facilityDescriptionColumn");
    this.facilityDescriptionColumn.FieldName = "Description";
    this.facilityDescriptionColumn.Name = "facilityDescriptionColumn";
    componentResourceManager.ApplyResources((object) this.facilityCodeColumn, "facilityCodeColumn");
    this.facilityCodeColumn.FieldName = "Code";
    this.facilityCodeColumn.Name = "facilityCodeColumn";
    componentResourceManager.ApplyResources((object) this.siteGrid, "siteGrid");
    this.siteGrid.EmbeddedNavigator.AccessibleDescription = componentResourceManager.GetString("siteGrid.EmbeddedNavigator.AccessibleDescription");
    this.siteGrid.EmbeddedNavigator.AccessibleName = componentResourceManager.GetString("siteGrid.EmbeddedNavigator.AccessibleName");
    this.siteGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = (DefaultBoolean) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.AllowHtmlTextInToolTip");
    this.siteGrid.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.Anchor");
    this.siteGrid.EmbeddedNavigator.BackgroundImage = (Image) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.BackgroundImage");
    this.siteGrid.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.BackgroundImageLayout");
    this.siteGrid.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.ImeMode");
    this.siteGrid.EmbeddedNavigator.TextLocation = (NavigatorButtonsTextLocation) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.TextLocation");
    this.siteGrid.EmbeddedNavigator.ToolTip = componentResourceManager.GetString("siteGrid.EmbeddedNavigator.ToolTip");
    this.siteGrid.EmbeddedNavigator.ToolTipIconType = (ToolTipIconType) componentResourceManager.GetObject("siteGrid.EmbeddedNavigator.ToolTipIconType");
    this.siteGrid.EmbeddedNavigator.ToolTipTitle = componentResourceManager.GetString("siteGrid.EmbeddedNavigator.ToolTipTitle");
    gridLevelNode.LevelTemplate = (BaseView) this.facilityGridView;
    gridLevelNode.RelationName = "Facilities";
    this.siteGrid.LevelTree.Nodes.AddRange(new GridLevelNode[1]
    {
      gridLevelNode
    });
    this.siteGrid.MainView = (BaseView) this.siteGridView;
    this.siteGrid.Name = "siteGrid";
    this.siteGrid.ViewCollection.AddRange(new BaseView[2]
    {
      (BaseView) this.siteGridView,
      (BaseView) this.facilityGridView
    });
    componentResourceManager.ApplyResources((object) this.siteGridView, "siteGridView");
    this.siteGridView.Columns.AddRange(new GridColumn[3]
    {
      this.nameColumn,
      this.descriptionColumn,
      this.codeColumn
    });
    this.siteGridView.GridControl = this.siteGrid;
    this.siteGridView.Name = "siteGridView";
    this.siteGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.siteGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.siteGridView.OptionsBehavior.Editable = false;
    this.siteGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.siteGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.siteGridView.OptionsView.EnableAppearanceOddRow = true;
    this.siteGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.nameColumn, "nameColumn");
    this.nameColumn.FieldName = "Name";
    this.nameColumn.Name = "nameColumn";
    componentResourceManager.ApplyResources((object) this.descriptionColumn, "descriptionColumn");
    this.descriptionColumn.FieldName = "Description";
    this.descriptionColumn.Name = "descriptionColumn";
    componentResourceManager.ApplyResources((object) this.codeColumn, "codeColumn");
    this.codeColumn.FieldName = "Code";
    this.codeColumn.Name = "codeColumn";
    componentResourceManager.ApplyResources((object) this.siteNameTextEdit, "siteNameTextEdit");
    this.siteNameTextEdit.EnterMoveNextControl = true;
    this.siteNameTextEdit.FormatString = (string) null;
    this.siteNameTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.siteNameTextEdit.IsMandatory = true;
    this.siteNameTextEdit.IsReadOnly = false;
    this.siteNameTextEdit.IsUndoing = false;
    this.siteNameTextEdit.Name = "siteNameTextEdit";
    this.siteNameTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("siteNameTextEdit.Properties.AccessibleDescription");
    this.siteNameTextEdit.Properties.AccessibleName = componentResourceManager.GetString("siteNameTextEdit.Properties.AccessibleName");
    this.siteNameTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.siteNameTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.siteNameTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.siteNameTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.siteNameTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.AutoHeight");
    this.siteNameTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.siteNameTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.AutoComplete");
    this.siteNameTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.BeepOnError");
    this.siteNameTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("siteNameTextEdit.Properties.Mask.EditMask");
    this.siteNameTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.siteNameTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.MaskType");
    this.siteNameTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.PlaceHolder");
    this.siteNameTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.SaveLiteral");
    this.siteNameTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.ShowPlaceHolders");
    this.siteNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.siteNameTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("siteNameTextEdit.Properties.NullValuePrompt");
    this.siteNameTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("siteNameTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.siteNameTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.siteNameTextEdit.Validator = (ICustomValidator) null;
    this.siteNameTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.layoutControl, "layoutControl");
    this.layoutControl.Controls.Add((Control) this.siteCodeTextEdit);
    this.layoutControl.Controls.Add((Control) this.siteNameTextEdit);
    this.layoutControl.Controls.Add((Control) this.siteDescriptionTextEdit);
    this.layoutControl.Controls.Add((Control) this.siteGrid);
    this.layoutControl.Name = "layoutControl";
    this.layoutControl.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.siteCodeTextEdit, "siteCodeTextEdit");
    this.siteCodeTextEdit.EnterMoveNextControl = true;
    this.siteCodeTextEdit.FormatString = (string) null;
    this.siteCodeTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.siteCodeTextEdit.IsMandatory = true;
    this.siteCodeTextEdit.IsReadOnly = false;
    this.siteCodeTextEdit.IsUndoing = false;
    this.siteCodeTextEdit.Name = "siteCodeTextEdit";
    this.siteCodeTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("siteCodeTextEdit.Properties.AccessibleDescription");
    this.siteCodeTextEdit.Properties.AccessibleName = componentResourceManager.GetString("siteCodeTextEdit.Properties.AccessibleName");
    this.siteCodeTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.siteCodeTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.siteCodeTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.siteCodeTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.siteCodeTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.AutoHeight");
    this.siteCodeTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.siteCodeTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.AutoComplete");
    this.siteCodeTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.BeepOnError");
    this.siteCodeTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("siteCodeTextEdit.Properties.Mask.EditMask");
    this.siteCodeTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.siteCodeTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.MaskType");
    this.siteCodeTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.PlaceHolder");
    this.siteCodeTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.SaveLiteral");
    this.siteCodeTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.ShowPlaceHolders");
    this.siteCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.siteCodeTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("siteCodeTextEdit.Properties.NullValuePrompt");
    this.siteCodeTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("siteCodeTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.siteCodeTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.siteCodeTextEdit.Validator = (ICustomValidator) null;
    this.siteCodeTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.siteDescriptionTextEdit, "siteDescriptionTextEdit");
    this.siteDescriptionTextEdit.EnterMoveNextControl = true;
    this.siteDescriptionTextEdit.FormatString = (string) null;
    this.siteDescriptionTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.siteDescriptionTextEdit.IsReadOnly = false;
    this.siteDescriptionTextEdit.IsUndoing = false;
    this.siteDescriptionTextEdit.Name = "siteDescriptionTextEdit";
    this.siteDescriptionTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("siteDescriptionTextEdit.Properties.AccessibleDescription");
    this.siteDescriptionTextEdit.Properties.AccessibleName = componentResourceManager.GetString("siteDescriptionTextEdit.Properties.AccessibleName");
    this.siteDescriptionTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.siteDescriptionTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.siteDescriptionTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.AutoHeight");
    this.siteDescriptionTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.siteDescriptionTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.AutoComplete");
    this.siteDescriptionTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.BeepOnError");
    this.siteDescriptionTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("siteDescriptionTextEdit.Properties.Mask.EditMask");
    this.siteDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.siteDescriptionTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.MaskType");
    this.siteDescriptionTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.PlaceHolder");
    this.siteDescriptionTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.SaveLiteral");
    this.siteDescriptionTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.ShowPlaceHolders");
    this.siteDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.siteDescriptionTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("siteDescriptionTextEdit.Properties.NullValuePrompt");
    this.siteDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("siteDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.siteDescriptionTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.siteDescriptionTextEdit.Validator = (ICustomValidator) null;
    this.siteDescriptionTextEdit.Value = (object) "";
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlGroup2,
      (BaseLayoutItem) this.layoutGroupSites
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3
    });
    this.layoutControlGroup2.Location = new Point(501, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(439, 730);
    this.layoutControlItem1.Control = (Control) this.siteNameTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(415, 24);
    this.layoutControlItem1.TextSize = new Size(64 /*0x40*/, 13);
    this.layoutControlItem2.Control = (Control) this.siteDescriptionTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 24);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(415, 24);
    this.layoutControlItem2.TextSize = new Size(64 /*0x40*/, 13);
    this.layoutControlItem3.Control = (Control) this.siteCodeTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 48 /*0x30*/);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(415, 638);
    this.layoutControlItem3.TextSize = new Size(64 /*0x40*/, 13);
    componentResourceManager.ApplyResources((object) this.layoutGroupSites, "layoutGroupSites");
    this.layoutGroupSites.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem4
    });
    this.layoutGroupSites.Location = new Point(0, 0);
    this.layoutGroupSites.Name = "layoutGroupSites";
    this.layoutGroupSites.Size = new Size(501, 730);
    this.layoutControlItem4.Control = (Control) this.siteGrid;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 0);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(477, 686);
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl);
    this.Name = nameof (SiteMasterDetailBrowser);
    this.facilityGridView.EndInit();
    this.siteGrid.EndInit();
    this.siteGridView.EndInit();
    this.siteNameTextEdit.Properties.EndInit();
    this.layoutControl.EndInit();
    this.layoutControl.ResumeLayout(false);
    this.siteCodeTextEdit.Properties.EndInit();
    this.siteDescriptionTextEdit.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutGroupSites.EndInit();
    this.layoutControlItem4.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void OnUpdateModelCallBack(ModelChangedEventArgs e);
}
