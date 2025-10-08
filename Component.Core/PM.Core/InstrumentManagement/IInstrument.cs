// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.IInstrument
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Communication;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.InstrumentManagement;

public interface IInstrument
{
  Guid? InstrumentTypeSignature { get; set; }

  string Name { get; set; }

  string SerialNumber { get; set; }

  ICommunicationChannel CommunicationChannel { get; set; }

  Bitmap Image { get; set; }
}
