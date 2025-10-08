// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.FollowUpState
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton.Transition;

[DebuggerDisplay("State: {State} Guards: {Guards.Name}")]
public class FollowUpState : ITransition
{
  public FollowUpState(State state)
  {
    this.State = !(state == (State) null) ? state : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (state));
  }

  public FollowUpState(IGuard guard, State state)
    : this(state)
  {
    GuardComposition guardComposition;
    if (guard == null)
      guardComposition = new GuardComposition(Array.Empty<IGuard>());
    else
      guardComposition = new GuardComposition(new IGuard[1]
      {
        guard
      });
    this.Guards = guardComposition;
  }

  public FollowUpState(GuardComposition guards, State state)
    : this(state)
  {
    this.Guards = guards ?? new GuardComposition(Array.Empty<IGuard>());
  }

  public GuardComposition Guards { get; private set; }

  public State State { get; private set; }

  public override string ToString()
  {
    return $"FollowUpState [State: {this.State}] [Guards: {this.Guards}]";
  }
}
