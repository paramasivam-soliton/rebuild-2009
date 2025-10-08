// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ApplicationComponentBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Plugin;
using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface;

public abstract class ApplicationComponentBase : IApplicationComponent, IPlugin
{
  [Localizable(true)]
  public string Name { get; protected set; }

  [Localizable(true)]
  public string Description { get; protected set; }

  [Localizable(false)]
  public Guid Fingerprint { get; protected set; }

  public int LoadPriority { get; protected set; }

  public Type ActiveModuleType { get; protected set; }

  public bool IsActive { get; set; }

  public Type[] ConfigurationModuleTypes { get; set; }
}
