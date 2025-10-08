// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Facility
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

[DebuggerDisplay("Name={Name}, Code={Code}")]
[DataExchangeRecord]
public class Facility
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn("FacilityId")]
  public Guid Id { get; set; }

  [DbGeneratedColumn]
  public Guid? SiteId { get; set; }

  [DbReferenceRelation("SiteId")]
  public Site Site { get; set; }

  [DbIntermediateTableRelation("FacilityId", "LocationTypeId", "FacilityLocationTypeAssociation")]
  [DataExchangeColumn("Locations")]
  public List<LocationType> LocationTypes { get; set; }

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

  public string LongName
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.Name);
      if (!string.IsNullOrEmpty(this.Code))
        stringBuilder.AppendFormat(" [{0}]", (object) this.Code);
      if (this.Site != null)
        stringBuilder.AppendFormat(" ({0})", (object) this.Site.Name);
      return stringBuilder.ToString();
    }
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is Facility facility && this.Id.Equals(facility.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
