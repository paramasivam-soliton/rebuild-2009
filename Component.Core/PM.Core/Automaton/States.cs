// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.States
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Automaton;

public abstract class States
{
  public static readonly State Initializing = new State(nameof (Initializing));
  public static readonly State Idle = new State(nameof (Idle));
  public static readonly State CheckingPermission = new State(nameof (CheckingPermission));
  public static readonly State Viewing = new State(nameof (Viewing));
  public static readonly State Adding = new State(nameof (Adding));
  public static readonly State Editing = new State(nameof (Editing));
  public static readonly State Deleting = new State(nameof (Deleting));
  public static readonly State Closing = new State(nameof (Closing));
  public static readonly State Saving = new State(nameof (Saving));
  public static readonly State Finishing = new State(nameof (Finishing));
  public static readonly State ChangingSelection = new State(nameof (ChangingSelection));
  public static readonly State Selecting = new State(nameof (Selecting));
  public static readonly State QueryingHowToHandleEditedItem = new State(nameof (QueryingHowToHandleEditedItem));
  public static readonly State QueryingHowToHandleNewItem = new State(nameof (QueryingHowToHandleNewItem));
  public static readonly State Undoing = new State(nameof (Undoing));
  public static readonly State Sleeping = new State(nameof (Sleeping));
  public static readonly State Importing = new State(nameof (Importing));
}
