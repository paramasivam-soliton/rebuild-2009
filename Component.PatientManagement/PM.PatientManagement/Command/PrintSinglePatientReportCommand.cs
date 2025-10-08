// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.PrintSinglePatientReportCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.AudiologyTest;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class PrintSinglePatientReportCommand : CommandBase
{
  private readonly PatientManager patientManager;
  private readonly Type reportType;
  private readonly ReportFormat reportFormat;
  private TestType[] testTypes;

  private PrintSinglePatientReportCommand(PatientManager patientManager, Type reportType)
  {
    if (patientManager == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientManager));
    if (reportType == (Type) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    this.Name = nameof (PrintSinglePatientReportCommand);
    this.patientManager = patientManager;
    this.reportType = reportType;
  }

  public PrintSinglePatientReportCommand(
    PatientManager patientManager,
    Type reportType,
    params TestType[] testTypes)
    : this(patientManager, reportType)
  {
    this.testTypes = testTypes;
  }

  public PrintSinglePatientReportCommand(
    PatientManager patientManager,
    Type reportType,
    ReportFormat reportFormat,
    params TestType[] testTypes)
    : this(patientManager, reportType)
  {
    this.reportFormat = reportFormat;
    this.testTypes = testTypes;
  }

  public override void Execute()
  {
    List<PrintParameter> printParameterList = new List<PrintParameter>();
    foreach (TestType testType in this.testTypes)
    {
      printParameterList.Add(new PrintParameter()
      {
        Name = "ReportFormat",
        Value = (object) this.reportFormat
      });
      printParameterList.Add(new PrintParameter()
      {
        Name = "TestType",
        Value = (object) testType
      });
    }
    this.patientManager.Print(this.reportType, printParameterList.ToArray());
  }
}
