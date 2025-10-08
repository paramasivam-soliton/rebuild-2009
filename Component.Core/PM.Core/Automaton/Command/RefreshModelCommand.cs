// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.RefreshModelCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class RefreshModelCommand : CommandBase
{
  private readonly IModel model;

  public RefreshModelCommand(IModel model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (RefreshModelCommand);
    this.model = model;
  }

  public override void Execute() => this.model.RefreshData();
}
