// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077DataConverter
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using System;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

public class Type1077DataConverter : ISupportDataConversion
{
  public Guid Id { get; protected set; }

  public string Name { get; protected set; }

  public Type1077DataConverter()
  {
    this.Id = new Guid("E49940C2-5CB9-44ef-94B9-7B9E107BA91B");
    this.Name = "Type1077";
  }

  [CLSCompliant(false)]
  public object GetValue(DataTypes dataTypes, byte[] binaryData, ushort offset)
  {
    return DataConverter.GetValue(dataTypes, binaryData);
  }

  [CLSCompliant(false)]
  public object GetValue(DataTypes dataTypes, bool isDynamicArray, byte[] binaryData)
  {
    return (object) null;
  }

  [CLSCompliant(false)]
  public byte[] GetByteArray(object value, DataTypes dataTypes)
  {
    return DataConverter.GetByteArray(value);
  }
}
