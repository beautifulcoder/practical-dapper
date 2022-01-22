namespace Dapper.Domain.Entities;

public record EmployeeManager(
  int RecursionLevel,
  int BusinessEntityId,
  string OrganizationNode)
{
  public EmployeeManager() : this(0, 0, string.Empty) {}
};
