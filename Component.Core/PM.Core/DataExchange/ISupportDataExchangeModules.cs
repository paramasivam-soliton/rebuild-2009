// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.ISupportDataExchangeModules
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Plugin;
using PathMedical.UserInterface;

#nullable disable
namespace PathMedical.DataExchange;

public interface ISupportDataExchangeModules : IPlugin
{
  IApplicationComponentModule ImportModule { get; }

  IApplicationComponentModule ExportModule { get; }

  IApplicationComponentModule ConfigurationImportModule { get; }

  IApplicationComponentModule ConfigurationExportModule { get; }

  IApplicationComponentModule ConfigurationModule { get; }
}
