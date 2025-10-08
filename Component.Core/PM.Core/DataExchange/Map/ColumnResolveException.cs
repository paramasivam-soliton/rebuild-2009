// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.ColumnResolveException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.DataExchange.Map;

[DebuggerDisplay("Message: {Message} InnerException: {InnerException}")]
public class ColumnResolveException : Exception
{
  public ColumnResolveException()
  {
  }

  public ColumnResolveException(string message)
    : base(message)
  {
  }

  public ColumnResolveException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected ColumnResolveException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
