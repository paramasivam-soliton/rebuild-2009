// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser.UserAccountMasterDetailBrowser
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using PathMedical.Automaton;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser;

[ToolboxItem(false)]
public sealed class UserAccountMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DXValidationProvider validationProvider;
  private ModelMapper<User> modelMapper;
  private IContainer components;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private GridControl userGridControl;
  private GridView userGridView;
  private LayoutControlItem layoutUserGridControl;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressTextEdit userLoginName;
  private LayoutControlItem layoutLoginName;
  private DevExpressTextEdit userForename;
  private DevExpressTextEdit userSurname;
  private DevExpressTextEdit userLoginId;
  private LayoutControlItem layoutLoginId;
  private LayoutControlItem layoutUserSurname;
  private LayoutControlItem layoutUserForename;
  private DevExpressComboBoxEdit userFacility;
  private DevExpressComboBoxEdit userProfile;
  private LayoutControlItem layoutUserProfile;
  private LayoutControlItem layoutUserFacility;
  private DevExpressComboBoxEdit userState;
  private LayoutControlItem layoutUserState;
  private LayoutControlGroup layoutUserInformation;
  private EmptySpaceItem emptySpaceItem2;
  private GridColumn columnUserLoginName;
  private GridColumn columnUserSurname;
  private GridColumn columnFullName;
  private GridColumn columnForename;
  private GridColumn columnLoginId;
  private GridColumn columnIsActive;
  private GridColumn columnLanguage;
  private GridColumn columnLockTimeStamp;
  private GridColumn columnProfile;
  private DevExpressComboBoxEdit userLanguage;
  private LayoutControlItem layoutUserLanguage;
  private LayoutControlGroup layoutPrivateUserInformation;
  private DevExpressTextEdit userLockedTimeStamp;
  private LayoutControlItem layoutUserLockedTimeStamp;
  private LayoutControlGroup layoutSystemInformation;
  private DevExpressPasswordEdit userPassword;
  private LayoutControlItem layoutUserPassword;
  private DevExpressPasswordEdit userPasswordVerification;
  private LayoutControlItem layoutUserPasswordVerification;
  private LayoutControlGroup layoutUserGroup;

  public UserAccountMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.validationProvider = new DXValidationProvider();
    this.CreateCommands();
    this.Dock = DockStyle.Fill;
    this.HelpMarker = "user_mgmt_01.html";
  }

  public UserAccountMasterDetailBrowser(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    DevExpressSingleSelectionGridViewHelper<User> selectionGridViewHelper = new DevExpressSingleSelectionGridViewHelper<User>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.userGridView, model, Triggers.ChangeSelection);
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
    if (SystemConfigurationManager.Instance.IsSiteAndFacilityManagementActive)
      return;
    this.layoutUserFacility.Visibility = LayoutVisibility.Never;
  }

  private void CreateCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    RibbonPageGroup maintenanceGroup = ribbonHelper.CreateMaintenanceGroup(Resources.UserAccountMasterDetailBrowser_RibbonMaintenanceGroup, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("UserAdd"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("UserEdit"), (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("UserDelete"));
    maintenanceGroup.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(Resources.UserAccountMasterDetailBrowser_UnlockAccountButton, Resources.UserAccountMasterDetailBrowser_UnlockAccountButtonDescription, Resources.UserAccountMasterDetailBrowser_UnlockAccountButtonToolTip, (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("UserUnlock"), UserProfileManagementTriggers.UnlockUserAccount));
    this.ToolbarElements.Add((object) maintenanceGroup);
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.UserAccountMasterDetailBrowser_RibbonModificationGroup));
    RibbonPageGroup ribbonPageGroup1 = ribbonHelper.CreateRibbonPageGroup(Resources.UserAccountMasterDetailBrowser_RibbonConfigurationGroup);
    BarButtonItem largeImageButton = ribbonHelper.CreateLargeImageButton(Resources.UserAccountMasterDetailBrowser_ConfigureProfilesButtonName, Resources.UserAccountMasterDetailBrowser_ConfigureProfileButtonDescription, string.Empty, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Profiles") as Bitmap, UserProfileManagementTriggers.SwitchToProfileBrowser);
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton);
    this.ToolbarElements.Add((object) ribbonPageGroup1);
    foreach (IInstrumentPlugin instrumentPlugin1 in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      IInstrumentPlugin instrumentPlugin2 = instrumentPlugin1;
      if (instrumentPlugin2 != null)
      {
        RibbonPageGroup ribbonPageGroup2 = ribbonHelper.CreateRibbonPageGroup(instrumentPlugin1.Name);
        if (instrumentPlugin2.ConfigurationSynchronizationModuleType != (Type) null)
        {
          IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin2.ConfigurationSynchronizationModuleType);
          ribbonPageGroup2.ItemLinks.Add((BarItem) ribbonHelper.CreateLargeImageButton(applicationComponentModule));
        }
        if (ribbonPageGroup2.ItemLinks.Count > 0)
          this.ToolbarElements.Add((object) ribbonPageGroup2);
      }
    }
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.UserAccountMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeSelectionValues()
  {
    this.userState.DataSource = (object) new List<object>()
    {
      (object) new ComboBoxEditItemWrapper(Resources.UserAccountMasterDetailBrowser_UserAccountStateActive, (object) true),
      (object) new ComboBoxEditItemWrapper(Resources.UserAccountMasterDetailBrowser_UserAccountStateInactive, (object) false)
    };
    this.userLanguage.DataSource = (object) SystemConfigurationManager.Instance.SupportedLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl.Name))).Distinct<ComboBoxEditItemWrapper>().OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  public override bool ValidateView()
  {
    bool flag1 = false;
    CompareAgainstControlValidationRule rule = new CompareAgainstControlValidationRule();
    rule.Control = (Control) this.userPassword;
    rule.CompareControlOperator = CompareControlOperator.Equals;
    rule.CaseSensitive = true;
    rule.ErrorType = ErrorType.Critical;
    rule.ErrorText = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("PasswordsDontMatch");
    this.validationProvider.SetValidationRule((Control) this.userPasswordVerification, (ValidationRuleBase) rule);
    bool flag2 = this.validationProvider.Validate((Control) this.userPasswordVerification);
    if (base.ValidateView() & flag2)
      flag1 = true;
    return flag1;
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool flag = this.ViewMode == ViewModeType.Adding;
    this.userPasswordVerification.IsMandatory = flag;
    this.userPassword.IsMandatory = flag;
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && (e.ChangedObject is User || e.ChangedObject is ICollection<User>))
    {
      object obj = (object) (e.ChangedObject as ICollection<User>);
      if (obj == null)
        obj = (object) new User[1]
        {
          e.ChangedObject as User
        };
      ICollection<User> users = (ICollection<User>) obj;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new UserAccountMasterDetailBrowser.UpdateUsersCallBack(this.FillFields), (object) users);
      else
        this.FillFields(users);
    }
    if (!(e.ChangedObject is IEnumerable<UserProfile>) || e.ChangeType == ChangeType.SelectionChanged)
      return;
    IEnumerable<UserProfile> changedObject = e.ChangedObject as IEnumerable<UserProfile>;
    if (this.userProfile.InvokeRequired)
      this.userProfile.BeginInvoke((Delegate) new UserAccountMasterDetailBrowser.UpdateProfilesCallBack(this.FillProfile), (object) changedObject);
    else
      this.FillProfile(changedObject);
  }

  private void FillFields(ICollection<User> users)
  {
    this.userPassword.IsMandatory = this.userPasswordVerification.IsMandatory = users.Count == 1 && EntityHelper.IsNewEntity((object) users.First<User>());
    this.modelMapper.CopyModelToUI(users);
  }

  private void FillProfile(IEnumerable<UserProfile> userProfiles)
  {
    this.userProfile.DataSource = (object) userProfiles.Select<UserProfile, ComboBoxEditItemWrapper>((Func<UserProfile, ComboBoxEditItemWrapper>) (up => new ComboBoxEditItemWrapper(up.Name, (object) up))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeModelMapper()
  {
    ModelMapper<User> modelMapper = new ModelMapper<User>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.LoginName), (object) this.userLoginName);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.LoginId), (object) this.userLoginId);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Forename), (object) this.userForename);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Surname), (object) this.userSurname);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Password), (object) this.userPassword);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Password), (object) this.userPasswordVerification);
    modelMapper.Add((Expression<Func<User, object>>) (u => (object) u.IsActive), (object) this.userState);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Profile), (object) this.userProfile);
    modelMapper.Add((Expression<Func<User, object>>) (u => u.Language), (object) this.userLanguage);
    modelMapper.Add((Expression<Func<User, object>>) (u => (object) u.LockTimestamp), (object) this.userLockedTimeStamp);
    this.modelMapper = modelMapper;
    this.modelMapper.SetUIEnabledForced(false, (object) this.userLockedTimeStamp);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserAccountMasterDetailBrowser));
    StyleFormatCondition styleFormatCondition = new StyleFormatCondition();
    this.columnIsActive = new GridColumn();
    this.layoutControl1 = new LayoutControl();
    this.userPasswordVerification = new DevExpressPasswordEdit();
    this.userPassword = new DevExpressPasswordEdit();
    this.userLockedTimeStamp = new DevExpressTextEdit();
    this.userLanguage = new DevExpressComboBoxEdit();
    this.userState = new DevExpressComboBoxEdit();
    this.userFacility = new DevExpressComboBoxEdit();
    this.userProfile = new DevExpressComboBoxEdit();
    this.userForename = new DevExpressTextEdit();
    this.userSurname = new DevExpressTextEdit();
    this.userLoginId = new DevExpressTextEdit();
    this.userLoginName = new DevExpressTextEdit();
    this.userGridControl = new GridControl();
    this.userGridView = new GridView();
    this.columnUserLoginName = new GridColumn();
    this.columnUserSurname = new GridColumn();
    this.columnForename = new GridColumn();
    this.columnFullName = new GridColumn();
    this.columnLoginId = new GridColumn();
    this.columnLanguage = new GridColumn();
    this.columnProfile = new GridColumn();
    this.columnLockTimeStamp = new GridColumn();
    this.layoutLoginId = new LayoutControlItem();
    this.layoutUserFacility = new LayoutControlItem();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutUserInformation = new LayoutControlGroup();
    this.layoutLoginName = new LayoutControlItem();
    this.layoutUserSurname = new LayoutControlItem();
    this.layoutUserProfile = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutUserForename = new LayoutControlItem();
    this.layoutPrivateUserInformation = new LayoutControlGroup();
    this.layoutUserLanguage = new LayoutControlItem();
    this.layoutUserPassword = new LayoutControlItem();
    this.layoutUserPasswordVerification = new LayoutControlItem();
    this.layoutSystemInformation = new LayoutControlGroup();
    this.layoutUserState = new LayoutControlItem();
    this.layoutUserLockedTimeStamp = new LayoutControlItem();
    this.layoutUserGroup = new LayoutControlGroup();
    this.layoutUserGridControl = new LayoutControlItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.userPasswordVerification.Properties.BeginInit();
    this.userPassword.Properties.BeginInit();
    this.userLockedTimeStamp.Properties.BeginInit();
    this.userLanguage.Properties.BeginInit();
    this.userState.Properties.BeginInit();
    this.userFacility.Properties.BeginInit();
    this.userProfile.Properties.BeginInit();
    this.userForename.Properties.BeginInit();
    this.userSurname.Properties.BeginInit();
    this.userLoginId.Properties.BeginInit();
    this.userLoginName.Properties.BeginInit();
    this.userGridControl.BeginInit();
    this.userGridView.BeginInit();
    this.layoutLoginId.BeginInit();
    this.layoutUserFacility.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutUserInformation.BeginInit();
    this.layoutLoginName.BeginInit();
    this.layoutUserSurname.BeginInit();
    this.layoutUserProfile.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutUserForename.BeginInit();
    this.layoutPrivateUserInformation.BeginInit();
    this.layoutUserLanguage.BeginInit();
    this.layoutUserPassword.BeginInit();
    this.layoutUserPasswordVerification.BeginInit();
    this.layoutSystemInformation.BeginInit();
    this.layoutUserState.BeginInit();
    this.layoutUserLockedTimeStamp.BeginInit();
    this.layoutUserGroup.BeginInit();
    this.layoutUserGridControl.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.columnIsActive, "columnIsActive");
    this.columnIsActive.FieldName = "IsActive";
    this.columnIsActive.Name = "columnIsActive";
    this.layoutControl1.Controls.Add((Control) this.userPasswordVerification);
    this.layoutControl1.Controls.Add((Control) this.userPassword);
    this.layoutControl1.Controls.Add((Control) this.userLockedTimeStamp);
    this.layoutControl1.Controls.Add((Control) this.userLanguage);
    this.layoutControl1.Controls.Add((Control) this.userState);
    this.layoutControl1.Controls.Add((Control) this.userFacility);
    this.layoutControl1.Controls.Add((Control) this.userProfile);
    this.layoutControl1.Controls.Add((Control) this.userForename);
    this.layoutControl1.Controls.Add((Control) this.userSurname);
    this.layoutControl1.Controls.Add((Control) this.userLoginId);
    this.layoutControl1.Controls.Add((Control) this.userLoginName);
    this.layoutControl1.Controls.Add((Control) this.userGridControl);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutLoginId,
      (BaseLayoutItem) this.layoutUserFacility
    });
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.userPasswordVerification, "userPasswordVerification");
    this.userPasswordVerification.EnterMoveNextControl = true;
    this.userPasswordVerification.FormatString = (string) null;
    this.userPasswordVerification.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPasswordVerification.IsMandatory = true;
    this.userPasswordVerification.IsReadOnly = true;
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
    this.userPasswordVerification.Properties.ReadOnly = true;
    this.userPasswordVerification.StyleController = (IStyleController) this.layoutControl1;
    this.userPasswordVerification.Validator = (ICustomValidator) null;
    this.userPasswordVerification.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.userPassword, "userPassword");
    this.userPassword.EnterMoveNextControl = true;
    this.userPassword.FormatString = (string) null;
    this.userPassword.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userPassword.IsMandatory = true;
    this.userPassword.IsReadOnly = true;
    this.userPassword.IsUndoing = false;
    this.userPassword.Name = "userPassword";
    this.userPassword.PasswordConfirmationPartner = this.userPasswordVerification;
    this.userPassword.Properties.Appearance.BackColor = Color.LightYellow;
    this.userPassword.Properties.Appearance.BorderColor = Color.Yellow;
    this.userPassword.Properties.Appearance.Options.UseBackColor = true;
    this.userPassword.Properties.Appearance.Options.UseBorderColor = true;
    this.userPassword.Properties.BorderStyle = BorderStyles.Simple;
    this.userPassword.Properties.NullValuePrompt = componentResourceManager.GetString("userPassword.Properties.NullValuePrompt");
    this.userPassword.Properties.PasswordChar = '*';
    this.userPassword.Properties.ReadOnly = true;
    this.userPassword.StyleController = (IStyleController) this.layoutControl1;
    this.userPassword.Validator = (ICustomValidator) null;
    this.userPassword.Value = (object) "**********";
    componentResourceManager.ApplyResources((object) this.userLockedTimeStamp, "userLockedTimeStamp");
    this.userLockedTimeStamp.EnterMoveNextControl = true;
    this.userLockedTimeStamp.FormatString = (string) null;
    this.userLockedTimeStamp.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLockedTimeStamp.IsReadOnly = true;
    this.userLockedTimeStamp.IsUndoing = false;
    this.userLockedTimeStamp.Name = "userLockedTimeStamp";
    this.userLockedTimeStamp.Properties.Appearance.BorderColor = Color.LightGray;
    this.userLockedTimeStamp.Properties.Appearance.Options.UseBorderColor = true;
    this.userLockedTimeStamp.Properties.BorderStyle = BorderStyles.Simple;
    this.userLockedTimeStamp.Properties.ReadOnly = true;
    this.userLockedTimeStamp.ShowModified = false;
    this.userLockedTimeStamp.StyleController = (IStyleController) this.layoutControl1;
    this.userLockedTimeStamp.Validator = (ICustomValidator) null;
    this.userLockedTimeStamp.Value = (object) "";
    this.userLanguage.EnterMoveNextControl = true;
    this.userLanguage.FormatString = (string) null;
    this.userLanguage.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLanguage.IsReadOnly = true;
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
    this.userLanguage.Properties.ReadOnly = true;
    this.userLanguage.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userLanguage.ShowEmptyElement = true;
    this.userLanguage.StyleController = (IStyleController) this.layoutControl1;
    this.userLanguage.Validator = (ICustomValidator) null;
    this.userLanguage.Value = (object) null;
    this.userState.EnterMoveNextControl = true;
    this.userState.FormatString = (string) null;
    this.userState.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userState.IsReadOnly = true;
    this.userState.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.userState, "userState");
    this.userState.Name = "userState";
    this.userState.Properties.Appearance.BorderColor = Color.LightGray;
    this.userState.Properties.Appearance.Options.UseBorderColor = true;
    this.userState.Properties.BorderStyle = BorderStyles.Simple;
    this.userState.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("userState.Properties.Buttons"))
    });
    this.userState.Properties.ReadOnly = true;
    this.userState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userState.ShowEmptyElement = false;
    this.userState.StyleController = (IStyleController) this.layoutControl1;
    this.userState.Validator = (ICustomValidator) null;
    this.userState.Value = (object) null;
    this.userFacility.EnterMoveNextControl = true;
    this.userFacility.FormatString = (string) null;
    this.userFacility.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userFacility.IsActive = false;
    this.userFacility.IsReadOnly = true;
    this.userFacility.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.userFacility, "userFacility");
    this.userFacility.Name = "userFacility";
    this.userFacility.Properties.Appearance.BorderColor = Color.LightGray;
    this.userFacility.Properties.Appearance.Options.UseBorderColor = true;
    this.userFacility.Properties.BorderStyle = BorderStyles.Simple;
    this.userFacility.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("userFacility.Properties.Buttons"))
    });
    this.userFacility.Properties.ReadOnly = true;
    this.userFacility.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userFacility.ShowEmptyElement = true;
    this.userFacility.StyleController = (IStyleController) this.layoutControl1;
    this.userFacility.Validator = (ICustomValidator) null;
    this.userFacility.Value = (object) null;
    this.userProfile.EnterMoveNextControl = true;
    this.userProfile.FormatString = (string) null;
    this.userProfile.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userProfile.IsMandatory = true;
    this.userProfile.IsReadOnly = true;
    this.userProfile.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.userProfile, "userProfile");
    this.userProfile.Name = "userProfile";
    this.userProfile.Properties.Appearance.BackColor = Color.LightYellow;
    this.userProfile.Properties.Appearance.BorderColor = Color.LightGray;
    this.userProfile.Properties.Appearance.Options.UseBackColor = true;
    this.userProfile.Properties.Appearance.Options.UseBorderColor = true;
    this.userProfile.Properties.BorderStyle = BorderStyles.Simple;
    this.userProfile.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("userProfile.Properties.Buttons"))
    });
    this.userProfile.Properties.ReadOnly = true;
    this.userProfile.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.userProfile.ShowEmptyElement = true;
    this.userProfile.StyleController = (IStyleController) this.layoutControl1;
    this.userProfile.Validator = (ICustomValidator) null;
    this.userProfile.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.userForename, "userForename");
    this.userForename.EnterMoveNextControl = true;
    this.userForename.FormatString = (string) null;
    this.userForename.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userForename.IsReadOnly = true;
    this.userForename.IsUndoing = false;
    this.userForename.Name = "userForename";
    this.userForename.Properties.Appearance.BorderColor = Color.Yellow;
    this.userForename.Properties.Appearance.Options.UseBorderColor = true;
    this.userForename.Properties.BorderStyle = BorderStyles.Simple;
    this.userForename.Properties.ReadOnly = true;
    this.userForename.StyleController = (IStyleController) this.layoutControl1;
    this.userForename.Validator = (ICustomValidator) null;
    this.userForename.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.userSurname, "userSurname");
    this.userSurname.EnterMoveNextControl = true;
    this.userSurname.FormatString = (string) null;
    this.userSurname.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userSurname.IsReadOnly = true;
    this.userSurname.IsUndoing = false;
    this.userSurname.Name = "userSurname";
    this.userSurname.Properties.Appearance.BorderColor = Color.Yellow;
    this.userSurname.Properties.Appearance.Options.UseBorderColor = true;
    this.userSurname.Properties.BorderStyle = BorderStyles.Simple;
    this.userSurname.Properties.ReadOnly = true;
    this.userSurname.StyleController = (IStyleController) this.layoutControl1;
    this.userSurname.Validator = (ICustomValidator) null;
    this.userSurname.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.userLoginId, "userLoginId");
    this.userLoginId.EnterMoveNextControl = true;
    this.userLoginId.FormatString = (string) null;
    this.userLoginId.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLoginId.IsReadOnly = true;
    this.userLoginId.IsUndoing = false;
    this.userLoginId.Name = "userLoginId";
    this.userLoginId.Properties.Appearance.BorderColor = Color.LightGray;
    this.userLoginId.Properties.Appearance.Options.UseBorderColor = true;
    this.userLoginId.Properties.BorderStyle = BorderStyles.Simple;
    this.userLoginId.Properties.ReadOnly = true;
    this.userLoginId.ShowModified = false;
    this.userLoginId.StyleController = (IStyleController) this.layoutControl1;
    this.userLoginId.Validator = (ICustomValidator) null;
    this.userLoginId.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.userLoginName, "userLoginName");
    this.userLoginName.EnterMoveNextControl = true;
    this.userLoginName.FormatString = (string) null;
    this.userLoginName.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.userLoginName.IsMandatory = true;
    this.userLoginName.IsReadOnly = true;
    this.userLoginName.IsUndoing = false;
    this.userLoginName.Name = "userLoginName";
    this.userLoginName.Properties.Appearance.BackColor = Color.LightYellow;
    this.userLoginName.Properties.Appearance.BorderColor = Color.Yellow;
    this.userLoginName.Properties.Appearance.Options.UseBackColor = true;
    this.userLoginName.Properties.Appearance.Options.UseBorderColor = true;
    this.userLoginName.Properties.BorderStyle = BorderStyles.Simple;
    this.userLoginName.Properties.ReadOnly = true;
    this.userLoginName.StyleController = (IStyleController) this.layoutControl1;
    this.userLoginName.Validator = (ICustomValidator) null;
    this.userLoginName.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.userGridControl, "userGridControl");
    this.userGridControl.MainView = (BaseView) this.userGridView;
    this.userGridControl.Name = "userGridControl";
    this.userGridControl.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.userGridView
    });
    this.userGridView.Columns.AddRange(new GridColumn[9]
    {
      this.columnUserLoginName,
      this.columnUserSurname,
      this.columnForename,
      this.columnFullName,
      this.columnLoginId,
      this.columnLanguage,
      this.columnProfile,
      this.columnIsActive,
      this.columnLockTimeStamp
    });
    styleFormatCondition.Appearance.ForeColor = Color.Red;
    styleFormatCondition.Appearance.Options.HighPriority = true;
    styleFormatCondition.Appearance.Options.UseForeColor = true;
    styleFormatCondition.ApplyToRow = true;
    styleFormatCondition.Column = this.columnIsActive;
    styleFormatCondition.Condition = FormatConditionEnum.Equal;
    styleFormatCondition.Value1 = (object) false;
    this.userGridView.FormatConditions.AddRange(new StyleFormatCondition[1]
    {
      styleFormatCondition
    });
    this.userGridView.GridControl = this.userGridControl;
    this.userGridView.Name = "userGridView";
    this.userGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.userGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.userGridView.OptionsBehavior.Editable = false;
    this.userGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.userGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.userGridView.OptionsView.EnableAppearanceOddRow = true;
    this.userGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.columnUserLoginName, "columnUserLoginName");
    this.columnUserLoginName.FieldName = "LoginName";
    this.columnUserLoginName.Name = "columnUserLoginName";
    componentResourceManager.ApplyResources((object) this.columnUserSurname, "columnUserSurname");
    this.columnUserSurname.FieldName = "Surname";
    this.columnUserSurname.Name = "columnUserSurname";
    componentResourceManager.ApplyResources((object) this.columnForename, "columnForename");
    this.columnForename.FieldName = "Forename";
    this.columnForename.Name = "columnForename";
    componentResourceManager.ApplyResources((object) this.columnFullName, "columnFullName");
    this.columnFullName.FieldName = "FullName";
    this.columnFullName.Name = "columnFullName";
    componentResourceManager.ApplyResources((object) this.columnLoginId, "columnLoginId");
    this.columnLoginId.FieldName = "LoginId";
    this.columnLoginId.Name = "columnLoginId";
    componentResourceManager.ApplyResources((object) this.columnLanguage, "columnLanguage");
    this.columnLanguage.FieldName = "LanguageName";
    this.columnLanguage.Name = "columnLanguage";
    componentResourceManager.ApplyResources((object) this.columnProfile, "columnProfile");
    this.columnProfile.FieldName = "Profile.Name";
    this.columnProfile.Name = "columnProfile";
    componentResourceManager.ApplyResources((object) this.columnLockTimeStamp, "columnLockTimeStamp");
    this.columnLockTimeStamp.DisplayFormat.FormatString = "d";
    this.columnLockTimeStamp.DisplayFormat.FormatType = FormatType.DateTime;
    this.columnLockTimeStamp.FieldName = "LockTimestamp";
    this.columnLockTimeStamp.Name = "columnLockTimeStamp";
    this.layoutLoginId.Control = (Control) this.userLoginId;
    componentResourceManager.ApplyResources((object) this.layoutLoginId, "layoutLoginId");
    this.layoutLoginId.Location = new Point(0, 31 /*0x1F*/);
    this.layoutLoginId.Name = "layoutLoginId";
    this.layoutLoginId.Size = new Size(275, 31 /*0x1F*/);
    this.layoutLoginId.TextSize = new Size(55, 13);
    this.layoutLoginId.TextToControlDistance = 5;
    this.layoutUserFacility.Control = (Control) this.userFacility;
    componentResourceManager.ApplyResources((object) this.layoutUserFacility, "layoutUserFacility");
    this.layoutUserFacility.Location = new Point(0, 96 /*0x60*/);
    this.layoutUserFacility.Name = "layoutUserFacility";
    this.layoutUserFacility.Size = new Size(396, 24);
    this.layoutUserFacility.TextSize = new Size(55, 13);
    this.layoutUserFacility.TextToControlDistance = 5;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutUserInformation,
      (BaseLayoutItem) this.layoutPrivateUserInformation,
      (BaseLayoutItem) this.layoutSystemInformation,
      (BaseLayoutItem) this.layoutUserGroup
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(940, 710);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(500, 348);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(420, 342);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutUserInformation, "layoutUserInformation");
    this.layoutUserInformation.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutLoginName,
      (BaseLayoutItem) this.layoutUserSurname,
      (BaseLayoutItem) this.layoutUserProfile,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutUserForename
    });
    this.layoutUserInformation.Location = new Point(500, 0);
    this.layoutUserInformation.Name = "layoutUserInformation";
    this.layoutUserInformation.Size = new Size(420, 140);
    this.layoutLoginName.AllowHide = false;
    this.layoutLoginName.Control = (Control) this.userLoginName;
    componentResourceManager.ApplyResources((object) this.layoutLoginName, "layoutLoginName");
    this.layoutLoginName.Location = new Point(0, 0);
    this.layoutLoginName.Name = "layoutLoginName";
    this.layoutLoginName.Size = new Size(258, 24);
    this.layoutLoginName.TextSize = new Size(55, 13);
    this.layoutUserSurname.Control = (Control) this.userSurname;
    componentResourceManager.ApplyResources((object) this.layoutUserSurname, "layoutUserSurname");
    this.layoutUserSurname.Location = new Point(0, 48 /*0x30*/);
    this.layoutUserSurname.Name = "layoutUserSurname";
    this.layoutUserSurname.Size = new Size(258, 24);
    this.layoutUserSurname.TextSize = new Size(55, 13);
    this.layoutUserProfile.Control = (Control) this.userProfile;
    componentResourceManager.ApplyResources((object) this.layoutUserProfile, "layoutUserProfile");
    this.layoutUserProfile.Location = new Point(0, 72);
    this.layoutUserProfile.Name = "layoutUserProfile";
    this.layoutUserProfile.Size = new Size(396, 24);
    this.layoutUserProfile.TextSize = new Size(55, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(258, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(138, 72);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutUserForename.Control = (Control) this.userForename;
    componentResourceManager.ApplyResources((object) this.layoutUserForename, "layoutUserForename");
    this.layoutUserForename.Location = new Point(0, 24);
    this.layoutUserForename.Name = "layoutUserForename";
    this.layoutUserForename.Size = new Size(258, 24);
    this.layoutUserForename.TextSize = new Size(55, 13);
    componentResourceManager.ApplyResources((object) this.layoutPrivateUserInformation, "layoutPrivateUserInformation");
    this.layoutPrivateUserInformation.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutUserLanguage,
      (BaseLayoutItem) this.layoutUserPassword,
      (BaseLayoutItem) this.layoutUserPasswordVerification
    });
    this.layoutPrivateUserInformation.Location = new Point(500, 140);
    this.layoutPrivateUserInformation.Name = "layoutPrivateUserInformation";
    this.layoutPrivateUserInformation.Size = new Size(420, 116);
    this.layoutUserLanguage.Control = (Control) this.userLanguage;
    componentResourceManager.ApplyResources((object) this.layoutUserLanguage, "layoutUserLanguage");
    this.layoutUserLanguage.Location = new Point(0, 48 /*0x30*/);
    this.layoutUserLanguage.Name = "layoutUserLanguage";
    this.layoutUserLanguage.Size = new Size(396, 24);
    this.layoutUserLanguage.TextSize = new Size(55, 13);
    this.layoutUserPassword.Control = (Control) this.userPassword;
    componentResourceManager.ApplyResources((object) this.layoutUserPassword, "layoutUserPassword");
    this.layoutUserPassword.Location = new Point(0, 0);
    this.layoutUserPassword.Name = "layoutUserPassword";
    this.layoutUserPassword.Size = new Size(396, 24);
    this.layoutUserPassword.TextSize = new Size(55, 13);
    this.layoutUserPasswordVerification.Control = (Control) this.userPasswordVerification;
    componentResourceManager.ApplyResources((object) this.layoutUserPasswordVerification, "layoutUserPasswordVerification");
    this.layoutUserPasswordVerification.Location = new Point(0, 24);
    this.layoutUserPasswordVerification.Name = "layoutUserPasswordVerification";
    this.layoutUserPasswordVerification.Size = new Size(396, 24);
    this.layoutUserPasswordVerification.TextSize = new Size(55, 13);
    componentResourceManager.ApplyResources((object) this.layoutSystemInformation, "layoutSystemInformation");
    this.layoutSystemInformation.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutUserState,
      (BaseLayoutItem) this.layoutUserLockedTimeStamp
    });
    this.layoutSystemInformation.Location = new Point(500, 256 /*0x0100*/);
    this.layoutSystemInformation.Name = "layoutSystemInformation";
    this.layoutSystemInformation.Size = new Size(420, 92);
    this.layoutUserState.Control = (Control) this.userState;
    componentResourceManager.ApplyResources((object) this.layoutUserState, "layoutUserState");
    this.layoutUserState.Location = new Point(0, 0);
    this.layoutUserState.Name = "layoutUserState";
    this.layoutUserState.Size = new Size(396, 24);
    this.layoutUserState.TextSize = new Size(55, 13);
    this.layoutUserLockedTimeStamp.Control = (Control) this.userLockedTimeStamp;
    componentResourceManager.ApplyResources((object) this.layoutUserLockedTimeStamp, "layoutUserLockedTimeStamp");
    this.layoutUserLockedTimeStamp.Location = new Point(0, 24);
    this.layoutUserLockedTimeStamp.Name = "layoutUserLockedTimeStamp";
    this.layoutUserLockedTimeStamp.Size = new Size(396, 24);
    this.layoutUserLockedTimeStamp.TextSize = new Size(55, 13);
    componentResourceManager.ApplyResources((object) this.layoutUserGroup, "layoutUserGroup");
    this.layoutUserGroup.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutUserGridControl
    });
    this.layoutUserGroup.Location = new Point(0, 0);
    this.layoutUserGroup.Name = "layoutUserGroup";
    this.layoutUserGroup.Size = new Size(500, 690);
    this.layoutUserGridControl.Control = (Control) this.userGridControl;
    componentResourceManager.ApplyResources((object) this.layoutUserGridControl, "layoutUserGridControl");
    this.layoutUserGridControl.Location = new Point(0, 0);
    this.layoutUserGridControl.Name = "layoutUserGridControl";
    this.layoutUserGridControl.ShowInCustomizationForm = false;
    this.layoutUserGridControl.Size = new Size(476, 646);
    this.layoutUserGridControl.TextSize = new Size(0, 0);
    this.layoutUserGridControl.TextToControlDistance = 0;
    this.layoutUserGridControl.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (UserAccountMasterDetailBrowser);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.userPasswordVerification.Properties.EndInit();
    this.userPassword.Properties.EndInit();
    this.userLockedTimeStamp.Properties.EndInit();
    this.userLanguage.Properties.EndInit();
    this.userState.Properties.EndInit();
    this.userFacility.Properties.EndInit();
    this.userProfile.Properties.EndInit();
    this.userForename.Properties.EndInit();
    this.userSurname.Properties.EndInit();
    this.userLoginId.Properties.EndInit();
    this.userLoginName.Properties.EndInit();
    this.userGridControl.EndInit();
    this.userGridView.EndInit();
    this.layoutLoginId.EndInit();
    this.layoutUserFacility.EndInit();
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutUserInformation.EndInit();
    this.layoutLoginName.EndInit();
    this.layoutUserSurname.EndInit();
    this.layoutUserProfile.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutUserForename.EndInit();
    this.layoutPrivateUserInformation.EndInit();
    this.layoutUserLanguage.EndInit();
    this.layoutUserPassword.EndInit();
    this.layoutUserPasswordVerification.EndInit();
    this.layoutSystemInformation.EndInit();
    this.layoutUserState.EndInit();
    this.layoutUserLockedTimeStamp.EndInit();
    this.layoutUserGroup.EndInit();
    this.layoutUserGridControl.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateProfilesCallBack(IEnumerable<UserProfile> userProfiles);

  private delegate void UpdateUsersCallBack(ICollection<User> users);
}
