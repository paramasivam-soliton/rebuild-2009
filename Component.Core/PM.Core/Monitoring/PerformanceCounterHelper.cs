// Decompiled with JetBrains decompiler
// Type: PathMedical.Monitoring.PerformanceCounterHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.Monitoring;

public static class PerformanceCounterHelper
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(nameof (PerformanceCounterHelper), "$Rev$");

  public static PerformanceCounter CreateSimpleCounter(
    string counterName,
    string counterHelp,
    PerformanceCounterType counterType,
    string categoryName)
  {
    return new PerformanceCounter()
    {
      CounterName = counterName,
      ReadOnly = false,
      RawValue = 0,
      CategoryName = categoryName
    };
  }

  public static void Create()
  {
    CounterCreationDataCollection creationDataCollection = new CounterCreationDataCollection();
  }

  public static PerformanceCounterCategory CreateCategory(string categoryName, string categoryHelp)
  {
    PerformanceCounterCategory category = (PerformanceCounterCategory) null;
    new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, Environment.MachineName, categoryName).Assert();
    CounterCreationDataCollection counterData = new CounterCreationDataCollection();
    if (PerformanceCounterCategory.Exists(categoryName))
    {
      try
      {
        category = ((IEnumerable<PerformanceCounterCategory>) PerformanceCounterCategory.GetCategories(Environment.MachineName)).Where<PerformanceCounterCategory>((Func<PerformanceCounterCategory, bool>) (c => string.Compare(c.CategoryName, categoryName) == 0)).FirstOrDefault<PerformanceCounterCategory>();
      }
      catch (ArgumentNullException ex)
      {
        PerformanceCounterHelper.Logger.Error((Exception) ex, "Failure while getting performance counter category {0}.", (object) categoryName);
      }
    }
    else
    {
      new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, Environment.MachineName, categoryName).Assert();
      category = PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.SingleInstance, counterData);
    }
    return category;
  }
}
