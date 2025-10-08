// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.TransitionBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Login;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.Automaton.Transition;

[DebuggerDisplay("State: {State} Guards: {Guards} Trigger: {Trigger.Name} Command: {Command.Name}")]
internal class TransitionBase : ITransition
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (TransitionBase), "$Rev: 1456 $");
  private static readonly ILogger PermissionLogger = LogFactory.Instance.Create("Permission");

  public TransitionBase(
    Trigger trigger,
    State state,
    IGuard guard,
    ICommand commandToExecute,
    State nextState)
    : this(trigger, state, new GuardComposition(new IGuard[1]
    {
      guard
    }), commandToExecute, nextState)
  {
  }

  public TransitionBase(
    Trigger trigger,
    State state,
    IGuard guard,
    ICommand commandToExecute,
    State nextState,
    params ITransition[] transitions)
    : this(trigger, state, new GuardComposition(new IGuard[1]
    {
      guard
    }), commandToExecute, nextState, transitions)
  {
  }

  public TransitionBase(
    Trigger trigger,
    State state,
    IGuard guard,
    ICommand commandToExecute,
    params ITransition[] transitions)
    : this(trigger, state, new GuardComposition(new IGuard[1]
    {
      guard
    }), commandToExecute, state, transitions)
  {
  }

  public TransitionBase(
    Trigger trigger,
    State state,
    GuardComposition guards,
    ICommand commandToExecute,
    State nextState,
    params ITransition[] transitions)
  {
    if (trigger == (Trigger) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (trigger));
    if (state == (State) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (state));
    if (nextState == (State) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (nextState));
    this.Trigger = trigger;
    this.State = state;
    this.Guards = guards;
    this.Command = commandToExecute;
    this.NextState = nextState;
    this.FollowUpTransitions = new List<ITransition>((IEnumerable<ITransition>) transitions);
  }

  public Trigger Trigger { get; private set; }

  public ICommand Command { get; private set; }

  public State NextState { get; private set; }

  public List<ITransition> FollowUpTransitions { get; private set; }

  public bool IsAccessGranted
  {
    get
    {
      bool isAccessGranted;
      if (this.Guards != null)
      {
        isAccessGranted = this.Guards.GetGuards<PermissionGuard>().All<IGuard>((Func<IGuard, bool>) (g => g.Execute(new TriggerEventArgs(this.Trigger))));
        if (TransitionBase.PermissionLogger.IsInfoEnabled)
        {
          string str = LoginManager.Instance.LoggedInUserData != null ? LoginManager.Instance.LoggedInUserData.FullName : "Unknown";
          TransitionBase.PermissionLogger.Info($"Requested access for {this.Trigger} has been {(isAccessGranted ? (object) "granted" : (object) "rejected")} for {str}.");
        }
      }
      else
        isAccessGranted = true;
      return isAccessGranted;
    }
  }

  public State State { get; private set; }

  public GuardComposition Guards { get; private set; }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("TransitionBase [{0}] [{1}] [Guards: {2}] [NextState: {3}] [Command: {4}]", (object) this.State, (object) this.Trigger, (object) this.Guards, (object) this.NextState, (object) this.Command);
    if (this.FollowUpTransitions != null && this.FollowUpTransitions.Count > 0)
    {
      foreach (ITransition followUpTransition in this.FollowUpTransitions)
      {
        stringBuilder.Append(" [FurtherTransition ");
        stringBuilder.Append((object) followUpTransition);
        stringBuilder.Append("]");
      }
    }
    return stringBuilder.ToString();
  }
}
