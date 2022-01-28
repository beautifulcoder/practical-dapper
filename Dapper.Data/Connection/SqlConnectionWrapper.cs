using System.Data;

namespace Dapper.Data.Connection;

public class SqlConnectionWrapper: ISqlConnection
{
  private readonly IDbConnection _dbConnection;

  public SqlConnectionWrapper(IDbConnection dbConnection)
  {
    _dbConnection = dbConnection;
  }

  public Task<int> ExecuteAsync(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.ExecuteAsync(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public Task<IEnumerable<T>> QueryAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.QueryAsync<T>(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public Task<T> QueryFirstAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.QueryFirstAsync<T>(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public Task<T?> QueryFirstOrDefaultAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.QueryFirstOrDefaultAsync<T?>(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public Task<T> QuerySingleAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.QuerySingleAsync<T>(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public Task<T?> QuerySingleOrDefaultAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    _dbConnection.QuerySingleOrDefaultAsync<T?>(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType);

  public async Task<IGridReader> QueryMultipleAsync(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null) =>
    new GridReaderWrapper(await _dbConnection.QueryMultipleAsync(
      sql,
      param,
      transaction?.GetTransaction(),
      commandTimeout,
      commandType).ConfigureAwait(false));

  public void Open() => _dbConnection.Open();

  public ISqlTransaction BeginTransaction(
    IsolationLevel isolationLevel = IsolationLevel.Unspecified) =>
    new SqlTransactionWrapper(
      _dbConnection.BeginTransaction(isolationLevel));

  public void Dispose() => _dbConnection.Dispose();
}
