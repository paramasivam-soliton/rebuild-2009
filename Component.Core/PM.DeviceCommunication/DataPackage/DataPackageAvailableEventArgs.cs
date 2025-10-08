// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.DataPackage.DataPackageAvailableEventArgs
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using PathMedical.Communication;
using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.DeviceCommunication.DataPackage;

public class DataPackageAvailableEventArgs : EventArgs
{
  public static readonly BinaryDataPackage Empty;

  public BinaryDataPackage DataPackage { get; set; }

  public DataPackageAvailableEventArgs(BinaryDataPackage dataPackage)
  {
    this.DataPackage = dataPackage != null ? dataPackage : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dataPackage));
  }
}
