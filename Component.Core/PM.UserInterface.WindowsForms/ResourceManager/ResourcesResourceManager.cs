// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourcesResourceManager
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResourcesResourceManager : System.Resources.ResourceManager
{
  private string baseNameField;
  private string extension = "resources";

  protected string Extension
  {
    get => this.extension;
    set => this.extension = value;
  }

  protected virtual void Initialize(string baseName, Assembly assembly)
  {
    this.baseNameField = baseName;
    this.ResourceSets = new Hashtable();
  }

  public ResourcesResourceManager(string baseName, Assembly assembly)
  {
    this.Initialize(baseName, assembly);
  }

  public ResourcesResourceManager(string baseName) => this.Initialize(baseName, (Assembly) null);

  public ResourcesResourceManager(Type resourceType)
  {
    this.Initialize(resourceType.Name, resourceType.Assembly);
  }

  protected override ResourceSet InternalGetResourceSet(
    CultureInfo cultureInfo,
    bool createIfNotExists,
    bool tryParents)
  {
    if (this.ResourceSets.Contains((object) cultureInfo.Name))
      return this.ResourceSets[(object) cultureInfo.Name] as ResourceSet;
    ResourceSet resourceSet = (ResourceSet) new ResourcesResourceSet(this.baseNameField, cultureInfo, this.extension);
    this.ResourceSets.Add((object) cultureInfo.Name, (object) resourceSet);
    return resourceSet;
  }
}
