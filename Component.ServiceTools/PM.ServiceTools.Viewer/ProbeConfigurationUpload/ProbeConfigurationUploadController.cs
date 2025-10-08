// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeConfigurationUpload.ProbeConfigurationUploadController
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Transition;
using PathMedical.ServiceTools.WindowsForms.ProbeConfigurator;
using PathMedical.Type1077;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeConfigurationUpload;

internal class ProbeConfigurationUploadController : 
  Controller<Type1077Manager, ProbeConfiguratorEditor>
{
  public ProbeConfigurationUploadController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = Type1077Manager.Instance;
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).AddFromTo(States.Viewing).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
  }
}
