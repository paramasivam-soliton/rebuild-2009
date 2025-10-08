// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.RiskIndicatorManagement.PatientRiskIndicator
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DataExchange.Description;
using System;

#nullable disable
namespace PathMedical.PatientManagement.RiskIndicatorManagement;

[DataExchangeRecord("PatientRiskIndicator")]
public class PatientRiskIndicator
{
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DataExchangeColumn]
  public Guid PatientId { get; set; }

  [DataExchangeColumn]
  public Guid RiskIndicatorId { get; set; }

  [DataExchangeColumn]
  public ushort PatientRiskIndicatorValue { get; set; }
}
