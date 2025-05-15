using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Web.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<User?> Get(string username, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserAsync(username, cancellationToken);

        return result;
    }
}