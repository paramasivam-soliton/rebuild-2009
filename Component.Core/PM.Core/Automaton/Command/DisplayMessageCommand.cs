// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.DisplayMessageCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class DisplayMessageCommand : CommandBase
{
  private readonly IView view;

  public DisplayMessageCommand(IView view, string messageText)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    if (string.IsNullOrEmpty(messageText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (messageText));
    this.Name = $"DisplayMessageCommand {messageText}";
    this.MessageText = messageText;
    this.view = view;
  }

  public string MessageText { get; set; }

  public override void Execute() => this.view.DisplayMessage(this.MessageText);
}
