namespace MongoDb.Domain.Connection;

public sealed class MongoConnectionEntity
{
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public string Color { get; set; }
    public required Guid Id { get; init; }
}