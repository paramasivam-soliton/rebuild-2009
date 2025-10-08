// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EntityLoadInformation
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class EntityLoadInformation
{
  public EntityLoadInformation(LoadEntityOption loadEntityOption)
  {
    this.LoadEntityOption = loadEntityOption;
  }

  public LoadEntityOption LoadEntityOption { get; private set; }
}
