// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Automaton.ChangeUserAssignmentCommand
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement;
using System;

#nullable disable
namespace PathMedical.InstrumentManagement.Automaton;

public class ChangeUserAssignmentCommand : CommandBase, IUndoableCommand, ICommand
{
  private readonly InstrumentManager instrumentManager;
  private IView view;
  private User user;
  private bool remeberedAssignedUserFlag;

  public ChangeUserAssignmentCommand(InstrumentManager instrumentManager, IView view)
  {
    if (instrumentManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrumentManager));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("View");
    this.instrumentManager = instrumentManager;
    this.view = view;
  }

  public override void Execute()
  {
  }

  public void Undo()
  {
    this.user.UserOnInstrumentValue = this.remeberedAssignedUserFlag;
    this.instrumentManager.SelectedUser = this.user;
    this.instrumentManager.ReportAssignmentModification();
  }
}
