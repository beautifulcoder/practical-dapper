using Microsoft.Data.SqlClient;

namespace Dapper.Data.Connection;

public class ConnectionProvider: IConnectionProvider
{
  public ISqlConnection GetAdventureWorksConnection() =>
    new SqlConnectionWrapper(
      new SqlConnection(
        new SqlConnectionStringBuilder
        {
          ApplicationName = "Practical Dapper",
          DataSource = "(localdb)\\MSSQLLocalDB",
          InitialCatalog = "AdventureWorks",
          Encrypt = false,
          TrustServerCertificate = true,
          MaxPoolSize = 100,
          ConnectTimeout = 15
        }
        .ToString()));
}
