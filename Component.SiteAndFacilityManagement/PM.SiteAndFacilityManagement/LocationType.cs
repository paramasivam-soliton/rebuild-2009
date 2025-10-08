// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.LocationType
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

[DebuggerDisplay("Name={Name}, Code={Code}")]
[DataExchangeRecord("Location")]
public class LocationType
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn("LocationId")]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Name { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Description { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Code { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? Inactive { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is LocationType locationType && this.Id.Equals(locationType.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();

  public override string ToString() => this.Name;
}
