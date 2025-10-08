// Decompiled with JetBrains decompiler
// Type: PathMedical.Communication.CommunicationChannelException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.Communication;

[Serializable]
public class CommunicationChannelException : Exception
{
  public CommunicationChannelException()
  {
  }

  public CommunicationChannelException(string message)
    : base(message)
  {
  }

  public CommunicationChannelException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected CommunicationChannelException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
