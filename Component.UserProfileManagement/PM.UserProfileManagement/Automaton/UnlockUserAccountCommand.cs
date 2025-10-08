// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Automaton.UnlockUserAccountCommand
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.Properties;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Automaton;

public class UnlockUserAccountCommand : CommandBase
{
  private readonly UserManager model;

  public UnlockUserAccountCommand(UserManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (UnlockUserAccountCommand);
    this.model = model;
  }

  public override void Execute()
  {
    if (!(SystemConfigurationManager.Instance.InformUserAfterSuccessfulStorageOperation == StorageConfirmation.Yes & this.model.UnlockUserAccount()))
      return;
    int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox(Resources.UnlockUserAccountCommand_ConfirmationUnlockMessage, Resources.UnlockUserAccountCommand_ConfirmationUnlockMessageWindowTitle, AnswerOptionType.OK, QuestionIcon.Information);
  }
}
