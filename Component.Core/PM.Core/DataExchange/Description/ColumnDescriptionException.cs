// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.ColumnDescriptionException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.DataExchange.Description;

[DebuggerDisplay("Message: {Message} InnerException: {InnerException}")]
public class ColumnDescriptionException : Exception
{
  public ColumnDescriptionException()
  {
  }

  public ColumnDescriptionException(string message)
    : base(message)
  {
  }

  public ColumnDescriptionException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected ColumnDescriptionException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
