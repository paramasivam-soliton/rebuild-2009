// Decompiled with JetBrains decompiler
// Type: PathMedical.FileService.TransmissionItems
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.FileService;

[DbTable(HasHistory = false)]
public class TransmissionItems
{
  [DbColumn]
  public Guid TransmissionId { get; set; }

  [DbColumn]
  public Guid TestId { get; set; }
}
