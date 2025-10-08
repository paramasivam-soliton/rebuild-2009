// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.PredefinedCommentAssociation
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

[DebuggerDisplay("Predefined Comment Association")]
[DataExchangeRecord("PredefinedCommentAssociation")]
public class PredefinedCommentAssociation
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid AssociationId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid ReferenceId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public virtual Guid PredefinedCommentId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? CreationDate { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? UserAccountId { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public virtual EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is PredefinedCommentAssociation commentAssociation && this.ReferenceId.Equals(commentAssociation.ReferenceId) && this.PredefinedCommentId.Equals(commentAssociation.PredefinedCommentId);
  }

  public override int GetHashCode()
  {
    return this.GetType().GetHashCode() + this.ReferenceId.GetHashCode() + this.PredefinedCommentId.GetHashCode();
  }
}
