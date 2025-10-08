// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressCheckedComboBoxEdit
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.Extensions;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class DevExpressCheckedComboBoxEdit : 
  CheckedComboBoxEdit,
  ISupportUndo,
  IValidatableControl,
  IControl
{
  private readonly DXValidationProvider validationProvider = new DXValidationProvider();
  protected PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressCheckedComboBoxEdit> ControlModificationHelper;
  private ICollection valuesBeforeModification;
  private bool isMandatory;
  private bool isModified;
  private readonly Color? defaultBackColor;
  private readonly Color? borderColor;

  public DevExpressCheckedComboBoxEdit()
  {
    this.ControlModificationHelper = new PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.ControlModificationHelper<DevExpressCheckedComboBoxEdit>(this);
    this.EnterMoveNextControl = true;
    this.BorderStyle = BorderStyles.Simple;
    this.defaultBackColor = new Color?(this.Properties.Appearance.BackColor);
    this.borderColor = new Color?(Color.LightGray);
    this.Popup += new EventHandler(this.OnPopup);
    this.EditValueChanged += new EventHandler(this.OnEditValueChanged);
    this.IsActive = true;
    this.ShowModified = true;
    this.Properties.SelectAllItemVisible = false;
    this.Properties.ShowButtons = false;
    this.Properties.ShowPopupCloseButton = false;
  }

  private void OnPopup(object sender, EventArgs e)
  {
    this.valuesBeforeModification = (ICollection) this.SelectedValues.Cast<object>().ToList<object>();
  }

  private void OnEditValueChanged(object sender, EventArgs e)
  {
    if (this.valuesBeforeModification != null && !this.SelectedValues.IsContentEqual(this.valuesBeforeModification))
    {
      this.ControlModificationHelper.BeginHandleUserModification((object) this.valuesBeforeModification);
      this.ControlModificationHelper.EndHandleUserModification();
    }
    this.Validate();
  }

  [Description("The property that delivers the desription of an item, which shall be displayed in the combobox.")]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public string ItemDescriptionProperty { get; set; }

  [Description("Gets or sets the data source")]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public object DataSource
  {
    set
    {
      try
      {
        this.Properties.BeginUpdate();
        this.Properties.DataSource = value;
      }
      finally
      {
        this.Properties.EndUpdate();
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
  public bool IsActive
  {
    get => this.Visible;
    set => this.Visible = value;
  }

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
    get => (object) this.SelectedValues;
    set => this.SelectedValues = value as ICollection;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsValueSetting { get; protected set; }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MemberExpression UniqueModelMemberIdentifier { get; set; }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    ValidationResult validationResult = this.ControlModificationHelper.PerformValidation(this.validationProvider);
    this.ValidationMessage = validationResult.ValidationMessage;
    return validationResult.IsValid;
  }

  public string ValidationMessage { get; protected set; }

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
    if (!color.HasValue)
      return;
    this.Properties.Appearance.BackColor = color.Value;
  }

  private void SetFieldBorderColor(Color? color)
  {
    if (!color.HasValue)
      return;
    this.Properties.Appearance.BorderColor = color.Value;
  }

  public ICollection SelectedValues
  {
    get
    {
      List<object> selectedValues = new List<object>();
      for (int index = 0; index < this.Properties.Items.Count; ++index)
      {
        CheckedListBoxItem checkedListBoxItem = this.Properties.Items[index];
        if (checkedListBoxItem.CheckState == CheckState.Checked)
          selectedValues.Add(checkedListBoxItem.Value);
      }
      return (ICollection) selectedValues;
    }
    set
    {
      try
      {
        this.IsValueSetting = true;
        this.Properties.Items.BeginUpdate();
        HashSet<object> objectSet = new HashSet<object>((value ?? (ICollection) new object[0]).Cast<object>());
        for (int index = 0; index < this.Properties.GetItems().Count; ++index)
        {
          CheckedListBoxItem checkedListBoxItem = this.Properties.Items[index];
          checkedListBoxItem.CheckState = objectSet.Contains(checkedListBoxItem.Value) ? CheckState.Checked : CheckState.Unchecked;
        }
        this.Properties.Items.EndUpdate();
        this.RefreshEditValue();
        this.SetSaved();
      }
      finally
      {
        this.IsValueSetting = false;
      }
    }
  }

  public override string ToString() => $"DevExpressCheckedComboBoxEdit {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
