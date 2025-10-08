// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Instrument
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.Communication;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace PathMedical.InstrumentManagement;

[DataExchangeRecord("Instrument")]
public class Instrument : IInstrument
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? InstrumentTypeSignature { get; set; }

  [DataExchangeColumn]
  public ushort? InstrumentType { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Name { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string SerialNumber { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Code { get; set; }

  [DataExchangeColumn]
  public ulong FreeStorage { get; set; }

  [DataExchangeColumn]
  public ulong UsedStorage { get; set; }

  [DataExchangeColumn]
  public ulong TotalStorage { get; set; }

  [DbReferenceRelation("SiteId")]
  public Site Site { get; set; }

  [DbIntermediateTableRelation("InstrumentId", "UserId", "InstrumentUserAssociation")]
  [DataExchangeColumn]
  public List<User> UsersOnInstrument { get; set; }

  public GlobalInstrumentConfiguration GlobalInstrumentConfiguration
  {
    get => GlobalInstrumentConfiguration.Instance;
  }

  [DbColumn]
  [DataExchangeColumn]
  public string Language { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? LastConnected { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? LastUpdated { get; set; }

  [DataExchangeColumn]
  public bool IsMessagePanelOpen { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string HardwareVersion { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string SoftwareVersion { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public long? FirmwareBuildNumber { get; set; }

  [DataExchangeColumn]
  public uint? BatteryState { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string LanguagePackName { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  public ICommunicationChannel CommunicationChannel { get; set; }

  public Bitmap Image { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    bool flag = false;
    if (obj is Instrument instrument)
    {
      string str1 = instrument.SerialNumber ?? string.Empty;
      string str2 = this.SerialNumber ?? string.Empty;
      flag = this.Id.Equals(instrument.Id) && str2.Equals(str1);
    }
    return flag;
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
