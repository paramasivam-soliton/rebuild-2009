// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.RiskIndicatorManagement.RiskIndicator
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.HistoryTracker;
using PathMedical.PatientManagement.Properties;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.RiskIndicatorManagement;

[DbTable(HasHistory = true)]
[DebuggerDisplay("{Name} : {PatientRiskIndicatorValue}")]
[DataExchangeRecord("RiskIndicator")]
public class RiskIndicator : ICloneable
{
  public RiskIndicator()
  {
    this.NameTranslationList = new List<ResourceTranslation>();
    this.DescriptionTranslationList = new List<ResourceTranslation>();
  }

  [DbPrimaryKeyColumn]
  [DataExchangeColumn("RiskIndicatorId")]
  public Guid Id { get; set; }

  [DbColumn]
  public string NameLanguageId { get; set; }

  [DbBackReferenceRelation("NameLanguageId", "ResourceName")]
  public List<ResourceTranslation> NameTranslationList { get; set; }

  [DataExchangeColumn]
  public string Name
  {
    get => this.GetName(SystemConfigurationManager.Instance.BaseLanguage);
    set => this.SetName(SystemConfigurationManager.Instance.BaseLanguage, value);
  }

  public string LocalizedName => this.GetName(SystemConfigurationManager.Instance.CurrentLanguage);

  public string SafeName => this.GetSafeName(SystemConfigurationManager.Instance.CurrentLanguage);

  [DbColumn]
  public string DescriptionLanguageID { get; set; }

  [DbBackReferenceRelation("DescriptionLanguageID", "ResourceName")]
  public List<ResourceTranslation> DescriptionTranslationList { get; set; }

  public string Description
  {
    get => this.GetDescription(SystemConfigurationManager.Instance.BaseLanguage);
    set => this.SetDescription(SystemConfigurationManager.Instance.BaseLanguage, value);
  }

  public string LocalizedDescription
  {
    get => this.GetDescription(SystemConfigurationManager.Instance.CurrentLanguage);
  }

  public string SafeDescription
  {
    get => this.GetSafeDescription(SystemConfigurationManager.Instance.CurrentLanguage);
  }

  [DbColumn]
  [HistoryField(ResourceNameTranslation = "RiskIndicatorIsActive", ResourceValueTranslation = "RiskIndicatorIsActiveValue")]
  public bool? IsActive { get; set; }

  [DbColumn]
  [HistoryField(ResourceNameTranslation = "RiskIndicatorPreventScreening", ResourceValueTranslation = "RiskIndicatorPreventScreeningValue")]
  public bool? PreventScreening { get; set; }

  [DbColumn(IsHistoryRelevant = false)]
  public int? OrderNumber { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  [DbColumn]
  public DateTime? Archived { get; set; }

  [DbRelationValue("RiskIndicatorValue")]
  [DataExchangeColumn]
  public RiskIndicatorValueType PatientRiskIndicatorValue { get; set; }

  public Bitmap PatientRiskIndicatorImage
  {
    get
    {
      switch (this.PatientRiskIndicatorValue)
      {
        case RiskIndicatorValueType.No:
          return (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorNo");
        case RiskIndicatorValueType.Yes:
          return (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorYes");
        default:
          return (Bitmap) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorUnknown");
      }
    }
  }

  public CultureInfo TranslationCulture => RiskIndicatorManager.Instance.TranslationCulture;

  public string TranslationName
  {
    get => this.GetName(RiskIndicatorManager.Instance.TranslationCulture);
    set => this.SetName(RiskIndicatorManager.Instance.TranslationCulture, value);
  }

  public string TranslationDescription
  {
    get => this.GetDescription(RiskIndicatorManager.Instance.TranslationCulture);
    set => this.SetDescription(RiskIndicatorManager.Instance.TranslationCulture, value);
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public object Clone() => this.MemberwiseClone();

  public void SetName(CultureInfo cultureInfo, string name)
  {
    ResourceTranslation resourceTranslation1 = (ResourceTranslation) null;
    if (this.NameTranslationList != null && cultureInfo != null)
    {
      IEnumerable<ResourceTranslation> source = this.NameTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureInfo.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        resourceTranslation1 = source.First<ResourceTranslation>();
    }
    if (resourceTranslation1 != null)
    {
      resourceTranslation1.ResourceText = name;
    }
    else
    {
      if (this.NameTranslationList == null)
        this.NameTranslationList = new List<ResourceTranslation>();
      ResourceTranslation resourceTranslation2 = new ResourceTranslation();
      if (cultureInfo != null)
        resourceTranslation2.Culture = cultureInfo.Name;
      resourceTranslation2.ResourceSet = "RiskIndicatorList";
      resourceTranslation2.ResourceText = name;
      this.NameTranslationList.Add(resourceTranslation2);
    }
  }

  public string GetName(CultureInfo cultureInfo)
  {
    string name = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.NameTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source = this.NameTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        name = source.First<ResourceTranslation>().ResourceText;
    }
    return name;
  }

  private string GetSafeName(CultureInfo cultureInfo)
  {
    string safeName = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.NameTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source = this.NameTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source == null || source.Count<ResourceTranslation>() == 0)
        source = this.NameTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == SystemConfigurationManager.Instance.BaseLanguage.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        safeName = source.First<ResourceTranslation>().ResourceText;
    }
    return safeName;
  }

  public void SetDescription(CultureInfo cultureInfo, string description)
  {
    ResourceTranslation resourceTranslation = (ResourceTranslation) null;
    if (this.DescriptionTranslationList != null && cultureInfo != null)
    {
      IEnumerable<ResourceTranslation> source = this.DescriptionTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureInfo.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        resourceTranslation = source.First<ResourceTranslation>();
    }
    if (resourceTranslation != null)
    {
      resourceTranslation.ResourceText = description;
    }
    else
    {
      if (this.DescriptionTranslationList == null)
        this.DescriptionTranslationList = new List<ResourceTranslation>();
      if (cultureInfo == null)
        return;
      this.DescriptionTranslationList.Add(new ResourceTranslation()
      {
        Culture = cultureInfo.Name,
        ResourceSet = "RiskIndicatorList",
        ResourceText = description
      });
    }
  }

  public string GetDescription(CultureInfo cultureInfo)
  {
    string description = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.DescriptionTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source = this.DescriptionTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        description = source.First<ResourceTranslation>().ResourceText;
    }
    return description;
  }

  public string GetSafeDescription(CultureInfo cultureInfo)
  {
    string safeDescription = string.Empty;
    CultureInfo cultureToQuery = cultureInfo ?? SystemConfigurationManager.Instance.BaseLanguage;
    if (this.DescriptionTranslationList != null)
    {
      IEnumerable<ResourceTranslation> source = this.DescriptionTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == cultureToQuery.Name));
      if (source == null || source.Count<ResourceTranslation>() == 0)
        source = this.DescriptionTranslationList.Where<ResourceTranslation>((Func<ResourceTranslation, bool>) (t => t.Culture == SystemConfigurationManager.Instance.BaseLanguage.Name));
      if (source != null && source.Count<ResourceTranslation>() > 0)
        safeDescription = source.First<ResourceTranslation>().ResourceText;
    }
    return safeDescription;
  }

  public override bool Equals(object obj)
  {
    return obj is RiskIndicator riskIndicator && this.Id.Equals(riskIndicator.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();

  public override string ToString() => $"RiskIndicator {this.Id}";
}
