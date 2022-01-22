namespace Dapper.Domain.Entities;

public record SalesOrder(
  int CustomerId,
  int SalesOrderId,
  int RowNumber)
{
  public SalesOrder() : this(0, 0, 0) {}
};
