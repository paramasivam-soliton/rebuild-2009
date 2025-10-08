// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.ResourceManagerExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.SystemConfiguration;
using System;
using System.Globalization;
using System.Resources;
using System.Text;

#nullable disable
namespace PathMedical.Extensions;

public static class ResourceManagerExtensions
{
  public static object GetResourceByResourceName(
    this ResourceManager resourceManager,
    string resourceName,
    CultureInfo cultureInfo)
  {
    if (resourceManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (resourceManager));
    if (resourceName == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (resourceName));
    if (cultureInfo == null)
      cultureInfo = SystemConfigurationManager.Instance.BaseLanguage;
    object resourceByResourceName = resourceManager.GetObject(resourceName, cultureInfo) ?? resourceManager.GetObject(resourceName.ToUpperInvariant(), cultureInfo);
    if (resourceByResourceName == null && cultureInfo != CultureInfo.InvariantCulture)
      resourceByResourceName = resourceManager.GetResourceByResourceName(resourceName, cultureInfo.Parent);
    return resourceByResourceName;
  }

  public static object GetResourceByResourceName(
    this ResourceManager resourceManager,
    Enum resourceName,
    CultureInfo cultureInfo)
  {
    if (resourceManager == null || resourceName == null || cultureInfo == null)
      return (object) null;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0}.{1}", (object) resourceName.GetType(), (object) resourceName);
    stringBuilder.Replace(".", "__");
    return resourceManager.GetResourceByResourceName(stringBuilder.ToString(), cultureInfo);
  }
}
