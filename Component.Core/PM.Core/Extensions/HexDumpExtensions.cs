// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.HexDumpExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Text;

#nullable disable
namespace PathMedical.Extensions;

public static class HexDumpExtensions
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (HexDumpExtensions), "$Rev: 818 $");

  public static string ToHex(this byte[] array)
  {
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (array != null && array.Length != 0)
      {
        foreach (byte num in array)
          stringBuilder.AppendFormat("{0:x2} ", (object) num);
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      }
      return stringBuilder.ToString();
    }
    catch (ArgumentException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping byte array to hex string.");
      throw;
    }
    catch (FormatException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping byte array to hex string.");
      throw;
    }
  }

  public static string ToHex(this string text, bool useAscii)
  {
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      byte[] numArray = useAscii ? Encoding.ASCII.GetBytes(text) : Encoding.Unicode.GetBytes(text);
      if (numArray.Length != 0)
      {
        foreach (byte num in numArray)
          stringBuilder.AppendFormat("{0:x2} ", (object) num);
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      }
      return stringBuilder.ToString();
    }
    catch (ArgumentException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping to hex string.");
      throw;
    }
    catch (FormatException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping byte array to hex string.");
      throw;
    }
  }

  public static string ToHex(this ushort value)
  {
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("0x{0:x2}", (object) value);
      return stringBuilder.ToString();
    }
    catch (ArgumentException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping to hex string.");
      throw;
    }
    catch (FormatException ex)
    {
      HexDumpExtensions.Logger.Error((Exception) ex, "Failure while dumping byte array to hex string.");
      throw;
    }
  }
}
