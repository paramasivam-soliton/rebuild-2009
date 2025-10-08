// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressComboBoxEdit
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public sealed class DevExpressComboBoxEdit : 
  ComboBoxEdit,
  ISupportUndo,
  IValidatableControl,
  IControl
{
  private readonly DXValidationProvider validationProvider = new DXValidationProvider();
  private readonly ControlModificationHelper<DevExpressComboBoxEdit> controlModificationHelper;
  private bool isMandatory;
  private bool isModified;
  private Color? defaultBackColor;
  private Color? borderColor = new Color?(Color.LightGray);

  public DevExpressComboBoxEdit()
  {
    this.controlModificationHelper = new ControlModificationHelper<DevExpressComboBoxEdit>(this);
    this.EnterMoveNextControl = true;
    this.BorderStyle = BorderStyles.Simple;
    this.EditValueChanging += new ChangingEventHandler(this.DevExpressComboBoxEdit_EditValueChanging);
    this.EditValueChanged += new EventHandler(this.DevExpressComboBoxEdit_EditValueChanged);
    this.IsActive = true;
    this.ShowModified = true;
    this.ShowEmptyElement = true;
  }

  private void DevExpressComboBoxEdit_EditValueChanging(object sender, ChangingEventArgs e)
  {
    this.controlModificationHelper.BeginHandleUserModification(this.GetUnwrappedValue(e.OldValue));
  }

  private void DevExpressComboBoxEdit_EditValueChanged(object sender, EventArgs e)
  {
    this.controlModificationHelper.EndHandleUserModification();
    this.Validate();
  }

  public bool ShowEmptyElement { get; set; }

  [Description("Gets or sets the data source")]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public object DataSource
  {
    set
    {
      object selectedItem = this.SelectedItem;
      bool isModified = this.IsModified;
      if (value != null && value is ICollection && (value as ICollection).GetType().GetElementType() != (Type) null && (value as ICollection).GetType().GetElementType().IsEnum)
      {
        ComboBoxItemCollection items = this.Properties.Items;
        try
        {
          items.Clear();
          items.BeginUpdate();
          items.AddRange((object[]) (value as ICollection).Cast<object>().Select<object, ComboBoxEditItemWrapper>((Func<object, ComboBoxEditItemWrapper>) (v => new ComboBoxEditItemWrapper((Enum) v))).ToArray<ComboBoxEditItemWrapper>());
        }
        finally
        {
          items.EndUpdate();
        }
      }
      else if (value != null && value is ICollection)
      {
        ComboBoxItemCollection items = this.Properties.Items;
        try
        {
          items.BeginUpdate();
          items.Clear();
          if (this.ShowEmptyElement)
            items.Add((object) new ComboBoxEditItemWrapper(string.Empty, (object) null));
          items.AddRange(value as ICollection);
        }
        finally
        {
          items.EndUpdate();
        }
      }
      else
      {
        this.Properties.Items.Clear();
        this.Properties.Items.Add(value);
      }
      this.IsModified = isModified;
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

  [Description("Defines the value of a field.")]
  [Localizable(false)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Data")]
  public object Value
  {
    get => this.GetUnwrappedValue(this.SelectedItem);
    set
    {
      try
      {
        this.IsValueSetting = true;
        if (value == null)
        {
          this.SelectedItem = (object) null;
        }
        else
        {
          ComboBoxEditItemWrapper itemByValue = this.FindItemByValue(value);
          if (itemByValue != null)
            this.SelectedItem = (object) itemByValue;
          else if (!Convert.ToString(value).Equals(string.Empty))
          {
            ComboBoxEditItemWrapper boxEditItemWrapper = new ComboBoxEditItemWrapper(Convert.ToString(value), value);
            this.Properties.Items.Add((object) boxEditItemWrapper);
            this.SelectedItem = (object) boxEditItemWrapper;
          }
          else
            this.SelectedItem = (object) null;
        }
        this.SetSaved();
      }
      finally
      {
        this.IsValueSetting = false;
      }
    }
  }

  public bool IsValueSetting { get; private set; }

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

  public void RestoreValue(object valueToRestore)
  {
    this.controlModificationHelper.RestoreValue(valueToRestore);
    this.Focus();
  }

  public void SetSaved() => this.controlModificationHelper.SetSaved();

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

  public string ValidationMessage { get; private set; }

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

  private ComboBoxEditItemWrapper FindItem(ComboBoxEditItemWrapper item)
  {
    ComboBoxEditItemWrapper boxEditItemWrapper = (ComboBoxEditItemWrapper) null;
    if (this.Properties != null && this.Properties.Items != null)
    {
      IEnumerable items = (IEnumerable) this.Properties.Items;
      if (items != null)
        boxEditItemWrapper = items.OfType<ComboBoxEditItemWrapper>().FirstOrDefault<ComboBoxEditItemWrapper>((Func<ComboBoxEditItemWrapper, bool>) (i => i == item));
    }
    return boxEditItemWrapper;
  }

  private ComboBoxEditItemWrapper FindItemByValue(object item)
  {
    ComboBoxEditItemWrapper itemByValue = (ComboBoxEditItemWrapper) null;
    if (this.Properties != null && this.Properties.Items != null)
    {
      IEnumerable items = (IEnumerable) this.Properties.Items;
      if (items != null)
        itemByValue = items.OfType<ComboBoxEditItemWrapper>().FirstOrDefault<ComboBoxEditItemWrapper>((Func<ComboBoxEditItemWrapper, bool>) (i => i.Value != null && i.Value.Equals(item)));
    }
    return itemByValue;
  }

  private object GetUnwrappedValue(object maybeWrappedItem)
  {
    return maybeWrappedItem is ComboBoxEditItemWrapper ? (maybeWrappedItem as ComboBoxEditItemWrapper).Value : maybeWrappedItem;
  }

  public override string ToString() => $"DevExpressComboBoxEdit {this.Name} [{this.Text}]";

  bool IValidatableControl.get_CausesValidation() => this.CausesValidation;

  void IValidatableControl.set_CausesValidation(bool value) => this.CausesValidation = value;

  void IValidatableControl.add_Validating(CancelEventHandler value) => this.Validating += value;

  void IValidatableControl.remove_Validating(CancelEventHandler value) => this.Validating -= value;

  void IValidatableControl.add_Validated(EventHandler value) => this.Validated += value;

  void IValidatableControl.remove_Validated(EventHandler value) => this.Validated -= value;
}
