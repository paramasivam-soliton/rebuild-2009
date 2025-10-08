// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Automaton.Guard.IsOneItemAvailableGuard
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.AudiologyTest;
using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.Automaton.Guard;

public class IsOneItemAvailableGuard : GuardBase
{
  private readonly PatientManager model;
  private readonly TestType[] testTypes;

  public IsOneItemAvailableGuard(PatientManager model, params TestType[] testTypes)
  {
    this.model = model != null ? model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.testTypes = testTypes;
  }

  public override bool Execute(TriggerEventArgs e)
  {
    bool flag = false;
    if (this.model != null && this.model.SelectedPatient != null && this.model.SelectedPatient.AudiologyTests != null)
    {
      foreach (TestType testType in this.testTypes)
      {
        TestType type = testType;
        flag = this.model.SelectedPatient.AudiologyTests.Any<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t => t.TestType == type));
        if (flag)
          break;
      }
    }
    return flag;
  }
}
