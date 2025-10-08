// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.DeviceService.DeviceServiceStateEventArgs
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using System;

#nullable disable
namespace PathMedical.DeviceCommunication.DeviceService;

public class DeviceServiceStateEventArgs : EventArgs
{
  private ServiceState state;

  public ServiceState State
  {
    get => this.state;
    set => this.state = value;
  }

  public DeviceServiceStateEventArgs(ServiceState serviceState) => this.state = serviceState;
}
