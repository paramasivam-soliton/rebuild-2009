// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser.FacilityMasterDetailBrowser
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
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser;

[ToolboxItem(false)]
public sealed class FacilityMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressSingleSelectionGridViewHelper<Facility> facilityMvcHelper;
  private ModelMapper<Facility> modelMapper;
  private IContainer components;
  private GridControl facilityGrid;
  private GridView facilityGridView;
  private DevExpressTextEdit facilityNameTextEdit;
  private LayoutControl layoutControl;
  private DevExpressTextEdit facilityCodeTextEdit;
  private DevExpressTextEdit facilityDescriptionTextEdit;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutControlItem3;
  private GridColumn nameColumn;
  private GridColumn descriptionColumn;
  private GridColumn codeColumn;
  private LayoutControlGroup layoutControlGroup2;
  private DevExpressComboBoxEdit siteComboBoxEdit;
  private LayoutControlItem layoutControlItem5;
  private DevExpressCheckedComboBoxEdit locationTypesCheckedComboBoxEdit;
  private LayoutControlItem layoutControlItem6;
  private LayoutControlGroup layoutGroupFacility;

  public FacilityMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.HelpMarker = "facility_mgmt_01.html";
  }

  public FacilityMasterDetailBrowser(IModel model)
    : this()
  {
    this.facilityMvcHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<Facility>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.facilityGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.FacilityMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.FacilityMasterDetailBrowser_RibbonMaintenanceGroup, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_FacilityAdd") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_FacilityEdit") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_FacilityDelete") as Bitmap));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.FacilityMasterDetailBrowser_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.FacilityMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new FacilityMasterDetailBrowser.OnUpdateModelCallBack(this.OnUpdateModelData), (object) e);
    else
      this.OnUpdateModelData(e);
  }

  private void OnUpdateModelData(ModelChangedEventArgs e)
  {
    if (e.ChangeType == ChangeType.ListLoaded && e.ChangedObject is IEnumerable<PathMedical.SiteAndFacilityManagement.Site>)
      this.siteComboBoxEdit.DataSource = (object) (e.ChangedObject as IEnumerable<PathMedical.SiteAndFacilityManagement.Site>).Select<PathMedical.SiteAndFacilityManagement.Site, ComboBoxEditItemWrapper>((Func<PathMedical.SiteAndFacilityManagement.Site, ComboBoxEditItemWrapper>) (s => new ComboBoxEditItemWrapper(s.Name, (object) s))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
    if (e.ChangeType == ChangeType.ListLoaded && e.ChangedObject is IEnumerable<LocationType>)
      this.locationTypesCheckedComboBoxEdit.DataSource = (object) (e.ChangedObject as IEnumerable<LocationType>).OrderBy<LocationType, string>((Func<LocationType, string>) (lt => lt.Name)).ToList<LocationType>();
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && e.Type == typeof (Facility) && !e.IsList)
      this.FillFields(e.ChangedObject as Facility);
    if (e.ChangeType != ChangeType.ItemDeleted || e.ChangedObject != null)
      return;
    this.FillFields(e.ChangedObject as Facility);
  }

  private void FillFields(Facility facility)
  {
    this.modelMapper.SetUIEnabled(facility != null && this.ViewMode != 0);
    this.modelMapper.CopyModelToUI(facility);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeModelMapper()
  {
    ModelMapper<Facility> modelMapper = new ModelMapper<Facility>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<Facility, object>>) (f => f.Name), (object) this.facilityNameTextEdit);
    modelMapper.Add((Expression<Func<Facility, object>>) (f => f.Description), (object) this.facilityDescriptionTextEdit);
    modelMapper.Add((Expression<Func<Facility, object>>) (f => f.Code), (object) this.facilityCodeTextEdit);
    modelMapper.Add((Expression<Func<Facility, object>>) (f => f.Site), (object) this.siteComboBoxEdit);
    modelMapper.Add((Expression<Func<Facility, object>>) (f => f.LocationTypes), (object) this.locationTypesCheckedComboBoxEdit);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FacilityMasterDetailBrowser));
    this.facilityGrid = new GridControl();
    this.facilityGridView = new GridView();
    this.nameColumn = new GridColumn();
    this.descriptionColumn = new GridColumn();
    this.codeColumn = new GridColumn();
    this.facilityNameTextEdit = new DevExpressTextEdit();
    this.layoutControl = new LayoutControl();
    this.locationTypesCheckedComboBoxEdit = new DevExpressCheckedComboBoxEdit();
    this.facilityCodeTextEdit = new DevExpressTextEdit();
    this.facilityDescriptionTextEdit = new DevExpressTextEdit();
    this.siteComboBoxEdit = new DevExpressComboBoxEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutGroupFacility = new LayoutControlGroup();
    this.layoutControlItem4 = new LayoutControlItem();
    this.facilityGrid.BeginInit();
    this.facilityGridView.BeginInit();
    this.facilityNameTextEdit.Properties.BeginInit();
    this.layoutControl.BeginInit();
    this.layoutControl.SuspendLayout();
    this.locationTypesCheckedComboBoxEdit.Properties.BeginInit();
    this.facilityCodeTextEdit.Properties.BeginInit();
    this.facilityDescriptionTextEdit.Properties.BeginInit();
    this.siteComboBoxEdit.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutGroupFacility.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.facilityGrid, "facilityGrid");
    this.facilityGrid.EmbeddedNavigator.AccessibleDescription = componentResourceManager.GetString("facilityGrid.EmbeddedNavigator.AccessibleDescription");
    this.facilityGrid.EmbeddedNavigator.AccessibleName = componentResourceManager.GetString("facilityGrid.EmbeddedNavigator.AccessibleName");
    this.facilityGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = (DefaultBoolean) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.AllowHtmlTextInToolTip");
    this.facilityGrid.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.Anchor");
    this.facilityGrid.EmbeddedNavigator.BackgroundImage = (Image) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.BackgroundImage");
    this.facilityGrid.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.BackgroundImageLayout");
    this.facilityGrid.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.ImeMode");
    this.facilityGrid.EmbeddedNavigator.TextLocation = (NavigatorButtonsTextLocation) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.TextLocation");
    this.facilityGrid.EmbeddedNavigator.ToolTip = componentResourceManager.GetString("facilityGrid.EmbeddedNavigator.ToolTip");
    this.facilityGrid.EmbeddedNavigator.ToolTipIconType = (ToolTipIconType) componentResourceManager.GetObject("facilityGrid.EmbeddedNavigator.ToolTipIconType");
    this.facilityGrid.EmbeddedNavigator.ToolTipTitle = componentResourceManager.GetString("facilityGrid.EmbeddedNavigator.ToolTipTitle");
    this.facilityGrid.MainView = (BaseView) this.facilityGridView;
    this.facilityGrid.Name = "facilityGrid";
    this.facilityGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.facilityGridView
    });
    componentResourceManager.ApplyResources((object) this.facilityGridView, "facilityGridView");
    this.facilityGridView.Columns.AddRange(new GridColumn[3]
    {
      this.nameColumn,
      this.descriptionColumn,
      this.codeColumn
    });
    this.facilityGridView.GridControl = this.facilityGrid;
    this.facilityGridView.Name = "facilityGridView";
    this.facilityGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.facilityGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.facilityGridView.OptionsBehavior.Editable = false;
    this.facilityGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.facilityGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.facilityGridView.OptionsView.EnableAppearanceOddRow = true;
    this.facilityGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.nameColumn, "nameColumn");
    this.nameColumn.FieldName = "Name";
    this.nameColumn.Name = "nameColumn";
    componentResourceManager.ApplyResources((object) this.descriptionColumn, "descriptionColumn");
    this.descriptionColumn.FieldName = "Description";
    this.descriptionColumn.Name = "descriptionColumn";
    componentResourceManager.ApplyResources((object) this.codeColumn, "codeColumn");
    this.codeColumn.FieldName = "Code";
    this.codeColumn.Name = "codeColumn";
    componentResourceManager.ApplyResources((object) this.facilityNameTextEdit, "facilityNameTextEdit");
    this.facilityNameTextEdit.EnterMoveNextControl = true;
    this.facilityNameTextEdit.FormatString = (string) null;
    this.facilityNameTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.facilityNameTextEdit.IsMandatory = true;
    this.facilityNameTextEdit.IsReadOnly = false;
    this.facilityNameTextEdit.IsUndoing = false;
    this.facilityNameTextEdit.Name = "facilityNameTextEdit";
    this.facilityNameTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("facilityNameTextEdit.Properties.AccessibleDescription");
    this.facilityNameTextEdit.Properties.AccessibleName = componentResourceManager.GetString("facilityNameTextEdit.Properties.AccessibleName");
    this.facilityNameTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.facilityNameTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.facilityNameTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.facilityNameTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.facilityNameTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.AutoHeight");
    this.facilityNameTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.facilityNameTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.AutoComplete");
    this.facilityNameTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.BeepOnError");
    this.facilityNameTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("facilityNameTextEdit.Properties.Mask.EditMask");
    this.facilityNameTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.facilityNameTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.MaskType");
    this.facilityNameTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.PlaceHolder");
    this.facilityNameTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.SaveLiteral");
    this.facilityNameTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.ShowPlaceHolders");
    this.facilityNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.facilityNameTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("facilityNameTextEdit.Properties.NullValuePrompt");
    this.facilityNameTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("facilityNameTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.facilityNameTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.facilityNameTextEdit.Validator = (ICustomValidator) null;
    this.facilityNameTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.layoutControl, "layoutControl");
    this.layoutControl.Controls.Add((Control) this.locationTypesCheckedComboBoxEdit);
    this.layoutControl.Controls.Add((Control) this.facilityCodeTextEdit);
    this.layoutControl.Controls.Add((Control) this.facilityNameTextEdit);
    this.layoutControl.Controls.Add((Control) this.facilityDescriptionTextEdit);
    this.layoutControl.Controls.Add((Control) this.siteComboBoxEdit);
    this.layoutControl.Controls.Add((Control) this.facilityGrid);
    this.layoutControl.Name = "layoutControl";
    this.layoutControl.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.locationTypesCheckedComboBoxEdit, "locationTypesCheckedComboBoxEdit");
    this.locationTypesCheckedComboBoxEdit.EnterMoveNextControl = true;
    this.locationTypesCheckedComboBoxEdit.FormatString = (string) null;
    this.locationTypesCheckedComboBoxEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.locationTypesCheckedComboBoxEdit.IsReadOnly = false;
    this.locationTypesCheckedComboBoxEdit.ItemDescriptionProperty = "Name";
    this.locationTypesCheckedComboBoxEdit.Name = "locationTypesCheckedComboBoxEdit";
    this.locationTypesCheckedComboBoxEdit.Properties.AccessibleDescription = componentResourceManager.GetString("locationTypesCheckedComboBoxEdit.Properties.AccessibleDescription");
    this.locationTypesCheckedComboBoxEdit.Properties.AccessibleName = componentResourceManager.GetString("locationTypesCheckedComboBoxEdit.Properties.AccessibleName");
    this.locationTypesCheckedComboBoxEdit.Properties.Appearance.BorderColor = Color.LightGray;
    this.locationTypesCheckedComboBoxEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.locationTypesCheckedComboBoxEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.AutoHeight");
    this.locationTypesCheckedComboBoxEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.locationTypesCheckedComboBoxEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Buttons"))
    });
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.AutoComplete");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.BeepOnError");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.EditMask = componentResourceManager.GetString("locationTypesCheckedComboBoxEdit.Properties.Mask.EditMask");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.IgnoreMaskBlank");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.MaskType");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.PlaceHolder");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.SaveLiteral");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.ShowPlaceHolders");
    this.locationTypesCheckedComboBoxEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.locationTypesCheckedComboBoxEdit.Properties.NullValuePrompt = componentResourceManager.GetString("locationTypesCheckedComboBoxEdit.Properties.NullValuePrompt");
    this.locationTypesCheckedComboBoxEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Properties.NullValuePromptShowForEmptyValue");
    this.locationTypesCheckedComboBoxEdit.Properties.SelectAllItemVisible = false;
    this.locationTypesCheckedComboBoxEdit.Properties.ShowButtons = false;
    this.locationTypesCheckedComboBoxEdit.Properties.ShowPopupCloseButton = false;
    this.locationTypesCheckedComboBoxEdit.SelectedValues = (ICollection) componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.SelectedValues");
    this.locationTypesCheckedComboBoxEdit.StyleController = (IStyleController) this.layoutControl;
    this.locationTypesCheckedComboBoxEdit.Validator = (ICustomValidator) null;
    this.locationTypesCheckedComboBoxEdit.Value = componentResourceManager.GetObject("locationTypesCheckedComboBoxEdit.Value");
    componentResourceManager.ApplyResources((object) this.facilityCodeTextEdit, "facilityCodeTextEdit");
    this.facilityCodeTextEdit.EnterMoveNextControl = true;
    this.facilityCodeTextEdit.FormatString = (string) null;
    this.facilityCodeTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.facilityCodeTextEdit.IsMandatory = true;
    this.facilityCodeTextEdit.IsReadOnly = false;
    this.facilityCodeTextEdit.IsUndoing = false;
    this.facilityCodeTextEdit.Name = "facilityCodeTextEdit";
    this.facilityCodeTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("facilityCodeTextEdit.Properties.AccessibleDescription");
    this.facilityCodeTextEdit.Properties.AccessibleName = componentResourceManager.GetString("facilityCodeTextEdit.Properties.AccessibleName");
    this.facilityCodeTextEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.facilityCodeTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.facilityCodeTextEdit.Properties.Appearance.Options.UseBackColor = true;
    this.facilityCodeTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.facilityCodeTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.AutoHeight");
    this.facilityCodeTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.facilityCodeTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.AutoComplete");
    this.facilityCodeTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.BeepOnError");
    this.facilityCodeTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("facilityCodeTextEdit.Properties.Mask.EditMask");
    this.facilityCodeTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.facilityCodeTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.MaskType");
    this.facilityCodeTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.PlaceHolder");
    this.facilityCodeTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.SaveLiteral");
    this.facilityCodeTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.ShowPlaceHolders");
    this.facilityCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.facilityCodeTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("facilityCodeTextEdit.Properties.NullValuePrompt");
    this.facilityCodeTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("facilityCodeTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.facilityCodeTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.facilityCodeTextEdit.Validator = (ICustomValidator) null;
    this.facilityCodeTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.facilityDescriptionTextEdit, "facilityDescriptionTextEdit");
    this.facilityDescriptionTextEdit.EnterMoveNextControl = true;
    this.facilityDescriptionTextEdit.FormatString = (string) null;
    this.facilityDescriptionTextEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.facilityDescriptionTextEdit.IsReadOnly = false;
    this.facilityDescriptionTextEdit.IsUndoing = false;
    this.facilityDescriptionTextEdit.Name = "facilityDescriptionTextEdit";
    this.facilityDescriptionTextEdit.Properties.AccessibleDescription = componentResourceManager.GetString("facilityDescriptionTextEdit.Properties.AccessibleDescription");
    this.facilityDescriptionTextEdit.Properties.AccessibleName = componentResourceManager.GetString("facilityDescriptionTextEdit.Properties.AccessibleName");
    this.facilityDescriptionTextEdit.Properties.Appearance.BorderColor = Color.Yellow;
    this.facilityDescriptionTextEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.facilityDescriptionTextEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.AutoHeight");
    this.facilityDescriptionTextEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.facilityDescriptionTextEdit.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.AutoComplete");
    this.facilityDescriptionTextEdit.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.BeepOnError");
    this.facilityDescriptionTextEdit.Properties.Mask.EditMask = componentResourceManager.GetString("facilityDescriptionTextEdit.Properties.Mask.EditMask");
    this.facilityDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.IgnoreMaskBlank");
    this.facilityDescriptionTextEdit.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.MaskType");
    this.facilityDescriptionTextEdit.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.PlaceHolder");
    this.facilityDescriptionTextEdit.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.SaveLiteral");
    this.facilityDescriptionTextEdit.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.ShowPlaceHolders");
    this.facilityDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.Mask.UseMaskAsDisplayFormat");
    this.facilityDescriptionTextEdit.Properties.NullValuePrompt = componentResourceManager.GetString("facilityDescriptionTextEdit.Properties.NullValuePrompt");
    this.facilityDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("facilityDescriptionTextEdit.Properties.NullValuePromptShowForEmptyValue");
    this.facilityDescriptionTextEdit.StyleController = (IStyleController) this.layoutControl;
    this.facilityDescriptionTextEdit.Validator = (ICustomValidator) null;
    this.facilityDescriptionTextEdit.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.siteComboBoxEdit, "siteComboBoxEdit");
    this.siteComboBoxEdit.EnterMoveNextControl = true;
    this.siteComboBoxEdit.FormatString = (string) null;
    this.siteComboBoxEdit.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.siteComboBoxEdit.IsMandatory = true;
    this.siteComboBoxEdit.IsReadOnly = false;
    this.siteComboBoxEdit.IsUndoing = false;
    this.siteComboBoxEdit.Name = "siteComboBoxEdit";
    this.siteComboBoxEdit.Properties.AccessibleDescription = componentResourceManager.GetString("siteComboBoxEdit.Properties.AccessibleDescription");
    this.siteComboBoxEdit.Properties.AccessibleName = componentResourceManager.GetString("siteComboBoxEdit.Properties.AccessibleName");
    this.siteComboBoxEdit.Properties.Appearance.BackColor = Color.LightYellow;
    this.siteComboBoxEdit.Properties.Appearance.BorderColor = Color.LightGray;
    this.siteComboBoxEdit.Properties.Appearance.Options.UseBackColor = true;
    this.siteComboBoxEdit.Properties.Appearance.Options.UseBorderColor = true;
    this.siteComboBoxEdit.Properties.AutoHeight = (bool) componentResourceManager.GetObject("siteComboBoxEdit.Properties.AutoHeight");
    this.siteComboBoxEdit.Properties.BorderStyle = BorderStyles.Simple;
    this.siteComboBoxEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("siteComboBoxEdit.Properties.Buttons"))
    });
    this.siteComboBoxEdit.Properties.NullValuePrompt = componentResourceManager.GetString("siteComboBoxEdit.Properties.NullValuePrompt");
    this.siteComboBoxEdit.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("siteComboBoxEdit.Properties.NullValuePromptShowForEmptyValue");
    this.siteComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.siteComboBoxEdit.ShowEmptyElement = true;
    this.siteComboBoxEdit.StyleController = (IStyleController) this.layoutControl;
    this.siteComboBoxEdit.Validator = (ICustomValidator) null;
    this.siteComboBoxEdit.Value = (object) null;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlGroup2,
      (BaseLayoutItem) this.layoutGroupFacility
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem6,
      (BaseLayoutItem) this.layoutControlItem5
    });
    this.layoutControlGroup2.Location = new Point(501, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(439, 730);
    this.layoutControlItem1.Control = (Control) this.facilityNameTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(415, 24);
    this.layoutControlItem1.TextSize = new Size(69, 13);
    this.layoutControlItem2.Control = (Control) this.facilityDescriptionTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 24);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(415, 24);
    this.layoutControlItem2.TextSize = new Size(69, 13);
    this.layoutControlItem3.Control = (Control) this.facilityCodeTextEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 48 /*0x30*/);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(415, 24);
    this.layoutControlItem3.TextSize = new Size(69, 13);
    this.layoutControlItem6.Control = (Control) this.locationTypesCheckedComboBoxEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(0, 96 /*0x60*/);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(415, 590);
    this.layoutControlItem6.TextSize = new Size(69, 13);
    this.layoutControlItem5.Control = (Control) this.siteComboBoxEdit;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(0, 72);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(415, 24);
    this.layoutControlItem5.TextSize = new Size(69, 13);
    componentResourceManager.ApplyResources((object) this.layoutGroupFacility, "layoutGroupFacility");
    this.layoutGroupFacility.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem4
    });
    this.layoutGroupFacility.Location = new Point(0, 0);
    this.layoutGroupFacility.Name = "layoutGroupFacility";
    this.layoutGroupFacility.Size = new Size(501, 730);
    this.layoutControlItem4.Control = (Control) this.facilityGrid;
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
    this.Name = nameof (FacilityMasterDetailBrowser);
    this.facilityGrid.EndInit();
    this.facilityGridView.EndInit();
    this.facilityNameTextEdit.Properties.EndInit();
    this.layoutControl.EndInit();
    this.layoutControl.ResumeLayout(false);
    this.locationTypesCheckedComboBoxEdit.Properties.EndInit();
    this.facilityCodeTextEdit.Properties.EndInit();
    this.facilityDescriptionTextEdit.Properties.EndInit();
    this.siteComboBoxEdit.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutGroupFacility.EndInit();
    this.layoutControlItem4.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void OnUpdateModelCallBack(ModelChangedEventArgs e);
}
