using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using OauthServer.Domain.Interfaces;

namespace OauthServer.Presentation.Validators;

public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly ILogger<CustomResourceOwnerPasswordValidator> _logger;
    private readonly IUserRepository _userRepository;

    public CustomResourceOwnerPasswordValidator(
        IUserRepository userRepository,
        ILogger<CustomResourceOwnerPasswordValidator> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        _logger.LogInformation("Starting validation for user: {UserName}", context.UserName);

        var user = await _userRepository.GetUserByUserNameAsync(context.UserName);

        if (user == null)
        {
            _logger.LogWarning("User not found: {UserName}", context.UserName);
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "Invalid username or password");
            return;
        }

        _logger.LogInformation("User found: {UserName}", user.UserName);

        if (!BCrypt.Net.BCrypt.Verify(context.Password, user.Password))
        {
            _logger.LogWarning("Invalid password for user: {UserName}", context.UserName);
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "Invalid username or password");
            return;
        }

        _logger.LogInformation("Password verified for user: {UserName}", user.UserName);

        context.Result = new GrantValidationResult(
            user.Id,
            "custom");
    }
}