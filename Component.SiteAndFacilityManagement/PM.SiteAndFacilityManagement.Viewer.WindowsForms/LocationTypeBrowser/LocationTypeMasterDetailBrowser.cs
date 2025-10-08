// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser.LocationTypeMasterDetailBrowser
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
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
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser;

[ToolboxItem(false)]
public sealed class LocationTypeMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private DevExpressSingleSelectionGridViewHelper<LocationType> locationTypeMvcHelper;
  private ModelMapper<LocationType> modelMapper;
  private IContainer components;
  private GridControl locationTypeGrid;
  private GridView locationTypeGridView;
  private DevExpressTextEdit locationTypeNameTextEdit;
  private LayoutControl layoutControl;
  private DevExpressTextEdit locationTypeCodeTextEdit;
  private DevExpressTextEdit locationTypeDescriptionTextEdit;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutControlItem3;
  private GridColumn nameColumn;
  private GridColumn descriptionColumn;
  private GridColumn codeColumn;
  private LayoutControlGroup layoutControlGroup2;
  private DevExpressTextEdit locationDeletedTimeStamp;
  private LayoutControlItem layoutLocationDeletedTimeStamp;
  private LayoutControlGroup layoutGroupLocations;

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new LocationTypeMasterDetailBrowser.OnUpdateModelCallBack(this.OnUpdateModelData), (object) e);
    else
      this.OnUpdateModelData(e);
  }

  private void OnUpdateModelData(ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && e.Type == typeof (LocationType) && !e.IsList)
      this.FillFields(e.ChangedObject as LocationType);
    if (e.ChangeType != ChangeType.ItemDeleted || e.ChangedObject != null)
      return;
    this.FillFields(e.ChangedObject as LocationType);
  }

  public LocationTypeMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.BuildUI();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.HelpMarker = "locations_mgmt_01.html";
  }

  public LocationTypeMasterDetailBrowser(IModel model)
    : this()
  {
    this.locationTypeMvcHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<LocationType>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.locationTypeGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void BuildUI()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.LocationTypeMasterDetailBrowser_NavigationGroupName));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.LocationTypeMasterDetailBrowser_LocationGroupName, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_LocationAdd"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_LocationEdit"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_LocationDelete")));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.LocationTypeMasterDetailBrowser_ModificationGroupName));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.LocationTypeMasterDetailBrowser_HelpGroupName, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void FillFields(LocationType locationType)
  {
    this.modelMapper.SetUIEnabled(locationType != null && this.ViewMode != 0);
    this.modelMapper.CopyModelToUI(locationType);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeModelMapper()
  {
    ModelMapper<LocationType> modelMapper = new ModelMapper<LocationType>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<LocationType, object>>) (lt => lt.Name), (object) this.locationTypeNameTextEdit);
    modelMapper.Add((Expression<Func<LocationType, object>>) (lt => lt.Description), (object) this.locationTypeDescriptionTextEdit);
    modelMapper.Add((Expression<Func<LocationType, object>>) (lt => lt.Code), (object) this.locationTypeCodeTextEdit);
    modelMapper.Add((Expression<Func<LocationType, object>>) (lt => (object) lt.Inactive), (object) this.locationDeletedTimeStamp);
    this.modelMapper = modelMapper;
    this.modelMapper.SetUIEnabledForced(false, (object) this.locationDeletedTimeStamp);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LocationTypeMasterDetailBrowser));
    this.locationTypeGrid = new GridControl();
    this.locationTypeGridView = new GridView();
    this.nameColumn = new GridColumn();
    this.descriptionColumn = new GridColumn();
    this.codeColumn = new GridColumn();
    this.locationTypeNameTextEdit = new DevExpressTextEdit();
    this.layoutControl = new LayoutControl();
    this.locationDeletedTimeStamp = new DevExpressTextEdit();
    this.locationTypeCodeTextEdit = new DevExpressTextEdit();
    this.locationTypeDescriptionTextEdit = new DevExpressTextEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutLocationDeletedTimeStamp = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutGroupLocations = new LayoutControlGroup();
    this.layoutControlItem4 = new LayoutControlItem();
    this.locationTypeGrid.BeginInit();
    this.locationTypeGridView.BeginInit();
    this.locationTypeNameTextEdit.Properties.BeginInit();
    this.layoutControl.BeginInit();
    this.layoutControl.SuspendLayout();
    this.locationDeletedTimeStamp.Properties.BeginInit();
    this.locationTypeCodeTextEdit.Properties.BeginInit();
    this.locationTypeDescriptionTextEdit.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutLocationDeletedTimeStamp.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutGroupLocations.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.locationTypeGrid, "locationTypeGrid");
    this.locationTypeGrid.EmbeddedNavigator.AccessibleDescription = componentResourceManager.GetString("locationTypeGrid.EmbeddedNavigator.AccessibleDescription");
    this.locationTypeGrid.EmbeddedNavigator.AccessibleName = componentResourceManager.GetString("locationTypeGrid.EmbeddedNavigator.AccessibleName");
    this.locationTypeGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = (DefaultBoolean) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.AllowHtmlTextInToolTip");
    this.locationTypeGrid.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.Anchor");
    this.locationTypeGrid.EmbeddedNavigator.BackgroundImage = (Image) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.BackgroundImage");
    this.locationTypeGrid.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.BackgroundImageLayout");
    this.locationTypeGrid.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.ImeMode");
    this.locationTypeGrid.EmbeddedNavigator.TextLocation = (NavigatorButtonsTextLocation) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.TextLocation");
    this.locationTypeGrid.EmbeddedNavigator.ToolTip = componentResourceManager.GetString("locationTypeGrid.EmbeddedNavigator.ToolTip");
    this.locationTypeGrid.EmbeddedNavigator.ToolTipIconType = (ToolTipIconType) componentResourceManager.GetObject("locationTypeGrid.EmbeddedNavigator.ToolTipIconType");
    this.locationTypeGrid.EmbeddedNavigator.ToolTipTitle = componentResourceManager.GetString("locationTypeGrid.EmbeddedNavigator.ToolTipTitle");
    this.locationTypeGrid.MainView = (BaseView) this.locationTypeGridView;
    this.locationTypeGrid.Name = "locationTypeGrid";
    this.locationTypeGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.locationTypeGridView
    });
    componentResourceManager.ApplyResources((object) this.locationTypeGridView, "locationTypeGridView");
    this.locationTypeGridView.Columns.AddRange(new GridColumn[3]
    {
      this.nameColumn,
      this.descriptionColumn,
      this.codeColumn
    });
    this.locationTypeGridView.GridControl = this.locationTypeGrid;
    this.locationTypeGridView.Name = "locationTypeGridView";
    this.locationTypeGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.locationTypeGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.locationTypeGridView.OptionsBehavior.Editable = false;
    this.locationTypeGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.locationTypeGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.locationTypeGridView.OptionsView.EnableAppearanceOddRow = true;
    this.locationTypeGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.nameColumn, "nameColumn");
    this.nameColumn.FieldName = "Name";
    this.nameColumn.Name = "nameColumn";
    componentResourceManager.ApplyResources((object) this.descriptionColumn, "descriptionColumn");
    this.descriptionColumn.FieldName = "Description";
    this.descriptionColumn.Name = "descriptionColumn";
    componentResourceManager.ApplyResources((object) this.codeColumn, "codeColumn");
    this.codeColumn.FieldName = "Code";
    this.codeColumn.Name = "codeColumn";
    componentResourceManager.ApplyResources((object) this.locationTypeNameTextEdit, "locationTypeNameTextEdit");
    this.locationTypeNameTextEdit.EnterMoveNextControl = true;
    this.locationTypeNameTextEdit.FormatString = (string) null;
    this.locationTypeNameTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.locationTypeNameTextEdit.IsMandatory = true;
    this.locationTypeNameTextEdit.IsReadOnly = false;
    this.locationTypeNameTextEdit.IsUndoing = false;
    this.locationTypeNameTextEdit.Name = "locationTypeNameTextEdit";
    this.locationTypeNameTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("locationTypeNameTextEdit.Properties.AccessibleDescription");
    this.locationTypeNameTextEdit.Properties.AccessibleName = componentResourceManager.GetString("locationTypeNameTextEdit.Properties.AccessibleName");
    this.locationTypeNameTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.locationTypeNameTextEdit.Properties.Appearance.BorderColor = Color.LightGray;
    this.locationTypeNameTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.locationTypeNameTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.locationTypeNameTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.AutoHeight");
    this.locationTypeNameTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.locationTypeNameTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.AutoComplete");
    this.locationTypeNameTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.BeepOnError");
    this.locationTypeNameTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("locationTypeNameTextEdit.Properties.Mask.EditMask");
    this.locationTypeNameTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.locationTypeNameTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.MaskType");
    this.locationTypeNameTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.PlaceHolder");
    this.locationTypeNameTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.SaveLiteral");
    this.locationTypeNameTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.ShowPlaceHolders");
    this.locationTypeNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.locationTypeNameTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("locationTypeNameTextEdit.Properties.NullValuePrompt");
    this.locationTypeNameTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("locationTypeNameTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.locationTypeNameTextEdit.ShowModified = false;
    this.locationTypeNameTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.locationTypeNameTextEdit.Validator = (ICustomValidator) null;
    this.locationTypeNameTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.layoutControl, "layoutControl");
    this.layoutControl.Controls.Add((Control) this.locationDeletedTimeStamp);
    this.layoutControl.Controls.Add((Control) this.locationTypeCodeTextEdit);
    this.layoutControl.Controls.Add((Control) this.locationTypeNameTextEdit);
    this.layoutControl.Controls.Add((Control) this.locationTypeDescriptionTextEdit);
    this.layoutControl.Controls.Add((Control) this.locationTypeGrid);
    this.layoutControl.Name = "layoutControl";
    this.layoutControl.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.locationDeletedTimeStamp, "locationDeletedTimeStamp");
    this.locationDeletedTimeStamp.EnterMoveNextControl = true;
    this.locationDeletedTimeStamp.FormatString = (string) null;
    this.locationDeletedTimeStamp.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.locationDeletedTimeStamp.IsReadOnly = false;
    this.locationDeletedTimeStamp.IsUndoing = false;
    this.locationDeletedTimeStamp.Name = "locationDeletedTimeStamp";
    this.locationDeletedTimeStamp.Properties.AccessibleDescription = componentResourceManager.GetString("locationDeletedTimeStamp.Properties.AccessibleDescription");
    this.locationDeletedTimeStamp.Properties.AccessibleName = componentResourceManager.GetString("locationDeletedTimeStamp.Properties.AccessibleName");
    this.locationDeletedTimeStamp.Properties.Appearance.BorderColor = Color.Yellow;
    this.locationDeletedTimeStamp.Properties.Appearance.Options.UseBorderColor = true;
    this.locationDeletedTimeStamp.Properties.AutoHeight = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.AutoHeight");
    this.locationDeletedTimeStamp.Properties.BorderStyle = BorderStyles.Simple;
    this.locationDeletedTimeStamp.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.AutoComplete");
    this.locationDeletedTimeStamp.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.BeepOnError");
    this.locationDeletedTimeStamp.Properties.Mask.EditMask = componentResourceManager.GetString("locationDeletedTimeStamp.Properties.Mask.EditMask");
    this.locationDeletedTimeStamp.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.IgnoreMaskBlank");
    this.locationDeletedTimeStamp.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.MaskType");
    this.locationDeletedTimeStamp.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.PlaceHolder");
    this.locationDeletedTimeStamp.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.SaveLiteral");
    this.locationDeletedTimeStamp.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.ShowPlaceHolders");
    this.locationDeletedTimeStamp.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.Mask.UseMaskAsDisplayFormat");
    this.locationDeletedTimeStamp.Properties.NullValuePrompt = componentResourceManager.GetString("locationDeletedTimeStamp.Properties.NullValuePrompt");
    this.locationDeletedTimeStamp.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("locationDeletedTimeStamp.Properties.NullValuePromptShowForEmptyValue");
    this.locationDeletedTimeStamp.StyleController = (IStyleController) this.layoutControl;
    this.locationDeletedTimeStamp.Validator = (ICustomValidator) null;
    this.locationDeletedTimeStamp.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.locationTypeCodeTextEdit, "locationTypeCodeTextEdit");
    this.locationTypeCodeTextEdit.EnterMoveNextControl = true;
    this.locationTypeCodeTextEdit.FormatString = (string) null;
    this.locationTypeCodeTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.locationTypeCodeTextEdit.IsMandatory = true;
    this.locationTypeCodeTextEdit.IsReadOnly = false;
    this.locationTypeCodeTextEdit.IsUndoing = false;
    this.locationTypeCodeTextEdit.Name = "locationTypeCodeTextEdit";
    this.locationTypeCodeTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("locationTypeCodeTextEdit.Properties.AccessibleDescription");
    this.locationTypeCodeTextEdit.Properties.AccessibleName = componentResourceManager.GetString("locationTypeCodeTextEdit.Properties.AccessibleName");
    this.locationTypeCodeTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.locationTypeCodeTextEdit.Properties.Appearance.BorderColor = Color.LightGray;
    this.locationTypeCodeTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.locationTypeCodeTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.locationTypeCodeTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.AutoHeight");
    this.locationTypeCodeTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.locationTypeCodeTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.AutoComplete");
    this.locationTypeCodeTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.BeepOnError");
    this.locationTypeCodeTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("locationTypeCodeTextEdit.Properties.Mask.EditMask");
    this.locationTypeCodeTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.locationTypeCodeTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.MaskType");
    this.locationTypeCodeTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.PlaceHolder");
    this.locationTypeCodeTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.SaveLiteral");
    this.locationTypeCodeTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.ShowPlaceHolders");
    this.locationTypeCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.locationTypeCodeTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("locationTypeCodeTextEdit.Properties.NullValuePrompt");
    this.locationTypeCodeTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("locationTypeCodeTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.locationTypeCodeTextEdit.ShowModified = false;
    this.locationTypeCodeTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.locationTypeCodeTextEdit.Validator = (ICustomValidator) null;
    this.locationTypeCodeTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.locationTypeDescriptionTextEdit, "locationTypeDescriptionTextEdit");
    this.locationTypeDescriptionTextEdit.EnterMoveNextControl = true;
    this.locationTypeDescriptionTextEdit.FormatString = (string) null;
    this.locationTypeDescriptionTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.locationTypeDescriptionTextEdit.IsReadOnly = false;
    this.locationTypeDescriptionTextEdit.IsUndoing = false;
    this.locationTypeDescriptionTextEdit.Name = "locationTypeDescriptionTextEdit";
    this.locationTypeDescriptionTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("locationTypeDescriptionTextEdit.Properties.AccessibleDescription");
    this.locationTypeDescriptionTextEdit.Properties.AccessibleName = componentResourceManager.GetString("locationTypeDescriptionTextEdit.Properties.AccessibleName");
    this.locationTypeDescriptionTextEdit.Properties.Appearance.BorderColor = Color.LightGray;
    this.locationTypeDescriptionTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.locationTypeDescriptionTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.AutoHeight");
    this.locationTypeDescriptionTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.locationTypeDescriptionTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.AutoComplete");
    this.locationTypeDescriptionTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.BeepOnError");
    this.locationTypeDescriptionTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("locationTypeDescriptionTextEdit.Properties.Mask.EditMask");
    this.locationTypeDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.locationTypeDescriptionTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.MaskType");
    this.locationTypeDescriptionTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.PlaceHolder");
    this.locationTypeDescriptionTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.SaveLiteral");
    this.locationTypeDescriptionTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.ShowPlaceHolders");
    this.locationTypeDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.locationTypeDescriptionTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("locationTypeDescriptionTextEdit.Properties.NullValuePrompt");
    this.locationTypeDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("locationTypeDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.locationTypeDescriptionTextEdit.ShowModified = false;
    this.locationTypeDescriptionTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.locationTypeDescriptionTextEdit.Validator = (ICustomValidator) null;
    this.locationTypeDescriptionTextEdit.Value = (object) "";
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlGroup2,
      (BaseLayoutItem) this.layoutGroupLocations
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutLocationDeletedTimeStamp,
      (BaseLayoutItem) this.layoutControlItem3
    });
    this.layoutControlGroup2.Location = new Point(501, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(439, 730);
    this.layoutControlItem1.Control = (Control) this.locationTypeNameTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(415, 24);
    this.layoutControlItem1.TextSize = new Size(64 /*0x40*/, 13);
    this.layoutControlItem2.Control = (Control) this.locationTypeDescriptionTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 24);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(415, 24);
    this.layoutControlItem2.TextSize = new Size(64 /*0x40*/, 13);
    this.layoutLocationDeletedTimeStamp.Control = (Control) this.locationDeletedTimeStamp;
    componentResourceManager.ApplyResources((object) this.layoutLocationDeletedTimeStamp, "layoutLocationDeletedTimeStamp");
    this.layoutLocationDeletedTimeStamp.Location = new Point(0, 72);
    this.layoutLocationDeletedTimeStamp.Name = "layoutLocationDeletedTimeStamp";
    this.layoutLocationDeletedTimeStamp.Size = new Size(415, 614);
    this.layoutLocationDeletedTimeStamp.TextSize = new Size(64 /*0x40*/, 13);
    this.layoutLocationDeletedTimeStamp.Visibility = LayoutVisibility.Never;
    this.layoutControlItem3.Control = (Control) this.locationTypeCodeTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 48 /*0x30*/);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(415, 24);
    this.layoutControlItem3.TextSize = new Size(64 /*0x40*/, 13);
    componentResourceManager.ApplyResources((object) this.layoutGroupLocations, "layoutGroupLocations");
    this.layoutGroupLocations.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem4
    });
    this.layoutGroupLocations.Location = new Point(0, 0);
    this.layoutGroupLocations.Name = "layoutGroupLocations";
    this.layoutGroupLocations.Size = new Size(501, 730);
    this.layoutControlItem4.Control = (Control) this.locationTypeGrid;
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
    this.Name = nameof (LocationTypeMasterDetailBrowser);
    this.locationTypeGrid.EndInit();
    this.locationTypeGridView.EndInit();
    this.locationTypeNameTextEdit.Properties.EndInit();
    this.layoutControl.EndInit();
    this.layoutControl.ResumeLayout(false);
    this.locationDeletedTimeStamp.Properties.EndInit();
    this.locationTypeCodeTextEdit.Properties.EndInit();
    this.locationTypeDescriptionTextEdit.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutLocationDeletedTimeStamp.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutGroupLocations.EndInit();
    this.layoutControlItem4.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void OnUpdateModelCallBack(ModelChangedEventArgs e);
}
