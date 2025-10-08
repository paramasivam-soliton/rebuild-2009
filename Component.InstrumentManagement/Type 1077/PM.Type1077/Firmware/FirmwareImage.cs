// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Firmware.FirmwareImage
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.Type1077.Firmware;

[DbTable(Name = "Type1077FirmwareImage")]
public class FirmwareImage
{
  [DbPrimaryKeyColumn]
  public Guid Id { get; set; }

  [DbColumn]
  public Guid InstrumentTypeSignature { get; set; }

  [DbColumn(Name = "Date")]
  public DateTime DateTime { get; set; }

  [DbColumn]
  public string Version { get; set; }

  [DbColumn]
  public long BuildNumber { get; set; }

  [DbColumn(Name = "LanguagePack")]
  public string LanguagePackName { get; set; }

  [DbColumn]
  public string Languages { get; set; }

  [DbColumn]
  public string CheckSum { get; set; }

  [DbColumn]
  public byte[] Image { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  [CLSCompliant(false)]
  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is FirmwareImage firmwareImage && this.Id.Equals(firmwareImage.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
