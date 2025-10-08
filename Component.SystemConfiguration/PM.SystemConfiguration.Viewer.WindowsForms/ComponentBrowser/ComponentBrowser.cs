// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser
// Assembly: PM.SystemConfiguration.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D6114BC-3807-4057-97EB-FB0AA393F8AD
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.DataExchange;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration.Core;
using PathMedical.SystemConfiguration.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser;

[ToolboxItem(false)]
public sealed class ComponentBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<GlobalSystemConfiguration> modelMapper;
  private IContainer components;
  private LayoutControl layoutControl;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlGroup layoutControlGroup3;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressComboBoxEdit trackingSystemConfiguration;
  private LayoutControlGroup layoutControlGroup2;
  private DevExpressComboBoxEdit systemLanguage;
  private LayoutControlGroup layoutLanguageConfiguration;
  private LayoutControlItem layoutLanguageSelection;
  private EmptySpaceItem emptySpaceItem2;
  private DevExpressComboBoxEdit deletionConfirmation;
  private DevExpressComboBoxEdit storeConfirmation;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem2;
  private EmptySpaceItem emptySpaceItem3;
  private LayoutControlItem layoutControlItem4;
  private EmptySpaceItem emptySpaceItem4;
  private EmptySpaceItem emptySpaceItem5;
  private EmptySpaceItem emptySpaceItem7;
  private DevExpressButton loadPictureButton;
  private LayoutControlItem layoutControlItem5;
  private DevExpressPictureBox reportPictureImage;
  private LayoutControlGroup layoutControlGroup4;
  private LayoutControlItem layoutControlItem1;
  private DevExpressButton restoreDefaultPicture;
  private LayoutControlItem layoutControlItem6;
  private DevExpressComboBoxEdit dataModificationWarning;
  private LayoutControlItem layoutDataModificationWarning;

  public ComponentBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
    this.loadPictureButton.Click += new EventHandler(this.LoadPicture);
    this.restoreDefaultPicture.Click += new EventHandler(this.RestoreDefaultLogo);
  }

  public ComponentBrowser(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ComponentBrowser_RibbonModificationGroup));
    foreach (IApplicationComponent applicationComponent in SystemConfigurationManager.Instance.ApplicationComponents.Where<IApplicationComponent>((Func<IApplicationComponent, bool>) (ac => ac.ConfigurationModuleTypes != null && ac.ConfigurationModuleTypes.Length != 0)))
    {
      string caption = GlobalResourceEnquirer.Instance.GetResourceByName($"{applicationComponent.Fingerprint}.Name") as string;
      if (string.IsNullOrEmpty(caption))
        caption = applicationComponent.Name;
      RibbonPageGroup ribbonPageGroup = ribbonHelper.CreateRibbonPageGroup(caption);
      foreach (Type configurationModuleType in applicationComponent.ConfigurationModuleTypes)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(configurationModuleType);
        BarButtonItem largeImageButton = ribbonHelper.CreateLargeImageButton(applicationComponentModule);
        ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton);
      }
      this.ToolbarElements.Add((object) ribbonPageGroup);
    }
    RibbonPageGroup ribbonPageGroup1 = ribbonHelper.CreateRibbonPageGroup(Resources.ComponentBrowser_RibbonPageGroup);
    foreach (ISupportDataExchangeModules dataExchangeModules in SystemConfigurationManager.Instance.Plugins.OfType<ISupportDataExchangeModules>().Where<ISupportDataExchangeModules>((Func<ISupportDataExchangeModules, bool>) (p => p.ConfigurationModule != null)).OrderBy<ISupportDataExchangeModules, string>((Func<ISupportDataExchangeModules, string>) (p => p.Name)).ToArray<ISupportDataExchangeModules>())
    {
      IApplicationComponentModule configurationModule = dataExchangeModules.ConfigurationModule;
      BarButtonItem largeImageButton = ribbonHelper.CreateLargeImageButton(configurationModule.ShortcutName ?? configurationModule.Name, configurationModule.Description, string.Empty, configurationModule.Image as Bitmap, new Trigger(configurationModule.Id.ToString()));
      ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton);
    }
    if (ribbonPageGroup1.ItemLinks.Count > 0)
      this.ToolbarElements.Add((object) ribbonPageGroup1);
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ComponentBrowser_RibbonHelp, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeSelectionValues()
  {
    this.trackingSystemConfiguration.DataSource = (object) new TrackingSytem[2]
    {
      TrackingSytem.None,
      TrackingSytem.Generic
    };
    this.storeConfirmation.DataSource = (object) new StorageConfirmation[2]
    {
      StorageConfirmation.Yes,
      StorageConfirmation.No
    };
    this.deletionConfirmation.DataSource = (object) new DeletionConfirmation[2]
    {
      DeletionConfirmation.Yes,
      DeletionConfirmation.No
    };
    this.dataModificationWarning.DataSource = (object) new DataModificationWarning[2]
    {
      DataModificationWarning.Yes,
      DataModificationWarning.No
    };
    this.systemLanguage.DataSource = (object) SystemConfigurationManager.Instance.SupportedLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl.Name))).Distinct<ComboBoxEditItemWrapper>().OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  private void InitializeModelMapper()
  {
    ModelMapper<GlobalSystemConfiguration> modelMapper = new ModelMapper<GlobalSystemConfiguration>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => (object) s.DisplayMessageAfterSuccessfullyDeletion), (object) this.deletionConfirmation);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => (object) s.DisplayMessageAfterSuccessfullyStorage), (object) this.storeConfirmation);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => (object) s.TrackingSystem), (object) this.trackingSystemConfiguration);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => s.DefaultSystemLanguage), (object) this.systemLanguage);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => s.ReportPicture), (object) this.reportPictureImage);
    modelMapper.Add((Expression<Func<GlobalSystemConfiguration, object>>) (s => (object) s.DisplayDataModificationWarning), (object) this.dataModificationWarning);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.ItemEdited || !(e.Type == typeof (GlobalSystemConfiguration)))
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser.UpdateConfigurationCallBack(this.FillFields), (object) (e.ChangedObject as GlobalSystemConfiguration));
    else
      this.FillFields(e.ChangedObject as GlobalSystemConfiguration);
  }

  private void FillFields(GlobalSystemConfiguration configuration)
  {
    this.restoreDefaultPicture.Enabled = !configuration.defaultPicture;
    this.modelMapper.CopyModelToUI(configuration);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool isUIEnabled = this.ViewMode != 0;
    this.modelMapper.SetUIEnabled(isUIEnabled);
    this.loadPictureButton.Enabled = isUIEnabled;
  }

  private void LoadPicture(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Filter = Resources.ComponentBrowser_FileFilter;
    OpenFileDialog openFileDialog2 = openFileDialog1;
    if (openFileDialog2.ShowDialog() != DialogResult.OK)
      return;
    if (this.reportPictureImage.Image != null)
      this.reportPictureImage.BackgroundImage = this.reportPictureImage.Image;
    this.reportPictureImage.Image = (Image) new Bitmap(openFileDialog2.FileName);
  }

  public string CreateBase64EncodedImage(string file)
  {
    string base64EncodedImage = string.Empty;
    byte[] numArray1 = new byte[0];
    if (!string.IsNullOrEmpty(file))
    {
      byte[] numArray2 = File.ReadAllBytes(file);
      if (((IEnumerable<byte>) numArray2).Count<byte>() > 0)
        base64EncodedImage = Convert.ToBase64String(numArray2);
    }
    return base64EncodedImage;
  }

  public byte[] CreateBinaryImage(string base64EncodedImage)
  {
    return !string.IsNullOrEmpty(base64EncodedImage) ? Convert.FromBase64String(base64EncodedImage) : (byte[]) null;
  }

  private void RestoreDefaultLogo(object sender, EventArgs e)
  {
    using (MemoryStream memoryStream = new MemoryStream(this.CreateBinaryImage(SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("8B5B064B-EC5F-4524-96DD-0E88BD3FA952"), "DefaultReportPicture"))))
    {
      if (!(Image.FromStream((Stream) memoryStream) is Bitmap bitmap))
        return;
      this.reportPictureImage.Image = (Image) bitmap;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser));
    this.layoutControl = new LayoutControl();
    this.restoreDefaultPicture = new DevExpressButton();
    this.reportPictureImage = new DevExpressPictureBox();
    this.deletionConfirmation = new DevExpressComboBoxEdit();
    this.storeConfirmation = new DevExpressComboBoxEdit();
    this.systemLanguage = new DevExpressComboBoxEdit();
    this.trackingSystemConfiguration = new DevExpressComboBoxEdit();
    this.loadPictureButton = new DevExpressButton();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem4 = new LayoutControlItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutLanguageConfiguration = new LayoutControlGroup();
    this.layoutLanguageSelection = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlGroup4 = new LayoutControlGroup();
    this.layoutControlItem5 = new LayoutControlItem();
    this.emptySpaceItem7 = new EmptySpaceItem();
    this.emptySpaceItem5 = new EmptySpaceItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.dataModificationWarning = new DevExpressComboBoxEdit();
    this.layoutDataModificationWarning = new LayoutControlItem();
    this.layoutControl.BeginInit();
    this.layoutControl.SuspendLayout();
    this.reportPictureImage.Properties.BeginInit();
    this.deletionConfirmation.Properties.BeginInit();
    this.storeConfirmation.Properties.BeginInit();
    this.systemLanguage.Properties.BeginInit();
    this.trackingSystemConfiguration.Properties.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutLanguageConfiguration.BeginInit();
    this.layoutLanguageSelection.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlGroup4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.emptySpaceItem7.BeginInit();
    this.emptySpaceItem5.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.dataModificationWarning.Properties.BeginInit();
    this.layoutDataModificationWarning.BeginInit();
    this.SuspendLayout();
    this.layoutControl.Controls.Add((Control) this.dataModificationWarning);
    this.layoutControl.Controls.Add((Control) this.restoreDefaultPicture);
    this.layoutControl.Controls.Add((Control) this.reportPictureImage);
    this.layoutControl.Controls.Add((Control) this.deletionConfirmation);
    this.layoutControl.Controls.Add((Control) this.storeConfirmation);
    this.layoutControl.Controls.Add((Control) this.systemLanguage);
    this.layoutControl.Controls.Add((Control) this.trackingSystemConfiguration);
    this.layoutControl.Controls.Add((Control) this.loadPictureButton);
    componentResourceManager.ApplyResources((object) this.layoutControl, "layoutControl");
    this.layoutControl.HiddenItems.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlGroup2
    });
    this.layoutControl.Name = "layoutControl";
    this.layoutControl.Root = this.layoutControlGroup1;
    this.restoreDefaultPicture.FormatString = (string) null;
    this.restoreDefaultPicture.IsActive = true;
    this.restoreDefaultPicture.IsMandatory = false;
    this.restoreDefaultPicture.IsModified = false;
    this.restoreDefaultPicture.IsNavigationOnly = false;
    this.restoreDefaultPicture.IsReadOnly = false;
    this.restoreDefaultPicture.IsUndoDisabled = false;
    this.restoreDefaultPicture.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.restoreDefaultPicture, "restoreDefaultPicture");
    this.restoreDefaultPicture.Name = "restoreDefaultPicture";
    this.restoreDefaultPicture.ShowModified = false;
    this.restoreDefaultPicture.StyleController = (IStyleController) this.layoutControl;
    this.restoreDefaultPicture.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.restoreDefaultPicture.Validator = (ICustomValidator) null;
    this.restoreDefaultPicture.Value = (object) null;
    this.reportPictureImage.FormatString = (string) null;
    this.reportPictureImage.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.reportPictureImage.IsMandatory = false;
    this.reportPictureImage.IsReadOnly = false;
    this.reportPictureImage.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.reportPictureImage, "reportPictureImage");
    this.reportPictureImage.Name = "reportPictureImage";
    this.reportPictureImage.Properties.Appearance.BorderColor = Color.LightGray;
    this.reportPictureImage.Properties.Appearance.Options.UseBorderColor = true;
    this.reportPictureImage.Properties.NullText = componentResourceManager.GetString("reportPictureImage.Properties.NullText");
    this.reportPictureImage.Properties.ShowMenu = false;
    this.reportPictureImage.Properties.SizeMode = PictureSizeMode.Squeeze;
    this.reportPictureImage.StyleController = (IStyleController) this.layoutControl;
    this.reportPictureImage.Validator = (ICustomValidator) null;
    this.reportPictureImage.Value = (object) null;
    this.deletionConfirmation.EnterMoveNextControl = true;
    this.deletionConfirmation.FormatString = (string) null;
    this.deletionConfirmation.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.deletionConfirmation.IsReadOnly = false;
    this.deletionConfirmation.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.deletionConfirmation, "deletionConfirmation");
    this.deletionConfirmation.Name = "deletionConfirmation";
    this.deletionConfirmation.Properties.Appearance.BorderColor = Color.LightGray;
    this.deletionConfirmation.Properties.Appearance.Options.UseBorderColor = true;
    this.deletionConfirmation.Properties.BorderStyle = BorderStyles.Simple;
    this.deletionConfirmation.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("deletionConfirmation.Properties.Buttons"))
    });
    this.deletionConfirmation.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.deletionConfirmation.ShowEmptyElement = false;
    this.deletionConfirmation.StyleController = (IStyleController) this.layoutControl;
    this.deletionConfirmation.Validator = (ICustomValidator) null;
    this.deletionConfirmation.Value = (object) null;
    this.storeConfirmation.EnterMoveNextControl = true;
    this.storeConfirmation.FormatString = (string) null;
    this.storeConfirmation.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.storeConfirmation.IsReadOnly = false;
    this.storeConfirmation.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.storeConfirmation, "storeConfirmation");
    this.storeConfirmation.Name = "storeConfirmation";
    this.storeConfirmation.Properties.Appearance.BorderColor = Color.LightGray;
    this.storeConfirmation.Properties.Appearance.Options.UseBorderColor = true;
    this.storeConfirmation.Properties.BorderStyle = BorderStyles.Simple;
    this.storeConfirmation.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("storeConfirmation.Properties.Buttons"))
    });
    this.storeConfirmation.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.storeConfirmation.ShowEmptyElement = false;
    this.storeConfirmation.StyleController = (IStyleController) this.layoutControl;
    this.storeConfirmation.Validator = (ICustomValidator) null;
    this.storeConfirmation.Value = (object) null;
    this.systemLanguage.EnterMoveNextControl = true;
    this.systemLanguage.FormatString = (string) null;
    this.systemLanguage.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.systemLanguage.IsReadOnly = false;
    this.systemLanguage.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.systemLanguage, "systemLanguage");
    this.systemLanguage.Name = "systemLanguage";
    this.systemLanguage.Properties.Appearance.BorderColor = Color.LightGray;
    this.systemLanguage.Properties.Appearance.Options.UseBorderColor = true;
    this.systemLanguage.Properties.BorderStyle = BorderStyles.Simple;
    this.systemLanguage.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("systemLanguage.Properties.Buttons"))
    });
    this.systemLanguage.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.systemLanguage.ShowEmptyElement = false;
    this.systemLanguage.StyleController = (IStyleController) this.layoutControl;
    this.systemLanguage.Validator = (ICustomValidator) null;
    this.systemLanguage.Value = (object) null;
    this.trackingSystemConfiguration.EnterMoveNextControl = true;
    this.trackingSystemConfiguration.FormatString = (string) null;
    this.trackingSystemConfiguration.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.trackingSystemConfiguration.IsActive = false;
    this.trackingSystemConfiguration.IsReadOnly = false;
    this.trackingSystemConfiguration.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.trackingSystemConfiguration, "trackingSystemConfiguration");
    this.trackingSystemConfiguration.Name = "trackingSystemConfiguration";
    this.trackingSystemConfiguration.Properties.Appearance.BorderColor = Color.LightGray;
    this.trackingSystemConfiguration.Properties.Appearance.Options.UseBorderColor = true;
    this.trackingSystemConfiguration.Properties.BorderStyle = BorderStyles.Simple;
    this.trackingSystemConfiguration.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("trackingSystemConfiguration.Properties.Buttons"))
    });
    this.trackingSystemConfiguration.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.trackingSystemConfiguration.ShowEmptyElement = false;
    this.trackingSystemConfiguration.StyleController = (IStyleController) this.layoutControl;
    this.trackingSystemConfiguration.Validator = (ICustomValidator) null;
    this.trackingSystemConfiguration.Value = (object) null;
    this.loadPictureButton.FormatString = (string) null;
    this.loadPictureButton.IsActive = true;
    this.loadPictureButton.IsMandatory = false;
    this.loadPictureButton.IsModified = false;
    this.loadPictureButton.IsNavigationOnly = false;
    this.loadPictureButton.IsReadOnly = false;
    this.loadPictureButton.IsUndoDisabled = false;
    this.loadPictureButton.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.loadPictureButton, "loadPictureButton");
    this.loadPictureButton.Name = "loadPictureButton";
    this.loadPictureButton.ShowModified = false;
    this.loadPictureButton.StyleController = (IStyleController) this.layoutControl;
    this.loadPictureButton.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.loadPictureButton.Validator = (ICustomValidator) null;
    this.loadPictureButton.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.emptySpaceItem4
    });
    this.layoutControlGroup2.Location = new Point(0, 160 /*0xA0*/);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(940, 68);
    this.layoutControlItem4.Control = (Control) this.trackingSystemConfiguration;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 0);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(362, 24);
    this.layoutControlItem4.TextSize = new Size(78, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(362, 0);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(554, 24);
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutControlGroup3,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutLanguageConfiguration,
      (BaseLayoutItem) this.layoutControlGroup4
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.layoutDataModificationWarning
    });
    this.layoutControlGroup3.Location = new Point(0, 68);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(940, 116);
    this.layoutControlItem3.Control = (Control) this.storeConfirmation;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(235, 24);
    this.layoutControlItem3.TextSize = new Size(126, 13);
    this.layoutControlItem2.Control = (Control) this.deletionConfirmation;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 24);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(235, 24);
    this.layoutControlItem2.TextSize = new Size(126, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(235, 0);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(681, 72);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 351);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(940, 379);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutLanguageConfiguration, "layoutLanguageConfiguration");
    this.layoutLanguageConfiguration.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutLanguageSelection,
      (BaseLayoutItem) this.emptySpaceItem2
    });
    this.layoutLanguageConfiguration.Location = new Point(0, 0);
    this.layoutLanguageConfiguration.Name = "layoutLanguageConfiguration";
    this.layoutLanguageConfiguration.Size = new Size(940, 68);
    this.layoutLanguageSelection.Control = (Control) this.systemLanguage;
    componentResourceManager.ApplyResources((object) this.layoutLanguageSelection, "layoutLanguageSelection");
    this.layoutLanguageSelection.Location = new Point(0, 0);
    this.layoutLanguageSelection.Name = "layoutLanguageSelection";
    this.layoutLanguageSelection.Size = new Size(359, 24);
    this.layoutLanguageSelection.TextSize = new Size(126, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(359, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(557, 24);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup4, "layoutControlGroup4");
    this.layoutControlGroup4.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.emptySpaceItem7,
      (BaseLayoutItem) this.emptySpaceItem5,
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem6
    });
    this.layoutControlGroup4.Location = new Point(0, 184);
    this.layoutControlGroup4.Name = "layoutControlGroup4";
    this.layoutControlGroup4.Size = new Size(940, 167);
    this.layoutControlItem5.Control = (Control) this.loadPictureButton;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(493, 0);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(116, 26);
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem7, "emptySpaceItem7");
    this.emptySpaceItem7.Location = new Point(493, 52);
    this.emptySpaceItem7.Name = "emptySpaceItem7";
    this.emptySpaceItem7.Size = new Size(116, 71);
    this.emptySpaceItem7.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem5, "emptySpaceItem5");
    this.emptySpaceItem5.Location = new Point(609, 0);
    this.emptySpaceItem5.MinSize = new Size(104, 24);
    this.emptySpaceItem5.Name = "emptySpaceItem5";
    this.emptySpaceItem5.Size = new Size(307, 123);
    this.emptySpaceItem5.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem5.TextSize = new Size(0, 0);
    this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlItem1.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutControlItem1.Control = (Control) this.reportPictureImage;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.MinSize = new Size(138, 24);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(493, 123);
    this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
    this.layoutControlItem1.TextSize = new Size(126, 13);
    this.layoutControlItem6.Control = (Control) this.restoreDefaultPicture;
    componentResourceManager.ApplyResources((object) this.layoutControlItem6, "layoutControlItem6");
    this.layoutControlItem6.Location = new Point(493, 26);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(116, 26);
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.dataModificationWarning.EnterMoveNextControl = true;
    this.dataModificationWarning.FormatString = (string) null;
    this.dataModificationWarning.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.dataModificationWarning.IsReadOnly = false;
    this.dataModificationWarning.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.dataModificationWarning, "dataModificationWarning");
    this.dataModificationWarning.Name = "dataModificationWarning";
    this.dataModificationWarning.Properties.Appearance.BorderColor = Color.LightGray;
    this.dataModificationWarning.Properties.Appearance.Options.UseBorderColor = true;
    this.dataModificationWarning.Properties.BorderStyle = BorderStyles.Simple;
    this.dataModificationWarning.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("devExpressComboBoxEdit1.Properties.Buttons"))
    });
    this.dataModificationWarning.ShowEmptyElement = true;
    this.dataModificationWarning.StyleController = (IStyleController) this.layoutControl;
    this.dataModificationWarning.Validator = (ICustomValidator) null;
    this.dataModificationWarning.Value = (object) null;
    this.layoutDataModificationWarning.Control = (Control) this.dataModificationWarning;
    componentResourceManager.ApplyResources((object) this.layoutDataModificationWarning, "layoutDataModificationWarning");
    this.layoutDataModificationWarning.Location = new Point(0, 48 /*0x30*/);
    this.layoutDataModificationWarning.Name = "layoutDataModificationWarning";
    this.layoutDataModificationWarning.Size = new Size(235, 24);
    this.layoutDataModificationWarning.TextSize = new Size(126, 13);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl);
    this.Name = nameof (ComponentBrowser);
    this.layoutControl.EndInit();
    this.layoutControl.ResumeLayout(false);
    this.reportPictureImage.Properties.EndInit();
    this.deletionConfirmation.Properties.EndInit();
    this.storeConfirmation.Properties.EndInit();
    this.systemLanguage.Properties.EndInit();
    this.trackingSystemConfiguration.Properties.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem4.EndInit();
    this.emptySpaceItem4.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlGroup3.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem2.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutLanguageConfiguration.EndInit();
    this.layoutLanguageSelection.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlGroup4.EndInit();
    this.layoutControlItem5.EndInit();
    this.emptySpaceItem7.EndInit();
    this.emptySpaceItem5.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem6.EndInit();
    this.dataModificationWarning.Properties.EndInit();
    this.layoutDataModificationWarning.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateConfigurationCallBack(GlobalSystemConfiguration configuration);
}
