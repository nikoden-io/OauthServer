using OauthServer.Domain.Entities;

namespace OauthServer.Domain.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string id);
    Task<ApplicationUser> GetUserByUserNameAsync(string userName);
    Task CreateUserAsync(ApplicationUser user);
}