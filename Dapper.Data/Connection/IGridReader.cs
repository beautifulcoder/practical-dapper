namespace Dapper.Data.Connection;

public interface IGridReader: IDisposable
{
  /// <summary>
  /// Read the next grid of results.
  /// </summary>
  /// <typeparam name="T">The type to read.</typeparam>
  /// <param name="buffered">Whether the results should be buffered in memory.</param>
  Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true);
}
