// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.User
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PathMedical.UserProfileManagement;

[DebuggerDisplay("LoginName={LoginName}, IsActive={IsActive}")]
[DataExchangeRecord("User")]
public class User : ICloneable
{
  public User() => this.IsActive = true;

  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string LoginName { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string LoginId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Forename { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Surname { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Password { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? PasswordSalt { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Language { get; set; }

  public string LanguageName
  {
    get
    {
      if (!string.IsNullOrEmpty(this.Language))
      {
        try
        {
          return new CultureInfo(this.Language).DisplayName;
        }
        catch (ArgumentException ex)
        {
        }
      }
      return string.Empty;
    }
  }

  [DbColumn]
  [DataExchangeColumn]
  public bool IsActive { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? LockTimestamp { get; set; }

  [DbColumn]
  public int? FailedLogins { get; set; }

  [DataExchangeColumn]
  public Guid? ProfileId
  {
    get => this.Profile == null ? new Guid?() : new Guid?(this.Profile.Id);
    set
    {
      if (!value.HasValue || UserProfileManager.Instance.Profiles == null)
        return;
      this.Profile = UserProfileManager.Instance.Profiles.FirstOrDefault<UserProfile>((Func<UserProfile, bool>) (p =>
      {
        Guid id = p.Id;
        Guid? nullable = value;
        return nullable.HasValue && id == nullable.GetValueOrDefault();
      }));
    }
  }

  [DbReferenceRelation("ProfileID")]
  [DataExchangeColumn]
  public UserProfile Profile { get; set; }

  [DbRelationValue("IsOnInstrument")]
  [DataExchangeColumn]
  public bool UserOnInstrumentValue { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public string FullName
  {
    get
    {
      return string.Join(", ", ((IEnumerable<string>) new string[2]
      {
        this.Forename,
        this.Surname
      }).Where<string>((Func<string, bool>) (str => !string.IsNullOrEmpty(str))).ToArray<string>());
    }
  }

  public object Clone() => this.MemberwiseClone();

  public override bool Equals(object obj) => obj is User user && this.Id.Equals(user.Id);

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
