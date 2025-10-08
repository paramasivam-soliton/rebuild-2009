// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.TriggerExecutionInformation
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Automaton;

public class TriggerExecutionInformation
{
  public TriggerExecutionInformation() => this.AccessPermissionId = Guid.Empty;

  public TriggerExecutionInformation(Trigger trigger)
    : this()
  {
    this.Trigger = trigger;
  }

  public TriggerExecutionInformation(Trigger trigger, Guid accessPermissionId)
    : this(trigger)
  {
    this.AccessPermissionId = accessPermissionId;
  }

  public Trigger Trigger { get; protected set; }

  public Guid AccessPermissionId { get; protected set; }
}
