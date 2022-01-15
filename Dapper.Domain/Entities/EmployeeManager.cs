namespace Dapper.Domain.Entities;

public class EmployeeManager
{
  public int RecursionLevel { get; set; }
  public int BusinessEntityId { get; set; }
  public string OrganizationNode { get; set; } = string.Empty;
}
