// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Configurator.ConfigurationEditorDpoaeMasterDetailBrowser
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.AudiologyTest;
using PathMedical.DPOAE.Viewer.WindowsForms.Properties;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Configurator;

[ToolboxItem(false)]
public sealed class ConfigurationEditorDpoaeMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressSingleSelectionGridViewHelper<DpoaePreset> presetSingleSelectionGridHelper;
  private ModelMapper<DpoaePreset> modelMapper;
  private IContainer components;
  private LayoutControl layoutControlDpoaeConfigurationManagement;
  private GridControl presetGrid;
  private GridView presetGridView;
  private LayoutControlGroup layoutDpoaeConfigurationManagement;
  private LayoutControlItem layoutControlItem1;
  private DevExpressTextEdit presetName;
  private LayoutControlItem layoutPresetName;
  private DevExpressTextEdit presetDescription;
  private LayoutControlItem layoutPresetDescription;
  private LayoutControlGroup layoutCommonInformation;
  private LayoutControlGroup layoutDpoaeConfiguration;
  private GridColumn columnPresetName;
  private GridColumn columnPresetDescription;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressComboBoxEdit presetCategory;
  private LayoutControlItem layoutPresetCategory;
  private DevExpressComboBoxEdit dpoaePresetNumber;
  private LayoutControlItem layoutControlItem2;
  private DevExpressMemoEdit dpoaePresetNumberDescription;
  private LayoutControlItem layoutDpoaePresetNumberDescription;
  private LayoutControlGroup layoutGroupDpoaeProtocols;

  public ConfigurationEditorDpoaeMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.InitializeSelectionValues();
    this.Dock = DockStyle.Fill;
    this.HelpMarker = "config_dpoae_01.html";
  }

  public ConfigurationEditorDpoaeMasterDetailBrowser(IModel model)
    : this()
  {
    this.presetSingleSelectionGridHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<DpoaePreset>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.presetGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.InitializeModelMapper();
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ConfigurationEditorDpoaeMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.ConfigurationEditorDpoaeMasterDetailBrowser_RibbonMaintenanceGroup, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_DpoaeAdd") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_DpoaeEdit") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_DpoaeDelete") as Bitmap));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ConfigurationEditorDpoaeMasterDetailBrowser_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ConfigurationEditorDpoaeMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.ChangedObject is DpoaePreset) && !(e.ChangedObject is ICollection<DpoaePreset>))
      return;
    object presets = (object) (e.ChangedObject as ICollection<DpoaePreset>);
    if (presets == null)
      presets = (object) new DpoaePreset[1]
      {
        e.ChangedObject as DpoaePreset
      };
    this.FillFields((ICollection<DpoaePreset>) presets);
  }

  private void InitializeModelMapper()
  {
    ModelMapper<DpoaePreset> modelMapper = new ModelMapper<DpoaePreset>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<DpoaePreset, object>>) (i => i.Name), (object) this.presetName);
    modelMapper.Add((Expression<Func<DpoaePreset, object>>) (i => i.Description), (object) this.presetDescription);
    modelMapper.Add((Expression<Func<DpoaePreset, object>>) (i => (object) i.PresetNumber), (object) this.dpoaePresetNumber);
    modelMapper.Add((Expression<Func<DpoaePreset, object>>) (i => (object) i.Category), (object) this.presetCategory);
    this.modelMapper = modelMapper;
  }

  private void InitializeSelectionValues()
  {
    this.dpoaePresetNumber.DataSource = (object) new DpoaeProtocolType[4]
    {
      DpoaeProtocolType.Protocol1,
      DpoaeProtocolType.Protocol2,
      DpoaeProtocolType.Protocol3,
      DpoaeProtocolType.Protocol4
    };
    this.presetCategory.DataSource = (object) new PresetCategoryType[2]
    {
      PresetCategoryType.Basic,
      PresetCategoryType.Enhanced
    };
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  private void FillFields(ICollection<DpoaePreset> presets)
  {
    this.modelMapper.CopyModelToUI(presets);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void DpoaePresetChanged(object sender, EventArgs e)
  {
    if (this.dpoaePresetNumber != null && this.dpoaePresetNumber.Value != null)
      this.dpoaePresetNumberDescription.Value = (object) (GlobalResourceEnquirer.Instance.GetResourceByName((Enum) (DpoaeProtocolType) this.dpoaePresetNumber.Value, "Description") as string);
    else
      this.dpoaePresetNumberDescription.Value = (object) string.Empty;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationEditorDpoaeMasterDetailBrowser));
    this.layoutControlDpoaeConfigurationManagement = new LayoutControl();
    this.dpoaePresetNumberDescription = new DevExpressMemoEdit();
    this.presetCategory = new DevExpressComboBoxEdit();
    this.dpoaePresetNumber = new DevExpressComboBoxEdit();
    this.presetDescription = new DevExpressTextEdit();
    this.presetName = new DevExpressTextEdit();
    this.presetGrid = new GridControl();
    this.presetGridView = new GridView();
    this.columnPresetName = new GridColumn();
    this.columnPresetDescription = new GridColumn();
    this.layoutDpoaeConfigurationManagement = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutCommonInformation = new LayoutControlGroup();
    this.layoutPresetName = new LayoutControlItem();
    this.layoutPresetDescription = new LayoutControlItem();
    this.layoutPresetCategory = new LayoutControlItem();
    this.layoutDpoaeConfiguration = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutDpoaePresetNumberDescription = new LayoutControlItem();
    this.layoutGroupDpoaeProtocols = new LayoutControlGroup();
    this.layoutControlDpoaeConfigurationManagement.BeginInit();
    this.layoutControlDpoaeConfigurationManagement.SuspendLayout();
    this.dpoaePresetNumberDescription.Properties.BeginInit();
    this.presetCategory.Properties.BeginInit();
    this.dpoaePresetNumber.Properties.BeginInit();
    this.presetDescription.Properties.BeginInit();
    this.presetName.Properties.BeginInit();
    this.presetGrid.BeginInit();
    this.presetGridView.BeginInit();
    this.layoutDpoaeConfigurationManagement.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutCommonInformation.BeginInit();
    this.layoutPresetName.BeginInit();
    this.layoutPresetDescription.BeginInit();
    this.layoutPresetCategory.BeginInit();
    this.layoutDpoaeConfiguration.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutDpoaePresetNumberDescription.BeginInit();
    this.layoutGroupDpoaeProtocols.BeginInit();
    this.SuspendLayout();
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.dpoaePresetNumberDescription);
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.presetCategory);
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.dpoaePresetNumber);
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.presetDescription);
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.presetName);
    this.layoutControlDpoaeConfigurationManagement.Controls.Add((Control) this.presetGrid);
    componentResourceManager.ApplyResources((object) this.layoutControlDpoaeConfigurationManagement, "layoutControlDpoaeConfigurationManagement");
    this.layoutControlDpoaeConfigurationManagement.Name = "layoutControlDpoaeConfigurationManagement";
    this.layoutControlDpoaeConfigurationManagement.Root = this.layoutDpoaeConfigurationManagement;
    componentResourceManager.ApplyResources((object) this.dpoaePresetNumberDescription, "dpoaePresetNumberDescription");
    this.dpoaePresetNumberDescription.FormatString = (string) null;
    this.dpoaePresetNumberDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.dpoaePresetNumberDescription.IsReadOnly = true;
    this.dpoaePresetNumberDescription.IsUndoing = false;
    this.dpoaePresetNumberDescription.Name = "dpoaePresetNumberDescription";
    this.dpoaePresetNumberDescription.Properties.Appearance.BorderColor = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
    this.dpoaePresetNumberDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.dpoaePresetNumberDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.dpoaePresetNumberDescription.Properties.ReadOnly = true;
    this.dpoaePresetNumberDescription.ShowModified = false;
    this.dpoaePresetNumberDescription.StyleController = (IStyleController) this.layoutControlDpoaeConfigurationManagement;
    this.dpoaePresetNumberDescription.Validator = (ICustomValidator) null;
    this.dpoaePresetNumberDescription.Value = (object) "";
    this.presetCategory.EnterMoveNextControl = true;
    this.presetCategory.FormatString = (string) null;
    this.presetCategory.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.presetCategory.IsMandatory = true;
    this.presetCategory.IsReadOnly = false;
    this.presetCategory.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.presetCategory, "presetCategory");
    this.presetCategory.Name = "presetCategory";
    this.presetCategory.Properties.Appearance.BackColor = Color.LightYellow;
    this.presetCategory.Properties.Appearance.BorderColor = Color.LightGray;
    this.presetCategory.Properties.Appearance.Options.UseBackColor = true;
    this.presetCategory.Properties.Appearance.Options.UseBorderColor = true;
    this.presetCategory.Properties.BorderStyle = BorderStyles.Simple;
    this.presetCategory.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("presetCategory.Properties.Buttons"))
    });
    this.presetCategory.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.presetCategory.ShowEmptyElement = true;
    this.presetCategory.StyleController = (IStyleController) this.layoutControlDpoaeConfigurationManagement;
    this.presetCategory.Validator = (ICustomValidator) null;
    this.presetCategory.Value = (object) null;
    this.dpoaePresetNumber.EnterMoveNextControl = true;
    this.dpoaePresetNumber.FormatString = (string) null;
    this.dpoaePresetNumber.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.dpoaePresetNumber.IsMandatory = true;
    this.dpoaePresetNumber.IsReadOnly = false;
    this.dpoaePresetNumber.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.dpoaePresetNumber, "dpoaePresetNumber");
    this.dpoaePresetNumber.Name = "dpoaePresetNumber";
    this.dpoaePresetNumber.Properties.Appearance.BackColor = Color.LightYellow;
    this.dpoaePresetNumber.Properties.Appearance.BorderColor = Color.LightGray;
    this.dpoaePresetNumber.Properties.Appearance.Options.UseBackColor = true;
    this.dpoaePresetNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.dpoaePresetNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.dpoaePresetNumber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("dpoaePresetNumber.Properties.Buttons"))
    });
    this.dpoaePresetNumber.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.dpoaePresetNumber.ShowEmptyElement = true;
    this.dpoaePresetNumber.StyleController = (IStyleController) this.layoutControlDpoaeConfigurationManagement;
    this.dpoaePresetNumber.Validator = (ICustomValidator) null;
    this.dpoaePresetNumber.Value = (object) null;
    this.dpoaePresetNumber.SelectedValueChanged += new EventHandler(this.DpoaePresetChanged);
    componentResourceManager.ApplyResources((object) this.presetDescription, "presetDescription");
    this.presetDescription.EnterMoveNextControl = true;
    this.presetDescription.FormatString = (string) null;
    this.presetDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.presetDescription.IsReadOnly = false;
    this.presetDescription.IsUndoing = false;
    this.presetDescription.Name = "presetDescription";
    this.presetDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.presetDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.presetDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.presetDescription.StyleController = (IStyleController) this.layoutControlDpoaeConfigurationManagement;
    this.presetDescription.Validator = (ICustomValidator) null;
    this.presetDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.presetName, "presetName");
    this.presetName.EnterMoveNextControl = true;
    this.presetName.FormatString = (string) null;
    this.presetName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.presetName.IsMandatory = true;
    this.presetName.IsReadOnly = false;
    this.presetName.IsUndoing = false;
    this.presetName.Name = "presetName";
    this.presetName.Properties.Appearance.BackColor = Color.LightYellow;
    this.presetName.Properties.Appearance.BorderColor = Color.Yellow;
    this.presetName.Properties.Appearance.Options.UseBackColor = true;
    this.presetName.Properties.Appearance.Options.UseBorderColor = true;
    this.presetName.Properties.BorderStyle = BorderStyles.Simple;
    this.presetName.Properties.MaxLength = 6;
    this.presetName.StyleController = (IStyleController) this.layoutControlDpoaeConfigurationManagement;
    this.presetName.Validator = (ICustomValidator) null;
    this.presetName.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.presetGrid, "presetGrid");
    this.presetGrid.MainView = (BaseView) this.presetGridView;
    this.presetGrid.Name = "presetGrid";
    this.presetGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.presetGridView
    });
    this.presetGridView.Columns.AddRange(new GridColumn[2]
    {
      this.columnPresetName,
      this.columnPresetDescription
    });
    this.presetGridView.GridControl = this.presetGrid;
    this.presetGridView.Name = "presetGridView";
    this.presetGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.presetGridView.OptionsBehavior.Editable = false;
    this.presetGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.presetGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.presetGridView.OptionsView.EnableAppearanceOddRow = true;
    this.presetGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.columnPresetName, "columnPresetName");
    this.columnPresetName.FieldName = "Name";
    this.columnPresetName.Name = "columnPresetName";
    componentResourceManager.ApplyResources((object) this.columnPresetDescription, "columnPresetDescription");
    this.columnPresetDescription.FieldName = "Description";
    this.columnPresetDescription.Name = "columnPresetDescription";
    componentResourceManager.ApplyResources((object) this.layoutDpoaeConfigurationManagement, "layoutDpoaeConfigurationManagement");
    this.layoutDpoaeConfigurationManagement.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutDpoaeConfigurationManagement.GroupBordersVisible = false;
    this.layoutDpoaeConfigurationManagement.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutCommonInformation,
      (BaseLayoutItem) this.layoutDpoaeConfiguration,
      (BaseLayoutItem) this.layoutGroupDpoaeProtocols
    });
    this.layoutDpoaeConfigurationManagement.Location = new Point(0, 0);
    this.layoutDpoaeConfigurationManagement.Name = "layoutAbrConfigurationManagement";
    this.layoutDpoaeConfigurationManagement.Size = new Size(940, 710);
    this.layoutDpoaeConfigurationManagement.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutDpoaeConfigurationManagement.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.presetGrid;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(464, 646);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(488, 592);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(432, 98);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutCommonInformation, "layoutCommonInformation");
    this.layoutCommonInformation.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutPresetName,
      (BaseLayoutItem) this.layoutPresetDescription,
      (BaseLayoutItem) this.layoutPresetCategory
    });
    this.layoutCommonInformation.Location = new Point(488, 0);
    this.layoutCommonInformation.Name = "layoutCommonInformation";
    this.layoutCommonInformation.Size = new Size(432, 116);
    this.layoutPresetName.Control = (Control) this.presetName;
    componentResourceManager.ApplyResources((object) this.layoutPresetName, "layoutPresetName");
    this.layoutPresetName.Location = new Point(0, 0);
    this.layoutPresetName.Name = "layoutPresetName";
    this.layoutPresetName.Size = new Size(408, 24);
    this.layoutPresetName.TextSize = new Size(87, 13);
    this.layoutPresetDescription.Control = (Control) this.presetDescription;
    componentResourceManager.ApplyResources((object) this.layoutPresetDescription, "layoutPresetDescription");
    this.layoutPresetDescription.Location = new Point(0, 24);
    this.layoutPresetDescription.Name = "layoutPresetDescription";
    this.layoutPresetDescription.Size = new Size(408, 24);
    this.layoutPresetDescription.TextSize = new Size(87, 13);
    this.layoutPresetCategory.Control = (Control) this.presetCategory;
    componentResourceManager.ApplyResources((object) this.layoutPresetCategory, "layoutPresetCategory");
    this.layoutPresetCategory.Location = new Point(0, 48 /*0x30*/);
    this.layoutPresetCategory.Name = "layoutPresetCategory";
    this.layoutPresetCategory.Size = new Size(408, 24);
    this.layoutPresetCategory.TextSize = new Size(87, 13);
    componentResourceManager.ApplyResources((object) this.layoutDpoaeConfiguration, "layoutDpoaeConfiguration");
    this.layoutDpoaeConfiguration.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutDpoaePresetNumberDescription
    });
    this.layoutDpoaeConfiguration.Location = new Point(488, 116);
    this.layoutDpoaeConfiguration.Name = "layoutAbrConfiguration";
    this.layoutDpoaeConfiguration.Size = new Size(432, 476);
    this.layoutControlItem2.Control = (Control) this.dpoaePresetNumber;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(408, 24);
    this.layoutControlItem2.TextSize = new Size(87, 13);
    this.layoutDpoaePresetNumberDescription.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutDpoaePresetNumberDescription.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutDpoaePresetNumberDescription.Control = (Control) this.dpoaePresetNumberDescription;
    componentResourceManager.ApplyResources((object) this.layoutDpoaePresetNumberDescription, "layoutDpoaePresetNumberDescription");
    this.layoutDpoaePresetNumberDescription.Location = new Point(0, 24);
    this.layoutDpoaePresetNumberDescription.Name = "layoutDpoaePresetNumberDescription";
    this.layoutDpoaePresetNumberDescription.Size = new Size(408, 408);
    this.layoutDpoaePresetNumberDescription.TextSize = new Size(87, 13);
    componentResourceManager.ApplyResources((object) this.layoutGroupDpoaeProtocols, "layoutGroupDpoaeProtocols");
    this.layoutGroupDpoaeProtocols.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutGroupDpoaeProtocols.Location = new Point(0, 0);
    this.layoutGroupDpoaeProtocols.Name = "layoutGroupDpoaeProtocols";
    this.layoutGroupDpoaeProtocols.Size = new Size(488, 690);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControlDpoaeConfigurationManagement);
    this.Name = nameof (ConfigurationEditorDpoaeMasterDetailBrowser);
    this.layoutControlDpoaeConfigurationManagement.EndInit();
    this.layoutControlDpoaeConfigurationManagement.ResumeLayout(false);
    this.dpoaePresetNumberDescription.Properties.EndInit();
    this.presetCategory.Properties.EndInit();
    this.dpoaePresetNumber.Properties.EndInit();
    this.presetDescription.Properties.EndInit();
    this.presetName.Properties.EndInit();
    this.presetGrid.EndInit();
    this.presetGridView.EndInit();
    this.layoutDpoaeConfigurationManagement.EndInit();
    this.layoutControlItem1.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutCommonInformation.EndInit();
    this.layoutPresetName.EndInit();
    this.layoutPresetDescription.EndInit();
    this.layoutPresetCategory.EndInit();
    this.layoutDpoaeConfiguration.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutDpoaePresetNumberDescription.EndInit();
    this.layoutGroupDpoaeProtocols.EndInit();
    this.ResumeLayout(false);
  }
}
