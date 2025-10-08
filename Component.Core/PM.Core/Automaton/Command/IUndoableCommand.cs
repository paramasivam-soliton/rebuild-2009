// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.IUndoableCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Automaton.Command;

public interface IUndoableCommand : ICommand
{
  void Undo();
}
