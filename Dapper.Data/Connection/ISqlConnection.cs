using System.Data;

namespace Dapper.Data.Connection;

public interface ISqlConnection: IDisposable
{
  /// <summary>
  /// Execute a command asynchronously using Task.
  /// </summary>
  /// <param name="sql">The SQL to execute for this query.</param>
  /// <param name="param">The parameters to use for this query.</param>
  /// <param name="transaction">The transaction to use for this query.</param>
  /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
  /// <param name="commandType">Is it a stored proc or a batch?</param>
  /// <returns>The number of rows affected.</returns>
  Task<int> ExecuteAsync(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// /// <summary>
  /// Execute a query asynchronously using Task.
  /// </summary>
  /// <typeparam name="T">The type of results to return.</typeparam>
  /// <param name="sql">The SQL to execute for the query.</param>
  /// <param name="param">The parameters to pass, if any.</param>
  /// <param name="transaction">The transaction to use, if any.</param>
  /// <param name="commandTimeout">The command timeout (in seconds).</param>
  /// <param name="commandType">The type of command to execute.</param>
  /// <returns>
  /// A sequence of data of <typeparamref name="T"/>; if a basic type (int,
  /// string, etc) is queried then the data from the first column in assumed,
  /// otherwise an instance is created per row, and a direct
  /// column-name===member-name mapping is assumed (case insensitive).
  /// </returns>
  Task<IEnumerable<T>> QueryAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// <summary>
  /// Execute a single-row query asynchronously using Task.
  /// </summary>
  /// <typeparam name="T">The type of result to return.</typeparam>
  /// <param name="sql">The SQL to execute for the query.</param>
  /// <param name="param">The parameters to pass, if any.</param>
  /// <param name="transaction">The transaction to use, if any.</param>
  /// <param name="commandTimeout">The command timeout (in seconds).</param>
  /// <param name="commandType">The type of command to execute.</param>
  Task<T> QueryFirstAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// <summary>
  /// Execute a single-row query asynchronously using Task.
  /// </summary>
  /// <typeparam name="T">The type of result to return.</typeparam>
  /// <param name="sql">The SQL to execute for the query.</param>
  /// <param name="param">The parameters to pass, if any.</param>
  /// <param name="transaction">The transaction to use, if any.</param>
  /// <param name="commandTimeout">The command timeout (in seconds).</param>
  /// <param name="commandType">The type of command to execute.</param>
  Task<T?> QueryFirstOrDefaultAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// <summary>
  /// Execute a single-row query asynchronously using Task.
  /// </summary>
  /// <typeparam name="T">The type of result to return.</typeparam>
  /// <param name="sql">The SQL to execute for the query.</param>
  /// <param name="param">The parameters to pass, if any.</param>
  /// <param name="transaction">The transaction to use, if any.</param>
  /// <param name="commandTimeout">The command timeout (in seconds).</param>
  /// <param name="commandType">The type of command to execute.</param>
  Task<T> QuerySingleAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// <summary>
  /// Execute a single-row query asynchronously using Task.
  /// </summary>
  /// <typeparam name="T">The type to return.</typeparam>
  /// <param name="sql">The SQL to execute for the query.</param>
  /// <param name="param">The parameters to pass, if any.</param>
  /// <param name="transaction">The transaction to use, if any.</param>
  /// <param name="commandTimeout">The command timeout (in seconds).</param>
  /// <param name="commandType">The type of command to execute.</param>
  Task<T?> QuerySingleOrDefaultAsync<T>(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  /// <summary>
  /// Execute a command that returns multiple result sets, and access each in turn.
  /// </summary>
  /// <param name="sql">The SQL to execute for this query.</param>
  /// <param name="param">The parameters to use for this query.</param>
  /// <param name="transaction">The transaction to use for this query.</param>
  /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
  /// <param name="commandType">Is it a stored proc or a batch?</param>
  Task<IGridReader> QueryMultipleAsync(
    string sql,
    object? param = null,
    ISqlTransaction? transaction = null,
    int? commandTimeout = null,
    CommandType? commandType = null);

  ISqlTransaction BeginTransaction(
    IsolationLevel isolationLevel = IsolationLevel.Unspecified);
}
