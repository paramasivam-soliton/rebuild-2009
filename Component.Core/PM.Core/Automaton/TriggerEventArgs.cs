// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.TriggerEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Trigger: {RequestedTrigger}, Context: {TriggerContext}")]
public sealed class TriggerEventArgs : CancelEventArgs
{
  public static readonly TriggerEventArgs Empty = new TriggerEventArgs((Trigger) null);

  public Trigger RequestedTrigger { get; private set; }

  public bool IsAsynchronousExecution { get; private set; }

  public TriggerContext TriggerContext { get; private set; }

  public EventHandler<TriggerExecutedEventArgs> TriggerCallBack { get; private set; }

  public TriggerEventArgs(Trigger trigger) => this.RequestedTrigger = trigger;

  public TriggerEventArgs(Trigger trigger, TriggerContext context)
    : this(trigger)
  {
    this.TriggerContext = context;
  }

  public TriggerEventArgs(Trigger trigger, TriggerContext context, bool asynchronousExection)
    : this(trigger, context)
  {
    this.IsAsynchronousExecution = asynchronousExection;
  }

  public TriggerEventArgs(
    Trigger trigger,
    TriggerContext context,
    bool asynchronousExection,
    EventHandler<TriggerExecutedEventArgs> callBack)
    : this(trigger, context, asynchronousExection)
  {
    this.TriggerCallBack += callBack;
  }
}
