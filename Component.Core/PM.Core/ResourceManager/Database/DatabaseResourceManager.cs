// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.Database.DatabaseResourceManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

#nullable disable
namespace PathMedical.ResourceManager.Database;

public sealed class DatabaseResourceManager : ComponentResourceManager
{
  private string baseNameField;
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (DatabaseResourceManager));

  public DatabaseResourceManager(string baseName) => this.Initialize(baseName);

  public DatabaseResourceManager(Type resourceType) => this.Initialize(resourceType.Name);

  private void Initialize(string baseName)
  {
    this.baseNameField = baseName;
    this.ResourceSets = new Hashtable();
  }

  protected override ResourceSet InternalGetResourceSet(
    CultureInfo cultureInfo,
    bool createIfNotExists,
    bool tryParents)
  {
    ResourceSet resourceSet;
    if (this.ResourceSets.Contains((object) cultureInfo.Name))
    {
      resourceSet = this.ResourceSets[(object) cultureInfo.Name] as ResourceSet;
    }
    else
    {
      resourceSet = (ResourceSet) new DatabaseResourceSet(this.baseNameField, cultureInfo);
      this.ResourceSets.Add((object) cultureInfo.Name, (object) resourceSet);
    }
    return resourceSet;
  }

  public override string GetString(string name) => this.GetString(name, (CultureInfo) null);

  public override string GetString(string name, CultureInfo culture)
  {
    string empty = base.GetString(name, culture);
    if (empty == null)
    {
      empty = string.Empty;
      DatabaseResourceManager.Logger.Warning("Resource string with name {0} for culture {1} not found!", (object) name, culture != null ? (object) culture.Name : (object) "null");
    }
    return empty;
  }
}
