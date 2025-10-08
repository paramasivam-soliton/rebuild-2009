// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.DataAccessLayer.InstrumentAdapter
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;

#nullable disable
namespace PathMedical.InstrumentManagement.DataAccessLayer;

public class InstrumentAdapter(DBScope scope) : AdapterBase<Instrument>(scope)
{
}
