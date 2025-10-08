// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Attributes.InstrumentColumnDescriptionAttribute
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using System;

#nullable disable
namespace PathMedical.Type1077.DataExchange.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
[CLSCompliant(false)]
public class InstrumentColumnDescriptionAttribute : Attribute
{
  public int UniqueFieldIdentifier { get; protected set; }

  public InstrumentColumnDescriptionAttribute(int uniqueFieldIdentifier)
  {
    this.UniqueFieldIdentifier = uniqueFieldIdentifier;
  }
}
