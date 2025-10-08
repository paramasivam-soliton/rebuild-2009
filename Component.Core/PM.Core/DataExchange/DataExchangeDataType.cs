// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.DataExchangeDataType
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.DataExchange;

public class DataExchangeDataType
{
  public string Name { get; protected set; }

  public Type Type { get; protected set; }

  public DataExchangeDataType(string name, Type type)
  {
    this.Name = name;
    this.Type = type;
  }

  public DataExchangeDataType(DataTypes dataType)
  {
    switch (dataType)
    {
      case DataTypes.Guid:
        this.AssignMembers("Guid", typeof (Guid));
        break;
      case DataTypes.Boolean:
        this.AssignMembers("Boolean", typeof (bool));
        break;
      case DataTypes.String:
        this.AssignMembers("String", typeof (object));
        break;
      case DataTypes.Int8:
        this.AssignMembers("Int8", typeof (sbyte));
        break;
      case DataTypes.Int8Array:
        this.AssignMembers("Int8Array", typeof (sbyte[]));
        break;
      case DataTypes.UInt8:
        this.AssignMembers("UInt8", typeof (byte));
        break;
      case DataTypes.UInt8Array:
        this.AssignMembers("UInt8Array", typeof (byte[]));
        break;
      case DataTypes.Int16:
        this.AssignMembers("Int16", typeof (short));
        break;
      case DataTypes.Int16Array:
        this.AssignMembers("Int16Array", typeof (short[]));
        break;
      case DataTypes.UInt16:
        this.AssignMembers("UInt16", typeof (ushort));
        break;
      case DataTypes.UInt16Array:
        this.AssignMembers("UInt16Array", typeof (ushort[]));
        break;
      case DataTypes.Int32:
        this.AssignMembers("Int32", typeof (int));
        break;
      case DataTypes.UInt32:
        this.AssignMembers("UInt32", typeof (uint));
        break;
      case DataTypes.Float:
        this.AssignMembers("Float", typeof (float));
        break;
      case DataTypes.FloatArray:
        this.AssignMembers("FloatArray", typeof (float[]));
        break;
      case DataTypes.Fract16:
        this.AssignMembers("Fract16", typeof (float));
        break;
      case DataTypes.DateTime:
        this.AssignMembers("DateTime", typeof (DateTime));
        break;
      default:
        this.AssignMembers("Unkown", typeof (object));
        break;
    }
  }

  public static object CreateInstance(DataTypes dataType, object value)
  {
    return new DataExchangeDataType(dataType).CreateInstanceWithValue(value);
  }

  private void AssignMembers(string name, Type type)
  {
    this.Name = name;
    this.Type = type;
  }

  private object CreateInstanceWithValue(object value)
  {
    int length = 0;
    try
    {
      object obj1 = (object) null;
      if (value != null && this.Type.IsAssignableFrom(value.GetType()))
      {
        if (this.Type.IsGenericType && this.Type.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
          obj1 = new NullableConverter(this.Type).ConvertFrom(value);
        else if (this.Type.IsArray)
        {
          Array array = value as Array;
          Type elementType = this.Type.GetElementType();
          if (array != null)
          {
            Array instance = Array.CreateInstance(elementType, array.Length);
            length = array.Length;
            for (int index = 0; index < length; ++index)
            {
              object obj2 = Convert.ChangeType(array.GetValue(index), elementType);
              instance.SetValue(obj2, index);
            }
            obj1 = (object) instance;
          }
        }
        else
          obj1 = !(value is IConvertible) ? value : Convert.ChangeType(value, this.Type);
      }
      object instanceWithValue = this.Type.IsArray ? (object) Array.CreateInstance(this.Type, length) : Activator.CreateInstance(this.Type);
      if (instanceWithValue != null)
        instanceWithValue = obj1;
      return instanceWithValue;
    }
    catch (ArgumentException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
