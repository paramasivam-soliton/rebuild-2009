// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.Comment
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using System;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

public class Comment
{
  public virtual string BaseText { get; set; }

  public virtual string Text { get; set; }

  public virtual string Examiner { get; protected set; }

  public virtual DateTime? CreationDate { get; set; }
}
