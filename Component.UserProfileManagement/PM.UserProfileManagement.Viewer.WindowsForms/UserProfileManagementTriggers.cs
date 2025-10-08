// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.UserProfileManagementTriggers
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms;

public abstract class UserProfileManagementTriggers
{
  public static readonly Trigger SwitchToProfileBrowser = new Trigger(nameof (SwitchToProfileBrowser));
  public static readonly Trigger ChangeSelectedAccessPermission = new Trigger(nameof (ChangeSelectedAccessPermission));
  public static readonly Trigger UnlockUserAccount = new Trigger(nameof (UnlockUserAccount));
}
