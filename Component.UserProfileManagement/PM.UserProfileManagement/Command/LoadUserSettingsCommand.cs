// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Command.LoadUserSettingsCommand
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Login;
using PathMedical.UserProfileManagement.Automaton;

#nullable disable
namespace PathMedical.UserProfileManagement.Command;

public class LoadUserSettingsCommand : CommandBase
{
  private readonly UserSelectionTriggerContext context;
  private readonly UserManager userManager;

  public LoadUserSettingsCommand(UserManager userManager) => this.userManager = userManager;

  public override void Execute()
  {
    if (this.userManager == null || this.TriggerEventArgs == null || !(LoginManager.Instance.LoggedInUserData.Entity is User entity))
      return;
    this.userManager.ChangeSingleSelection(entity);
  }
}
