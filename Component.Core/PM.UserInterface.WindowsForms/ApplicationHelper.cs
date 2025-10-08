// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ApplicationHelper
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Culture;
using PathMedical.Exception;
using PathMedical.FileSystem;
using PathMedical.Logging;
using PathMedical.ResourceManager;
using PathMedical.ResourceManager.Database;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.WindowsForms.Culture;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms;

public sealed class ApplicationHelper
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (SystemConfigurationManager), "$Rev: 1225 $");
  private Mutex mutex;
  private DateTime configuruationStart = DateTime.Now;

  public static ApplicationHelper Instance => PathMedical.Singleton.Singleton<ApplicationHelper>.Instance;

  public string ProductName { get; set; }

  public string ProductVersion { get; set; }

  public string ApplicationDataDirectory { get; set; }

  public string ApplicationExecutionDirectoy { get; set; }

  public CultureInfo NativeExceptionCulture { get; set; }

  public string BaseLanguage { get; set; }

  public string OtherLanguages { get; set; }

  public Guid DatabaseProviderId { get; set; }

  public string DatabaseConnectionString { get; set; }

  public Guid LoginVerifierId { get; set; }

  public Guid PermissionManagerId { get; set; }

  public string HelpFile { get; set; }

  private ApplicationHelper()
  {
  }

  public bool IsApplicationAlreadyActive()
  {
    bool createdNew;
    this.mutex = new Mutex(false, Assembly.GetEntryAssembly().FullName, out createdNew);
    return !createdNew;
  }

  public void DeactivateApplication()
  {
    if (this.mutex == null)
      return;
    try
    {
      this.mutex.WaitOne();
      this.mutex.ReleaseMutex();
    }
    catch (ObjectDisposedException ex)
    {
      throw;
    }
    catch (AbandonedMutexException ex)
    {
      throw;
    }
    catch (InvalidOperationException ex)
    {
      throw;
    }
    catch (ApplicationException ex)
    {
      throw;
    }
  }

  public void ConfigureApplication()
  {
    this.configuruationStart = DateTime.Now;
    string path1_1 = this.ApplicationDataDirectory.Replace(this.ProductVersion, string.Empty);
    ApplicationHelper.Logger.Info("Configuring Application.");
    ApplicationHelper.Logger.Debug("Application start parameter: {0}", (object) Environment.CommandLine);
    SystemConfigurationManager.Instance.LogEnvironment();
    string path1_2 = this.ApplicationExecutionDirectoy.Substring(0, this.ApplicationExecutionDirectoy.LastIndexOf('\\'));
    SystemConfigurationManager.Instance.RegisterExecutingAssemblyPlugins();
    SystemConfigurationManager.Instance.RegisterPluginAssemblies(Path.Combine(path1_2, "Plugin"));
    SystemConfigurationManager.Instance.RegisterPluginAssemblies(Path.Combine(path1_2, "Base"));
    ExceptionFactory.Instance.CultureManager = (ICultureManager) new CultureManager();
    CultureInfo cultureToSet = (CultureInfo) null;
    try
    {
      cultureToSet = this.NativeExceptionCulture;
    }
    catch (ArgumentException ex)
    {
      ApplicationHelper.Logger.Error((System.Exception) ex, "Unable to set the configured culture for exception handling. Please check NativeExceptionLanguage parameter in configuration file.");
    }
    ExceptionFactory.Instance.CultureManager.SetCulture(cultureToSet);
    GlobalResourceEnquirer.Instance.RegisterResourceManager((System.Resources.ResourceManager) new DatabaseResourceManager(""));
    string connectionString = "Data Source=" + Path.Combine(path1_1, this.DatabaseConnectionString);
    if (this.DatabaseProviderId == new Guid("6F7B317F-0B9D-4aba-BDA6-139EFADE18AB"))
    {
      string path2 = this.DatabaseConnectionString.Substring(0, this.DatabaseConnectionString.IndexOf(';'));
      string str = Path.Combine(path1_1, path2);
      if (!FileSystemHelper.DoesFileExists(str))
        throw new ApplicationException("The database file is missing.\nPlease contact your adminstrator to restore the backup.");
      SystemConfigurationManager.Instance.AddBackupFile(str);
    }
    SystemConfigurationManager.Instance.ConfigureDatabasePlugin(this.DatabaseProviderId, connectionString);
    SystemConfigurationManager.Instance.SetLanguageSupport(this.BaseLanguage, this.OtherLanguages);
    SystemConfigurationManager.Instance.SetLanguage(SystemConfigurationManager.Instance.CurrentLanguage);
    SystemConfigurationManager.Instance.TemporaryInstrumentDataDirectory = Path.Combine(path1_1, "InstrumentData");
    SystemConfigurationManager.Instance.ApplicationDataDirectory = path1_1;
    SystemConfigurationManager.Instance.ApplicationLogFile = Path.Combine(path1_1, $"{this.ProductName}.txt");
    ApplicationHelper.Logger.LogTo(SystemConfigurationManager.Instance.ApplicationLogFile);
    SystemConfigurationManager.Instance.ConfigureLoginManager(this.LoginVerifierId, this.PermissionManagerId);
    SystemConfigurationManager.Instance.UserInterfaceManager = (IUserInterfaceManager) UserInterfaceManager.Instance;
    SystemConfigurationManager.Instance.DocumentPrintManager = (IDocumentPrintManager) DocumentPrintingManager.Instance;
  }

  public void LoadAllPlugins(bool startAsServiceTool)
  {
    SystemConfigurationManager.Instance.LoadRegisteredPluginTypes();
    ApplicationHelper.Logger.Info("Starting application components");
    IEnumerable<IApplicationComponent> source = SystemConfigurationManager.Instance.Plugins.OfType<IApplicationComponent>();
    if (!startAsServiceTool)
    {
      if (Application.ProductName.Equals("Mira"))
      {
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("A255A96F-5A7E-4CA9-8B67-DA37EFB8EFDF"))));
      }
      else
      {
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("0BF386E1-2DA1-423a-9D1E-658F24B753E4"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("0135E402-053D-4696-B68B-F574A1096246"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("3C09F61E-4C2B-4c91-A753-90BED7D9EE41"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("A4465EA9-02B7-4b12-A530-7946D4813552"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("D1C72D61-1CD3-467e-9F41-0343B4AAFA76"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("2FD02C7C-E616-4a89-AAF0-E455C5928C74"))));
        UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("5EE518CA-0897-48b0-8A05-8577B1F5882E"))));
      }
    }
    else
      UserInterfaceManager.Instance.AddApplicationComponent(source.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (c => c.Fingerprint == new Guid("0C138E27-B9D5-4a7a-AF02-7E9AB34CEEDB"))));
    ApplicationHelper.Logger.Info("Application components started");
    SystemConfigurationManager.Instance.UserInterfaceManager.HelpUrl = this.HelpFile;
    ApplicationHelper.Logger.Info("Configuration loaded [{0} seconds]", (object) DateTime.Now.Subtract(this.configuruationStart).TotalSeconds);
  }
}
