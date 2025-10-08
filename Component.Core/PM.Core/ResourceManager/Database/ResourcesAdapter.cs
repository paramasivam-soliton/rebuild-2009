// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.Database.ResourcesAdapter
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Properties;
using PathMedical.SystemConfiguration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;

#nullable disable
namespace PathMedical.ResourceManager.Database;

public class ResourcesAdapter
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ResourcesAdapter), "$Rev: 1284 $");
  private static readonly object LockObject = new object();
  private static Dictionary<string, Dictionary<string, Dictionary<string, object>>> cache;

  private static Dictionary<string, Dictionary<string, Dictionary<string, object>>> Cache
  {
    get
    {
      if (ResourcesAdapter.cache == null)
      {
        lock (ResourcesAdapter.LockObject)
        {
          if (ResourcesAdapter.cache == null)
            ResourcesAdapter.FillCache();
        }
      }
      return ResourcesAdapter.cache;
    }
  }

  private static void FillCache()
  {
    try
    {
      using (DBScope scope = new DBScope(TransactionLevel.Independent))
      {
        AdapterBase<ResourceTranslation> adapterBase = new AdapterBase<ResourceTranslation>(scope);
        ResourcesAdapter.cache = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        foreach (ResourceTranslation resourceTranslation in (IEnumerable<ResourceTranslation>) adapterBase.All)
        {
          string key = resourceTranslation.Culture ?? CultureInfo.InvariantCulture.Name;
          Dictionary<string, object> dictionary = ResourcesAdapter.cache.GetOrCreateValue<string, Dictionary<string, Dictionary<string, object>>>(key).GetOrCreateValue<string, Dictionary<string, object>>(resourceTranslation.ResourceSet.ToUpperInvariant());
          object obj;
          if (resourceTranslation.ResourceType != null && resourceTranslation.ResourceType.Equals("System.Drawing.Bitmap"))
          {
            using (MemoryStream memoryStream = new MemoryStream(resourceTranslation.ResourceImage))
              obj = (object) new Bitmap((Stream) memoryStream);
          }
          else
            obj = (object) resourceTranslation.ResourceText;
          dictionary.AddOrOverwriteEntry<string, object>(resourceTranslation.ResourceName.ToUpperInvariant(), obj);
        }
        scope.Complete();
      }
    }
    catch (DbException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.ResourcesAdapter_ReadFailure), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      ResourcesAdapter.Logger.Error(ex, "Failure while loading resources from the database.");
      throw;
    }
  }

  public object GetResource(string resourceSetName, string resourceName)
  {
    return this.GetResource(resourceSetName, resourceName, (CultureInfo) null);
  }

  public object GetResource(string resourceSetName, string resourceName, CultureInfo cultureInfo)
  {
    if (resourceSetName == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (resourceSetName));
    if (resourceName == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (resourceName));
    if (cultureInfo == null)
      cultureInfo = SystemConfigurationManager.Instance.BaseLanguage;
    resourceSetName = resourceSetName.ToUpperInvariant();
    resourceName = resourceName.ToUpperInvariant();
    object resource = (object) null;
    Dictionary<string, Dictionary<string, object>> dictionary1;
    Dictionary<string, object> dictionary2;
    if (ResourcesAdapter.Cache.TryGetValue(cultureInfo.Name, out dictionary1) && dictionary1.TryGetValue(resourceSetName, out dictionary2))
      dictionary2.TryGetValue(resourceName, out resource);
    if (resource != null)
      return resource;
    return cultureInfo != CultureInfo.InvariantCulture ? this.GetResource(resourceSetName, resourceName, cultureInfo.Parent) : (object) null;
  }

  public object GetResourceByName(string resourceName, CultureInfo cultureInfo)
  {
    if (resourceName == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (resourceName));
    if (cultureInfo == null)
      cultureInfo = SystemConfigurationManager.Instance.BaseLanguage;
    Dictionary<string, Dictionary<string, object>> dictionary;
    if (ResourcesAdapter.Cache.TryGetValue(cultureInfo.Name, out dictionary))
    {
      foreach (string key in dictionary.Keys)
      {
        object resource = this.GetResource(key, resourceName, cultureInfo);
        if (resource != null)
          return resource;
      }
    }
    return cultureInfo != CultureInfo.InvariantCulture ? this.GetResourceByName(resourceName, cultureInfo.Parent) : (object) null;
  }

  public Hashtable Get(CultureInfo cultureToLoad, string baseNameField)
  {
    if (cultureToLoad == null)
      cultureToLoad = SystemConfigurationManager.Instance.BaseLanguage;
    Dictionary<string, Dictionary<string, object>> enumerable;
    if (!ResourcesAdapter.Cache.TryGetValue(cultureToLoad.Name, out enumerable))
      return new Hashtable();
    if (!string.IsNullOrEmpty(baseNameField))
    {
      Dictionary<string, object> d;
      return !enumerable.TryGetValue(baseNameField, out d) ? new Hashtable() : new Hashtable((IDictionary) d);
    }
    Dictionary<string, object> mergedResourceSets = new Dictionary<string, object>();
    enumerable.ForEach<KeyValuePair<string, Dictionary<string, object>>>((Action<KeyValuePair<string, Dictionary<string, object>>>) (pair => pair.Value.ForEach<KeyValuePair<string, object>>((Action<KeyValuePair<string, object>>) (pair2 => mergedResourceSets.AddOrOverwriteEntry<string, object>(pair2.Key, pair2.Value)))));
    return new Hashtable((IDictionary) mergedResourceSets);
  }
}
