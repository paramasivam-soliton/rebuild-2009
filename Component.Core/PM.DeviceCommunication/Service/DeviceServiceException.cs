// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.DeviceService.DeviceServiceException
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.DeviceCommunication.DeviceService;

[Serializable]
public class DeviceServiceException : Exception
{
  public DeviceServiceException()
  {
  }

  public DeviceServiceException(string message)
    : base(message)
  {
  }

  public DeviceServiceException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected DeviceServiceException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
