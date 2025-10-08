// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.ConnectionException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.DatabaseManagement;

[Serializable]
public class ConnectionException : Exception
{
  public ConnectionException()
  {
  }

  public ConnectionException(string message)
    : base(message)
  {
  }

  public ConnectionException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected ConnectionException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
