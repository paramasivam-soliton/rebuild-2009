// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CancelNewItemCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CancelNewItemCommand : CommandBase
{
  private readonly ISingleEditingModel model;

  public CancelNewItemCommand(ISingleEditingModel model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (CancelNewItemCommand);
    this.model = model;
  }

  public override void Execute() => this.model.CancelNewItem();
}
