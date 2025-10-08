// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.VistaDB.Properties.Resources
// Assembly: PM.DatabaseManagement.VistaDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 07F3111D-3061-4F48-BD47-8636F088222C
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.VistaDB.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.DatabaseManagement.VistaDB.Properties;

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
      if (PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceMan == null)
        PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceMan = new ResourceManager("PathMedical.DatabaseManagement.VistaDB.Properties.Resources", typeof (PathMedical.DatabaseManagement.VistaDB.Properties.Resources).Assembly);
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture;
    set => PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture = value;
  }

  internal static string CantConnectToDatabase
  {
    get
    {
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.ResourceManager.GetString(nameof (CantConnectToDatabase), PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToCloseConnection
  {
    get
    {
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.ResourceManager.GetString(nameof (UnableToCloseConnection), PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToCommitTransaction
  {
    get
    {
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.ResourceManager.GetString(nameof (UnableToCommitTransaction), PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture);
    }
  }

  internal static string UnableToRollTransactionBack
  {
    get
    {
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.ResourceManager.GetString(nameof (UnableToRollTransactionBack), PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture);
    }
  }

  internal static string UndefinedConnectionString
  {
    get
    {
      return PathMedical.DatabaseManagement.VistaDB.Properties.Resources.ResourceManager.GetString(nameof (UndefinedConnectionString), PathMedical.DatabaseManagement.VistaDB.Properties.Resources.resourceCulture);
    }
  }
}
