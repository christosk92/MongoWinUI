using Mediator;
using MongoDb.Domain.Connection;

namespace MongoDb.Application.Connection.Commands;

public sealed class GetAllConnectionsRequest : IRequest<GetAllConnectionsResult>
{

}

public sealed record GetAllConnectionsResult(IReadOnlyCollection<StoredConnectionEntity> Connections);