// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.ExceptionHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;

#nullable disable
namespace PathMedical.DatabaseManagement;

internal static class ExceptionHelper
{
  internal static TEx CreateException<TEx>(string messageResourceName) where TEx : System.Exception
  {
    return ExceptionFactory.Instance.CreateException<TEx>(ComponentResourceManagement.Instance.ResourceManager.GetString(messageResourceName));
  }

  internal static TEx CreateException<TEx>(string messageResourceName, params object[] parameter) where TEx : System.Exception
  {
    return ExceptionFactory.Instance.CreateException<TEx>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString(messageResourceName), parameter));
  }

  internal static TEx CreateException<TEx>(System.Exception inner, string messageResourceName) where TEx : System.Exception
  {
    return ExceptionFactory.Instance.CreateException<TEx>(ComponentResourceManagement.Instance.ResourceManager.GetString(messageResourceName), inner);
  }

  internal static TEx CreateException<TEx>(
    System.Exception inner,
    string messageResourceName,
    params object[] parameter)
    where TEx : System.Exception
  {
    return ExceptionFactory.Instance.CreateException<TEx>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString(messageResourceName), parameter), inner);
  }
}
