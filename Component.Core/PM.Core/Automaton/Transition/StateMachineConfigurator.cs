// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.StateMachineConfigurator
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Transition;

public class StateMachineConfigurator
{
  public StateMachineConfigurator(StateMachine stateMachine, IModel model, IView view)
  {
    if (stateMachine == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stateMachine));
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.StateMachine = stateMachine;
    this.Model = model;
    this.View = view;
    if (!(model is ISingleEditingModel))
      return;
    this.SaveCommand = (ICommand) new CommandComposition(new ICommand[5]
    {
      (ICommand) new CopyUIToModelCommand(this.View),
      (ICommand) new PathMedical.Automaton.Command.SaveCommand(this.Model as ISingleEditingModel),
      (ICommand) new ChangeViewModeCommand(this.View, ViewModeType.Editing),
      (ICommand) new SetSavedCommand(),
      (ICommand) new EnableRevertCommand(true)
    });
    this.DiscardCommand = (ICommand) new DiscardModificationCommand(this.Model as ISingleEditingModel, this.View);
    this.HandleModificationGuard = new HandleModificationGuard(this.View, this.SaveCommand, this.DiscardCommand);
  }

  protected StateMachine StateMachine { get; set; }

  protected IModel Model { get; set; }

  protected IView View { get; set; }

  public ICommand SaveCommand { get; protected set; }

  public ICommand DiscardCommand { get; protected set; }

  public HandleModificationGuard HandleModificationGuard { get; protected set; }

  public TransitionDefinition SupportValidationAndSaving()
  {
    IsViewValidGuard isViewValidGuard = new IsViewValidGuard(this.View);
    IsNotGuard isViewInvalidGuard = new IsNotGuard((IGuard) isViewValidGuard);
    return this.SupportValidationAndSaving((IGuard) isViewValidGuard, (IGuard) isViewInvalidGuard);
  }

  public TransitionDefinition SupportValidationAndSaving(
    IGuard isViewValidGuard,
    IGuard isViewInvalidGuard)
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding, States.Editing).SetTrigger(Triggers.Save).SetGuard(isViewValidGuard).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      this.SaveCommand,
      (ICommand) new RefreshModelCommand(this.Model)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    string messageText = ComponentResourceManagement.Instance.ResourceManager.GetString("DataNotValidCorrectFirst");
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(Triggers.Save).SetGuard(isViewInvalidGuard).SetCommand((ICommand) new DisplayMessageCommand(this.View, messageText)).ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportDeleting<TEntity>() where TEntity : class, new()
  {
    return this.SupportDeleting<TEntity>((ICommand) new DeleteCommand(this.Model as ISingleEditingModel), ComponentResourceManagement.Instance.ResourceManager.GetString("ReallyDeleteRecord"));
  }

  public TransitionDefinition SupportDeleting<TEntity>(
    ICommand deleteCommand,
    string confirmQuestion)
    where TEntity : class, new()
  {
    GuardComposition transitionGuard = new GuardComposition(Array.Empty<IGuard>());
    if (this.Model is ISingleSelectionModel<TEntity>)
      transitionGuard.Add((IGuard) new IsOneItemSelectedGuard<TEntity>(this.Model as ISingleSelectionModel<TEntity>));
    else if (this.Model is IMultiSelectionModel<TEntity>)
      transitionGuard.Add((IGuard) new IsOneItemFocusedGuard<TEntity>(this.Model as IMultiSelectionModel<TEntity>));
    transitionGuard.Add((IGuard) new AskSimpleQuestionGuard(this.View, confirmQuestion, AnswerOptionType.YesNo, AnswerType.Yes));
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Viewing).SetTrigger(Triggers.Delete).SetGuard((IGuard) transitionGuard).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new DeleteCommand(this.Model as ISingleEditingModel),
      (ICommand) new RefreshModelCommand(this.Model)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportEditingWithDifferentModule<TEntity>(
    Type moduleType,
    Trigger additionalTriggerToFire)
    where TEntity : class, new()
  {
    return this.SupportEditing<TEntity>((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new ChangeModuleCommand(moduleType, additionalTriggerToFire),
      (ICommand) new CleanUpViewCommand(this.View)
    }));
  }

  public TransitionDefinition SupportEditing<TEntity>(ICommand editCommand) where TEntity : class, new()
  {
    GuardComposition transitionGuard = new GuardComposition(Array.Empty<IGuard>());
    if (this.Model is ISingleSelectionModel<TEntity>)
      transitionGuard.Add((IGuard) new IsOneItemSelectedGuard<TEntity>(this.Model as ISingleSelectionModel<TEntity>));
    else if (this.Model is IMultiSelectionModel<TEntity>)
      transitionGuard.Add((IGuard) new IsOneItemFocusedGuard<TEntity>(this.Model as IMultiSelectionModel<TEntity>));
    transitionGuard.Add((IGuard) this.HandleModificationGuard);
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(Triggers.Edit).SetGuard((IGuard) transitionGuard).SetCommand(editCommand);
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportAdding()
  {
    return this.SupportAdding((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new ChangeViewModeCommand(this.View, ViewModeType.Adding),
      (ICommand) new AddNewItemCommand(this.Model as ISingleEditingModel)
    }));
  }

  public TransitionDefinition SupportAddingWithDifferentModule(Type moduleType)
  {
    return this.SupportAddingWithDifferentModule(moduleType, (Trigger) null);
  }

  public TransitionDefinition SupportAddingWithDifferentModule(
    Type moduleType,
    Trigger additionalTriggerToFire)
  {
    CommandComposition transitionCommand = new CommandComposition(new ICommand[3]
    {
      (ICommand) new AddNewItemCommand(this.Model as ISingleEditingModel),
      (ICommand) new ChangeModuleCommand(moduleType, additionalTriggerToFire),
      (ICommand) new CleanUpViewCommand(this.View)
    });
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Editing, States.Editing).AddFromTo(States.Viewing, States.Viewing).SetTrigger(Triggers.Add).SetGuard((IGuard) this.HandleModificationGuard).SetCommand((ICommand) transitionCommand);
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportAdding(ICommand addCommand)
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Editing, States.Adding).AddFromTo(States.Viewing, States.Adding).SetTrigger(Triggers.Add).SetGuard((IGuard) this.HandleModificationGuard).SetCommand(addCommand);
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public void SupportUndoing()
  {
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(Triggers.Undo).SetGuard((IGuard) new IsUndoableGuard()).SetCommand((ICommand) new UndoCommand()).ApplyTo(this.StateMachine);
  }

  public TransitionDefinition SupportViewMode(EnterViewModeOptions enterViewModeOptions)
  {
    GuardComposition transitionGuard = new GuardComposition(Array.Empty<IGuard>())
    {
      enterViewModeOptions.PermissionGuard
    };
    if (enterViewModeOptions.PreventedByAnyViewMode != null && enterViewModeOptions.PreventedByAnyViewMode.Count > 0)
    {
      foreach (ViewModeType preventingViewMode in enterViewModeOptions.PreventedByAnyViewMode)
      {
        IsViewModeAllowedGuard modeAllowedGuard = new IsViewModeAllowedGuard(this.StateMachine, enterViewModeOptions.ViewMode, preventingViewMode);
        transitionGuard.Add((IGuard) modeAllowedGuard);
      }
    }
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Idle, enterViewModeOptions.State).SetTrigger(Triggers.StartModule).SetGuard((IGuard) transitionGuard).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand(this.View),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new RefreshModelCommand(this.Model),
      (ICommand) new ChangeViewModeCommand(this.View, enterViewModeOptions.ViewMode)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportSuspending()
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Adding, States.Idle).AddFromTo(States.Editing, States.Idle).AddFromTo(States.Viewing, States.Idle).SetTrigger(Triggers.CloseModule).SetGuard((IGuard) this.HandleModificationGuard);
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public void SupportRevertModification()
  {
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(Triggers.RevertModifications).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new RevertModificationCommand(this.Model as ISingleEditingModel),
      (ICommand) new ClearUndoHistoryCommand()
    })).ApplyTo(this.StateMachine);
  }

  public void SupportGoBack()
  {
    new TransitionDefinition().AddFromTo(States.Idle).AddFromTo(States.Viewing).AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(Triggers.GoBack).SetGuard((IGuard) this.HandleModificationGuard).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand(this.View),
      (ICommand) new RefreshModelCommand(this.Model),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new GoBackCommand()
    })).ApplyTo(this.StateMachine);
  }

  public TransitionDefinition SupportSwitchModule(Trigger trigger, Type moduleType)
  {
    return this.SupportSwitchModule(trigger, moduleType, (Type) null);
  }

  public TransitionDefinition SupportSwitchModule(
    Trigger trigger,
    Type moduleType,
    Type hostingAppComponentType)
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(trigger).SetGuard((IGuard) this.HandleModificationGuard).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand(this.View),
      (ICommand) new RefreshModelCommand(this.Model),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new ChangeModuleCommand(moduleType, (Trigger) null, hostingAppComponentType)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportSwitchModule(SwitchModuleOptions switchModuleOptions)
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(switchModuleOptions.Trigger).SetGuard((IGuard) new GuardComposition(new IGuard[2]
    {
      (IGuard) this.HandleModificationGuard,
      switchModuleOptions.CreateGuard()
    })).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand(this.View),
      (ICommand) new RefreshModelCommand(this.Model),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new ChangeModuleCommand(switchModuleOptions.ModuleType)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public TransitionDefinition SupportStartAssistant(Trigger trigger, Type moduleType)
  {
    TransitionDefinition transitionDefinition = new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(trigger).SetGuard((IGuard) this.HandleModificationGuard).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand(this.View),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new StartAssistantCommand(moduleType),
      (ICommand) new RefreshModelCommand(this.Model)
    }));
    transitionDefinition.ApplyTo(this.StateMachine);
    return transitionDefinition;
  }

  public void SupportGettingHelp()
  {
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(Triggers.Help).SetCommand((ICommand) new HelpCommand()).ApplyTo(this.StateMachine);
  }

  public void SupportChangeSelection<TEntity>() where TEntity : class, new()
  {
    this.SupportChangeSelection<TEntity>(Triggers.ChangeSelection);
  }

  public void SupportChangeSelection<TEntity>(Trigger trigger) where TEntity : class, new()
  {
    CommandComposition changeSelectionCommand = new CommandComposition(new ICommand[3]
    {
      (ICommand) new ChangeSelectionCommand<TEntity>(this.Model),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new EnableRevertCommand(true)
    });
    this.SupportChangeSelection(trigger, (IGuard) this.HandleModificationGuard, (ICommand) changeSelectionCommand);
  }

  public void SupportChangeSelectionSimple<TEntity>() where TEntity : class, new()
  {
    this.SupportChangeSelectionSimple<TEntity>(Triggers.ChangeSelection);
  }

  public void SupportChangeSelectionSimple<TEntity>(Trigger trigger) where TEntity : class, new()
  {
    ChangeSelectionCommand<TEntity> changeSelectionCommand = new ChangeSelectionCommand<TEntity>(this.Model);
    this.SupportChangeSelection(trigger, (IGuard) null, (ICommand) changeSelectionCommand);
  }

  protected void SupportChangeSelection(
    Trigger trigger,
    IGuard guard,
    ICommand changeSelectionCommand)
  {
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding, States.Editing).AddFromTo(States.Viewing).SetTrigger(trigger).SetGuard(guard).SetCommand(changeSelectionCommand).ApplyTo(this.StateMachine);
  }

  public void SupportChangeCurrent<TEntity>() where TEntity : class, new()
  {
    this.SupportChangeCurrent<TEntity>(Triggers.ChangeCurrent);
  }

  public void SupportChangeCurrent<TEntity>(Trigger trigger) where TEntity : class, new()
  {
    CommandComposition changeSelectionCommand = new CommandComposition(new ICommand[3]
    {
      (ICommand) new ChangeCurrentItemCommand<TEntity>(this.Model as IMultiSelectionModel<TEntity>),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new EnableRevertCommand(true)
    });
    this.SupportChangeSelection(trigger, (IGuard) this.HandleModificationGuard, (ICommand) changeSelectionCommand);
  }

  public void SupportChangeCurrentSimple<TEntity>() where TEntity : class, new()
  {
    ChangeCurrentItemCommand<TEntity> changeSelectionCommand = new ChangeCurrentItemCommand<TEntity>(this.Model as IMultiSelectionModel<TEntity>);
    this.SupportChangeSelection(Triggers.ChangeCurrent, (IGuard) null, (ICommand) changeSelectionCommand);
  }

  protected void SupportChangeCurrentItem(
    Trigger trigger,
    IGuard guard,
    ICommand changeCurrentItemCommand)
  {
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding, States.Editing).AddFromTo(States.Viewing).SetTrigger(trigger).SetGuard(guard).SetCommand(changeCurrentItemCommand).ApplyTo(this.StateMachine);
  }
}
