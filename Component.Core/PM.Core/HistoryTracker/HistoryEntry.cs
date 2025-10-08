// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.HistoryEntry
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.HistoryTracker;

public class HistoryEntry
{
  public object Entity { get; set; }

  public DateTime Timestamp { get; set; }

  public string UserName { get; set; }

  public Guid? UserId { get; set; }

  public int HistoryId { get; set; }

  public HistoryActionType? HistoryAction { get; set; }
}
