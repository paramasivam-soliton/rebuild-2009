// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.SetUnknownRiskIndicatorsToNoCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class SetUnknownRiskIndicatorsToNoCommand : CommandBase, IUndoableCommand, ICommand
{
  private PatientManager patientManager;
  private Dictionary<RiskIndicator, RiskIndicatorValueType> rememberedRiskIndicatorsAndValues;

  public SetUnknownRiskIndicatorsToNoCommand(PatientManager patientManager)
  {
    this.patientManager = patientManager != null ? patientManager : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientManager));
  }

  public override void Execute()
  {
    this.rememberedRiskIndicatorsAndValues = this.patientManager.SetUnkownRiskIndicatorsToNo();
  }

  public void Undo()
  {
    this.rememberedRiskIndicatorsAndValues.ForEach<KeyValuePair<RiskIndicator, RiskIndicatorValueType>>((Action<KeyValuePair<RiskIndicator, RiskIndicatorValueType>>) (pair => pair.Key.PatientRiskIndicatorValue = pair.Value));
    this.patientManager.ReportRiskIndicatorModification();
  }
}
