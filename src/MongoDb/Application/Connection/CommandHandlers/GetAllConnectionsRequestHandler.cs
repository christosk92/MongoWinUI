using Mediator;
using MongoDb.Application.Connection.Commands;
using MongoDb.Application.Connection.DataAccess;

namespace MongoDb.Application.Connection.CommandHandlers;

public sealed class GetAllConnectionsRequestHandler : IRequestHandler<GetAllConnectionsRequest, GetAllConnectionsResult>
{
    private readonly IConnectionsRepository _connectionsRepository;

    public GetAllConnectionsRequestHandler(IConnectionsRepository connectionsRepository)
    {
        _connectionsRepository = connectionsRepository;
    }

    public ValueTask<GetAllConnectionsResult> Handle(GetAllConnectionsRequest request, CancellationToken cancellationToken)
    {
        return new ValueTask<GetAllConnectionsResult>(new GetAllConnectionsResult(_connectionsRepository.GetAllConnections()));
    }
}