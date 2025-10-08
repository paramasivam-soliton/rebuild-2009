// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.SqlStatementHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.HistoryTracker;
using PathMedical.Login;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public static class SqlStatementHelper
{
  public const string INSERTSQL = "INSERT INTO [##TABLE##]        (##COLUMNS##) VALUES (##VALUES##);\n##HISTORY##";
  public const string UPDATESQL = "UPDATE [##TABLE##] SET    ##SETS## WHERE  [##IDCOLUMN##] = @pEntityID;\n##HISTORY##";
  public const string DELETESQL = "##HISTORY##\nDELETE FROM [##TABLE##] WHERE  [##IDCOLUMN##] = @pEntityID;";
  public const string WRITEHISTORYSQL = "INSERT INTO [##TABLE##History] (HistoryTimestamp, HistoryUserName, HistoryUserID, HistoryAction, ##COLUMNS##) VALUES (@pHistoryTimeStamp, @pHistoryUserName, @pHistoryUserID, @pHistoryAction, ##HISTORYVALUES##);";
  public const string TABLE = "##TABLE##";
  public const string IDCOLUMN = "##IDCOLUMN##";
  public const string COLUMNS = "##COLUMNS##";
  public const string VALUES = "##VALUES##";
  public const string HISTORYVALUES = "##HISTORYVALUES##";
  public const string SETS = "##SETS##";
  public const string ENTITYIDPARAM = "pEntityID";
  public const string HISTORY = "##HISTORY##";
  public const string HISTORYVERSION = "HistoryVersion";
  public const string HISTORYTIMESTAMP = "HistoryTimestamp";
  public const string HISTORYUSERNAME = "HistoryUserName";
  public const string HISTORYUSERID = "HistoryUserID";
  public const string HISTORYACTION = "HistoryAction";
  public const string HISTORYTIMESTAMPPARAM = "pHistoryTimeStamp";
  public const string HISTORYUSERNAMEPARAM = "pHistoryUserName";
  public const string HISTORYUSERIDPARAM = "pHistoryUserID";
  public const string HISTORYACTIONPARAM = "pHistoryAction";

  public static Dictionary<string, object> CreateHistoryParameters(
    DBScope scope,
    HistoryActionType? historyAction)
  {
    return new Dictionary<string, object>()
    {
      {
        "pHistoryTimeStamp",
        (object) scope.TransactionTimestamp
      },
      {
        "pHistoryUserName",
        LoginManager.Instance.LoggedInUserData != null ? (object) LoginManager.Instance.LoggedInUserData.FullName : (object) string.Empty
      },
      {
        "pHistoryUserID",
        (object) (LoginManager.Instance.LoggedInUserData != null ? LoginManager.Instance.LoggedInUserData.Id : Guid.Empty)
      },
      {
        "pHistoryAction",
        (object) historyAction
      }
    };
  }
}
