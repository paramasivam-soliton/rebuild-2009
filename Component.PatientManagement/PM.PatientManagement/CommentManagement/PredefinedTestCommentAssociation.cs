// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.PredefinedTestCommentAssociation
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

[DbTable(Name = "PredefinedCommentAssociation")]
public class PredefinedTestCommentAssociation : PredefinedCommentAssociation
{
  [DbBackReferenceRelation("PredefinedCommentId", "Id")]
  public PredefinedComment PredefinedComment { get; set; }

  public override EntityLoadInformation LoadInformation { get; set; }
}
