// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressPasswordEdit
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressPasswordEdit : DevExpressTextEdit
{
  private DevExpressPasswordEdit passwordConfirmationPartner;
  private bool hasStartedMutualValidation;
  private object encryptedValue;

  public DevExpressPasswordEdit()
  {
    this.Properties.NullValuePrompt = "**********";
    this.Properties.PasswordChar = Convert.ToChar("*");
    this.TextChanged += new EventHandler(this.OnTextChanged);
  }

  [Description("The other password field (for mutual confirmation).")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public DevExpressPasswordEdit PasswordConfirmationPartner
  {
    get => this.passwordConfirmationPartner;
    set
    {
      if (this.passwordConfirmationPartner == value)
        return;
      this.passwordConfirmationPartner = value;
      if (value == null)
        return;
      value.PasswordConfirmationPartner = this;
    }
  }

  [Description("Defines the value of a field.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  public override object Value
  {
    get => string.IsNullOrEmpty(this.Text) ? this.encryptedValue : (object) this.Text;
    set
    {
      if (this.IsUndoing)
      {
        if (string.IsNullOrEmpty(value as string))
          this.Text = (string) null;
        else
          this.Text = value as string;
      }
      else
      {
        this.Text = (string) null;
        this.encryptedValue = value;
      }
      this.SetSaved();
    }
  }

  public override bool Validate()
  {
    if (this.PasswordConfirmationPartner == null)
      return base.Validate();
    this.DoValidate();
    string str = PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("PasswordFieldsNotEqual");
    CompareAgainstControlValidationRule rule = new CompareAgainstControlValidationRule();
    rule.CompareControlOperator = CompareControlOperator.Equals;
    rule.Control = (Control) this.PasswordConfirmationPartner;
    rule.ErrorText = str;
    rule.ErrorType = ErrorType.Critical;
    this.ValidationProvider.SetValidationRule((Control) this, (ValidationRuleBase) rule);
    bool flag = this.ValidationProvider.Validate((Control) this);
    this.ValidationMessage = flag ? (string) null : str;
    if (this.PasswordConfirmationPartner != null)
    {
      if (!this.PasswordConfirmationPartner.hasStartedMutualValidation)
      {
        try
        {
          this.hasStartedMutualValidation = true;
          this.PasswordConfirmationPartner.Validate();
        }
        finally
        {
          this.hasStartedMutualValidation = false;
        }
      }
    }
    if (flag)
      flag = base.Validate();
    return flag;
  }

  protected void OnTextChanged(object sender, EventArgs e) => this.Validate();

  public override string ToString() => $"DevExpressPasswordEdit {this.Name} [{this.Text}]";
}
