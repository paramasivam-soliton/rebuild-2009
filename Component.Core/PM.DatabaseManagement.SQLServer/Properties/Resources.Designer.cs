// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.SQLServer.Properties.Resources
// Assembly: PM.DatabaseManagement.SQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B7FE44B-84F0-46A5-B0C2-93A6A55264BB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.SQLServer.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.DatabaseManagement.SQLServer.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceMan == null)
        PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceMan = new ResourceManager("PathMedical.DatabaseManagement.SQLServer.Properties.Resources", typeof (PathMedical.DatabaseManagement.SQLServer.Properties.Resources).Assembly);
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture;
    set => PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture = value;
  }

  internal static string CantConnectToDatabase
  {
    get
    {
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.ResourceManager.GetString(nameof (CantConnectToDatabase), PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToCloseConnection
  {
    get
    {
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.ResourceManager.GetString(nameof (UnableToCloseConnection), PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToCommitTransaction
  {
    get
    {
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.ResourceManager.GetString(nameof (UnableToCommitTransaction), PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToRollTransactionBack
  {
    get
    {
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.ResourceManager.GetString(nameof (UnableToRollTransactionBack), PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture);
    }
  }

  internal static string UndefinedConnectionString
  {
    get
    {
      return PathMedical.DatabaseManagement.SQLServer.Properties.Resources.ResourceManager.GetString(nameof (UndefinedConnectionString), PathMedical.DatabaseManagement.SQLServer.Properties.Resources.resourceCulture);
    }
  }
}
