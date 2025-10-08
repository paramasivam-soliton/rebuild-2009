// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser.LocationTypeBrowserController
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.SiteAndFacilityManagement.Automaton;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser;

public class LocationTypeBrowserController : 
  Controller<LocationTypeManager, LocationTypeMasterDetailBrowser>
{
  public LocationTypeBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = LocationTypeManager.Instance;
    this.View = new LocationTypeMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditSites, SiteFacilityManagementPermissions.AddAndEditFacilities);
    machineConfigurator.SupportDeleting<LocationType>().NeedsAnyPermission(SiteFacilityManagementPermissions.DeleteSites, SiteFacilityManagementPermissions.DeleteFacilities);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SiteFacilityManagementPermissions.ViewSites, SiteFacilityManagementPermissions.AddAndEditSites, SiteFacilityManagementPermissions.ViewFacilities, SiteFacilityManagementPermissions.AddAndEditFacilities).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditSites, SiteFacilityManagementPermissions.AddAndEditFacilities)).SetGuard((IGuard) new IsOneItemAvailableGuard<LocationType>((ISingleSelectionModel<LocationType>) this.Model));
    machineConfigurator.SupportChangeSelection<LocationType>();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding, States.Editing).SetTrigger(Triggers.Save).SetGuard((IGuard) new IsViewValidGuard((IView) this.View)).SetCommand((ICommand) new CommandComposition(new ICommand[3]
    {
      machineConfigurator.SaveCommand,
      (ICommand) new AssignLocationToAllFacilitiesCommand((IView) this.View, this.Model),
      (ICommand) new RefreshModelCommand((IModel) this.Model)
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(Triggers.Save).SetGuard((IGuard) new IsNotGuard((IGuard) new IsViewValidGuard((IView) this.View))).SetCommand((ICommand) new DisplayMessageCommand((IView) this.View, PathMedical.ComponentResourceManagement.Instance.ResourceManager.GetString("DataNotValidCorrectFirst"))).ApplyTo(this.StateMachine);
    machineConfigurator.SupportSwitchModule(Triggers.GoBack, typeof (SiteBrowserComponentModule));
    machineConfigurator.SupportGettingHelp();
    machineConfigurator.SupportSuspending();
  }
}
