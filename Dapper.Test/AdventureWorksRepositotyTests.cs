using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Data;
using Dapper.Data.Connection;
using Dapper.Domain;
using Dapper.Domain.Entities;
using Moq;
using Xunit;

namespace Dapper.Test;

public class AdventureWorksRepositoryTests
{
  private readonly Mock<ISqlConnection> _db;
  private readonly Mock<IGridReader> _reader;
  private readonly IAdventureWorksRepository _repo;

  public AdventureWorksRepositoryTests()
  {
    var trans = new Mock<ISqlTransaction>();
    _db = new Mock<ISqlConnection>();
    _db
      .Setup(m => m.BeginTransaction(IsolationLevel.Unspecified))
      .Returns(trans.Object);
    _reader = new Mock<IGridReader>();

    var connectionProvider = new Mock<IConnectionProvider>();
    connectionProvider
      .Setup(m => m.GetAdventureWorksConnection())
      .Returns(_db.Object);

    _repo = new AdventureWorksRepository(connectionProvider.Object);
  }

  [Fact]
  public async Task UpdateSalesOrder()
  {
    // arrange
    _db
      .Setup(m => m.ExecuteAsync(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(1);

    // act
    var result = await _repo.UpdateSalesOrder(1, 5);

    // assert
    Assert.Equal(1, result);
  }

  [Fact]
  public async Task GetSalesOrders()
  {
    // arrange
    _db
      .Setup(m => m.QueryAsync<SalesOrder>(
        It.Is<string>(s => s != null),
        null,
        null,
        null,
        null))
      .ReturnsAsync(new List<SalesOrder>());

    // act
    var result = await _repo.GetSalesOrders();

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetFirstSalesOrder()
  {
    // arrange
    _db
      .Setup(m => m.QueryFirstAsync<SalesOrder>(
        It.Is<string>(s => s != null),
        null,
        null,
        null,
        null))
      .ReturnsAsync(new SalesOrder());

    // act
    var result = await _repo.GetFirstSalesOrder();

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetFirstSalesOrderOrDefault()
  {
    // arrange
    _db
      .Setup(m => m.QueryFirstOrDefaultAsync<SalesOrder>(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(new SalesOrder());

    // act
    var result = await _repo.GetFirstSalesOrderOrDefault(1);

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetSingleSalesOrder()
  {
    // arrange
    _db
      .Setup(m => m.QuerySingleAsync<SalesOrder>(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(new SalesOrder());

    // act
    var result = await _repo.GetSingleSalesOrder(1);

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetSingleSalesOrderOrDefault()
  {
    // arrange
    _db
      .Setup(m => m.QuerySingleOrDefaultAsync<SalesOrder>(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(new SalesOrder());

    // act
    var result = await _repo.GetSingleSalesOrderOrDefault(1);

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetSalesOrdersMultiple()
  {
    // arrange
    _reader
      .Setup(m => m.ReadAsync<SalesOrder>(true))
      .ReturnsAsync(new List<SalesOrder>());

    _db
      .Setup(m => m.QueryMultipleAsync(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(_reader.Object);

    // act
    var result = await _repo.GetSalesOrders(1);

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetEmployeeManagers()
  {
    // arrange
    _db
      .Setup(m => m.QueryAsync<EmployeeManager>(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        CommandType.StoredProcedure))
      .ReturnsAsync(new List<EmployeeManager>());

    // act
    var result = await _repo.GetEmployeeManagers(1);

    // assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task UpdateSalesOrderTransaction()
  {
    // arrange
    _db
      .Setup(m => m.ExecuteAsync(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        It.Is<ISqlTransaction>(t => t != null),
        null,
        null))
      .ReturnsAsync(1);

    // act
    var result = await _repo.UpdateSalesOrderTransaction(
      new List<int> {1, 1}, 5);

    // assert
    Assert.Equal(2, result);
  }

  [Fact]
  public async Task CreateSalesOrderTvp()
  {
    // arrange/act
    await _repo.CreateSalesOrderTvp();

    // assert
    _db
      .Verify(m => m.ExecuteAsync(
        It.Is<string>(s => s != null),
        null,
        null,
        null,
        null));
  }

  [Fact]
  public async Task UpdateSalesOrdersTvp()
  {
    // arrange
    _db
      .Setup(m => m.ExecuteAsync(
        It.Is<string>(s => s != null),
        It.Is<object>(o => o != null),
        null,
        null,
        null))
      .ReturnsAsync(1);

    // act
    var result = await _repo.UpdateSalesOrdersTvp(new List<SalesOrder>
    {
      new() {CustomerId = 1, RowNumber = 1, SalesOrderId = 1}
    });

    // assert
    Assert.Equal(1, result);
  }
}
