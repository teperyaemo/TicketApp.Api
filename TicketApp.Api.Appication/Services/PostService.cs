using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Appication.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContext _userContext;

    public PostService(IPostRepository postRepository, IUserContext userContext)
    {
        _postRepository = postRepository;
        _userContext = userContext;
    }

    public async Task<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _postRepository.GetPostAsync(id, cancellationToken);
    }

    public async Task<Post?> UpdatePost(PostUpdateDto postDto, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostAsync(postDto.Id, cancellationToken);
        post.Text = postDto.Text;

        await _postRepository.SaveChangesAsync(cancellationToken);

        return post;
    }

    public async Task<List<Post>?> GetPostPageAsync(int page, int take, CancellationToken cancellationToken)
    {
        return await _postRepository.GetPostPageAsync((page - 1) * take, take, cancellationToken);
    }

    public async Task<Guid> CreatePostAsync(PostDto postDto, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = _userContext.UserId,
            Text = postDto.Text,
            CreatedAt = DateTimeOffset.Now,
            EditedAt = null
        };

        await _postRepository.CreatePost(post, cancellationToken);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return post.Id;
    }

    public async Task DeletePost(Guid id, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostAsync(id, cancellationToken);

        if (post != null)
            _postRepository.DeletePost(post);

        await _postRepository.SaveChangesAsync(cancellationToken);
    }
}