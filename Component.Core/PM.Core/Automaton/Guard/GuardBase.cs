// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.GuardBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Automaton.Guard;

public abstract class GuardBase : IGuard
{
  public string Name { get; protected set; }

  public abstract bool Execute(TriggerEventArgs e);
}
