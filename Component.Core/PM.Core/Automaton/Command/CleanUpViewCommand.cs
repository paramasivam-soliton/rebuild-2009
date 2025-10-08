// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CleanUpViewCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CleanUpViewCommand : CommandBase
{
  private readonly IView view;

  public CleanUpViewCommand(IView view)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = nameof (CleanUpViewCommand);
    this.view = view;
  }

  public override void Execute()
  {
    if (this.view == null)
      return;
    this.view.CleanUpView();
  }
}
