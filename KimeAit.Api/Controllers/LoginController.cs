using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KimeAit.Api.Data;
using KimeAit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KimeAit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;

    private readonly IKimeAitDbContext _dbContext;

    private readonly IConfiguration _config;

    public LoginController(ILogger<LoginController> logger, IKimeAitDbContext dbContext, IConfiguration config)
    {
        _logger = logger;
        _dbContext = dbContext;
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel login)
    {
        IActionResult response = Unauthorized();

        if (await IsAuthAsync(login))
        {
            var tokenString = GenerateJsonWebToken();
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    private async Task<bool> IsAuthAsync(LoginRequestModel request)
    {
        return await _dbContext
            .UserAccounts
            .AnyAsync(p => p.UserName == request.Username && p.Password == request.Password);
    }

    private string GenerateJsonWebToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: signCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}