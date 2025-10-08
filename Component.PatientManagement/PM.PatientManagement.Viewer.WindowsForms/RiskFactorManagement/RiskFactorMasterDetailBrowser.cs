// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.RiskFactorManagement.RiskFactorMasterDetailBrowser
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.RiskFactorManagement;

public sealed class RiskFactorMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View, IPerformUndo
{
  private readonly DevExpressSingleSelectionGridViewHelper<RiskIndicator> riskInidicatorGridHelper;
  private ModelMapper<RiskIndicator> modelMapper;
  private bool processingLangaugeChange;
  private IContainer components;
  private LayoutControl rootLayoutControl;
  private GridControl riskIndicatorGrid;
  private GridView riskIndicatorGridView;
  private LayoutControlGroup rootLayoutControlGroup;
  private LayoutControlItem layoutRiskIndicatorGrid;
  private DevExpressTextEdit riskIndicatorName;
  private LayoutControlGroup layoutRiskIndicatorBasicGroup;
  private LayoutControlItem layoutRiskIndicatorName;
  private GridColumn gridColumnRiskIndicatorName;
  private DevExpressMemoEdit riskIndicatorDescription;
  private LayoutControlItem layoutRiskIndicatorDescription;
  private DevExpressComboBoxEdit riskIndicatorActive;
  private LayoutControlItem layoutRiskIndicatorActive;
  private DevExpressComboBoxEdit translationLanguage;
  private LayoutControlGroup layoutTranslationGroup;
  private LayoutControlItem layoutTranslationLanguage;
  private DevExpressMemoEdit translationDescription;
  private DevExpressTextEdit translationName;
  private LayoutControlItem layoutTranslationName;
  private LayoutControlItem layoutTranslationDescription;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlGroup layoutGroupRiskIndicator;

