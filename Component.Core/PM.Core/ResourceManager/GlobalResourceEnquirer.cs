// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.GlobalResourceEnquirer
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Extensions;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.ResourceManager;

public class GlobalResourceEnquirer
{
  private readonly List<System.Resources.ResourceManager> globalResourceSources = new List<System.Resources.ResourceManager>();

  public static GlobalResourceEnquirer Instance => PathMedical.Singleton.Singleton<GlobalResourceEnquirer>.Instance;

  private GlobalResourceEnquirer()
  {
  }

  public void RegisterResourceManager(System.Resources.ResourceManager globalResourceSource)
  {
    if (globalResourceSource == null || this.globalResourceSources.Contains(globalResourceSource))
      return;
    this.globalResourceSources.Add(globalResourceSource);
  }

  public object GetResourceByName(string resourceName)
  {
    return this.GetResourceByName(resourceName, SystemConfigurationManager.Instance.CurrentLanguage);
  }

  public object GetResourceByName(string resourceName, CultureInfo cultureInfo)
  {
    return this.globalResourceSources == null ? (object) null : this.globalResourceSources.Select<System.Resources.ResourceManager, object>((Func<System.Resources.ResourceManager, object>) (grs => grs.GetResourceByResourceName(resourceName, cultureInfo))).FirstOrDefault<object>((Func<object, bool>) (res => res != null));
  }

  public object GetResourceByName(Enum resourceName)
  {
    return this.GetResourceByName(resourceName, string.Empty, SystemConfigurationManager.Instance.CurrentLanguage);
  }

  public object GetResourceByName(Enum resourceName, string suffix)
  {
    return this.GetResourceByName(resourceName, suffix, SystemConfigurationManager.Instance.CurrentLanguage);
  }

  public object GetResourceByName(Enum resourceName, string suffix, CultureInfo cultureInfo)
  {
    if (resourceName == null || cultureInfo == null)
      return (object) null;
    if (this.globalResourceSources == null)
      return (object) null;
    StringBuilder resourceNameToLookUp = new StringBuilder();
    resourceNameToLookUp.AppendFormat("{0}.{1}", (object) resourceName.GetType(), (object) resourceName);
    if (!string.IsNullOrEmpty(suffix))
      resourceNameToLookUp.AppendFormat(".{0}", (object) suffix);
    resourceNameToLookUp.Replace(".", "__");
    return this.globalResourceSources.Select<System.Resources.ResourceManager, object>((Func<System.Resources.ResourceManager, object>) (grs => grs.GetObject(resourceNameToLookUp.ToString(), cultureInfo))).FirstOrDefault<object>((Func<object, bool>) (res => res != null));
  }
}
