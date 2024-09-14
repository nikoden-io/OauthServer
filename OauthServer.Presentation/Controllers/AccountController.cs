using Microsoft.AspNetCore.Mvc;
using OauthServer.Domain.Entities;
using OauthServer.Domain.Interfaces;

namespace OauthServer.Presentation.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var existingUser = await _userRepository.GetUserByUserNameAsync(model.UserName);
        if (existingUser != null) return BadRequest("User already exists.");

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Email = model.Email
        };

        await _userRepository.CreateUserAsync(user);

        return Ok("User registered successfully.");
    }
}

public class RegisterModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}