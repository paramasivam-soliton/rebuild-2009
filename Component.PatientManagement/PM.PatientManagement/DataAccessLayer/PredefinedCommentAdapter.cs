// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.DataAccessLayer.PredefinedCommentAdapter
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.ResourceManager;
using System;

#nullable disable
namespace PathMedical.PatientManagement.DataAccessLayer;

public class PredefinedCommentAdapter : AdapterBase<PredefinedComment>
{
  private ResourceAdapter resourceAdapter;

  public PredefinedCommentAdapter(DBScope scope)
    : base(scope)
  {
    this.resourceAdapter = new ResourceAdapter(scope);
  }

  public override void Store(PredefinedComment predefinedComment)
  {
    if (predefinedComment == null)
      return;
    if (string.IsNullOrEmpty(predefinedComment.CommentLanguageId))
      predefinedComment.CommentLanguageId = Guid.NewGuid().ToString();
    foreach (ResourceTranslation commentTranslation in predefinedComment.CommentTranslationList)
      commentTranslation.ResourceName = predefinedComment.CommentLanguageId;
    base.Store(predefinedComment);
  }
}
