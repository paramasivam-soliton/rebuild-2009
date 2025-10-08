// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.DeleteTestCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class DeleteTestCommand : CommandBase
{
  private readonly PatientManager model;
  private object audiologyTest;

  public DeleteTestCommand(PatientManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (DeleteTestCommand);
    this.model = model;
  }

  public override void Execute()
  {
    this.model.DeleteTest(this.model.SelectedPatient, this.model.SelectedAudiologyTest);
  }
}
