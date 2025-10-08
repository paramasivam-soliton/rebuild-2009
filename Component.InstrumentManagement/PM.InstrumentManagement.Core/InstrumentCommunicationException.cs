// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.InstrumentCommunicationException
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.InstrumentManagement;

[DebuggerDisplay("Message: {Message} InnerException: {InnerException}")]
public class InstrumentCommunicationException : Exception
{
  public InstrumentCommunicationException()
  {
  }

  public InstrumentCommunicationException(string message)
    : base(message)
  {
  }

  public InstrumentCommunicationException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected InstrumentCommunicationException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
