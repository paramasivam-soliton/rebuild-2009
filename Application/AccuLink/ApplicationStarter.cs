// Decompiled with JetBrains decompiler
// Type: GN.Otometrics.NHS.ApplicationStarter
// Assembly: AccuLink, Version=0.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0C33E5-E7AA-4F2A-B1D7-6F85B2D56646
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\AccuLink.exe

using GN.Otometrics.NHS.Properties;
using PathMedical.PatientManagement;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.WindowsForms;
using PathMedical.UserInterface.WindowsForms.Recovery;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace GN.Otometrics.NHS;

public class ApplicationStarter
{
  [STAThread]
  public static int Main(string[] args)
  {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    if (ApplicationHelper.Instance.IsApplicationAlreadyActive())
    {
      int num = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_Main_Application_already_active_"));
      return 1;
    }
    bool flag = false;
    bool startAsServiceTool = false;
    foreach (string strA in args)
    {
      if (string.Compare(strA, "/R") == 0)
      {
        flag = true;
        break;
      }
      if (string.Compare(strA, "/S") == 0)
      {
        startAsServiceTool = true;
        break;
      }
    }
    try
    {
      ApplicationStarter.SetupConfiguration();
      ApplicationHelper.Instance.ConfigureApplication();
      ApplicationHelper.Instance.LoadAllPlugins(startAsServiceTool);
      if (!flag)
      {
        ApplicationStarter.InformAboutUnsendData();
        Application.Run((Form) new GN.Otometrics.NHS.NHS());
      }
      else
        Application.Run((Form) new RecoveryConsoleView());
    }
    catch (ApplicationException ex)
    {
      string caption = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_Main_AccuLink");
      string format = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_Main_ApplicationException");
      if (format != null)
      {
        int num = (int) MessageBox.Show(string.Format(format, (object) ex.Message), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return 1;
    }
    finally
    {
      ApplicationHelper.Instance.DeactivateApplication();
      ApplicationStarter.InformAboutUnsendData();
      ApplicationStarter.PerformDatabaseBackup();
    }
    return 0;
  }

  private static void InformAboutUnsendData()
  {
    if (!SystemConfigurationManager.Instance.Plugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))) || PatientManager.Instance.AllPatients.Where<Patient>((Func<Patient, bool>) (p => p != null && p.AudiologyTests != null)).Count<Patient>() <= 0)
      return;
    int num = (int) MessageBox.Show(Resources.ApplicationStarter_InformAboutUnsendData_Text, Resources.ApplicationStarter_InformAboutUnsendData_WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private static void PerformDatabaseBackup()
  {
    if (!SystemConfigurationManager.Instance.Plugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))))
      return;
    string caption = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_MessageBoxTitle");
    if (string.IsNullOrEmpty(SystemConfigurationManager.Instance.SystemBackupFolder))
    {
      int num1 = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_Configure_BackupFolder_Message"), caption);
    }
    else
    {
      if (MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_Shall_the_database_now_be_backed_up_"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      try
      {
        string str = SystemConfigurationManager.Instance.Backup();
        if (string.IsNullOrEmpty(str))
        {
          int num2 = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_AccuLink_has_been_backed_up_successfully_"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          int num3 = (int) MessageBox.Show($"{ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_AccuLink_has_not_been_backed_up__")}{Environment.NewLine}{str}", caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
      catch (Exception ex)
      {
        int num4 = (int) MessageBox.Show(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_PerformDatabaseBackup_AccuLink_has_not_been_backed_up__") + ex.Message, caption);
      }
    }
  }

  private static void SetupConfiguration()
  {
    ApplicationHelper.Instance.ProductName = Application.ProductName;
    ApplicationHelper.Instance.ProductVersion = Application.ProductVersion;
    ApplicationHelper.Instance.ApplicationDataDirectory = Application.CommonAppDataPath;
    ApplicationHelper.Instance.ApplicationExecutionDirectoy = Application.ExecutablePath;
    ApplicationHelper.Instance.NativeExceptionCulture = new CultureInfo(ConfigurationManager.AppSettings["NativeExceptionLanguage"]);
    ApplicationHelper.Instance.DatabaseProviderId = new Guid(ConfigurationManager.AppSettings["DatabaseProviderFingerprint"]);
    ApplicationHelper.Instance.DatabaseConnectionString = ConfigurationManager.AppSettings["DataStorageLocation"];
    ApplicationHelper.Instance.BaseLanguage = ConfigurationManager.AppSettings["MultiLanguageBase"];
    ApplicationHelper.Instance.OtherLanguages = ConfigurationManager.AppSettings["MultiLanguageSupport"];
    ApplicationHelper.Instance.LoginVerifierId = new Guid(ConfigurationManager.AppSettings["LoginVerifierFingerprint"]);
    ApplicationHelper.Instance.PermissionManagerId = new Guid(ConfigurationManager.AppSettings["PermissionManagerFingerprint"]);
    ApplicationHelper.Instance.HelpFile = "Help\\AccuLink.{0}.chm";
  }
}
