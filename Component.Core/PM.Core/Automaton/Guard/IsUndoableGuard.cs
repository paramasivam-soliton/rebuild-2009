// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsUndoableGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsUndoableGuard : GuardBase
{
  public IsUndoableGuard() => this.Name = nameof (IsUndoableGuard);

  public override bool Execute(TriggerEventArgs e) => CommandManager.Instance.CanUndo;
}
