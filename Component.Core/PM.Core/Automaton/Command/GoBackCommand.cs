// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.GoBackCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Properties;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;

#nullable disable
namespace PathMedical.Automaton.Command;

public class GoBackCommand : CommandBase
{
  public GoBackCommand() => this.Name = nameof (GoBackCommand);

  public override void Execute()
  {
    if (!SystemConfigurationManager.Instance.UserInterfaceManager.GoBackToLastApplicationComponentModule())
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.GoBackCommand_GoBackFailed);
  }
}
