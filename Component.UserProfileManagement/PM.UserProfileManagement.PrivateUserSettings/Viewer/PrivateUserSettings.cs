// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings
// Assembly: PM.UserProfileManagement.PrivateUserSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E00D5CAE-3392-4F44-903A-23A515F3DC92
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.PrivateUserSettings.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.Login;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserProfileManagement.PrivateUserSettings.Viewer;

[ToolboxItem(false)]
public sealed class PrivateUserSettings : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DXValidationProvider validationProvider;
  private ModelMapper<User> modelMapper;
  private IContainer components;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private DevExpressComboBoxEdit userLanguage;
  private DevExpressPasswordEdit userPasswordVerification;
  private LayoutControlItem layoutUserPasswordVerification;
  private LayoutControlItem layoutUserLanguage;
  private DevExpressPasswordEdit userPassword;
  private LayoutControlItem layoutUserPassword;

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.ListLoaded && e.ChangeType != ChangeType.SelectionChanged || !(e.Type == typeof (User)) || e.ChangedObject == null)
      return;
    object obj = (object) (e.ChangedObject as ICollection<User>);
    if (obj == null)
      obj = (object) new User[1]{ e.ChangedObject as User };
    ICollection<User> source = (ICollection<User>) obj;
    if (LoginManager.Instance.LoggedInUserData == null)
      return;
    Guid userId = LoginManager.Instance.LoggedInUserData.Id;
    ICollection<User> user = (ICollection<User>) new User[1]
    {
      source.FirstOrDefault<User>((Func<User, bool>) (u => u.Id == userId))
    };
    if (user.Count != 1)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings.UpdateUserDateCallBack(this.FillFields), (object) user);
    else
      this.FillFields(user);
  }

  private void FillFields(ICollection<User> user)
  {
    this.userPassword.IsMandatory = true;
    this.userPasswordVerification.IsMandatory = true;
    this.modelMapper.CopyModelToUI(user);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeModelMapper()
  {
    ModelMapper<User> modelMapper = new ModelMapper<User>(true);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Password), (object) this.userPassword);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Password), (object) this.userPasswordVerification);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Language), (object) this.userLanguage);
    this.modelMapper = modelMapper;
  }

  public PrivateUserSettings()
  {
    this.InitializeComponent();
    this.validationProvider = new DXValidationProvider();
    this.InitializeSelectionValues();
    this.CreateToolBar();
    this.InitializeModelMapper();
  }

  public PrivateUserSettings(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolBar()
  {
    this.ToolbarElements.Clear();
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    RibbonPageGroup ribbonPageGroup = ribbonHelper.CreateRibbonPageGroup(PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources.PrivateUserSettings_RibbonModificationGroup);
    BarButtonItem largeImageButton1 = ribbonHelper.CreateLargeImageButton(PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources.PrivateUserSettings_SaveChangesButtonName, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources>.Instance.ResourceManager.GetObject("Save") as Bitmap, Triggers.Save);
    BarButtonItem largeImageButton2 = ribbonHelper.CreateLargeImageButton(PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources.PrivateUserSettings_RevertChangesButtonName, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources>.Instance.ResourceManager.GetObject("Revert") as Bitmap, Triggers.RevertModifications);
    BarButtonItem largeImageButton3 = ribbonHelper.CreateLargeImageButton(PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources.PrivateUserSettings_UndoChangeButtonName, string.Empty, string.Empty, ComponentResourceManagementBase<PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources>.Instance.ResourceManager.GetObject("Undo") as Bitmap, Triggers.Undo);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton1);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton2);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton3);
    this.ToolbarElements.Add((object) ribbonPageGroup);
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(PathMedical.UserProfileManagement.PrivateUserSettings.Properties.Resources.PrivateUserSettings_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeSelectionValues()
  {
    this.userLanguage.DataSource = (object) SystemConfigurationManager.Instance.SupportedLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl.Name))).Distinct<ComboBoxEditItemWrapper>().OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  public override bool ValidateView()
  {
    bool flag = false;
    CompareAgainstControlValidationRule rule = new CompareAgainstControlValidationRule();
    rule.Control = (Control) this.userPassword;
    rule.CompareControlOperator = CompareControlOperator.Equals;
    rule.CaseSensitive = true;
    rule.ErrorType = ErrorType.Critical;
    rule.ErrorText = PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties.Resources.PasswordsDontMatch;
    this.validationProvider.SetValidationRule((Control) this.userPasswordVerification, (ValidationRuleBase) rule);
    if (this.validationProvider.Validate((Control) this.userPasswordVerification))
      flag = true;
    return flag;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings));
    this.layoutControl1 = new LayoutControl();
    this.userPassword = new DevExpressPasswordEdit();
    this.userPasswordVerification = new DevExpressPasswordEdit();
    this.userLanguage = new DevExpressComboBoxEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutUserPasswordVerification = new LayoutControlItem();
    this.layoutUserLanguage = new LayoutControlItem();
    this.layoutUserPassword = new LayoutControlItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.userPassword.Properties.BeginInit();
    this.userPasswordVerification.Properties.BeginInit();
    this.userLanguage.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutUserPasswordVerification.BeginInit();
    this.layoutUserLanguage.BeginInit();
    this.layoutUserPassword.BeginInit();
    this.SuspendLayout();
    this.layoutControl1.Controls.Add((Control) this.userPassword);
    this.layoutControl1.Controls.Add((Control) this.userLanguage);
    this.layoutControl1.Controls.Add((Control) this.userPasswordVerification);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.userPassword, "userPassword");
    this.userPassword.EnterMoveNextControl = true;
    this.userPassword.FormatString = (string) null;
    this.userPassword.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPassword.IsMandatory = true;
    this.userPassword.IsReadOnly = false;
    this.userPassword.IsUndoing = false;
    this.userPassword.Name = "userPassword";
    this.userPassword.PasswordConfirmationPartner = this.userPasswordVerification;
    this.userPassword.Properties.Appearance.BackColor = Color.LightYellow;
    this.userPassword.Properties.Appearance.BorderColor = Color.LightGray;
    this.userPassword.Properties.Appearance.Options.UseBackColor = true;
    this.userPassword.Properties.Appearance.Options.UseBorderColor = true;
    this.userPassword.Properties.BorderStyle = BorderStyles.Simple;
    this.userPassword.Properties.NullValuePrompt = componentResourceManager.GetString("userPassword.Properties.NullValuePrompt");
    this.userPassword.Properties.PasswordChar = '*';
    this.userPassword.StyleController = (IStyleController) this.layoutControl1;
    this.userPassword.Validator = (ICustomValidator) null;
    this.userPassword.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.userPasswordVerification, "userPasswordVerification");
    this.userPasswordVerification.EnterMoveNextControl = true;
    this.userPasswordVerification.FormatString = (string) null;
    this.userPasswordVerification.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPasswordVerification.IsMandatory = true;
    this.userPasswordVerification.IsReadOnly = false;
    this.userPasswordVerification.IsUndoing = false;
    this.userPasswordVerification.Name = "userPasswordVerification";
    this.userPasswordVerification.PasswordConfirmationPartner = this.userPassword;
    this.userPasswordVerification.Properties.Appearance.BackColor = Color.LightYellow;
    this.userPasswordVerification.Properties.Appearance.BorderColor = Color.LightGray;
    this.userPasswordVerification.Properties.Appearance.Options.UseBackColor = true;
    this.userPasswordVerification.Properties.Appearance.Options.UseBorderColor = true;
    this.userPasswordVerification.Properties.BorderStyle = BorderStyles.Simple;
    this.userPasswordVerification.Properties.NullValuePrompt = componentResourceManager.GetString("userPasswordVerification.Properties.NullValuePrompt");
    this.userPasswordVerification.Properties.PasswordChar = '*';
    this.userPasswordVerification.StyleController = (IStyleController) this.layoutControl1;
    this.userPasswordVerification.Validator = (ICustomValidator) null;
    this.userPasswordVerification.Value = (object) null;
    this.userLanguage.EnterMoveNextControl = true;
    this.userLanguage.FormatString = (string) null;
    this.userLanguage.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLanguage.IsReadOnly = false;
    this.userLanguage.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.userLanguage, "userLanguage");
    this.userLanguage.Name = "userLanguage";
    this.userLanguage.Properties.Appearance.BorderColor = Color.LightGray;
    this.userLanguage.Properties.Appearance.Options.UseBorderColor = true;
    this.userLanguage.Properties.BorderStyle = BorderStyles.Simple;
    this.userLanguage.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("userLanguage.Properties.Buttons"))
    });
    this.userLanguage.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userLanguage.ShowEmptyElement = false;
    this.userLanguage.StyleController = (IStyleController) this.layoutControl1;
    this.userLanguage.Validator = (ICustomValidator) null;
    this.userLanguage.Value = (object) null;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutUserPasswordVerification,
      (BaseLayoutItem) this.layoutUserLanguage,
      (BaseLayoutItem) this.layoutUserPassword
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(456, 315);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    this.layoutUserPasswordVerification.Control = (Control) this.userPasswordVerification;
    componentResourceManager.ApplyResources((object) this.layoutUserPasswordVerification, "layoutUserPasswordVerification");
    this.layoutUserPasswordVerification.Location = new Point(0, 24);
    this.layoutUserPasswordVerification.Name = "layoutUserPasswordVerification";
    this.layoutUserPasswordVerification.Size = new Size(436, 24);
    this.layoutUserPasswordVerification.TextSize = new Size(53, 13);
    this.layoutUserLanguage.Control = (Control) this.userLanguage;
    componentResourceManager.ApplyResources((object) this.layoutUserLanguage, "layoutUserLanguage");
    this.layoutUserLanguage.Location = new Point(0, 48 /*0x30*/);
    this.layoutUserLanguage.Name = "layoutUserLanguage";
    this.layoutUserLanguage.Size = new Size(436, 247);
    this.layoutUserLanguage.TextSize = new Size(53, 13);
    this.layoutUserPassword.Control = (Control) this.userPassword;
    componentResourceManager.ApplyResources((object) this.layoutUserPassword, "layoutUserPassword");
    this.layoutUserPassword.Location = new Point(0, 0);
    this.layoutUserPassword.Name = "layoutUserPassword";
    this.layoutUserPassword.Size = new Size(436, 24);
    this.layoutUserPassword.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (PrivateUserSettings);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.userPassword.Properties.EndInit();
    this.userPasswordVerification.Properties.EndInit();
    this.userLanguage.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutUserPasswordVerification.EndInit();
    this.layoutUserLanguage.EndInit();
    this.layoutUserPassword.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateUserDateCallBack(ICollection<User> user);
}
