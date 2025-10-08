// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentCommunicator.DataPackage.FlashDataPackage
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Type1077.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

#nullable disable
namespace PathMedical.InstrumentCommunicator.DataPackage;

[CLSCompliant(false)]
public class FlashDataPackage : BinaryDataPackage
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (FlashDataPackage), "$Rev: 1091 $");
  public readonly ushort TransmissionHeaderSize = 22;
  private readonly ushort transmissionHeaderSignal = 9320;

  public List<byte> TransmissionHeader { get; protected set; }

  public uint BlockSize { get; protected set; }

  public uint BlockNumber { get; protected set; }

  public byte[] BlockHash { get; protected set; }

  public new List<byte> Elements
  {
    get
    {
      lock (this.Items)
        return this.Items.Skip<byte>((int) this.TransmissionHeaderSize).ToList<byte>();
    }
  }

  public FlashDataPackage()
  {
  }

  public FlashDataPackage(BinaryDataPackage dataPackage, bool dataPackageContainsTransmissionHeader)
  {
    if (!dataPackageContainsTransmissionHeader)
      this.BuildTransmissionHeader(dataPackage.Elements.ToArray());
    this.Add(dataPackage.Elements.ToArray());
  }

  public ValidationState Validate()
  {
    List<byte> list1 = this.Items.ToList<byte>();
    if (list1 == null || list1.Count < (int) this.TransmissionHeaderSize)
      return ValidationState.NotEnoughData;
    List<byte> list2 = this.Items.Take<byte>((int) this.TransmissionHeaderSize).ToList<byte>();
    ushort uint16 = DataConverter.ToUInt16(list2.ToArray(), 0);
    int count1 = 2;
    list2.RemoveRange(0, count1);
    if ((int) uint16 != (int) this.transmissionHeaderSignal)
    {
      FlashDataPackage.logger.Error("The data package contains invalid data. The transmission signal should {0} but is {1}.", (object) this.transmissionHeaderSignal, (object) uint16);
      return ValidationState.InvalidData;
    }
    this.BlockSize = DataConverter.ToUInt32(list2.ToArray(), 0);
    int count2 = 4;
    list2.RemoveRange(0, count2);
    int num1 = 16 /*0x10*/;
    this.BlockHash = DataConverter.ToUInt8Array(list2.ToArray(), num1);
    list2.RemoveRange(0, num1);
    uint num2 = (uint) this.TransmissionHeaderSize - (uint) num1 + this.BlockSize;
    FlashDataPackage.logger.Debug("Checking if data package is complete. Current size {0} estimated size {1}", (object) list1.Count, (object) num2);
    if ((long) list1.Count < (long) num2)
      return ValidationState.NotEnoughData;
    if ((long) this.BlockSize <= (long) num1)
    {
      FlashDataPackage.logger.Debug("Received a tranmission without data.");
      return ValidationState.NoDataAvailable;
    }
    byte[] array = this.Items.Skip<byte>((int) this.TransmissionHeaderSize).ToArray<byte>();
    byte[] hash = MD5.Create().ComputeHash(array);
    if (((IEnumerable<byte>) hash).SequenceEqual<byte>((IEnumerable<byte>) this.BlockHash))
      return ValidationState.Valid;
    FlashDataPackage.logger.Error("Computed hash {0} of data package doesn't match the hash sent by the instrument {1}.", (object) hash.ToHex(), (object) this.BlockHash.ToHex());
    return ValidationState.ChecksumMismatch;
  }

  public override void Clear()
  {
    this.BlockHash = (byte[]) null;
    this.BlockSize = 0U;
    this.BlockNumber = 0U;
    base.Clear();
  }

  protected void BuildTransmissionHeader(byte[] data)
  {
    List<byte> byteList = new List<byte>();
    int num = 16 /*0x10*/;
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.transmissionHeaderSignal));
    this.BlockSize = (uint) (ushort) (data.Length + num);
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.BlockSize));
    this.BlockHash = MD5.Create().ComputeHash(data);
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray((object) this.BlockHash));
    this.Add(byteList.ToArray());
  }

  public List<FlashDataPackage> SplittPackage(int packageSize)
  {
    List<FlashDataPackage> flashDataPackageList = new List<FlashDataPackage>();
    int uint32 = (int) Convert.ToUInt32(Math.Ceiling((double) this.Elements.Count / (double) packageSize));
    uint num = 0;
    List<byte> elements = this.Elements;
    while (elements.Count > 0)
    {
      BinaryDataPackage dataPackage = new BinaryDataPackage();
      dataPackage.Add(elements.Take<byte>(packageSize).ToArray<byte>());
      FlashDataPackage flashDataPackage = new FlashDataPackage(dataPackage, false);
      flashDataPackage.BlockNumber = num;
      elements.RemoveRange(0, Math.Min(packageSize, elements.Count));
      flashDataPackageList.Add(flashDataPackage);
      ++num;
    }
    return flashDataPackageList;
  }
}
