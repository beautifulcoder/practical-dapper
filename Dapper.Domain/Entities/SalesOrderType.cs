namespace Dapper.Domain.Entities;

public record SalesOrderType(
  byte Status,
  int SalesOrderId)
{
  public SalesOrderType() : this(0, 0) {}
};