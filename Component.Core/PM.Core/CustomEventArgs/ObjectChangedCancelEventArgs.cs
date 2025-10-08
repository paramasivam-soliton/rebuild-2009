// Decompiled with JetBrains decompiler
// Type: PathMedical.CustomEventArgs.ObjectChangedCancelEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;

#nullable disable
namespace PathMedical.CustomEventArgs;

public class ObjectChangedCancelEventArgs : CancelEventArgs
{
  public ObjectChangedCancelEventArgs(object objectChanged) => this.ChangedObject = objectChanged;

  public object ChangedObject { get; protected set; }
}
