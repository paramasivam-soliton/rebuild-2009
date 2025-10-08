// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077CommandManager
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.DeviceCommunication.Command;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.InstrumentCommunicator.DataPackage;
using PathMedical.Logging;
using PathMedical.ResourceManager;
using PathMedical.Type1077.DataPackage;
using PathMedical.Type1077.InstrumentCommand;
using PathMedical.Type1077.Properties;
using PathMedical.WatchDog;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PathMedical.Type1077;

[CLSCompliant(false)]
public class Type1077CommandManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (Type1077CommandManager), "$Rev: 1160 $");
  private readonly object stateLock = new object();
  private CommandState state;
  private bool communicationChannelReady;
  private BinaryBlockDataPackage[] dataBlocksToTransmit;
  private int currentDataBlock;
  private byte[] commandDataBlockToTransmit;
  private PathMedical.Communication.DataPackage<byte> uncompleteReceivedData;
  private List<byte> commandData;
  private int blockRetransmissions;
  private TransmissionStatusType lastTransmittedStatusBlock;
  private const int MaximumNumberOfBlockRetransmission = 4;

  public event EventHandler<CommandStateEventArgs> CommandStateChangingEventHandler;

  public event EventHandler<ChannelDataReceivedEventArgs> CommandDataAvailableEventHandler;

  public CommandState State
  {
    get => this.state;
    protected set
    {
      if (this.state.Equals((object) value))
        return;
      Type1077CommandManager.Logger.Debug("Changing command state from [{0}] to [{1}] for command [{2}]", (object) this.state, (object) value, (object) this.Command.ExecutionCommand);
      if (this.CommandStateChangingEventHandler != null)
        this.CommandStateChangingEventHandler((object) this, new CommandStateEventArgs(this.state, value));
      lock (this.stateLock)
        this.state = value;
    }
  }

  public Type1077Instrument Instrument { get; set; }

  public ICommunicationChannel CommunicationChannel { get; set; }

  public BinaryInstrumentCommand Command { get; set; }

  protected Thread CommandThread { get; set; }

  protected WatchDogTimer WatchDog { get; set; }

  public void Connect()
  {
    if (this.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Communication channel is undefined. Command can't be executed.");
    try
    {
      this.CommunicationChannel.ChannelStateChangedEventHandler += new EventHandler<ChannelStateEventArgs>(this.ChannelStateChangedEventHandler);
      this.CommunicationChannel.Open();
    }
    catch (CommunicationChannelException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommandManagerException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CantConnectToInstrument"), (System.Exception) ex);
    }
  }

  public void Disconnect()
  {
    if (this.CommunicationChannel == null)
      return;
    try
    {
      this.CommunicationChannel.Close();
    }
    catch (CommunicationChannelException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommandManagerException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CantDisconnectFromInstrument"), (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public void Execute(BinaryInstrumentCommand command)
  {
    if (this.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CommunicationChannelUndefined"));
    if (command == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CommandForExecutionUndefined"));
    if (!this.communicationChannelReady)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CommunicationChannelNotReady"));
    this.Command = command;
    this.ExecuteCommand();
  }

  public void Abort() => throw new NotImplementedException();

  private void ExecuteCommand()
  {
    this.State = CommandState.Unknown;
    if (this.WatchDog != null)
    {
      this.WatchDog.DeActivate();
      this.WatchDog = (WatchDogTimer) null;
    }
    this.WatchDog = new WatchDogTimer((int) this.Command.AcknowledgeTimeout, new Action(this.HandleCommunicationTimeout));
    this.WatchDog.Activate();
    this.commandData = new List<byte>();
    this.uncompleteReceivedData = new PathMedical.Communication.DataPackage<byte>();
    this.dataBlocksToTransmit = this.Command.DataBlocks;
    this.currentDataBlock = 0;
    this.commandDataBlockToTransmit = this.dataBlocksToTransmit[this.currentDataBlock].PackageBlock;
    this.Transmit(this.commandDataBlockToTransmit);
    while (this.state == CommandState.Unknown || this.state == CommandState.SentOut || this.state == CommandState.SendingOut)
      Thread.Sleep(1);
    if (this.WatchDog == null)
      return;
    this.WatchDog.DeActivate();
    this.WatchDog = (WatchDogTimer) null;
  }

  private void Transmit(byte[] dataBlock)
  {
    Type1077CommandManager.Logger.Debug("Sending data package block [{0}] of [{1}]. Block size [{2}].", (object) (this.currentDataBlock + 1), (object) this.dataBlocksToTransmit.Length, (object) this.dataBlocksToTransmit[this.currentDataBlock].PackageBlock.Length);
    BinaryDataPackage dataToWrite = new BinaryDataPackage(dataBlock);
    try
    {
      this.State = CommandState.SendingOut;
      this.CommunicationChannel.Write(dataToWrite);
      Thread.Sleep(1);
      if (this.State != CommandState.SendingOut)
        return;
      this.State = CommandState.SentOut;
    }
    catch (CommunicationChannelException ex)
    {
      Type1077CommandManager.Logger.Error((System.Exception) ex, "Failure while writing data to [{0}]", (object) this.CommunicationChannel);
      this.State = CommandState.Failed;
    }
  }

  private void ReTransmitStatusBlock() => this.TransmitStatusBlock(this.lastTransmittedStatusBlock);

  private void TransmitStatusBlock(TransmissionStatusType status)
  {
    this.lastTransmittedStatusBlock = status;
    string command = string.Empty;
    if (this.dataBlocksToTransmit != null && this.dataBlocksToTransmit.Length != 0 && this.dataBlocksToTransmit[this.currentDataBlock] != null && this.Command.ExecutionCommand.Equals("BCGF"))
      command = this.dataBlocksToTransmit[this.currentDataBlock].Command;
    if (!string.IsNullOrEmpty(command))
    {
      BinaryBlockDataPackage blockDataPackage = new BinaryBlockDataPackage(command, 0, 1, status, (byte[]) null);
      if (status != TransmissionStatusType.Acknowledged && status != TransmissionStatusType.Request)
        Type1077CommandManager.Logger.Info("Sending [{3}] data block for command [{0}] (block [{1}] of [{2}]) to instrument.", (object) command, (object) (this.currentDataBlock + 1), (object) (this.dataBlocksToTransmit != null ? this.dataBlocksToTransmit.Length : 0), (object) status);
      else
        Type1077CommandManager.Logger.Info("Sending [{3}] data block for command [{0}] (block [{1}] of [{2}]) to instrument.", (object) command, (object) (this.currentDataBlock + 1), (object) (this.dataBlocksToTransmit != null ? this.dataBlocksToTransmit.Length : 0), (object) status);
      this.Transmit(blockDataPackage.PackageBlock);
    }
    else
      Type1077CommandManager.Logger.Debug("Request to send status to instrument refused because no command is active that requires to send an acknowledge");
  }

  private void ChannelDataReceivedEventHandler(object sender, ChannelDataReceivedEventArgs e)
  {
    if (this.WatchDog != null && this.Command != null)
      this.WatchDog.Restart((int) this.Command.ExecutionTimeout);
    if (e == null || e.DataPackage == null || e.DataPackage.Elements.Count <= 0 || this.currentDataBlock >= this.dataBlocksToTransmit.Length)
      return;
    if (this.State == CommandState.TransmissionError || this.State == CommandState.Failed || this.State == CommandState.Performed)
    {
      Type1077CommandManager.Logger.Debug("Ignoring data because command [{0}] is in status [{1}]. Ignored data dump [{2}]", this.Command != null ? (object) this.Command.ExecutionCommand : (object) string.Empty, (object) this.State, (object) e.DataPackage.Elements.ToArray().ToHex());
    }
    else
    {
      this.uncompleteReceivedData.Add(e.DataPackage.Elements.ToArray());
      BinaryBlockDataPackage blockDataPackage = new BinaryBlockDataPackage(this.uncompleteReceivedData.Elements.ToArray());
      switch (blockDataPackage.Validate(this.Command))
      {
        case ValidationState.ChecksumMismatch:
          this.uncompleteReceivedData.Clear();
          ++this.blockRetransmissions;
          if (this.blockRetransmissions <= 4)
          {
            Type1077CommandManager.Logger.Info("Received data package with wrong checksum from instrument. Requesting retransmission [{0}/{1}] for block {2} of {3} [block size {4}] from instrument because received data package has status \"{5}\" .", (object) this.blockRetransmissions, (object) 4, (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) blockDataPackage.StatusCode);
            this.TransmitStatusBlock(TransmissionStatusType.NotAcknowledged);
            break;
          }
          Type1077CommandManager.Logger.Error("Received data package with wrong checksum from instrument. The maximum number of retransmission [{1}] has been reached. Request to abort communication at block [{2}] of [{3}] Block size [{4}] from instrument. Last received package has had status [{5}].", (object) this.blockRetransmissions, (object) 4, (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) blockDataPackage.StatusCode);
          this.TransmitStatusBlock(TransmissionStatusType.Abort);
          this.State = CommandState.TransmissionError;
          break;
        case ValidationState.Valid:
          this.uncompleteReceivedData.Clear();
          this.blockRetransmissions = 0;
          Type1077CommandManager.Logger.Debug("Received valid data package for block [{0}] of [{1}] block size [{2}] from instrument. Status is [{3}].", (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) blockDataPackage.StatusCode);
          if (this.Command != null && this.Command.ExecutionCommandType == BinaryInstrumentCommandType.SendingDataCommand)
          {
            switch (blockDataPackage.StatusCode)
            {
              case TransmissionStatusType.Request:
                return;
              case TransmissionStatusType.Acknowledged:
                ++this.currentDataBlock;
                if (this.currentDataBlock < this.dataBlocksToTransmit.Length)
                {
                  this.commandDataBlockToTransmit = this.dataBlocksToTransmit[this.currentDataBlock].PackageBlock;
                  this.Transmit(this.commandDataBlockToTransmit);
                  return;
                }
                Type1077CommandManager.Logger.Debug("Command [{0}] has been completed. Received data [{1}]", (object) this.Command.ExecutionCommand, (object) this.commandData.ToArray().ToHex());
                this.State = CommandState.Performed;
                return;
              case TransmissionStatusType.NotAcknowledged:
                this.Transmit(this.dataBlocksToTransmit[this.currentDataBlock].PackageBlock);
                return;
              case TransmissionStatusType.Abort:
                Type1077CommandManager.Logger.Info("Instrument is signaling an communication abort while exchanging block [{0}] of [{1}] block size [{2}] [retransmission {3}/{4}]. Received status code for block is [{5}].", (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) this.blockRetransmissions, (object) 4, (object) blockDataPackage.StatusCode);
                this.State = CommandState.TransmissionError;
                return;
              default:
                return;
            }
          }
          else
          {
            switch (blockDataPackage.StatusCode)
            {
              case TransmissionStatusType.Request:
                return;
              case TransmissionStatusType.Acknowledged:
                this.commandData.AddRange((IEnumerable<byte>) blockDataPackage.DataBlock);
                if ((int) blockDataPackage.BlockNumber < (int) blockDataPackage.TotalBlocks)
                  this.TransmitStatusBlock(TransmissionStatusType.Acknowledged);
                if ((int) blockDataPackage.BlockNumber != (int) blockDataPackage.TotalBlocks - 1)
                  return;
                if (this.WatchDog != null)
                {
                  this.WatchDog.DeActivate();
                  this.WatchDog = (WatchDogTimer) null;
                }
                if (this.CommandDataAvailableEventHandler != null)
                {
                  BinaryDataPackage dataPackage = new BinaryDataPackage(this.commandData.ToArray());
                  Type1077CommandManager.Logger.Debug("Command [{0}] has been completed. Received data [{1}]", (object) this.Command, (object) this.commandData.ToArray().ToHex());
                  try
                  {
                    Type1077CommandManager.Logger.Debug("Informing subscribers about new data.");
                    this.CommandDataAvailableEventHandler((object) this, new ChannelDataReceivedEventArgs(dataPackage));
                  }
                  catch (System.Exception ex)
                  {
                    Type1077CommandManager.Logger.Error(ex, "Failure while informing subscribers about command data.");
                    this.State = CommandState.Failed;
                  }
                }
                this.State = CommandState.Performed;
                return;
              case TransmissionStatusType.NotAcknowledged:
                if ((int) blockDataPackage.BlockNumber < (int) blockDataPackage.TotalBlocks - 1)
                {
                  this.Transmit(this.dataBlocksToTransmit[this.currentDataBlock].PackageBlock);
                  return;
                }
                this.ReTransmitStatusBlock();
                return;
              case TransmissionStatusType.Abort:
                this.State = CommandState.InstrumentAbort;
                return;
              default:
                return;
            }
          }
        case ValidationState.InvalidData:
          this.uncompleteReceivedData.Clear();
          ++this.blockRetransmissions;
          if (this.blockRetransmissions <= 4)
          {
            Type1077CommandManager.Logger.Info("Received invalid data package from instrument. Requesting retransmission [{0}/{1}] for block [{2}] of [{3}] block size [{4}] from instrument. Last received data package has status [{5}].", (object) this.blockRetransmissions, (object) 4, (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) blockDataPackage.StatusCode);
            this.TransmitStatusBlock(TransmissionStatusType.NotAcknowledged);
            break;
          }
          Type1077CommandManager.Logger.Error("Received invalid data package from instrument. The maximum number of retransmission [{1}] has been reached. Request to abort communication at block [{2}] of [{3}] block size [{4}] from instrument. Last received package has status [{5}].", (object) this.blockRetransmissions, (object) 4, (object) ((int) blockDataPackage.BlockNumber + 1), (object) blockDataPackage.TotalBlocks, (object) blockDataPackage.BlockSize, (object) blockDataPackage.StatusCode);
          this.TransmitStatusBlock(TransmissionStatusType.Abort);
          this.State = CommandState.TransmissionError;
          break;
      }
    }
  }

  private void ChannelStateChangedEventHandler(object sender, ChannelStateEventArgs e)
  {
    if (e == null)
      return;
    switch (e.State)
    {
      case ChannelState.Unknown:
        if (this.communicationChannelReady)
        {
          this.CommunicationChannel.ChannelDataReceivedEventHandler -= new EventHandler<ChannelDataReceivedEventArgs>(this.ChannelDataReceivedEventHandler);
          this.CommunicationChannel.ChannelStateChangedEventHandler -= new EventHandler<ChannelStateEventArgs>(this.ChannelStateChangedEventHandler);
        }
        this.communicationChannelReady = false;
        break;
      case ChannelState.Closed:
        if (this.communicationChannelReady)
        {
          this.CommunicationChannel.ChannelDataReceivedEventHandler -= new EventHandler<ChannelDataReceivedEventArgs>(this.ChannelDataReceivedEventHandler);
          this.CommunicationChannel.ChannelStateChangedEventHandler -= new EventHandler<ChannelStateEventArgs>(this.ChannelStateChangedEventHandler);
        }
        this.communicationChannelReady = false;
        break;
      case ChannelState.Open:
        if (!this.communicationChannelReady)
          this.CommunicationChannel.ChannelDataReceivedEventHandler += new EventHandler<ChannelDataReceivedEventArgs>(this.ChannelDataReceivedEventHandler);
        this.communicationChannelReady = true;
        break;
      default:
        if (this.communicationChannelReady)
        {
          this.CommunicationChannel.ChannelDataReceivedEventHandler -= new EventHandler<ChannelDataReceivedEventArgs>(this.ChannelDataReceivedEventHandler);
          this.CommunicationChannel.ChannelStateChangedEventHandler -= new EventHandler<ChannelStateEventArgs>(this.ChannelStateChangedEventHandler);
        }
        this.communicationChannelReady = false;
        break;
    }
    if (this.communicationChannelReady || this.WatchDog == null)
      return;
    this.WatchDog.DeActivate();
    this.WatchDog = (WatchDogTimer) null;
  }

  private void HandleCommunicationTimeout()
  {
    Guid guid = this.WatchDog != null ? this.WatchDog.Id : Guid.Empty;
    Type1077CommandManager.Logger.Error(string.Format("Watchdog [{3}] detected a timeout for command [{0}/{1}] running on communication channel [{2}].", (object) this.Command.Name, (object) this.Command.Id, (object) this.CommunicationChannel.Name, (object) guid));
    if (this.WatchDog != null)
      this.WatchDog.DeActivate();
    this.State = CommandState.TimedOut;
  }
}
