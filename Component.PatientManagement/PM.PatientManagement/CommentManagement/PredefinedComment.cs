// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.PredefinedComment
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.HistoryTracker;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

[DataExchangeRecord("PredefinedComment")]
public class PredefinedComment : Comment, ICloneable
{
  [DbPrimaryKeyColumn(Name = "Id")]
  [DataExchangeColumn]
  public Guid PredefinedCommentId { get; set; }

  [DataExchangeColumn]
  public override string BaseText
  {
    get => this.GetComment(SystemConfigurationManager.Instance.BaseLanguage, false);
    set => this.SetComment(SystemConfigurationManager.Instance.BaseLanguage, value);
  }

  public string LocalizedText
  {
    get => this.GetComment(SystemConfigurationManager.Instance.CurrentLanguage, true);
    set => this.BaseText = value;
  }

  public override string Text
  {
    get => this.GetSafeComment(SystemConfigurationManager.Instance.CurrentLanguage);
  }

  [DbColumn]
  public string CommentLanguageId { get; set; }

  [DbBackReferenceRelation("CommentLanguageId", "ResourceName")]
  public List<ResourceTranslation> CommentTranslationList { get; set; }

  [DbColumn]
  [HistoryField(ResourceNameTranslation = "PredefinedCommentIsActive", ResourceValueTranslation = "PredefinedCommentIsActiveValue")]
  [DataExchangeColumn]
  public bool? IsActive { get; set; }

  [DbRelationValue("AssociationID")]
  [DataExchangeColumn]
  public Guid? AssociationId { get; set; }

  [DbRelationValue("ReferenceID")]
  [DataExchangeColumn]
  public Guid? ReferenceId { get; set; }

  [DbRelationValue("UserAccountID")]
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

  [DbRelationValue("CreationDate")]
  [DataExchangeColumn]
  public override DateTime? CreationDate { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public PredefinedComment()
  {
    this.CommentTranslationList = new List<ResourceTranslation>();
    this.IsActive = new bool?(true);
  }

  public void SetComment(CultureInfo cultureInfo, string translatedComment)
  {
    ResourceTranslation resourceTranslation1 = (ResourceTranslation) null;
    if (this.CommentTranslationList != null && cultureInfo != null)
    {
      IEnumerable<ResourceTranslation> source = this.CommentTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureInfo.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        resourceTranslation1 = source.First<ResourceTranslation>();
    }
    if (resourceTranslation1 != null)
    {
      resourceTranslation1.ResourceText = translatedComment;
    }
    else
    {
      if (this.CommentTranslationList == null)
        this.CommentTranslationList = new List<ResourceTranslation>();
      ResourceTranslation resourceTranslation2 = new ResourceTranslation();
      if (cultureInfo != null)
        resourceTranslation2.Culture = cultureInfo.Name;
      resourceTranslation2.ResourceSet = "CommentList";
      resourceTranslation2.ResourceText = translatedComment;
      this.CommentTranslationList.Add(resourceTranslation2);
    }
  }

  public string GetComment(CultureInfo cultureInfo, bool fallBack)
  {
    string comment = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.CommentTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source = this.CommentTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        comment = source.First<ResourceTranslation>().ResourceText;
      else if (fallBack && cultureToQuery != SystemConfigurationManager.Instance.BaseLanguage)
        comment = this.GetComment(SystemConfigurationManager.Instance.BaseLanguage, false);
    }
    return comment;
  }

  private string GetSafeComment(CultureInfo cultureInfo)
  {
    string safeComment = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.CommentTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source1 = this.CommentTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source1 != null && source1.Count<ResourceTranslation>() > 0)
        safeComment = source1.First<ResourceTranslation>().ResourceText;
      if (string.IsNullOrEmpty(safeComment))
      {
        IEnumerable<ResourceTranslation> source2 = this.CommentTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == SystemConfigurationManager.Instance.BaseLanguage.Name));
        if (source2 != null && source2.Count<ResourceTranslation>() > 0)
          safeComment = source2.First<ResourceTranslation>().ResourceText;
      }
    }
    return safeComment;
  }

  public CultureInfo TranslationCulture => CommentManager.Instance.TranslationCulture;

  public string TranslationName
  {
    get => this.GetComment(CommentManager.Instance.TranslationCulture, false);
    set => this.SetComment(CommentManager.Instance.TranslationCulture, value);
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is PredefinedComment predefinedComment && this.PredefinedCommentId.Equals(predefinedComment.PredefinedCommentId) && this.AssociationId.Equals((object) predefinedComment.AssociationId) && this.CreationDate.Equals((object) predefinedComment.CreationDate);
  }

  public override int GetHashCode()
  {
    return this.GetType().GetHashCode() + this.PredefinedCommentId.GetHashCode() + this.AssociationId.GetHashCode() + this.CreationDate.GetHashCode();
  }

  public object Clone() => this.MemberwiseClone();
}
