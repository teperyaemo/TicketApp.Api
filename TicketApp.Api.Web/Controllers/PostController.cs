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
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Guid> Create([FromForm] PostDto dto, CancellationToken cancellationToken)
    {
        var result = await _postService.CreatePostAsync(dto, cancellationToken);

        return result;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Post?> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _postService.GetPostAsync(id, cancellationToken);

        return result;
    }

    [HttpGet("paged")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<List<Post>?> Get(int page, int take, CancellationToken cancellationToken)
    {
        var result = await _postService.GetPostPageAsync(page, take, cancellationToken);

        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Post?> Update(Guid id, [FromForm] PostDto dto, CancellationToken cancellationToken)
    {
        var result = await _postService.UpdatePost(id, dto, cancellationToken);

        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task Create(Guid id, CancellationToken cancellationToken)
    {
        await _postService.DeletePost(id, cancellationToken);
    }
}