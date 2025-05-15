using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Web.Controllers;

[ApiController]
[AllowAnonymous]
[Produces("application/json")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _authService.RegisterAsync(request.UserName, request.Password, cancellationToken);
            return Ok(new LoginResponse { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _authService.LoginAsync(request.UserName, request.Password, cancellationToken);
            return Ok(new LoginResponse { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
}