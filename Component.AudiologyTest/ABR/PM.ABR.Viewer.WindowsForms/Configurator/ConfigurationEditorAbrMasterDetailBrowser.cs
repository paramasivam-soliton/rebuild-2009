// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Configurator.ConfigurationEditorAbrMasterDetailBrowser
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.ABR.Viewer.WindowsForms.Properties;
using PathMedical.AudiologyTest;
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
namespace PathMedical.ABR.Viewer.WindowsForms.Configurator;

[ToolboxItem(false)]
public sealed class ConfigurationEditorAbrMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressSingleSelectionGridViewHelper<AbrPreset> presetSingleSelectionGridHelper;
  private ModelMapper<AbrPreset> modelMapper;
  private IContainer components;
  private LayoutControl layoutControlAbrConfigurationManagement;
  private GridControl presetGrid;
  private GridView presetGridView;
  private LayoutControlGroup layoutAbrConfigurationManagement;
  private LayoutControlItem layoutControlItem1;
  private DevExpressTextEdit presetName;
  private LayoutControlItem layoutPresetName;
  private DevExpressTextEdit presetDescription;
  private LayoutControlItem layoutPresetDescription;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressComboBoxEdit level;
  private LayoutControlGroup layoutCommonInformation;
  private LayoutControlGroup layoutAbrConfiguration;
  private LayoutControlItem layoutControlItem2;
  private GridColumn columnPresetName;
  private GridColumn columnPresetDescription;
  private DevExpressComboBoxEdit presetCategory;
  private LayoutControlItem layoutPresetCategory;
  private LayoutControlGroup layoutGroupPresets;

  public ConfigurationEditorAbrMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.InitializeSelectionValues();
    this.Dock = DockStyle.Fill;
    this.HelpMarker = "config_abr_01.html";
  }

  public ConfigurationEditorAbrMasterDetailBrowser(IModel model)
    : this()
  {
    this.presetSingleSelectionGridHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<AbrPreset>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.presetGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.InitializeModelMapper();
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ConfigurationEditorAbrMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.ConfigurationEditorAbrMasterDetailBrowser_RibbonMaintenanceGroup, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AbrAdd") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AbrEdit") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AbrDelete") as Bitmap));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ConfigurationEditorAbrMasterDetailBrowser_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ConfigurationEditorAbrMasterDetailBrowser_RibbonHelp, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.ChangedObject is AbrPreset) && !(e.ChangedObject is ICollection<AbrPreset>))
      return;
    object presets = (object) (e.ChangedObject as ICollection<AbrPreset>);
    if (presets == null)
      presets = (object) new AbrPreset[1]
      {
        e.ChangedObject as AbrPreset
      };
    this.FillFields((ICollection<AbrPreset>) presets);
  }

  private void InitializeModelMapper()
  {
    ModelMapper<AbrPreset> modelMapper = new ModelMapper<AbrPreset>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<AbrPreset, object>>) (i => i.Name), (object) this.presetName);
    modelMapper.Add((Expression<Func<AbrPreset, object>>) (i => i.Description), (object) this.presetDescription);
    modelMapper.Add((Expression<Func<AbrPreset, object>>) (i => (object) i.Level), (object) this.level);
    modelMapper.Add((Expression<Func<AbrPreset, object>>) (i => (object) i.Category), (object) this.presetCategory);
    this.modelMapper = modelMapper;
  }

  private void FillFields(ICollection<AbrPreset> presets)
  {
    this.modelMapper.CopyModelToUI(presets);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeSelectionValues()
  {
    this.level.DataSource = (object) new AbrLevels[4]
    {
      AbrLevels.Db30,
      AbrLevels.Db35,
      AbrLevels.Db40,
      AbrLevels.Db45
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

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationEditorAbrMasterDetailBrowser));
    this.layoutControlAbrConfigurationManagement = new LayoutControl();
    this.presetCategory = new DevExpressComboBoxEdit();
    this.level = new DevExpressComboBoxEdit();
    this.presetDescription = new DevExpressTextEdit();
    this.presetName = new DevExpressTextEdit();
    this.presetGrid = new GridControl();
    this.presetGridView = new GridView();
    this.columnPresetName = new GridColumn();
    this.columnPresetDescription = new GridColumn();
    this.layoutAbrConfigurationManagement = new LayoutControlGroup();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutCommonInformation = new LayoutControlGroup();
    this.layoutPresetName = new LayoutControlItem();
    this.layoutPresetDescription = new LayoutControlItem();
    this.layoutPresetCategory = new LayoutControlItem();
    this.layoutAbrConfiguration = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutGroupPresets = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlAbrConfigurationManagement.BeginInit();
    this.layoutControlAbrConfigurationManagement.SuspendLayout();
    this.presetCategory.Properties.BeginInit();
    this.level.Properties.BeginInit();
    this.presetDescription.Properties.BeginInit();
    this.presetName.Properties.BeginInit();
    this.presetGrid.BeginInit();
    this.presetGridView.BeginInit();
    this.layoutAbrConfigurationManagement.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutCommonInformation.BeginInit();
    this.layoutPresetName.BeginInit();
    this.layoutPresetDescription.BeginInit();
    this.layoutPresetCategory.BeginInit();
    this.layoutAbrConfiguration.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutGroupPresets.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.SuspendLayout();
    this.layoutControlAbrConfigurationManagement.Controls.Add((Control) this.presetCategory);
    this.layoutControlAbrConfigurationManagement.Controls.Add((Control) this.level);
    this.layoutControlAbrConfigurationManagement.Controls.Add((Control) this.presetDescription);
    this.layoutControlAbrConfigurationManagement.Controls.Add((Control) this.presetName);
    this.layoutControlAbrConfigurationManagement.Controls.Add((Control) this.presetGrid);
    componentResourceManager.ApplyResources((object) this.layoutControlAbrConfigurationManagement, "layoutControlAbrConfigurationManagement");
    this.layoutControlAbrConfigurationManagement.Name = "layoutControlAbrConfigurationManagement";
    this.layoutControlAbrConfigurationManagement.Root = this.layoutAbrConfigurationManagement;
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
    this.presetCategory.ShowEmptyElement = false;
    this.presetCategory.StyleController = (IStyleController) this.layoutControlAbrConfigurationManagement;
    this.presetCategory.Validator = (ICustomValidator) null;
    this.presetCategory.Value = (object) null;
    this.level.EnterMoveNextControl = true;
    this.level.FormatString = (string) null;
    this.level.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.level.IsMandatory = true;
    this.level.IsReadOnly = false;
    this.level.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.level, "level");
    this.level.Name = "level";
    this.level.Properties.Appearance.BackColor = Color.LightYellow;
    this.level.Properties.Appearance.BorderColor = Color.LightGray;
    this.level.Properties.Appearance.Options.UseBackColor = true;
    this.level.Properties.Appearance.Options.UseBorderColor = true;
    this.level.Properties.BorderStyle = BorderStyles.Simple;
    this.level.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("level.Properties.Buttons"))
    });
    this.level.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.level.ShowEmptyElement = false;
    this.level.StyleController = (IStyleController) this.layoutControlAbrConfigurationManagement;
    this.level.Validator = (ICustomValidator) null;
    this.level.Value = (object) null;
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
    this.presetDescription.StyleController = (IStyleController) this.layoutControlAbrConfigurationManagement;
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
    this.presetName.StyleController = (IStyleController) this.layoutControlAbrConfigurationManagement;
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
    componentResourceManager.ApplyResources((object) this.layoutAbrConfigurationManagement, "layoutAbrConfigurationManagement");
    this.layoutAbrConfigurationManagement.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutAbrConfigurationManagement.GroupBordersVisible = false;
    this.layoutAbrConfigurationManagement.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutCommonInformation,
      (BaseLayoutItem) this.layoutAbrConfiguration,
      (BaseLayoutItem) this.layoutGroupPresets
    });
    this.layoutAbrConfigurationManagement.Location = new Point(0, 0);
    this.layoutAbrConfigurationManagement.Name = "layoutAbrConfigurationManagement";
    this.layoutAbrConfigurationManagement.Size = new Size(940, 710);
    this.layoutAbrConfigurationManagement.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutAbrConfigurationManagement.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(488, 184);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(432, 506);
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
    this.layoutPresetName.TextSize = new Size(53, 13);
    this.layoutPresetDescription.Control = (Control) this.presetDescription;
    componentResourceManager.ApplyResources((object) this.layoutPresetDescription, "layoutPresetDescription");
    this.layoutPresetDescription.Location = new Point(0, 24);
    this.layoutPresetDescription.Name = "layoutPresetDescription";
    this.layoutPresetDescription.Size = new Size(408, 24);
    this.layoutPresetDescription.TextSize = new Size(53, 13);
    this.layoutPresetCategory.Control = (Control) this.presetCategory;
    componentResourceManager.ApplyResources((object) this.layoutPresetCategory, "layoutPresetCategory");
    this.layoutPresetCategory.Location = new Point(0, 48 /*0x30*/);
    this.layoutPresetCategory.Name = "layoutPresetCategory";
    this.layoutPresetCategory.Size = new Size(408, 24);
    this.layoutPresetCategory.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.layoutAbrConfiguration, "layoutAbrConfiguration");
    this.layoutAbrConfiguration.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.layoutAbrConfiguration.Location = new Point(488, 116);
    this.layoutAbrConfiguration.Name = "layoutAbrConfiguration";
    this.layoutAbrConfiguration.Size = new Size(432, 68);
    this.layoutControlItem2.Control = (Control) this.level;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(408, 24);
    this.layoutControlItem2.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.layoutGroupPresets, "layoutGroupPresets");
    this.layoutGroupPresets.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutGroupPresets.Location = new Point(0, 0);
    this.layoutGroupPresets.Name = "layoutGroupPresets";
    this.layoutGroupPresets.Size = new Size(488, 690);
    this.layoutControlItem1.Control = (Control) this.presetGrid;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(464, 646);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControlAbrConfigurationManagement);
    this.Name = nameof (ConfigurationEditorAbrMasterDetailBrowser);
    this.layoutControlAbrConfigurationManagement.EndInit();
    this.layoutControlAbrConfigurationManagement.ResumeLayout(false);
    this.presetCategory.Properties.EndInit();
    this.level.Properties.EndInit();
    this.presetDescription.Properties.EndInit();
    this.presetName.Properties.EndInit();
    this.presetGrid.EndInit();
    this.presetGridView.EndInit();
    this.layoutAbrConfigurationManagement.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutCommonInformation.EndInit();
    this.layoutPresetName.EndInit();
    this.layoutPresetDescription.EndInit();
    this.layoutPresetCategory.EndInit();
    this.layoutAbrConfiguration.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutGroupPresets.EndInit();
    this.layoutControlItem1.EndInit();
    this.ResumeLayout(false);
  }
}
