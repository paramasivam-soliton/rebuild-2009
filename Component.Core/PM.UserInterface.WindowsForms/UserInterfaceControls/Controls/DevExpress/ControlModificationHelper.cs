// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper`1
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.Automaton.Command;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Command;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class ControlModificationHelper<TControl> where TControl : Control, IControl, ISupportUndo, IValidatableControl
{
  private readonly TControl control;
  private bool isModificationReported;

  public ControlModificationHelper(TControl control) => this.control = control;

  public void BeginHandleUserModification(object oldValue)
  {
    if (this.control.IsActive && !this.control.IsReadOnly && !this.control.IsValueSetting && !this.control.IsUndoDisabled && !this.control.IsUndoing && !this.isModificationReported)
    {
      CommandManager.Instance.Add((ICommand) new ControlValueModificationCommand((ISupportUndo) this.control, oldValue));
      this.isModificationReported = true;
    }
    this.UpdateModificationStatus();
  }

  public void EndHandleUserModification()
  {
    this.isModificationReported = false;
    this.control.Validate();
    this.UpdateModificationStatus();
  }

  protected void RememberOriginalValue()
  {
    ObjectHistory.Instance.SetHistory((object) this.control.UniqueModelMemberIdentifier, this.control.Value);
  }

  public void UpdateModificationStatus()
  {
    if (this.control.IsNavigationOnly)
      return;
    if (this.control.Value != null)
    {
      if (this.control.Value is ICollection)
      {
        IEnumerable<object> objects = this.control.Value as IEnumerable<object>;
        if (!(ObjectHistory.Instance.GetHistory((object) this.control.UniqueModelMemberIdentifier, (object) objects) is IEnumerable<object> history) && objects != null && objects.Count<object>() > 0)
          this.control.IsModified = false;
        else if (objects != null && history != null)
          this.control.IsModified = !objects.SequenceEqual<object>(history);
        else
          this.control.IsModified = ObjectHistory.Instance.GetHistory((object) this.control.UniqueModelMemberIdentifier, (object) objects) != null;
      }
      else
        this.control.IsModified = !this.control.Value.Equals(ObjectHistory.Instance.GetHistory((object) this.control.UniqueModelMemberIdentifier, this.control.Value));
    }
    else
      this.control.IsModified = ObjectHistory.Instance.GetHistory((object) this.control.UniqueModelMemberIdentifier, this.control.Value) != null;
    this.control.Validate();
  }

  public void RestoreValue(object valueToRestore)
  {
    bool isUndoDisabled = this.control.IsUndoDisabled;
    try
    {
      this.control.IsUndoDisabled = true;
      this.control.IsUndoing = true;
      this.control.Value = valueToRestore;
      this.isModificationReported = false;
      this.UpdateModificationStatus();
      this.control.Validate();
    }
    finally
    {
      this.control.IsUndoDisabled = isUndoDisabled;
      this.control.IsUndoing = false;
    }
  }

  public void SetSaved()
  {
    if (this.control.IsUndoing)
      return;
    this.RememberOriginalValue();
    this.control.IsModified = false;
    this.EndHandleUserModification();
  }

  public ValidationResult PerformValidation(DXValidationProvider validationProvider)
  {
    bool flag = true;
    string validationMessage = (string) null;
    if (this.control.IsMandatory)
      flag = this.IsMandatoryFieldFulfilled(validationProvider, out validationMessage);
    if (flag && this.control.Validator != null)
      flag = this.IsFieldValidatorFulfilled(validationProvider, out validationMessage);
    if (flag)
      validationProvider.RemoveControlError((Control) this.control);
    return new ValidationResult()
    {
      IsValid = flag,
      ValidationMessage = validationMessage
    };
  }

  private bool IsMandatoryFieldFulfilled(
    DXValidationProvider validationProvider,
    out string validationMessage)
  {
    string str = string.Format(PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("MandatoryFieldIsEmpty"), (object) this.control.Text);
    ConditionValidationRule conditionValidationRule = new ConditionValidationRule();
    conditionValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
    conditionValidationRule.ErrorText = str;
    conditionValidationRule.ErrorType = ErrorType.Critical;
    ConditionValidationRule rule = conditionValidationRule;
    validationProvider.SetValidationRule((Control) this.control, (ValidationRuleBase) rule);
    bool flag = validationProvider.Validate((Control) this.control);
    validationMessage = flag ? (string) null : str;
    return flag;
  }

  private bool IsFieldValidatorFulfilled(
    DXValidationProvider validationProvider,
    out string validationMessage)
  {
    if (this.control.Value == null || this.control.Value is string && string.IsNullOrEmpty(this.control.Value as string))
    {
      validationMessage = string.Empty;
      return true;
    }
    string errorText = this.control.Validator.ErrorText;
    FieldValidationRule rule = new FieldValidationRule(this.control.Validator, errorText);
    validationProvider.SetValidationRule((Control) this.control, (ValidationRuleBase) rule);
    bool flag = validationProvider.Validate((Control) this.control);
    validationMessage = flag ? string.Empty : errorText;
    return flag;
  }
}
