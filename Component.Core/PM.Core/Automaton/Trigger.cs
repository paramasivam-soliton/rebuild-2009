// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Trigger
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Trigger: {Name}")]
public class Trigger
{
  public Trigger([Localizable(false)] string name) => this.Name = name;

  public override bool Equals(object obj)
  {
    Trigger trigger = obj as Trigger;
    return trigger != (Trigger) null && this.Name.Equals(trigger.Name);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Name.GetHashCode();

  public static bool operator ==(Trigger a, Trigger b) => object.Equals((object) a, (object) b);

  public static bool operator !=(Trigger a, Trigger b) => !object.Equals((object) a, (object) b);

  public override string ToString() => "Trigger: " + this.Name;

  public string Name { get; private set; }
}
