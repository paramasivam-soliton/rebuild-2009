// Decompiled with JetBrains decompiler
// Type: PathMedical.Monitoring.PerformanceMonitorInstaller
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Monitoring;

[RunInstaller(true)]
public class PerformanceMonitorInstaller : Installer
{
  private PerformanceCounterInstaller counterInstaller;

  public PerformanceMonitorInstaller()
  {
    this.counterInstaller = new PerformanceCounterInstaller();
    this.counterInstaller.CategoryName = "AccuLink";
    this.counterInstaller.CategoryType = PerformanceCounterCategoryType.SingleInstance;
    this.counterInstaller.Counters.Add(new CounterCreationData()
    {
      CounterName = "ModuleChanges",
      CounterType = PerformanceCounterType.NumberOfItems32
    });
    this.Installers.Add((Installer) this.counterInstaller);
  }

  public PerformanceCounterInstaller PerformanceCounterInstaller => this.counterInstaller;
}
