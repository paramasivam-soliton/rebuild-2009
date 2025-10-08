// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.DBScope
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;

#nullable disable
namespace PathMedical.DatabaseManagement;

public class DBScope : IDisposable
{
  private static readonly ILogger logger = LogFactory.Instance.Create(typeof (DBScope), "$Rev: 1276 $");
  protected static bool isSharedScope;
  protected static IDatabaseProvider dbProvider;
  protected static int sharedScopeCount;
  protected static DbTransaction sharedDbTransaction;
  protected static DateTime transactionTimestamp;
  protected bool constructorSucceeded;
  protected DbTransaction independentDbTransaction;
  protected bool isComplete;
  protected bool isOutermost;
  protected TransactionLevel transactionLevel;

  protected static IDatabaseProvider DBProvider
  {
    get
    {
      if (DBScope.dbProvider == null)
        DBScope.dbProvider = DatabaseProviderFactory.Instance.DatabaseProvider;
      return DBScope.dbProvider;
    }
  }

  public DbTransaction Transaction
  {
    get
    {
      return this.transactionLevel == TransactionLevel.Independent ? this.independentDbTransaction : DBScope.sharedDbTransaction;
    }
  }

  public bool IsActive => !this.isComplete;

  protected bool ReadyToCommit
  {
    get
    {
      return this.transactionLevel == TransactionLevel.Independent ? this.isComplete : DBScope.sharedScopeCount == 0;
    }
  }

  public DateTime TransactionTimestamp => DBScope.transactionTimestamp;

  public DBScope()
    : this(TransactionLevel.Shared)
  {
  }

  public DBScope(TransactionLevel transactionLevel)
  {
    try
    {
      this.transactionLevel = transactionLevel;
      if (transactionLevel == TransactionLevel.Independent)
      {
        this.independentDbTransaction = DBScope.DBProvider.GetOpenTransaction();
        DBScope.logger.Info("Started independent transaction.");
        this.isOutermost = true;
      }
      else
      {
        if (DBScope.sharedDbTransaction == null)
        {
          DBScope.sharedDbTransaction = DBScope.DBProvider.GetOpenTransaction();
          DBScope.logger.Info("Started shared transaction.");
          DBScope.transactionTimestamp = DateTime.Now;
        }
        ++DBScope.sharedScopeCount;
        DBScope.logger.Info("Number of shared scopes is {0}", (object) DBScope.sharedScopeCount);
        if (!DBScope.isSharedScope)
        {
          this.isOutermost = true;
          DBScope.isSharedScope = true;
        }
      }
      this.constructorSucceeded = true;
    }
    catch (Exception ex)
    {
      this.constructorSucceeded = false;
      DBScope.logger.Error(ex);
      throw;
    }
  }

  public DbCommand CreateDbCommand()
  {
    DbCommand dbCommand = DBScope.DBProvider.GetDbCommand();
    dbCommand.Connection = this.Transaction.Connection;
    dbCommand.Transaction = this.Transaction;
    return dbCommand;
  }

  private DbParameter CreateDbParameter(string parameterName, object parameterValue)
  {
    return DBScope.DBProvider.GetDbParameter(parameterName, parameterValue);
  }

  public void AddDbParameters(DbCommand command, Dictionary<string, object> paramDict)
  {
    foreach (KeyValuePair<string, object> keyValuePair in paramDict)
      this.AddDbParameter(command, keyValuePair.Key, keyValuePair.Value);
  }

  public void AddDbParameter(DbCommand command, string parameterName, object parameterValue)
  {
    if (command.Parameters.Contains(parameterName))
      return;
    command.Parameters.Add((object) this.CreateDbParameter(parameterName, parameterValue));
  }

  public void Complete()
  {
    this.isComplete = true;
    if (this.transactionLevel != TransactionLevel.Shared)
      return;
    --DBScope.sharedScopeCount;
    DBScope.logger.Info("Scope completed. {0} unfinished scopes remaining.", (object) DBScope.sharedScopeCount);
  }

  protected void FinishTransaction(DbTransaction transaction)
  {
    if (transaction != null)
    {
      if (this.ReadyToCommit)
      {
        DBScope.logger.Info("Committing the transaction.");
        DBScope.DBProvider.CommitTransaction(transaction);
        DBScope.logger.Info("Successfully committed the transaction.");
      }
      else
      {
        DBScope.logger.Info("Rolling back the transaction.");
        DBScope.DBProvider.RollbackTransaction(transaction);
        DBScope.logger.Info("Successfully rolled back the transaction.");
      }
    }
    DbConnection dbConnection = (DbConnection) null;
    if (this.transactionLevel == TransactionLevel.Independent)
    {
      if (this.independentDbTransaction != null)
      {
        dbConnection = this.independentDbTransaction.Connection;
        this.independentDbTransaction.Dispose();
        this.independentDbTransaction = (DbTransaction) null;
      }
    }
    else if (DBScope.sharedDbTransaction != null)
    {
      dbConnection = DBScope.sharedDbTransaction.Connection;
      DBScope.sharedDbTransaction.Dispose();
      DBScope.sharedDbTransaction = (DbTransaction) null;
    }
    if (this.independentDbTransaction != null || DBScope.sharedDbTransaction != null || dbConnection == null)
      return;
    dbConnection.Dispose();
  }

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.transactionLevel == TransactionLevel.Shared)
      {
        if (this.isOutermost)
        {
          try
          {
            this.FinishTransaction(DBScope.sharedDbTransaction);
            return;
          }
          finally
          {
            DBScope.isSharedScope = false;
            this.isOutermost = false;
            DBScope.sharedScopeCount = 0;
          }
        }
      }
      if (this.transactionLevel != TransactionLevel.Independent)
        return;
      this.FinishTransaction(this.independentDbTransaction);
    }
    else
    {
      this.FinishTransaction(DBScope.sharedDbTransaction);
      this.FinishTransaction(this.independentDbTransaction);
      if (!this.constructorSucceeded)
        return;
      DBScope.logger.Info("DBScope was not disposed correctly. Always use it in a using block.");
    }
  }

  ~DBScope() => this.Dispose(false);
}
