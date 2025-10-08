// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Command.ChangePermissionRightCommand
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Command;

public class ChangePermissionRightCommand : CommandBase, IUndoableCommand, ICommand
{
  private readonly UserProfileManager profileManager;
  private AccessPermission accessPermission;
  private bool rememberedAccessPermissionFlag;
  private IView view;

  public ChangePermissionRightCommand(UserProfileManager profileManager, IView view)
  {
    if (profileManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (profileManager));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.profileManager = profileManager;
    this.view = view;
  }

  public override void Execute()
  {
    if (this.profileManager.SelectedAccessPermission == null)
      return;
    this.accessPermission = this.profileManager.SelectedAccessPermission;
    this.rememberedAccessPermissionFlag = this.accessPermission.AccessPermissionFlag;
    if (this.TriggerEventArgs != null && this.TriggerEventArgs.TriggerContext != null && this.TriggerEventArgs.TriggerContext is ValueChangeTriggerContext && this.TriggerEventArgs.TriggerContext is ValueChangeTriggerContext triggerContext)
      this.profileManager.SelectedAccessPermission.AccessPermissionFlag = (bool) triggerContext.NewValue;
    this.profileManager.ReportAccessPermissionModification();
  }

  public void Undo()
  {
    this.accessPermission.AccessPermissionFlag = this.rememberedAccessPermissionFlag;
    this.profileManager.SelectedAccessPermission = this.accessPermission;
    this.profileManager.ReportAccessPermissionModification();
  }
}
