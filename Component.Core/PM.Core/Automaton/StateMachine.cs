// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.StateMachine
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Properties;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Name: {Name}, State: {State}")]
public class StateMachine
{
  private static readonly ILogger Logger = LogFactory.Instance.Create("ControllerFlow");
  private Collection<TransitionBase> registeredTransitionsList;
  private State state;

  public State State
  {
    get => this.state;
    private set
    {
      State state = this.state;
      this.state = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new StateMachineChangedEventArgs(state, this.state));
    }
  }

  public string Name { get; private set; }

  private string GetStateMachineInfo(TriggerEventArgs e)
  {
    return $"{this.Name} {this.state} Operation Id: {TriggerRequestScope.CurrentId}";
  }

  public event EventHandler<StateMachineChangedEventArgs> Changed;

  public StateMachine(string name, State initializationState)
  {
    if (string.IsNullOrEmpty(name))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (name));
    if (initializationState == (State) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (initializationState));
    this.Name = name;
    this.registeredTransitionsList = new Collection<TransitionBase>();
    this.State = initializationState;
  }

  public void RegisterTransition(
    State activeState,
    Trigger trigger,
    IGuard guard,
    ICommand commandToExecute)
  {
    this.RegisterTransition(activeState, trigger, new GuardComposition(new IGuard[1]
    {
      guard
    }), commandToExecute, activeState);
  }

  public void RegisterTransition(
    State activeState,
    Trigger trigger,
    ICommand commandToExecute,
    State transitionState,
    params ITransition[] followUpTransitions)
  {
    this.RegisterTransition(activeState, trigger, new GuardComposition((IGuard[]) null), commandToExecute, transitionState, followUpTransitions);
  }

  public void RegisterTransition(
    State activeState,
    Trigger trigger,
    IGuard guard,
    ICommand commandToExecute,
    State transitionState,
    params ITransition[] followUpTransitions)
  {
    this.RegisterTransition(activeState, trigger, new GuardComposition(new IGuard[1]
    {
      guard
    }), commandToExecute, transitionState, followUpTransitions);
  }

  public void RegisterTransition(
    State activeState,
    Trigger trigger,
    GuardComposition guards,
    ICommand commandToExecute,
    State transitionState,
    params ITransition[] followUpTransitions)
  {
    if (trigger == (Trigger) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (trigger));
    if (activeState == (State) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (activeState));
    if (this.registeredTransitionsList == null)
      this.registeredTransitionsList = new Collection<TransitionBase>();
    TransitionBase transitionToRegister = (TransitionBase) null;
    try
    {
      transitionToRegister = new TransitionBase(trigger, activeState, guards, commandToExecute, transitionState, followUpTransitions);
      if (this.registeredTransitionsList.Count > 0 && this.registeredTransitionsList.Count > 0)
      {
        IEnumerable<TransitionBase> source = this.registeredTransitionsList.Where<TransitionBase>((Func<TransitionBase, bool>) (t => t.State.Equals((object) transitionToRegister.State) && t.Trigger.Equals((object) transitionToRegister.Trigger) && t.Guards.Equals((object) transitionToRegister.Guards)));
        if (source.Count<TransitionBase>() > 0)
          this.registeredTransitionsList.Remove(source.First<TransitionBase>());
      }
      this.registeredTransitionsList.Add(transitionToRegister);
    }
    catch (ArgumentException ex)
    {
      StateMachine.Logger.Error((System.Exception) ex, "An error occurred while registering the transition {0}.", (object) transitionToRegister);
      throw ExceptionFactory.Instance.CreateException<StateMachineException>(Resources.StateMachine_Exception_RegisterTransitionError, (System.Exception) ex);
    }
  }

  public void Execute(TriggerEventArgs e)
  {
    if (e == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (e));
    using (new TriggerRequestScope())
    {
      StateMachine.Logger.Debug("[{0}] Got request to execute trigger. [{1}] [{2}]", (object) this.GetStateMachineInfo(e), (object) e.RequestedTrigger, (object) this.State);
      State state = this.State;
      try
      {
        if (!(this.FindAllowedTransition(this.FindTransitionsForTrigger(e), e) is TransitionBase allowedTransition1))
        {
          StateMachine.Logger.Debug("[{0}] No transition to execute trigger found. [{1}]", (object) this.GetStateMachineInfo(e), (object) e.RequestedTrigger);
        }
        else
        {
          this.ExecuteTransition(allowedTransition1, e);
          ITransition allowedTransition = this.FindAllowedTransition((IEnumerable<ITransition>) allowedTransition1.FollowUpTransitions, e);
          if (allowedTransition == null)
            return;
          this.ExecuteFollowUpTransition(allowedTransition, e);
        }
      }
      catch (ModelException ex)
      {
        StateMachine.Logger.Error((System.Exception) ex, "Error while executing the state machine. {0}", (object) ex.Message);
        e.Cancel = true;
        StateMachine.Logger.Debug("[{0}] Switching back from {1} to {2} due to an error while executing the state machine.", (object) this.GetStateMachineInfo(e), (object) this.State, (object) state);
        this.State = state;
        throw;
      }
      catch (System.Exception ex)
      {
        StateMachine.Logger.Error(ex, "Unexpected error while executing the state machine.");
        e.Cancel = true;
        StateMachine.Logger.Debug("[{0}] Switching back from {1} to {2} due to an error while executing the state machine.", (object) this.GetStateMachineInfo(e), (object) this.State, (object) state);
        this.State = state;
        throw;
      }
    }
  }

  private IEnumerable<ITransition> FindTransitionsForTrigger(TriggerEventArgs e)
  {
    List<TransitionBase> list = this.registeredTransitionsList.Where<TransitionBase>((Func<TransitionBase, bool>) (t => t.State.Equals((object) this.State) && t.Trigger.Equals((object) e.RequestedTrigger))).ToList<TransitionBase>();
    list.ForEach((Action<TransitionBase>) (t => StateMachine.Logger.Debug("[{0}] Found possible candidates for next transition: [{1}].", (object) this.GetStateMachineInfo(e), (object) t)));
    return list.OfType<ITransition>();
  }

  private ITransition FindAllowedTransition(
    IEnumerable<ITransition> transitions,
    TriggerEventArgs e)
  {
    if (transitions == null)
      return (ITransition) null;
    ITransition allowedTransition1 = transitions.FirstOrDefault<ITransition>((Func<ITransition, bool>) (t => t.Guards == null || t.Guards.Count == 0));
    if (allowedTransition1 != null)
    {
      StateMachine.Logger.Debug("[{0}] TransitionBase has no guard that observes the transition candidate: [{1}].", (object) this.GetStateMachineInfo(e), (object) allowedTransition1);
      return allowedTransition1;
    }
    ITransition allowedTransition2;
    try
    {
      allowedTransition2 = transitions.FirstOrDefault<ITransition>((Func<ITransition, bool>) (t => !e.Cancel && t.Guards.Execute(e)));
      if (allowedTransition2 != null)
        StateMachine.Logger.Debug("[{0}] Guards {1} of transition candidate agree to transit from {2} to {3}.", (object) this.GetStateMachineInfo(e), (object) allowedTransition2.Guards, (object) this.State, (object) allowedTransition2);
    }
    catch (System.Exception ex)
    {
      StateMachine.Logger.Error(ex, "[{0}] When searching for a transition candidate a guard threw an exception.", (object) this.GetStateMachineInfo(e));
      throw;
    }
    return allowedTransition2;
  }

  private void ExecuteTransition(TransitionBase transitionCandidate, TriggerEventArgs e)
  {
    StateMachine.Logger.Debug("[{0}] Begining transition. [{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
    if (transitionCandidate.NextState != (State) null && !this.State.Equals((object) transitionCandidate.NextState))
    {
      StateMachine.Logger.Info("[{0}] State machine switches to {1}", (object) this.GetStateMachineInfo(e), (object) transitionCandidate.NextState);
      this.State = transitionCandidate.NextState;
    }
    if (transitionCandidate.Command == null)
    {
      StateMachine.Logger.Debug("[{0}] Finalizing transition without executing a command. [{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
    }
    else
    {
      try
      {
        StateMachine.Logger.Info("[{0}] Starting command of transition. [{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
        transitionCandidate.Command.TriggerEventArgs = e;
        CommandManager.Instance.Execute(transitionCandidate.Command);
        StateMachine.Logger.Info("[{0}] Completed command of transition successfully.[{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
      }
      catch (System.Exception ex)
      {
        StateMachine.Logger.Error(ex, "[{0}] Exception caught while executing command of transition. [{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
        throw;
      }
      finally
      {
        StateMachine.Logger.Info("[{0}] Finalized transition. [{1}]", (object) this.GetStateMachineInfo(e), (object) transitionCandidate);
      }
    }
  }

  private void ExecuteFollowUpTransition(ITransition followUpTransition, TriggerEventArgs e)
  {
    if (followUpTransition.State != (State) null)
    {
      StateMachine.Logger.Info("[{0}] State machine switches from {1} to {2}", (object) this.GetStateMachineInfo(e), (object) this.State, (object) followUpTransition.State);
      this.State = followUpTransition.State;
    }
    if (!(followUpTransition is FollowUpTransition followUpTransition1))
      return;
    try
    {
      TriggerEventArgs e1 = new TriggerEventArgs(followUpTransition1.Trigger, e.TriggerContext);
      this.Execute(e1);
      if (!e1.Cancel)
        return;
      e.Cancel = true;
    }
    catch (System.Exception ex)
    {
      StateMachine.Logger.Error(ex, "[{0}] Exception caught while executing FollowUpTransition. [{1}] [{2}]", (object) this.GetStateMachineInfo(e), (object) followUpTransition1.Trigger, (object) this.State);
      throw;
    }
  }

  public override string ToString() => $"StateMachine [Name: {this.Name}] {this.state}]";

  public bool IsAccessGranted(Trigger trigger)
  {
    return this.registeredTransitionsList.Any<TransitionBase>((Func<TransitionBase, bool>) (t => t.Trigger == trigger && t.IsAccessGranted));
  }

  public List<Trigger> GetAllowedTriggers()
  {
    return this.registeredTransitionsList.Where<TransitionBase>((Func<TransitionBase, bool>) (t => t.State == this.State && t.IsAccessGranted)).Select<TransitionBase, Trigger>((Func<TransitionBase, Trigger>) (t => t.Trigger)).Distinct<Trigger>().ToList<Trigger>();
  }

  public Trigger GeCurrentTrigger()
  {
    if (this.state == States.Editing)
      return Triggers.StartModule;
    if (this.state == States.Adding)
      return Triggers.Add;
    return this.state == States.Deleting ? Triggers.Delete : (Trigger) null;
  }

  internal IEnumerable<TransitionBase> GetAllowedTranisition<T>() where T : ICommand
  {
    List<TransitionBase> allowedTranisition = new List<TransitionBase>();
    foreach (TransitionBase transitionBase in (IEnumerable<TransitionBase>) this.registeredTransitionsList.Where<TransitionBase>((Func<TransitionBase, bool>) (t => t.State == this.State && t.IsAccessGranted)).Distinct<TransitionBase>().ToList<TransitionBase>())
    {
      if (transitionBase != null)
      {
        if (transitionBase.Command is CommandComposition)
        {
          if ((transitionBase.Command as CommandComposition).Commands.OfType<T>().Count<T>() > 0)
            allowedTranisition.Add(transitionBase);
        }
        else if (transitionBase.Command is T)
          allowedTranisition.Add(transitionBase);
      }
    }
    return (IEnumerable<TransitionBase>) allowedTranisition;
  }
}
