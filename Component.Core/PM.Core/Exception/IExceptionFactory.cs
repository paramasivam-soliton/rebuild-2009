// Decompiled with JetBrains decompiler
// Type: PathMedical.Exception.IExceptionFactory
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Culture;
using System.Globalization;

#nullable disable
namespace PathMedical.Exception;

public interface IExceptionFactory
{
  CultureInfo CultureOfSystemException { get; set; }

  ICultureManager CultureManager { get; set; }

  T CreateException<T>() where T : System.Exception;

  T CreateException<T>(string message) where T : System.Exception;

  T CreateException<T>(string message, System.Exception innerException) where T : System.Exception;
}
