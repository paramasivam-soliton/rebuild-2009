// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.TransitionDefinition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Automaton.Transition;

public class TransitionDefinition
{
  private readonly List<State[]> fromToStates = new List<State[]>();
  private ICommand command;
  private GuardComposition guard;
  private Trigger trigger;

  public TransitionDefinition AddFromTo(State fromToState)
  {
    return this.AddFromTo(fromToState, fromToState);
  }

  public TransitionDefinition AddFromTo(State fromState, State toState)
  {
    this.fromToStates.Add(new State[2]{ fromState, toState });
    return this;
  }

  public TransitionDefinition SetTrigger(Trigger transitionTrigger)
  {
    this.trigger = transitionTrigger;
    return this;
  }

  public TransitionDefinition SetGuard(IGuard transitionGuard)
  {
    if (this.guard == null)
      this.guard = new GuardComposition(new IGuard[1]
      {
        transitionGuard
      });
    else
      this.guard.Add(transitionGuard);
    return this;
  }

  public TransitionDefinition SetCommand(ICommand transitionCommand)
  {
    this.command = transitionCommand;
    return this;
  }

  public TransitionDefinition NeedsAnyPermission(params Guid[] permissionIds)
  {
    PermissionGuardComposition guardComposition = new PermissionGuardComposition();
    foreach (Guid permissionId in permissionIds)
    {
      if (!permissionId.Equals(Guid.Empty))
      {
        PermissionGuard permissionGuard = new PermissionGuard(permissionId, true);
        guardComposition.Add(permissionGuard);
      }
    }
    if (this.guard == null)
      this.guard = new GuardComposition(Array.Empty<IGuard>());
    if (guardComposition.Count > 0)
      this.guard.Add((IGuard) guardComposition);
    return this;
  }

  public void ApplyTo(StateMachine stateMachine)
  {
    foreach (State[] fromToState in this.fromToStates)
      stateMachine.RegisterTransition(fromToState[0], this.trigger, this.guard, this.command, fromToState[1]);
  }
}
