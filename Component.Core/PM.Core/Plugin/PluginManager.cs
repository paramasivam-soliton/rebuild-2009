// Decompiled with JetBrains decompiler
// Type: PathMedical.Plugin.PluginManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Logging;
using PathMedical.Properties;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.Plugin;

[Obsolete]
public class PluginManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (PluginManager));
  private readonly List<Assembly> assemblies;
  private List<Type> assemblyTypes;
  private readonly List<IPlugin> registeredPlugins;

  public static PluginManager Instance => PathMedical.Singleton.Singleton<PluginManager>.Instance;

  private PluginManager()
  {
    this.assemblies = new List<Assembly>();
    this.registeredPlugins = new List<IPlugin>();
  }

  public void RegisterAssemblies(string directory)
  {
    foreach (string file in Directory.GetFiles(directory, "*.dll", SearchOption.TopDirectoryOnly))
      this.RegisterAssembly(file);
    if (this.assemblyTypes != null && this.assemblyTypes.Count != 0)
      return;
    this.assemblyTypes = this.assemblies.SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>) (a => (IEnumerable<Type>) a.GetTypes())).ToList<Type>();
  }

  public void RegisterAssembly(string assemblyName)
  {
    Assembly assembly;
    try
    {
      assembly = Assembly.LoadFrom(assemblyName);
      if (((IEnumerable<Type>) assembly.GetTypes()).Where<Type>((Func<Type, bool>) (t => typeof (IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)).Count<Type>() > 0)
        this.assemblies.Add(assembly);
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
    PluginManager.Logger.Info(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyLoadedSuccessfully, (object) assembly.FullName));
    if (this.assemblyTypes == null)
      return;
    this.assemblyTypes.Clear();
  }

  public void LoadPlugins()
  {
    SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<ITestPlugin>());
    SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<IInstrumentPlugin>());
    SystemConfigurationManager.Instance.AddPlugin((IPlugin[]) this.LoadPluginsOfType<IApplicationComponent>());
    this.assemblyTypes.Clear();
    this.assemblies.Clear();
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
        if (!this.registeredPlugins.Contains((IPlugin) obj3))
          this.registeredPlugins.Add((IPlugin) obj3);
        if (!objList.Contains(obj3))
          objList.Add(obj3);
      }
      else
      {
        System.Exception exception = (System.Exception) ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyIsNotAPluginException, (object) obj1, (object) typeof (T)));
        PluginManager.Logger.Error(exception);
        throw exception;
      }
    }
    return objList.ToArray();
  }

  [Obsolete]
  public T GetClassType<T>(string assemblyToInspect, string classToFind) where T : class, IPlugin
  {
    if (string.IsNullOrEmpty(assemblyToInspect))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (assemblyToInspect));
    if (string.IsNullOrEmpty(classToFind))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (classToFind));
    Assembly assembly;
    try
    {
      assembly = Assembly.Load(assemblyToInspect);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CantLoadAssembly, (object) assemblyToInspect), (System.Exception) ex);
    }
    catch (FileNotFoundException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyNotFoundException, (object) assemblyToInspect), (System.Exception) ex);
    }
    catch (BadImageFormatException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "AssemblyHasBadFormat"), (System.Exception) ex);
    }
    if (assembly == (Assembly) null)
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyNullException, (object) assemblyToInspect));
    PluginManager.Logger.Info(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyLoadedSuccessfully, (object) assembly.FullName));
    Type type;
    try
    {
      type = assembly.GetType(classToFind, true);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyClassNotFoundException, (object) classToFind, (object) assemblyToInspect), (System.Exception) ex);
    }
    catch (TypeLoadException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyClassTypeException, (object) classToFind, (object) assemblyToInspect), (System.Exception) ex);
    }
    PropertyInfo property;
    try
    {
      property = type.GetProperty("Instance");
    }
    catch (AmbiguousMatchException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CheckForSingletonException, (object) classToFind), (System.Exception) ex);
    }
    object obj = !(property != (PropertyInfo) null) || !property.PropertyType.IsClass ? Activator.CreateInstance(type) : property.GetValue((object) null, (object[]) null);
    if (obj is IPlugin && obj is T)
    {
      T classType = obj as T;
      if (!this.registeredPlugins.Contains((IPlugin) classType))
        this.registeredPlugins.Add((IPlugin) classType);
      return classType;
    }
    System.Exception exception = (System.Exception) ExceptionFactory.Instance.CreateException<PluginLoadException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AssemblyIsNotAPluginException, (object) classToFind, (object) typeof (T)));
    PluginManager.Logger.Error(exception);
    throw exception;
  }

  public IEnumerable<IPlugin> Plugins => (IEnumerable<IPlugin>) this.registeredPlugins;

  public IPlugin this[Guid fingerprint]
  {
    get
    {
      return this.registeredPlugins.Where<IPlugin>((Func<IPlugin, bool>) (plugin => plugin.Fingerprint.Equals(fingerprint))).FirstOrDefault<IPlugin>();
    }
  }
}
