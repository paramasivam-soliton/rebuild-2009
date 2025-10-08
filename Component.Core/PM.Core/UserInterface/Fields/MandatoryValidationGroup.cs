// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Fields.MandatoryValidationGroup
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.UserInterface.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.UserInterface.Fields;

public class MandatoryValidationGroup
{
  private readonly List<IControl> controls = new List<IControl>();
  private bool isGroupValidating;

  public MandatoryValidationGroup(FieldConfigurationType fieldConfigurationType)
  {
    this.FieldConfigurationType = fieldConfigurationType;
  }

  public FieldConfigurationType FieldConfigurationType { get; private set; }

  public bool AreAllFieldsFilled
  {
    get
    {
      return this.controls.Select<IControl, object>((Func<IControl, object>) (c => c.Value)).All<object>((Func<object, bool>) (v => v != null && !string.IsNullOrEmpty(v.ToString())));
    }
  }

  public void RegisterControls(IEnumerable<IControl> controlsToRegister)
  {
    foreach (IValidatableControl validatableControl in controlsToRegister.OfType<IValidatableControl>().ToList<IValidatableControl>())
    {
      validatableControl.CausesValidation = true;
      validatableControl.IsMandatory = true;
      validatableControl.Validated += new EventHandler(this.GroupItemValidated);
    }
    this.controls.AddRange(controlsToRegister);
  }

  private void GroupItemValidated(object sender, EventArgs e)
  {
    if (this.isGroupValidating)
      return;
    this.ValidateGroup();
  }

  public event EventHandler<MandatoryGroupValidatedEventArgs> GroupValidated;

  public bool ValidateGroup()
  {
    try
    {
      this.isGroupValidating = true;
      List<IValidatableControl> list = this.controls.OfType<IValidatableControl>().ToList<IValidatableControl>();
      bool validationResult = true;
      foreach (IValidatableControl validatableControl in list)
      {
        validatableControl.IsMandatory = true;
        if (!validatableControl.Validate())
        {
          validationResult = false;
          break;
        }
      }
      if (this.GroupValidated != null)
        this.GroupValidated((object) this, new MandatoryGroupValidatedEventArgs(validationResult));
      return validationResult;
    }
    finally
    {
      this.isGroupValidating = false;
    }
  }

  public void EnableGroup(bool enabled)
  {
    foreach (IValidatableControl validatableControl in this.controls.OfType<IValidatableControl>().ToList<IValidatableControl>())
    {
      validatableControl.IsMandatory = enabled;
      validatableControl.Validate();
    }
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("MandatoryGroup {0}: ", (object) this.GetHashCode());
    foreach (IControl control in this.controls)
      stringBuilder.AppendFormat("[{0}] ", (object) control);
    return stringBuilder.ToString();
  }
}
