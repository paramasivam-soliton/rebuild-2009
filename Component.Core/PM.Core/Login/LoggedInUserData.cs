// Decompiled with JetBrains decompiler
// Type: PathMedical.Login.LoggedInUserData
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Login;

public class LoggedInUserData
{
  public Guid Id { get; set; }

  public string FullName { get; set; }

  public object Entity { get; set; }

  public LoggedInUserData(Guid id, string fullName, object entity)
  {
    this.Id = id;
    this.FullName = fullName;
    this.Entity = entity;
  }
}
