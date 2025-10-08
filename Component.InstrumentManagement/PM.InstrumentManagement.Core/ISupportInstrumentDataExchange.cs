// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.ISupportInstrumentDataExchange
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.Communication;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Tokens;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.InstrumentManagement;

public interface ISupportInstrumentDataExchange
{
  Guid InstrumentSignature { get; }

  event EventHandler<OperationStateEventArgs> InstrumentEvaluationEventHander;

  event EventHandler<InstrumentAvailableEventArgs> InstrumentConnectedEventHander;

  event EventHandler<DataExchangeTokenSetAvailableEventArgs> DataAvailableForDataExchangeEventHandler;

  void EvaluateConnectedInstruments();

  void LoadData(ICommunicationChannel communicationChannel);

  void StoreData(IInstrument instrument, List<ISupportDataExchangeToken> data);

  void EraseData(IInstrument instrument);
}
