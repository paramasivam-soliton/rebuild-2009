// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataPackage.SimpleAcknowledgeDatapackage
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.Exception;
using PathMedical.InstrumentCommunicator.DataPackage;
using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.DataPackage;

[CLSCompliant(false)]
public class SimpleAcknowledgeDatapackage : BinaryDataPackage
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (SimpleAcknowledgeDatapackage), "$Rev: 570 $");
  private int acknowledgeSize = 4;
  private byte[] acknowledge;
  private byte[] notAcknowledge;

  public int AcknowedgeSize
  {
    get => this.acknowledgeSize;
    set
    {
      if (this.acknowledgeSize == value)
        return;
      this.acknowledgeSize = value;
      this.acknowledge = new byte[0];
      this.notAcknowledge = new byte[0];
    }
  }

  public byte[] Acknowledge
  {
    get => this.acknowledge;
    set
    {
      if (value == null || value.Length != this.acknowledgeSize)
        throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (Acknowledge));
      this.acknowledge = value;
    }
  }

  public byte[] NotAcknowledge
  {
    get => this.notAcknowledge;
    set
    {
      if (value == null || value.Length != this.acknowledgeSize)
        throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (NotAcknowledge));
      this.notAcknowledge = value;
    }
  }

  public int DataSize { get; set; }

  public new List<byte> Elements
  {
    get
    {
      lock (this.Items)
        return this.Items.Take<byte>(this.DataSize).ToList<byte>();
    }
  }

  public SimpleAcknowledgeDatapackage()
  {
    this.DataSize = 0;
    this.Acknowledge = new byte[4]
    {
      (byte) 79,
      (byte) 75,
      (byte) 13,
      (byte) 10
    };
    this.NotAcknowledge = new byte[4]
    {
      (byte) 69,
      (byte) 82,
      (byte) 13,
      (byte) 10
    };
  }

  public SimpleAcknowledgeDatapackage(BinaryDataPackage dataPackage)
  {
    this.Add(dataPackage.Elements.ToArray());
  }

  public ValidationState Validate()
  {
    if (this.Items.Count < this.DataSize + Math.Min(this.Acknowledge.Length, this.NotAcknowledge.Length))
      return ValidationState.NotEnoughData;
    byte[] array = this.Items.Skip<byte>(this.DataSize).ToArray<byte>();
    return ((IEnumerable<byte>) this.Acknowledge).SequenceEqual<byte>((IEnumerable<byte>) array) || ((IEnumerable<byte>) this.NotAcknowledge).SequenceEqual<byte>((IEnumerable<byte>) array) ? ValidationState.Valid : ValidationState.InvalidData;
  }
}
