// Decompiled with JetBrains decompiler
// Type: PathMedical.Plugin.ITestPlugin
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.IO;

#nullable disable
namespace PathMedical.Plugin;

public interface ITestPlugin : IPlugin
{
  Guid TestTypeSignature { get; }

  Guid InstrumentSignature { get; }

  Type DetailViewerComponentModuleType { get; }

  ITestDetailView CreateView(object entity);

  Type DetailReportType { get; }

  object GetTestInformation(Guid testId);

  void Delete(Guid testDetailId);

  Type ConfigurationModuleType { get; }

  void WriteConfiguration(Stream stream);

  void AssignTestToNewPatient(Guid newPatient, Guid audiologyTestInformation);
}
