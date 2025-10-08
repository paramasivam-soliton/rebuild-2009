// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Recovery.RecoveryConsoleView
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PathMedical.DatabaseManagement;
using PathMedical.Login;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.WindowsForms.Properties;
using PathMedical.UserProfileManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Recovery;

public class RecoveryConsoleView : XtraForm
{
  private const string MasterPassword = "Thilde";
  private IContainer components;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private SimpleButton CancelPasswordChange;
  private TextEdit newAdminPassword;
  private TextEdit masterPassword;
  private SimpleButton buttonChangePassword;
  private LayoutControlItem layoutMasterPassword;
  private LayoutControlItem layoutNewPassword;
  private LayoutControlItem layoutControlItem1;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlItem layoutControlItem2;

  public RecoveryConsoleView()
  {
    UserLookAndFeel.Default.SkinName = "Seven";
    this.InitializeComponent();
  }

  private void OnPasswordChangeClick(object sender, EventArgs e)
  {
    string caption = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RecoveryConsoleView_MessageBoxTitle");
    if ("Thilde".Equals(this.masterPassword.Text))
    {
      if (!string.IsNullOrEmpty(this.newAdminPassword.Text))
      {
        RecoveryConsoleView.RestoreRightsAndSetNewPassword(this.newAdminPassword.Text);
        int num = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RecoveryConsoleView_OnPasswordChangeClick_The_password_has_been_changed_successfully_"), caption);
      }
      else
      {
        int num1 = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RecoveryConsoleView_OnPasswordChangeClick_You_can_t_choose_an_empty_password_"), caption);
      }
    }
    else
    {
      int num2 = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RecoveryConsoleView_OnPasswordChangeClick_You_ve_entered_the_wrong_master_password_"), caption);
    }
  }

  private static void RestoreRightsAndSetNewPassword(string password)
  {
    using (DBScope dbScope = new DBScope())
    {
      User user = UserManager.Instance.Users.Single<User>((Func<User, bool>) (u => u.Id == UserManager.Instance.AdministratorAccountId));
      UserProfile userProfile = UserProfileManager.Instance.Profiles.Single<UserProfile>((Func<UserProfile, bool>) (p => p.Id == UserProfileManager.Instance.AdministratorProfileId));
      user.Profile = userProfile;
      foreach (AccessPermission accessPermission in user.Profile.ProfileAccessPermissions)
        accessPermission.AccessPermissionFlag = true;
      user.Password = LoginManager.Instance.EncryptPassword(password, user.PasswordSalt);
      user.LockTimestamp = new DateTime?();
      user.IsActive = true;
      UserManager.Instance.Store(user);
      dbScope.Complete();
    }
  }

  private void OnCancelChangePasswordClick(object sender, EventArgs e) => this.Close();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RecoveryConsoleView));
    this.layoutControl1 = new LayoutControl();
    this.CancelPasswordChange = new SimpleButton();
    this.newAdminPassword = new TextEdit();
    this.masterPassword = new TextEdit();
    this.buttonChangePassword = new SimpleButton();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutMasterPassword = new LayoutControlItem();
    this.layoutNewPassword = new LayoutControlItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.newAdminPassword.Properties.BeginInit();
    this.masterPassword.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutMasterPassword.BeginInit();
    this.layoutNewPassword.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.SuspendLayout();
    this.layoutControl1.Controls.Add((Control) this.CancelPasswordChange);
    this.layoutControl1.Controls.Add((Control) this.newAdminPassword);
    this.layoutControl1.Controls.Add((Control) this.masterPassword);
    this.layoutControl1.Controls.Add((Control) this.buttonChangePassword);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.CancelPasswordChange, "CancelPasswordChange");
    this.CancelPasswordChange.Name = "CancelPasswordChange";
    this.CancelPasswordChange.StyleController = (IStyleController) this.layoutControl1;
    this.CancelPasswordChange.Click += new EventHandler(this.OnCancelChangePasswordClick);
    componentResourceManager.ApplyResources((object) this.newAdminPassword, "newAdminPassword");
    this.newAdminPassword.Name = "newAdminPassword";
    this.newAdminPassword.Properties.PasswordChar = '*';
    this.newAdminPassword.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.masterPassword, "masterPassword");
    this.masterPassword.Name = "masterPassword";
    this.masterPassword.Properties.PasswordChar = '*';
    this.masterPassword.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.buttonChangePassword, "buttonChangePassword");
    this.buttonChangePassword.Name = "buttonChangePassword";
    this.buttonChangePassword.StyleController = (IStyleController) this.layoutControl1;
    this.buttonChangePassword.Click += new EventHandler(this.OnPasswordChangeClick);
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutMasterPassword,
      (BaseLayoutItem) this.layoutNewPassword,
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.ShowInCustomizationForm = false;
    this.layoutControlGroup1.Size = new Size(334, 192 /*0xC0*/);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    this.layoutMasterPassword.Control = (Control) this.masterPassword;
    componentResourceManager.ApplyResources((object) this.layoutMasterPassword, "layoutMasterPassword");
    this.layoutMasterPassword.Location = new Point(0, 0);
    this.layoutMasterPassword.Name = "layoutMasterPassword";
    this.layoutMasterPassword.Size = new Size(314, 24);
    this.layoutMasterPassword.TextSize = new Size(82, 13);
    this.layoutNewPassword.Control = (Control) this.newAdminPassword;
    componentResourceManager.ApplyResources((object) this.layoutNewPassword, "layoutNewPassword");
    this.layoutNewPassword.Location = new Point(0, 24);
    this.layoutNewPassword.Name = "layoutNewPassword";
    this.layoutNewPassword.Size = new Size(314, 24);
    this.layoutNewPassword.TextSize = new Size(82, 13);
    this.layoutControlItem1.Control = (Control) this.buttonChangePassword;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 146);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(157, 26);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 48 /*0x30*/);
    this.emptySpaceItem1.MinSize = new Size(104, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(314, 98);
    this.emptySpaceItem1.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutControlItem2.Control = (Control) this.CancelPasswordChange;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(157, 146);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(157, 26);
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (RecoveryConsoleView);
    this.ShowIcon = false;
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.newAdminPassword.Properties.EndInit();
    this.masterPassword.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutMasterPassword.EndInit();
    this.layoutNewPassword.EndInit();
    this.layoutControlItem1.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.ResumeLayout(false);
  }
}
