// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.FileInformationTriggerContext
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Automaton;

public class FileInformationTriggerContext : TriggerContext
{
  public FileInformationTriggerContext(string[] files) => this.FilesToLoad = files;

  public string[] FilesToLoad { get; protected set; }
}
