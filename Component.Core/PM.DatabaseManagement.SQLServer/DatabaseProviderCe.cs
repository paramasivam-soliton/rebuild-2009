// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.SQLServer.DatabaseProviderCe
// Assembly: PM.DatabaseManagement.SQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B7FE44B-84F0-46A5-B0C2-93A6A55264BB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.SQLServer.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Plugin;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Globalization;

#nullable disable
namespace PathMedical.DatabaseManagement.SQLServer;

public sealed class DatabaseProviderCe : IDatabaseProvider, IPlugin
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (DatabaseProviderCe), "$Rev: 1473 $");
  private readonly string name = "MS SQL Server CE";
  private readonly string description = "Provider for database access to MS SQL Server Compact Edition";
  private readonly Guid fingerprint = new Guid("6F7B317F-0B9D-4aba-BDA6-139EFADE18AB");
  private IsolationLevel isolationLevel;
  private string connectionString;

  public static DatabaseProviderCe Instance => PathMedical.Singleton.Singleton<DatabaseProviderCe>.Instance;

  public string Name => this.name;

  public string Description => this.description;

  public Guid Fingerprint => this.fingerprint;

  public int LoadPriority { get; protected set; }

  public IsolationLevel IsolationLevel
  {
    get => this.isolationLevel;
    set => this.isolationLevel = value;
  }

  public string ConnectionString
  {
    get => this.connectionString;
    set => this.connectionString = value;
  }

  private DatabaseProviderCe() => this.isolationLevel = IsolationLevel.ReadCommitted;

  public DbTransaction GetOpenTransaction()
  {
    if (string.IsNullOrEmpty(this.connectionString))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(ComponentResourceManager.Instance.ResourceManager.GetString("UndefinedConnectionString"));
    SqlCeConnection connectionToClose = (SqlCeConnection) null;
    try
    {
      connectionToClose = new SqlCeConnection(this.connectionString);
      connectionToClose.Open();
      return (DbTransaction) connectionToClose.BeginTransaction();
    }
    catch (SqlCeException ex)
    {
      this.CloseConnection((DbConnection) connectionToClose);
      throw ExceptionFactory.Instance.CreateException<ConnectionException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, ComponentResourceManager.Instance.ResourceManager.GetString("CantConnectToDatabase"), (object) this.connectionString, (object) AppDomain.CurrentDomain.BaseDirectory), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProviderCe.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("CantConnectToDatabase"), (object) this.connectionString, (object) AppDomain.CurrentDomain.BaseDirectory);
      this.CloseConnection((DbConnection) connectionToClose);
      throw;
    }
  }

  public void CommitTransaction(DbTransaction transactionToCommit)
  {
    if (transactionToCommit == null)
      return;
    try
    {
      transactionToCommit.Commit();
    }
    catch (SqlCeException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCommitTransaction"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProviderCe.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCommitTransaction"));
      throw;
    }
  }

  public void RollbackTransaction(DbTransaction transactionToRollback)
  {
    if (transactionToRollback == null)
      return;
    try
    {
      transactionToRollback.Rollback();
    }
    catch (SqlCeException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToRollTransactionBack"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProviderCe.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToRollTransactionBack"));
      throw;
    }
  }

  public void CloseConnection(DbTransaction transactionToClose)
  {
    if (transactionToClose == null || transactionToClose.Connection == null)
      return;
    this.CloseConnection(transactionToClose.Connection);
  }

  public void CloseConnection(DbConnection connectionToClose)
  {
    if (connectionToClose == null)
      return;
    try
    {
      if (connectionToClose.State == ConnectionState.Closed)
        return;
      connectionToClose.Close();
    }
    catch (SqlCeException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCloseConnection"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProviderCe.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCloseConnection"));
      throw;
    }
  }

  public void TestConnection()
  {
    DbTransaction transactionToClose = (DbTransaction) null;
    try
    {
      transactionToClose = this.GetOpenTransaction();
    }
    finally
    {
      this.CloseConnection(transactionToClose);
    }
  }

  public DbCommand GetDbCommand() => (DbCommand) new SqlCeCommand();

  public DbCommand GetDbCommand(string queryText, DbTransaction transactionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (transactionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (transactionToUse));
    if (!(transactionToUse is SqlCeTransaction))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (transactionToUse));
    if (!(transactionToUse.Connection is SqlCeConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("transactionToUse.Connection");
    DbCommand dbCommand = (DbCommand) null;
    if (transactionToUse is SqlCeTransaction sqlCeTransaction)
      dbCommand = (DbCommand) new SqlCeCommand(queryText, (SqlCeConnection) ((DbTransaction) sqlCeTransaction).Connection);
    return dbCommand;
  }

  public DbCommand GetDbCommand(string queryText, DbConnection connectionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (connectionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (connectionToUse));
    if (!(connectionToUse is SqlCeConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (connectionToUse));
    DbCommand dbCommand = (DbCommand) null;
    if (connectionToUse is SqlCeConnection connection)
      dbCommand = (DbCommand) new SqlCeCommand(queryText, connection);
    return dbCommand;
  }

  public DbParameter GetDbParameter(
    string parameterName,
    DbType parameterType,
    object parameterValue)
  {
    if (string.IsNullOrEmpty(parameterName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (parameterName));
    if (!Enum.IsDefined(typeof (DbType), (object) parameterType))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (parameterType));
    SqlCeParameter dbParameter = new SqlCeParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.DbType = parameterType;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }

  public DbParameter GetDbParameter(string parameterName, object parameterValue)
  {
    if (string.IsNullOrEmpty(parameterName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (parameterName));
    SqlCeParameter dbParameter = new SqlCeParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }
}
