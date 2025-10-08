// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.CurrentDataHelper`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface;

public class CurrentDataHelper<T> where T : class, new()
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (CurrentDataHelper<T>), "$Rev: 942 $");
  private DateTime lastLoad;
  private bool isOutdated;
  private BindingList<T> items;

  public static CurrentDataHelper<T> Instance => PathMedical.Singleton.Singleton<CurrentDataHelper<T>>.Instance;

  private CurrentDataHelper()
  {
  }

  public BindingList<T> Items
  {
    get
    {
      this.EnsureActuality();
      return this.items;
    }
  }

  public void MarkAsOutdated()
  {
    if (this.isOutdated)
      return;
    CurrentDataHelper<T>.Logger.Info("Item list of type {0} is now marked as outdated.", (object) typeof (T).Name);
    this.isOutdated = true;
  }

  public void EnsureActuality()
  {
    if (!this.IsReloadNecessary)
      return;
    this.PerformReload();
  }

  private bool IsReloadNecessary
  {
    get
    {
      if (this.items == null)
      {
        CurrentDataHelper<T>.Logger.Debug("Item list of type {0} is empty. Reloading proposed.", (object) typeof (T).Name);
        return true;
      }
      if (this.isOutdated)
      {
        CurrentDataHelper<T>.Logger.Debug("Item list of type {0} was marked as outdated. Reloading proposed.", (object) typeof (T).Name);
        return true;
      }
      using (DBScope scope = new DBScope(TransactionLevel.Independent))
      {
        this.isOutdated = this.HasCountChanged(scope) || this.HasNewerTimestamp(scope);
        scope.Complete();
      }
      return this.isOutdated;
    }
  }

  private bool HasCountChanged(DBScope scope)
  {
    bool flag = new AdapterBase<T>(scope).Count() != this.items.Count;
    if (flag)
      CurrentDataHelper<T>.Logger.Debug("Item list of type {0} has different count in the database.", (object) typeof (T).Name);
    return flag;
  }

  private bool HasNewerTimestamp(DBScope scope)
  {
    EntityHelper entityHelper = EntityHelper.For<T>();
    foreach (string str in ((IEnumerable<string>) new string[3]
    {
      entityHelper.InsertTimestampColumnName,
      entityHelper.InsertUpdateTimestampColumnName,
      entityHelper.UpdateTimestampColumnName
    }).Where<string>((Func<string, bool>) (tc => !string.IsNullOrEmpty(tc))))
    {
      using (DbCommand dbCommand = scope.CreateDbCommand())
      {
        dbCommand.CommandText = $"SELECT MAX([{str}]) FROM [{entityHelper.TableName}]";
        DateTime? nullable1 = dbCommand.ExecuteScalar() as DateTime?;
        int num;
        if (nullable1.HasValue)
        {
          DateTime? nullable2 = nullable1;
          DateTime lastLoad = this.lastLoad;
          num = nullable2.HasValue ? (nullable2.GetValueOrDefault() > lastLoad ? 1 : 0) : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
          CurrentDataHelper<T>.Logger.Debug("Item list of type {0} has newer timestamp in database column {1}.", (object) typeof (T).Name, (object) str);
          return true;
        }
      }
    }
    return false;
  }

  private void PerformReload()
  {
    CurrentDataHelper<T>.Logger.Info("Reloading item list of type {0}.", (object) typeof (T).Name);
    if (this.items == null)
      this.items = new BindingList<T>();
    ICollection<T> all;
    using (DBScope scope = new DBScope(TransactionLevel.Independent))
    {
      all = new AdapterBase<T>(scope).All;
      scope.Complete();
    }
    this.items.RaiseListChangedEvents = false;
    this.items.Clear();
    foreach (T obj in (IEnumerable<T>) all)
      this.items.Add(obj);
    this.items.RaiseListChangedEvents = true;
    this.items.ResetBindings();
    this.isOutdated = false;
    this.lastLoad = DateTime.Now;
    CurrentDataHelper<T>.Logger.Info("Successfully reloaded item list of type {0}.", (object) typeof (T).Name);
  }
}
