// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.MrnValidator
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.UserInterface.Fields;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class MrnValidator : ICustomValidator
{
  public MrnValidator() => this.Severity = false;

  public bool Validate(object value)
  {
    return value is string && MedicalRecordNumberChecker.Instance.CheckMrn(value as string, this.MrnType, this.Severity);
  }

  public string ErrorText { get; set; }

  public MedicalRecordTypes MrnType { get; set; }

  public bool Severity { get; set; }
}
