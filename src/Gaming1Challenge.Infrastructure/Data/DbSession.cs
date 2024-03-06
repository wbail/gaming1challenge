using System.Data;
using Npgsql;

namespace Gaming1Challenge.Infrastructure.Data;

public sealed class DbSession : IDisposable
{
    private Guid _id;

    public IDbConnection Connection { get; } = null!;

    public IDbTransaction Transaction { get; set; } = null!;

    public DbSession(string connectionString)
    {
        _id = Guid.NewGuid();
        Connection = new NpgsqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
