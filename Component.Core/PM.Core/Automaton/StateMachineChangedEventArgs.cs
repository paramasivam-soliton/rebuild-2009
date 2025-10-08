// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.StateMachineChangedEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Old: {OldState} - New: {NewState}")]
public class StateMachineChangedEventArgs : EventArgs
{
  public StateMachineChangedEventArgs(State oldState, State newState)
  {
    this.OldState = oldState;
    this.NewState = newState;
  }

  public State NewState { get; protected set; }

  public State OldState { get; protected set; }
}
