// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.ResourceTranslation
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;

#nullable disable
namespace PathMedical.ResourceManager;

[DbTable(Name = "ResourceSets")]
public class ResourceTranslation
{
  [DbPrimaryKeyColumn]
  public int ResourceId { get; set; }

  [DbColumn(Name = "ResourceSetName")]
  public string ResourceSet { get; set; }

  [DbColumn]
  public string ResourceName { get; set; }

  [DbColumn]
  public byte[] ResourceImage { get; set; }

  [DbColumn]
  public string ResourceType { get; set; }

  [DbColumn]
  public string Culture { get; set; }

  [DbColumn]
  public string ResourceText { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is ResourceTranslation resourceTranslation && this.ResourceId.Equals(resourceTranslation.ResourceId);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.ResourceId.GetHashCode();
}
