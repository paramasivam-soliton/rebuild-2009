// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ApplicationComponentModuleManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface;

public class ApplicationComponentModuleManager
{
  private readonly Dictionary<Type, IApplicationComponentModule> loadedModules;

  public static ApplicationComponentModuleManager Instance
  {
    get => PathMedical.Singleton.Singleton<ApplicationComponentModuleManager>.Instance;
  }

  private ApplicationComponentModuleManager()
  {
    this.loadedModules = new Dictionary<Type, IApplicationComponentModule>();
  }

  public IApplicationComponentModule Get(Type applicationComponentModuleType)
  {
    IApplicationComponentModule applicationComponentModule = (IApplicationComponentModule) null;
    if (!this.loadedModules.TryGetValue(applicationComponentModuleType, out applicationComponentModule))
    {
      object instance = Activator.CreateInstance(applicationComponentModuleType);
      if (instance is IApplicationComponentModule)
      {
        applicationComponentModule = instance as IApplicationComponentModule;
        this.loadedModules.Add(applicationComponentModuleType, applicationComponentModule);
      }
    }
    return applicationComponentModule;
  }

  public void DestroyCache() => this.loadedModules.Clear();
}
