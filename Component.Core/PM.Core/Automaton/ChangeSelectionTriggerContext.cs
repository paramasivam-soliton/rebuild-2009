// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.ChangeSelectionTriggerContext`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("OldSelection: {OldSelection}, NewSelection: {NewSelection}")]
public class ChangeSelectionTriggerContext<TEntity> : TriggerContext where TEntity : class, new()
{
  public ChangeSelectionTriggerContext(
    ICollection<TEntity> oldSelection,
    ICollection<TEntity> newSelection)
  {
    this.OldSelection = oldSelection;
    this.NewSelection = newSelection;
  }

  public ICollection<TEntity> NewSelection { get; protected set; }

  public ICollection<TEntity> OldSelection { get; protected set; }

  public bool AreMultipleItemsSelected => this.NewSelection != null && this.NewSelection.Count > 1;
}
