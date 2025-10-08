// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.SwitchAssistantViewCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class SwitchAssistantViewCommand : CommandBase
{
  private readonly IAssistantView assistantView;
  private readonly IView view;

  public SwitchAssistantViewCommand(IAssistantView assistantView, IView view)
  {
    if (assistantView == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (assistantView));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.assistantView = assistantView;
    this.view = view;
  }

  public override void Execute() => this.assistantView.SwitchContentView(this.view);
}
