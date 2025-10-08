// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientManagementTriggers
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.PatientManagement;

public static class PatientManagementTriggers
{
  public static readonly Trigger ConfigureRiskIndicator = new Trigger(nameof (ConfigureRiskIndicator));
  public static readonly Trigger ConfigureComment = new Trigger(nameof (ConfigureComment));
  public static readonly Trigger AssignUnknownRiskToNoRisk = new Trigger(nameof (AssignUnknownRiskToNoRisk));
  public static readonly Trigger SelectTest = new Trigger(nameof (SelectTest));
  public static readonly Trigger SelectRiskFactor = new Trigger(nameof (SelectRiskFactor));
  public static readonly Trigger SelectComment = new Trigger(nameof (SelectComment));
  public static readonly Trigger AddComment = new Trigger(nameof (AddComment));
  public static readonly Trigger DeleteComment = new Trigger(nameof (DeleteComment));
  public static readonly Trigger GetPatientDataFromInstrumentData = new Trigger(nameof (GetPatientDataFromInstrumentData));
  public static readonly Trigger SendPatientToInstrument = new Trigger(nameof (SendPatientToInstrument));
  public static readonly Trigger PrintSinglePatientReport = new Trigger(nameof (PrintSinglePatientReport));
  public static readonly Trigger PrintBasicOverallReport = new Trigger(nameof (PrintBasicOverallReport));
  public static readonly Trigger PrintBasicReportTeoae = new Trigger(nameof (PrintBasicReportTeoae));
  public static readonly Trigger PrintBasicReportDpoae = new Trigger(nameof (PrintBasicReportDpoae));
  public static readonly Trigger PrintBasicReportAbr = new Trigger(nameof (PrintBasicReportAbr));
  public static readonly Trigger PrintDetailOverallReport = new Trigger(nameof (PrintDetailOverallReport));
  public static readonly Trigger PrintDetailReportTeoae = new Trigger(nameof (PrintDetailReportTeoae));
  public static readonly Trigger PrintDetailReportDpoae = new Trigger(nameof (PrintDetailReportDpoae));
  public static readonly Trigger PrintDetailReportAbr = new Trigger(nameof (PrintDetailReportAbr));
  public static readonly Trigger DeleteTest = new Trigger(nameof (DeleteTest));
}
