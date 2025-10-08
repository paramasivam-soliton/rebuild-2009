// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.ModelException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

[Serializable]
public class ModelException : Exception
{
  public ModelException()
  {
  }

  public ModelException(string message)
    : base(message)
  {
  }

  public ModelException(string message, Exception inner)
    : base(message, inner)
  {
  }

  protected ModelException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
  {
  }
}
