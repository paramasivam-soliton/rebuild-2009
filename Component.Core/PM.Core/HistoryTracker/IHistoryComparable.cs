// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.IHistoryComparable
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;

#nullable disable
namespace PathMedical.HistoryTracker;

public interface IHistoryComparable
{
  void LoadHistory();

  BindingList<HistoryChangeItem> CompareHistoryRecord(HistoryEntry oldItem, HistoryEntry newItem);

  BindingList<HistoryChangeItem> CompareHistoryRecord(HistoryEntry historyItem);
}
