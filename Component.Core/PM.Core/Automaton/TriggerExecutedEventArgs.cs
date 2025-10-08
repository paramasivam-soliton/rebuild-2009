// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.TriggerExecutedEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Automaton;

public class TriggerExecutedEventArgs : EventArgs
{
  public TriggerExecutedEventArgs(TriggerExecutionState state, TriggerEventArgs triggerEventArgs)
  {
    this.State = state;
    this.TriggerEventArgs = triggerEventArgs;
  }

  public TriggerExecutionState State { get; private set; }

  public string Text { get; set; }

  public TriggerEventArgs TriggerEventArgs { get; private set; }
}
