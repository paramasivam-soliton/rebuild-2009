// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.ISupportDataConversion
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DataExchange;

public interface ISupportDataConversion
{
  Guid Id { get; }

  string Name { get; }

  object GetValue(DataTypes dataTypes, byte[] binaryData, ushort offset);

  object GetValue(DataTypes typesCode, bool isDynamicArray, byte[] binaryData);

  byte[] GetByteArray(object value, DataTypes dataTypes);
}
