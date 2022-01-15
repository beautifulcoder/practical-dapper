namespace Dapper.Data.Connection;

public interface IConnectionProvider
{
  ISqlConnection GetAdventureWorksConnection();
}
