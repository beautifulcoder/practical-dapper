using System.Data;

namespace Dapper.Data.Connection;

public interface ISqlTransaction: IDisposable
{
  void Commit();
  IDbTransaction GetTransaction();
}
