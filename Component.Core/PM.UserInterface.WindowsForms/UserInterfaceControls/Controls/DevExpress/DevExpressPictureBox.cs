// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressPictureBox
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressPictureBox : PictureEdit, ISupportUndo, IValidatableControl, IControl
{
  protected PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressPictureBox> ControlModificationHelper;
  private bool isModified;
  private readonly Color? borderColor = new Color?(Color.LightGray);

  public DevExpressPictureBox()
  {
    this.ControlModificationHelper = new PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressPictureBox>(this);
    this.ImageChanged += new EventHandler(this.OnValueChanged);
    this.IsActive = true;
    this.ShowModified = true;
  }

  private void OnValueChanged(object sender, EventArgs e)
  {
    this.ControlModificationHelper.BeginHandleUserModification((object) this.BackgroundImage);
    this.ControlModificationHelper.EndHandleUserModification();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MemberExpression UniqueModelMemberIdentifier { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool IsUndoDisabled { get; set; }

  public bool IsUndoing { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool IsNavigationOnly { get; set; }

  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowModified { get; set; }

  public void RestoreValue(object valueToRestore)
  {
    this.ControlModificationHelper.RestoreValue(valueToRestore);
  }

  public void SetSaved() => this.ControlModificationHelper.SetSaved();

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

  [Description("Defines the value of a field.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public object Value
  {
    get => (object) this.Image;
    set
    {
      try
      {
        this.IsValueSetting = true;
        this.Image = value as Image;
        this.SetSaved();
      }
      finally
      {
        this.IsValueSetting = false;
      }
    }
  }

  public bool IsValueSetting { get; protected set; }

  public string FormatString { get; set; }

  public bool IsMandatory { get; set; }

  public string ValidationMessage { get; private set; }

  public ICustomValidator Validator { get; set; }

  public bool Validate() => true;

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

  private void SetFieldBorderColor(Color? color)
  {
    if (!color.HasValue)
      return;
    this.Properties.Appearance.BorderColor = color.Value;
  }

  public override string ToString() => $"DevExpressPictureBox {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
