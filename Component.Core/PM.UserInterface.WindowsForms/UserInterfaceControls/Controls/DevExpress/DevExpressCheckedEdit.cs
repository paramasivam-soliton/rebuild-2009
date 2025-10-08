// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressCheckedEdit
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressCheckedEdit : CheckEdit, ISupportUndo, IValidatableControl, IControl
{
  private readonly ControlModificationHelper<DevExpressCheckedEdit> controlModificationHelper;
  private readonly DXValidationProvider validationProvider = new DXValidationProvider();
  private bool isMandatory;
  private bool isModified;
  private readonly Color? borderColor;
  private Color? defaultBackColor;

  private void SetTextFieldBackColor(Color? color)
  {
    if (!this.defaultBackColor.HasValue)
      this.defaultBackColor = new Color?(this.BackColor);
    if (!color.HasValue)
      return;
    this.BackColor = color.Value;
  }

  private void SetFieldBorderColor(Color? color)
  {
    if (!color.HasValue)
      return;
    this.Properties.Appearance.BorderColor = color.Value;
  }

  public DevExpressCheckedEdit()
  {
    this.controlModificationHelper = new ControlModificationHelper<DevExpressCheckedEdit>(this);
    this.BorderStyle = BorderStyles.Simple;
    this.EditValueChanging += new ChangingEventHandler(this.OnValueChanging);
    this.EditValueChanged += new EventHandler(this.OnValueChanged);
    this.Leave += new EventHandler(this.OnLeaveField);
    this.IsActive = true;
    this.ShowModified = true;
    this.controlModificationHelper.SetSaved();
  }

  public sealed override BorderStyles BorderStyle
  {
    get => base.BorderStyle;
    set => base.BorderStyle = value;
  }

  private void OnValueChanging(object sender, ChangingEventArgs e)
  {
    this.controlModificationHelper.BeginHandleUserModification(e.OldValue);
  }

  public void OnValueChanged(object sender, EventArgs e)
  {
    this.controlModificationHelper.UpdateModificationStatus();
    this.Validate();
  }

  private void OnLeaveField(object sender, EventArgs e)
  {
    this.controlModificationHelper.EndHandleUserModification();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MemberExpression UniqueModelMemberIdentifier { get; set; }

  public bool IsUndoing { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool IsUndoDisabled { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowModified { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool IsNavigationOnly { get; set; }

  public virtual void RestoreValue(object valueToRestore)
  {
    this.controlModificationHelper.RestoreValue(valueToRestore);
    this.Focus();
  }

  public void SetSaved() => this.controlModificationHelper.SetSaved();

  public new bool IsModified
  {
    get => this.isModified;
    set
    {
      this.isModified = value;
      if (this.DesignMode || !this.ShowModified)
        return;
      if (this.isModified)
      {
        this.BorderStyle = BorderStyles.Simple;
        this.SetFieldBorderColor(new Color?(Color.Yellow));
      }
      else
      {
        this.BorderStyle = BorderStyles.NoBorder;
        this.SetFieldBorderColor(this.borderColor);
      }
    }
  }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public Guid Id { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public bool IsReadOnly
  {
    get => this.Properties.ReadOnly;
    set => this.Properties.ReadOnly = value;
  }

  [Description("Defines if the field is active and will be displayed.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  [DefaultValue(true)]
  public bool IsActive { get; set; }

  [Description("The text that appears next to the field and describes the field content.")]
  [Localizable(true)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  [DefaultValue("Caption")]
  public string Caption { get; set; }

  [Description("Defines the value of a field.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public object Value
  {
    get => (object) Convert.ToBoolean(this.Checked);
    set
    {
      try
      {
        this.IsValueSetting = true;
        this.Checked = Convert.ToBoolean(value);
        this.SetSaved();
      }
      finally
      {
        this.IsValueSetting = false;
      }
    }
  }

  public bool IsValueSetting { get; protected set; }

  [Description("Defines if the field shall be formatted with this format string.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  public string FormatString { get; set; }

  [Description("Defines if the field must be entered.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  [DefaultValue(false)]
  public bool IsMandatory
  {
    get => this.isMandatory;
    set
    {
      this.isMandatory = value;
      if (this.isMandatory)
      {
        this.IsActive = true;
        this.SetTextFieldBackColor(new Color?(Color.LightYellow));
      }
      else
        this.SetTextFieldBackColor(this.defaultBackColor);
    }
  }

  public bool Validate()
  {
    this.DoValidate();
    ValidationResult validationResult = this.controlModificationHelper.PerformValidation(this.validationProvider);
    this.ValidationMessage = validationResult.ValidationMessage;
    return validationResult.IsValid;
  }

  public string ValidationMessage { get; protected set; }

  public ICustomValidator Validator { get; set; }

  public override string ToString() => $"DevExpressCheckedEdit {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
