// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.InstrumentCommand.BinaryInstrumentCommand
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Type1077.DataPackage;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.InstrumentCommand;

[CLSCompliant(false)]
public class BinaryInstrumentCommand
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (BinaryInstrumentCommand), "$Rev: 1403 $");
  private int blockSize;
  private readonly List<byte> data;

  public Guid Id { get; protected set; }

  public string Name { get; protected set; }

  public ushort ExecutionTimeout { get; set; }

  public ushort AcknowledgeTimeout { get; set; }

  public string ExecutionCommand { get; protected set; }

  public BinaryInstrumentCommandType ExecutionCommandType { get; protected set; }

  public int BlockSize
  {
    get => this.blockSize;
    set
    {
      if (value <= 0)
        return;
      this.blockSize = value;
    }
  }

  public BinaryBlockDataPackage[] DataBlocks => this.CreateDataBlocks();

  private BinaryInstrumentCommand()
  {
    this.AcknowledgeTimeout = (ushort) 5000;
    this.ExecutionTimeout = (ushort) 10000;
    this.BlockSize = 4096 /*0x1000*/;
    this.data = new List<byte>();
  }

  public BinaryInstrumentCommand(string command, BinaryInstrumentCommandType commandType)
    : this()
  {
    this.ExecutionCommand = !string.IsNullOrEmpty(command) ? command : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (command));
    this.ExecutionCommandType = commandType;
  }

  public BinaryInstrumentCommand(
    string name,
    string command,
    BinaryInstrumentCommandType commandType)
    : this(command, commandType)
  {
    this.Name = name;
  }

  public BinaryInstrumentCommand Append(byte[] rawData)
  {
    if (rawData.Length != 0)
    {
      BinaryInstrumentCommand.Logger.Debug("Appending raw data [{0}] for command [{1}]", (object) rawData.ToHex(), (object) this.ExecutionCommand);
      this.data.AddRange((IEnumerable<byte>) rawData);
    }
    return this;
  }

  private BinaryBlockDataPackage[] CreateDataBlocks()
  {
    List<BinaryBlockDataPackage> blockDataPackageList = new List<BinaryBlockDataPackage>();
    int count = this.blockSize - BinaryBlockDataPackage.TransmissionHeaderSize;
    if (count < BinaryBlockDataPackage.TransmissionHeaderSize)
      count = BinaryBlockDataPackage.TransmissionHeaderSize + this.blockSize;
    int int32 = Convert.ToInt32(Math.Ceiling((Decimal) this.data.Count / Convert.ToDecimal(count)));
    if (int32 == 0)
      ++int32;
    for (int blockNumber = 0; blockNumber < int32; ++blockNumber)
    {
      byte[] rawData = new byte[0];
      if (this.data != null && this.data.Count > 0)
        rawData = this.data.Skip<byte>(blockNumber * count).Take<byte>(count).ToArray<byte>();
      blockDataPackageList.Add(new BinaryBlockDataPackage(this.ExecutionCommand, blockNumber, int32, TransmissionStatusType.Request, rawData));
    }
    return blockDataPackageList.ToArray();
  }

  public override string ToString()
  {
    return $"BinaryInstrumentCommand {this.Name} [{this.ExecutionCommand}] type [{this.ExecutionCommandType}] acknowledge timeout [{this.AcknowledgeTimeout}] execution timeout [{this.ExecutionTimeout}] data [{(this.data != null ? (object) this.data.ToArray().ToHex() : (object) "<No Data>")}]";
  }
}
