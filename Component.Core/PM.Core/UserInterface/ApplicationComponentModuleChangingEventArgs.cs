// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ApplicationComponentModuleChangingEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface;

public class ApplicationComponentModuleChangingEventArgs : CancelEventArgs
{
  public IApplicationComponentModule Old { get; protected set; }

  public IApplicationComponentModule New { get; protected set; }

  public ApplicationComponentModuleChangingEventArgs(
    IApplicationComponentModule oldApplicationComponentModule,
    IApplicationComponentModule newApplicationCompnentModule)
  {
    this.Old = oldApplicationComponentModule;
    this.New = newApplicationCompnentModule;
  }
}
