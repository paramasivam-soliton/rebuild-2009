// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsModificationHandlingChosenGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsModificationHandlingChosenGuard : GuardBase
{
  private readonly HandleModificationCommand handleModificationCommand;
  private readonly HandleModificationType handleModificationType;

  public IsModificationHandlingChosenGuard(
    HandleModificationCommand command,
    HandleModificationType handleModificationType)
  {
    if (command == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (command));
    this.Name = nameof (IsModificationHandlingChosenGuard);
    this.handleModificationCommand = command;
    this.handleModificationType = handleModificationType;
  }

  public override bool Execute(TriggerEventArgs e)
  {
    return this.handleModificationCommand != null && this.handleModificationCommand.HandleModificationType == this.handleModificationType;
  }
}
