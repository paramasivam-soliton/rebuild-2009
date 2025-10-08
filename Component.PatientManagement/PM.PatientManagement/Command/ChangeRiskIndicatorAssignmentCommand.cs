// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.ChangeRiskIndicatorAssignmentCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class ChangeRiskIndicatorAssignmentCommand : CommandBase, IUndoableCommand, ICommand
{
  private PatientManager patientManager;
  private IView view;
  private Dictionary<RiskIndicator, RiskIndicatorValueType> rememberedRiskIndicatorsAndValues;

  public ChangeRiskIndicatorAssignmentCommand(PatientManager patientManager, IView view)
  {
    if (patientManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientManager));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("View");
    this.patientManager = patientManager;
    this.view = view;
  }

  public override void Execute()
  {
    this.rememberedRiskIndicatorsAndValues = this.patientManager.SelectedRiskIndicators.ToDictionary<RiskIndicator, RiskIndicator, RiskIndicatorValueType>((Func<RiskIndicator, RiskIndicator>) (ri => ri), (Func<RiskIndicator, RiskIndicatorValueType>) (ri => ri.PatientRiskIndicatorValue));
    this.view.CopyUIToModel();
    this.patientManager.ReportRiskIndicatorModification();
  }

  public void Undo()
  {
    this.rememberedRiskIndicatorsAndValues.ForEach<KeyValuePair<RiskIndicator, RiskIndicatorValueType>>((Action<KeyValuePair<RiskIndicator, RiskIndicatorValueType>>) (pair => pair.Key.PatientRiskIndicatorValue = pair.Value));
    this.patientManager.SelectedRiskIndicators = (ICollection<RiskIndicator>) this.rememberedRiskIndicatorsAndValues.Keys;
    this.patientManager.ReportRiskIndicatorModification();
  }
}
