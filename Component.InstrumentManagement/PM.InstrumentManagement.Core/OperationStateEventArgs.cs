// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.OperationStateEventArgs
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using System;

#nullable disable
namespace PathMedical.InstrumentManagement;

public class OperationStateEventArgs : EventArgs
{
  public OperationStateType State { get; protected set; }

  public OperationStateEventArgs(OperationStateType state) => this.State = state;
}
