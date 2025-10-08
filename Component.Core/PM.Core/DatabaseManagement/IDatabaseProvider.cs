// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.IDatabaseProvider
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Plugin;
using System.Data;
using System.Data.Common;

#nullable disable
namespace PathMedical.DatabaseManagement;

public interface IDatabaseProvider : IPlugin
{
  string ConnectionString { get; set; }

  DbTransaction GetOpenTransaction();

  void CommitTransaction(DbTransaction transactionToCommit);

  void RollbackTransaction(DbTransaction transactionToRollback);

  void CloseConnection(DbTransaction transactionToClose);

  void CloseConnection(DbConnection connectionToClose);

  void TestConnection();

  DbCommand GetDbCommand();

  DbCommand GetDbCommand(string queryText, DbTransaction transactionToUse);

  DbCommand GetDbCommand(string queryText, DbConnection connectionToUse);

  DbParameter GetDbParameter(string parameterName, DbType parameterType, object parameterValue);

  DbParameter GetDbParameter(string parameterName, object parameterValue);
}
