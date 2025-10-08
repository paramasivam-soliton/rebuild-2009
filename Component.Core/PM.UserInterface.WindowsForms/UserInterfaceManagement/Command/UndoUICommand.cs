// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Command.UndoUICommand
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Automaton.Command;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Command;

public class UndoUICommand : CommandBase
{
  public UndoUICommand() => this.Name = nameof (UndoUICommand);

  public override void Execute() => CommandManager.Instance.Undo();
}
