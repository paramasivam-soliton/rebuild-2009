// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.HistoryItemComparer`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.HistoryTracker;

public class HistoryItemComparer<T> where T : class
{
  public BindingList<HistoryChangeItem> Compare(T item, HistoryEntry historyItem)
  {
    if ((object) item == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (item));
    if (historyItem == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (historyItem));
    BindingList<HistoryChangeItem> bindingList = new BindingList<HistoryChangeItem>();
    ((IEnumerable<PropertyInfo>) typeof (T).GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (pi => pi.IsDefined(typeof (HistoryFieldAttribute), true))).Single<PropertyInfo>();
    return bindingList;
  }
}
