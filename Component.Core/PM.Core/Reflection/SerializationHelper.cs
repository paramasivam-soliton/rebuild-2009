// Decompiled with JetBrains decompiler
// Type: PathMedical.Reflection.SerializationHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Property;
using System;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.Reflection;

public static class SerializationHelper
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(nameof (SerializationHelper), "$Rev$");

  public static void Create<TAttribute>(
    SerializationInfo info,
    StreamingContext ctxt,
    object entity)
    where TAttribute : Attribute
  {
    if (entity == null || !entity.GetType().FullName.Equals(info.FullTypeName))
      return;
    foreach (SerializationEntry serializationEntry in info)
    {
      string name = serializationEntry.Name;
      if (!string.IsNullOrEmpty(name))
      {
        string oldValue = name.Substring(0, name.LastIndexOf('.') + 1);
        string propertyName = name.Replace(oldValue, string.Empty);
        object data = serializationEntry.Value;
        try
        {
          PropertyHelper<TAttribute>.SetValue(propertyName, data, ref entity);
        }
        catch (ArgumentNullException ex)
        {
          SerializationHelper.Logger.Error((Exception) ex, "Failure while deserializing an entity field [{0}] in class [{1}].", (object) propertyName, (object) oldValue);
        }
        catch (PropertyNotFoundException ex)
        {
          SerializationHelper.Logger.Error((Exception) ex, "Failure while deserializing an entity. The field [{0}] in class [{1}] was not found.", (object) propertyName, (object) oldValue);
        }
        catch (PropertyAssignmentException ex)
        {
          SerializationHelper.Logger.Error((Exception) ex, "Failure while deserializing an entity field [{0}] in class [{1}].", (object) propertyName, (object) oldValue);
        }
      }
    }
  }

  public static void Serialize<TAttribute>(
    SerializationInfo info,
    StreamingContext context,
    object entity)
    where TAttribute : Attribute
  {
    Type type = entity.GetType();
    if (!info.FullTypeName.Equals(type.FullName))
      return;
    foreach (PropertyInfo propertyInfo in type.GetPropertiesWithAttribute<TAttribute>())
    {
      string name = $"{(propertyInfo.ReflectedType != (Type) null ? (object) propertyInfo.ReflectedType.FullName : (object) string.Empty)}.{propertyInfo.Name}";
      object propertyValue = PropertyHelper<TAttribute>.GetPropertyValue(propertyInfo.Name, entity);
      info.AddValue(name, propertyValue);
    }
  }
}
