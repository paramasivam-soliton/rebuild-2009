// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.DataConverter
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

public static class DataConverter
{
  [CLSCompliant(false)]
  public static object GetValue(DataTypes datatype, byte[] binaryData)
  {
    object obj = (object) null;
    if (binaryData != null && binaryData.Length != 0)
    {
      switch (datatype)
      {
        case DataTypes.Guid:
          obj = (object) DataConverter.ToGuid(binaryData, 0);
          break;
        case DataTypes.String:
          obj = (object) DataConverter.ToString(binaryData, Encoding.Unicode);
          break;
        case DataTypes.Int8:
          obj = (object) DataConverter.ToInt8(binaryData, 0);
          break;
        case DataTypes.Int8Array:
          obj = (object) DataConverter.ToInt8Array(binaryData);
          break;
        case DataTypes.UInt8:
          obj = (object) DataConverter.ToUInt8(binaryData, 0);
          break;
        case DataTypes.UInt8Array:
          obj = (object) DataConverter.ToUInt8Array(binaryData, 0);
          break;
        case DataTypes.Int16:
          obj = (object) DataConverter.ToInt16(binaryData, 0);
          break;
        case DataTypes.Int16Array:
          obj = (object) DataConverter.ToInt16Array(binaryData);
          break;
        case DataTypes.UInt16:
          obj = (object) DataConverter.ToUInt16(binaryData, 0);
          break;
        case DataTypes.UInt16Array:
          obj = (object) DataConverter.ToUInt16Array(binaryData);
          break;
        case DataTypes.Int32:
          obj = (object) DataConverter.ToInt32(binaryData);
          break;
        case DataTypes.UInt32:
          obj = (object) DataConverter.ToUInt32(binaryData, 0);
          break;
        case DataTypes.Float:
          obj = (object) DataConverter.ToFloat(binaryData);
          break;
        case DataTypes.FloatArray:
          obj = (object) DataConverter.ToFloatArray(binaryData);
          break;
        case DataTypes.DateTime:
          obj = (object) DataConverter.ToDateTime(binaryData, 0);
          break;
        case DataTypes.Date:
          obj = (object) DataConverter.ToDate(binaryData, 0);
          break;
        default:
          throw ExceptionFactory.Instance.CreateException<DataConverterException>($"Unhandled conversion datatype: {datatype}");
      }
    }
    return obj;
  }

  public static Guid ToGuid(byte[] binaryData, int offset)
  {
    if (offset + 16 /*0x10*/ > binaryData.Length)
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Array contains not enough data for Guid conversion.");
    try
    {
      List<byte> byteList = new List<byte>();
      for (int index = offset; index < binaryData.Length || index < 16 /*0x10*/; ++index)
        byteList.Add(binaryData[index]);
      return new Guid(byteList.ToArray());
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Guid", (System.Exception) ex);
    }
    catch (IndexOutOfRangeException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Guid", (System.Exception) ex);
    }
  }

  public static DateTime ToDateTime(byte[] binaryData, int offset)
  {
    try
    {
      if (binaryData.Length < offset + 6)
        throw ExceptionFactory.Instance.CreateException<DataConverterException>("Not engough data to convert binary array to DateTime.");
      int year = (int) DataConverter.ToUInt8(binaryData, offset) + 1900;
      int num1 = (int) DataConverter.ToUInt8(binaryData, offset + 1);
      int num2 = (int) DataConverter.ToUInt8(binaryData, offset + 2);
      int uint8_1 = (int) DataConverter.ToUInt8(binaryData, offset + 3);
      int uint8_2 = (int) DataConverter.ToUInt8(binaryData, offset + 4);
      int num3 = (int) DataConverter.ToUInt8(binaryData, offset + 5);
      if (num3 > 59)
        num3 = 59;
      if (num1 == 0)
        num1 = 1;
      if (num2 == 0)
        num2 = 1;
      int month = num1;
      int day = num2;
      int hour = uint8_1;
      int minute = uint8_2;
      int second = num3;
      return new DateTime(year, month, day, hour, minute, second);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to DateTime", (System.Exception) ex);
    }
  }

  public static DateTime ToDate(byte[] binaryData, int offset)
  {
    try
    {
      if (binaryData.Length < offset + 4)
        throw ExceptionFactory.Instance.CreateException<DataConverterException>("Not engough data to convert binary array to Date.");
      int year = (int) DataConverter.ToUInt8(binaryData, offset) + 1900;
      int uint8_1 = (int) DataConverter.ToUInt8(binaryData, offset + 1);
      int uint8_2 = (int) DataConverter.ToUInt8(binaryData, offset + 2);
      int month = uint8_1;
      int day = uint8_2;
      return new DateTime(year, month, day, 0, 0, 0);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Date", (System.Exception) ex);
    }
  }

