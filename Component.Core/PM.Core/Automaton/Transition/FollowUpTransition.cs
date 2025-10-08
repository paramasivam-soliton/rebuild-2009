// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.FollowUpTransition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using System;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PathMedical.Automaton.Transition;

[DebuggerDisplay("State: {State} Guards: {Guards.Name} Trigger: {Trigger.Name}")]
public class FollowUpTransition : ITransition
{
  public FollowUpTransition(IGuard guard, Trigger trigger)
    : this(new GuardComposition(new IGuard[1]{ guard }), trigger)
  {
  }

  public FollowUpTransition(GuardComposition guards, Trigger trigger)
  {
    if (guards == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("guard");
    this.Trigger = !(trigger == (Trigger) null) ? trigger : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (trigger));
    this.Guards = guards;
  }

  public Trigger Trigger { get; private set; }

  public GuardComposition Guards { get; private set; }

  public State State { get; private set; }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Transtition [Guards: {0}] [Trigger: {1}]", (object) this.Guards, (object) this.Trigger);
    return stringBuilder.ToString();
  }
}
