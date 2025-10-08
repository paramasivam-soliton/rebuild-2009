// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser.InstrumentMasterDetailBrowser
// Assembly: PM.InstrumentManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 377C3581-C34D-4673-97B7-CC091DEDB55A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Viewer.WindowsForms.dll

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
using DevExpress.XtraTab;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.InstrumentManagement.Automaton;
using PathMedical.InstrumentManagement.Viewer.WindowsForms.Properties;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser;

[ToolboxItem(false)]
public sealed class InstrumentMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private RibbonHelper ribbonHelper;
  private RibbonPageGroup type1077ConfigGroup;
  private readonly DevExpressSingleSelectionGridViewHelper<Instrument> instrumentsMvcHelper;
  private readonly DevExpressSingleSelectionGridViewHelper<User> userMvcHelper;
  private ModelMapper<Instrument> modelMapper;
  private ModelMapper<User> userModelMapper;
  private IContainer components;
  private LayoutControl layoutInstrumentDetail;
  private LayoutControlGroup layoutInstrumentManagement;
  private GridControl instrumentGrid;
  private GridView instrumentGridView;
  private LayoutControlItem layoutInstrumentGrid;
  private GridColumn columnName;
  private GridColumn columnSerial;
  private GridColumn columnLastConnected;
  private XtraTabControl xtraTabControl1;
  private XtraTabPage configurationTab;
  private XtraTabPage userAssignmentTab;
  private LayoutControlItem layoutControlItem1;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutInstrumentConfiguration;
  private DevExpressTextEdit name;
  private LayoutControlItem layoutName;
  private DevExpressSpinEdit displayTimeout;
  private DevExpressComboBoxEdit language;
  private DevExpressSpinEdit powerTimeout;
  private DevExpressComboBoxEdit site;
  private DevExpressTextEdit code;
  private DevExpressTextEdit serial;
  private LayoutControlGroup layoutIndividualSettings;
  private LayoutControlItem layoutLanguage;
  private LayoutControlItem layoutSite;
  private LayoutControlItem layoutCode;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlItem layoutDisplayTimeout;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem12;
  private EmptySpaceItem emptySpaceItem4;
  private LayoutControlGroup layoutCommonConfiguration;
  private DevExpressComboBoxEdit deleteInstrumentDataRule;
  private DevExpressTextEdit lastUpdated;
  private DevExpressTextEdit lastConnected;
  private LayoutControlItem layoutDataDeleteInstrumentDataRule;
  private LayoutControlItem layoutLastSeen;
  private LayoutControlItem layoutLastUpdated;
  private DevExpressTextEdit firmwareVersion;
  private DevExpressTextEdit hardwareVersion;
  private LayoutControlGroup layoutSystemInformation;
  private LayoutControlItem layoutHardwareVersion;
  private LayoutControlItem layoutFirmwareVersion;
  private GridControl userGrid;
  private GridView userAssignmentView;
  private GridColumn columnLoginName;
  private GridColumn columnIsActive;
  private RepositoryItemCheckEdit flagCheckEdit;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlGroup layoutGroupInstruments;

  public InstrumentMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeFieldValues();
    this.InitializeModelMapper();
    this.HelpMarker = "instrument_mgmt_01.html";
  }

  private void InitializeFieldValues()
  {
    this.deleteInstrumentDataRule.DataSource = (object) new InstrumentDataDeletionRule[2]
    {
      InstrumentDataDeletionRule.Manual,
      InstrumentDataDeletionRule.AfterSuccessfullDownload
    };
    this.displayTimeout.Properties.MinValue = 1M;
    this.displayTimeout.Properties.MaxValue = 5M;
    this.displayTimeout.Properties.Increment = 1M;
    this.displayTimeout.Properties.EditMask = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.displayTimeout.Properties.DisplayFormat.FormatString = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.displayTimeout.Properties.DisplayFormat.FormatType = FormatType.Custom;
    this.displayTimeout.Properties.EditFormat.FormatString = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.displayTimeout.Properties.EditFormat.FormatType = FormatType.Custom;
    this.powerTimeout.Properties.MinValue = 2M;
    this.powerTimeout.Properties.MaxValue = 30M;
    this.powerTimeout.Properties.Increment = 1M;
    this.powerTimeout.Properties.EditMask = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.powerTimeout.Properties.DisplayFormat.FormatString = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.powerTimeout.Properties.DisplayFormat.FormatType = FormatType.Custom;
    this.powerTimeout.Properties.EditFormat.FormatString = Resources.InstrumentMasterDetailBrowser_TimeFormat;
    this.powerTimeout.Properties.EditFormat.FormatType = FormatType.Custom;
  }

  public InstrumentMasterDetailBrowser(IModel model)
    : this()
  {
    this.instrumentsMvcHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<Instrument>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.instrumentGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.userMvcHelper = new DevExpressSingleSelectionGridViewHelper<User>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.userAssignmentView, model, InstrumentManagementTriggers.SelectUser, "UsersOnInstrument");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new InstrumentMasterDetailBrowser.UpdateViewCallBack(this.UpdateView), (object) e.ChangeType, (object) e.Type, e.ChangedObject);
    else
      this.UpdateView(e.ChangeType, e.Type, e.ChangedObject);
  }

  private void UpdateView(ChangeType changeType, Type type, object item)
  {
    if ((changeType == ChangeType.SelectionChanged || changeType == ChangeType.ItemEdited || changeType == ChangeType.ItemAdded) && type == typeof (Instrument))
    {
      object obj = (object) (item as ICollection<Instrument>);
      if (obj == null)
        obj = (object) new Instrument[1]
        {
          item as Instrument
        };
      this.FillFields((ICollection<Instrument>) obj);
    }
    if (!(item is IEnumerable<PathMedical.SiteAndFacilityManagement.Site>) || changeType != ChangeType.ListLoaded)
      return;
    this.site.DataSource = (object) (item as IEnumerable<PathMedical.SiteAndFacilityManagement.Site>).Select<PathMedical.SiteAndFacilityManagement.Site, ComboBoxEditItemWrapper>((Func<PathMedical.SiteAndFacilityManagement.Site, ComboBoxEditItemWrapper>) (s => new ComboBoxEditItemWrapper(s.ToString(), (object) s))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  private void InitializeModelMapper()
  {
    bool isEditingEnabled = this.ViewMode != 0;
    ModelMapper<Instrument> modelMapper = new ModelMapper<Instrument>(isEditingEnabled);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.Name), (object) this.name);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.SerialNumber), (object) this.serial);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.Code), (object) this.code);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.Site), (object) this.site);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.Language), (object) this.language);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => (object) i.LastConnected), (object) this.lastConnected);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => (object) i.LastUpdated), (object) this.lastUpdated);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.HardwareVersion), (object) this.hardwareVersion);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => i.SoftwareVersion), (object) this.firmwareVersion);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => (object) i.GlobalInstrumentConfiguration.DisplayTimeout), (object) this.displayTimeout);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => (object) i.GlobalInstrumentConfiguration.PowerTimeout), (object) this.powerTimeout);
    modelMapper.Add((Expression<Func<Instrument, object>>) (i => (object) i.GlobalInstrumentConfiguration.DeletionRule), (object) this.deleteInstrumentDataRule);
    this.modelMapper = modelMapper;
    this.modelMapper.SetUIEnabledForced(false, (object) this.lastConnected, (object) this.lastUpdated, (object) this.hardwareVersion, (object) this.firmwareVersion, (object) this.language);
    this.userModelMapper = new ModelMapper<User>(isEditingEnabled);
    this.userModelMapper.SetUIEnabledForced(false);
  }

  private void CreateToolbarElements()
  {
    this.ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) this.ribbonHelper.CreateMaintenanceGroup(Resources.InstrumentMasterDetailBrowser_RibbonMaintanceGroup, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("InstrumentAdd"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("InstrumentEdit"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("InstrumentDelete")));
    this.ToolbarElements.Add((object) this.ribbonHelper.CreateModificationGroup(Resources.InstrumentMasterDetailBrowser_RibbonModificationGroup));
    this.type1077ConfigGroup = this.ribbonHelper.CreateRibbonPageGroup(Resources.InstrumentMasterDetailBrowser_RibbonPageGroup);
    foreach (ITestPlugin testPlugin in SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().Where<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.ConfigurationModuleType != (Type) null)))
      this.type1077ConfigGroup.ItemLinks.Add((BarItem) this.ribbonHelper.CreateLargeImageButton(ApplicationComponentModuleManager.Instance.Get(testPlugin.ConfigurationModuleType)));
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.FirmwareSearchModuleType != (Type) null || p.ConfigurationSynchronizationModuleType != (Type) null)))
    {
      if (instrumentPlugin.FirmwareSearchModuleType != (Type) null)
        this.type1077ConfigGroup.ItemLinks.Add((BarItem) this.ribbonHelper.CreateLargeImageButton(ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.FirmwareSearchModuleType)));
      if (instrumentPlugin.ConfigurationSynchronizationModuleType != (Type) null)
        this.type1077ConfigGroup.ItemLinks.Add((BarItem) this.ribbonHelper.CreateLargeImageButton(ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.ConfigurationSynchronizationModuleType)));
    }
    this.ToolbarElements.Add((object) this.type1077ConfigGroup);
    this.ToolbarElements.Add((object) this.ribbonHelper.CreateHelpGroup("Help", (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool isUIEnabled = this.ViewMode != 0;
    this.modelMapper.SetUIEnabled(isUIEnabled);
    this.userModelMapper.SetUIEnabled(isUIEnabled);
    this.userGrid.Enabled = isUIEnabled;
  }

  private void FillFields(ICollection<Instrument> instruments)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != ViewModeType.Viewing && instruments.FirstOrDefault<Instrument>() != null);
    this.modelMapper.CopyModelToUI(instruments);
  }

  public override void CopyUIToModel()
  {
    this.userModelMapper.CopyUIToModel();
    this.modelMapper.CopyUIToModel();
  }

  private void OnInstrumentUserAssignmentChanged(object sender, EventArgs e)
  {
    if (sender == null || !(sender is CheckEdit))
      return;
    CheckEdit control = sender as CheckEdit;
    if (UndoScope.IsUndoing((object) control))
      return;
    bool newValue = control.Checked;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.RefreshDataFromForm, (TriggerContext) new ValueChangeTriggerContext((object) null, (object) newValue)));
    this.userAssignmentView.PostEditor();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InstrumentMasterDetailBrowser));
    this.layoutInstrumentDetail = new LayoutControl();
    this.xtraTabControl1 = new XtraTabControl();
    this.configurationTab = new XtraTabPage();
    this.layoutControl1 = new LayoutControl();
    this.firmwareVersion = new DevExpressTextEdit();
    this.hardwareVersion = new DevExpressTextEdit();
    this.deleteInstrumentDataRule = new DevExpressComboBoxEdit();
    this.lastUpdated = new DevExpressTextEdit();
    this.lastConnected = new DevExpressTextEdit();
    this.displayTimeout = new DevExpressSpinEdit();
    this.language = new DevExpressComboBoxEdit();
    this.powerTimeout = new DevExpressSpinEdit();
    this.site = new DevExpressComboBoxEdit();
    this.code = new DevExpressTextEdit();
    this.serial = new DevExpressTextEdit();
    this.name = new DevExpressTextEdit();
    this.layoutInstrumentConfiguration = new LayoutControlGroup();
    this.layoutIndividualSettings = new LayoutControlGroup();
    this.layoutLanguage = new LayoutControlItem();
    this.layoutSite = new LayoutControlItem();
    this.layoutCode = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.layoutName = new LayoutControlItem();
    this.layoutCommonConfiguration = new LayoutControlGroup();
    this.layoutDisplayTimeout = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem12 = new LayoutControlItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutDataDeleteInstrumentDataRule = new LayoutControlItem();
    this.layoutSystemInformation = new LayoutControlGroup();
    this.layoutLastSeen = new LayoutControlItem();
    this.layoutLastUpdated = new LayoutControlItem();
    this.layoutHardwareVersion = new LayoutControlItem();
    this.layoutFirmwareVersion = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.userAssignmentTab = new XtraTabPage();
    this.userGrid = new GridControl();
    this.userAssignmentView = new GridView();
    this.columnLoginName = new GridColumn();
    this.columnIsActive = new GridColumn();
    this.flagCheckEdit = new RepositoryItemCheckEdit();
    this.instrumentGrid = new GridControl();
    this.instrumentGridView = new GridView();
    this.columnName = new GridColumn();
    this.columnSerial = new GridColumn();
    this.columnLastConnected = new GridColumn();
    this.layoutInstrumentManagement = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutGroupInstruments = new LayoutControlGroup();
    this.layoutInstrumentGrid = new LayoutControlItem();
    this.layoutInstrumentDetail.BeginInit();
    this.layoutInstrumentDetail.SuspendLayout();
    this.xtraTabControl1.BeginInit();
    this.xtraTabControl1.SuspendLayout();
    this.configurationTab.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.firmwareVersion.Properties.BeginInit();
    this.hardwareVersion.Properties.BeginInit();
    this.deleteInstrumentDataRule.Properties.BeginInit();
    this.lastUpdated.Properties.BeginInit();
    this.lastConnected.Properties.BeginInit();
    this.displayTimeout.Properties.BeginInit();
    this.language.Properties.BeginInit();
    this.powerTimeout.Properties.BeginInit();
    this.site.Properties.BeginInit();
    this.code.Properties.BeginInit();
    this.serial.Properties.BeginInit();
    this.name.Properties.BeginInit();
    this.layoutInstrumentConfiguration.BeginInit();
    this.layoutIndividualSettings.BeginInit();
    this.layoutLanguage.BeginInit();
    this.layoutSite.BeginInit();
    this.layoutCode.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.layoutName.BeginInit();
    this.layoutCommonConfiguration.BeginInit();
    this.layoutDisplayTimeout.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem12.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.layoutDataDeleteInstrumentDataRule.BeginInit();
    this.layoutSystemInformation.BeginInit();
    this.layoutLastSeen.BeginInit();
    this.layoutLastUpdated.BeginInit();
    this.layoutHardwareVersion.BeginInit();
    this.layoutFirmwareVersion.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.userAssignmentTab.SuspendLayout();
    this.userGrid.BeginInit();
    this.userAssignmentView.BeginInit();
    this.flagCheckEdit.BeginInit();
    this.instrumentGrid.BeginInit();
    this.instrumentGridView.BeginInit();
    this.layoutInstrumentManagement.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutGroupInstruments.BeginInit();
    this.layoutInstrumentGrid.BeginInit();
    this.SuspendLayout();
    this.layoutInstrumentDetail.Controls.Add((Control) this.xtraTabControl1);
    this.layoutInstrumentDetail.Controls.Add((Control) this.instrumentGrid);
    componentResourceManager.ApplyResources((object) this.layoutInstrumentDetail, "layoutInstrumentDetail");
    this.layoutInstrumentDetail.Name = "layoutInstrumentDetail";
    this.layoutInstrumentDetail.Root = this.layoutInstrumentManagement;
    componentResourceManager.ApplyResources((object) this.xtraTabControl1, "xtraTabControl1");
    this.xtraTabControl1.Name = "xtraTabControl1";
    this.xtraTabControl1.SelectedTabPage = this.configurationTab;
    this.xtraTabControl1.TabPages.AddRange(new XtraTabPage[2]
    {
      this.configurationTab,
      this.userAssignmentTab
    });
    this.configurationTab.Controls.Add((Control) this.layoutControl1);
    this.configurationTab.Name = "configurationTab";
    componentResourceManager.ApplyResources((object) this.configurationTab, "configurationTab");
    this.layoutControl1.Controls.Add((Control) this.firmwareVersion);
    this.layoutControl1.Controls.Add((Control) this.hardwareVersion);
    this.layoutControl1.Controls.Add((Control) this.deleteInstrumentDataRule);
    this.layoutControl1.Controls.Add((Control) this.lastUpdated);
    this.layoutControl1.Controls.Add((Control) this.lastConnected);
    this.layoutControl1.Controls.Add((Control) this.displayTimeout);
    this.layoutControl1.Controls.Add((Control) this.language);
    this.layoutControl1.Controls.Add((Control) this.powerTimeout);
    this.layoutControl1.Controls.Add((Control) this.site);
    this.layoutControl1.Controls.Add((Control) this.code);
    this.layoutControl1.Controls.Add((Control) this.serial);
    this.layoutControl1.Controls.Add((Control) this.name);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutInstrumentConfiguration;
    componentResourceManager.ApplyResources((object) this.firmwareVersion, "firmwareVersion");
    this.firmwareVersion.EnterMoveNextControl = true;
    this.firmwareVersion.FormatString = (string) null;
    this.firmwareVersion.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.firmwareVersion.IsReadOnly = false;
    this.firmwareVersion.IsUndoing = false;
    this.firmwareVersion.Name = "firmwareVersion";
    this.firmwareVersion.Properties.Appearance.BorderColor = Color.Yellow;
    this.firmwareVersion.Properties.Appearance.Options.UseBorderColor = true;
    this.firmwareVersion.Properties.BorderStyle = BorderStyles.Simple;
    this.firmwareVersion.Properties.Mask.EditMask = componentResourceManager.GetString("firmwareVersion.Properties.Mask.EditMask");
    this.firmwareVersion.StyleController = (IStyleController) this.layoutControl1;
    this.firmwareVersion.Validator = (ICustomValidator) null;
    this.firmwareVersion.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.hardwareVersion, "hardwareVersion");
    this.hardwareVersion.EnterMoveNextControl = true;
    this.hardwareVersion.FormatString = (string) null;
    this.hardwareVersion.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.hardwareVersion.IsReadOnly = false;
    this.hardwareVersion.IsUndoing = false;
    this.hardwareVersion.Name = "hardwareVersion";
    this.hardwareVersion.Properties.Appearance.BorderColor = Color.Yellow;
    this.hardwareVersion.Properties.Appearance.Options.UseBorderColor = true;
    this.hardwareVersion.Properties.BorderStyle = BorderStyles.Simple;
    this.hardwareVersion.Properties.Mask.EditMask = componentResourceManager.GetString("hardwareVersion.Properties.Mask.EditMask");
    this.hardwareVersion.StyleController = (IStyleController) this.layoutControl1;
    this.hardwareVersion.Validator = (ICustomValidator) null;
    this.hardwareVersion.Value = (object) "";
    this.deleteInstrumentDataRule.EnterMoveNextControl = true;
    this.deleteInstrumentDataRule.FormatString = (string) null;
    this.deleteInstrumentDataRule.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.deleteInstrumentDataRule.IsReadOnly = false;
    this.deleteInstrumentDataRule.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.deleteInstrumentDataRule, "deleteInstrumentDataRule");
    this.deleteInstrumentDataRule.Name = "deleteInstrumentDataRule";
    this.deleteInstrumentDataRule.Properties.Appearance.BorderColor = Color.LightGray;
    this.deleteInstrumentDataRule.Properties.Appearance.Options.UseBorderColor = true;
    this.deleteInstrumentDataRule.Properties.BorderStyle = BorderStyles.Simple;
    this.deleteInstrumentDataRule.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("deleteInstrumentDataRule.Properties.Buttons"))
    });
    this.deleteInstrumentDataRule.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.deleteInstrumentDataRule.ShowEmptyElement = true;
    this.deleteInstrumentDataRule.StyleController = (IStyleController) this.layoutControl1;
    this.deleteInstrumentDataRule.Validator = (ICustomValidator) null;
    this.deleteInstrumentDataRule.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.lastUpdated, "lastUpdated");
    this.lastUpdated.EnterMoveNextControl = true;
    this.lastUpdated.FormatString = (string) null;
    this.lastUpdated.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.lastUpdated.IsReadOnly = false;
    this.lastUpdated.IsUndoing = false;
    this.lastUpdated.Name = "lastUpdated";
    this.lastUpdated.Properties.Appearance.BorderColor = Color.Yellow;
    this.lastUpdated.Properties.Appearance.Options.UseBorderColor = true;
    this.lastUpdated.Properties.BorderStyle = BorderStyles.Simple;
    this.lastUpdated.Properties.Mask.EditMask = componentResourceManager.GetString("lastUpdated.Properties.Mask.EditMask");
    this.lastUpdated.StyleController = (IStyleController) this.layoutControl1;
    this.lastUpdated.Validator = (ICustomValidator) null;
    this.lastUpdated.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.lastConnected, "lastConnected");
    this.lastConnected.EnterMoveNextControl = true;
    this.lastConnected.FormatString = (string) null;
    this.lastConnected.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.lastConnected.IsReadOnly = false;
    this.lastConnected.IsUndoing = false;
    this.lastConnected.Name = "lastConnected";
    this.lastConnected.Properties.Appearance.BorderColor = Color.Yellow;
    this.lastConnected.Properties.Appearance.Options.UseBorderColor = true;
    this.lastConnected.Properties.BorderStyle = BorderStyles.Simple;
    this.lastConnected.Properties.Mask.EditMask = componentResourceManager.GetString("lastConnected.Properties.Mask.EditMask");
    this.lastConnected.StyleController = (IStyleController) this.layoutControl1;
    this.lastConnected.Validator = (ICustomValidator) null;
    this.lastConnected.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.displayTimeout, "displayTimeout");
    this.displayTimeout.EnterMoveNextControl = true;
    this.displayTimeout.FormatString = (string) null;
    this.displayTimeout.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.displayTimeout.IsReadOnly = false;
    this.displayTimeout.IsUndoing = false;
    this.displayTimeout.Name = "displayTimeout";
    this.displayTimeout.Properties.Appearance.BorderColor = Color.Yellow;
    this.displayTimeout.Properties.Appearance.Options.UseBorderColor = true;
    this.displayTimeout.Properties.BorderStyle = BorderStyles.Simple;
    this.displayTimeout.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.displayTimeout.Properties.MaxValue = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.displayTimeout.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.displayTimeout.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.displayTimeout.StyleController = (IStyleController) this.layoutControl1;
    this.displayTimeout.Validator = (ICustomValidator) null;
    this.displayTimeout.Value = (object) 1f;
    this.language.EnterMoveNextControl = true;
    this.language.FormatString = (string) null;
    this.language.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.language.IsReadOnly = false;
    this.language.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.language, "language");
    this.language.Name = "language";
    this.language.Properties.Appearance.BorderColor = Color.LightGray;
    this.language.Properties.Appearance.Options.UseBorderColor = true;
    this.language.Properties.BorderStyle = BorderStyles.Simple;
    this.language.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("language.Properties.Buttons"))
    });
    this.language.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.language.ShowEmptyElement = true;
    this.language.StyleController = (IStyleController) this.layoutControl1;
    this.language.Validator = (ICustomValidator) null;
    this.language.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.powerTimeout, "powerTimeout");
    this.powerTimeout.EnterMoveNextControl = true;
    this.powerTimeout.FormatString = (string) null;
    this.powerTimeout.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.powerTimeout.IsReadOnly = false;
    this.powerTimeout.IsUndoing = false;
    this.powerTimeout.Name = "powerTimeout";
    this.powerTimeout.Properties.Appearance.BorderColor = Color.Yellow;
    this.powerTimeout.Properties.Appearance.Options.UseBorderColor = true;
    this.powerTimeout.Properties.BorderStyle = BorderStyles.Simple;
    this.powerTimeout.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.powerTimeout.Properties.MaxValue = new Decimal(new int[4]
    {
      30,
      0,
      0,
      0
    });
    this.powerTimeout.Properties.MinValue = new Decimal(new int[4]
    {
      2,
      0,
      0,
      0
    });
    this.powerTimeout.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.powerTimeout.StyleController = (IStyleController) this.layoutControl1;
    this.powerTimeout.Validator = (ICustomValidator) null;
    this.powerTimeout.Value = (object) 20f;
    this.site.EnterMoveNextControl = true;
    this.site.FormatString = (string) null;
    this.site.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.site.IsReadOnly = false;
    this.site.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.site, "site");
    this.site.Name = "site";
    this.site.Properties.Appearance.BorderColor = Color.LightGray;
    this.site.Properties.Appearance.Options.UseBorderColor = true;
    this.site.Properties.BorderStyle = BorderStyles.Simple;
    this.site.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("site.Properties.Buttons"))
    });
    this.site.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.site.ShowEmptyElement = true;
    this.site.StyleController = (IStyleController) this.layoutControl1;
    this.site.Validator = (ICustomValidator) null;
    this.site.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.code, "code");
    this.code.EnterMoveNextControl = true;
    this.code.FormatString = (string) null;
    this.code.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.code.IsReadOnly = false;
    this.code.IsUndoing = false;
    this.code.Name = "code";
    this.code.Properties.Appearance.BorderColor = Color.Yellow;
    this.code.Properties.Appearance.Options.UseBorderColor = true;
    this.code.Properties.BorderStyle = BorderStyles.Simple;
    this.code.Properties.Mask.EditMask = componentResourceManager.GetString("code.Properties.Mask.EditMask");
    this.code.StyleController = (IStyleController) this.layoutControl1;
    this.code.Validator = (ICustomValidator) null;
    this.code.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.serial, "serial");
    this.serial.EnterMoveNextControl = true;
    this.serial.FormatString = (string) null;
    this.serial.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.serial.IsMandatory = true;
    this.serial.IsReadOnly = false;
    this.serial.IsUndoing = false;
    this.serial.Name = "serial";
    this.serial.Properties.Appearance.BackColor = Color.LightYellow;
    this.serial.Properties.Appearance.BorderColor = Color.Yellow;
    this.serial.Properties.Appearance.Options.UseBackColor = true;
    this.serial.Properties.Appearance.Options.UseBorderColor = true;
    this.serial.Properties.BorderStyle = BorderStyles.Simple;
    this.serial.Properties.Mask.EditMask = componentResourceManager.GetString("serial.Properties.Mask.EditMask");
    this.serial.StyleController = (IStyleController) this.layoutControl1;
    this.serial.Validator = (ICustomValidator) null;
    this.serial.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.name, "name");
    this.name.EnterMoveNextControl = true;
    this.name.FormatString = (string) null;
    this.name.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.name.IsMandatory = true;
    this.name.IsReadOnly = false;
    this.name.IsUndoing = false;
    this.name.Name = "name";
    this.name.Properties.Appearance.BackColor = Color.LightYellow;
    this.name.Properties.Appearance.BorderColor = Color.Yellow;
    this.name.Properties.Appearance.Options.UseBackColor = true;
    this.name.Properties.Appearance.Options.UseBorderColor = true;
    this.name.Properties.BorderStyle = BorderStyles.Simple;
    this.name.Properties.Mask.EditMask = componentResourceManager.GetString("name.Properties.Mask.EditMask");
    this.name.StyleController = (IStyleController) this.layoutControl1;
    this.name.Validator = (ICustomValidator) null;
    this.name.Value = (object) "";
    this.layoutInstrumentConfiguration.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutInstrumentConfiguration.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutInstrumentConfiguration, "layoutInstrumentConfiguration");
    this.layoutInstrumentConfiguration.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutInstrumentConfiguration.GroupBordersVisible = false;
    this.layoutInstrumentConfiguration.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutIndividualSettings,
      (BaseLayoutItem) this.layoutCommonConfiguration,
      (BaseLayoutItem) this.layoutSystemInformation,
      (BaseLayoutItem) this.emptySpaceItem1
    });
    this.layoutInstrumentConfiguration.Location = new Point(0, 0);
    this.layoutInstrumentConfiguration.Name = "layoutInstrumentConfiguration";
    this.layoutInstrumentConfiguration.Size = new Size(452, 657);
    this.layoutInstrumentConfiguration.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutInstrumentConfiguration.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutIndividualSettings, "layoutIndividualSettings");
    this.layoutIndividualSettings.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutLanguage,
      (BaseLayoutItem) this.layoutSite,
      (BaseLayoutItem) this.layoutCode,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.layoutName
    });
    this.layoutIndividualSettings.Location = new Point(0, 0);
    this.layoutIndividualSettings.Name = "layoutIndividualSettings";
    this.layoutIndividualSettings.Size = new Size(432, 164);
    this.layoutLanguage.Control = (Control) this.language;
    componentResourceManager.ApplyResources((object) this.layoutLanguage, "layoutLanguage");
    this.layoutLanguage.Location = new Point(0, 96 /*0x60*/);
    this.layoutLanguage.Name = "layoutLanguage";
    this.layoutLanguage.Size = new Size(408, 24);
    this.layoutLanguage.TextSize = new Size(85, 13);
    this.layoutSite.Control = (Control) this.site;
    componentResourceManager.ApplyResources((object) this.layoutSite, "layoutSite");
    this.layoutSite.Location = new Point(0, 72);
    this.layoutSite.Name = "layoutSite";
    this.layoutSite.Size = new Size(408, 24);
    this.layoutSite.TextSize = new Size(85, 13);
    this.layoutCode.Control = (Control) this.code;
    componentResourceManager.ApplyResources((object) this.layoutCode, "layoutCode");
    this.layoutCode.Location = new Point(0, 48 /*0x30*/);
    this.layoutCode.Name = "layoutCode";
    this.layoutCode.Size = new Size(408, 24);
    this.layoutCode.TextSize = new Size(85, 13);
    this.layoutControlItem5.Control = (Control) this.serial;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(0, 24);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(408, 24);
    this.layoutControlItem5.TextSize = new Size(85, 13);
    this.layoutName.Control = (Control) this.name;
    componentResourceManager.ApplyResources((object) this.layoutName, "layoutName");
    this.layoutName.Location = new Point(0, 0);
    this.layoutName.Name = "layoutName";
    this.layoutName.Size = new Size(408, 24);
    this.layoutName.TextSize = new Size(85, 13);
    componentResourceManager.ApplyResources((object) this.layoutCommonConfiguration, "layoutCommonConfiguration");
    this.layoutCommonConfiguration.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutDisplayTimeout,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem12,
      (BaseLayoutItem) this.emptySpaceItem4,
      (BaseLayoutItem) this.layoutDataDeleteInstrumentDataRule
    });
    this.layoutCommonConfiguration.Location = new Point(0, 164);
    this.layoutCommonConfiguration.Name = "layoutCommonConfiguration";
    this.layoutCommonConfiguration.Size = new Size(432, 116);
    this.layoutDisplayTimeout.Control = (Control) this.displayTimeout;
    componentResourceManager.ApplyResources((object) this.layoutDisplayTimeout, "layoutDisplayTimeout");
    this.layoutDisplayTimeout.Location = new Point(0, 0);
    this.layoutDisplayTimeout.Name = "layoutDisplayTimeout";
    this.layoutDisplayTimeout.Size = new Size(217, 24);
    this.layoutDisplayTimeout.TextSize = new Size(85, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(217, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(191, 24);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem12.Control = (Control) this.powerTimeout;
    componentResourceManager.ApplyResources((object) this.layoutControlItem12, "layoutControlItem12");
    this.layoutControlItem12.Location = new Point(0, 24);
    this.layoutControlItem12.Name = "layoutControlItem12";
    this.layoutControlItem12.Size = new Size(217, 24);
    this.layoutControlItem12.TextSize = new Size(85, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(217, 24);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(191, 24);
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    this.layoutDataDeleteInstrumentDataRule.Control = (Control) this.deleteInstrumentDataRule;
    componentResourceManager.ApplyResources((object) this.layoutDataDeleteInstrumentDataRule, "layoutDataDeleteInstrumentDataRule");
    this.layoutDataDeleteInstrumentDataRule.Location = new Point(0, 48 /*0x30*/);
    this.layoutDataDeleteInstrumentDataRule.Name = "layoutDataDeleteInstrumentDataRule";
    this.layoutDataDeleteInstrumentDataRule.Size = new Size(408, 24);
    this.layoutDataDeleteInstrumentDataRule.TextSize = new Size(85, 13);
    componentResourceManager.ApplyResources((object) this.layoutSystemInformation, "layoutSystemInformation");
    this.layoutSystemInformation.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutLastSeen,
      (BaseLayoutItem) this.layoutLastUpdated,
      (BaseLayoutItem) this.layoutHardwareVersion,
      (BaseLayoutItem) this.layoutFirmwareVersion
    });
    this.layoutSystemInformation.Location = new Point(0, 280);
    this.layoutSystemInformation.Name = "layoutSystemInformation";
    this.layoutSystemInformation.Size = new Size(432, 140);
    this.layoutLastSeen.Control = (Control) this.lastConnected;
    componentResourceManager.ApplyResources((object) this.layoutLastSeen, "layoutLastSeen");
    this.layoutLastSeen.Location = new Point(0, 0);
    this.layoutLastSeen.Name = "layoutLastSeen";
    this.layoutLastSeen.Size = new Size(408, 24);
    this.layoutLastSeen.TextSize = new Size(85, 13);
    this.layoutLastUpdated.Control = (Control) this.lastUpdated;
    componentResourceManager.ApplyResources((object) this.layoutLastUpdated, "layoutLastUpdated");
    this.layoutLastUpdated.Location = new Point(0, 24);
    this.layoutLastUpdated.Name = "layoutLastUpdated";
    this.layoutLastUpdated.Size = new Size(408, 24);
    this.layoutLastUpdated.TextSize = new Size(85, 13);
    this.layoutHardwareVersion.Control = (Control) this.hardwareVersion;
    componentResourceManager.ApplyResources((object) this.layoutHardwareVersion, "layoutHardwareVersion");
    this.layoutHardwareVersion.Location = new Point(0, 48 /*0x30*/);
    this.layoutHardwareVersion.Name = "layoutHardwareVersion";
    this.layoutHardwareVersion.Size = new Size(408, 24);
    this.layoutHardwareVersion.TextSize = new Size(85, 13);
    this.layoutFirmwareVersion.Control = (Control) this.firmwareVersion;
    componentResourceManager.ApplyResources((object) this.layoutFirmwareVersion, "layoutFirmwareVersion");
    this.layoutFirmwareVersion.Location = new Point(0, 72);
    this.layoutFirmwareVersion.Name = "layoutFirmwareVersion";
    this.layoutFirmwareVersion.Size = new Size(408, 24);
    this.layoutFirmwareVersion.TextSize = new Size(85, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 420);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(432, 217);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.userAssignmentTab.Appearance.HeaderActive.Options.UseTextOptions = true;
    this.userAssignmentTab.Appearance.HeaderActive.TextOptions.WordWrap = WordWrap.Wrap;
    this.userAssignmentTab.Controls.Add((Control) this.userGrid);
    this.userAssignmentTab.Name = "userAssignmentTab";
    componentResourceManager.ApplyResources((object) this.userAssignmentTab, "userAssignmentTab");
    componentResourceManager.ApplyResources((object) this.userGrid, "userGrid");
    this.userGrid.MainView = (BaseView) this.userAssignmentView;
    this.userGrid.Name = "userGrid";
    this.userGrid.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.flagCheckEdit
    });
    this.userGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.userAssignmentView
    });
    this.userAssignmentView.Columns.AddRange(new GridColumn[2]
    {
      this.columnLoginName,
      this.columnIsActive
    });
    this.userAssignmentView.GridControl = this.userGrid;
    this.userAssignmentView.Name = "userAssignmentView";
    this.userAssignmentView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.userAssignmentView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.userAssignmentView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.userAssignmentView.OptionsView.EnableAppearanceEvenRow = true;
    this.userAssignmentView.OptionsView.EnableAppearanceOddRow = true;
    this.userAssignmentView.OptionsView.ShowGroupPanel = false;
    this.userAssignmentView.SortInfo.AddRange(new GridColumnSortInfo[1]
    {
      new GridColumnSortInfo(this.columnLoginName, ColumnSortOrder.Ascending)
    });
    componentResourceManager.ApplyResources((object) this.columnLoginName, "columnLoginName");
    this.columnLoginName.FieldName = "LoginName";
    this.columnLoginName.Name = "columnLoginName";
    this.columnLoginName.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.columnIsActive, "columnIsActive");
    this.columnIsActive.ColumnEdit = (RepositoryItem) this.flagCheckEdit;
    this.columnIsActive.FieldName = "UserOnInstrumentValue";
    this.columnIsActive.Name = "columnIsActive";
    componentResourceManager.ApplyResources((object) this.flagCheckEdit, "flagCheckEdit");
    this.flagCheckEdit.Name = "flagCheckEdit";
    this.flagCheckEdit.CheckedChanged += new EventHandler(this.OnInstrumentUserAssignmentChanged);
    componentResourceManager.ApplyResources((object) this.instrumentGrid, "instrumentGrid");
    this.instrumentGrid.MainView = (BaseView) this.instrumentGridView;
    this.instrumentGrid.Name = "instrumentGrid";
    this.instrumentGrid.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.instrumentGridView
    });
    this.instrumentGridView.Columns.AddRange(new GridColumn[3]
    {
      this.columnName,
      this.columnSerial,
      this.columnLastConnected
    });
    this.instrumentGridView.GridControl = this.instrumentGrid;
    this.instrumentGridView.Name = "instrumentGridView";
    this.instrumentGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.instrumentGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.instrumentGridView.OptionsBehavior.Editable = false;
    this.instrumentGridView.OptionsCustomization.AllowFilter = false;
    this.instrumentGridView.OptionsFilter.AllowFilterEditor = false;
    this.instrumentGridView.OptionsMenu.EnableColumnMenu = false;
    this.instrumentGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.instrumentGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.instrumentGridView.OptionsView.EnableAppearanceOddRow = true;
    this.instrumentGridView.OptionsView.ShowDetailButtons = false;
    this.instrumentGridView.OptionsView.ShowGroupPanel = false;
    this.instrumentGridView.SortInfo.AddRange(new GridColumnSortInfo[1]
    {
      new GridColumnSortInfo(this.columnName, ColumnSortOrder.Ascending)
    });
    componentResourceManager.ApplyResources((object) this.columnName, "columnName");
    this.columnName.FieldName = "Name";
    this.columnName.Name = "columnName";
    componentResourceManager.ApplyResources((object) this.columnSerial, "columnSerial");
    this.columnSerial.FieldName = "SerialNumber";
    this.columnSerial.Name = "columnSerial";
    componentResourceManager.ApplyResources((object) this.columnLastConnected, "columnLastConnected");
    this.columnLastConnected.FieldName = "LastConnected";
    this.columnLastConnected.Name = "columnLastConnected";
    this.layoutInstrumentManagement.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutInstrumentManagement.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutInstrumentManagement, "layoutInstrumentManagement");
    this.layoutInstrumentManagement.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutInstrumentManagement.GroupBordersVisible = false;
    this.layoutInstrumentManagement.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutGroupInstruments
    });
    this.layoutInstrumentManagement.Location = new Point(0, 0);
    this.layoutInstrumentManagement.Name = "layoutInstrumentManagement";
    this.layoutInstrumentManagement.Size = new Size(940, 710);
    this.layoutInstrumentManagement.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutInstrumentManagement.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.xtraTabControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(457, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(463, 690);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutGroupInstruments, "layoutGroupInstruments");
    this.layoutGroupInstruments.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutInstrumentGrid
    });
    this.layoutGroupInstruments.Location = new Point(0, 0);
    this.layoutGroupInstruments.Name = "layoutGroupInstruments";
    this.layoutGroupInstruments.Size = new Size(457, 690);
    this.layoutInstrumentGrid.Control = (Control) this.instrumentGrid;
    componentResourceManager.ApplyResources((object) this.layoutInstrumentGrid, "layoutInstrumentGrid");
    this.layoutInstrumentGrid.Location = new Point(0, 0);
    this.layoutInstrumentGrid.Name = "layoutInstrumentGrid";
    this.layoutInstrumentGrid.ShowInCustomizationForm = false;
    this.layoutInstrumentGrid.Size = new Size(433, 646);
    this.layoutInstrumentGrid.TextSize = new Size(0, 0);
    this.layoutInstrumentGrid.TextToControlDistance = 0;
    this.layoutInstrumentGrid.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutInstrumentDetail);
    this.Name = nameof (InstrumentMasterDetailBrowser);
    this.layoutInstrumentDetail.EndInit();
    this.layoutInstrumentDetail.ResumeLayout(false);
    this.xtraTabControl1.EndInit();
    this.xtraTabControl1.ResumeLayout(false);
    this.configurationTab.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.firmwareVersion.Properties.EndInit();
    this.hardwareVersion.Properties.EndInit();
    this.deleteInstrumentDataRule.Properties.EndInit();
    this.lastUpdated.Properties.EndInit();
    this.lastConnected.Properties.EndInit();
    this.displayTimeout.Properties.EndInit();
    this.language.Properties.EndInit();
    this.powerTimeout.Properties.EndInit();
    this.site.Properties.EndInit();
    this.code.Properties.EndInit();
    this.serial.Properties.EndInit();
    this.name.Properties.EndInit();
    this.layoutInstrumentConfiguration.EndInit();
    this.layoutIndividualSettings.EndInit();
    this.layoutLanguage.EndInit();
    this.layoutSite.EndInit();
    this.layoutCode.EndInit();
    this.layoutControlItem5.EndInit();
    this.layoutName.EndInit();
    this.layoutCommonConfiguration.EndInit();
    this.layoutDisplayTimeout.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem12.EndInit();
    this.emptySpaceItem4.EndInit();
    this.layoutDataDeleteInstrumentDataRule.EndInit();
    this.layoutSystemInformation.EndInit();
    this.layoutLastSeen.EndInit();
    this.layoutLastUpdated.EndInit();
    this.layoutHardwareVersion.EndInit();
    this.layoutFirmwareVersion.EndInit();
    this.emptySpaceItem1.EndInit();
    this.userAssignmentTab.ResumeLayout(false);
    this.userGrid.EndInit();
    this.userAssignmentView.EndInit();
    this.flagCheckEdit.EndInit();
    this.instrumentGrid.EndInit();
    this.instrumentGridView.EndInit();
    this.layoutInstrumentManagement.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutGroupInstruments.EndInit();
    this.layoutInstrumentGrid.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateViewCallBack(ChangeType changeType, Type type, object item);
}
