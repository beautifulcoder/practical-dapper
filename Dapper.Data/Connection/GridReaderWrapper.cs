namespace Dapper.Data.Connection;

public class GridReaderWrapper: IGridReader
{
  private readonly SqlMapper.GridReader _gridReader;

  public GridReaderWrapper(SqlMapper.GridReader gridReader)
  {
    _gridReader = gridReader;
  }

  public Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true) =>
    _gridReader.ReadAsync<T>(buffered);

  public void Dispose() => _gridReader.Dispose();
}