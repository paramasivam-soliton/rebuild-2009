// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Mapper.ModelMapItem`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.UserInterface.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PathMedical.UserInterface.Mapper;

[DebuggerDisplay("{modelMemberAccess} - {ViewControl.GetType().Name}.{uiMemberAccess.Member.Name}")]
public class ModelMapItem<TEntity> where TEntity : class
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ModelMapItem<TEntity>), "$Rev: 951 $");
  private readonly MemberExpression uiMemberAccess;
  private readonly MemberExpression modelMemberAccess;

  public IControl ViewControl { get; private set; }

  public bool? IsEnabledForced { get; set; }

  public ModelMapItem(
    Expression<Func<TEntity, object>> entityPropertyAccess,
    IControl ui,
    Expression<Func<object>> uiPropertyAccess)
  {
    this.ViewControl = ui;
    this.uiMemberAccess = this.GetMemberExpression((Expression) uiPropertyAccess);
    this.modelMemberAccess = this.GetMemberExpression((Expression) entityPropertyAccess);
  }

  public void CopyModelToViewControl(ICollection<TEntity> entities, bool isUiEnabled)
  {
    if (entities.Count > 1 && !(this.ViewControl is IMultiValueControl))
    {
      this.EnableViewControl(false);
      this.SetCopiedValue((object) null, (object) this.ViewControl, this.uiMemberAccess);
      this.ValidateViewControl();
    }
    else
    {
      List<TEntity> list1 = entities.Where<TEntity>((Func<TEntity, bool>) (e => (object) e != null)).ToList<TEntity>();
      if (list1.Count == 0)
      {
        this.EnableViewControl(false);
        this.SetCopiedValue((object) null, (object) this.ViewControl, this.uiMemberAccess);
        this.ValidateViewControl();
      }
      else
      {
        this.EnableViewControl(isUiEnabled);
        if (this.ViewControl is ISupportUndo)
          ((ISupportUndo) this.ViewControl).UniqueModelMemberIdentifier = this.modelMemberAccess;
        List<object> list2 = list1.Select<TEntity, object>((Func<TEntity, object>) (e => this.GetCopiedValue((object) e, this.modelMemberAccess))).Distinct<object>().ToList<object>();
        if (this.ViewControl is IMultiValueControl viewControl)
        {
          viewControl.Values = (ICollection) list2;
          this.ValidateViewControl();
        }
        else
        {
          this.SetCopiedValue(list2.SingleOrDefault<object>(), (object) this.ViewControl, this.uiMemberAccess);
          this.ValidateViewControl();
        }
      }
    }
  }

  public void CopyViewControlToModel(ICollection<TEntity> entities)
  {
    if (entities == null || entities.Count > 1 && !(this.ViewControl is IMultiValueControl))
      return;
    if (this.ViewControl is IMultiValueControl viewControl)
    {
      if (viewControl.Values != null && viewControl.Values.Count > 1)
        return;
      object obj = viewControl.Values.Cast<object>().Single<object>();
      foreach (TEntity entity in (IEnumerable<TEntity>) entities)
        this.SetCopiedValue(obj, (object) entity, this.modelMemberAccess);
    }
    else
    {
      foreach (TEntity entity in (IEnumerable<TEntity>) entities)
        this.SetCopiedValue(this.GetCopiedValue((object) this.ViewControl, this.uiMemberAccess), (object) entity, this.modelMemberAccess);
    }
  }

  public void EnableViewControl(bool enable)
  {
    if (this.IsEnabledForced.HasValue)
      this.ViewControl.IsReadOnly = !this.IsEnabledForced.Value;
    else
      this.ViewControl.IsReadOnly = !enable;
  }

  private void ValidateViewControl()
  {
    if (!(this.ViewControl is IValidatableControl viewControl))
      return;
    viewControl.Validate();
  }

  private object GetCopiedValue(object source, MemberExpression memberAccess)
  {
    object copiedValue = this.GetMemberObject(source, memberAccess);
    if (copiedValue != null && copiedValue is ICloneable)
      copiedValue = (copiedValue as ICloneable).Clone();
    return copiedValue;
  }

  private void SetCopiedValue(object value, object target, MemberExpression memberAccess)
  {
    if (value != null && value is ICloneable)
      value = (value as ICloneable).Clone();
    Type propertyType = ((PropertyInfo) memberAccess.Member).PropertyType;
    if (value != null && !propertyType.IsAssignableFrom(value.GetType()))
    {
      if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
        value = new NullableConverter(propertyType).ConvertFrom(value);
      else if (value is IList && propertyType.IsGenericType && (typeof (IList).IsAssignableFrom(propertyType) || propertyType.GetGenericTypeDefinition() == typeof (IList<>)))
      {
        IList instance = Activator.CreateInstance(typeof (List<>).MakeGenericType(propertyType.GetGenericArguments()[0])) as IList;
        foreach (object obj in (IEnumerable) (value as IList))
          instance?.Add(obj);
        value = (object) instance;
      }
      else
      {
        try
        {
          value = Convert.ChangeType(value, propertyType);
        }
        catch (InvalidCastException ex)
        {
          value = (object) null;
        }
      }
    }
    this.SetMemberObject(value, target, memberAccess);
  }

  private void SetMemberObject(object value, object target, MemberExpression me)
  {
    try
    {
      if (me.Expression is MemberExpression)
        target = this.GetMemberObject(target, me.Expression as MemberExpression);
      MethodInfo setMethod = ((PropertyInfo) me.Member).GetSetMethod();
      if (!(setMethod != (MethodInfo) null))
        return;
      setMethod.Invoke(target, new object[1]{ value });
    }
    catch (System.Exception ex)
    {
      ModelMapItem<TEntity>.Logger.Error(ex, "Failure while setting member for property [{0}] [{1}]", target, me != null ? (object) me.Member.ToString() : (object) string.Empty);
      throw;
    }
  }

  private object GetMemberObject(object source, MemberExpression me)
  {
    if (me.Expression is MemberExpression)
    {
      object obj = this.GetMemberObject(source, me.Expression as MemberExpression);
      if (obj == null && me.Expression.Type.GetConstructor(Type.EmptyTypes) != (ConstructorInfo) null)
      {
        obj = Activator.CreateInstance(me.Expression.Type);
        this.SetMemberObject(obj, source, me.Expression as MemberExpression);
      }
      source = obj;
    }
    if (source == null)
      return (object) null;
    return (object) (me.Member as PropertyInfo) != null ? (me.Member as PropertyInfo).GetGetMethod().Invoke(source, (object[]) null) : source;
  }

  private MemberExpression GetMemberExpression(Expression expression)
  {
    if (expression is LambdaExpression lambdaExpression)
    {
      MemberExpression memberExpression = (MemberExpression) null;
      if (lambdaExpression.Body is MemberExpression)
        memberExpression = lambdaExpression.Body as MemberExpression;
      else if (lambdaExpression.Body is UnaryExpression)
        memberExpression = (lambdaExpression.Body as UnaryExpression).Operand as MemberExpression;
      if (memberExpression != null)
        return memberExpression;
    }
    throw ExceptionFactory.Instance.CreateException<ModelMapException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("NoPropertyExpressionGiven"), (object) expression));
  }
}
