// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Command.ChangePrivateSettingCommand
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Automaton.Command;

#nullable disable
namespace PathMedical.UserProfileManagement.Command;

public class ChangePrivateSettingCommand : CommandBase
{
  private readonly UserManager userManager;

  public ChangePrivateSettingCommand(UserManager userManager) => this.userManager = userManager;

  public override void Execute()
  {
    if (this.userManager == null || this.TriggerEventArgs == null)
      return;
    this.userManager.ChangePrivateUserSettings();
  }
}
