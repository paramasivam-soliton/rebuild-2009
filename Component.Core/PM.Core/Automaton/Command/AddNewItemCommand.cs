// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.AddNewItemCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class AddNewItemCommand : CommandBase
{
  private readonly ISingleEditingModel model;
  private bool wasCompletedSuccessfully;

  public AddNewItemCommand(ISingleEditingModel model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (AddNewItemCommand);
    this.model = model;
  }

  public override void Execute()
  {
    this.wasCompletedSuccessfully = false;
    this.model.PrepareAddItem();
    this.wasCompletedSuccessfully = true;
  }

  public void Undo()
  {
    if (!this.wasCompletedSuccessfully)
      return;
    this.model.CancelNewItem();
  }
}
