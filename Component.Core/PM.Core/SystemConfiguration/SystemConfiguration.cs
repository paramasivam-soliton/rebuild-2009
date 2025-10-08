// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.SystemConfiguration
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration;

internal class SystemConfiguration
{
  [DbPrimaryKeyColumn]
  public Guid Id { get; set; }

  [DbColumn]
  public Guid? ComponentId { get; set; }

  [DbColumn]
  public string Key { get; set; }

  [DbColumn]
  public string Value { get; set; }

  public bool IsChanged { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is PathMedical.SystemConfiguration.SystemConfiguration systemConfiguration && this.Id.Equals(systemConfiguration.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
