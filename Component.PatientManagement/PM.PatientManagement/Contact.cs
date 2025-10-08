// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Contact
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PathMedical.PatientManagement;

[DbTable(HasHistory = true)]
[DebuggerDisplay("Contact {Forename1} {Surname} {ContactRelationType} {Id}")]
public class Contact
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbRelationValue("PatientId")]
  [DataExchangeColumn]
  public Guid PatientId { get; set; }

  [DbRelationValue("RelationToPatient")]
  [DataExchangeColumn]
  public ContactRelationType ContactRelationType { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Title { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Forename1 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Forename2 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Initial { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Surname { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string AlsoKnownAs { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? DateOfBirth { get; set; }

  [DataExchangeColumn]
  public DateTime? CalculatedDateOfBirth { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public PathMedical.PatientManagement.Gender? Gender { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Height { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Weight { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string BirthLocation { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string LanguageCode { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string NationalityCode { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string SocialSecurityNumber { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string EMail1 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string EMail2 { get; set; }

  [DbColumn]
  public string Freetext1 { get; set; }

  [DbColumn]
  public string Freetext2 { get; set; }

  [DbColumn]
  public string Freetext3 { get; set; }

  [DbColumn]
  public string Freetext4 { get; set; }

  [DbColumn]
  public string Freetext5 { get; set; }

  [DbColumn]
  public string Freetext6 { get; set; }

  [DbColumn]
  public string Freetext7 { get; set; }

  [DbColumn]
  public string Freetext8 { get; set; }

  [DbColumn]
  public string Freetext9 { get; set; }

  [DbColumn]
  public string Freetext10 { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  [DbIntermediateTableRelation("ContactId", "ContactAddressId", "ContactToContactAddressAssociation", "AddressType", 1, HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public Address PrimaryAddress { get; set; }

  [DbIntermediateTableRelation("ContactId", "ContactAddressId", "ContactToContactAddressAssociation", "AddressType", 2, HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public List<Address> AdditionalAddresses { get; set; }

  public string FullName
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = ", ";
      bool flag = false;
      if (!string.IsNullOrEmpty(this.Surname))
      {
        stringBuilder.Append(this.Surname);
        flag = true;
      }
      if (!string.IsNullOrEmpty(this.Forename1))
      {
        if (flag)
          stringBuilder.Append(str);
        stringBuilder.Append(this.Forename1);
        str = " ";
        flag = true;
      }
      if (!string.IsNullOrEmpty(this.Forename2))
      {
        if (flag)
          stringBuilder.Append(str);
        stringBuilder.Append(this.Forename2);
        str = " ";
        flag = true;
      }
      if (!string.IsNullOrEmpty(this.Initial))
      {
        if (flag)
          stringBuilder.Append(str);
        stringBuilder.Append(this.Forename2);
      }
      return stringBuilder.ToString();
    }
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj) => obj is Contact contact && this.Id.Equals(contact.Id);

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