  public static string ToChar(byte[] binaryData)
  {
    try
    {
      return BitConverter.ToChar(binaryData, 0).ToString();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Char", (System.Exception) ex);
    }
  }

  public static string ToString(byte[] binaryData, Encoding encoding)
  {
    Encoding encoding1 = encoding ?? Encoding.Unicode;
    try
    {
      return encoding1.GetString(binaryData);
    }
    catch (DecoderFallbackException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to String", (System.Exception) ex);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to String", (System.Exception) ex);
    }
  }

  public static int ToInt32(byte[] binaryData)
  {
    try
    {
      return BitConverter.ToInt32(binaryData, 0);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Int32", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static uint ToUInt32(byte[] binaryData, int offset)
  {
    try
    {
      return BitConverter.ToUInt32(binaryData, offset);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to UInt32", (System.Exception) ex);
    }
  }

  public static short ToInt16(byte[] binaryData, int offset)
  {
    try
    {
      return BitConverter.ToInt16(binaryData, offset);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Int16", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static ushort ToUInt16(byte[] binaryData, int offset)
  {
    try
    {
      return BitConverter.ToUInt16(binaryData, offset);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to UInt16", (System.Exception) ex);
    }
  }

  public static byte ToUInt8(byte[] binaryData, int offset)
  {
    try
    {
      if (binaryData != null && binaryData.Length > offset)
        return binaryData[offset];
      throw new ArgumentException(nameof (binaryData));
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to UInt8", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static sbyte ToInt8(byte[] binaryData, int offset)
  {
    try
    {
      if (binaryData != null && binaryData.Length > offset)
        return (sbyte) binaryData[offset];
      throw new ArgumentException(nameof (binaryData));
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Int8", (System.Exception) ex);
    }
  }

  public static float ToFloat(byte[] binaryData)
  {
    try
    {
      return BitConverter.ToSingle(binaryData, 0);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Float", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static float[] ToFloatArray(byte[] binaryData, int dimension)
  {
    try
    {
      List<float> floatList = new List<float>();
      int num = 0;
      for (int startIndex = 0; startIndex <= binaryData.Length; startIndex += 4)
      {
        ++num;
        floatList.Add(BitConverter.ToSingle(binaryData, startIndex));
        if (num == dimension)
          break;
      }
      return floatList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Float array", (System.Exception) ex);
    }
  }

  public static float[] ToFloatArray(byte[] binaryData)
  {
    return DataConverter.ToFloatArray(binaryData, binaryData.Length / 4);
  }

  [CLSCompliant(false)]
  public static sbyte[] ToInt8Array(byte[] binaryData, int dimension)
  {
    try
    {
      List<sbyte> sbyteList = new List<sbyte>();
      int num1 = 0;
      foreach (byte num2 in binaryData)
      {
        ++num1;
        sbyteList.Add((sbyte) num2);
        if (num1 == dimension)
          break;
      }
      return sbyteList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to SByte array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static sbyte[] ToInt8Array(byte[] binaryData)
  {
    return DataConverter.ToInt8Array(binaryData, binaryData.Length);
  }

  public static byte[] ToUInt8Array(byte[] binaryData, int dimension)
  {
    try
    {
      List<byte> byteList = new List<byte>();
      int num1 = 0;
      foreach (byte num2 in binaryData)
      {
        ++num1;
        byteList.Add(num2);
        if (num1 == dimension)
          break;
      }
      return byteList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to UInt8 array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static ushort[] ToUInt16Array(byte[] binaryData, int dimension)
  {
    try
    {
      List<ushort> ushortList = new List<ushort>();
      int num = 0;
      for (int startIndex = 0; startIndex <= binaryData.Length; startIndex += 2)
      {
        ++num;
        ushortList.Add(BitConverter.ToUInt16(binaryData, startIndex));
        if (num == dimension)
          break;
      }
      return ushortList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to UInt16 array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static ushort[] ToUInt16Array(byte[] binaryData)
  {
    return DataConverter.ToUInt16Array(binaryData, binaryData.Length / 2);
  }

  public static short[] ToInt16Array(byte[] binaryData, int dimension)
  {
    try
    {
      List<short> shortList = new List<short>();
      int num = 0;
      for (int startIndex = 0; startIndex <= binaryData.Length; startIndex += 2)
      {
        ++num;
        shortList.Add(BitConverter.ToInt16(binaryData, startIndex));
        if (num == dimension)
          break;
      }
      return shortList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Int16 array", (System.Exception) ex);
    }
  }

  public static short[] ToInt16Array(byte[] binaryData)
  {
    return DataConverter.ToInt16Array(binaryData, binaryData.Length / 2);
  }

  public static int[] ToInt32Array(byte[] binaryData, int dimension)
  {
    try
    {
      List<int> intList = new List<int>();
      int num = 0;
      for (int startIndex = 0; startIndex <= binaryData.Length; startIndex += 4)
      {
        ++num;
        intList.Add(BitConverter.ToInt32(binaryData, startIndex));
        if (num == dimension)
          break;
      }
      return intList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting datatype from binary array to Int16 array", (System.Exception) ex);
    }
  }

  public static int[] ToInt32Array(byte[] binaryData)
  {
    return DataConverter.ToInt32Array(binaryData, binaryData.Length / 4);
  }

  public static byte[] GetByteArray(object value)
  {
    byte[] byteArray = (byte[]) null;
    if (value != null)
    {
      switch (Type.GetTypeCode(value.GetType()))
      {
        case TypeCode.Object:
          switch (value)
          {
            case Guid guid:
              byteArray = DataConverter.GetByteArray(guid);
              break;
            case Array _:
              Array array = value as Array;
              List<byte> byteList = new List<byte>();
              foreach (object obj in array)
                byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(obj));
              byteArray = byteList.ToArray();
              break;
          }
          break;
        case TypeCode.Char:
          byteArray = DataConverter.GetByteArray((string) value);
          break;
        case TypeCode.SByte:
          byteArray = DataConverter.GetByteArray((sbyte) value);
          break;
        case TypeCode.Byte:
          byteArray = DataConverter.GetByteArray((byte) value);
          break;
        case TypeCode.Int16:
          byteArray = DataConverter.GetByteArray((short) value);
          break;
        case TypeCode.UInt16:
          byteArray = DataConverter.GetByteArray((ushort) value);
          break;
        case TypeCode.Int32:
          byteArray = DataConverter.GetByteArray((int) value);
          break;
        case TypeCode.UInt32:
          byteArray = DataConverter.GetByteArray((uint) value);
          break;
        case TypeCode.Int64:
          byteArray = DataConverter.GetByteArray((float) (long) value);
          break;
        case TypeCode.UInt64:
          byteArray = DataConverter.GetByteArray((ulong) value);
          break;
        case TypeCode.Single:
          byteArray = DataConverter.GetByteArray((float) value);
          break;
        case TypeCode.DateTime:
          byteArray = DataConverter.GetByteArray((DateTime) value);
          break;
        case TypeCode.String:
          byteArray = DataConverter.GetByteArray((string) value);
          break;
      }
    }
    return byteArray;
  }

  public static byte[] GetByteArray(Guid value) => value.ToByteArray();

  public static byte[] GetByteArray(DateTime value)
  {
    try
    {
      return new List<byte>()
      {
        (byte) (value.Year - 1900),
        (byte) value.Month,
        (byte) value.Day,
        (byte) value.Hour,
        (byte) value.Minute,
        (byte) value.Second
      }.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting DateTime to binary array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static byte[] GetByteArray(sbyte value)
  {
    return new byte[1]{ (byte) value };
  }

  public static byte[] GetByteArray(byte value)
  {
    return new byte[1]{ value };
  }

  public static byte[] GetByteArray(short value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting Int16 to binary array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static byte[] GetByteArray(ushort value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting UInt16 to binary array", (System.Exception) ex);
    }
  }

  public static byte[] GetByteArray(int value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting Int32 to binary array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static byte[] GetByteArray(uint value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting UInt32 to binary array", (System.Exception) ex);
    }
  }

  [CLSCompliant(false)]
  public static byte[] GetByteArray(ulong value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting UInt64 to binary array", (System.Exception) ex);
    }
  }

  public static byte[] GetByteArray(float value)
  {
    try
    {
      return BitConverter.GetBytes(value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting Float to binary array", (System.Exception) ex);
    }
  }

  public static byte[] GetByteArray(string value)
  {
    try
    {
      List<byte> byteList = new List<byte>();
      foreach (char ch in value)
        byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(ch));
      return byteList.ToArray();
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataConverterException>("Failure while converting String to binary array", (System.Exception) ex);
    }
  }
}
