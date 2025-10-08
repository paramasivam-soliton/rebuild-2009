// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.EnumerableMatchComparison`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Extensions;

public class EnumerableMatchComparison<TItem, TIdentifier> where TItem : class
{
  internal EnumerableMatchComparison(TItem item1, TItem item2, TIdentifier identifier)
  {
    this.ItemInFirstEnumerable = item1;
    this.ItemInSecondEnumerable = item2;
    this.Identifier = identifier;
  }

  public TIdentifier Identifier { get; private set; }

  public TItem ItemInFirstEnumerable { get; private set; }

  public TItem ItemInSecondEnumerable { get; private set; }
}
