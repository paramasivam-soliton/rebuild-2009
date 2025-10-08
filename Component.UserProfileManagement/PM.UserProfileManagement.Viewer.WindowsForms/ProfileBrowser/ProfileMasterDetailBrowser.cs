// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser.ProfileMasterDetailBrowser
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using DevExpress.Data;
using DevExpress.Utils;
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
using PathMedical.ResourceManager;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser;

[ToolboxItem(false)]
public sealed class ProfileMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private bool isPermissionFieldsFilling;
  private ModelMapper<AccessPermission> permissionModelMapper;
  private DevExpressSingleSelectionGridViewHelper<AccessPermission> permissionMvcHelper;
  private ModelMapper<UserProfile> profileModelMapper;
  private DevExpressSingleSelectionGridViewHelper<UserProfile> profileMvcHelper;
  private IContainer components;
  private GridControl profileGridControl;
  private GridView profileGridView;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem profileGridLayoutControlItem;
  private DevExpressMemoEdit profileDescription;
  private DevExpressTextEdit profileName;
  private LayoutControlGroup profileControlGroup;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem2;
  private DevExpressTextEdit permissionName;
  private GridControl permissionGridControl;
  private GridView permissionGridView;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem4;
  private DevExpressMemoEdit permissionDescription;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlGroup permissionControlGroup;
  private GridColumn profileNamegridColumn;
  private GridColumn profileDescriptionGridColumn;
  private GridColumn componentNameGridColumn;
  private GridColumn nameGridColumn;
  private GridColumn flagGridColumn;
  private RepositoryItemCheckEdit flagCheckEdit;
  private LayoutControlGroup layoutControlGroup2;

  public ProfileMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.BuildUI();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.HelpMarker = "user_profile_mgmt_01.html";
  }

  public ProfileMasterDetailBrowser(IModel model)
    : this()
  {
    this.profileMvcHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<UserProfile>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.profileGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.permissionMvcHelper = new DevExpressSingleSelectionGridViewHelper<AccessPermission>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.permissionGridView, model, UserProfileManagementTriggers.ChangeSelectedAccessPermission);
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    switch (e.ChangeType)
    {
      case ChangeType.ItemAdded:
      case ChangeType.ItemEdited:
      case ChangeType.SelectionChanged:
        if (e.Type == typeof (UserProfile) && !e.IsList)
        {
          this.FillFields((UserProfile) e.ChangedObject);
          break;
        }
        if (!(e.Type == typeof (AccessPermission)) || e.IsList)
          break;
        this.FillFields((AccessPermission) e.ChangedObject);
        break;
      case ChangeType.ListLoaded:
        if (!(e.ChangedObject is ICollection<AccessPermission>))
          break;
        this.permissionGridView.ExpandAllGroups();
        break;
    }
  }

  private void InitializeModelMapper()
  {
    bool isEditingEnabled = this.ViewMode != 0;
    ModelMapper<UserProfile> modelMapper1 = new ModelMapper<UserProfile>(isEditingEnabled);
    modelMapper1.Add((Expression<Func<UserProfile, object>>) (p => p.Name), (object) this.profileName);
    modelMapper1.Add((Expression<Func<UserProfile, object>>) (p => p.Description), (object) this.profileDescription);
    this.profileModelMapper = modelMapper1;
    ModelMapper<AccessPermission> modelMapper2 = new ModelMapper<AccessPermission>(isEditingEnabled);
    modelMapper2.Add((Expression<Func<AccessPermission, object>>) (ap => ap.Name), (object) this.permissionName);
    modelMapper2.Add((Expression<Func<AccessPermission, object>>) (ap => ap.Description), (object) this.permissionDescription);
    this.permissionModelMapper = modelMapper2;
    this.permissionModelMapper.SetUIEnabledForced(false, (object) this.permissionName, (object) this.permissionDescription);
  }

  private void BuildUI()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ProfileMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.ProfileMasterDetailBrowser_RibbonMaintenanceGroup, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("ProfileAdd"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("ProfileEdit"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("ProfileDelete")));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ProfileMasterDetailBrowser_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ProfileMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool isUIEnabled = this.ViewMode != 0;
    this.profileModelMapper.SetUIEnabled(isUIEnabled);
    this.permissionModelMapper.SetUIEnabled(isUIEnabled);
    this.permissionGridView.OptionsBehavior.Editable = isUIEnabled;
  }

  private void FillFields(UserProfile profile)
  {
    this.profileModelMapper.SetUIEnabled(profile != null && this.ViewMode != 0);
    this.profileModelMapper.CopyModelToUI((ICollection<UserProfile>) new UserProfile[1]
    {
      profile
    });
  }

  private void FillFields(AccessPermission accessPermission)
  {
    try
    {
      this.isPermissionFieldsFilling = true;
      this.permissionModelMapper.SetUIEnabled(accessPermission != null && this.ViewMode != 0);
      this.permissionModelMapper.CopyModelToUI((ICollection<AccessPermission>) new AccessPermission[1]
      {
        accessPermission
      });
    }
    finally
    {
      this.isPermissionFieldsFilling = false;
    }
  }

  public override void CopyUIToModel() => this.profileModelMapper.CopyUIToModel();

  private void flagCheckEdit_CheckedChanged(object sender, EventArgs e)
  {
    if (sender == null || this.isPermissionFieldsFilling || !(sender is CheckEdit checkEdit))
      return;
    bool newValue = checkEdit.Checked;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.RefreshDataFromForm, (TriggerContext) new ValueChangeTriggerContext((object) null, (object) newValue)));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProfileMasterDetailBrowser));
    this.profileGridControl = new GridControl();
    this.profileGridView = new GridView();
    this.profileNamegridColumn = new GridColumn();
    this.profileDescriptionGridColumn = new GridColumn();
    this.layoutControl1 = new LayoutControl();
    this.permissionDescription = new DevExpressMemoEdit();
    this.permissionName = new DevExpressTextEdit();
    this.permissionGridControl = new GridControl();
    this.permissionGridView = new GridView();
    this.componentNameGridColumn = new GridColumn();
    this.nameGridColumn = new GridColumn();
    this.flagGridColumn = new GridColumn();
    this.flagCheckEdit = new RepositoryItemCheckEdit();
    this.profileDescription = new DevExpressMemoEdit();
    this.profileName = new DevExpressTextEdit();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.profileControlGroup = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.permissionControlGroup = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.profileGridLayoutControlItem = new LayoutControlItem();
    this.profileGridControl.BeginInit();
    this.profileGridView.BeginInit();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.permissionDescription.Properties.BeginInit();
    this.permissionName.Properties.BeginInit();
    this.permissionGridControl.BeginInit();
    this.permissionGridView.BeginInit();
    this.flagCheckEdit.BeginInit();
    this.profileDescription.Properties.BeginInit();
    this.profileName.Properties.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.profileControlGroup.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.permissionControlGroup.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.profileGridLayoutControlItem.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.profileGridControl, "profileGridControl");
    this.profileGridControl.MainView = (BaseView) this.profileGridView;
    this.profileGridControl.Name = "profileGridControl";
    this.profileGridControl.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.profileGridView
    });
    this.profileGridView.Columns.AddRange(new GridColumn[2]
    {
      this.profileNamegridColumn,
      this.profileDescriptionGridColumn
    });
    this.profileGridView.GridControl = this.profileGridControl;
    this.profileGridView.Name = "profileGridView";
    this.profileGridView.OptionsBehavior.Editable = false;
    this.profileGridView.OptionsCustomization.AllowFilter = false;
    this.profileGridView.OptionsDetail.EnableMasterViewMode = false;
    this.profileGridView.OptionsFilter.AllowFilterEditor = false;
    this.profileGridView.OptionsMenu.EnableColumnMenu = false;
    this.profileGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.profileGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.profileGridView.OptionsView.EnableAppearanceOddRow = true;
    this.profileGridView.OptionsView.ShowDetailButtons = false;
    this.profileGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.profileNamegridColumn, "profileNamegridColumn");
    this.profileNamegridColumn.FieldName = "Name";
    this.profileNamegridColumn.Name = "profileNamegridColumn";
    componentResourceManager.ApplyResources((object) this.profileDescriptionGridColumn, "profileDescriptionGridColumn");
    this.profileDescriptionGridColumn.FieldName = "Description";
    this.profileDescriptionGridColumn.Name = "profileDescriptionGridColumn";
    this.layoutControl1.Controls.Add((Control) this.permissionDescription);
    this.layoutControl1.Controls.Add((Control) this.permissionName);
    this.layoutControl1.Controls.Add((Control) this.permissionGridControl);
    this.layoutControl1.Controls.Add((Control) this.profileDescription);
    this.layoutControl1.Controls.Add((Control) this.profileName);
    this.layoutControl1.Controls.Add((Control) this.profileGridControl);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem5
    });
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.permissionDescription, "permissionDescription");
    this.permissionDescription.EnterMoveNextControl = true;
    this.permissionDescription.FormatString = (string) null;
    this.permissionDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.permissionDescription.IsReadOnly = true;
    this.permissionDescription.IsUndoing = false;
    this.permissionDescription.MinimumSize = new Size(388, 44);
    this.permissionDescription.Name = "permissionDescription";
    this.permissionDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.permissionDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.permissionDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.permissionDescription.Properties.ReadOnly = true;
    this.permissionDescription.StyleController = (IStyleController) this.layoutControl1;
    this.permissionDescription.Validator = (ICustomValidator) null;
    this.permissionDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.permissionName, "permissionName");
    this.permissionName.EnterMoveNextControl = true;
    this.permissionName.FormatString = (string) null;
    this.permissionName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.permissionName.IsReadOnly = true;
    this.permissionName.IsUndoing = false;
    this.permissionName.Name = "permissionName";
    this.permissionName.Properties.Appearance.BorderColor = Color.Yellow;
    this.permissionName.Properties.Appearance.Options.UseBorderColor = true;
    this.permissionName.Properties.BorderStyle = BorderStyles.Simple;
    this.permissionName.Properties.ReadOnly = true;
    this.permissionName.StyleController = (IStyleController) this.layoutControl1;
    this.permissionName.Validator = (ICustomValidator) null;
    this.permissionName.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.permissionGridControl, "permissionGridControl");
    this.permissionGridControl.MainView = (BaseView) this.permissionGridView;
    this.permissionGridControl.Name = "permissionGridControl";
    this.permissionGridControl.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.flagCheckEdit
    });
    this.permissionGridControl.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.permissionGridView
    });
    this.permissionGridView.Columns.AddRange(new GridColumn[3]
    {
      this.componentNameGridColumn,
      this.nameGridColumn,
      this.flagGridColumn
    });
    this.permissionGridView.GridControl = this.permissionGridControl;
    this.permissionGridView.GroupCount = 1;
    this.permissionGridView.Name = "permissionGridView";
    this.permissionGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.permissionGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.permissionGridView.OptionsBehavior.Editable = false;
    this.permissionGridView.OptionsPrint.ExpandAllDetails = true;
    this.permissionGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.permissionGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.permissionGridView.OptionsView.EnableAppearanceOddRow = true;
    this.permissionGridView.OptionsView.ShowGroupPanel = false;
    this.permissionGridView.SortInfo.AddRange(new GridColumnSortInfo[2]
    {
      new GridColumnSortInfo(this.componentNameGridColumn, ColumnSortOrder.Ascending),
      new GridColumnSortInfo(this.nameGridColumn, ColumnSortOrder.Ascending)
    });
    componentResourceManager.ApplyResources((object) this.componentNameGridColumn, "componentNameGridColumn");
    this.componentNameGridColumn.FieldName = "ComponentName";
    this.componentNameGridColumn.Name = "componentNameGridColumn";
    componentResourceManager.ApplyResources((object) this.nameGridColumn, "nameGridColumn");
    this.nameGridColumn.FieldName = "Name";
    this.nameGridColumn.Name = "nameGridColumn";
    this.nameGridColumn.OptionsColumn.AllowEdit = false;
    this.nameGridColumn.OptionsColumn.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.flagGridColumn, "flagGridColumn");
    this.flagGridColumn.ColumnEdit = (RepositoryItem) this.flagCheckEdit;
    this.flagGridColumn.FieldName = "AccessPermissionFlag";
    this.flagGridColumn.Name = "flagGridColumn";
    componentResourceManager.ApplyResources((object) this.flagCheckEdit, "flagCheckEdit");
    this.flagCheckEdit.Name = "flagCheckEdit";
    this.flagCheckEdit.CheckedChanged += new EventHandler(this.flagCheckEdit_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.profileDescription, "profileDescription");
    this.profileDescription.FormatString = (string) null;
    this.profileDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.profileDescription.IsReadOnly = false;
    this.profileDescription.IsUndoing = false;
    this.profileDescription.MinimumSize = new Size(394, 44);
    this.profileDescription.Name = "profileDescription";
    this.profileDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.profileDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.profileDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.profileDescription.StyleController = (IStyleController) this.layoutControl1;
    this.profileDescription.Validator = (ICustomValidator) null;
    this.profileDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.profileName, "profileName");
    this.profileName.EnterMoveNextControl = true;
    this.profileName.FormatString = (string) null;
    this.profileName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.profileName.IsMandatory = true;
    this.profileName.IsReadOnly = true;
    this.profileName.IsUndoing = false;
    this.profileName.Name = "profileName";
    this.profileName.Properties.Appearance.BackColor = Color.LightYellow;
    this.profileName.Properties.Appearance.BorderColor = Color.Yellow;
    this.profileName.Properties.Appearance.Options.UseBackColor = true;
    this.profileName.Properties.Appearance.Options.UseBorderColor = true;
    this.profileName.Properties.BorderStyle = BorderStyles.Simple;
    this.profileName.Properties.ReadOnly = true;
    this.profileName.StyleController = (IStyleController) this.layoutControl1;
    this.profileName.Validator = (ICustomValidator) null;
    this.profileName.Value = (object) "";
    this.layoutControlItem4.Control = (Control) this.permissionName;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 419);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(449, 24);
    this.layoutControlItem4.TextSize = new Size(53, 13);
    this.layoutControlItem4.TextToControlDistance = 5;
    this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlItem5.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutControlItem5.Control = (Control) this.permissionDescription;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(0, 419);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(449, 107);
    this.layoutControlItem5.TextSize = new Size(53, 13);
    this.layoutControlItem5.TextToControlDistance = 5;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.profileControlGroup,
      (BaseLayoutItem) this.layoutControlGroup2
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(940, 710);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.profileControlGroup, "profileControlGroup");
    this.profileControlGroup.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.permissionControlGroup
    });
    this.profileControlGroup.Location = new Point(423, 0);
    this.profileControlGroup.Name = "profileControlGroup";
    this.profileControlGroup.Size = new Size(497, 690);
    this.layoutControlItem2.Control = (Control) this.profileName;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(473, 24);
    this.layoutControlItem2.TextSize = new Size(53, 13);
    this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlItem3.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutControlItem3.Control = (Control) this.profileDescription;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 24);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(473, 52);
    this.layoutControlItem3.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.permissionControlGroup, "permissionControlGroup");
    this.permissionControlGroup.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.permissionControlGroup.Location = new Point(0, 76);
    this.permissionControlGroup.Name = "permissionControlGroup";
    this.permissionControlGroup.Size = new Size(473, 570);
    this.layoutControlItem1.Control = (Control) this.permissionGridControl;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(449, 526);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.profileGridLayoutControlItem
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(423, 690);
    this.profileGridLayoutControlItem.Control = (Control) this.profileGridControl;
    componentResourceManager.ApplyResources((object) this.profileGridLayoutControlItem, "profileGridLayoutControlItem");
    this.profileGridLayoutControlItem.Location = new Point(0, 0);
    this.profileGridLayoutControlItem.Name = "profileGridLayoutControlItem";
    this.profileGridLayoutControlItem.Size = new Size(399, 646);
    this.profileGridLayoutControlItem.TextSize = new Size(0, 0);
    this.profileGridLayoutControlItem.TextToControlDistance = 0;
    this.profileGridLayoutControlItem.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (ProfileMasterDetailBrowser);
    this.profileGridControl.EndInit();
    this.profileGridView.EndInit();
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.permissionDescription.Properties.EndInit();
    this.permissionName.Properties.EndInit();
    this.permissionGridControl.EndInit();
    this.permissionGridView.EndInit();
    this.flagCheckEdit.EndInit();
    this.profileDescription.Properties.EndInit();
    this.profileName.Properties.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutControlGroup1.EndInit();
    this.profileControlGroup.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.permissionControlGroup.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.profileGridLayoutControlItem.EndInit();
    this.ResumeLayout(false);
  }
}
