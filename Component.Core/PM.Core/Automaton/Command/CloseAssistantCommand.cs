// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CloseAssistantCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CloseAssistantCommand : CommandBase
{
  private readonly Action closeAssistantAction;

  public CloseAssistantCommand(Action closeAssistantAction)
  {
    this.closeAssistantAction = closeAssistantAction != null ? closeAssistantAction : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (closeAssistantAction));
  }

  public override void Execute() => this.closeAssistantAction();
}
