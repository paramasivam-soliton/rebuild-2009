// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsViewModeAllowedGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsViewModeAllowedGuard : GuardBase
{
  private readonly StateMachine stateMachine;
  private readonly ViewModeType viewModeToApply;
  private readonly ViewModeType preventingViewMode;

  public IsViewModeAllowedGuard(
    StateMachine stateMachine,
    ViewModeType viewModeToApply,
    ViewModeType preventingViewMode)
  {
    this.stateMachine = stateMachine != null ? stateMachine : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stateMachine));
    this.viewModeToApply = viewModeToApply;
    this.preventingViewMode = preventingViewMode;
  }

  public override bool Execute(TriggerEventArgs e)
  {
    bool flag = true;
    if (this.stateMachine != null)
    {
      foreach (KeyValuePair<ICommand, TransitionBase> keyValuePair in this.stateMachine.GetAllowedTranisition<ChangeViewModeCommand>().ToDictionary<TransitionBase, ICommand, TransitionBase>((Func<TransitionBase, ICommand>) (t => t.Command), (Func<TransitionBase, TransitionBase>) (t => t)))
      {
        if (keyValuePair.Key is CommandComposition && (keyValuePair.Key as CommandComposition).Commands.Where<ICommand>((Func<ICommand, bool>) (c => c is ChangeViewModeCommand)).Cast<ChangeViewModeCommand>().Where<ChangeViewModeCommand>((Func<ChangeViewModeCommand, bool>) (c => c.ViewMode == this.preventingViewMode)).ToList<ChangeViewModeCommand>().Count > 0 && keyValuePair.Value != null)
          flag = !keyValuePair.Value.Guards.Execute(e);
      }
    }
    return flag;
  }
}
