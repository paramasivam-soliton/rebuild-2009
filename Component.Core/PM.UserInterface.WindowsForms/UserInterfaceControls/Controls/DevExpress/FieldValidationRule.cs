// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.FieldValidationRule
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.Exception;
using PathMedical.UserInterface.Fields;
using System;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class FieldValidationRule : ValidationRule
{
  protected ICustomValidator customValidator;

  public FieldValidationRule(ICustomValidator customValidator, string errorText)
  {
    if (customValidator == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (customValidator));
    if (errorText == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (errorText));
    this.customValidator = customValidator;
    this.ErrorText = errorText;
  }

  public override bool Validate(Control control, object value)
  {
    int num = this.customValidator.Validate(value) ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.ErrorText = this.customValidator.ErrorText;
    return num != 0;
  }
}
