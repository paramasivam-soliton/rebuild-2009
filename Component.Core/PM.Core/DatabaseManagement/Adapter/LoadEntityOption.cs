// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.LoadEntityOption
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using PathMedical.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

[DebuggerDisplay("Prefix: {Prefix}, LoadType: {LoadType}, AttachType: {AttachType}")]
public class LoadEntityOption
{
  private LoadEntityOption(Type typeToLoad, Type listTypeToAttachTo)
  {
    this.LoadType = typeToLoad;
    this.AttachType = listTypeToAttachTo;
    this.IsAttachTypeList = true;
    this.EntityHelper = EntityHelper.For(this.LoadType);
    this.Children = (ICollection<LoadEntityOption>) new List<LoadEntityOption>();
    this.Prefix = "";
    this.ArrangeInHierarchy();
  }

  private LoadEntityOption(PropertyInfo propertyInfo)
  {
    this.PropertyInfo = propertyInfo;
    if (propertyInfo.PropertyType.IsGenericType && (typeof (IList).IsAssignableFrom(propertyInfo.PropertyType) || propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof (IList<>)))
    {
      this.IsAttachTypeList = true;
      this.AttachType = propertyInfo.PropertyType;
      this.LoadType = propertyInfo.PropertyType.GetGenericArguments()[0];
      this.EntityHelper = EntityHelper.For(this.LoadType);
    }
    else
    {
      this.AttachType = propertyInfo.DeclaringType;
      this.LoadType = propertyInfo.PropertyType;
      this.EntityHelper = EntityHelper.For(this.LoadType);
    }
    this.PropertyHelper = PropertyHelper.For(propertyInfo);
    this.RelationHelper = RelationHelper.For(propertyInfo);
    this.Children = (ICollection<LoadEntityOption>) new List<LoadEntityOption>();
    this.Prefix = "";
    this.ArrangeInHierarchy();
    this.RelationJoinInfo = new RelationJoinInfo(this.PropertyInfo);
  }

  public Type LoadType { get; private set; }

  public Type AttachType { get; private set; }

  public bool IsAttachTypeList { get; private set; }

  public bool HasRelationValue { get; private set; }

  public PropertyInfo PropertyInfo { get; private set; }

  public PropertyHelper PropertyHelper { get; private set; }

  public string PrefixedPrimaryKeyName { get; private set; }

  public EntityHelper EntityHelper { get; private set; }

  public RelationHelper RelationHelper { get; private set; }

  public string Prefix { get; private set; }

  public ICollection<LoadEntityOption> Children { get; private set; }

  public LoadEntityOption Parent { get; private set; }

  public Dictionary<string, PropertyHelper> PrefixedFillColumnPropertyMap { get; private set; }

  public RelationJoinInfo RelationJoinInfo { get; private set; }

  public static LoadEntityOption CreateRootLoadOption(Type typeToLoad)
  {
    if (typeToLoad == (Type) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (typeToLoad));
    Type listTypeToAttachTo = typeof (List<>).MakeGenericType(typeToLoad);
    return new LoadEntityOption(typeToLoad, listTypeToAttachTo);
  }

  public void AddNestedLoadEntityOption(string propertyName)
  {
    if (propertyName == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    if (propertyName.Contains("."))
    {
      int length = propertyName.IndexOf(".");
      string thisPropertyName = propertyName.Substring(0, length);
      string propertyName1 = propertyName.Substring(length + 1);
      LoadEntityOption loadEntityOption = this.Children.FirstOrDefault<LoadEntityOption>((Func<LoadEntityOption, bool>) (l => l.PropertyInfo.Name == thisPropertyName));
      if (loadEntityOption == null)
      {
        this.AddNestedLoadEntityOption(thisPropertyName);
        loadEntityOption = this.Children.FirstOrDefault<LoadEntityOption>((Func<LoadEntityOption, bool>) (l => l.PropertyInfo.Name == thisPropertyName));
      }
      loadEntityOption?.AddNestedLoadEntityOption(propertyName1);
    }
    else
    {
      PropertyInfo propertyInfo = this.LoadType.GetProperty(propertyName);
      if (propertyInfo == (PropertyInfo) null || propertyInfo.GetCustomAttributes(typeof (DbRelationAttribute), true).Length == 0)
        throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("PropertyIsNotInARelationOrDoesNotExist"), (object) propertyName));
      if (this.Children.Any<LoadEntityOption>((Func<LoadEntityOption, bool>) (c => c.PropertyInfo == propertyInfo)))
        return;
      this.AddChild(new LoadEntityOption(propertyInfo));
    }
  }

  private void AddChild(LoadEntityOption child)
  {
    this.Children.Add(child);
    child.Parent = this;
    child.Prefix = $"{this.Prefix}{child.PropertyInfo.Name}_";
    child.ArrangeInHierarchy();
  }

  private void ArrangeInHierarchy()
  {
    this.PrefixedPrimaryKeyName = this.Prefix + this.EntityHelper.PrimaryKeyName;
    this.PrefixedFillColumnPropertyMap = this.EntityHelper.ColumnPropertyMap.ToDictionary<KeyValuePair<string, PropertyHelper>, string, PropertyHelper>((Func<KeyValuePair<string, PropertyHelper>, string>) (p => this.Prefix + p.Key), (Func<KeyValuePair<string, PropertyHelper>, PropertyHelper>) (p => p.Value));
    foreach (KeyValuePair<string, PropertyHelper> relationValueProperty in this.EntityHelper.RelationValuePropertyMap)
      this.PrefixedFillColumnPropertyMap.Add(this.Prefix + relationValueProperty.Value.PropertyName, relationValueProperty.Value);
    if (this.Parent == null || this.EntityHelper.RelationValuePropertyMap.Count <= 0)
      return;
    this.HasRelationValue = true;
  }

  public void DoInRelationHierarchy(
    object entity,
    Action<object, object, LoadEntityOption> action,
    ProcessRelationHierarchy processRelationHierarchy)
  {
    if (entity == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    if (entity.GetType() != this.LoadType)
      throw new ArgumentException($"Invalid type (Current: {entity.GetType()} - Expected: {this.LoadType}");
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) this.Children)
    {
      ICollection<object> relatedEntities = child.RelationHelper.GetRelatedEntities(entity);
      if (relatedEntities.Count != 0)
      {
        foreach (object entity1 in (IEnumerable<object>) relatedEntities)
        {
          if (processRelationHierarchy == ProcessRelationHierarchy.TopToBottom)
            action(entity, entity1, child);
          if (processRelationHierarchy != ProcessRelationHierarchy.OnlyTopLevel)
            child.DoInRelationHierarchy(entity1, action, processRelationHierarchy);
          if (processRelationHierarchy == ProcessRelationHierarchy.BottomToTop)
            action(entity, entity1, child);
        }
      }
    }
  }

  public override int GetHashCode()
  {
    int hashCode = this.GetType().GetHashCode() + this.Prefix.GetHashCode() + this.LoadType.GetHashCode() + this.AttachType.GetHashCode();
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) this.Children)
      hashCode += child.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object obj)
  {
    return obj is LoadEntityOption loadEntityOption && loadEntityOption.GetHashCode() == this.GetHashCode();
  }
}
