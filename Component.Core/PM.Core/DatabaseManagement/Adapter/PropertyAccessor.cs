// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.PropertyAccessor
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class PropertyAccessor
{
  protected PropertyAccessor.Getter getter;
  protected PropertyAccessor.Setter setter;

  public PropertyAccessor(PropertyInfo propertyInfo)
  {
    this.setter = PropertyAccessor.CreateSetter(propertyInfo);
    this.getter = PropertyAccessor.CreateGetter(propertyInfo);
  }

  public virtual void SetValue(object target, object value)
  {
    if (value == DBNull.Value)
      value = (object) null;
    this.setter(target, value);
  }

  public virtual object GetValue(object source) => this.getter(source);

  protected static PropertyAccessor.Setter CreateSetter(PropertyInfo propertyInfo)
  {
    MethodInfo setMethod = propertyInfo.GetSetMethod();
    Type[] parameterTypes = new Type[2];
    parameterTypes[0] = parameterTypes[1] = typeof (object);
    DynamicMethod dynamicMethod = new DynamicMethod($"_Set{propertyInfo.Name}_", typeof (void), parameterTypes, propertyInfo.DeclaringType);
    ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
    ilGenerator.Emit(OpCodes.Ldarg_1);
    if (propertyInfo.PropertyType.IsClass)
      ilGenerator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
    else
      ilGenerator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
    ilGenerator.EmitCall(OpCodes.Callvirt, setMethod, (Type[]) null);
    ilGenerator.Emit(OpCodes.Ret);
    return (PropertyAccessor.Setter) dynamicMethod.CreateDelegate(typeof (PropertyAccessor.Setter));
  }

  protected static PropertyAccessor.Getter CreateGetter(PropertyInfo propertyInfo)
  {
    MethodInfo getMethod = propertyInfo.GetGetMethod();
    Type[] parameterTypes = new Type[1]{ typeof (object) };
    DynamicMethod dynamicMethod = new DynamicMethod($"_Get{propertyInfo.Name}_", typeof (object), parameterTypes, propertyInfo.DeclaringType);
    ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
    ilGenerator.DeclareLocal(typeof (object));
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
    ilGenerator.EmitCall(OpCodes.Callvirt, getMethod, (Type[]) null);
    if (!propertyInfo.PropertyType.IsClass)
      ilGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
    ilGenerator.Emit(OpCodes.Ret);
    return (PropertyAccessor.Getter) dynamicMethod.CreateDelegate(typeof (PropertyAccessor.Getter));
  }

  protected delegate object Getter(object target);

  protected delegate void Setter(object target, object value);
}
