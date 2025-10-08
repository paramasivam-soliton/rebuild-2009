// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.PluginLoadedEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Plugin;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration;

public class PluginLoadedEventArgs : EventArgs
{
  public IPlugin Plugin { get; protected set; }

  public PluginLoadedEventArgs(IPlugin plugin) => this.Plugin = plugin;
}