  public RiskFactorMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
  }

  public RiskFactorMasterDetailBrowser(IModel model)
    : this()
  {
    this.riskInidicatorGridHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<RiskIndicator>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.riskIndicatorGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.RiskFactorMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.RiskFactorMasterDetailBrowser_RibbonMaintenanceGroup, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskAdd") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskEdit") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskDelete") as Bitmap));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.RiskFactorMasterDetailBrowser_RibbonModifactionGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.RiskFactorMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && e.Type == typeof (RiskIndicator) && !e.IsList)
      this.FillFields(e.ChangedObject as RiskIndicator);
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.Type == typeof (RiskIndicator)) || !e.IsList || !(e.ChangedObject is IEnumerable<RiskIndicator> changedObject) || changedObject.Count<RiskIndicator>() <= 0)
      return;
    this.FillFields(changedObject.FirstOrDefault<RiskIndicator>());
  }

  private void FillFields(RiskIndicator riskIndicator)
  {
    this.InitializeSelectionValues();
    this.modelMapper.SetUIEnabled(riskIndicator != null && this.ViewMode != 0);
    try
    {
      this.processingLangaugeChange = true;
      this.modelMapper.CopyModelToUI(riskIndicator);
    }
    finally
    {
      this.processingLangaugeChange = false;
    }
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeSelectionValues()
  {
    this.translationLanguage.DataSource = (object) SystemConfigurationManager.Instance.OptionalLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl))).Distinct<ComboBoxEditItemWrapper>().OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
    this.riskIndicatorActive.DataSource = (object) ((IEnumerable<bool>) new bool[2]
    {
      true,
      false
    }).Select<bool, ComboBoxEditItemWrapper>((Func<bool, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl))).ToList<ComboBoxEditItemWrapper>();
  }

  private void InitializeModelMapper()
  {
    ModelMapper<RiskIndicator> modelMapper = new ModelMapper<RiskIndicator>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => r.Name), (object) this.riskIndicatorName);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => r.Description), (object) this.riskIndicatorDescription);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => (object) r.IsActive), (object) this.riskIndicatorActive);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => r.TranslationCulture), (object) this.translationLanguage);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => r.TranslationName), (object) this.translationName);
    modelMapper.Add((Expression<Func<RiskIndicator, object>>) (r => r.TranslationDescription), (object) this.translationDescription);
    this.modelMapper = modelMapper;
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  private void OnLanguageTranslationChanging(object sender, ChangingEventArgs e)
  {
    if (this.processingLangaugeChange || e.NewValue == null)
      return;
    if (e.NewValue.Equals(e.OldValue))
      return;
    try
    {
      this.processingLangaugeChange = true;
      if (!(e.NewValue is ComboBoxEditItemWrapper newValue))
        return;
      ChangeCultureTriggerContext context = new ChangeCultureTriggerContext(newValue.Value as CultureInfo);
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeLanguage, (TriggerContext) context));
    }
    finally
    {
      this.processingLangaugeChange = false;
    }
  }

  public void RestoreValue(object control, object valueToRestore)
  {
    if (control == null || !(control is DevExpressComboBoxEdit))
      return;
    ((BaseEdit) control).EditValue = valueToRestore;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RiskFactorMasterDetailBrowser));
    this.rootLayoutControl = new LayoutControl();
    this.translationDescription = new DevExpressMemoEdit();
    this.translationName = new DevExpressTextEdit();
    this.translationLanguage = new DevExpressComboBoxEdit();
    this.riskIndicatorActive = new DevExpressComboBoxEdit();
    this.riskIndicatorDescription = new DevExpressMemoEdit();
    this.riskIndicatorName = new DevExpressTextEdit();
    this.riskIndicatorGrid = new GridControl();
    this.riskIndicatorGridView = new GridView();
    this.gridColumnRiskIndicatorName = new GridColumn();
    this.rootLayoutControlGroup = new LayoutControlGroup();
    this.layoutRiskIndicatorBasicGroup = new LayoutControlGroup();
    this.layoutRiskIndicatorName = new LayoutControlItem();
    this.layoutRiskIndicatorDescription = new LayoutControlItem();
    this.layoutRiskIndicatorActive = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutTranslationGroup = new LayoutControlGroup();
    this.layoutTranslationLanguage = new LayoutControlItem();
    this.layoutTranslationName = new LayoutControlItem();
    this.layoutTranslationDescription = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutGroupRiskIndicator = new LayoutControlGroup();
    this.layoutRiskIndicatorGrid = new LayoutControlItem();
    this.rootLayoutControl.BeginInit();
    this.rootLayoutControl.SuspendLayout();
    this.translationDescription.Properties.BeginInit();
    this.translationName.Properties.BeginInit();
    this.translationLanguage.Properties.BeginInit();
    this.riskIndicatorActive.Properties.BeginInit();
    this.riskIndicatorDescription.Properties.BeginInit();
    this.riskIndicatorName.Properties.BeginInit();
    this.riskIndicatorGrid.BeginInit();
    this.riskIndicatorGridView.BeginInit();
    this.rootLayoutControlGroup.BeginInit();
    this.layoutRiskIndicatorBasicGroup.BeginInit();
    this.layoutRiskIndicatorName.BeginInit();
    this.layoutRiskIndicatorDescription.BeginInit();
    this.layoutRiskIndicatorActive.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutTranslationGroup.BeginInit();
    this.layoutTranslationLanguage.BeginInit();
    this.layoutTranslationName.BeginInit();
    this.layoutTranslationDescription.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutGroupRiskIndicator.BeginInit();
    this.layoutRiskIndicatorGrid.BeginInit();
    this.SuspendLayout();
    this.rootLayoutControl.Controls.Add((Control) this.translationDescription);
    this.rootLayoutControl.Controls.Add((Control) this.translationName);
    this.rootLayoutControl.Controls.Add((Control) this.translationLanguage);
    this.rootLayoutControl.Controls.Add((Control) this.riskIndicatorActive);
    this.rootLayoutControl.Controls.Add((Control) this.riskIndicatorDescription);
    this.rootLayoutControl.Controls.Add((Control) this.riskIndicatorName);
    this.rootLayoutControl.Controls.Add((Control) this.riskIndicatorGrid);
    componentResourceManager.ApplyResources((object) this.rootLayoutControl, "rootLayoutControl");
    this.rootLayoutControl.Name = "rootLayoutControl";
    this.rootLayoutControl.Root = this.rootLayoutControlGroup;
    componentResourceManager.ApplyResources((object) this.translationDescription, "translationDescription");
    this.translationDescription.FormatString = (string) null;
    this.translationDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.translationDescription.IsReadOnly = false;
    this.translationDescription.IsUndoing = false;
    this.translationDescription.Name = "translationDescription";
    this.translationDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.translationDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.translationDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.translationDescription.Properties.LinesCount = 4;
    this.translationDescription.StyleController = (IStyleController) this.rootLayoutControl;
    this.translationDescription.Validator = (ICustomValidator) null;
    this.translationDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.translationName, "translationName");
    this.translationName.EnterMoveNextControl = true;
    this.translationName.FormatString = (string) null;
    this.translationName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.translationName.IsReadOnly = false;
    this.translationName.IsUndoing = false;
    this.translationName.Name = "translationName";
    this.translationName.Properties.Appearance.BorderColor = Color.Yellow;
    this.translationName.Properties.Appearance.Options.UseBorderColor = true;
    this.translationName.Properties.BorderStyle = BorderStyles.Simple;
    this.translationName.StyleController = (IStyleController) this.rootLayoutControl;
    this.translationName.Validator = (ICustomValidator) null;
    this.translationName.Value = (object) "";
    this.translationLanguage.EnterMoveNextControl = true;
    this.translationLanguage.FormatString = (string) null;
    this.translationLanguage.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.translationLanguage.IsNavigationOnly = true;
    this.translationLanguage.IsReadOnly = false;
    this.translationLanguage.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.translationLanguage, "translationLanguage");
    this.translationLanguage.Name = "translationLanguage";
    this.translationLanguage.Properties.Appearance.BorderColor = Color.LightGray;
    this.translationLanguage.Properties.Appearance.Options.UseBorderColor = true;
    this.translationLanguage.Properties.BorderStyle = BorderStyles.Simple;
    this.translationLanguage.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("translationLanguage.Properties.Buttons"))
    });
    this.translationLanguage.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.translationLanguage.ShowEmptyElement = false;
    this.translationLanguage.ShowModified = false;
    this.translationLanguage.StyleController = (IStyleController) this.rootLayoutControl;
    this.translationLanguage.Validator = (ICustomValidator) null;
    this.translationLanguage.Value = (object) null;
    this.translationLanguage.EditValueChanging += new ChangingEventHandler(this.OnLanguageTranslationChanging);
    this.riskIndicatorActive.EnterMoveNextControl = true;
    this.riskIndicatorActive.FormatString = (string) null;
    this.riskIndicatorActive.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorActive.IsReadOnly = false;
    this.riskIndicatorActive.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.riskIndicatorActive, "riskIndicatorActive");
    this.riskIndicatorActive.Name = "riskIndicatorActive";
    this.riskIndicatorActive.Properties.Appearance.BorderColor = Color.LightGray;
    this.riskIndicatorActive.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorActive.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorActive.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("riskIndicatorActive.Properties.Buttons"))
    });
    this.riskIndicatorActive.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riskIndicatorActive.ShowEmptyElement = false;
    this.riskIndicatorActive.StyleController = (IStyleController) this.rootLayoutControl;
    this.riskIndicatorActive.Validator = (ICustomValidator) null;
    this.riskIndicatorActive.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.riskIndicatorDescription, "riskIndicatorDescription");
    this.riskIndicatorDescription.FormatString = (string) null;
    this.riskIndicatorDescription.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorDescription.IsReadOnly = false;
    this.riskIndicatorDescription.IsUndoing = false;
    this.riskIndicatorDescription.Name = "riskIndicatorDescription";
    this.riskIndicatorDescription.Properties.Appearance.BorderColor = Color.Yellow;
    this.riskIndicatorDescription.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorDescription.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorDescription.Properties.LinesCount = 4;
    this.riskIndicatorDescription.StyleController = (IStyleController) this.rootLayoutControl;
    this.riskIndicatorDescription.Validator = (ICustomValidator) null;
    this.riskIndicatorDescription.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.riskIndicatorName, "riskIndicatorName");
    this.riskIndicatorName.EnterMoveNextControl = true;
    this.riskIndicatorName.FormatString = (string) null;
    this.riskIndicatorName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.riskIndicatorName.IsMandatory = true;
    this.riskIndicatorName.IsReadOnly = false;
    this.riskIndicatorName.IsUndoing = false;
    this.riskIndicatorName.Name = "riskIndicatorName";
    this.riskIndicatorName.Properties.Appearance.BackColor = Color.LightYellow;
    this.riskIndicatorName.Properties.Appearance.BorderColor = Color.Yellow;
    this.riskIndicatorName.Properties.Appearance.Options.UseBackColor = true;
    this.riskIndicatorName.Properties.Appearance.Options.UseBorderColor = true;
    this.riskIndicatorName.Properties.BorderStyle = BorderStyles.Simple;
    this.riskIndicatorName.StyleController = (IStyleController) this.rootLayoutControl;
    this.riskIndicatorName.Validator = (ICustomValidator) null;
    this.riskIndicatorName.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.riskIndicatorGrid, "riskIndicatorGrid");
    this.riskIndicatorGrid.MainView = (BaseView) this.riskIndicatorGridView;
    this.riskIndicatorGrid.Name = "riskIndicatorGrid";
    this.riskIndicatorGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.riskIndicatorGridView
    });
    this.riskIndicatorGridView.Columns.AddRange(new GridColumn[1]
    {
      this.gridColumnRiskIndicatorName
    });
    this.riskIndicatorGridView.GridControl = this.riskIndicatorGrid;
    this.riskIndicatorGridView.Name = "riskIndicatorGridView";
    this.riskIndicatorGridView.OptionsBehavior.Editable = false;
    this.riskIndicatorGridView.OptionsCustomization.AllowFilter = false;
    this.riskIndicatorGridView.OptionsFilter.AllowFilterEditor = false;
    this.riskIndicatorGridView.OptionsMenu.EnableColumnMenu = false;
    this.riskIndicatorGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.riskIndicatorGridView.OptionsView.ShowDetailButtons = false;
    this.riskIndicatorGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.gridColumnRiskIndicatorName, "gridColumnRiskIndicatorName");
    this.gridColumnRiskIndicatorName.FieldName = "Name";
    this.gridColumnRiskIndicatorName.Name = "gridColumnRiskIndicatorName";
    this.rootLayoutControlGroup.AppearanceItemCaption.Options.UseTextOptions = true;
    this.rootLayoutControlGroup.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.rootLayoutControlGroup, "rootLayoutControlGroup");
    this.rootLayoutControlGroup.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.rootLayoutControlGroup.GroupBordersVisible = false;
    this.rootLayoutControlGroup.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorBasicGroup,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutTranslationGroup,
      (BaseLayoutItem) this.layoutGroupRiskIndicator
    });
    this.rootLayoutControlGroup.Location = new Point(0, 0);
    this.rootLayoutControlGroup.Name = "rootLayoutControlGroup";
    this.rootLayoutControlGroup.Size = new Size(857, 676);
    this.rootLayoutControlGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.rootLayoutControlGroup.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorBasicGroup, "layoutRiskIndicatorBasicGroup");
    this.layoutRiskIndicatorBasicGroup.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorName,
      (BaseLayoutItem) this.layoutRiskIndicatorDescription,
      (BaseLayoutItem) this.layoutRiskIndicatorActive
    });
    this.layoutRiskIndicatorBasicGroup.Location = new Point(415, 0);
    this.layoutRiskIndicatorBasicGroup.Name = "layoutRiskIndicatorBasicGroup";
    this.layoutRiskIndicatorBasicGroup.Size = new Size(422, 209);
    this.layoutRiskIndicatorName.Control = (Control) this.riskIndicatorName;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorName, "layoutRiskIndicatorName");
    this.layoutRiskIndicatorName.Location = new Point(0, 0);
    this.layoutRiskIndicatorName.Name = "layoutRiskIndicatorName";
    this.layoutRiskIndicatorName.Size = new Size(398, 24);
    this.layoutRiskIndicatorName.TextSize = new Size(53, 13);
    this.layoutRiskIndicatorDescription.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutRiskIndicatorDescription.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutRiskIndicatorDescription.Control = (Control) this.riskIndicatorDescription;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorDescription, "layoutRiskIndicatorDescription");
    this.layoutRiskIndicatorDescription.Location = new Point(0, 24);
    this.layoutRiskIndicatorDescription.Name = "layoutRiskIndicatorDescription";
    this.layoutRiskIndicatorDescription.Size = new Size(398, 117);
    this.layoutRiskIndicatorDescription.TextSize = new Size(53, 13);
    this.layoutRiskIndicatorActive.Control = (Control) this.riskIndicatorActive;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorActive, "layoutRiskIndicatorActive");
    this.layoutRiskIndicatorActive.Location = new Point(0, 141);
    this.layoutRiskIndicatorActive.Name = "layoutRiskIndicatorActive";
    this.layoutRiskIndicatorActive.Size = new Size(398, 24);
    this.layoutRiskIndicatorActive.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(415, 461);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(422, 195);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutTranslationGroup, "layoutTranslationGroup");
    this.layoutTranslationGroup.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutTranslationLanguage,
      (BaseLayoutItem) this.layoutTranslationName,
      (BaseLayoutItem) this.layoutTranslationDescription,
      (BaseLayoutItem) this.emptySpaceItem1
    });
    this.layoutTranslationGroup.Location = new Point(415, 209);
    this.layoutTranslationGroup.Name = "layoutTranslationGroup";
    this.layoutTranslationGroup.Size = new Size(422, 252);
    this.layoutTranslationLanguage.Control = (Control) this.translationLanguage;
    componentResourceManager.ApplyResources((object) this.layoutTranslationLanguage, "layoutTranslationLanguage");
    this.layoutTranslationLanguage.Location = new Point(0, 0);
    this.layoutTranslationLanguage.Name = "layoutTranslationLanguage";
    this.layoutTranslationLanguage.Size = new Size(398, 24);
    this.layoutTranslationLanguage.TextSize = new Size(53, 13);
    this.layoutTranslationName.Control = (Control) this.translationName;
    componentResourceManager.ApplyResources((object) this.layoutTranslationName, "layoutTranslationName");
    this.layoutTranslationName.Location = new Point(0, 50);
    this.layoutTranslationName.Name = "layoutTranslationName";
    this.layoutTranslationName.Size = new Size(398, 24);
    this.layoutTranslationName.TextSize = new Size(53, 13);
    this.layoutTranslationDescription.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutTranslationDescription.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutTranslationDescription.Control = (Control) this.translationDescription;
    componentResourceManager.ApplyResources((object) this.layoutTranslationDescription, "layoutTranslationDescription");
    this.layoutTranslationDescription.Location = new Point(0, 74);
    this.layoutTranslationDescription.Name = "layoutTranslationDescription";
    this.layoutTranslationDescription.Size = new Size(398, 134);
    this.layoutTranslationDescription.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(398, 26);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutGroupRiskIndicator, "layoutGroupRiskIndicator");
    this.layoutGroupRiskIndicator.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutRiskIndicatorGrid
    });
    this.layoutGroupRiskIndicator.Location = new Point(0, 0);
    this.layoutGroupRiskIndicator.Name = "layoutGroupRiskIndicator";
    this.layoutGroupRiskIndicator.Size = new Size(415, 656);
    this.layoutRiskIndicatorGrid.Control = (Control) this.riskIndicatorGrid;
    componentResourceManager.ApplyResources((object) this.layoutRiskIndicatorGrid, "layoutRiskIndicatorGrid");
    this.layoutRiskIndicatorGrid.Location = new Point(0, 0);
    this.layoutRiskIndicatorGrid.Name = "layoutRiskIndicatorGrid";
    this.layoutRiskIndicatorGrid.ShowInCustomizationForm = false;
    this.layoutRiskIndicatorGrid.Size = new Size(391, 612);
    this.layoutRiskIndicatorGrid.TextSize = new Size(0, 0);
    this.layoutRiskIndicatorGrid.TextToControlDistance = 0;
    this.layoutRiskIndicatorGrid.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rootLayoutControl);
    this.Name = nameof (RiskFactorMasterDetailBrowser);
    this.rootLayoutControl.EndInit();
    this.rootLayoutControl.ResumeLayout(false);
    this.translationDescription.Properties.EndInit();
    this.translationName.Properties.EndInit();
    this.translationLanguage.Properties.EndInit();
    this.riskIndicatorActive.Properties.EndInit();
    this.riskIndicatorDescription.Properties.EndInit();
    this.riskIndicatorName.Properties.EndInit();
    this.riskIndicatorGrid.EndInit();
    this.riskIndicatorGridView.EndInit();
    this.rootLayoutControlGroup.EndInit();
    this.layoutRiskIndicatorBasicGroup.EndInit();
    this.layoutRiskIndicatorName.EndInit();
    this.layoutRiskIndicatorDescription.EndInit();
    this.layoutRiskIndicatorActive.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutTranslationGroup.EndInit();
    this.layoutTranslationLanguage.EndInit();
    this.layoutTranslationName.EndInit();
    this.layoutTranslationDescription.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutGroupRiskIndicator.EndInit();
    this.layoutRiskIndicatorGrid.EndInit();
    this.ResumeLayout(false);
  }
}
