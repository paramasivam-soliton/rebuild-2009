// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Command.CommandManagerException
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.DeviceCommunication.Command;

[Serializable]
public class CommandManagerException : Exception
{
  public CommandManagerException()
  {
  }

  public CommandManagerException(string message)
    : base(message)
  {
  }

  public CommandManagerException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected CommandManagerException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
