// Decompiled with JetBrains decompiler
// Type: PathMedical.Communication.ICommunicationChannel
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Text;

#nullable disable
namespace PathMedical.Communication;

public interface ICommunicationChannel
{
  string Name { get; set; }

  Encoding Encoding { get; set; }

  event EventHandler<ChannelStateEventArgs> ChannelStateChangedEventHandler;

  event EventHandler<ChannelDataReceivedEventArgs> ChannelDataReceivedEventHandler;

  event EventHandler<ChannelControlEventArgs> ChannelControlEventHandler;

  event EventHandler<EventArgs> ChannelTimeOutEventHandler;

  void Open();

  void Close();

  void Write(BinaryDataPackage dataToWrite);
}
