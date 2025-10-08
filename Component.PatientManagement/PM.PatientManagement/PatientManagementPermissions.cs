// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientManagementPermissions
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using System;

#nullable disable
namespace PathMedical.PatientManagement;

public static class PatientManagementPermissions
{
  public static readonly Guid ViewPatients = new Guid("880BE1E3-093E-4662-822C-84C5222DCBB0");
  public static readonly Guid AddAndEditPatients = new Guid("F4BE3C68-6202-4753-8C5E-B4178BF89B3A");
  public static readonly Guid DeletePatients = new Guid("0454AAB5-F713-4432-82E7-2CB15A06B95B");
  public static readonly Guid ConfigurePatientManagement = new Guid("83EC6F27-0A70-41ec-BD0E-5B0289F73D34");
  public static readonly Guid MaintainRiskIndicators = new Guid("90482128-10C8-45a2-AA33-84AD39BBF37B");
  public static readonly Guid MaintainComments = new Guid("C7C2F750-58B4-486A-B23D-4AB8485E75C9");
  public static readonly Guid ReAssignTests = new Guid("BC6434E6-D4B4-425f-89C3-CA7C14CF477E");
}
