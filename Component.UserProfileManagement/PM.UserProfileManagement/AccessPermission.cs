// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.AccessPermission
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.ResourceManager;
using System;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace PathMedical.UserProfileManagement;

[DataExchangeRecord]
[DebuggerDisplay("AccessPermission: {Name}")]
public class AccessPermission
{
  private string componentDescription;
  private string componentName;
  private string description;
  private string name;

  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid ComponentId { get; set; }

  [DbRelationValue("AccessPermissionFlag")]
  [DataExchangeColumn(Identifier = "AccessGranted")]
  public bool AccessPermissionFlag { get; set; }

  public string ComponentName
  {
    get
    {
      if (string.IsNullOrEmpty(this.componentName))
      {
        GlobalResourceEnquirer instance = GlobalResourceEnquirer.Instance;
        Guid componentId = this.ComponentId;
        string resourceName = componentId.ToString() + ".Name";
        object resourceByName = instance.GetResourceByName(resourceName, (CultureInfo) null);
        string str;
        if (resourceByName == null)
        {
          componentId = this.ComponentId;
          str = componentId.ToString() + ".Name";
        }
        else
          str = resourceByName.ToString();
        this.componentName = str;
      }
      return this.componentName;
    }
  }

  public string ComponentDescription
  {
    get
    {
      if (string.IsNullOrEmpty(this.componentDescription))
      {
        GlobalResourceEnquirer instance = GlobalResourceEnquirer.Instance;
        Guid componentId = this.ComponentId;
        string resourceName = componentId.ToString() + ".Description";
        object resourceByName = instance.GetResourceByName(resourceName, (CultureInfo) null);
        string str;
        if (resourceByName == null)
        {
          componentId = this.ComponentId;
          str = componentId.ToString() + ".Description";
        }
        else
          str = resourceByName.ToString();
        this.componentDescription = str;
      }
      return this.componentDescription;
    }
  }

  [DbColumn]
  public string NameLanguageId { get; set; }

  public string Name
  {
    get
    {
      if (string.IsNullOrEmpty(this.name))
      {
        object resourceByName = GlobalResourceEnquirer.Instance.GetResourceByName(this.NameLanguageId);
        this.name = resourceByName != null ? resourceByName.ToString() : this.NameLanguageId;
      }
      return this.name;
    }
  }

  [DbColumn]
  public string DescriptionLanguageId { get; set; }

  public string Description
  {
    get
    {
      if (string.IsNullOrEmpty(this.description))
      {
        object resourceByName = GlobalResourceEnquirer.Instance.GetResourceByName(this.DescriptionLanguageId);
        this.description = resourceByName != null ? resourceByName.ToString() : this.DescriptionLanguageId;
      }
      return this.description;
    }
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is AccessPermission accessPermission && this.Id.Equals(accessPermission.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
