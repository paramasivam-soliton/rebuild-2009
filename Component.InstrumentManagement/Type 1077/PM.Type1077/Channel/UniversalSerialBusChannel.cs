// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Channel.UniversalSerialBusChannel
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using FTD2XX_NET;
using PathMedical.Communication;
using PathMedical.DeviceCommunication.Channel;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Type1077.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

#nullable disable
namespace PathMedical.Type1077.Channel;

[CLSCompliant(false)]
public class UniversalSerialBusChannel : ICommunicationChannel, IDisposable
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (UniversalSerialBusChannel), "$Rev: 1571 $");
  private bool isInstanceDisposed;
  private readonly object portLocker;
  private FTDI portToUse;
  private BackgroundWorker observeInterfaceWorker;
  private readonly EventWaitHandle dataReceivedHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
  private int sharedUsages;
  private Guid sharedUsageIdentifier;

  public Encoding Encoding { get; set; }

  public string Name { get; set; }

  [CLSCompliant(false)]
  public CommunicationSpeedType Speed { get; set; }

  public event EventHandler<ChannelStateEventArgs> ChannelStateChangedEventHandler;

  public event EventHandler<ChannelDataReceivedEventArgs> ChannelDataReceivedEventHandler;

  public event EventHandler<ChannelControlEventArgs> ChannelControlEventHandler;

  public event EventHandler<EventArgs> ChannelTimeOutEventHandler;

  public UniversalSerialBusChannel(string serialNumber)
  {
    this.Name = serialNumber;
    this.portLocker = new object();
    this.Speed = CommunicationSpeedType.Bps115200;
    this.sharedUsages = 0;
  }

  public static string[] GetAvailablePortNames()
  {
    List<string> stringList = new List<string>();
    FTDI ftdi = new FTDI();
    uint objA = 0;
    if (ftdi.GetNumberOfDevices(ref objA) != null || object.Equals((object) objA, (object) 0))
      return stringList.ToArray();
    FTDI.FT_DEVICE_INFO_NODE[] ftDeviceInfoNodeArray = new FTDI.FT_DEVICE_INFO_NODE[(int) objA];
    if (ftdi.GetDeviceList(ftDeviceInfoNodeArray) != null)
      return stringList.ToArray();
    for (int index = 0; index < ftDeviceInfoNodeArray.Length; ++index)
      stringList.Add(Convert.ToString(ftDeviceInfoNodeArray[index].LocId));
    return stringList.ToArray();
  }

  public void Open()
  {
    if (string.IsNullOrEmpty(this.Name))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.UniversalSerialBusChannel_OpenPortFailed));
    lock (this.portLocker)
      ++this.sharedUsages;
    if (this.portToUse != null && this.portToUse.IsOpen)
    {
      UniversalSerialBusChannel.Logger.Debug($"Starting shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. Opened shared usage {this.sharedUsages}");
      if (this.ChannelStateChangedEventHandler == null)
        return;
      UniversalSerialBusChannel.Logger.Debug("Firing ChannelStateChanged event. State [{0}]", (object) ChannelState.Open);
      this.ChannelStateChangedEventHandler((object) this, new ChannelStateEventArgs(ChannelState.Open));
    }
    else
    {
      lock (this.portLocker)
      {
        try
        {
          UniversalSerialBusChannel.Logger.Debug("Opening communication channel [{0}]. Speed [{1}].", (object) this.Name, (object) this.Speed);
          this.portToUse = new FTDI();
          this.portToUse.OpenByLocation((uint) Convert.ToUInt16(this.Name));
          this.portToUse.Purge(1U);
          this.portToUse.Purge(2U);
          this.portToUse.SetBaudRate((uint) this.Speed);
          this.portToUse.SetEventNotification(1U, this.dataReceivedHandle);
          DateTime now = DateTime.Now;
          while (!this.portToUse.IsOpen)
          {
            Thread.Sleep(1);
            if (DateTime.Now.Subtract(now).TotalSeconds > 5.0)
              throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format(Resources.UniversalSerialBusChannel_DriverOpenPortFailure, (object) this.Name));
          }
          this.sharedUsageIdentifier = Guid.NewGuid();
          UniversalSerialBusChannel.Logger.Debug($"Created shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. Opened shared usage {this.sharedUsages}.");
        }
        catch (FTDI.FT_EXCEPTION ex)
        {
          UniversalSerialBusChannel.Logger.Error((System.Exception) ex, "Failure while opening USB port");
          throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format(Resources.UniversalSerialBusChannel_FailureOpenUSB, (object) this.Name));
        }
      }
      this.observeInterfaceWorker = new BackgroundWorker();
      this.observeInterfaceWorker.DoWork += new DoWorkEventHandler(this.ObservePort);
      this.observeInterfaceWorker.RunWorkerAsync();
      if (this.ChannelStateChangedEventHandler == null)
        return;
      UniversalSerialBusChannel.Logger.Debug("Firing ChannelStateChanged event. State [{0}]", (object) ChannelState.Open);
      this.ChannelStateChangedEventHandler((object) this, new ChannelStateEventArgs(ChannelState.Open));
    }
  }

  public void Close()
  {
    bool flag = true;
    lock (this.portLocker)
    {
      --this.sharedUsages;
      if (this.sharedUsages > 1)
      {
        flag = false;
        UniversalSerialBusChannel.Logger.Debug($"Closed shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. {this.sharedUsages} unfinished communication scopes remaining.");
      }
    }
    if (!flag)
      return;
    this.RemoveSubscribers();
    if (this.portToUse != null && this.portToUse.IsOpen)
    {
      lock (this.portLocker)
      {
        try
        {
          UniversalSerialBusChannel.Logger.Debug("Closing communication channel [{0}].", (object) this.Name);
          this.portToUse.Close();
          Thread.Sleep(50);
          UniversalSerialBusChannel.Logger.Debug($"Closed shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. {this.sharedUsages} unfinished communication scopes remaining.");
          UniversalSerialBusChannel.Logger.Debug("Communication channel [{0}] has been closed successfully.", (object) this.Name);
        }
        catch (FTDI.FT_EXCEPTION ex)
        {
          UniversalSerialBusChannel.Logger.Debug((System.Exception) ex, $"Failure while closing shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. {this.sharedUsages} unfinished communication scopes remaining.");
          throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format(Resources.UniversalSerialBusChannel_ClosingCommunicationChannel, (object) this.Name));
        }
        catch (System.Exception ex)
        {
          UniversalSerialBusChannel.Logger.Debug(ex, $"Failure while closing shared communication scope for {this.Name} with ID {this.sharedUsageIdentifier}. {this.sharedUsages} unfinished communication scopes remaining.");
          throw;
        }
      }
    }
    if (this.ChannelStateChangedEventHandler == null)
      return;
    UniversalSerialBusChannel.Logger.Debug("Firing ChannelStateChanged event. State [{0}]", (object) ChannelState.Closed);
    this.ChannelStateChangedEventHandler((object) this, new ChannelStateEventArgs(ChannelState.Closed));
  }

  public void Write(BinaryDataPackage data)
  {
    if (data == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (data));
    if (this.portToUse == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Communication channel is undefined!");
    if (!this.portToUse.IsOpen)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Communication channel is not open!");
    byte[] array = data.Elements.ToArray();
    uint num = 0;
    lock (this.portLocker)
    {
      try
      {
        this.portToUse.Write(array, array.Length, ref num);
        UniversalSerialBusChannel.Logger.Debug("Sent out [{0}] bytes on communication channel [{1}]. Dump [{2}]", (object) array.Length, (object) this.Name, (object) array.ToHex());
      }
      catch (FTDI.FT_EXCEPTION ex)
      {
        UniversalSerialBusChannel.Logger.Error((System.Exception) ex, "Failure while writing to communication channel");
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format(Resources.UniversalSerialBusChannel_ClosingCommunicationChannel, (object) this.Name));
      }
      catch (System.Exception ex)
      {
        UniversalSerialBusChannel.Logger.Error(ex, "Can't write to communication channel [{0}].", (object) this.Name);
        throw;
      }
    }
  }

  private void ObservePort(object sender, DoWorkEventArgs e)
  {
    while (this.portToUse.IsOpen && this.dataReceivedHandle != null)
    {
      this.dataReceivedHandle.WaitOne();
      try
      {
        this.ChannelDataReceived();
      }
      catch (CommunicationChannelException ex)
      {
        UniversalSerialBusChannel.Logger.Error((System.Exception) ex, "Failure while reading from [{0}].", (object) this.Name);
      }
      catch (System.Exception ex)
      {
        UniversalSerialBusChannel.Logger.Error(ex, "Failure while reading from [{0}].", (object) this.Name);
        this.Close();
      }
    }
  }

  private void ChannelDataReceived()
  {
    if (this.portToUse == null)
      return;
    byte[] numArray = (byte[]) null;
    uint length = 0;
    uint num = 0;
    try
    {
      if (this.portToUse.IsOpen)
      {
        if (this.portToUse.GetRxBytesAvailable(ref length) == null)
        {
          if (length > 0U)
          {
            Thread.Sleep(20);
            numArray = new byte[(int) length];
            if (this.portToUse.Read(numArray, length, ref num) == null)
            {
              if ((int) length != (int) num)
                UniversalSerialBusChannel.Logger.Error("Bytes actually read [{0}] < Bytes available [{1}].", (object) num, (object) length);
              UniversalSerialBusChannel.Logger.Debug("Received data on [{0}]. Dump [{1}]", (object) this.Name, (object) numArray.ToHex());
            }
          }
        }
      }
    }
    catch (FTDI.FT_EXCEPTION ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format(Resources.UniversalSerialBusChannel_ChannelReadingError, (object) this.Name), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      UniversalSerialBusChannel.Logger.Error(ex, "Can't read from communication channel [{0}].", (object) this.Name);
      throw;
    }
    if (numArray == null || numArray.Length == 0 || this.ChannelDataReceivedEventHandler == null)
      return;
    BinaryDataPackage dataPackage = new BinaryDataPackage();
    dataPackage.Add(numArray);
    if (this.ChannelDataReceivedEventHandler == null)
      return;
    this.ChannelDataReceivedEventHandler((object) this, new ChannelDataReceivedEventArgs(dataPackage));
  }

  public void Dispose()
  {
    if (this.isInstanceDisposed)
      return;
    this.Dispose(true);
  }

  protected virtual void Dispose(bool clearAllResources)
  {
    if (!clearAllResources)
      return;
    if (this.portToUse != null)
    {
      lock (this.portLocker)
      {
        UniversalSerialBusChannel.Logger.Debug("Disposing instance for communication channel [{0}].", (object) this.Name);
        this.portToUse = (FTDI) null;
      }
    }
    this.isInstanceDisposed = true;
  }

  private void RemoveSubscribers()
  {
    if (this.ChannelDataReceivedEventHandler != null)
    {
      UniversalSerialBusChannel.Logger.Debug("Removing all subscribers of ChannelDataReceivedEventHandler");
      this.ChannelDataReceivedEventHandler = (EventHandler<ChannelDataReceivedEventArgs>) null;
    }
    if (this.ChannelTimeOutEventHandler == null)
      return;
    UniversalSerialBusChannel.Logger.Debug("Removing all subscribers of ChannelTimeOutEventHandler");
    this.ChannelTimeOutEventHandler = (EventHandler<EventArgs>) null;
  }
}
