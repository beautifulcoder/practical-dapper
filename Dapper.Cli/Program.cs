using Dapper.Data;
using Dapper.Data.Connection;

var conn = new ConnectionProvider();
var repo = new AdventureWorksRepository(conn);

var executeResult = await repo
  .UpdateSalesOrder(0, 5)
  .ConfigureAwait(false);
Console.WriteLine("Execute # of rows = " + executeResult);

var queryResult = await repo
  .GetSalesOrders()
  .ConfigureAwait(false);
Console.WriteLine("Query # of rows = " + queryResult.Count);

var queryFirstResult = await repo
  .GetFirstSalesOrder()
  .ConfigureAwait(false);
Console.WriteLine("QueryFirst SalesOrderId = " +
  queryFirstResult.SalesOrderId);

var queryFirstOrDefaultResult = await repo
  .GetFirstSalesOrderOrDefault(29825)
  .ConfigureAwait(false);
Console.WriteLine("QueryFirstOrDefault SalesOrderId = " +
  queryFirstOrDefaultResult?.SalesOrderId);

var querySingleResult = await repo
  .GetSingleSalesOrder(43659)
  .ConfigureAwait(false);
Console.WriteLine("QuerySingle SalesOrderId = " +
  querySingleResult.SalesOrderId);

var querySingleOrDefaultResult = await repo
  .GetSingleSalesOrderOrDefault(0)
  .ConfigureAwait(false);
Console.WriteLine("QuerySingleOrDefault SalesOrderId = " +
  querySingleOrDefaultResult?.SalesOrderId);

var queryMultipleResult = await repo
  .GetSalesOrders(29825)
  .ConfigureAwait(false);
Console.WriteLine("QueryMultiple # of rows = " + queryMultipleResult.Count);

var queryStoredProcedureResult = await repo
  .GetEmployeeManagers(9)
  .ConfigureAwait(false);
Console.WriteLine("QueryStoredProcedure # of rows = " + queryStoredProcedureResult.Count);

await repo.CreateSalesOrderTvp();
Console.WriteLine("Created dbo.SalesOrderType TVP");

var queryTvpResult = await repo
  .GetSalesOrdersTvp(queryResult)
  .ConfigureAwait(false);
Console.WriteLine("QueryTvp # of rows = " + queryTvpResult.Count);
