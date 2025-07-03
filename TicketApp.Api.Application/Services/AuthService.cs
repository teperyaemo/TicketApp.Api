using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TicketApp.Api.Application.CommonServices;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Enums;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserAsync(username, cancellationToken);
        if (existingUser != null) throw new Exception("Username already exists");

        var passwordHash = PasswordHasher.HashPassword(password);
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            PasswordHash = passwordHash,
            CreatedAt = DateTimeOffset.Now,
            Role = nameof(UserRoles.User),
            Tickets = new List<Ticket>()
        };

        await _userRepository.CreateUserAsync(newUser, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return await GenerateJwtTokenAsync(newUser);
    }

    public async Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(username, cancellationToken);
        if (user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, password))
            throw new Exception("Invalid username or password");

        return await GenerateJwtTokenAsync(user);
    }

    private async Task<string> GenerateJwtTokenAsync(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(jwtSettings["TokenLifetime"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}