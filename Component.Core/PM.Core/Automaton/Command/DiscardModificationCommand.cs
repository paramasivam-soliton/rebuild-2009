// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.DiscardModificationCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class DiscardModificationCommand : CommandBase
{
  private readonly ISingleEditingModel model;
  private readonly IView view;

  public DiscardModificationCommand(ISingleEditingModel model, IView view)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.model = model;
    this.view = view;
    this.Name = nameof (DiscardModificationCommand);
  }

  public override void Execute()
  {
    CommandManager.Instance.Reset();
    if (this.view.ViewMode == ViewModeType.Adding)
    {
      this.model.CancelNewItem();
      this.view.ViewMode = ViewModeType.Editing;
    }
    else
      this.model.RevertModifications();
  }
}
