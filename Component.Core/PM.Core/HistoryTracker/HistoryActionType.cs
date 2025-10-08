// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.HistoryActionType
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.HistoryTracker;

public enum HistoryActionType : short
{
  InsertEntity = 1,
  UpdateEntity = 2,
  DeleteEntity = 3,
  InsertRelation = 11, // 0x000B
  UpdateRelation = 12, // 0x000C
  DeleteRelation = 13, // 0x000D
}
