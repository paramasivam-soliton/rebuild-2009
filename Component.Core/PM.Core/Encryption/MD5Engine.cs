// Decompiled with JetBrains decompiler
// Type: PathMedical.Encryption.MD5Engine
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace PathMedical.Encryption;

public class MD5Engine
{
  public static string GetMD5Hash(string text)
  {
    if (string.IsNullOrEmpty(text))
      return string.Empty;
    byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(text));
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < hash.Length; ++index)
      stringBuilder.Append(hash[index].ToString("x2"));
    return stringBuilder.ToString();
  }

  public static string GetMD5Hash(byte[] value)
  {
    byte[] hash = new MD5CryptoServiceProvider().ComputeHash(value);
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < hash.Length; ++index)
      stringBuilder.Append(hash[index].ToString("x2"));
    return stringBuilder.ToString();
  }

  public static byte[] GetMd5HashBinary(string text)
  {
    return string.IsNullOrEmpty(text) ? new byte[0] : new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(text));
  }
}
