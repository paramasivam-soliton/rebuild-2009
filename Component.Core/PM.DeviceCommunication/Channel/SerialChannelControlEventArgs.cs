// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Channel.SerialChannelControlEventArgs
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using PathMedical.Communication;

#nullable disable
namespace PathMedical.DeviceCommunication.Channel;

public class SerialChannelControlEventArgs : ChannelControlEventArgs
{
  private ControlType controlSignal;

  public ControlType ControlSignal
  {
    get => this.controlSignal;
    set => this.controlSignal = value;
  }
}
