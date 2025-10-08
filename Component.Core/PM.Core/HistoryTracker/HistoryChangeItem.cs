// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.HistoryChangeItem
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.HistoryTracker;

public class HistoryChangeItem
{
  public string Description { get; set; }

  public string OldValue { get; set; }

  public string NewValue { get; set; }
}
