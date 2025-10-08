// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.Configuration.ConfigurationEditor
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.Configuration;

[ToolboxItem(false)]
public sealed class ConfigurationEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View, IPerformUndo
{
  private readonly ModelMapper<PatientManagementConfiguration> modelMapper;
  private IContainer components;
  private GridControl gridControl1;
  private BandedGridView bandedGridView1;
  private BandedGridColumn fieldColumn;
  private BandedGridColumn activeColumn;
  private BandedGridColumn group1Column;
  private BandedGridColumn group2Column;
  private BandedGridColumn group3Column;
  private GridBand gridBand1;
  private GridBand gridBand2;
  private RepositoryItemCheckEdit activeCheckEdit;
  private RepositoryItemCheckEdit group1CheckEdit;
  private RepositoryItemCheckEdit group2CheckEdit;
  private RepositoryItemCheckEdit group3CheckEdit;
  private RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
  private GridView repositoryItemGridLookUpEdit1View;
  private DevExpressComboBoxEdit configField11Combobox;
  private DevExpressComboBoxEdit configField12Combobox;
  private DevExpressComboBoxEdit configField21Combobox;
  private DevExpressComboBoxEdit configField22Combobox;
  private DevExpressComboBoxEdit configField31Combobox;
  private DevExpressComboBoxEdit configField32Combobox;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlGroup groupMandatoryGroupLevel;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlGroup layoutControlGroup4;
  private LayoutControlGroup layoutControlGroup3;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlItem layoutControlItem6;
  private LayoutControlItem layoutControlItem7;
  private DevExpressComboBoxEdit patientIdConfiguration;
  private LayoutControlItem layoutControlItem8;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem2;

