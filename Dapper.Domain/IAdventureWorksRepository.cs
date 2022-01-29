using Dapper.Domain.Entities;

namespace Dapper.Domain;

public interface IAdventureWorksRepository
{
  Task<int> UpdateSalesOrder(int salesOrderId, byte status);
  Task<List<SalesOrder>> GetSalesOrders();
  Task<SalesOrder> GetFirstSalesOrder();
  Task<SalesOrder?> GetFirstSalesOrderOrDefault(int customerId);
  Task<SalesOrder> GetSingleSalesOrder(int salesOrderId);
  Task<SalesOrder?> GetSingleSalesOrderOrDefault(int salesOrderId);
  Task<List<SalesOrder>> GetSalesOrders(int customerId);
  Task<List<EmployeeManager>> GetEmployeeManagers(int businessEntityId);
  Task<int> UpdateSalesOrderTransaction(List<int> salesOrderIds, byte status);
  Task CreateSalesOrderTvp();
  Task<int> UpdateSalesOrdersTvp(List<SalesOrderType> salesOrders);
}
