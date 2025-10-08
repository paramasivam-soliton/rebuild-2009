// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ChangeViewModeCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ChangeViewModeCommand : CommandBase
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ChangeViewModeCommand), "$Rev: 1448 $");
  private readonly IView view;
  private ViewModeType lastViewMode;

  public ViewModeType ViewMode { get; private set; }

  public ChangeViewModeCommand(IView view, ViewModeType viewMode)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = nameof (ChangeViewModeCommand);
    this.view = view;
    this.ViewMode = viewMode;
  }

  public override void Execute()
  {
    if (this.view == null)
      return;
    this.lastViewMode = this.view.ViewMode;
    this.view.ViewMode = this.ViewMode;
    ChangeViewModeCommand.Logger.Debug("Changed View mode of View {0} from {1} to {2}", (object) this.view, (object) this.lastViewMode, (object) this.ViewMode);
  }
}
