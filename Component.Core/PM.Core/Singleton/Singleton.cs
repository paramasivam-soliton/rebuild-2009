// Decompiled with JetBrains decompiler
// Type: PathMedical.Singleton.Singleton`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Reflection;

#nullable disable
namespace PathMedical.Singleton;

public static class Singleton<T> where T : class
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (T), "$Rev: 1017 $");
  private static readonly T SingletonInstance;

  static Singleton()
  {
    try
    {
      PathMedical.Singleton.Singleton<T>.Logger.Debug("Creating singleton instance of type {0}", (object) typeof (T));
      PathMedical.Singleton.Singleton<T>.SingletonInstance = typeof (T).InvokeMember(typeof (T).Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, (object) null, (object[]) null) as T;
    }
    catch (Exception ex)
    {
      PathMedical.Singleton.Singleton<T>.Logger.Error(ex, "Error when creating singleton instance of type {0}", (object) typeof (T));
      throw;
    }
  }

  public static T Instance => PathMedical.Singleton.Singleton<T>.SingletonInstance;
}
