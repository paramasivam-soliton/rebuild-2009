// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ObjectHistory
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface;

public class ObjectHistory
{
  private readonly Dictionary<object, object> history;
  private readonly object historyLocker;
  private Dictionary<object, object> resetList;

  public static ObjectHistory Instance => PathMedical.Singleton.Singleton<ObjectHistory>.Instance;

  private ObjectHistory()
  {
    this.history = new Dictionary<object, object>();
    this.historyLocker = new object();
  }

  public void SetHistory(object member, object value)
  {
    if (member == null)
      return;
    object memberIdentifier = ObjectHistory.GetUniqueMemberIdentifier(member);
    if (this.history.Keys.Contains<object>(memberIdentifier))
    {
      lock (this.historyLocker)
        this.history[memberIdentifier] = value;
    }
    else
    {
      lock (this.historyLocker)
        this.history.Add(memberIdentifier, value);
    }
  }

  public object GetHistory(object member, object currentValue)
  {
    if (member == null)
      return (object) null;
    object memberIdentifier = ObjectHistory.GetUniqueMemberIdentifier(member);
    object history = (object) null;
    if (this.history.Keys.Contains<object>(memberIdentifier))
      history = this.history[memberIdentifier];
    return history;
  }

  public void ResetObject(object member)
  {
    object memberIdentifier = ObjectHistory.GetUniqueMemberIdentifier(member);
    if (!this.history.Keys.Contains<object>(memberIdentifier))
      return;
    lock (this.historyLocker)
      this.history.Remove(memberIdentifier);
  }

  public void Reset()
  {
    lock (this.historyLocker)
      this.history.Clear();
  }

  public void BeginReset()
  {
    this.resetList = new Dictionary<object, object>();
    lock (this.historyLocker)
    {
      foreach (KeyValuePair<object, object> keyValuePair in this.history)
        this.resetList.Add(keyValuePair.Key, keyValuePair.Value);
    }
  }

  public void CommitReset()
  {
    foreach (KeyValuePair<object, object> reset in this.resetList)
    {
      lock (this.historyLocker)
      {
        if (this.history.ContainsKey(reset.Key))
        {
          object obj = this.history[reset.Key];
          if (obj != null)
          {
            if (obj.Equals(reset.Value))
              this.history.Remove(reset.Key);
          }
        }
      }
    }
  }

  private static object GetUniqueMemberIdentifier(object member)
  {
    return (object) $"{Convert.ToString(member)}_{member.GetHashCode()}";
  }
}
