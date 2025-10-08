// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowserController
// Assembly: PM.SystemConfiguration.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D6114BC-3807-4057-97EB-FB0AA393F8AD
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.DataExchange;
using PathMedical.SystemConfiguration.Core;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser;

public class ComponentBrowserController : Controller<ComponentBrowserManager, PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser>
{
  public ComponentBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = ComponentBrowserManager.Instance;
    this.View = new PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SystemConfigurationPermissions.ViewConfiguration, SystemConfigurationPermissions.EditConfiguration).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SystemConfigurationPermissions.EditConfiguration));
    machineConfigurator.SupportUndoing();
    foreach (IApplicationComponent applicationComponent in SystemConfigurationManager.Instance.ApplicationComponents.Where<IApplicationComponent>((Func<IApplicationComponent, bool>) (ac => ac.ConfigurationModuleTypes != null && ac.ConfigurationModuleTypes.Length != 0)))
    {
      foreach (Type configurationModuleType in applicationComponent.ConfigurationModuleTypes)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(configurationModuleType);
        machineConfigurator.SupportSwitchModule(new Trigger(applicationComponentModule.Id.ToString()), configurationModuleType, typeof (SystemConfigurationComponent)).NeedsAnyPermission(SystemConfigurationPermissions.ViewConfiguration, SystemConfigurationPermissions.EditConfiguration);
      }
    }
    foreach (ISupportDataExchangeModules dataExchangeModules in SystemConfigurationManager.Instance.Plugins.OfType<ISupportDataExchangeModules>().Where<ISupportDataExchangeModules>((Func<ISupportDataExchangeModules, bool>) (p => p.ConfigurationModule != null)).OrderBy<ISupportDataExchangeModules, string>((Func<ISupportDataExchangeModules, string>) (p => p.Name)).ToArray<ISupportDataExchangeModules>())
      machineConfigurator.SupportSwitchModule(new Trigger(dataExchangeModules.ConfigurationModule.Id.ToString()), dataExchangeModules.ConfigurationModule.GetType(), typeof (SystemConfigurationComponent));
    new TransitionDefinition().AddFromTo(States.Editing).SetTrigger(Triggers.ModificationPerformed).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new ChangeValueCommand()))).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
  }
}
