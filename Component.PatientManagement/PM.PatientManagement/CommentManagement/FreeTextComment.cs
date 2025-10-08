// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.FreeTextComment
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.UserProfileManagement;
using System;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

[DbTable(HasHistory = true)]
[DebuggerDisplay("Comment {Id} {Comment}")]
[DataExchangeRecord("FreeTextComment")]
[Serializable]
public class FreeTextComment : Comment
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid ReferenceId { get; set; }

  [DbColumn(Name = "Text")]
  [DataExchangeColumn]
  public override string BaseText { get; set; }

  public override string Text
  {
    get => this.BaseText;
    set => this.BaseText = value;
  }

  [DbColumn]
  [DataExchangeColumn]
  public override DateTime? CreationDate { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? UserAccountId { get; set; }

  public override string Examiner
  {
    get
    {
      if (this.UserAccountId.HasValue)
      {
        User user = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
        {
          Guid id = u.Id;
          Guid? userAccountId = this.UserAccountId;
          return userAccountId.HasValue && id == userAccountId.GetValueOrDefault();
        }));
        if (user != null)
          return user.LoginName;
      }
      return string.Empty;
    }
  }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is FreeTextComment freeTextComment && this.Id.Equals(freeTextComment.Id) && this.ReferenceId.Equals(freeTextComment.ReferenceId) && this.CreationDate.Equals((object) freeTextComment.CreationDate);
  }

  public override int GetHashCode()
  {
    return this.GetType().GetHashCode() + this.Id.GetHashCode() + this.ReferenceId.GetHashCode() + this.CreationDate.GetHashCode();
  }
}
