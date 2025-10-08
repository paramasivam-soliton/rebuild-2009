// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.InstrumentManagementPermissions
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using System;

#nullable disable
namespace PathMedical.InstrumentManagement;

public static class InstrumentManagementPermissions
{
  public static readonly Guid ViewInstruments = new Guid("058A96DD-356B-4337-BEFF-390E6113A298");
  public static readonly Guid AddAndEditInstruments = new Guid("A216402B-47B1-4791-823F-7FD44EEF7BD4");
  public static readonly Guid DeleteInstruments = new Guid("0EA99B54-5303-482e-B120-39E2DBB2FD88");
  public static readonly Guid ResetInstrumentUsers = new Guid("F7A3A1C6-BBFB-4a8c-9A45-08996D181C58");
  public static readonly Guid ConfigureSettings = new Guid("E4AEADFD-0717-4250-B77B-8CA134B43AB2");
  public static readonly Guid ConfigureTestModules = new Guid("E4AEADFD-0717-4250-B77B-8CA134B43AB2");
}
