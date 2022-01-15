namespace Dapper.Domain.Entities;

public class SalesOrder
{
  public int CustomerId { get; set; }
  public int SalesOrderId { get; set; }
  public int RowNumber { get; set; }
}
