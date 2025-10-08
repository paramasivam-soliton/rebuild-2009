// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.DataAccessLayer.FreeTextCommentAdapter
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.PatientManagement.CommentManagement;

#nullable disable
namespace PathMedical.PatientManagement.DataAccessLayer;

public class FreeTextCommentAdapter(DBScope scope) : AdapterBase<FreeTextComment>(scope)
{
}
