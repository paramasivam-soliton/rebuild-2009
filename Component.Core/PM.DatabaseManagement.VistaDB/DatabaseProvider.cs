// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.VistaDB.DatabaseProvider
// Assembly: PM.DatabaseManagement.VistaDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 07F3111D-3061-4F48-BD47-8636F088222C
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.VistaDB.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Plugin;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using VistaDB.Diagnostic;
using VistaDB.Provider;

#nullable disable
namespace PathMedical.DatabaseManagement.VistaDB;

public sealed class DatabaseProvider : IDatabaseProvider, IPlugin
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (DatabaseProvider), "$Rev: 1473 $");
  private readonly string name = "VistaDB";
  private readonly string description = "Provider for database access to VistaDB";
  private readonly Guid fingerprint = new Guid("3CDB9E14-9C75-49e0-A60F-F235DC6606DB");
  private IsolationLevel isolationLevel;
  private string connectionString;

  public static DatabaseProvider Instance => PathMedical.Singleton.Singleton<DatabaseProvider>.Instance;

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

  private DatabaseProvider() => this.isolationLevel = IsolationLevel.Snapshot;

  public DbTransaction GetOpenTransaction()
  {
    if (string.IsNullOrEmpty(this.connectionString))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(ComponentResourceManager.Instance.ResourceManager.GetString("UndefinedConnectionString"));
    VistaDBConnection connectionToClose = (VistaDBConnection) null;
    try
    {
      connectionToClose = new VistaDBConnection(this.connectionString);
      connectionToClose.Open();
      return (DbTransaction) connectionToClose.BeginTransaction();
    }
    catch (VistaDBException ex)
    {
      this.CloseConnection((DbConnection) connectionToClose);
      throw ExceptionFactory.Instance.CreateException<ConnectionException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, ComponentResourceManager.Instance.ResourceManager.GetString("CantConnectToDatabase"), (object) this.connectionString, (object) AppDomain.CurrentDomain.BaseDirectory), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProvider.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("CantConnectToDatabase"), (object) this.connectionString, (object) AppDomain.CurrentDomain.BaseDirectory);
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
    catch (VistaDBException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCommitTransaction"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProvider.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCommitTransaction"));
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
    catch (VistaDBException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToRollTransactionBack"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProvider.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToRollTransactionBack"));
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
    catch (VistaDBException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCloseConnection"), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      DatabaseProvider.logger.Error(ex, ComponentResourceManager.Instance.ResourceManager.GetString("UnableToCloseConnection"));
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

  public DbCommand GetDbCommand() => (DbCommand) new VistaDBCommand();

  public DbCommand GetDbCommand(string queryText, DbTransaction transactionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (transactionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (transactionToUse));
    if (!(transactionToUse is VistaDBTransaction))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (transactionToUse));
    if (!(transactionToUse.Connection is VistaDBConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("transactionToUse.Connection");
    DbCommand dbCommand = (DbCommand) null;
    if (transactionToUse is VistaDBTransaction vistaDbTransaction)
      dbCommand = (DbCommand) new VistaDBCommand(queryText, vistaDbTransaction.Connection);
    return dbCommand;
  }

  public DbCommand GetDbCommand(string queryText, DbConnection connectionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (connectionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (connectionToUse));
    if (!(connectionToUse is VistaDBConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (connectionToUse));
    DbCommand dbCommand = (DbCommand) null;
    if (connectionToUse is VistaDBConnection connection)
      dbCommand = (DbCommand) new VistaDBCommand(queryText, connection);
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
    VistaDBParameter dbParameter = new VistaDBParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.DbType = parameterType;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }

  public DbParameter GetDbParameter(string parameterName, object parameterValue)
  {
    if (string.IsNullOrEmpty(parameterName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (parameterName));
    VistaDBParameter dbParameter = new VistaDBParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }
}
