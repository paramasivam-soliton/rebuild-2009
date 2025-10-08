// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Channel.CommunicationSpeedType
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

#nullable disable
namespace PathMedical.DeviceCommunication.Channel;

public enum CommunicationSpeedType
{
  None = 0,
  Bps2400 = 2400, // 0x00000960
  Bps9600 = 9600, // 0x00002580
  Bps33600 = 33600, // 0x00008340
  Bps56400 = 56400, // 0x0000DC50
  Bps115200 = 115200, // 0x0001C200
}
