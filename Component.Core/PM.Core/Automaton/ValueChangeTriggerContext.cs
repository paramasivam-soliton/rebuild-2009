// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.ValueChangeTriggerContext
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.UserInterface.Controls;

#nullable disable
namespace PathMedical.Automaton;

public class ValueChangeTriggerContext : TriggerContext
{
  public ValueChangeTriggerContext(object oldValue, object newValue)
  {
    this.OldValue = oldValue;
    this.NewValue = newValue;
  }

  public ValueChangeTriggerContext(
    object oldValue,
    object newValue,
    object control,
    IPerformUndo undoPerformer)
    : this(oldValue, newValue)
  {
    this.Control = control;
    this.UndoPerformer = undoPerformer;
  }

  public object OldValue { get; protected set; }

  public object NewValue { get; protected set; }

  public object Control { get; protected set; }

  public IPerformUndo UndoPerformer { get; protected set; }
}
