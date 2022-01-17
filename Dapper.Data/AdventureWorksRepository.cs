using System.Data;
using Dapper.Data.Connection;
using Dapper.Data.Extensions;
using Dapper.Domain;
using Dapper.Domain.Entities;

namespace Dapper.Data;

public class AdventureWorksRepository: IAdventureWorksRepository
{
  private readonly IConnectionProvider _db;

  public AdventureWorksRepository(IConnectionProvider db)
  {
    _db = db;
  }

  public async Task<int> UpdateSalesOrder(int salesOrderId, byte status)
  {
    const string sql = @"
      UPDATE Sales.SalesOrderHeader
      SET STATUS = @status
      WHERE SalesOrderID = @salesOrderId";

    var param = new {status, salesOrderId};
    using var conn = _db.GetAdventureWorksConnection();

    return await conn.ExecuteAsync(sql, param)
      .ConfigureAwait(false);
  }

  public async Task<List<SalesOrder>> GetSalesOrders()
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader";

    using var conn = _db.GetAdventureWorksConnection();

    return (await conn.QueryAsync<SalesOrder>(sql)
        .ConfigureAwait(false))
      .ToList();
  }

  public async Task<SalesOrder> GetFirstSalesOrder()
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      ORDER BY SalesOrderID";

    using var conn = _db.GetAdventureWorksConnection();

    return await conn.QueryFirstAsync<SalesOrder>(sql)
      .ConfigureAwait(false);
  }

  public async Task<SalesOrder?> GetFirstSalesOrderOrDefault(
    int customerId)
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      WHERE CustomerID = @customerId
      ORDER BY SalesOrderID";

    var param = new {customerId};
    using var conn = _db.GetAdventureWorksConnection();

    return await conn.QueryFirstOrDefaultAsync<SalesOrder>(sql, param)
      .ConfigureAwait(false);
  }

  public async Task<SalesOrder> GetSingleSalesOrder(int salesOrderId)
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      WHERE SalesOrderID = @salesOrderId
      ORDER BY SalesOrderID";

    var param = new {salesOrderId};
    using var conn = _db.GetAdventureWorksConnection();

    return await conn.QuerySingleAsync<SalesOrder>(sql, param)
      .ConfigureAwait(false);
  }

  public async Task<SalesOrder?> GetSingleSalesOrderOrDefault(
    int salesOrderId)
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      WHERE SalesOrderID = @salesOrderId
      ORDER BY SalesOrderID";

    var param = new {salesOrderId};
    using var conn = _db.GetAdventureWorksConnection();

    return await conn.QuerySingleOrDefaultAsync<SalesOrder>(sql, param)
      .ConfigureAwait(false);
  }

  public async Task<List<SalesOrder>> GetSalesOrders(int customerId)
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID as [SalesEyeDeed],
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader

      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      ORDER BY SalesOrderID

      SELECT CustomerID, SalesOrderID,
      ROW_NUMBER() OVER(ORDER BY CustomerID) AS RowNumber
      FROM Sales.SalesOrderHeader
      WHERE CustomerID = @customerId
      ORDER BY SalesOrderID";

    var param = new {customerId};
    using var conn = _db.GetAdventureWorksConnection();
    using var query = await conn.QueryMultipleAsync(sql, param)
      .ConfigureAwait(false);

    var result = new List<SalesOrder>();

    result.AddRange((await query.ReadAsync<SalesOrder>()
        .ConfigureAwait(false))
      .ToList());

    result.AddRange((await query.ReadAsync<SalesOrder>()
        .ConfigureAwait(false))
      .ToList());

    result.AddRange((await query.ReadAsync<SalesOrder>()
        .ConfigureAwait(false))
      .ToList());

    return result;
  }

  public async Task<List<EmployeeManager>> GetEmployeeManagers(
    int businessEntityId)
  {
    const string sql = "dbo.uspGetEmployeeManagers";

    var param = new {businessEntityId};
    using var conn = _db.GetAdventureWorksConnection();

    return (await conn.QueryAsync<EmployeeManager>(
          sql,
          param,
          commandType: CommandType.StoredProcedure)
        .ConfigureAwait(false))
      .ToList();
  }

  public async Task<int> UpdateSalesOrderTransaction(
    int salesOrderId,
    byte status)
  {
    const string sql = @"
      UPDATE Sales.SalesOrderHeader
      SET STATUS = @status
      WHERE SalesOrderID = @salesOrderId";

    var param = new {status, salesOrderId};
    using var conn = _db.GetAdventureWorksConnection();
    using var trans = conn.BeginTransaction();

    var result = await conn.ExecuteAsync(sql, param, trans)
      .ConfigureAwait(false);

    result += await conn.ExecuteAsync(sql, param, trans)
      .ConfigureAwait(false);

    trans.Commit();

    return result;
  }

  public async Task CreateSalesOrderTvp()
  {
    const string sql = @"
    DROP TYPE IF EXISTS dbo.SalesOrderType

    CREATE TYPE dbo.SalesOrderType AS TABLE
    (
      CustomerID INT,
      SalesOrderID INT,
      RowNumber INT
    )";

    using var conn = _db.GetAdventureWorksConnection();
    await conn.ExecuteAsync(sql).ConfigureAwait(false);
  }

  public async Task<List<SalesOrder>> GetSalesOrdersTvp(
    List<SalesOrder> salesOrders)
  {
    const string sql = @"
      SELECT CustomerID, SalesOrderID, RowNumber
      FROM @tvp";

    var param = new
    {
      tvp = salesOrders
        .ToDataTable()
        .AsTableValuedParameter("SalesOrderType")
    };

    using var conn = _db.GetAdventureWorksConnection();

    return (await conn.QueryAsync<SalesOrder>(sql, param)
        .ConfigureAwait(false))
      .ToList();
  }
}
