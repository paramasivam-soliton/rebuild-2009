// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.IInstrumentPlugin
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Plugin;
using System;

#nullable disable
namespace PathMedical.InstrumentManagement;

public interface IInstrumentPlugin : IPlugin
{
  Type DataDownloadModuleType { get; }

  Type DataUploadModuleType { get; }

  Type ConfigurationSynchronizationModuleType { get; }

  Type FirmwareSearchModuleType { get; }
}
