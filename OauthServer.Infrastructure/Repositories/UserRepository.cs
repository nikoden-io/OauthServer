using MongoDB.Driver;
using OauthServer.Domain.Entities;
using OauthServer.Domain.Interfaces;
using OauthServer.Infrastructure.Data;

namespace OauthServer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<ApplicationUser> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.Users;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
    {
        var filter = Builders<ApplicationUser>.Filter.Eq(u => u.UserName, userName);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateUserAsync(ApplicationUser user)
    {
        await _users.InsertOneAsync(user);
    }
}