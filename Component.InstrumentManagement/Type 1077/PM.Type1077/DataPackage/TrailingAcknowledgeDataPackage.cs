// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataPackage.TrailingAcknowledgeDataPackage
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.InstrumentCommunicator.DataPackage;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.DataPackage;

[CLSCompliant(false)]
public class TrailingAcknowledgeDataPackage : BinaryDataPackage
{
  public byte[] Acknowledge { get; set; }

  public int DataSize { get; set; }

  public new List<byte> Elements
  {
    get
    {
      lock (this.Items)
        return this.Items.Take<byte>(this.DataSize).ToList<byte>();
    }
  }

  public TrailingAcknowledgeDataPackage()
  {
    this.DataSize = 0;
    this.Acknowledge = new byte[2]{ (byte) 13, (byte) 10 };
  }

  public TrailingAcknowledgeDataPackage(BinaryDataPackage dataPackage)
  {
    this.Add(dataPackage.Elements.ToArray());
  }

  public ValidationState Validate()
  {
    if (this.Items.Count < this.Acknowledge.Length)
      return ValidationState.NotEnoughData;
    this.DataSize = this.Items.Count - this.Acknowledge.Length;
    return ((IEnumerable<byte>) this.Acknowledge).SequenceEqual<byte>((IEnumerable<byte>) this.Items.Skip<byte>(this.DataSize).ToArray<byte>()) ? ValidationState.Valid : ValidationState.NotEnoughData;
  }
}
