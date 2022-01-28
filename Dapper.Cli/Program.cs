using Dapper.Data;
using Dapper.Data.Connection;

var conn = new ConnectionProvider();
var repo = new AdventureWorksRepository(conn);

var executeResult = await repo.UpdateSalesOrder(0, 5);
Console.WriteLine("Execute # of rows = " + executeResult);

var queryResult = await repo.GetSalesOrders();
Console.WriteLine("Query # of rows = " + queryResult.Count);

var queryFirstResult = await repo.GetFirstSalesOrder();
Console.WriteLine("QueryFirst SalesOrderId = " +
  queryFirstResult.SalesOrderId);

var queryFirstOrDefaultResult = await repo.GetFirstSalesOrderOrDefault(29825);
Console.WriteLine("QueryFirstOrDefault SalesOrderId = " +
  queryFirstOrDefaultResult?.SalesOrderId);

var querySingleResult = await repo.GetSingleSalesOrder(43659);
Console.WriteLine("QuerySingle SalesOrderId = " +
  querySingleResult.SalesOrderId);

var querySingleOrDefaultResult = await repo.GetSingleSalesOrderOrDefault(0);
Console.WriteLine("QuerySingleOrDefault SalesOrderId = " +
  querySingleOrDefaultResult?.SalesOrderId);

var queryMultipleResult = await repo.GetSalesOrders(29825);
Console.WriteLine("QueryMultiple # of rows = " + queryMultipleResult.Count);

var queryStoredProcedureResult = await repo.GetEmployeeManagers(9);
Console.WriteLine("QueryStoredProcedure # of rows = " + queryStoredProcedureResult.Count);

var transactionResult = await repo.UpdateSalesOrderTransaction(
  new List<int> {0, 0}, 5);
Console.WriteLine("Transaction # of rows = " + transactionResult);

await repo.CreateSalesOrderTvp();
Console.WriteLine("Created dbo.SalesOrderType TVP");

var tvpResult = await repo.UpdateSalesOrdersTvp(queryResult);
Console.WriteLine("ExecuteTvp # of rows = " + tvpResult);
