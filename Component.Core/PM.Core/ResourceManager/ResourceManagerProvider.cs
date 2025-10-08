// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.ResourceManagerProvider
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace PathMedical.ResourceManager;

public class ResourceManagerProvider
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (ResourceManagerProvider));
  private static Type resourceManagerType = typeof (ComponentResourceManager);

  public static Type ResourceManagerType
  {
    get => ResourceManagerProvider.resourceManagerType;
    set
    {
      ResourceManagerProvider.resourceManagerType = value.FullName.Equals("System.Resources.ResourceManager", StringComparison.InvariantCultureIgnoreCase) || value.IsSubclassOf(typeof (System.Resources.ResourceManager)) ? value : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ResourceManagerType must be a sub class of System.ComponentModel.ComponentResourceManager"));
    }
  }

  public static void GetResourceManager(Type type, out ComponentResourceManager resourceManager)
  {
    Type[] types = new Type[1]{ typeof (Type) };
    object[] parameters = new object[1]{ (object) type };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (ComponentResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, ComponentResourceManagement.Instance.ResourceManager.GetString("ResourceManagerNoAdequateConstructorException"), (object) ResourceManagerProvider.resourceManagerType.FullName, (object) type.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(string.Format((IFormatProvider) CultureInfo.InvariantCulture, ComponentResourceManagement.Instance.ResourceManager.GetString("CreateResourceManagerException"), (object) ResourceManagerProvider.resourceManagerType));
      throw;
    }
  }

  public static void GetResourceManager(
    string baseName,
    Assembly assembly,
    out ComponentResourceManager resourceManager)
  {
    Type[] types = new Type[2]
    {
      typeof (string),
      typeof (Assembly)
    };
    object[] parameters = new object[2]
    {
      (object) baseName,
      (object) assembly
    };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (ComponentResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "A resource manager shall be created but no adequate constructor is available. The type of the ResourceManager is '{0}'.", (object) ResourceManagerProvider.resourceManagerType.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(ex, "Failure while creating a resource manager of type {0}.", (object) ResourceManagerProvider.resourceManagerType);
      throw;
    }
  }

  public static void GetResourceManager(
    string baseName,
    Assembly assembly,
    Type usingResourceSet,
    out ComponentResourceManager resourceManager)
  {
    Type[] types = new Type[3]
    {
      typeof (string),
      typeof (Assembly),
      typeof (Type)
    };
    object[] parameters = new object[3]
    {
      (object) baseName,
      (object) assembly,
      (object) usingResourceSet
    };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (ComponentResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "A resource manager shall be created but no adequate constructor is available. The type of the ResourceManager is '{0}'.", (object) ResourceManagerProvider.resourceManagerType.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(ex, "Failure while creating a resource manager of type {0}.", (object) ResourceManagerProvider.resourceManagerType);
      throw;
    }
  }

  public static void GetResourceManager(Type type, out System.Resources.ResourceManager resourceManager)
  {
    Type[] types = new Type[1]{ typeof (Type) };
    object[] parameters = new object[1]{ (object) type };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (System.Resources.ResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "A resource manager shall be created but no adequate constructor is available. The type of the ResourceManager is '{0}'.", (object) ResourceManagerProvider.resourceManagerType.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(ex, "Failure while creating a resource manager of type {0}.", (object) ResourceManagerProvider.resourceManagerType);
      throw;
    }
  }

  public static void GetResourceManager(
    string baseName,
    Assembly assembly,
    out System.Resources.ResourceManager resourceManager)
  {
    Type[] types = new Type[2]
    {
      typeof (string),
      typeof (Assembly)
    };
    object[] parameters = new object[2]
    {
      (object) baseName,
      (object) assembly
    };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (System.Resources.ResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "A resource manager shall be created but no adequate constructor is available. The type of the ResourceManager is '{0}'.", (object) ResourceManagerProvider.resourceManagerType.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(ex, "Failure while creating a resource manager of type {0}.", (object) ResourceManagerProvider.resourceManagerType);
      throw;
    }
  }

  public static void GetResourceManager(
    string baseName,
    Assembly assembly,
    Type usingResourceSet,
    out System.Resources.ResourceManager resourceManager)
  {
    Type[] types = new Type[3]
    {
      typeof (string),
      typeof (Assembly),
      typeof (Type)
    };
    object[] parameters = new object[3]
    {
      (object) baseName,
      (object) assembly,
      (object) usingResourceSet
    };
    try
    {
      ConstructorInfo constructor = ResourceManagerProvider.resourceManagerType.GetConstructor(types);
      resourceManager = constructor != (ConstructorInfo) null ? (System.Resources.ResourceManager) constructor.Invoke(parameters) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "A resource manager shall be created but no adequate constructor is available. The type of the ResourceManager is '{0}'.", (object) ResourceManagerProvider.resourceManagerType.FullName));
    }
    catch (System.Exception ex)
    {
      ResourceManagerProvider.logger.Error(ex, "Failure while creating a resource manager of type {0}.", (object) ResourceManagerProvider.resourceManagerType);
      throw;
    }
  }
}
