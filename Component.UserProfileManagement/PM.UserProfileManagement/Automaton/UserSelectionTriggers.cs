// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Automaton.UserSelectionTriggers
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.UserProfileManagement.Automaton;

public abstract class UserSelectionTriggers
{
  public static readonly Trigger Save = new Trigger("SaveUserSettings");
  public static readonly Trigger Load = new Trigger("LoadUser");
}
