using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skreet2k.Common.Models;
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
    public async Task<Result<LoginResponse>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = new Result<LoginResponse>();
        var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{};':\",./<>?\\|";

        if (!string.IsNullOrWhiteSpace(request.UserName) && 
            !string.IsNullOrWhiteSpace(request.Password) &&
            request.UserName.Length > 5 && 
            request.Password.Length > 5 && 
            request.UserName.All(c => allowedChars.Contains(c) && 
            request.Password.All(x => allowedChars.Contains(x))))
        {
            try
            {
                var token = await _authService.RegisterAsync(request.UserName, request.Password, cancellationToken);
                result.Content = new LoginResponse{ Token = token };
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
        
        result.ErrorMessage = "Некорректные логин или пароль";
        return result;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Result<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = new Result<LoginResponse>();
        
        try
        {
            var token = await _authService.LoginAsync(request.UserName, request.Password, cancellationToken);
            result.Content = new LoginResponse{ Token = token };
            return result;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            result.ReturnCode = 401;
            return result;
        }
    }
}