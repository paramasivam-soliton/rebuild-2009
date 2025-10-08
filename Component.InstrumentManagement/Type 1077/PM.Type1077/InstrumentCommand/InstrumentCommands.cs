// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.InstrumentCommand.InstrumentCommands
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using System;

#nullable disable
namespace PathMedical.Type1077.InstrumentCommand;

[CLSCompliant(false)]
public static class InstrumentCommands
{
  public static BinaryInstrumentCommand GetDateTime = new BinaryInstrumentCommand("Get Date and Time", "BCGT", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand GetInstrumentInformation = new BinaryInstrumentCommand("Get Device Information", "BCDI", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand GetInstrumentFlash = new BinaryInstrumentCommand("Get Flash Data", "BCGF", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand EraseInstrumentFlash = new BinaryInstrumentCommand("Erase instrument flash", "BCEF", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand RebootInstrument = new BinaryInstrumentCommand("Reboot Device", "BCRD", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand DeleteDuplicates = new BinaryInstrumentCommand("Delete Duplicates", "BCDD", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand CollectGarbage = new BinaryInstrumentCommand("Collect Garbage", "BCGG", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand BeginConfiguartion = new BinaryInstrumentCommand("Begin Configuration", "BCFS", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand CommitConfiguartion = new BinaryInstrumentCommand("Commit Configuration", "BCFR", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand CloseMessagePanel = new BinaryInstrumentCommand("Close Message Panel", "BCID", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand SwitchToneGeneratorOn = new BinaryInstrumentCommand("Switch tone generator on", "BCTO", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand SwitchToneGeneratorOff = new BinaryInstrumentCommand("Switch tone generator off", "BCTF", BinaryInstrumentCommandType.SendingDataCommand);
  public static BinaryInstrumentCommand GetMicrophoneRmsValue = new BinaryInstrumentCommand("Delivers the root mean square of the microphone", "BCMG", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand GetLoopBackCableRmsValue = new BinaryInstrumentCommand("Delivers the rms values for the loop back cable tests", "BCLR", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand GetProbeInformation = new BinaryInstrumentCommand("Delivers information about the probe", "BCPI", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand StartAbrAmplifier = new BinaryInstrumentCommand("Start the ABR amplifier", "BCAR", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand StopAbrAmplifier = new BinaryInstrumentCommand("Stop the ABR amplifier", "BCAO", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand GetImpedanceInformation = new BinaryInstrumentCommand("Get Impedances", "BCAI", BinaryInstrumentCommandType.ReceivingDataCommand);
  public static BinaryInstrumentCommand ResetAllUserLocks = new BinaryInstrumentCommand("Reset all user locks", "BCRU", BinaryInstrumentCommandType.SendingDataCommand);

  public static BinaryInstrumentCommand SetDateTime
  {
    get
    {
      return new BinaryInstrumentCommand("Set Date and Time", "BCST", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand AppendToInstrumentFlash
  {
    get
    {
      return new BinaryInstrumentCommand("Append data to flash", "BCSF", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand UpdateInstrumentFirmware
  {
    get
    {
      return new BinaryInstrumentCommand("Update Firmware", "BCFW", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand OpenMessagePanel
  {
    get
    {
      return new BinaryInstrumentCommand("Open Message Panel", "BCII", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand DisplayMessage
  {
    get
    {
      return new BinaryInstrumentCommand("Display Message", "BCIS", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand SetFrequency
  {
    get
    {
      return new BinaryInstrumentCommand("Set the frequency with which the tone generator shall work", "BCSS", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand SetFrequencyAndLevel
  {
    get
    {
      return new BinaryInstrumentCommand("Set the frequency and level with which the tone generator shall work", "BCLF", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand SetServiceDate
  {
    get
    {
      return new BinaryInstrumentCommand("Set the actual date as service date and reset the next service data", "BCRS", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand SetFirmwareLicence
  {
    get
    {
      return new BinaryInstrumentCommand("Set the new firmware licence", "BCSL", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }

  public static BinaryInstrumentCommand SetProbeCalibrationValues
  {
    get
    {
      return new BinaryInstrumentCommand("Set calibration values for the connected probe", "BCPC", BinaryInstrumentCommandType.SendingDataCommand);
    }
  }
}
