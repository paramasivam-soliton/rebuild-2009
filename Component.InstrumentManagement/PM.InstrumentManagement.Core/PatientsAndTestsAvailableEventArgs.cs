// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.InstrumentDataAvailableEventArgs
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.InstrumentManagement;

public class InstrumentDataAvailableEventArgs : EventArgs
{
  public List<object> Data { get; protected set; }

  public InstrumentDataAvailableEventArgs(List<object> data) => this.Data = data;
}
