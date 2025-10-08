// Decompiled with JetBrains decompiler
// Type: GN.Otometrics.NHS.ApplicationStarter
// Assembly: AccuScreenServicetool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E723BD4-2FBA-4A66-910E-0878AA53AFFA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\AccuScreenServicetool.exe

using GN.Otometrics.NHS.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.WindowsForms;
using System;
using System.Configuration;
using System.Globalization;
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
    try
    {
      ApplicationStarter.SetupConfiguration();
      ApplicationHelper.Instance.ConfigureApplication();
      ApplicationHelper.Instance.LoadAllPlugins(true);
      Application.Run((Form) new GN.Otometrics.NHS.NHS());
    }
    catch (ApplicationException ex)
    {
      string caption = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ApplicationStarter_Main_AccuScreen");
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
    }
    return 0;
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
  }
}
