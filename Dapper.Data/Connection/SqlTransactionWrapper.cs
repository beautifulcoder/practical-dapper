using System.Data;

namespace Dapper.Data.Connection;

public class SqlTransactionWrapper: ISqlTransaction
{
  private readonly IDbTransaction _dbTransaction;

  public SqlTransactionWrapper(IDbTransaction dbTransaction)
  {
    _dbTransaction = dbTransaction;
  }

  public void Commit() => _dbTransaction.Commit();

  public IDbTransaction GetTransaction() => _dbTransaction;

  public void Dispose() => _dbTransaction.Dispose();
}
