// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.ComponentConfigurationInfo
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration;

public class ComponentConfigurationInfo
{
  public IApplicationComponent ApplicationComponent { get; private set; }

  public object Image { get; private set; }

  public Trigger Trigger { get; private set; }

  public Type ConfigurationModuleType { get; private set; }

  public ComponentConfigurationInfo(
    IApplicationComponent applicationComponent,
    Type configurationModuleType,
    object image)
  {
    this.ApplicationComponent = applicationComponent;
    this.ConfigurationModuleType = configurationModuleType;
    this.Image = image;
    this.Trigger = new Trigger(this.ApplicationComponent.Name);
  }
}
