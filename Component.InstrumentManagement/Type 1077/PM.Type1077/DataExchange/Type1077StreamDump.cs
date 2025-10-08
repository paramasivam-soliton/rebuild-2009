// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077StreamDump
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange.Description;
using PathMedical.Type1077.DataExchange.Column;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

public class Type1077StreamDump
{
  private readonly List<RecordDescription> recordDescriptions;
  private Stream baseStream;

  protected BinaryReader Reader { get; set; }

  protected StreamWriter Writer { get; set; }

  private Type1077StreamDump(Stream stream, string file)
  {
    this.recordDescriptions = new List<RecordDescription>();
    this.baseStream = stream;
    this.Writer = new StreamWriter(file);
    this.Reader = new BinaryReader(stream);
  }

  public void Dump()
  {
    List<InstrumentColumn> instrumentColumnList = new List<InstrumentColumn>();
    long length = this.Reader.BaseStream.Length;
    List<byte> byteList = new List<byte>();
    long num1 = 0;
    while (num1 + 4L < length)
    {
      byteList.AddRange((IEnumerable<byte>) this.Reader.ReadBytes(4));
      long num2 = num1 + 4L;
      ushort uint16 = DataConverter.ToUInt16(byteList.GetRange(2, 2).ToArray(), 0);
      if (num2 + (long) uint16 <= length)
      {
        byteList.AddRange((IEnumerable<byte>) this.Reader.ReadBytes((int) uint16));
        num1 = num2 + (long) uint16;
        InstrumentColumn instrumentColumn = new InstrumentColumn(byteList.ToArray());
        this.Writer.WriteLine(instrumentColumn.ToString());
        instrumentColumnList.Add(instrumentColumn);
      }
      else
      {
        num1 = length;
        this.Writer.WriteLine("Skipped a field since there is not enough data to read.");
      }
      byteList.Clear();
    }
  }
}
