// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagerException
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.PatientManagement;

public class CommentManagerException : Exception
{
  public CommentManagerException()
  {
  }

  public CommentManagerException(string message)
    : base(message)
  {
  }

  public CommentManagerException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected CommentManagerException(
    SerializationInfo serializationInfo,
    StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
