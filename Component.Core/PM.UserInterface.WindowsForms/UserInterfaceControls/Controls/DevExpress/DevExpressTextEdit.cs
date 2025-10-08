// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressTextEdit
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Mask;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressTextEdit : TextEdit, ISupportUndo, IValidatableControl, IControl
{
  protected DXValidationProvider ValidationProvider = new DXValidationProvider();
  protected PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressTextEdit> ControlModificationHelper;
  private string formatString;
  private bool isMandatory;
  private bool isModified;
  private Color? defaultBackgroundColor;
  private readonly Color? borderColor = new Color?(Color.LightGray);

  public DevExpressTextEdit()
  {
    this.ControlModificationHelper = new PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressTextEdit>(this);
    this.EnterMoveNextControl = true;
    this.BorderStyle = BorderStyles.Simple;
    this.EditValueChanging += new ChangingEventHandler(this.OnValueChanging);
    this.EditValueChanged += new EventHandler(this.OnValueChanged);
    this.Leave += new EventHandler(this.OnFieldLeave);
    this.IsActive = true;
    this.ShowModified = true;
    this.ControlModificationHelper.SetSaved();
  }

  private void OnValueChanging(object sender, ChangingEventArgs e)
  {
    this.ControlModificationHelper.BeginHandleUserModification(e.OldValue);
  }

  private void OnValueChanged(object sender, EventArgs e) => this.Validate();

  private void OnFieldLeave(object sender, EventArgs e)
  {
    this.ControlModificationHelper.EndHandleUserModification();
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
  public virtual object Value
  {
    get => (object) this.Text;
    set
    {
      try
      {
        this.IsValueSetting = true;
        if (value is string)
          this.Text = value as string;
        else
          this.Text = Convert.ToString(value);
        this.SetSaved();
      }
      finally
      {
        this.IsValueSetting = false;
      }
    }
  }

  public bool IsValueSetting { get; protected set; }

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
    this.ControlModificationHelper.RestoreValue(valueToRestore);
    this.Focus();
  }

  public void SetSaved() => this.ControlModificationHelper.SetSaved();

  [Description("Defines if the field shall be formatted with this format string.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  public string FormatString
  {
    get => this.formatString;
    set
    {
      this.formatString = value;
      if (!string.IsNullOrEmpty(this.formatString))
      {
        this.Properties.Mask.EditMask = this.formatString;
        this.Properties.Mask.MaskType = MaskType.RegEx;
        this.Properties.Mask.PlaceHolder = '#';
        this.Properties.Mask.ShowPlaceHolders = false;
        this.Properties.Mask.UseMaskAsDisplayFormat = true;
      }
      else
      {
        this.Properties.Mask.MaskType = MaskType.None;
        this.Properties.Mask.PlaceHolder = '_';
        this.Properties.Mask.ShowPlaceHolders = true;
        this.Properties.Mask.UseMaskAsDisplayFormat = false;
      }
    }
  }

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
        this.SetTextFieldBackColor(this.defaultBackgroundColor);
    }
  }

  public virtual bool Validate()
  {
    this.DoValidate();
    ValidationResult validationResult = this.ControlModificationHelper.PerformValidation(this.ValidationProvider);
    this.ValidationMessage = validationResult.ValidationMessage;
    return validationResult.IsValid;
  }

  public string ValidationMessage { get; protected set; }

  [Description("Defines if the validator.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public ICustomValidator Validator { get; set; }

  public new bool IsModified
  {
    get => this.isModified;
    set
    {
      this.isModified = value;
      if (!this.ShowModified)
        return;
      if (this.isModified)
        this.SetFieldBorderColor(new Color?(Color.Yellow));
      else
        this.SetFieldBorderColor(this.borderColor);
    }
  }

  private void SetTextFieldBackColor(Color? color)
  {
    if (!this.defaultBackgroundColor.HasValue)
      this.defaultBackgroundColor = new Color?(this.BackColor);
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

  public override string ToString() => $"DevExpressTextEdit {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
