// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.SQLServer.DatabaseProvider
// Assembly: PM.DatabaseManagement.SQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B7FE44B-84F0-46A5-B0C2-93A6A55264BB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.SQLServer.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Plugin;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

#nullable disable
namespace PathMedical.DatabaseManagement.SQLServer;

public sealed class DatabaseProvider : IDatabaseProvider, IPlugin
{
  private static ILogger logger = LogFactory.Instance.Create(typeof (DatabaseProvider), "$Rev: 1473 $");
  private readonly string name = "MS SQL Server";
  private readonly string description = "Provider for database access to MS SQL Server Express, Standard and Enterprise Edition";
  private readonly Guid fingerprint = new Guid("18DE9D17-450E-4b07-85C0-C9112EB7F345");
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

  private DatabaseProvider() => this.isolationLevel = IsolationLevel.ReadCommitted;

  public DbTransaction GetOpenTransaction()
  {
    if (string.IsNullOrEmpty(this.connectionString))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(ComponentResourceManager.Instance.ResourceManager.GetString("UndefinedConnectionString"));
    SqlConnection connectionToClose = (SqlConnection) null;
    try
    {
      connectionToClose = new SqlConnection(this.connectionString);
      connectionToClose.Open();
      return (DbTransaction) connectionToClose.BeginTransaction();
    }
    catch (SqlException ex)
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
    catch (SqlException ex)
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
    catch (SqlException ex)
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
    catch (SqlException ex)
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

  public DbCommand GetDbCommand() => (DbCommand) new SqlCommand();

  public DbCommand GetDbCommand(string queryText, DbTransaction transactionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (transactionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (transactionToUse));
    if (!(transactionToUse is SqlTransaction))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (transactionToUse));
    if (!(transactionToUse.Connection is SqlConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("transactionToUse.Connection");
    DbCommand dbCommand = (DbCommand) null;
    if (transactionToUse is SqlTransaction sqlTransaction)
      dbCommand = (DbCommand) new SqlCommand(queryText, sqlTransaction.Connection);
    return dbCommand;
  }

  public DbCommand GetDbCommand(string queryText, DbConnection connectionToUse)
  {
    if (string.IsNullOrEmpty(queryText))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (queryText));
    if (connectionToUse == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (connectionToUse));
    if (!(connectionToUse is SqlConnection))
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (connectionToUse));
    DbCommand dbCommand = (DbCommand) null;
    if (connectionToUse is SqlConnection connection)
      dbCommand = (DbCommand) new SqlCommand(queryText, connection);
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
    SqlParameter dbParameter = new SqlParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.DbType = parameterType;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }

  public DbParameter GetDbParameter(string parameterName, object parameterValue)
  {
    if (string.IsNullOrEmpty(parameterName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (parameterName));
    SqlParameter dbParameter = new SqlParameter();
    dbParameter.ParameterName = parameterName;
    dbParameter.Value = parameterValue;
    return (DbParameter) dbParameter;
  }
}
