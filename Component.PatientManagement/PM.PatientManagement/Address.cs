// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Address
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.PatientManagement;

[DbTable(Name = "ContactAddress", HasHistory = true)]
[DebuggerDisplay("Address {Address1} {City} {AddressType} {Id}")]
public class Address
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbRelationValue("AddressType")]
  [DataExchangeColumn]
  public AddressType AddressType { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Address1 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Address2 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Zip { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string City { get; set; }

  public string ZipCity => $"{this.Zip} {this.City}";

  [DbColumn]
  [DataExchangeColumn]
  public string State { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Country { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Phone { get; set; }

  [DbColumn]
  public string CellPhone { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Fax { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj) => obj is Address address && this.Id.Equals(address.Id);

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
