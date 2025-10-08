// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.ModelChangedEventArgs
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Property;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

[DebuggerDisplay("{ChangeType} {ChangedObject!=null?ChangedObject.GetType():null}")]
public class ModelChangedEventArgs : EventArgs
{
  [DebuggerStepThrough]
  private ModelChangedEventArgs(
    object changedObject,
    ChangeType changeType,
    Type type,
    bool isList)
  {
    this.ChangedObject = changedObject;
    this.ChangeType = changeType;
    this.Type = type;
    this.IsList = isList;
  }

  public object ChangedObject { get; protected set; }

  public ChangeType ChangeType { get; protected set; }

  public Type Type { get; protected set; }

  public bool IsList { get; protected set; }

  [DebuggerStepThrough]
  public static ModelChangedEventArgs Create<T>(T changedObject, ChangeType changeType)
  {
    bool isList = typeof (IEnumerable).IsAssignableFrom(typeof (T)) && typeof (T).IsGenericType;
    Type type = isList ? typeof (T).GetGenericArguments()[0] : typeof (T);
    return new ModelChangedEventArgs((object) changedObject, changeType, type, isList);
  }

  public override string ToString()
  {
    return $"ModelChangedEventArgs Type: {this.ChangeType}, IsList: {(this.IsList ? (object) "Yes" : (object) "No")}, Data Type: {this.Type}, Object: {(this.ChangedObject != null ? (object) this.ChangedObject.ToString() : (object) "null")}";
  }

  public string ToShortString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0}/{1}", (object) this.ChangeType, (object) this.Type.Name);
    stringBuilder.AppendFormat("/{0}", (object) PropertyHelper.GetPropertyTypeName(this.ChangedObject));
    return stringBuilder.ToString();
  }
}
