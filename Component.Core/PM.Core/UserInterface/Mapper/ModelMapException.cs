// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Mapper.ModelMapException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.UserInterface.Mapper;

[Serializable]
public class ModelMapException : Exception
{
  public ModelMapException()
  {
  }

  public ModelMapException(string message)
    : base(message)
  {
  }

  public ModelMapException(string message, Exception inner)
    : base(message, inner)
  {
  }
}
