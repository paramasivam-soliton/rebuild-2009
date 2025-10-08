// Decompiled with JetBrains decompiler
// Type: PathMedical.Property.PropertyHelper`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace PathMedical.Property;

public class PropertyHelper<T> where T : Attribute
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(nameof (PropertyHelper<T>), "$Rev: 1308 $");

  public static object GetPropertyValue(string propertyName, object data)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    object propertyValue = (object) null;
    try
    {
      string[] source = propertyName.Split('.');
      string propertyName1 = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
      {
        object valueByPropertyName = PropertyHelper<T>.GetValueByPropertyName(propertyName1, data);
        if (valueByPropertyName != null)
          propertyValue = PropertyHelper<T>.GetPropertyValue(string.Join(".", source, 1, source.Length - 1), valueByPropertyName);
      }
      else
        propertyValue = PropertyHelper<T>.GetValueByPropertyName(propertyName1, data);
      PropertyHelper<T>.Logger.Debug("Delivering value [{0}] for property [{1}].", propertyValue, (object) propertyName);
    }
    catch (System.Exception ex)
    {
      string message = $"Failure while converting data for property [{propertyName}]";
      PropertyHelper<T>.Logger.Error(ex, message);
      throw;
    }
    return propertyValue;
  }

  public static object GetPropertyValue(string propertyName, string index, object data)
  {
    object propertyValue = (object) null;
    try
    {
      string[] source = propertyName.Split('.');
      string propertyName1 = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
      {
        object valueByPropertyName = PropertyHelper<T>.GetValueByPropertyName(propertyName1, data);
        if (valueByPropertyName != null)
          propertyValue = PropertyHelper<T>.GetPropertyValue(string.Join(".", source, 1, source.Length - 1), index, valueByPropertyName);
      }
      else if (data is IEnumerable)
      {
        int int32 = Convert.ToInt32(index);
        if (data is IList list)
        {
          if (int32 < list.Count)
            propertyValue = PropertyHelper<T>.GetValueByPropertyName(propertyName1, list[int32]);
        }
      }
    }
    catch (System.Exception ex)
    {
      string message = $"Failure while converting data for property [{propertyName}]";
      PropertyHelper<T>.Logger.Error(ex, message);
      throw;
    }
    return propertyValue;
  }

  public static List<object> GetPropertyArray(string propertyName, object data)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    List<object> source1 = (List<object>) null;
    try
    {
      string[] source2 = propertyName.Split('.');
      string propertyName1 = source2[0];
      if (((IEnumerable<string>) source2).Count<string>() > 1)
      {
        object valueByPropertyName = PropertyHelper<T>.GetValueByPropertyName(propertyName1, data);
        if (valueByPropertyName != null)
          source1 = PropertyHelper<T>.GetPropertyArray(string.Join(".", source2, 1, source2.Length - 1), valueByPropertyName);
      }
      else
        source1 = PropertyHelper<T>.GetArrayValuesByPropertyName(propertyName1, data);
      PropertyHelper<T>.Logger.Debug("Delivering list with [{0}] item(s) for property [{1}].", (object) (source1 != null ? source1.Count<object>() : 0), (object) propertyName);
    }
    catch (System.Exception ex)
    {
      string message = $"Failure while converting array data for property [{propertyName}]";
      PropertyHelper<T>.Logger.Error(ex, message);
      throw;
    }
    return source1;
  }

  public static void SetValue(string propertyName, object data, ref object target)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    if (target == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (target));
    try
    {
      string[] source = propertyName.Split('.');
      string propertyName1 = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
      {
        object classInstance = target;
        for (int index = 0; index < ((IEnumerable<string>) source).Count<string>() - 1; ++index)
          classInstance = PropertyHelper<T>.GetValue(classInstance, source[index]);
        PropertyHelper<T>.SetValueByPropertyName(source[source.Length - 1], data, ref classInstance);
      }
      else
        PropertyHelper<T>.SetValueByPropertyName(propertyName1, data, ref target);
    }
    catch (System.Exception ex)
    {
      PropertyHelper<T>.Logger.Error(ex, "Failure while setting value [{0}] for property [{1}] in class [{2}]", data, (object) propertyName, target);
      throw;
    }
  }

  private static List<object> GetArrayValuesByPropertyName(string propertyName, object data)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    List<object> valuesByPropertyName = new List<object>();
    Type type = (Type) null;
    if (data != null)
      type = data.GetType();
    if (type != (Type) null)
    {
      PropertyInfo propertyInfo = type.GetPropertiesWithAttribute<T>().FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (pi => PropertyHelper<T>.GetColumnName(pi) == propertyName));
      if (propertyInfo != (PropertyInfo) null && propertyInfo.GetValue(data, (object[]) null) is IEnumerable enumerable)
      {
        foreach (object obj in enumerable)
          valuesByPropertyName.Add(obj);
      }
    }
    return valuesByPropertyName;
  }

  private static object GetValueByPropertyName(string propertyName, object classInstance)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    object valueByPropertyName = (object) null;
    if (classInstance != null)
    {
      PropertyInfo propertyInfo = classInstance.GetType().GetPropertiesWithAttribute<T>().FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (pi => PropertyHelper<T>.GetColumnName(pi) == propertyName));
      valueByPropertyName = propertyInfo != (PropertyInfo) null ? propertyInfo.GetValue(classInstance, (object[]) null) : throw ExceptionFactory.Instance.CreateException<PropertyNotFoundException>(string.Format(Resources.PropertyHelper_NoMemberPropertyFound, (object) propertyName));
    }
    return valueByPropertyName;
  }

  private static string GetColumnName(PropertyInfo propertyInfo)
  {
    string columnName = propertyInfo.Name;
    if (typeof (T) == typeof (DataExchangeColumnAttribute))
    {
      DataExchangeColumnAttribute customAttribute = (DataExchangeColumnAttribute) propertyInfo.GetCustomAttributes(typeof (DataExchangeColumnAttribute), true)[0];
      if (customAttribute != null && customAttribute.Identifier != null)
        columnName = customAttribute.Identifier as string;
    }
    return columnName;
  }

  private static void SetValueByPropertyName(
    string propertyName,
    object value,
    ref object classInstance)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    if (classInstance == null || value == null)
      return;
    Type type = value.GetType();
    PropertyInfo propertyInfo = classInstance.GetType().GetPropertiesWithAttribute<T>().FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (pi => PropertyHelper<T>.GetColumnName(pi) == propertyName));
    if (!(propertyInfo != (PropertyInfo) null))
      throw ExceptionFactory.Instance.CreateException<PropertyNotFoundException>(string.Format(Resources.PropertyHelper_NoMemberPropertyFound, (object) propertyName));
    object obj1 = value;
    Type propertyType = propertyInfo.PropertyType;
    TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
    if (converter != null && converter.CanConvertFrom(obj1.GetType()))
      obj1 = converter.ConvertFrom(obj1);
    else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
      obj1 = new NullableConverter(propertyType).ConvertFrom((object) Convert.ToString(value));
    else if (propertyType.IsArray)
    {
      Array array = obj1 as Array;
      Type elementType = propertyType.GetElementType();
      if (array != null)
      {
        Array instance = Array.CreateInstance(elementType, array.Length);
        for (int index = 0; index < array.Length; ++index)
        {
          object obj2 = Convert.ChangeType(array.GetValue(index), elementType);
          instance.SetValue(obj2, index);
        }
        obj1 = (object) instance;
      }
    }
    try
    {
      propertyInfo.SetValue(classInstance, obj1, (object[]) null);
      if (!(typeof (T) == typeof (DataExchangeColumnAttribute)))
        return;
      DataExchangeColumnAttribute exchangeColumnAttribute = (DataExchangeColumnAttribute) ((IEnumerable<object>) propertyInfo.GetCustomAttributes(typeof (DataExchangeColumnAttribute), true)).FirstOrDefault<object>();
      if (exchangeColumnAttribute == null)
        return;
      exchangeColumnAttribute.ValueLoaded = true;
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PropertyAssignmentException>(string.Format(Resources.PropertyHelper_ValueAssignmentException, (object) propertyName, (object) propertyType, obj1, (object) type), (System.Exception) ex);
    }
    catch (TargetException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PropertyAssignmentException>(string.Format(Resources.PropertyHelper_ValueAssignmentException, (object) propertyName, (object) propertyType, obj1, (object) type), (System.Exception) ex);
    }
    catch (TargetParameterCountException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PropertyAssignmentException>(string.Format(Resources.PropertyHelper_ValueAssignmentException, (object) propertyName, (object) propertyType, obj1, (object) type), (System.Exception) ex);
    }
    catch (MethodAccessException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PropertyAssignmentException>(string.Format(Resources.PropertyHelper_ValueAssignmentException, (object) propertyName, (object) propertyType, obj1, (object) type), (System.Exception) ex);
    }
    catch (TargetInvocationException ex)
    {
      throw ExceptionFactory.Instance.CreateException<PropertyAssignmentException>(string.Format(Resources.PropertyHelper_ValueAssignmentException, (object) propertyName, (object) propertyType, obj1, (object) type), (System.Exception) ex);
    }
  }

  private static object GetValue(object source, string propertyName)
  {
    if (string.IsNullOrEmpty(propertyName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyName));
    object obj = (object) null;
    if (source != null)
    {
      PropertyInfo propertyInfo = source.GetType().GetPropertiesWithAttribute<T>().FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (pi => PropertyHelper<T>.GetColumnName(pi) == propertyName));
      obj = propertyInfo != (PropertyInfo) null ? propertyInfo.GetValue(source, (object[]) null) : throw ExceptionFactory.Instance.CreateException<PropertyNotFoundException>(string.Format(Resources.PropertyHelper_NoMemberPropertyFound, (object) propertyName));
      if (obj == null)
      {
        try
        {
          obj = Activator.CreateInstance(propertyInfo.PropertyType);
        }
        catch (ArgumentException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (NotSupportedException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (TargetInvocationException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (MethodAccessException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (MemberAccessException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (InvalidComObjectException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (COMException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        catch (TypeLoadException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PropertyCreationException>(string.Format(Resources.PropertyHelper_InstanceCreationException, (object) propertyInfo.PropertyType, (object) propertyName), (System.Exception) ex);
        }
        propertyInfo.SetValue(source, obj, (object[]) null);
      }
    }
    return obj;
  }
}
