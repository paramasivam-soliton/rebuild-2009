// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.UndoScope
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface;

public class UndoScope : IDisposable
{
  private static readonly List<object> controls = new List<object>();
  private readonly object control;

  public UndoScope(object control)
  {
    this.control = control;
    UndoScope.controls.Add(control);
  }

  public static bool IsUndoing(object control) => UndoScope.controls.Contains(control);

  public void Dispose() => UndoScope.controls.Remove(this.control);
}
