// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressButton
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressButton : SimpleButton, ISupportUndo, IValidatableControl, IControl
{
  protected ControlModificationHelper<DevExpressButton> ControlMedificationHelper;

  public DevExpressButton()
  {
    this.ControlMedificationHelper = new ControlModificationHelper<DevExpressButton>(this);
    this.IsActive = true;
  }

  public MemberExpression UniqueModelMemberIdentifier { get; set; }

  public bool IsUndoDisabled { get; set; }

  public bool IsUndoing { get; set; }

  public bool IsModified { get; set; }

  public bool IsNavigationOnly { get; set; }

  public bool ShowModified { get; set; }

  public void RestoreValue(object valueToRestore)
  {
  }

  public void SetSaved() => this.ControlMedificationHelper.SetSaved();

  public bool IsReadOnly { get; set; }

  public bool IsActive { get; set; }

  public object Value { get; set; }

  public bool IsValueSetting { get; private set; }

  public string FormatString { get; set; }

  public bool IsMandatory { get; set; }

  public string ValidationMessage { get; private set; }

  public ICustomValidator Validator { get; set; }

  public bool Validate() => true;

  public override string ToString() => $"DevExpressButton {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
