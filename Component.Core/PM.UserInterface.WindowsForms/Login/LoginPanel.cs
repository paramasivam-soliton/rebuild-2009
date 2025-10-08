// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Login.LoginPanel
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using PathMedical.Login;
using PathMedical.Plugin;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.WindowsForms.Panel;
using PathMedical.UserInterface.WindowsForms.Properties;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Login;

public class LoginPanel : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;
  private LayoutControl layoutLoginControl;
  private LabelControl loginMessageLabel;
  private TextEdit loginPassword;
  private SimpleButton loginButton;
  private TextEdit loginName;
  private LayoutControlGroup layoutLogin;
  private LayoutControlItem layoutLoginName;
  private LayoutControlItem layoutPassword;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlItem layoutLoginButton;
  private LayoutControlItem layoutLoginMessageLabel;
  private EmptySpaceItem emptySpaceItem2;
  private FastTableLayoutPanel fastTableLayoutPanel1;

  public LoginPanel()
  {
    this.InitializeComponent();
    SystemConfigurationManager.Instance.UserInterfaceManager.SetFormSubmissionControl((object) this.loginButton);
    if (!Application.ProductName.Equals("Mira"))
      return;
    this.layoutLogin.CaptionImage = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("LoggedInUser") as Bitmap);
    this.layoutLogin.Text = "Your Mira Credentials";
  }

  private void loginButtonClick(object sender, EventArgs e)
  {
    if (!this.IsFormFilled())
      return;
    LoginResult loginResult = LoginManager.Instance.TryLoginUser(this.loginName.Text, this.loginPassword.Text);
    this.loginMessageLabel.Appearance.Image = (Image) null;
    switch (loginResult.Kind)
    {
      case LoginResultKind.Successful:
        this.loginMessageLabel.Text = string.Empty;
        UserInterfaceManager.Instance.ContinueAfterLogin();
        this.loginName.Text = string.Empty;
        break;
      case LoginResultKind.WrongPassword:
      case LoginResultKind.UnknownUser:
        this.loginMessageLabel.Appearance.Image = DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider.GetErrorIconInternal(ErrorType.Critical);
        this.loginMessageLabel.Text = PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("LoginFailed");
        break;
      case LoginResultKind.UserLocked:
        this.loginMessageLabel.Appearance.Image = DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider.GetErrorIconInternal(ErrorType.Critical);
        if (SystemConfigurationManager.Instance.Plugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))))
        {
          this.loginMessageLabel.Text = PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("LoginFailedAccountLockedForever");
          break;
        }
        this.loginMessageLabel.Text = string.Format(PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("LoginFailedAccountLocked"), (object) SystemConfigurationManager.Instance.UserAccountLockingTime);
        break;
    }
    this.loginPassword.Text = string.Empty;
  }

  private bool IsFormFilled()
  {
    bool flag = true;
    if (string.IsNullOrEmpty(this.loginName.Text))
    {
      flag = false;
      this.loginName.BackColor = Color.LightPink;
    }
    else
      this.loginName.BackColor = Color.Empty;
    if (string.IsNullOrEmpty(this.loginPassword.Text))
    {
      flag = false;
      this.loginPassword.BackColor = Color.LightPink;
    }
    else
      this.loginPassword.BackColor = Color.Empty;
    return flag;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LoginPanel));
    this.loginName = new TextEdit();
    this.layoutLoginControl = new LayoutControl();
    this.loginMessageLabel = new LabelControl();
    this.loginPassword = new TextEdit();
    this.loginButton = new SimpleButton();
    this.layoutLogin = new LayoutControlGroup();
    this.layoutLoginName = new LayoutControlItem();
    this.layoutPassword = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutLoginButton = new LayoutControlItem();
    this.layoutLoginMessageLabel = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.fastTableLayoutPanel1 = new FastTableLayoutPanel(this.components);
    this.loginName.Properties.BeginInit();
    this.layoutLoginControl.BeginInit();
    this.layoutLoginControl.SuspendLayout();
    this.loginPassword.Properties.BeginInit();
    this.layoutLogin.BeginInit();
    this.layoutLoginName.BeginInit();
    this.layoutPassword.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutLoginButton.BeginInit();
    this.layoutLoginMessageLabel.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.fastTableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.loginName, "loginName");
    this.loginName.Name = "loginName";
    this.loginName.StyleController = (IStyleController) this.layoutLoginControl;
    this.layoutLoginControl.AllowCustomizationMenu = false;
    this.layoutLoginControl.Controls.Add((Control) this.loginMessageLabel);
    this.layoutLoginControl.Controls.Add((Control) this.loginPassword);
    this.layoutLoginControl.Controls.Add((Control) this.loginButton);
    this.layoutLoginControl.Controls.Add((Control) this.loginName);
    componentResourceManager.ApplyResources((object) this.layoutLoginControl, "layoutLoginControl");
    this.layoutLoginControl.Name = "layoutLoginControl";
    this.layoutLoginControl.Root = this.layoutLogin;
    this.loginMessageLabel.ImageAlignToText = ImageAlignToText.LeftCenter;
    componentResourceManager.ApplyResources((object) this.loginMessageLabel, "loginMessageLabel");
    this.loginMessageLabel.Name = "loginMessageLabel";
    this.loginMessageLabel.StyleController = (IStyleController) this.layoutLoginControl;
    componentResourceManager.ApplyResources((object) this.loginPassword, "loginPassword");
    this.loginPassword.Name = "loginPassword";
    this.loginPassword.Properties.PasswordChar = '*';
    this.loginPassword.StyleController = (IStyleController) this.layoutLoginControl;
    componentResourceManager.ApplyResources((object) this.loginButton, "loginButton");
    this.loginButton.Name = "loginButton";
    this.loginButton.StyleController = (IStyleController) this.layoutLoginControl;
    this.loginButton.Click += new EventHandler(this.loginButtonClick);
    this.layoutLogin.AppearanceGroup.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.layoutLogin.AppearanceGroup.Options.UseFont = true;
    this.layoutLogin.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutLogin.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutLogin.CaptionImage = (Image) Resources.GN_LoggedInUser;
    componentResourceManager.ApplyResources((object) this.layoutLogin, "layoutLogin");
    this.layoutLogin.Items.AddRange(new BaseLayoutItem[6]
    {
      (BaseLayoutItem) this.layoutLoginName,
      (BaseLayoutItem) this.layoutPassword,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutLoginButton,
      (BaseLayoutItem) this.layoutLoginMessageLabel,
      (BaseLayoutItem) this.emptySpaceItem2
    });
    this.layoutLogin.Location = new Point(0, 0);
    this.layoutLogin.Name = "Root";
    this.layoutLogin.Size = new Size(336, 171);
    this.layoutLoginName.Control = (Control) this.loginName;
    componentResourceManager.ApplyResources((object) this.layoutLoginName, "layoutLoginName");
    this.layoutLoginName.Location = new Point(0, 0);
    this.layoutLoginName.Name = "layoutLoginName";
    this.layoutLoginName.Size = new Size(316, 24);
    this.layoutLoginName.TextSize = new Size(46, 13);
    this.layoutPassword.Control = (Control) this.loginPassword;
    componentResourceManager.ApplyResources((object) this.layoutPassword, "layoutPassword");
    this.layoutPassword.Location = new Point(0, 24);
    this.layoutPassword.Name = "layoutPassword";
    this.layoutPassword.Size = new Size(316, 24);
    this.layoutPassword.TextSize = new Size(46, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 82);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(316, 18);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutLoginButton.Control = (Control) this.loginButton;
    componentResourceManager.ApplyResources((object) this.layoutLoginButton, "layoutLoginButton");
    this.layoutLoginButton.Location = new Point(0, 100);
    this.layoutLoginButton.Name = "layoutLoginButton";
    this.layoutLoginButton.Size = new Size(316, 26);
    this.layoutLoginButton.TextSize = new Size(0, 0);
    this.layoutLoginButton.TextToControlDistance = 0;
    this.layoutLoginButton.TextVisible = false;
    this.layoutLoginMessageLabel.Control = (Control) this.loginMessageLabel;
    componentResourceManager.ApplyResources((object) this.layoutLoginMessageLabel, "layoutLoginMessageLabel");
    this.layoutLoginMessageLabel.Location = new Point(0, 65);
    this.layoutLoginMessageLabel.Name = "layoutLoginMessageLabel";
    this.layoutLoginMessageLabel.Size = new Size(316, 17);
    this.layoutLoginMessageLabel.TextSize = new Size(0, 0);
    this.layoutLoginMessageLabel.TextToControlDistance = 0;
    this.layoutLoginMessageLabel.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 48 /*0x30*/);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(316, 17);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.fastTableLayoutPanel1, "fastTableLayoutPanel1");
    this.fastTableLayoutPanel1.Controls.Add((Control) this.layoutLoginControl, 1, 1);
    this.fastTableLayoutPanel1.Name = "fastTableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.fastTableLayoutPanel1);
    this.Name = nameof (LoginPanel);
    this.loginName.Properties.EndInit();
    this.layoutLoginControl.EndInit();
    this.layoutLoginControl.ResumeLayout(false);
    this.loginPassword.Properties.EndInit();
    this.layoutLogin.EndInit();
    this.layoutLoginName.EndInit();
    this.layoutPassword.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutLoginButton.EndInit();
    this.layoutLoginMessageLabel.EndInit();
    this.emptySpaceItem2.EndInit();
    this.fastTableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
