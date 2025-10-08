// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.DataPackage.ObjectDataPackage
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using System.Collections;

#nullable disable
namespace PathMedical.DeviceCommunication.DataPackage;

public class ObjectDataPackage : PathMedical.Communication.DataPackage<object>
{
  public ObjectDataPackage()
  {
  }

  public ObjectDataPackage(IEnumerable items)
  {
    foreach (object obj in items)
      this.Add(obj);
  }
}
