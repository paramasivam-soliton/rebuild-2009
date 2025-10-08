// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.State
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Name: {Name}")]
public class State
{
  public State(string name) => this.Name = name;

  public override bool Equals(object obj)
  {
    State state = obj as State;
    return state != (State) null && this.Name.Equals(state.Name);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Name.GetHashCode();

  public static bool operator ==(State a, State b) => object.Equals((object) a, (object) b);

  public static bool operator !=(State a, State b) => !object.Equals((object) a, (object) b);

  public override string ToString() => "State: " + this.Name;

  public string Name { get; private set; }
}
