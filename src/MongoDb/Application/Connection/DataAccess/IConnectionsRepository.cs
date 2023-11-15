using System.Collections.Immutable;
using System.Threading.Tasks.Sources;
using LiteDB;
using MongoDb.Application.Connection.Commands;
using MongoDb.Domain.Connection;

namespace MongoDb.Application.Connection.DataAccess;

public interface IConnectionsRepository
{
    IReadOnlyCollection<StoredConnectionEntity> GetAllConnections();
}

internal sealed class ConnectionsRepository : IConnectionsRepository
{
    private readonly ILiteCollection<StoredConnectionEntity> _connections;

    public ConnectionsRepository(ILiteDatabase db)
    {
        _connections = db.GetCollection<StoredConnectionEntity>();
    }

    public IReadOnlyCollection<StoredConnectionEntity> GetAllConnections()
    {
        var connections = _connections.FindAll();
        return connections.ToImmutableArray();
    }
}