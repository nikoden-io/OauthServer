using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using OauthServer.Domain.Interfaces;

namespace OauthServer.Presentation.Services;

public class CustomProfileService : IProfileService
{
    private readonly ILogger<CustomProfileService> _logger;
    private readonly IUserRepository _userRepository;

    public CustomProfileService(IUserRepository userRepository, ILogger<CustomProfileService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.GetSubjectId();

        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new("sub", user.Id),
                    new("name", user.UserName),
                    new("email", user.Email)
                };

                context.IssuedClaims.AddRange(claims);
            }
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var userId = context.Subject.GetSubjectId();
        _logger.LogInformation("IsActiveAsync called for user ID: {UserId}", userId);

        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user != null)
            {
                _logger.LogInformation("User {UserName} is active.", user.UserName);
                context.IsActive = true;
                return;
            }

            _logger.LogWarning("User with ID {UserId} not found.", userId);
        }
        else
        {
            _logger.LogWarning("User ID is null or empty.");
        }

        context.IsActive = false;
    }
}