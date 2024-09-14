using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OauthServer.Domain.Entities;

namespace OauthServer.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(configuration["DatabaseName"]);
    }

    public IMongoCollection<ApplicationUser> Users => _database.GetCollection<ApplicationUser>("Users");
}