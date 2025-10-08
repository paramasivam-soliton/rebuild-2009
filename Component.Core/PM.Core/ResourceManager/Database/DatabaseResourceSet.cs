// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.Database.DatabaseResourceSet
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Globalization;
using System.Resources;

#nullable disable
namespace PathMedical.ResourceManager.Database;

public class DatabaseResourceSet : CustomResourceSet
{
  private readonly string baseNameField;
  private readonly CultureInfo cultureInfo;

  public DatabaseResourceSet(string baseNameField, CultureInfo cultureInfo)
    : base((IResourceReader) new DatabaseResourceReader(baseNameField, cultureInfo))
  {
    this.baseNameField = baseNameField;
    this.cultureInfo = cultureInfo;
  }

  public override Type GetDefaultReader() => typeof (DatabaseResourceReader);

  public override IResourceReader CreateDefaultReader()
  {
    return (IResourceReader) new DatabaseResourceReader(this.baseNameField, this.cultureInfo);
  }

  public override Type GetDefaultWriter() => typeof (DatabaseResourceWriter);

  public override IResourceWriter CreateDefaultWriter()
  {
    return (IResourceWriter) new DatabaseResourceWriter();
  }
}
