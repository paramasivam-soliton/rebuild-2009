// Decompiled with JetBrains decompiler
// Type: PathMedical.Reflection.ReflectionHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security;

#nullable disable
namespace PathMedical.Reflection;

public static class ReflectionHelper
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ReflectionHelper));

  public static T GetClass<T>(object[] args) where T : class
  {
    T obj1 = default (T);
    List<Type> typeList = new List<Type>();
    List<object> objectList = new List<object>();
    foreach (object obj2 in args)
    {
      typeList.Add(obj2.GetType());
      objectList.Add(obj2);
    }
    try
    {
      ConstructorInfo constructor = typeof (T).GetConstructor(typeList.ToArray());
      object obj3 = constructor != (ConstructorInfo) null ? constructor.Invoke(objectList.ToArray()) : throw new ReflectionHelperException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The requested class doesn't support the required constructor."));
      if (obj3 != null)
        obj1 = obj3 as T;
    }
    catch (ArgumentException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (MemberAccessException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (TargetInvocationException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (TargetParameterCountException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (NotSupportedException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (SecurityException ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error((Exception) ex, message);
      throw new ReflectionHelperException(message, (Exception) ex);
    }
    catch (Exception ex)
    {
      string message = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failure while constructing instance of {0}", (object) typeof (T));
      ReflectionHelper.Logger.Error(ex, message);
      throw;
    }
    return obj1;
  }
}
