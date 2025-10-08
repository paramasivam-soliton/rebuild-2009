// Decompiled with JetBrains decompiler
// Type: PathMedical.Communication.ChannelDataReceivedEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Communication;

public class ChannelDataReceivedEventArgs : EventArgs
{
  public ChannelDataReceivedEventArgs(BinaryDataPackage dataPackage)
  {
    this.DataPackage = dataPackage != null ? dataPackage : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dataPackage));
  }

  public BinaryDataPackage DataPackage { get; protected set; }
}
