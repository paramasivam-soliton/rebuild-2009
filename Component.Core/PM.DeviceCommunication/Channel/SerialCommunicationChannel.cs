// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Channel.SerialCommunicationChannel
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using PathMedical.Communication;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PathMedical.DeviceCommunication.Channel;

[DebuggerDisplay("Port = {portName} IsOpen = {(this.serialPortToUse!=null?this.serialPortToUse.IsOpen:false)}")]
public class SerialCommunicationChannel : ICommunicationChannel, IDisposable
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (SerialCommunicationChannel), "$Rev: 1087 $");
  private bool isInstanceDisposed;
  private SerialPort serialPortToUse;
  private object serialPortLocker;
  private string portName;

  public Encoding Encoding { get; set; }

  public string Name
  {
    get => this.portName;
    set
    {
      this.portName = Regex.Match(value, "^COM\\d*").Success ? value : throw new ArgumentException("The name of the port is not valid!");
    }
  }

  public CommunicationSpeedType Speed { get; set; }

  public event EventHandler<ChannelStateEventArgs> ChannelStateChangedEventHandler;

  public event EventHandler<ChannelDataReceivedEventArgs> ChannelDataReceivedEventHandler;

  public event EventHandler<ChannelControlEventArgs> ChannelControlEventHandler;

  public event EventHandler<EventArgs> ChannelTimeOutEventHandler;

  public SerialCommunicationChannel(string channelName)
  {
    this.portName = channelName;
    this.Speed = CommunicationSpeedType.Bps115200;
    this.serialPortLocker = new object();
  }

  public void Open()
  {
    if (string.IsNullOrEmpty(this.portName))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The communication channel that shall be opened is undefined. Please set the port name and try again."));
    if (this.serialPortToUse != null && this.serialPortToUse.IsOpen)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Communication channel {0} is already in use!", (object) this.portName));
    try
    {
      SerialCommunicationChannel.logger.Debug("Opening communication channel {0} with {1}.", (object) this.portName, (object) this.Speed);
      this.serialPortToUse = new SerialPort(this.portName, (int) this.Speed);
      this.serialPortToUse.Encoding = Encoding.UTF8;
      this.serialPortToUse.PinChanged += new SerialPinChangedEventHandler(this.SerialPortToUsePinChanged);
      this.serialPortToUse.DataReceived += new SerialDataReceivedEventHandler(this.SerialPortToUseDataReceived);
    }
    catch (IOException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't connect to the interface {0}.", (object) this.portName), (System.Exception) ex);
    }
    lock (this.serialPortLocker)
    {
      try
      {
        this.serialPortToUse.Open();
        SerialCommunicationChannel.logger.Info("Communication channel {0} has been opened successfully.", (object) this.portName);
      }
      catch (InvalidOperationException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (ArgumentException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (IOException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (UnauthorizedAccessException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0} due to permission rights.", (object) this.portName), (System.Exception) ex);
      }
      catch (SecurityException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't open the communication channel {0} due to permission rights.", (object) this.portName), (System.Exception) ex);
      }
    }
    if (this.ChannelStateChangedEventHandler == null)
      return;
    SerialCommunicationChannel.logger.Debug("Firing ChannelStateChanged event. State: {0}", (object) ChannelState.Open);
    this.ChannelStateChangedEventHandler((object) this, new ChannelStateEventArgs(ChannelState.Open));
  }

  public void Close()
  {
    this.RemoveSubscribers();
    if (this.serialPortToUse != null && this.serialPortToUse.IsOpen)
    {
      lock (this.serialPortLocker)
      {
        try
        {
          SerialCommunicationChannel.logger.Debug("Closing communication channel {0}.", (object) this.portName);
          this.serialPortToUse.Close();
          SerialCommunicationChannel.logger.Info("Communication channel {0} has been closed successfully.", (object) this.portName);
        }
        catch (ArgumentException ex)
        {
          throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't close communication channel {0}.", (object) this.portName), (System.Exception) ex);
        }
        catch (InvalidOperationException ex)
        {
          throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't close communication channel {0}.", (object) this.portName), (System.Exception) ex);
        }
        catch (IOException ex)
        {
          throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't close communication channel {0}.", (object) this.portName), (System.Exception) ex);
        }
        catch (System.Exception ex)
        {
          SerialCommunicationChannel.logger.Error(ex, "Can't close communication channel {0}.", (object) this.portName);
          throw;
        }
      }
    }
    if (this.ChannelStateChangedEventHandler == null)
      return;
    SerialCommunicationChannel.logger.Debug("Firing ChannelStateChanged event. State: {0}", (object) ChannelState.Closed);
    this.ChannelStateChangedEventHandler((object) this, new ChannelStateEventArgs(ChannelState.Closed));
  }

  public void Write(BinaryDataPackage data)
  {
    if (data == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (data));
    if (this.serialPortToUse == null && !this.serialPortToUse.IsOpen)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Serial port is not open!");
    byte[] array = data.Elements.ToArray();
    lock (this.serialPortLocker)
    {
      try
      {
        this.serialPortToUse.DiscardInBuffer();
        this.serialPortToUse.Write(array, 0, array.Length);
        SerialCommunicationChannel.logger.Debug("Sent out {0} bytes on communication channel {1}. Dump: {2}", (object) array.Length, (object) this.portName, (object) array.ToHex());
      }
      catch (ArgumentNullException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't write to the interface {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (InvalidOperationException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't write to the interface {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't write to the interface {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (ArgumentException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't write to the interface {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (TimeoutException ex)
      {
        throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't write to the interface {0}.", (object) this.portName), (System.Exception) ex);
      }
      catch (System.Exception ex)
      {
        SerialCommunicationChannel.logger.Error(ex, "Can't write to the interface {0}.", (object) this.portName);
        throw;
      }
    }
  }

  private void SerialPortToUsePinChanged(object sender, SerialPinChangedEventArgs eventArgs)
  {
    if (sender == null || eventArgs == null)
      return;
    SerialCommunicationChannel.logger.Debug("A Pin has been changed on {0}: {1}.", (object) this.portName, (object) eventArgs.EventType);
    SerialChannelControlEventArgs e = new SerialChannelControlEventArgs();
    if (this.ChannelControlEventHandler == null)
      return;
    switch (eventArgs.EventType)
    {
      case SerialPinChange.CtsChanged:
        e.ControlSignal = ControlType.None;
        break;
      case SerialPinChange.DsrChanged:
        e.ControlSignal = ControlType.None;
        break;
      case SerialPinChange.CDChanged:
        e.ControlSignal = ControlType.None;
        break;
      case SerialPinChange.Break:
        e.ControlSignal = ControlType.None;
        break;
      case SerialPinChange.Ring:
        e.ControlSignal = ControlType.Ring;
        break;
      default:
        e.ControlSignal = ControlType.None;
        break;
    }
    this.ChannelControlEventHandler((object) this, (ChannelControlEventArgs) e);
  }

  private void SerialPortToUseDataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
  {
    if (this.serialPortToUse == null)
      return;
    byte[] numArray = (byte[]) null;
    try
    {
      if (this.serialPortToUse.IsOpen)
      {
        if (this.serialPortToUse.BytesToRead > 0)
        {
          Thread.Sleep(50);
          numArray = new byte[this.serialPortToUse.BytesToRead];
          int num = this.serialPortToUse.Read(numArray, 0, numArray.Length);
          if (num != numArray.Length)
            SerialCommunicationChannel.logger.Error("Bytes actually read {0} < Bytes available {1}.", (object) num, (object) numArray.Length);
          SerialCommunicationChannel.logger.Debug("Received data on {0}. Dump: {1}", (object) this.portName, (object) numArray.ToHex());
        }
      }
    }
    catch (ArgumentNullException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't read from communication channel {0}.", (object) this.portName), (System.Exception) ex);
    }
    catch (ObjectDisposedException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while resetting timers for communication."), (System.Exception) ex);
    }
    catch (InvalidOperationException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't read from communication channel {0}.", (object) this.portName), (System.Exception) ex);
    }
    catch (ArgumentOutOfRangeException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't read from communication channel {0}.", (object) this.portName), (System.Exception) ex);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't read from communication channel {0}.", (object) this.portName), (System.Exception) ex);
    }
    catch (TimeoutException ex)
    {
      throw ExceptionFactory.Instance.CreateException<CommunicationChannelException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Can't read from communication channel {0}.", (object) this.portName), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      SerialCommunicationChannel.logger.Error(ex, "Can't read from communication channel {0}.", (object) this.portName);
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

  private void RemoveSubscribers()
  {
    if (this.ChannelDataReceivedEventHandler != null)
    {
      SerialCommunicationChannel.logger.Debug("Removing all subscribers of ChannelDataReceivedEventHandler");
      this.ChannelDataReceivedEventHandler = (EventHandler<ChannelDataReceivedEventArgs>) null;
    }
    if (this.ChannelTimeOutEventHandler == null)
      return;
    SerialCommunicationChannel.logger.Debug("Removing all subscribers of ChannelTimeOutEventHandler");
    this.ChannelTimeOutEventHandler = (EventHandler<EventArgs>) null;
  }

  public static string[] GetAvailablePortNames() => SerialPort.GetPortNames();

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
    if (this.serialPortToUse != null)
    {
      lock (this.serialPortLocker)
      {
        SerialCommunicationChannel.logger.Debug("Disposing instance for communication channel {0}.", (object) this.portName);
        this.serialPortToUse.Dispose();
      }
    }
    this.isInstanceDisposed = true;
  }
}
