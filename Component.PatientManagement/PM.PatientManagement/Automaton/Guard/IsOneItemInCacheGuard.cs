// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Automaton.Guard.IsOneItemInCacheGuard
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Automaton.Guard;

public class IsOneItemInCacheGuard : GuardBase
{
  private readonly PatientManager model;

  public IsOneItemInCacheGuard(PatientManager model)
  {
    this.model = model != null ? model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
  }

  public override bool Execute(TriggerEventArgs e)
  {
    bool flag = false;
    if (this.model != null && this.model.TestToCopy != null)
      flag = true;
    return flag;
  }
}
