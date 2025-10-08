// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Culture.CultureManager
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Culture;
using PathMedical.Exception;
using PathMedical.Logging;
using System;
using System.Globalization;
using System.Threading;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Culture;

public class CultureManager : ICultureManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (CultureManager), "$Rev: 1593 $");
  private CultureInfo cultureInfo;

  public void SetCulture(CultureInfo cultureToSet)
  {
    if (cultureToSet == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (cultureToSet));
    try
    {
      this.cultureInfo = cultureToSet;
    }
    catch (NotSupportedException ex)
    {
      CultureManager.Logger.Error((System.Exception) ex, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("CantSwitchCulture"), (object) Thread.CurrentThread.CurrentUICulture, (object) cultureToSet);
    }
    catch (ArgumentException ex)
    {
      CultureManager.Logger.Error((System.Exception) ex, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("CantSwitchCulture"), (object) Thread.CurrentThread.CurrentUICulture, (object) cultureToSet);
    }
    catch (FormatException ex)
    {
      CultureManager.Logger.Error((System.Exception) ex, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("CantSwitchCulture"), (object) Thread.CurrentThread.CurrentUICulture, (object) cultureToSet);
    }
    catch (OutOfMemoryException ex)
    {
      CultureManager.Logger.Error((System.Exception) ex, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("CantSwitchCulture"), (object) Thread.CurrentThread.CurrentUICulture, (object) cultureToSet);
    }
  }

  public CultureInfo GetCurrentUICulture() => Thread.CurrentThread.CurrentUICulture;
}
