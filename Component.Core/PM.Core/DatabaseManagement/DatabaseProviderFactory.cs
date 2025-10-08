// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.DatabaseProviderFactory
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Properties;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

#nullable disable
namespace PathMedical.DatabaseManagement;

public class DatabaseProviderFactory
{
  private IDatabaseProvider databaseProvider;

  public static DatabaseProviderFactory Instance => PathMedical.Singleton.Singleton<DatabaseProviderFactory>.Instance;

  private DatabaseProviderFactory()
  {
  }

  public IDatabaseProvider DatabaseProvider
  {
    get => this.databaseProvider;
    set => this.databaseProvider = value;
  }

  public string ConnectionString
  {
    get => this.databaseProvider.ConnectionString;
    set => this.databaseProvider.ConnectionString = value;
  }

  public DbTransaction GetOpenTransaction()
  {
    return this.databaseProvider != null ? this.databaseProvider.GetOpenTransaction() : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
  }

  public void CommitTransaction(DbTransaction transactionToCommit)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    this.databaseProvider.CommitTransaction(transactionToCommit);
  }

  public void RollbackTransaction(DbTransaction transactionToRollback)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    this.databaseProvider.RollbackTransaction(transactionToRollback);
  }

  public void CloseConnection(DbTransaction transactionToClose)
  {
    this.databaseProvider.CloseConnection(transactionToClose);
  }

  public void CloseConnection(DbConnection connectionToClose)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    this.databaseProvider.CloseConnection(connectionToClose);
  }

  public void TestConnection()
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    this.databaseProvider.TestConnection();
  }

  public DbCommand GetDbCommand()
  {
    return this.databaseProvider != null ? this.databaseProvider.GetDbCommand() : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
  }

  public DbCommand GetDbCommand(string queryText, DbTransaction transactionToUse)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider);
    return this.databaseProvider.GetDbCommand(queryText, transactionToUse);
  }

  public DbCommand GetDbCommand(string queryText, DbConnection connectionToUse)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    return this.databaseProvider.GetDbCommand(queryText, connectionToUse);
  }

  public DbParameter GetDbParameter(
    string parameterName,
    DbType parameterType,
    object parameterValue)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    return this.databaseProvider.GetDbParameter(parameterName, parameterType, parameterValue);
  }

  public DbParameter GetDbParameter(string parameterName, object parameterValue)
  {
    if (this.databaseProvider == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.DatabaseProviderFactory_UnconfiguredDatabaseProvider));
    return this.databaseProvider.GetDbParameter(parameterName, parameterValue);
  }
}
