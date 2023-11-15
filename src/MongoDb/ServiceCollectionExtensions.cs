using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDb.Application.Connection.DataAccess;

namespace MongoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, string path)
    {
        services.AddScoped<IConnectionsRepository, ConnectionsRepository>();
        Directory.CreateDirectory(path);
        var db = new LiteDatabase(Path.Combine(path, "data.db"));
        services.AddSingleton< ILiteDatabase>(db);

        return services;
    }
}