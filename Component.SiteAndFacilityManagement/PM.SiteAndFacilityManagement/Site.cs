// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Site
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
public class Site
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn("SiteId")]
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

  [DbBackReferenceRelation("Id", "SiteId")]
  public List<Facility> Facilities { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? Inactive { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj) => obj is Site site && this.Id.Equals(site.Id);

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.Name);
    if (!string.IsNullOrEmpty(this.Code))
      stringBuilder.AppendFormat(" [{0}]", (object) this.Code);
    return stringBuilder.ToString();
  }
}
