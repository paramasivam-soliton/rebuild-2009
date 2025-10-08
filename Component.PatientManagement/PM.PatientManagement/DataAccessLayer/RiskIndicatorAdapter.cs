// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.DataAccessLayer.RiskIndicatorAdapter
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.ResourceManager;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.DataAccessLayer;

public class RiskIndicatorAdapter : AdapterBase<RiskIndicator>
{
  private ResourceAdapter resourceAdapter;

  public RiskIndicatorAdapter(DBScope scope)
    : base(scope)
  {
    this.resourceAdapter = new ResourceAdapter(scope);
  }

  public override void Store(RiskIndicator riskIndicator)
  {
    if (riskIndicator == null)
      return;
    if (string.IsNullOrEmpty(riskIndicator.NameLanguageId))
      riskIndicator.NameLanguageId = Guid.NewGuid().ToString();
    if (string.IsNullOrEmpty(riskIndicator.DescriptionLanguageID))
      riskIndicator.DescriptionLanguageID = Guid.NewGuid().ToString();
    foreach (ResourceTranslation nameTranslation in riskIndicator.NameTranslationList)
      nameTranslation.ResourceName = riskIndicator.NameLanguageId;
    foreach (ResourceTranslation descriptionTranslation in riskIndicator.DescriptionTranslationList)
      descriptionTranslation.ResourceName = riskIndicator.DescriptionLanguageID;
    base.Store(riskIndicator);
  }

  public override void Store(ICollection<RiskIndicator> riskIndicators)
  {
    foreach (RiskIndicator riskIndicator in (IEnumerable<RiskIndicator>) riskIndicators)
      this.Store(riskIndicator);
  }

  public override void Delete(RiskIndicator entity)
  {
    foreach (ResourceTranslation nameTranslation in entity.NameTranslationList)
      this.resourceAdapter.Delete(nameTranslation);
    foreach (ResourceTranslation descriptionTranslation in entity.DescriptionTranslationList)
      this.resourceAdapter.Delete(descriptionTranslation);
    base.Delete(entity);
  }
}
