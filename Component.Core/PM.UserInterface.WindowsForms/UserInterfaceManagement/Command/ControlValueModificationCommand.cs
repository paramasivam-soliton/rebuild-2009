// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Command.ControlValueModificationCommand
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.Controls;
using System;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Command;

public class ControlValueModificationCommand : IUndoableCommand, ICommand
{
  private ISupportUndo control;
  private object orginalValue;

  public ControlValueModificationCommand(ISupportUndo control, object orginalValue)
  {
    this.control = control != null ? control : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (control));
    this.orginalValue = orginalValue;
  }

  public void Undo()
  {
    this.WasCompletedSuccessfully = false;
    this.control.RestoreValue(this.orginalValue);
    this.WasCompletedSuccessfully = true;
  }

  public string Name => this.GetType().Name;

  public void Execute()
  {
  }

  public bool WasCompletedSuccessfully { get; protected set; }

  public object Owner => (object) this.control;

  public TriggerEventArgs TriggerEventArgs { get; set; }

  public override string ToString()
  {
    return $"{this.Name} (orginal value {this.orginalValue} in control {this.control})";
  }
}
