// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Controls.IValidatableControl
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.UserInterface.Fields;
using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface.Controls;

public interface IValidatableControl : IControl
{
  string FormatString { get; set; }

  bool IsMandatory { get; set; }

  string ValidationMessage { get; }

  ICustomValidator Validator { get; set; }

  bool CausesValidation { get; set; }

  bool Validate();

  event CancelEventHandler Validating;

  event EventHandler Validated;
}
