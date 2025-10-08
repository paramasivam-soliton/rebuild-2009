// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ApplicationComponentChangingEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface;

public class ApplicationComponentChangingEventArgs : CancelEventArgs
{
  public IApplicationComponent Old { get; protected set; }

  public IApplicationComponent New { get; protected set; }

  public ApplicationComponentChangingEventArgs(
    IApplicationComponent oldApplicationComponent,
    IApplicationComponent newApplicationComponent)
  {
    this.Old = oldApplicationComponent;
    this.New = newApplicationComponent;
  }
}
