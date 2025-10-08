// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.AccuLinkDataExchangeConfiguration
// Assembly: PM.DataExchange.AccuLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45ECC252-AAEE-4427-BE78-DF1068000887
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.dll

#nullable disable
namespace PathMedical.DataExchange.AccuLink;

public class AccuLinkDataExchangeConfiguration
{
  public string ExportFolder { get; set; }

  public string ImportFolder { get; set; }

  public bool? ImportRiskIndicators { get; set; }

  public bool? ExportRiskIndicators { get; set; }

  public bool? ActivateImportedRiskIndicators { get; set; }

  public bool? ImportPredefinedComments { get; set; }

  public bool? ActivateImportedPredefinedComments { get; set; }

  public bool? ExportPredefinedComments { get; set; }

  public bool? ImportUsers { get; set; }

  public bool? ExportUsers { get; set; }
}
