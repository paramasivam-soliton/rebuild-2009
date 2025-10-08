// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.AbrPreset
// Assembly: PM.ABR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 09E7728F-8618-4147-9D4A-E38CA516B245
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;

#nullable disable
namespace PathMedical.ABR;

[DataExchangeRecord]
public class AbrPreset
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Name { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Description { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public AbrLevels Level { get; set; }

  [DbColumn(Name = "Category")]
  [DataExchangeColumn]
  public PresetCategoryType Category { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }
}
