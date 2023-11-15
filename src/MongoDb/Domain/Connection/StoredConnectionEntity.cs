namespace MongoDb.Domain.Connection;

public sealed record StoredConnectionEntity(MongoConnectionEntity Entity, bool IsSaved, int Order, DateTimeOffset LastUsed, int Frequency);