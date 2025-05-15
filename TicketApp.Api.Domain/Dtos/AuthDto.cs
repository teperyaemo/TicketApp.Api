using System.ComponentModel.DataAnnotations;

namespace TicketApp.Api.Domain.Dtos;

public class RegisterRequest
{
    [Required] public string UserName { get; set; }

    [Required] [MinLength(6)] public string Password { get; set; }
}

public class LoginRequest
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}