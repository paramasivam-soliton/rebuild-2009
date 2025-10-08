// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Automaton.AbortDataDownloadCommand
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using System;

#nullable disable
namespace PathMedical.Type1077.Automaton;

[CLSCompliant(false)]
public class AbortDataDownloadCommand : CommandBase
{
  private readonly Type1077Manager dataManager;

  public AbortDataDownloadCommand(Type1077Manager dataManager) => this.dataManager = dataManager;

  public override void Execute()
  {
    if (this.dataManager == null)
      return;
    TriggerEventArgs triggerEventArgs = this.TriggerEventArgs;
  }
}
