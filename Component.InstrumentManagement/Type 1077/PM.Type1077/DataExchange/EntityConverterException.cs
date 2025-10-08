// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.EntityConverterException
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.Type1077;

[Serializable]
public class EntityConverterException : Exception
{
  public EntityConverterException()
  {
  }

  public EntityConverterException(string message)
    : base(message)
  {
  }

  public EntityConverterException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected EntityConverterException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
