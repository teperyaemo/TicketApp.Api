using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Domain.Dtos;
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
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<User?> Get(string username, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserAsync(username, cancellationToken);

        return result;
    }
    
    
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<User?> Get(CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserMeAsync(cancellationToken);

        return result;
    }

    [HttpPost("picture")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не загружен.");
        }
        
        var allowedContentTypes = new[] { "image/jpeg", "image/png" };
        if (!allowedContentTypes.Contains(file.ContentType))
        {
            return BadRequest("Допустимы только изображения в формате JPEG, PNG.");
        }
        
        try
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();

            var dto = new UserUpdateProfilePictureDto()
            {
                ProfilePicture = imageData
            };
            
            var result = await _userService.UpdateUserProfilePicturAsync(dto, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }
}