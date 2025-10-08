// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.GuardComposition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class GuardComposition : GuardBase, IEnumerable
{
  public GuardComposition(params IGuard[] guards)
  {
    if (guards == null)
      return;
    this.Guards = new List<IGuard>(((IEnumerable<IGuard>) guards).Where<IGuard>((Func<IGuard, bool>) (g => g != null)));
    this.Name = string.Join(", ", this.Guards.Select<IGuard, string>((Func<IGuard, string>) (g => g.Name)).ToArray<string>());
  }

  protected List<IGuard> Guards { get; private set; }

  public int Count
  {
    get
    {
      int count = 0;
      if (this.Guards != null)
        count = this.Guards.Count;
      return count;
    }
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this.Guards.GetEnumerator();

  public void Add(IGuard guard)
  {
    if (guard == null)
      return;
    if (this.Guards == null)
      this.Guards = new List<IGuard>();
    this.Guards.Add(guard);
    this.Name = string.Join(", ", this.Guards.Select<IGuard, string>((Func<IGuard, string>) (g => g.Name)).ToArray<string>());
  }

  public override bool Execute(TriggerEventArgs e)
  {
    return this.Guards == null || this.Guards.Count == 0 || this.Guards.All<IGuard>((Func<IGuard, bool>) (g => g.Execute(e)));
  }

  public override string ToString() => this.Name ?? "No guards";

  public List<IGuard> GetGuards<TGuard>() where TGuard : IGuard
  {
    List<IGuard> guards = new List<IGuard>();
    guards.AddRange(this.Guards.OfType<TGuard>().Cast<IGuard>());
    guards.AddRange((IEnumerable<IGuard>) this.Guards.OfType<GuardComposition>().SelectMany<GuardComposition, IGuard>((Func<GuardComposition, IEnumerable<IGuard>>) (g => (IEnumerable<IGuard>) g.GetGuards<TGuard>())).ToList<IGuard>());
    return guards;
  }
}
