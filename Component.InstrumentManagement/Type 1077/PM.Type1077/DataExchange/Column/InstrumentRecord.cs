// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Column.InstrumentRecord
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.DataExchange.Column;

[CLSCompliant(false)]
public class InstrumentRecord
{
  private readonly Dictionary<ushort, InstrumentColumn> instrumentColumns;

  public IEnumerable<InstrumentColumn> Columns
  {
    get => (IEnumerable<InstrumentColumn>) this.instrumentColumns.Values.ToList<InstrumentColumn>();
  }

  public InstrumentRecord() => this.instrumentColumns = new Dictionary<ushort, InstrumentColumn>();

  public InstrumentRecord Add(InstrumentColumn instrumentColumn)
  {
    if (instrumentColumn != null && !this.instrumentColumns.ContainsKey(instrumentColumn.UniqueFieldIdentifier))
      this.instrumentColumns.Add(instrumentColumn.UniqueFieldIdentifier, instrumentColumn);
    return this;
  }

  public byte[] ToByteArray()
  {
    List<byte> byteList1 = new List<byte>();
    IOrderedEnumerable<InstrumentColumn> orderedEnumerable1 = this.instrumentColumns.Values.Where<InstrumentColumn>((Func<InstrumentColumn, bool>) (ic => ic.UniqueFieldIdentifier < (ushort) 24576 /*0x6000*/ || ic.UniqueFieldIdentifier > (ushort) 24585)).OrderBy<InstrumentColumn, ushort>((Func<InstrumentColumn, ushort>) (ic => ic.UniqueFieldIdentifier));
    List<byte> byteList2 = new List<byte>();
    foreach (InstrumentColumn instrumentColumn in (IEnumerable<InstrumentColumn>) orderedEnumerable1)
      byteList2.AddRange((IEnumerable<byte>) instrumentColumn.ToByteArray());
    IOrderedEnumerable<InstrumentColumn> orderedEnumerable2 = this.instrumentColumns.Values.Where<InstrumentColumn>((Func<InstrumentColumn, bool>) (ic => ic.UniqueFieldIdentifier >= (ushort) 24579 && ic.UniqueFieldIdentifier <= (ushort) 24585)).OrderBy<InstrumentColumn, ushort>((Func<InstrumentColumn, ushort>) (ic => ic.UniqueFieldIdentifier));
    List<byte> byteList3 = new List<byte>();
    foreach (InstrumentColumn instrumentColumn in (IEnumerable<InstrumentColumn>) orderedEnumerable2)
    {
      switch (instrumentColumn.UniqueFieldIdentifier)
      {
        case 24579:
          byteList3.AddRange((IEnumerable<byte>) new InstrumentColumn((ushort) 24579, DataTypes.UInt16, (object) (int) ushort.MaxValue).ToByteArray());
          continue;
        case 24584:
          byteList3.AddRange((IEnumerable<byte>) new InstrumentColumn((ushort) 24584, DataTypes.UInt16, (object) 7).ToByteArray());
          continue;
        case 24585:
          byteList3.AddRange((IEnumerable<byte>) new InstrumentColumn((ushort) 24585, DataTypes.DateTime, (object) DateTime.Now).ToByteArray());
          continue;
        default:
          byteList3.AddRange((IEnumerable<byte>) instrumentColumn.ToByteArray());
          continue;
      }
    }
    IOrderedEnumerable<InstrumentColumn> orderedEnumerable3 = this.instrumentColumns.Values.Where<InstrumentColumn>((Func<InstrumentColumn, bool>) (ic => ic.UniqueFieldIdentifier >= (ushort) 24576 /*0x6000*/ && ic.UniqueFieldIdentifier <= (ushort) 24578)).OrderBy<InstrumentColumn, ushort>((Func<InstrumentColumn, ushort>) (ic => ic.UniqueFieldIdentifier));
    List<byte> collection = new List<byte>();
    int num = 18 + byteList3.Count<byte>() + byteList2.Count<byte>();
    foreach (InstrumentColumn instrumentColumn in (IEnumerable<InstrumentColumn>) orderedEnumerable3)
    {
      switch (instrumentColumn.UniqueFieldIdentifier)
      {
        case 24576 /*0x6000*/:
          collection.AddRange((IEnumerable<byte>) new InstrumentColumn((ushort) 24576 /*0x6000*/, DataTypes.UInt16, (object) 50115).ToByteArray());
          continue;
        case 24578:
          collection.AddRange((IEnumerable<byte>) new InstrumentColumn((ushort) 24578, DataTypes.UInt16, (object) num).ToByteArray());
          continue;
        default:
          collection.AddRange((IEnumerable<byte>) instrumentColumn.ToByteArray());
          continue;
      }
    }
    byteList1.AddRange((IEnumerable<byte>) collection);
    byteList1.AddRange((IEnumerable<byte>) byteList3);
    byteList1.AddRange((IEnumerable<byte>) byteList2);
    return byteList1.ToArray();
  }
}
