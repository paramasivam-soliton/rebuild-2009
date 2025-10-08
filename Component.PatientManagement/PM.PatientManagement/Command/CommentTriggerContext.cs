// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.CommentTriggerContext
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class CommentTriggerContext : TriggerContext
{
  public object Comment { get; protected set; }

  public CommentTriggerContext(object comment) => this.Comment = comment;
}
