// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataPackage.BinaryBlockDataPackage
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.Extensions;
using PathMedical.InstrumentCommunicator.DataPackage;
using PathMedical.Logging;
using PathMedical.Type1077.DataExchange;
using PathMedical.Type1077.InstrumentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace PathMedical.Type1077.DataPackage;

[CLSCompliant(false)]
public class BinaryBlockDataPackage : BinaryDataPackage
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (BinaryBlockDataPackage), "$Rev: 1091 $");
  public static int TransmissionHeaderSize = 30;
  private const ushort StandardTransmissionHeaderSignal = 9320;

  public string Command { get; protected set; }

  public ushort TransmissionHeaderSignal { get; protected set; }

  public ushort BlockSize { get; protected set; }

  public ushort BlockNumber { get; set; }

  public ushort TotalBlocks { get; set; }

  public TransmissionStatusType StatusCode { get; protected set; }

  public byte[] CheckSum { get; protected set; }

  public byte[] DataBlock { get; set; }

  public byte[] PackageBlock => this.CreatePackageBlock();

  private BinaryBlockDataPackage()
  {
    this.TransmissionHeaderSignal = (ushort) 0;
    this.BlockNumber = (ushort) 0;
    this.TotalBlocks = (ushort) 0;
    this.StatusCode = TransmissionStatusType.Request;
    this.DataBlock = new byte[0];
    this.CheckSum = new byte[16 /*0x10*/];
  }

  public BinaryBlockDataPackage(
    string command,
    int blockNumber,
    int totalBlocks,
    TransmissionStatusType status,
    byte[] rawData)
    : this()
  {
    byte[] numArray = rawData ?? new byte[0];
    this.Command = command;
    this.TransmissionHeaderSignal = (ushort) 9320;
    this.BlockNumber = Convert.ToUInt16(blockNumber);
    this.TotalBlocks = Convert.ToUInt16(totalBlocks);
    this.StatusCode = status;
    this.BlockSize = Convert.ToUInt16(numArray.Length);
    this.DataBlock = numArray;
    if (numArray.Length == 0)
      return;
    this.CheckSum = MD5.Create().ComputeHash(this.DataBlock);
  }

  public BinaryBlockDataPackage(byte[] rawData)
    : this()
  {
    if (rawData.Length < BinaryBlockDataPackage.TransmissionHeaderSize)
      return;
    int count1 = 0;
    int count2;
    this.Command = DataConverter.ToString(((IEnumerable<byte>) rawData).Skip<byte>(count1).Take<byte>(count2 = count1 + 4).ToArray<byte>(), Encoding.ASCII);
    int count3;
    this.TransmissionHeaderSignal = DataConverter.ToUInt16(((IEnumerable<byte>) rawData).Skip<byte>(count2).Take<byte>(count3 = count2 + 2).ToArray<byte>(), 0);
    int count4;
    this.BlockSize = DataConverter.ToUInt16(((IEnumerable<byte>) rawData).Skip<byte>(count3).Take<byte>(count4 = count3 + 2).ToArray<byte>(), 0);
    int count5;
    this.BlockNumber = DataConverter.ToUInt16(((IEnumerable<byte>) rawData).Skip<byte>(count4).Take<byte>(count5 = count4 + 2).ToArray<byte>(), 0);
    int count6;
    this.TotalBlocks = DataConverter.ToUInt16(((IEnumerable<byte>) rawData).Skip<byte>(count5).Take<byte>(count6 = count5 + 2).ToArray<byte>(), 0);
    int count7;
    this.StatusCode = (TransmissionStatusType) DataConverter.ToUInt16(((IEnumerable<byte>) rawData).Skip<byte>(count6).Take<byte>(count7 = count6 + 2).ToArray<byte>(), 0);
    this.CheckSum = DataConverter.ToUInt8Array(((IEnumerable<byte>) rawData).Skip<byte>(count7).Take<byte>(count7 + 16 /*0x10*/).ToArray<byte>(), 16 /*0x10*/);
    if (rawData.Length < BinaryBlockDataPackage.TransmissionHeaderSize + (int) this.BlockSize)
      return;
    this.DataBlock = ((IEnumerable<byte>) rawData).Skip<byte>(BinaryBlockDataPackage.TransmissionHeaderSize).Take<byte>((int) this.BlockSize).ToArray<byte>();
  }

  public ValidationState Validate(BinaryInstrumentCommand command)
  {
    if (this.TransmissionHeaderSignal == (ushort) 0)
      return ValidationState.NotEnoughData;
    List<byte> list = ((IEnumerable<byte>) this.DataBlock).ToList<byte>();
    if (!this.Command.Equals(command.ExecutionCommand) || this.TransmissionHeaderSignal != (ushort) 9320)
      return ValidationState.InvalidData;
    if (list.Count < (int) this.BlockSize)
      return ValidationState.NotEnoughData;
    if (this.DataBlock.Length != 0)
    {
      byte[] hash = MD5.Create().ComputeHash(this.DataBlock);
      if (!((IEnumerable<byte>) hash).SequenceEqual<byte>((IEnumerable<byte>) this.CheckSum))
      {
        BinaryBlockDataPackage.Logger.Error("Computed hash {0} of data package doesn't match the hash sent by the instrument {1}.", (object) hash.ToHex(), (object) this.CheckSum.ToHex());
        return ValidationState.ChecksumMismatch;
      }
    }
    return ValidationState.Valid;
  }

  protected byte[] CreatePackageBlock()
  {
    List<byte> byteList = new List<byte>();
    byteList.AddRange(((IEnumerable<byte>) Encoding.ASCII.GetBytes(this.Command)).Take<byte>(4));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.TransmissionHeaderSignal));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.DataBlock.Length)));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.BlockNumber)));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.TotalBlocks)));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16((object) this.StatusCode)));
    byteList.AddRange((IEnumerable<byte>) this.CheckSum);
    if (this.DataBlock.Length != 0)
      byteList.AddRange((IEnumerable<byte>) this.DataBlock);
    return byteList.ToArray();
  }
}
