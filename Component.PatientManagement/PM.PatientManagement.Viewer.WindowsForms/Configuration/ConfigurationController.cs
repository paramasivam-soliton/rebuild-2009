// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.Configuration.ConfigurationController
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationController : Controller<PatientConfigManager, ConfigurationEditor>
{
  public ConfigurationController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = PatientConfigManager.Instance;
    this.View = new ConfigurationEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(PatientManagementPermissions.ConfigurePatientManagement));
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Editing).SetTrigger(Triggers.ModificationPerformed).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new ChangeValueCommand()))).ApplyTo(this.StateMachine);
  }
}
