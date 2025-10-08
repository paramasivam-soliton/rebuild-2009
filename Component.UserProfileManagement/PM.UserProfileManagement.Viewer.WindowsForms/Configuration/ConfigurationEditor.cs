// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration.ConfigurationEditor
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration;

[ToolboxItem(false)]
public sealed class ConfigurationEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<UserProfileManagementConfiguration> modelMapper;
  private IContainer components;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControl layoutControl1;
  private DevExpressComboBoxEdit profileCombobox;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlGroup layoutControlGroup3;
  private DevExpressPasswordEdit adminPasswordVerification;
  private DevExpressPasswordEdit adminPassword;
  private LayoutControlItem passwordTextbox;
  private LayoutControlItem passwordTextboxVerification;
  private DevExpressCheckedEdit activeCheckbox;
  private LayoutControlItem layoutControlItem3;
  private EmptySpaceItem emptySpaceItem2;
  private EmptySpaceItem emptySpaceItem3;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressSpinEdit userLockingTime;
  private LayoutControlGroup layoutUserManagementSettingGroup;
  private LayoutControlItem layoutUserLockingTime;
  private EmptySpaceItem emptySpaceItem4;

  public ConfigurationEditor()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.CreateRibbonBarCommands();
    this.InitializeModelMapper();
  }

  public ConfigurationEditor(IModel model)
    : this()
  {
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateRibbonBarCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ConfigurationEditor_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ConfigurationEditor_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ConfigurationEditor_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeModelMapper()
  {
    ModelMapper<UserProfileManagementConfiguration> modelMapper = new ModelMapper<UserProfileManagementConfiguration>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<UserProfileManagementConfiguration, object>>) (umpc => (object) umpc.IsUserManagementActive), (object) this.activeCheckbox);
    modelMapper.Add((Expression<Func<UserProfileManagementConfiguration, object>>) (upmc => upmc.DisabledUserManagementAdminPassword), (object) this.adminPassword);
    modelMapper.Add((Expression<Func<UserProfileManagementConfiguration, object>>) (umpc => umpc.DisabledUserManagementAdminPassword), (object) this.adminPasswordVerification);
    modelMapper.Add((Expression<Func<UserProfileManagementConfiguration, object>>) (upmc => upmc.DisabledUserManagementProfile), (object) this.profileCombobox);
    modelMapper.Add((Expression<Func<UserProfileManagementConfiguration, object>>) (upmc => (object) upmc.UserAccountLockingTime), (object) this.userLockingTime);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType == ChangeType.ItemEdited && e.Type == typeof (UserProfileManagementConfiguration))
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new ConfigurationEditor.UpdateConfigurationCallBack(this.FillFields), (object) (e.ChangedObject as UserProfileManagementConfiguration));
      else
        this.FillFields(e.ChangedObject as UserProfileManagementConfiguration);
    }
    else
    {
      if (e.ChangeType != ChangeType.ListLoaded || !(e.Type == typeof (UserProfile)))
        return;
      if (this.profileCombobox.InvokeRequired)
        this.profileCombobox.BeginInvoke((Delegate) new ConfigurationEditor.UpdateProfilesCallBack(this.FillProfile), (object) (e.ChangedObject as IEnumerable<UserProfile>));
      else
        this.FillProfile(e.ChangedObject as IEnumerable<UserProfile>);
    }
  }

  private void FillFields(UserProfileManagementConfiguration configuration)
  {
    this.profileCombobox.IsMandatory = configuration.IsUserManagementActive;
    this.adminPassword.IsMandatory = this.adminPasswordVerification.IsMandatory = configuration.IsUserManagementActive && string.IsNullOrEmpty(configuration.DisabledUserManagementAdminPassword);
    this.modelMapper.CopyModelToUI(configuration);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  private void FillProfile(IEnumerable<UserProfile> userProfiles)
  {
    this.profileCombobox.DataSource = (object) userProfiles.Select<UserProfile, ComboBoxEditItemWrapper>((Func<UserProfile, ComboBoxEditItemWrapper>) (up => new ComboBoxEditItemWrapper(up.Name, (object) up))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
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
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControl1 = new LayoutControl();
    this.userLockingTime = new DevExpressSpinEdit();
    this.activeCheckbox = new DevExpressCheckedEdit();
    this.adminPasswordVerification = new DevExpressPasswordEdit();
    this.adminPassword = new DevExpressPasswordEdit();
    this.profileCombobox = new DevExpressComboBoxEdit();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.passwordTextbox = new LayoutControlItem();
    this.passwordTextboxVerification = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutUserManagementSettingGroup = new LayoutControlGroup();
    this.layoutUserLockingTime = new LayoutControlItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutControlGroup1.BeginInit();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.userLockingTime.Properties.BeginInit();
    this.activeCheckbox.Properties.BeginInit();
    this.adminPasswordVerification.Properties.BeginInit();
    this.adminPassword.Properties.BeginInit();
    this.profileCombobox.Properties.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.passwordTextbox.BeginInit();
    this.passwordTextboxVerification.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutUserManagementSettingGroup.BeginInit();
    this.layoutUserLockingTime.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.SuspendLayout();
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Controls.Add((Control) this.userLockingTime);
    this.layoutControl1.Controls.Add((Control) this.activeCheckbox);
    this.layoutControl1.Controls.Add((Control) this.adminPasswordVerification);
    this.layoutControl1.Controls.Add((Control) this.adminPassword);
    this.layoutControl1.Controls.Add((Control) this.profileCombobox);
    this.layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlGroup3
    });
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.userLockingTime, "userLockingTime");
    this.userLockingTime.EnterMoveNextControl = true;
    this.userLockingTime.FormatString = (string) null;
    this.userLockingTime.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLockingTime.IsReadOnly = false;
    this.userLockingTime.IsUndoing = false;
    this.userLockingTime.Name = "userLockingTime";
    this.userLockingTime.Properties.AccessibleDescription = componentResourceManager.GetString("userLockingTime.Properties.AccessibleDescription");
    this.userLockingTime.Properties.AccessibleName = componentResourceManager.GetString("userLockingTime.Properties.AccessibleName");
    this.userLockingTime.Properties.Appearance.BorderColor = Color.Yellow;
    this.userLockingTime.Properties.Appearance.Options.UseBorderColor = true;
    this.userLockingTime.Properties.AutoHeight = (bool) componentResourceManager.GetObject("userLockingTime.Properties.AutoHeight");
    this.userLockingTime.Properties.BorderStyle = BorderStyles.Simple;
    this.userLockingTime.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.userLockingTime.Properties.DisplayFormat.FormatString = "#,##0 min";
    this.userLockingTime.Properties.DisplayFormat.FormatType = FormatType.Custom;
    this.userLockingTime.Properties.EditFormat.FormatString = "#,##0 min";
    this.userLockingTime.Properties.EditFormat.FormatType = FormatType.Custom;
    this.userLockingTime.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("userLockingTime.Properties.Mask.AutoComplete");
    this.userLockingTime.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("userLockingTime.Properties.Mask.BeepOnError");
    this.userLockingTime.Properties.Mask.EditMask = componentResourceManager.GetString("userLockingTime.Properties.Mask.EditMask");
    this.userLockingTime.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("userLockingTime.Properties.Mask.IgnoreMaskBlank");
    this.userLockingTime.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("userLockingTime.Properties.Mask.MaskType");
    this.userLockingTime.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("userLockingTime.Properties.Mask.PlaceHolder");
    this.userLockingTime.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("userLockingTime.Properties.Mask.SaveLiteral");
    this.userLockingTime.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("userLockingTime.Properties.Mask.ShowPlaceHolders");
    this.userLockingTime.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("userLockingTime.Properties.Mask.UseMaskAsDisplayFormat");
    this.userLockingTime.Properties.MaxValue = new Decimal(new int[4]
    {
      60,
      0,
      0,
      0
    });
    this.userLockingTime.Properties.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.userLockingTime.Properties.NullValuePrompt = componentResourceManager.GetString("userLockingTime.Properties.NullValuePrompt");
    this.userLockingTime.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("userLockingTime.Properties.NullValuePromptShowForEmptyValue");
    this.userLockingTime.StyleController = (IStyleController) this.layoutControl1;
    this.userLockingTime.Validator = (ICustomValidator) null;
    this.userLockingTime.Value = (object) 1f;
    componentResourceManager.ApplyResources((object) this.activeCheckbox, "activeCheckbox");
    this.activeCheckbox.FormatString = (string) null;
    this.activeCheckbox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.activeCheckbox.IsReadOnly = false;
    this.activeCheckbox.IsUndoing = false;
    this.activeCheckbox.Name = "activeCheckbox";
    this.activeCheckbox.Properties.AccessibleDescription = componentResourceManager.GetString("activeCheckbox.Properties.AccessibleDescription");
    this.activeCheckbox.Properties.AccessibleName = componentResourceManager.GetString("activeCheckbox.Properties.AccessibleName");
    this.activeCheckbox.Properties.Appearance.BackColor = Color.Transparent;
    this.activeCheckbox.Properties.Appearance.BorderColor = Color.Yellow;
    this.activeCheckbox.Properties.Appearance.Options.UseBackColor = true;
    this.activeCheckbox.Properties.Appearance.Options.UseBorderColor = true;
    this.activeCheckbox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("activeCheckbox.Properties.AutoHeight");
    this.activeCheckbox.Properties.BorderStyle = BorderStyles.Simple;
    this.activeCheckbox.Properties.Caption = componentResourceManager.GetString("activeCheckbox.Properties.Caption");
    this.activeCheckbox.Properties.DisplayValueChecked = componentResourceManager.GetString("activeCheckbox.Properties.DisplayValueChecked");
    this.activeCheckbox.Properties.DisplayValueGrayed = componentResourceManager.GetString("activeCheckbox.Properties.DisplayValueGrayed");
    this.activeCheckbox.Properties.DisplayValueUnchecked = componentResourceManager.GetString("activeCheckbox.Properties.DisplayValueUnchecked");
    this.activeCheckbox.StyleController = (IStyleController) this.layoutControl1;
    this.activeCheckbox.Validator = (ICustomValidator) null;
    this.activeCheckbox.Value = (object) false;
    componentResourceManager.ApplyResources((object) this.adminPasswordVerification, "adminPasswordVerification");
    this.adminPasswordVerification.EnterMoveNextControl = true;
    this.adminPasswordVerification.FormatString = (string) null;
    this.adminPasswordVerification.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.adminPasswordVerification.IsMandatory = true;
    this.adminPasswordVerification.IsReadOnly = false;
    this.adminPasswordVerification.IsUndoing = false;
    this.adminPasswordVerification.Name = "adminPasswordVerification";
    this.adminPasswordVerification.PasswordConfirmationPartner = this.adminPassword;
    this.adminPasswordVerification.Properties.AccessibleDescription = componentResourceManager.GetString("adminPasswordVerification.Properties.AccessibleDescription");
    this.adminPasswordVerification.Properties.AccessibleName = componentResourceManager.GetString("adminPasswordVerification.Properties.AccessibleName");
    this.adminPasswordVerification.Properties.Appearance.BackColor = Color.LightYellow;
    this.adminPasswordVerification.Properties.Appearance.BorderColor = Color.LightGray;
    this.adminPasswordVerification.Properties.Appearance.Options.UseBackColor = true;
    this.adminPasswordVerification.Properties.Appearance.Options.UseBorderColor = true;
    this.adminPasswordVerification.Properties.AutoHeight = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.AutoHeight");
    this.adminPasswordVerification.Properties.BorderStyle = BorderStyles.Simple;
    this.adminPasswordVerification.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.AutoComplete");
    this.adminPasswordVerification.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.BeepOnError");
    this.adminPasswordVerification.Properties.Mask.EditMask = componentResourceManager.GetString("adminPasswordVerification.Properties.Mask.EditMask");
    this.adminPasswordVerification.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.IgnoreMaskBlank");
    this.adminPasswordVerification.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.MaskType");
    this.adminPasswordVerification.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.PlaceHolder");
    this.adminPasswordVerification.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.SaveLiteral");
    this.adminPasswordVerification.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.ShowPlaceHolders");
    this.adminPasswordVerification.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.Mask.UseMaskAsDisplayFormat");
    this.adminPasswordVerification.Properties.NullValuePrompt = componentResourceManager.GetString("adminPasswordVerification.Properties.NullValuePrompt");
    this.adminPasswordVerification.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("adminPasswordVerification.Properties.NullValuePromptShowForEmptyValue");
    this.adminPasswordVerification.Properties.PasswordChar = '*';
    this.adminPasswordVerification.StyleController = (IStyleController) this.layoutControl1;
    this.adminPasswordVerification.Validator = (ICustomValidator) null;
    this.adminPasswordVerification.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.adminPassword, "adminPassword");
    this.adminPassword.EnterMoveNextControl = true;
    this.adminPassword.FormatString = (string) null;
    this.adminPassword.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.adminPassword.IsMandatory = true;
    this.adminPassword.IsReadOnly = false;
    this.adminPassword.IsUndoing = false;
    this.adminPassword.Name = "adminPassword";
    this.adminPassword.PasswordConfirmationPartner = this.adminPasswordVerification;
    this.adminPassword.Properties.AccessibleDescription = componentResourceManager.GetString("adminPassword.Properties.AccessibleDescription");
    this.adminPassword.Properties.AccessibleName = componentResourceManager.GetString("adminPassword.Properties.AccessibleName");
    this.adminPassword.Properties.Appearance.BackColor = Color.LightYellow;
    this.adminPassword.Properties.Appearance.BorderColor = Color.LightGray;
    this.adminPassword.Properties.Appearance.Options.UseBackColor = true;
    this.adminPassword.Properties.Appearance.Options.UseBorderColor = true;
    this.adminPassword.Properties.AutoHeight = (bool) componentResourceManager.GetObject("adminPassword.Properties.AutoHeight");
    this.adminPassword.Properties.BorderStyle = BorderStyles.Simple;
    this.adminPassword.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("adminPassword.Properties.Mask.AutoComplete");
    this.adminPassword.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("adminPassword.Properties.Mask.BeepOnError");
    this.adminPassword.Properties.Mask.EditMask = componentResourceManager.GetString("adminPassword.Properties.Mask.EditMask");
    this.adminPassword.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("adminPassword.Properties.Mask.IgnoreMaskBlank");
    this.adminPassword.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("adminPassword.Properties.Mask.MaskType");
    this.adminPassword.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("adminPassword.Properties.Mask.PlaceHolder");
    this.adminPassword.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("adminPassword.Properties.Mask.SaveLiteral");
    this.adminPassword.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("adminPassword.Properties.Mask.ShowPlaceHolders");
    this.adminPassword.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("adminPassword.Properties.Mask.UseMaskAsDisplayFormat");
    this.adminPassword.Properties.NullValuePrompt = componentResourceManager.GetString("adminPassword.Properties.NullValuePrompt");
    this.adminPassword.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("adminPassword.Properties.NullValuePromptShowForEmptyValue");
    this.adminPassword.Properties.PasswordChar = '*';
    this.adminPassword.StyleController = (IStyleController) this.layoutControl1;
    this.adminPassword.Validator = (ICustomValidator) null;
    this.adminPassword.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.profileCombobox, "profileCombobox");
    this.profileCombobox.EnterMoveNextControl = true;
    this.profileCombobox.FormatString = (string) null;
    this.profileCombobox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.profileCombobox.IsActive = false;
    this.profileCombobox.IsMandatory = true;
    this.profileCombobox.IsReadOnly = false;
    this.profileCombobox.IsUndoing = false;
    this.profileCombobox.Name = "profileCombobox";
    this.profileCombobox.Properties.AccessibleDescription = componentResourceManager.GetString("profileCombobox.Properties.AccessibleDescription");
    this.profileCombobox.Properties.AccessibleName = componentResourceManager.GetString("profileCombobox.Properties.AccessibleName");
    this.profileCombobox.Properties.Appearance.BackColor = Color.LightYellow;
    this.profileCombobox.Properties.Appearance.BorderColor = Color.LightGray;
    this.profileCombobox.Properties.Appearance.Options.UseBackColor = true;
    this.profileCombobox.Properties.Appearance.Options.UseBorderColor = true;
    this.profileCombobox.Properties.AutoHeight = (bool) componentResourceManager.GetObject("profileCombobox.Properties.AutoHeight");
    this.profileCombobox.Properties.BorderStyle = BorderStyles.Simple;
    this.profileCombobox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("profileCombobox.Properties.Buttons"))
    });
    this.profileCombobox.Properties.NullValuePrompt = componentResourceManager.GetString("profileCombobox.Properties.NullValuePrompt");
    this.profileCombobox.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("profileCombobox.Properties.NullValuePromptShowForEmptyValue");
    this.profileCombobox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.profileCombobox.ShowEmptyElement = true;
    this.profileCombobox.StyleController = (IStyleController) this.layoutControl1;
    this.profileCombobox.Validator = (ICustomValidator) null;
    this.profileCombobox.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[6]
    {
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.passwordTextbox,
      (BaseLayoutItem) this.passwordTextboxVerification,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.emptySpaceItem3
    });
    this.layoutControlGroup3.Location = new Point(0, 0);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(758, 141);
    this.layoutControlItem2.Control = (Control) this.profileCombobox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 25);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(380, 24);
    this.layoutControlItem2.TextSize = new Size(264, 13);
    this.passwordTextbox.Control = (Control) this.adminPassword;
    componentResourceManager.ApplyResources((object) this.passwordTextbox, "passwordTextbox");
    this.passwordTextbox.Location = new Point(0, 49);
    this.passwordTextbox.Name = "passwordTextbox";
    this.passwordTextbox.Size = new Size(380, 24);
    this.passwordTextbox.TextSize = new Size(264, 13);
    this.passwordTextboxVerification.Control = (Control) this.adminPasswordVerification;
    componentResourceManager.ApplyResources((object) this.passwordTextboxVerification, "passwordTextboxVerification");
    this.passwordTextboxVerification.Location = new Point(0, 73);
    this.passwordTextboxVerification.Name = "passwordTextboxVerification";
    this.passwordTextboxVerification.Size = new Size(380, 24);
    this.passwordTextboxVerification.TextSize = new Size(264, 13);
    this.layoutControlItem3.Control = (Control) this.activeCheckbox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(218, 25);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(218, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(516, 25);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(380, 25);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(354, 72);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    this.layoutControlGroup2.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup2.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutUserManagementSettingGroup
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(778, 511 /*0x01FF*/);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 68);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(758, 423);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutUserManagementSettingGroup, "layoutUserManagementSettingGroup");
    this.layoutUserManagementSettingGroup.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutUserLockingTime,
      (BaseLayoutItem) this.emptySpaceItem4
    });
    this.layoutUserManagementSettingGroup.Location = new Point(0, 0);
    this.layoutUserManagementSettingGroup.Name = "layoutUserManagementSettingGroup";
    this.layoutUserManagementSettingGroup.Size = new Size(758, 68);
    this.layoutUserLockingTime.Control = (Control) this.userLockingTime;
    componentResourceManager.ApplyResources((object) this.layoutUserLockingTime, "layoutUserLockingTime");
    this.layoutUserLockingTime.Location = new Point(0, 0);
    this.layoutUserLockingTime.Name = "layoutUserLockingTime";
    this.layoutUserLockingTime.OptionsToolTip.IconToolTipTitle = "User Locking";
    this.layoutUserLockingTime.OptionsToolTip.ToolTip = componentResourceManager.GetString("resource.ToolTip");
    this.layoutUserLockingTime.OptionsToolTip.ToolTipIconType = (ToolTipIconType) componentResourceManager.GetObject("resource.ToolTipIconType");
    this.layoutUserLockingTime.OptionsToolTip.ToolTipTitle = componentResourceManager.GetString("resource.ToolTipTitle");
    this.layoutUserLockingTime.Size = new Size(224 /*0xE0*/, 24);
    this.layoutUserLockingTime.TextSize = new Size(166, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(224 /*0xE0*/, 0);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(510, 24);
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (ConfigurationEditor);
    this.layoutControlGroup1.EndInit();
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.userLockingTime.Properties.EndInit();
    this.activeCheckbox.Properties.EndInit();
    this.adminPasswordVerification.Properties.EndInit();
    this.adminPassword.Properties.EndInit();
    this.profileCombobox.Properties.EndInit();
    this.layoutControlGroup3.EndInit();
    this.layoutControlItem2.EndInit();
    this.passwordTextbox.EndInit();
    this.passwordTextboxVerification.EndInit();
    this.layoutControlItem3.EndInit();
    this.emptySpaceItem2.EndInit();
    this.emptySpaceItem3.EndInit();
    this.layoutControlGroup2.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutUserManagementSettingGroup.EndInit();
    this.layoutUserLockingTime.EndInit();
    this.emptySpaceItem4.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateConfigurationCallBack(UserProfileManagementConfiguration configuration);

  private delegate void UpdateProfilesCallBack(IEnumerable<UserProfile> userProfile);
}
