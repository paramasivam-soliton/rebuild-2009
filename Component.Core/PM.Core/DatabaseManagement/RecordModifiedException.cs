// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.RecordModifiedException
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DatabaseManagement;

[Serializable]
public class RecordModifiedException : ExecutionException
{
  public RecordModifiedException()
  {
  }

  public RecordModifiedException(string message)
    : base(message)
  {
  }

  public RecordModifiedException(string message, Exception inner)
    : base(message, inner)
  {
  }
}
