// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Triggers
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Automaton;

public abstract class Triggers
{
  public static readonly Trigger StartModule = new Trigger(nameof (StartModule));
  public static readonly Trigger Add = new Trigger(nameof (Add));
  public static readonly Trigger Edit = new Trigger(nameof (Edit));
  public static readonly Trigger Delete = new Trigger(nameof (Delete));
  public static readonly Trigger DeleteSubItem = new Trigger(nameof (DeleteSubItem));
  public static readonly Trigger Cut = new Trigger(nameof (Cut));
  public static readonly Trigger Paste = new Trigger(nameof (Paste));
  public static readonly Trigger Undo = new Trigger(nameof (Undo));
  public static readonly Trigger Save = new Trigger(nameof (Save));
  public static readonly Trigger Abort = new Trigger(nameof (Abort));
  public static readonly Trigger AbortAndClose = new Trigger(nameof (AbortAndClose));
  public static readonly Trigger ChangeSelection = new Trigger(nameof (ChangeSelection));
  public static readonly Trigger ChangeCurrent = new Trigger(nameof (ChangeCurrent));
  public static readonly Trigger CloseModule = new Trigger(nameof (CloseModule));
  public static readonly Trigger Help = new Trigger(nameof (Help));
  public static readonly Trigger QuitApplication = new Trigger(nameof (QuitApplication));
  public static readonly Trigger Export = new Trigger(nameof (Export));
  public static readonly Trigger Import = new Trigger(nameof (Import));
  public static readonly Trigger RefreshDataFromForm = new Trigger(nameof (RefreshDataFromForm));
  public static readonly Trigger RevertModifications = new Trigger(nameof (RevertModifications));
  public static readonly Trigger Continue = new Trigger(nameof (Continue));
  public static readonly Trigger GoBack = new Trigger(nameof (GoBack));
  public static readonly Trigger Search = new Trigger(nameof (Search));
  public static readonly Trigger StartAssistant = new Trigger(nameof (StartAssistant));
  public static readonly Trigger CloseAssistant = new Trigger(nameof (CloseAssistant));
  public static readonly Trigger ModificationPerformed = new Trigger(nameof (ModificationPerformed));
  public static readonly Trigger ChangeLanguage = new Trigger(nameof (ChangeLanguage));
}
