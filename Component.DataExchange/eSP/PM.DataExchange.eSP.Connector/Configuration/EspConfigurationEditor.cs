// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration.EspConfigurationEditor
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using PathMedical.DataExchange.eSP.Connector.WindowsForms.Properties;
using PathMedical.Exception;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration;

[ToolboxItem(false)]
public sealed class EspConfigurationEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DXValidationProvider validationProvider;
  private ModelMapper<EspConfiguration> modelMapper;
  private IContainer components;
  private LayoutControl layoutControl;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlGroup layoutEspConfiguration;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlGroup layoutEspUserConfiguration;
  private DevExpressComboBoxEdit userProfile;
  private DevExpressTextEdit espSite;
  private DevExpressTextEdit espRemoteAddress;
  private LayoutControlItem layoutEspRemoteAddress;
  private LayoutControlItem layoutEspSite;
  private LayoutControlItem layoutUserProfile;
  private DevExpressPasswordEdit userPassword;
  private LayoutControlItem layoutUserPassword;
  private DevExpressPasswordEdit userPasswordVerification;
  private LayoutControlItem layoutUserPasswordVerification;
  private DevExpressTextEdit backupFolder;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutControlItem1;

  public EspConfigurationEditor()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.validationProvider = new DXValidationProvider();
    this.espRemoteAddress.Validator = (ICustomValidator) new UriValidator()
    {
      ErrorText = "The URI for the eSP server is invalid."
    };
    this.backupFolder.Validator = (ICustomValidator) new FolderNameValidator()
    {
      ErrorText = "The backup folder name contains not the corrent format."
    };
  }

  public EspConfigurationEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.EspConfigurationEditor_GoBackGroupName));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.EspConfigurationEditor_ModificationGroupName));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.EspConfigurationEditor_HelpGroupName, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeModelMapper()
  {
    ModelMapper<EspConfiguration> modelMapper = new ModelMapper<EspConfiguration>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.EspRemoteAddress), (object) this.espRemoteAddress);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.HomeSite), (object) this.espSite);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.DefaultUserPassword), (object) this.userPassword);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.DefaultUserPassword), (object) this.userPasswordVerification);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.DefaultUserProfile), (object) this.userProfile);
    modelMapper.Add((Expression<Func<EspConfiguration, object>>) (s => s.BackupFolder), (object) this.backupFolder);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType == ChangeType.ItemEdited && e.Type == typeof (EspConfiguration))
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new EspConfigurationEditor.UpdateEspConfigurationCallBack(this.FillFields), (object) (e.ChangedObject as EspConfiguration));
      else
        this.FillFields(e.ChangedObject as EspConfiguration);
    }
    else
    {
      if (e.ChangeType != ChangeType.ListLoaded || !(e.Type == typeof (UserProfile)))
        return;
      if (this.userProfile.InvokeRequired)
        this.userProfile.BeginInvoke((Delegate) new EspConfigurationEditor.UpdateProfilesCallBack(this.FillProfile), (object) (e.ChangedObject as IEnumerable<UserProfile>));
      else
        this.FillProfile(e.ChangedObject as IEnumerable<UserProfile>);
    }
  }

  private void FillFields(EspConfiguration configuration)
  {
    this.userPassword.IsMandatory = this.userPasswordVerification.IsMandatory = string.IsNullOrEmpty(configuration.DefaultUserPassword);
    this.modelMapper.CopyModelToUI(configuration);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void FillProfile(IEnumerable<UserProfile> userProfiles)
  {
    this.userProfile.DataSource = (object) userProfiles.Select<UserProfile, ComboBoxEditItemWrapper>((Func<UserProfile, ComboBoxEditItemWrapper>) (up => new ComboBoxEditItemWrapper(up.Name, (object) up))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  public override bool ValidateView()
  {
    bool flag1 = false;
    bool flag2 = true;
    if (this.espRemoteAddress.Validator != null)
    {
      this.validationProvider.SetValidationRule((Control) this.espRemoteAddress, (ValidationRuleBase) new FieldValidationRule(this.espRemoteAddress.Validator, this.espRemoteAddress.Validator.ErrorText));
      flag2 = this.validationProvider.Validate((Control) this.espRemoteAddress);
    }
    CompareAgainstControlValidationRule rule = new CompareAgainstControlValidationRule();
    rule.Control = (Control) this.userPassword;
    rule.CompareControlOperator = CompareControlOperator.Equals;
    rule.CaseSensitive = true;
    rule.ErrorType = ErrorType.Critical;
    rule.ErrorText = PathMedical.ComponentResourceManagement.Instance.ResourceManager.GetString("PasswordsDontMatch");
    this.validationProvider.SetValidationRule((Control) this.userPasswordVerification, (ValidationRuleBase) rule);
    bool flag3 = this.validationProvider.Validate((Control) this.userPasswordVerification);
    if (base.ValidateView() & flag3 & flag2)
      flag1 = true;
    return flag1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.layoutControl = new LayoutControl();
    this.userPassword = new DevExpressPasswordEdit();
    this.userPasswordVerification = new DevExpressPasswordEdit();
    this.userProfile = new DevExpressComboBoxEdit();
    this.espSite = new DevExpressTextEdit();
    this.espRemoteAddress = new DevExpressTextEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutEspConfiguration = new LayoutControlGroup();
    this.layoutEspRemoteAddress = new LayoutControlItem();
    this.layoutEspSite = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutEspUserConfiguration = new LayoutControlGroup();
    this.layoutUserProfile = new LayoutControlItem();
    this.layoutUserPassword = new LayoutControlItem();
    this.layoutUserPasswordVerification = new LayoutControlItem();
    this.backupFolder = new DevExpressTextEdit();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControl.BeginInit();
    this.layoutControl.SuspendLayout();
    this.userPassword.Properties.BeginInit();
    this.userPasswordVerification.Properties.BeginInit();
    this.userProfile.Properties.BeginInit();
    this.espSite.Properties.BeginInit();
    this.espRemoteAddress.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutEspConfiguration.BeginInit();
    this.layoutEspRemoteAddress.BeginInit();
    this.layoutEspSite.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutEspUserConfiguration.BeginInit();
    this.layoutUserProfile.BeginInit();
    this.layoutUserPassword.BeginInit();
    this.layoutUserPasswordVerification.BeginInit();
    this.backupFolder.Properties.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.SuspendLayout();
    this.layoutControl.Controls.Add((Control) this.backupFolder);
    this.layoutControl.Controls.Add((Control) this.userPassword);
    this.layoutControl.Controls.Add((Control) this.userProfile);
    this.layoutControl.Controls.Add((Control) this.espSite);
    this.layoutControl.Controls.Add((Control) this.espRemoteAddress);
    this.layoutControl.Controls.Add((Control) this.userPasswordVerification);
    this.layoutControl.Dock = DockStyle.Fill;
    this.layoutControl.Location = new Point(0, 0);
    this.layoutControl.Name = "layoutControl";
    this.layoutControl.Root = this.layoutControlGroup1;
    this.layoutControl.Size = new Size(960, 750);
    this.layoutControl.TabIndex = 4;
    this.layoutControl.Text = "layoutControl1";
    this.userPassword.Caption = (string) null;
    this.userPassword.EnterMoveNextControl = true;
    this.userPassword.FormatString = (string) null;
    this.userPassword.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPassword.IsMandatory = true;
    this.userPassword.IsReadOnly = false;
    this.userPassword.IsUndoing = false;
    this.userPassword.Location = new Point(128 /*0x80*/, 160 /*0xA0*/);
    this.userPassword.Name = "userPassword";
    this.userPassword.PasswordConfirmationPartner = this.userPasswordVerification;
    this.userPassword.Properties.Appearance.BackColor = Color.LightYellow;
    this.userPassword.Properties.Appearance.BorderColor = Color.LightGray;
    this.userPassword.Properties.Appearance.Options.UseBackColor = true;
    this.userPassword.Properties.Appearance.Options.UseBorderColor = true;
    this.userPassword.Properties.BorderStyle = BorderStyles.Simple;
    this.userPassword.Properties.NullValuePrompt = "**********";
    this.userPassword.Properties.PasswordChar = '*';
    this.userPassword.Size = new Size(808, 20);
    this.userPassword.StyleController = (IStyleController) this.layoutControl;
    this.userPassword.TabIndex = 13;
    this.userPassword.Validator = (ICustomValidator) null;
    this.userPassword.Value = (object) null;
    this.userPasswordVerification.Caption = (string) null;
    this.userPasswordVerification.EnterMoveNextControl = true;
    this.userPasswordVerification.FormatString = (string) null;
    this.userPasswordVerification.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPasswordVerification.IsMandatory = true;
    this.userPasswordVerification.IsReadOnly = false;
    this.userPasswordVerification.IsUndoing = false;
    this.userPasswordVerification.Location = new Point(128 /*0x80*/, 184);
    this.userPasswordVerification.Name = "userPasswordVerification";
    this.userPasswordVerification.PasswordConfirmationPartner = this.userPassword;
    this.userPasswordVerification.Properties.Appearance.BackColor = Color.LightYellow;
    this.userPasswordVerification.Properties.Appearance.BorderColor = Color.LightGray;
    this.userPasswordVerification.Properties.Appearance.Options.UseBackColor = true;
    this.userPasswordVerification.Properties.Appearance.Options.UseBorderColor = true;
    this.userPasswordVerification.Properties.BorderStyle = BorderStyles.Simple;
    this.userPasswordVerification.Properties.NullValuePrompt = "**********";
    this.userPasswordVerification.Properties.PasswordChar = '*';
    this.userPasswordVerification.Size = new Size(808, 20);
    this.userPasswordVerification.StyleController = (IStyleController) this.layoutControl;
    this.userPasswordVerification.TabIndex = 14;
    this.userPasswordVerification.Validator = (ICustomValidator) null;
    this.userPasswordVerification.Value = (object) null;
    this.userProfile.EnterMoveNextControl = true;
    this.userProfile.FormatString = (string) null;
    this.userProfile.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userProfile.IsMandatory = true;
    this.userProfile.IsReadOnly = false;
    this.userProfile.IsUndoing = false;
    this.userProfile.Location = new Point(128 /*0x80*/, 136);
    this.userProfile.Name = "userProfile";
    this.userProfile.Properties.Appearance.BackColor = Color.LightYellow;
    this.userProfile.Properties.Appearance.BorderColor = Color.LightGray;
    this.userProfile.Properties.Appearance.Options.UseBackColor = true;
    this.userProfile.Properties.Appearance.Options.UseBorderColor = true;
    this.userProfile.Properties.BorderStyle = BorderStyles.Simple;
    this.userProfile.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.userProfile.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userProfile.ShowEmptyElement = true;
    this.userProfile.Size = new Size(808, 20);
    this.userProfile.StyleController = (IStyleController) this.layoutControl;
    this.userProfile.TabIndex = 12;
    this.userProfile.Validator = (ICustomValidator) null;
    this.userProfile.Value = (object) null;
    this.espSite.Caption = (string) null;
    this.espSite.EditValue = (object) "";
    this.espSite.EnterMoveNextControl = true;
    this.espSite.FormatString = (string) null;
    this.espSite.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.espSite.IsMandatory = true;
    this.espSite.IsReadOnly = false;
    this.espSite.IsUndoing = false;
    this.espSite.Location = new Point(128 /*0x80*/, 68);
    this.espSite.Name = "espSite";
    this.espSite.Properties.Appearance.BackColor = Color.LightYellow;
    this.espSite.Properties.Appearance.BorderColor = Color.LightGray;
    this.espSite.Properties.Appearance.Options.UseBackColor = true;
    this.espSite.Properties.Appearance.Options.UseBorderColor = true;
    this.espSite.Properties.BorderStyle = BorderStyles.Simple;
    this.espSite.Size = new Size(808, 20);
    this.espSite.StyleController = (IStyleController) this.layoutControl;
    this.espSite.TabIndex = 11;
    this.espSite.Validator = (ICustomValidator) null;
    this.espSite.Value = (object) "";
    this.espRemoteAddress.Caption = (string) null;
    this.espRemoteAddress.EditValue = (object) "";
    this.espRemoteAddress.EnterMoveNextControl = true;
    this.espRemoteAddress.FormatString = (string) null;
    this.espRemoteAddress.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.espRemoteAddress.IsMandatory = true;
    this.espRemoteAddress.IsReadOnly = false;
    this.espRemoteAddress.IsUndoing = false;
    this.espRemoteAddress.Location = new Point(128 /*0x80*/, 44);
    this.espRemoteAddress.Name = "espRemoteAddress";
    this.espRemoteAddress.Properties.Appearance.BackColor = Color.LightYellow;
    this.espRemoteAddress.Properties.Appearance.BorderColor = Color.LightGray;
    this.espRemoteAddress.Properties.Appearance.Options.UseBackColor = true;
    this.espRemoteAddress.Properties.Appearance.Options.UseBorderColor = true;
    this.espRemoteAddress.Properties.BorderStyle = BorderStyles.Simple;
    this.espRemoteAddress.Size = new Size(808, 20);
    this.espRemoteAddress.StyleController = (IStyleController) this.layoutControl;
    this.espRemoteAddress.TabIndex = 10;
    this.espRemoteAddress.Validator = (ICustomValidator) null;
    this.espRemoteAddress.Value = (object) "";
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutEspConfiguration,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutEspUserConfiguration,
      (BaseLayoutItem) this.layoutControlGroup2
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.Text = "layoutControlGroup1";
    this.layoutControlGroup1.TextVisible = false;
    this.layoutEspConfiguration.CustomizationFormText = "eSP Configuration";
    this.layoutEspConfiguration.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutEspRemoteAddress,
      (BaseLayoutItem) this.layoutEspSite
    });
    this.layoutEspConfiguration.Location = new Point(0, 0);
    this.layoutEspConfiguration.Name = "layoutEspConfiguration";
    this.layoutEspConfiguration.Size = new Size(940, 92);
    this.layoutEspConfiguration.Text = "eSP Configuration";
    this.layoutEspRemoteAddress.Control = (Control) this.espRemoteAddress;
    this.layoutEspRemoteAddress.CustomizationFormText = "eSP Remote Address";
    this.layoutEspRemoteAddress.Location = new Point(0, 0);
    this.layoutEspRemoteAddress.Name = "layoutEspRemoteAddress";
    this.layoutEspRemoteAddress.Size = new Size(916, 24);
    this.layoutEspRemoteAddress.Text = "eSP Remote Address";
    this.layoutEspRemoteAddress.TextSize = new Size(100, 13);
    this.layoutEspSite.Control = (Control) this.espSite;
    this.layoutEspSite.CustomizationFormText = "Site";
    this.layoutEspSite.Location = new Point(0, 24);
    this.layoutEspSite.Name = "layoutEspSite";
    this.layoutEspSite.Size = new Size(916, 24);
    this.layoutEspSite.Text = "Site";
    this.layoutEspSite.TextSize = new Size(100, 13);
    this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
    this.emptySpaceItem1.Location = new Point(0, 276);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(940, 454);
    this.emptySpaceItem1.Text = "emptySpaceItem1";
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutEspUserConfiguration.CustomizationFormText = "eSP User Configuration";
    this.layoutEspUserConfiguration.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutUserProfile,
      (BaseLayoutItem) this.layoutUserPassword,
      (BaseLayoutItem) this.layoutUserPasswordVerification
    });
    this.layoutEspUserConfiguration.Location = new Point(0, 92);
    this.layoutEspUserConfiguration.Name = "layoutEspUserConfiguration";
    this.layoutEspUserConfiguration.Size = new Size(940, 116);
    this.layoutEspUserConfiguration.Text = "eSP User Configuration";
    this.layoutUserProfile.Control = (Control) this.userProfile;
    this.layoutUserProfile.CustomizationFormText = "Default User Profile";
    this.layoutUserProfile.Location = new Point(0, 0);
    this.layoutUserProfile.Name = "layoutUserProfile";
    this.layoutUserProfile.Size = new Size(916, 24);
    this.layoutUserProfile.Text = "Default User Profile";
    this.layoutUserProfile.TextSize = new Size(100, 13);
    this.layoutUserPassword.Control = (Control) this.userPassword;
    this.layoutUserPassword.CustomizationFormText = "Default Password";
    this.layoutUserPassword.Location = new Point(0, 24);
    this.layoutUserPassword.Name = "layoutUserPassword";
    this.layoutUserPassword.Size = new Size(916, 24);
    this.layoutUserPassword.Text = "Default Password";
    this.layoutUserPassword.TextSize = new Size(100, 13);
    this.layoutUserPasswordVerification.Control = (Control) this.userPasswordVerification;
    this.layoutUserPasswordVerification.CustomizationFormText = "User Password Verification";
    this.layoutUserPasswordVerification.Location = new Point(0, 48 /*0x30*/);
    this.layoutUserPasswordVerification.Name = "layoutUserPasswordVerification";
    this.layoutUserPasswordVerification.Size = new Size(916, 24);
    this.layoutUserPasswordVerification.Text = "Password Verfication";
    this.layoutUserPasswordVerification.TextSize = new Size(100, 13);
    this.backupFolder.Caption = (string) null;
    this.backupFolder.EnterMoveNextControl = true;
    this.backupFolder.FormatString = (string) null;
    this.backupFolder.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.backupFolder.IsMandatory = true;
    this.backupFolder.IsReadOnly = false;
    this.backupFolder.IsUndoing = false;
    this.backupFolder.Location = new Point(128 /*0x80*/, 252);
    this.backupFolder.Name = "backupFolder";
    this.backupFolder.Properties.Appearance.BorderColor = Color.LightGray;
    this.backupFolder.Properties.Appearance.Options.UseBorderColor = true;
    this.backupFolder.Properties.BorderStyle = BorderStyles.Simple;
    this.backupFolder.Size = new Size(808, 20);
    this.backupFolder.StyleController = (IStyleController) this.layoutControl;
    this.backupFolder.TabIndex = 15;
    this.backupFolder.Validator = (ICustomValidator) null;
    this.backupFolder.Value = (object) "";
    this.layoutControlItem1.Control = (Control) this.backupFolder;
    this.layoutControlItem1.CustomizationFormText = "Backup Folder";
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(916, 24);
    this.layoutControlItem1.Text = "Backup Folder";
    this.layoutControlItem1.TextSize = new Size(100, 13);
    this.layoutControlGroup2.CustomizationFormText = "System Safety";
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutControlGroup2.Location = new Point(0, 208 /*0xD0*/);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(940, 68);
    this.layoutControlGroup2.Text = "System Safety";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl);
    this.Name = nameof (EspConfigurationEditor);
    this.Size = new Size(960, 750);
    this.layoutControl.EndInit();
    this.layoutControl.ResumeLayout(false);
    this.userPassword.Properties.EndInit();
    this.userPasswordVerification.Properties.EndInit();
    this.userProfile.Properties.EndInit();
    this.espSite.Properties.EndInit();
    this.espRemoteAddress.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutEspConfiguration.EndInit();
    this.layoutEspRemoteAddress.EndInit();
    this.layoutEspSite.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutEspUserConfiguration.EndInit();
    this.layoutUserProfile.EndInit();
    this.layoutUserPassword.EndInit();
    this.layoutUserPasswordVerification.EndInit();
    this.backupFolder.Properties.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateEspConfigurationCallBack(EspConfiguration configuration);

  private delegate void UpdateProfilesCallBack(IEnumerable<UserProfile> userProfiles);
}
