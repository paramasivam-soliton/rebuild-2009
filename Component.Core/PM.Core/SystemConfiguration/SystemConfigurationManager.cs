// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.SystemConfigurationManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.Exception;
using PathMedical.FileSystem;
using PathMedical.InstrumentManagement;
using PathMedical.Logging;
using PathMedical.Login;
using PathMedical.Permission;
using PathMedical.Plugin;
using PathMedical.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

#nullable disable
namespace PathMedical.SystemConfiguration;

public sealed class SystemConfigurationManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (SystemConfigurationManager), "$Rev$");
  private readonly List<CultureInfo> languages;
  private readonly List<IPlugin> registeredPlugins = new List<IPlugin>();
  private readonly List<IApplicationComponent> applicationComponents = new List<IApplicationComponent>();
  private List<PathMedical.SystemConfiguration.SystemConfiguration> systemConfigurationEntries;
  private readonly Guid siteManagementFingerprint = new Guid("3C09F61E-4C2B-4c91-A753-90BED7D9EE41");
  private readonly Guid userManagementFingerprint = new Guid("0135E402-053D-4696-B68B-F574A1096246");
  private readonly Guid systemManagement = new Guid("4A7C8559-064D-4271-B001-953A0B01C5F3");
  private readonly List<string> filesToBackup = new List<string>();
  private List<Type> assemblyTypes;

  public static SystemConfigurationManager Instance
  {
    get => PathMedical.Singleton.Singleton<SystemConfigurationManager>.Instance;
  }

  private SystemConfigurationManager()
  {
    this.languages = new List<CultureInfo>();
    this.SetLanguage(new CultureInfo("en"));
    this.registeredPlugins = new List<IPlugin>();
  }

  public CultureInfo BaseLanguage => new CultureInfo(this.BaseLanguageIdentifier);

  public string BaseLanguageIdentifier
  {
    get
    {
      string languageIdentifier = this.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultSystemLanguage");
      if (string.IsNullOrEmpty(languageIdentifier))
        languageIdentifier = "en";
      return languageIdentifier;
    }
    set
    {
      this.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultSystemLanguage", value);
    }
  }

  public CultureInfo CurrentLanguage => Thread.CurrentThread.CurrentUICulture;

  public DateTimeFormatInfo CurrentDateTimeFormat => CultureInfo.CurrentCulture.DateTimeFormat;

  public IEnumerable<CultureInfo> SupportedLanguages => (IEnumerable<CultureInfo>) this.languages;

  public IEnumerable<CultureInfo> OptionalLanguages
  {
    get
    {
      List<CultureInfo> optionalLanguages = new List<CultureInfo>();
      if (this.languages != null)
        optionalLanguages.AddRange(this.languages.Where<CultureInfo>((Func<CultureInfo, bool>) (cultureInfo => !cultureInfo.Equals((object) this.BaseLanguage))));
      return (IEnumerable<CultureInfo>) optionalLanguages;
    }
  }

  public void SetLanguageSupport(string baseLanguageCode, string additionalLanguageList)
  {
    this.languages.Clear();
    try
    {
      this.languages.Add(new CultureInfo(baseLanguageCode));
    }
    catch (ArgumentException ex)
    {
      SystemConfigurationManager.Logger.Error((System.Exception) ex, "Unkown culture definition. Please check <MultiLanguageBase> parameter in configuration file.");
      return;
    }
    foreach (string name in ((IEnumerable<string>) additionalLanguageList.Split(',')).Where<string>((Func<string, bool>) (sp => !string.IsNullOrEmpty(sp))))
    {
      try
      {
        this.languages.Add(new CultureInfo(name));
      }
      catch (ArgumentException ex)
      {
        SystemConfigurationManager.Logger.Error((System.Exception) ex, "Unkown culture definition. Please check <MultiLanguageSupport> parameter in configuration file.");
      }
    }
  }

  public void SetLanguage(CultureInfo culture)
  {
    if (culture == null)
    {
      Thread.CurrentThread.CurrentUICulture = SystemConfigurationManager.Instance.BaseLanguage;
      ApplicationComponentModuleManager.Instance.DestroyCache();
    }
    else
    {
      if (!(culture.IetfLanguageTag != Thread.CurrentThread.CurrentUICulture.IetfLanguageTag))
        return;
      Thread.CurrentThread.CurrentUICulture = culture;
      ApplicationComponentModuleManager.Instance.DestroyCache();
    }
  }

  public IEnumerable<IPlugin> Plugins => (IEnumerable<IPlugin>) this.registeredPlugins;

  public void AddPlugin(IPlugin[] plugins)
  {
    foreach (IPlugin plugin in plugins)
      this.AddPlugin(plugin);
  }

  public void AddPlugin(IPlugin plugin)
  {
    if (plugin == null || plugin.Fingerprint.Equals(Guid.Empty) || this.registeredPlugins.Contains(plugin))
      return;
    this.registeredPlugins.Add(plugin);
    if (plugin is IApplicationComponent)
      this.AddApplicationComponent(plugin as IApplicationComponent);
    if (!(plugin is ISupportPluginDataExchange))
      return;
    DataExchangeManager.Instance.RegisterDataExchangePlugin(plugin as ISupportPluginDataExchange);
  }

  public bool IsPluginLoaded(Guid fingerprint)
  {
    return this.registeredPlugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(fingerprint)));
  }

  public IEnumerable<IApplicationComponent> ApplicationComponents
  {
    get => (IEnumerable<IApplicationComponent>) this.applicationComponents;
  }

  private void AddApplicationComponent(IApplicationComponent appliationComponent)
  {
    if (appliationComponent == null || appliationComponent.Fingerprint.Equals(Guid.Empty) || this.applicationComponents.Contains(appliationComponent))
      return;
    this.applicationComponents.Add(appliationComponent);
  }

  private void RebuildApplicationComponents()
  {
    List<IApplicationComponentModule> applicationComponentModuleList = new List<IApplicationComponentModule>();
    foreach (object applicationComponent in this.applicationComponents)
      applicationComponent.GetType();
  }

  public IUserInterfaceManager UserInterfaceManager { get; set; }

  public IDocumentPrintManager DocumentPrintManager { get; set; }

  public StorageConfirmation InformUserAfterSuccessfulStorageOperation
  {
    get
    {
      StorageConfirmation storageOperation = StorageConfirmation.No;
      string configurationValue = this.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyStorage");
      if (!string.IsNullOrEmpty(configurationValue))
      {
        try
        {
          storageOperation = (StorageConfirmation) Enum.Parse(typeof (StorageConfirmation), configurationValue);
        }
        catch (ArgumentException ex)
        {
        }
      }
      return storageOperation;
    }
    set
    {
      this.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyStorage", value.ToString());
      this.StoreConfigurationChanges();
    }
  }

  public DeletionConfirmation InformUserAfterSuccessfulDeleteOperation
  {
    get
    {
      DeletionConfirmation successfulDeleteOperation = DeletionConfirmation.No;
      string configurationValue = this.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyDeletion");
      if (!string.IsNullOrEmpty(configurationValue))
      {
        try
        {
          successfulDeleteOperation = (DeletionConfirmation) Enum.Parse(typeof (DeletionConfirmation), configurationValue);
        }
        catch (ArgumentException ex)
        {
        }
      }
      return successfulDeleteOperation;
    }
    set
    {
      this.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyDeletion", value.ToString());
      this.StoreConfigurationChanges();
    }
  }

  public DataModificationWarning InformUserAfterBeginChangingData
  {
    get
    {
      DataModificationWarning beginChangingData = DataModificationWarning.No;
      string configurationValue = this.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayDataModificationWarning");
      if (!string.IsNullOrEmpty(configurationValue))
      {
        try
        {
          beginChangingData = (DataModificationWarning) Enum.Parse(typeof (DataModificationWarning), configurationValue);
        }
        catch (ArgumentException ex)
        {
        }
      }
      return beginChangingData;
    }
    set
    {
      this.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayDataModificationWarning", value.ToString());
      this.StoreConfigurationChanges();
    }
  }

  public string SystemBackupFolder
  {
    get => this.GetSystemConfigurationValue(this.systemManagement, "BackupFolder");
  }

  private IEnumerable<PathMedical.SystemConfiguration.SystemConfiguration> SystemConfigurationEntries
  {
    get
    {
      if (this.systemConfigurationEntries == null)
        this.LoadSystemConfigurationEntries();
      return (IEnumerable<PathMedical.SystemConfiguration.SystemConfiguration>) this.systemConfigurationEntries;
    }
  }

  private void LoadSystemConfigurationEntries()
  {
    using (DBScope scope = new DBScope())
    {
      this.systemConfigurationEntries = new AdapterBase<PathMedical.SystemConfiguration.SystemConfiguration>(scope).All.ToList<PathMedical.SystemConfiguration.SystemConfiguration>();
      scope.Complete();
    }
  }

  private PathMedical.SystemConfiguration.SystemConfiguration GetSystemConfiguration(
    Guid? componentId,
    string key)
  {
    PathMedical.SystemConfiguration.SystemConfiguration systemConfiguration = this.SystemConfigurationEntries.FirstOrDefault<PathMedical.SystemConfiguration.SystemConfiguration>((Func<PathMedical.SystemConfiguration.SystemConfiguration, bool>) (sce =>
    {
      Guid? componentId1 = sce.ComponentId;
      Guid? nullable = componentId;
      return (componentId1.HasValue == nullable.HasValue ? (componentId1.HasValue ? (componentId1.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && sce.Key == key;
    }));
    if (systemConfiguration == null)
    {
      systemConfiguration = new PathMedical.SystemConfiguration.SystemConfiguration()
      {
        ComponentId = componentId,
        Key = key,
        IsChanged = true,
        Value = string.Empty
      };
      this.systemConfigurationEntries.Add(systemConfiguration);
    }
    return systemConfiguration;
  }

  public string GetSystemConfigurationValue(Guid componentId, string key)
  {
    return this.GetSystemConfiguration(new Guid?(componentId), key).Value;
  }

  public bool? GetSystemConfigurationValueAsBoolean(Guid componentId, string key)
  {
    bool result;
    return !bool.TryParse(this.GetSystemConfiguration(new Guid?(componentId), key).Value, out result) ? new bool?() : new bool?(result);
  }

  public string GetSystemConfigurationValue(string key)
  {
    return this.GetSystemConfiguration(new Guid?(), key).Value;
  }

  public void SetSystemConfigurationValue(string key, string value)
  {
    PathMedical.SystemConfiguration.SystemConfiguration systemConfiguration = this.GetSystemConfiguration(new Guid?(), key);
    systemConfiguration.Value = value;
    systemConfiguration.IsChanged = true;
  }

  public void SetSystemConfigurationValue(Guid componentId, string key, string value)
  {
    PathMedical.SystemConfiguration.SystemConfiguration systemConfiguration = this.GetSystemConfiguration(new Guid?(componentId), key);
    systemConfiguration.Value = value;
    systemConfiguration.IsChanged = true;
  }

  public void SetSystemConfigurationValue(Guid componentId, string key, bool? value)
  {
    PathMedical.SystemConfiguration.SystemConfiguration systemConfiguration = this.GetSystemConfiguration(new Guid?(componentId), key);
    systemConfiguration.Value = !value.HasValue ? string.Empty : value.ToString();
    systemConfiguration.IsChanged = true;
  }

  public void StoreConfigurationChanges()
  {
    using (DBScope scope = new DBScope())
    {
      AdapterBase<PathMedical.SystemConfiguration.SystemConfiguration> adapterBase = new AdapterBase<PathMedical.SystemConfiguration.SystemConfiguration>(scope);
      foreach (PathMedical.SystemConfiguration.SystemConfiguration entity in this.SystemConfigurationEntries.Where<PathMedical.SystemConfiguration.SystemConfiguration>((Func<PathMedical.SystemConfiguration.SystemConfiguration, bool>) (sce => sce.IsChanged)))
      {
        adapterBase.Store(entity);
        entity.IsChanged = false;
      }
      this.LoadSystemConfigurationEntries();
      scope.Complete();
    }
  }

  public void ResetConfigurationChanges() => this.LoadSystemConfigurationEntries();

  public bool IsSiteAndFacilityManagementActive
  {
    get
    {
      return bool.TrueString.Equals(this.GetSystemConfigurationValue(this.siteManagementFingerprint, "IsActive"));
    }
    set
    {
      this.SetSystemConfigurationValue(this.siteManagementFingerprint, "IsActive", value.ToString());
      this.StoreConfigurationChanges();
    }
  }

  public bool IsUserManagementActive
  {
    get
    {
      return bool.TrueString.Equals(this.GetSystemConfigurationValue(this.userManagementFingerprint, "IsActive"));
    }
    set
    {
      this.SetSystemConfigurationValue(this.userManagementFingerprint, "IsActive", value.ToString());
      this.StoreConfigurationChanges();
    }
  }

  public int UserAccountLockingTime
  {
    get
    {
      string configurationValue = this.GetSystemConfigurationValue(this.userManagementFingerprint, nameof (UserAccountLockingTime));
      return !string.IsNullOrEmpty(configurationValue) ? Convert.ToInt32(configurationValue) : 15;
    }
  }

  public bool ShallTrackHistory => false;

  public string TemporaryInstrumentDataDirectory { get; set; }

  public string ApplicationDataDirectory { get; set; }

  public string ApplicationLogFile { get; set; }

  public void AddBackupFile(string file)
  {
    if (string.IsNullOrEmpty(file))
      return;
    this.filesToBackup.Add(file);
  }

  public string Backup()
  {
    if (string.IsNullOrEmpty(this.SystemBackupFolder))
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.SystemConfigurationManager_UnconfiguredBackupFolder);
    if (!FileSystemHelper.DoesFolderExists(this.SystemBackupFolder))
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.SystemConfigurationManager_BackupFolderMissing);
    if (this.filesToBackup.Count == 0)
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.SystemConfigurationManager_NoFilesToBackup);
    string str = Path.Combine(this.SystemBackupFolder, string.Format("AccuLink-{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}-{0:fff}_{1}_{2}", (object) DateTime.Now, (object) Environment.MachineName, (object) Environment.UserName));
    try
    {
      Directory.CreateDirectory(str);
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>($"{Resources.SystemConfigurationManager_FolderCreationException}{Environment.NewLine}{ex.Message}", ex);
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string fileName in this.filesToBackup)
    {
      if (!FileSystemHelper.DoesFileExists(fileName))
      {
        stringBuilder.Append(string.Format(Resources.SystemConfigurationManager_BackupFileMissing, (object) fileName, (object) Environment.NewLine));
      }
      else
      {
        try
        {
          FileSystemHelper.CopyFile(fileName, str);
        }
        catch (System.Exception ex)
        {
          stringBuilder.Append(ex.Message);
        }
      }
    }
    return stringBuilder.ToString();
  }

  public void LogEnvironment()
  {
    SystemConfigurationManager.Logger.Info("Operating system: {0}", (object) Environment.OSVersion);
    SystemConfigurationManager.Logger.Info("Computer name: {0}", (object) Environment.MachineName);
    SystemConfigurationManager.Logger.Info("CLR runtime version: {0}", (object) Environment.Version);
  }

  public void RegisterExecutingAssemblyPlugins()
  {
    Assembly entryAssembly = Assembly.GetEntryAssembly();
    SystemConfigurationManager.Logger.Debug("Inspecting executing assembly \"{0}\" for plugins", (object) entryAssembly.FullName);
    this.RegisterPluginAssembly(entryAssembly);
    SystemConfigurationManager.Logger.Debug("Completed inspection of executing assembly for plugins");
  }

  public void RegisterPluginAssemblies(string directory)
  {
    foreach (string file in Directory.GetFiles(directory, "*.dll", SearchOption.TopDirectoryOnly))
      this.RegisterPluginAssembly(file);
  }

  public void RegisterPluginAssembly(string assemblyName)
  {
    Assembly assembly;
    try
    {
      assembly = Assembly.LoadFrom(assemblyName);
      this.RegisterPluginAssembly(assembly);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CantLoadAssembly, (object) assemblyName), (System.Exception) ex);
    }
    catch (FileNotFoundException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyNotFoundException, (object) assemblyName), (System.Exception) ex);
    }
    catch (BadImageFormatException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "AssemblyHasBadFormat"), (System.Exception) ex);
    }
    if (assembly == (Assembly) null)
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyNullException, (object) assemblyName));
  }

  public void RegisterPluginAssembly(Assembly assembly)
  {
    if (assembly == (Assembly) null)
      return;
    try
    {
      IEnumerable<Type> types = ((IEnumerable<Type>) assembly.GetTypes()).Where<Type>((Func<Type, bool>) (t => typeof (IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface));
      if (types != null)
      {
        foreach (Type pluginType in types)
          this.RegisterPluginType(pluginType);
      }
    }
    catch (ReflectionTypeLoadException ex)
    {
      foreach (System.Exception loaderException in ex.LoaderExceptions)
        Console.WriteLine(loaderException.Message);
    }
    SystemConfigurationManager.Logger.Info(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyLoadedSuccessfully, (object) assembly.FullName));
  }

  private void RegisterPluginType(Type pluginType)
  {
    if (pluginType == (Type) null)
      return;
    if (this.assemblyTypes == null || this.assemblyTypes.Count == 0)
      this.assemblyTypes = new List<Type>();
    if (!this.assemblyTypes.Contains(pluginType))
      this.assemblyTypes.Add(pluginType);
    Type type = this.assemblyTypes.Where<Type>((Func<Type, bool>) (t => string.Compare(t.FullName, "PathMedical.SystemConfiguration.Viewer.WindowsForms.SystemConfigurationComponent") == 0)).FirstOrDefault<Type>();
    if (!(type != (Type) null))
      return;
    this.assemblyTypes.Remove(type);
    this.assemblyTypes.Add(type);
  }

  public void LoadRegisteredPluginTypes()
  {
    try
    {
      SystemConfigurationManager.Logger.Info("Loading plugins");
      SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<ITestPlugin>());
      SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<IInstrumentPlugin>());
      SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<ISupportDataExchangeModules>());
      SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<IApplicationComponent>());
      this.assemblyTypes.Clear();
    }
    finally
    {
      SystemConfigurationManager.Logger.Info("Loading loaded");
    }
  }

  public T[] LoadPluginsOfType<T>() where T : class, IPlugin
  {
    List<T> objList = new List<T>();
    foreach (Type type in this.assemblyTypes.Where<Type>((Func<Type, bool>) (t => typeof (T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)).ToArray<Type>())
    {
      T obj1 = default (T);
      PropertyInfo property;
      try
      {
        property = type.GetProperty("Instance");
      }
      catch (AmbiguousMatchException ex)
      {
        throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CheckForSingletonException, (object) type), (System.Exception) ex);
      }
      object obj2;
      try
      {
        obj2 = !(property != (PropertyInfo) null) || !property.PropertyType.IsClass ? Activator.CreateInstance(type) : property.GetValue((object) null, (object[]) null);
      }
      catch (TargetInvocationException ex)
      {
        throw;
      }
      if (obj2 is IPlugin && obj2 is T)
      {
        T obj3 = obj2 as T;
        this.AddPlugin((IPlugin) obj3);
        if (!objList.Contains(obj3))
          objList.Add(obj3);
      }
      else
      {
        System.Exception exception = (System.Exception) ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyIsNotAPluginException, (object) obj1, (object) typeof (T)));
        SystemConfigurationManager.Logger.Error(exception);
        throw exception;
      }
    }
    return objList.ToArray();
  }

  public void ConfigureDatabasePlugin(Guid databaseProviderId, string connectionString)
  {
    IDatabaseProvider databaseProvider = ((IEnumerable<IDatabaseProvider>) this.LoadPluginsOfType<IDatabaseProvider>()).Where<IDatabaseProvider>((Func<IDatabaseProvider, bool>) (p => p.Fingerprint == databaseProviderId)).FirstOrDefault<IDatabaseProvider>();
    if (databaseProvider == null)
      return;
    databaseProvider.ConnectionString = connectionString;
    DatabaseProviderFactory.Instance.DatabaseProvider = databaseProvider;
  }

  public void ConfigureLoginManager(
    Guid loginVerifierFingerprint,
    Guid permissionManagerFingerprint)
  {
    LoginManager.Instance.InitializeLoginVerifier(((IEnumerable<ILoginVerifier>) this.LoadPluginsOfType<ILoginVerifier>()).Where<ILoginVerifier>((Func<ILoginVerifier, bool>) (p => p.Fingerprint == loginVerifierFingerprint)).FirstOrDefault<ILoginVerifier>());
    PermissionManager.Instance.ConcretePermissionManager = ((IEnumerable<IPermissionManager>) this.LoadPluginsOfType<IPermissionManager>()).Where<IPermissionManager>((Func<IPermissionManager, bool>) (p => p.Fingerprint == permissionManagerFingerprint)).FirstOrDefault<IPermissionManager>();
  }

  public string TrackingSystem
  {
    get => this.GetSystemConfigurationValue(this.systemManagement, nameof (TrackingSystem));
    set
    {
      this.SetSystemConfigurationValue(this.systemManagement, nameof (TrackingSystem), value.ToString());
      this.StoreConfigurationChanges();
    }
  }
}
