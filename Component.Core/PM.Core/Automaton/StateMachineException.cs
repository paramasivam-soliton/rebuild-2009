// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.StateMachineException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.Automaton;

[DebuggerDisplay("Message: {Message} InnerException: {InnerException}")]
public class StateMachineException : Exception
{
  public StateMachineException()
  {
  }

  public StateMachineException(string message)
    : base(message)
  {
  }

  public StateMachineException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected StateMachineException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