  public ConfigurationEditor()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.patientIdConfiguration.DataSource = (object) new MedicalRecordTypes[5]
    {
      MedicalRecordTypes.None,
      MedicalRecordTypes.MRC,
      MedicalRecordTypes.GERMANY,
      MedicalRecordTypes.CPR_DK,
      MedicalRecordTypes.TURKEY
    };
    this.CreateRibbonBarCommands();
    this.HelpMarker = "config_patients_01.html";
  }

  public ConfigurationEditor(IModel model)
    : this()
  {
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    ModelMapper<PatientManagementConfiguration> modelMapper = new ModelMapper<PatientManagementConfiguration>(true);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList1Field1), (object) this.configField11Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList1Field2), (object) this.configField12Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList2Field1), (object) this.configField21Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList2Field2), (object) this.configField22Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList3Field1), (object) this.configField31Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => pmc.PatientBrowserListConfigurationList3Field2), (object) this.configField32Combobox);
    modelMapper.Add((Expression<Func<PatientManagementConfiguration, object>>) (pmc => (object) pmc.PatientIdFormat), (object) this.patientIdConfiguration);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType == ChangeType.ListLoaded && e.Type == typeof (PatientFieldConfigItem))
    {
      this.gridControl1.DataSource = e.ChangedObject;
      List<ComboBoxEditItemWrapper> list = ((IEnumerable<PatientFieldConfigItem>) e.ChangedObject).Select<PatientFieldConfigItem, ComboBoxEditItemWrapper>((Func<PatientFieldConfigItem, ComboBoxEditItemWrapper>) (pfci => new ComboBoxEditItemWrapper(pfci.FieldName, (object) pfci.FieldId))).ToList<ComboBoxEditItemWrapper>();
      this.configField11Combobox.DataSource = (object) list;
      this.configField12Combobox.DataSource = (object) list;
      this.configField21Combobox.DataSource = (object) list;
      this.configField22Combobox.DataSource = (object) list;
      this.configField31Combobox.DataSource = (object) list;
      this.configField32Combobox.DataSource = (object) list;
    }
    else if (e.ChangeType == ChangeType.SelectionChanged && e.Type == typeof (PatientManagementConfiguration))
      this.modelMapper.CopyModelToUI((PatientManagementConfiguration) e.ChangedObject);
    this.patientIdConfiguration.DataSource = (object) new MedicalRecordTypes[5]
    {
      MedicalRecordTypes.None,
      MedicalRecordTypes.MRC,
      MedicalRecordTypes.GERMANY,
      MedicalRecordTypes.CPR_DK,
      MedicalRecordTypes.TURKEY
    };
  }

  private void CreateRibbonBarCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ConfigurationEditor_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ConfigurationEditor_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ConfigurationEditor_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void OnCheckedChanged(object sender, EventArgs e)
  {
    CheckEdit control1 = (CheckEdit) sender;
    if (UndoScope.IsUndoing((object) control1))
      return;
    CheckEditPosition control2 = new CheckEditPosition()
    {
      CheckEdit = control1,
      RowHandle = this.bandedGridView1.FocusedRowHandle,
      Column = this.bandedGridView1.FocusedColumn
    };
    bool newValue = control1.Checked;
    ValueChangeTriggerContext context = new ValueChangeTriggerContext((object) !newValue, (object) newValue, (object) control2, (IPerformUndo) this);
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ModificationPerformed, (TriggerContext) context));
    this.bandedGridView1.PostEditor();
  }

  public void RestoreValue(object control, object valueToRestore)
  {
    CheckEditPosition checkEditPosition = (CheckEditPosition) control;
    this.bandedGridView1.FocusedRowHandle = checkEditPosition.RowHandle;
    this.bandedGridView1.FocusedColumn = checkEditPosition.Column;
    checkEditPosition.CheckEdit.Focus();
    this.bandedGridView1.ShowEditor();
    checkEditPosition.CheckEdit.Checked = (bool) valueToRestore;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationEditor));
    this.gridControl1 = new GridControl();
    this.bandedGridView1 = new BandedGridView();
    this.gridBand1 = new GridBand();
    this.fieldColumn = new BandedGridColumn();
    this.activeColumn = new BandedGridColumn();
    this.activeCheckEdit = new RepositoryItemCheckEdit();
    this.gridBand2 = new GridBand();
    this.group1Column = new BandedGridColumn();
    this.group1CheckEdit = new RepositoryItemCheckEdit();
    this.group2Column = new BandedGridColumn();
    this.group2CheckEdit = new RepositoryItemCheckEdit();
    this.group3Column = new BandedGridColumn();
    this.group3CheckEdit = new RepositoryItemCheckEdit();
    this.repositoryItemGridLookUpEdit1 = new RepositoryItemGridLookUpEdit();
    this.repositoryItemGridLookUpEdit1View = new GridView();
    this.configField11Combobox = new DevExpressComboBoxEdit();
    this.layoutControl1 = new LayoutControl();
    this.patientIdConfiguration = new DevExpressComboBoxEdit();
    this.configField32Combobox = new DevExpressComboBoxEdit();
    this.configField31Combobox = new DevExpressComboBoxEdit();
    this.configField12Combobox = new DevExpressComboBoxEdit();
    this.configField22Combobox = new DevExpressComboBoxEdit();
    this.configField21Combobox = new DevExpressComboBoxEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.groupMandatoryGroupLevel = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlGroup4 = new LayoutControlGroup();
    this.layoutControlItem8 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem7 = new LayoutControlItem();
    this.gridControl1.BeginInit();
    this.bandedGridView1.BeginInit();
    this.activeCheckEdit.BeginInit();
    this.group1CheckEdit.BeginInit();
    this.group2CheckEdit.BeginInit();
    this.group3CheckEdit.BeginInit();
    this.repositoryItemGridLookUpEdit1.BeginInit();
    this.repositoryItemGridLookUpEdit1View.BeginInit();
    this.configField11Combobox.Properties.BeginInit();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.patientIdConfiguration.Properties.BeginInit();
    this.configField32Combobox.Properties.BeginInit();
    this.configField31Combobox.Properties.BeginInit();
    this.configField12Combobox.Properties.BeginInit();
    this.configField22Combobox.Properties.BeginInit();
    this.configField21Combobox.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.groupMandatoryGroupLevel.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlGroup4.BeginInit();
    this.layoutControlItem8.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem7.BeginInit();
    this.SuspendLayout();
    this.gridControl1.AccessibleDescription = (string) null;
    this.gridControl1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gridControl1, "gridControl1");
    this.gridControl1.BackgroundImage = (Image) null;
    this.gridControl1.EmbeddedNavigator.AccessibleDescription = (string) null;
    this.gridControl1.EmbeddedNavigator.AccessibleName = (string) null;
    this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = (DefaultBoolean) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip");
    this.gridControl1.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.Anchor");
    this.gridControl1.EmbeddedNavigator.BackgroundImage = (Image) null;
    this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout");
    this.gridControl1.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.ImeMode");
    this.gridControl1.EmbeddedNavigator.TextLocation = (NavigatorButtonsTextLocation) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.TextLocation");
    this.gridControl1.EmbeddedNavigator.ToolTip = componentResourceManager.GetString("gridControl1.EmbeddedNavigator.ToolTip");
    this.gridControl1.EmbeddedNavigator.ToolTipIconType = (ToolTipIconType) componentResourceManager.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType");
    this.gridControl1.EmbeddedNavigator.ToolTipTitle = componentResourceManager.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
    this.gridControl1.Font = (Font) null;
    this.gridControl1.MainView = (BaseView) this.bandedGridView1;
    this.gridControl1.Name = "gridControl1";
    this.gridControl1.RepositoryItems.AddRange(new RepositoryItem[5]
    {
      (RepositoryItem) this.activeCheckEdit,
      (RepositoryItem) this.group1CheckEdit,
      (RepositoryItem) this.group2CheckEdit,
      (RepositoryItem) this.group3CheckEdit,
      (RepositoryItem) this.repositoryItemGridLookUpEdit1
    });
    this.gridControl1.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.bandedGridView1
    });
    this.bandedGridView1.Bands.AddRange(new GridBand[2]
    {
      this.gridBand1,
      this.gridBand2
    });
    componentResourceManager.ApplyResources((object) this.bandedGridView1, "bandedGridView1");
    this.bandedGridView1.Columns.AddRange(new BandedGridColumn[5]
    {
      this.fieldColumn,
      this.activeColumn,
      this.group1Column,
      this.group2Column,
      this.group3Column
    });
    this.bandedGridView1.CustomizationFormBounds = new Rectangle(1002, 545, 216, 178);
    this.bandedGridView1.GridControl = this.gridControl1;
    this.bandedGridView1.Name = "bandedGridView1";
    this.bandedGridView1.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.gridBand1, "gridBand1");
    this.gridBand1.Columns.Add(this.fieldColumn);
    this.gridBand1.Columns.Add(this.activeColumn);
    componentResourceManager.ApplyResources((object) this.fieldColumn, "fieldColumn");
    this.fieldColumn.FieldName = "FieldName";
    this.fieldColumn.Name = "fieldColumn";
    this.fieldColumn.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.activeColumn, "activeColumn");
    this.activeColumn.ColumnEdit = (RepositoryItem) this.activeCheckEdit;
    this.activeColumn.FieldName = "IsActive";
    this.activeColumn.Name = "activeColumn";
    this.activeCheckEdit.AccessibleDescription = (string) null;
    this.activeCheckEdit.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.activeCheckEdit, "activeCheckEdit");
    this.activeCheckEdit.Name = "activeCheckEdit";
    this.activeCheckEdit.CheckedChanged += new EventHandler(this.OnCheckedChanged);
    componentResourceManager.ApplyResources((object) this.gridBand2, "gridBand2");
    this.gridBand2.Columns.Add(this.group1Column);
    this.gridBand2.Columns.Add(this.group2Column);
    this.gridBand2.Columns.Add(this.group3Column);
    componentResourceManager.ApplyResources((object) this.group1Column, "group1Column");
    this.group1Column.ColumnEdit = (RepositoryItem) this.group1CheckEdit;
    this.group1Column.FieldName = "IsInMandatoryGroup1";
    this.group1Column.Name = "group1Column";
    this.group1CheckEdit.AccessibleDescription = (string) null;
    this.group1CheckEdit.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.group1CheckEdit, "group1CheckEdit");
    this.group1CheckEdit.Name = "group1CheckEdit";
    this.group1CheckEdit.CheckedChanged += new EventHandler(this.OnCheckedChanged);
    componentResourceManager.ApplyResources((object) this.group2Column, "group2Column");
    this.group2Column.ColumnEdit = (RepositoryItem) this.group2CheckEdit;
    this.group2Column.FieldName = "IsInMandatoryGroup2";
    this.group2Column.Name = "group2Column";
    this.group2CheckEdit.AccessibleDescription = (string) null;
    this.group2CheckEdit.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.group2CheckEdit, "group2CheckEdit");
    this.group2CheckEdit.Name = "group2CheckEdit";
    this.group2CheckEdit.CheckedChanged += new EventHandler(this.OnCheckedChanged);
    componentResourceManager.ApplyResources((object) this.group3Column, "group3Column");
    this.group3Column.ColumnEdit = (RepositoryItem) this.group3CheckEdit;
    this.group3Column.FieldName = "IsInMandatoryGroup3";
    this.group3Column.Name = "group3Column";
    this.group3CheckEdit.AccessibleDescription = (string) null;
    this.group3CheckEdit.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.group3CheckEdit, "group3CheckEdit");
    this.group3CheckEdit.Name = "group3CheckEdit";
    this.group3CheckEdit.CheckedChanged += new EventHandler(this.OnCheckedChanged);
    this.repositoryItemGridLookUpEdit1.AccessibleDescription = (string) null;
    this.repositoryItemGridLookUpEdit1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.repositoryItemGridLookUpEdit1, "repositoryItemGridLookUpEdit1");
    this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("repositoryItemGridLookUpEdit1.Buttons"))
    });
    this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
    this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
    componentResourceManager.ApplyResources((object) this.repositoryItemGridLookUpEdit1View, "repositoryItemGridLookUpEdit1View");
    this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
    this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
    this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.configField11Combobox, "configField11Combobox");
    this.configField11Combobox.BackgroundImage = (Image) null;
    this.configField11Combobox.EditValue = (object) null;
    this.configField11Combobox.EnterMoveNextControl = true;
    this.configField11Combobox.FormatString = (string) null;
    this.configField11Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField11Combobox.IsReadOnly = false;
    this.configField11Combobox.IsUndoing = false;
    this.configField11Combobox.Name = "configField11Combobox";
    this.configField11Combobox.Properties.AccessibleDescription = (string) null;
    this.configField11Combobox.Properties.AccessibleName = (string) null;
    this.configField11Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField11Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField11Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField11Combobox.Properties.AutoHeight");
    this.configField11Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField11Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField11Combobox.Properties.Buttons"))
    });
    this.configField11Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField11Combobox.Properties.NullValuePrompt");
    this.configField11Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField11Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField11Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField11Combobox.ShowEmptyElement = true;
    this.configField11Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField11Combobox.Validator = (ICustomValidator) null;
    this.configField11Combobox.Value = (object) null;
    this.layoutControl1.AccessibleDescription = (string) null;
    this.layoutControl1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.BackgroundImage = (Image) null;
    this.layoutControl1.Controls.Add((Control) this.patientIdConfiguration);
    this.layoutControl1.Controls.Add((Control) this.gridControl1);
    this.layoutControl1.Controls.Add((Control) this.configField32Combobox);
    this.layoutControl1.Controls.Add((Control) this.configField11Combobox);
    this.layoutControl1.Controls.Add((Control) this.configField31Combobox);
    this.layoutControl1.Controls.Add((Control) this.configField12Combobox);
    this.layoutControl1.Controls.Add((Control) this.configField22Combobox);
    this.layoutControl1.Controls.Add((Control) this.configField21Combobox);
    this.layoutControl1.Font = (Font) null;
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.patientIdConfiguration, "patientIdConfiguration");
    this.patientIdConfiguration.BackgroundImage = (Image) null;
    this.patientIdConfiguration.EditValue = (object) null;
    this.patientIdConfiguration.EnterMoveNextControl = true;
    this.patientIdConfiguration.FormatString = (string) null;
    this.patientIdConfiguration.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientIdConfiguration.IsMandatory = true;
    this.patientIdConfiguration.IsReadOnly = false;
    this.patientIdConfiguration.IsUndoing = false;
    this.patientIdConfiguration.Name = "patientIdConfiguration";
    this.patientIdConfiguration.Properties.AccessibleDescription = (string) null;
    this.patientIdConfiguration.Properties.AccessibleName = (string) null;
    this.patientIdConfiguration.Properties.Appearance.BackColor = Color.LightYellow;
    this.patientIdConfiguration.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientIdConfiguration.Properties.Appearance.Options.UseBackColor = true;
    this.patientIdConfiguration.Properties.Appearance.Options.UseBorderColor = true;
    this.patientIdConfiguration.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientIdConfiguration.Properties.AutoHeight");
    this.patientIdConfiguration.Properties.BorderStyle = BorderStyles.Simple;
    this.patientIdConfiguration.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientIdConfiguration.Properties.Buttons"))
    });
    this.patientIdConfiguration.Properties.NullValuePrompt = componentResourceManager.GetString("patientIdConfiguration.Properties.NullValuePrompt");
    this.patientIdConfiguration.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientIdConfiguration.Properties.NullValuePromptShowForEmptyValue");
    this.patientIdConfiguration.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientIdConfiguration.ShowEmptyElement = true;
    this.patientIdConfiguration.StyleController = (IStyleController) this.layoutControl1;
    this.patientIdConfiguration.Validator = (ICustomValidator) null;
    this.patientIdConfiguration.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.configField32Combobox, "configField32Combobox");
    this.configField32Combobox.BackgroundImage = (Image) null;
    this.configField32Combobox.EditValue = (object) null;
    this.configField32Combobox.EnterMoveNextControl = true;
    this.configField32Combobox.FormatString = (string) null;
    this.configField32Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField32Combobox.IsReadOnly = false;
    this.configField32Combobox.IsUndoing = false;
    this.configField32Combobox.Name = "configField32Combobox";
    this.configField32Combobox.Properties.AccessibleDescription = (string) null;
    this.configField32Combobox.Properties.AccessibleName = (string) null;
    this.configField32Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField32Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField32Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField32Combobox.Properties.AutoHeight");
    this.configField32Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField32Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField32Combobox.Properties.Buttons"))
    });
    this.configField32Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField32Combobox.Properties.NullValuePrompt");
    this.configField32Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField32Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField32Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField32Combobox.ShowEmptyElement = true;
    this.configField32Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField32Combobox.Validator = (ICustomValidator) null;
    this.configField32Combobox.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.configField31Combobox, "configField31Combobox");
    this.configField31Combobox.BackgroundImage = (Image) null;
    this.configField31Combobox.EditValue = (object) null;
    this.configField31Combobox.EnterMoveNextControl = true;
    this.configField31Combobox.FormatString = (string) null;
    this.configField31Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField31Combobox.IsReadOnly = false;
    this.configField31Combobox.IsUndoing = false;
    this.configField31Combobox.Name = "configField31Combobox";
    this.configField31Combobox.Properties.AccessibleDescription = (string) null;
    this.configField31Combobox.Properties.AccessibleName = (string) null;
    this.configField31Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField31Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField31Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField31Combobox.Properties.AutoHeight");
    this.configField31Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField31Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField31Combobox.Properties.Buttons"))
    });
    this.configField31Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField31Combobox.Properties.NullValuePrompt");
    this.configField31Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField31Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField31Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField31Combobox.ShowEmptyElement = true;
    this.configField31Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField31Combobox.Validator = (ICustomValidator) null;
    this.configField31Combobox.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.configField12Combobox, "configField12Combobox");
    this.configField12Combobox.BackgroundImage = (Image) null;
    this.configField12Combobox.EditValue = (object) null;
    this.configField12Combobox.EnterMoveNextControl = true;
    this.configField12Combobox.FormatString = (string) null;
    this.configField12Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField12Combobox.IsReadOnly = false;
    this.configField12Combobox.IsUndoing = false;
    this.configField12Combobox.Name = "configField12Combobox";
    this.configField12Combobox.Properties.AccessibleDescription = (string) null;
    this.configField12Combobox.Properties.AccessibleName = (string) null;
    this.configField12Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField12Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField12Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField12Combobox.Properties.AutoHeight");
    this.configField12Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField12Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField12Combobox.Properties.Buttons"))
    });
    this.configField12Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField12Combobox.Properties.NullValuePrompt");
    this.configField12Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField12Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField12Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField12Combobox.ShowEmptyElement = true;
    this.configField12Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField12Combobox.Validator = (ICustomValidator) null;
    this.configField12Combobox.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.configField22Combobox, "configField22Combobox");
    this.configField22Combobox.BackgroundImage = (Image) null;
    this.configField22Combobox.EditValue = (object) null;
    this.configField22Combobox.EnterMoveNextControl = true;
    this.configField22Combobox.FormatString = (string) null;
    this.configField22Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField22Combobox.IsReadOnly = false;
    this.configField22Combobox.IsUndoing = false;
    this.configField22Combobox.Name = "configField22Combobox";
    this.configField22Combobox.Properties.AccessibleDescription = (string) null;
    this.configField22Combobox.Properties.AccessibleName = (string) null;
    this.configField22Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField22Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField22Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField22Combobox.Properties.AutoHeight");
    this.configField22Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField22Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField22Combobox.Properties.Buttons"))
    });
    this.configField22Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField22Combobox.Properties.NullValuePrompt");
    this.configField22Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField22Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField22Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField22Combobox.ShowEmptyElement = true;
    this.configField22Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField22Combobox.Validator = (ICustomValidator) null;
    this.configField22Combobox.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.configField21Combobox, "configField21Combobox");
    this.configField21Combobox.BackgroundImage = (Image) null;
    this.configField21Combobox.EditValue = (object) null;
    this.configField21Combobox.EnterMoveNextControl = true;
    this.configField21Combobox.FormatString = (string) null;
    this.configField21Combobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.configField21Combobox.IsReadOnly = false;
    this.configField21Combobox.IsUndoing = false;
    this.configField21Combobox.Name = "configField21Combobox";
    this.configField21Combobox.Properties.AccessibleDescription = (string) null;
    this.configField21Combobox.Properties.AccessibleName = (string) null;
    this.configField21Combobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.configField21Combobox.Properties.Appearance.Options.UseBorderColor = true;
    this.configField21Combobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("configField21Combobox.Properties.AutoHeight");
    this.configField21Combobox.Properties.BorderStyle = BorderStyles.Simple;
    this.configField21Combobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("configField21Combobox.Properties.Buttons"))
    });
    this.configField21Combobox.Properties.NullValuePrompt = componentResourceManager.GetString("configField21Combobox.Properties.NullValuePrompt");
    this.configField21Combobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("configField21Combobox.Properties.NullValuePromptShowForEmptyValue");
    this.configField21Combobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.configField21Combobox.ShowEmptyElement = true;
    this.configField21Combobox.StyleController = (IStyleController) this.layoutControl1;
    this.configField21Combobox.Validator = (ICustomValidator) null;
    this.configField21Combobox.Value = (object) null;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.groupMandatoryGroupLevel,
      (BaseLayoutItem) this.layoutControlGroup4,
      (BaseLayoutItem) this.layoutControlGroup3
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(1128, 696);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.groupMandatoryGroupLevel, "groupMandatoryGroupLevel");
    this.groupMandatoryGroupLevel.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.emptySpaceItem2
    });
    this.groupMandatoryGroupLevel.Location = new Point(0, 184);
    this.groupMandatoryGroupLevel.Name = "groupMandatoryGroupLevel";
    this.groupMandatoryGroupLevel.Size = new Size(1108, 492);
    this.layoutControlItem1.Control = (Control) this.gridControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(542, 448);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(542, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(542, 448);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup4, "layoutControlGroup4");
    this.layoutControlGroup4.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem8,
      (BaseLayoutItem) this.emptySpaceItem1
    });
    this.layoutControlGroup4.Location = new Point(0, 0);
    this.layoutControlGroup4.Name = "layoutControlGroup4";
    this.layoutControlGroup4.Size = new Size(1108, 68);
    this.layoutControlItem8.Control = (Control) this.patientIdConfiguration;
    componentResourceManager.ApplyResources((object) this.layoutControlItem8, "layoutControlItem8");
    this.layoutControlItem8.Location = new Point(0, 0);
    this.layoutControlItem8.Name = "layoutControlItem8";
    this.layoutControlItem8.Size = new Size(492, 24);
    this.layoutControlItem8.TextSize = new Size(102, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(492, 0);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(592, 24);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[6]
    {
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.layoutControlItem6,
      (BaseLayoutItem) this.layoutControlItem7
    });
    this.layoutControlGroup3.Location = new Point(0, 68);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(1108, 116);
    this.layoutControlItem2.Control = (Control) this.configField11Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(541, 24);
    this.layoutControlItem2.TextSize = new Size(102, 13);
    this.layoutControlItem3.Control = (Control) this.configField12Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(541, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(543, 24);
    this.layoutControlItem3.TextSize = new Size(102, 13);
    this.layoutControlItem4.Control = (Control) this.configField21Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 24);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(541, 24);
    this.layoutControlItem4.TextSize = new Size(102, 13);
    this.layoutControlItem5.Control = (Control) this.configField22Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(541, 24);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(543, 24);
    this.layoutControlItem5.TextSize = new Size(102, 13);
    this.layoutControlItem6.Control = (Control) this.configField31Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(0, 48 /*0x30*/);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(541, 24);
    this.layoutControlItem6.TextSize = new Size(102, 13);
    this.layoutControlItem7.Control = (Control) this.configField32Combobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem7, "layoutControlItem7");
    this.layoutControlItem7.Location = new Point(541, 48 /*0x30*/);
    this.layoutControlItem7.Name = "layoutControlItem7";
    this.layoutControlItem7.Size = new Size(543, 24);
    this.layoutControlItem7.TextSize = new Size(102, 13);
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.layoutControl1);
    this.Font = (Font) null;
    this.Name = nameof (ConfigurationEditor);
    this.gridControl1.EndInit();
    this.bandedGridView1.EndInit();
    this.activeCheckEdit.EndInit();
    this.group1CheckEdit.EndInit();
    this.group2CheckEdit.EndInit();
    this.group3CheckEdit.EndInit();
    this.repositoryItemGridLookUpEdit1.EndInit();
    this.repositoryItemGridLookUpEdit1View.EndInit();
    this.configField11Combobox.Properties.EndInit();
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.patientIdConfiguration.Properties.EndInit();
    this.configField32Combobox.Properties.EndInit();
    this.configField31Combobox.Properties.EndInit();
    this.configField12Combobox.Properties.EndInit();
    this.configField22Combobox.Properties.EndInit();
    this.configField21Combobox.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.groupMandatoryGroupLevel.EndInit();
    this.layoutControlItem1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlGroup4.EndInit();
    this.layoutControlItem8.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlGroup3.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem7.EndInit();
    this.ResumeLayout(false);
  }
}
