// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CopyUIToModelCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CopyUIToModelCommand : CommandBase
{
  protected IView View;

  public CopyUIToModelCommand(IView view)
  {
    this.View = view != null ? view : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = nameof (CopyUIToModelCommand);
  }

  public override void Execute() => this.View.CopyUIToModel();
}
