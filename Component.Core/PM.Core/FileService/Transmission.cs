// Decompiled with JetBrains decompiler
// Type: PathMedical.FileService.Transmission
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.FileService;

[DbTable(HasHistory = false)]
public class Transmission
{
  [DbPrimaryKeyColumn]
  public Guid Id { get; set; }

  [DbColumn]
  public DateTime Created { get; set; }

  [DbColumn]
  public Guid UserId { get; set; }

  [DbColumn]
  public int Type { get; set; }

  [DbColumn]
  public Guid Retransmission { get; set; }
}
