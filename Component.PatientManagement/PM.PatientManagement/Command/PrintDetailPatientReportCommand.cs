// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.PrintDetailPatientReportCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.AudiologyTest;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class PrintDetailPatientReportCommand : CommandBase
{
  private PatientManager patientManager;
  private readonly Type reportType;
  private readonly TestType testType;

  public PrintDetailPatientReportCommand(PatientManager patientManager, Type reportType)
  {
    if (patientManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientManager));
    if (reportType == (Type) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    this.Name = nameof (PrintDetailPatientReportCommand);
    this.patientManager = patientManager;
    this.reportType = reportType;
    this.testType = TestType.None;
  }

  public override void Execute()
  {
    this.patientManager.Print(this.reportType, new PrintParameter()
    {
      Name = "TestType",
      Value = (object) (short) this.testType
    });
  }
}
