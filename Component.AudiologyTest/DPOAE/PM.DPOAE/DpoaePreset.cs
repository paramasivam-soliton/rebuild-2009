// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.DpoaePreset
// Assembly: PM.DPOAE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38B92F02-B758-4EF7-9103-415B55783CFC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.DPOAE;

public class DpoaePreset
{
  [DbPrimaryKeyColumn]
  public Guid Id { get; set; }

  [DbColumn]
  public string Name { get; set; }

  [DbColumn]
  public string Description { get; set; }

  [DbColumn]
  public DpoaeProtocolType PresetNumber { get; set; }

  [DbColumn]
  public PresetCategoryType Category { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }
}
