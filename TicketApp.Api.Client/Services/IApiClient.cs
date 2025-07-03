using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Skreet2k.Common.Models;
using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Client.Services;


[Headers("accept: application/json")]
public interface IApiClient
{
    //Auth
    [Post("/api/Auth/register")]
    Task<Result<LoginResponse>> Register([Body] RegisterRequest request, CancellationToken cancellationToken);
    
    [Post("/api/Auth/login")]
    Task<Result<LoginResponse>> Login([Body] LoginRequest request, CancellationToken cancellationToken);
    
    //Concert
    [Post("/api/Concert")]
    Task<Guid> CreateConcert([Body] ConcertDto dto, CancellationToken cancellationToken);

    [Post("/api/Concert/buyTicket/{id}")]
    Task<Guid> BuyTicket([AliasAs("id")] Guid id, CancellationToken cancellationToken);

    [Get("/api/Concert/id/{id}")]
    Task<Concert?> GetConcertById([AliasAs("id")] Guid id, CancellationToken cancellationToken);

    [Get("/api/Concert/name/{name}")]
    Task<List<Concert>?> GetConcertByName([AliasAs("name")] string name, CancellationToken cancellationToken);

    [Get("/api/Concert/paged")]
    Task<List<Concert>?> GetConcertPaged([Query] int page, [Query] int take, [Query] string? name, CancellationToken cancellationToken);

    [Put("/api/Concert/{id}")]
    Task<Concert?> UpdateConcert([AliasAs("id")] Guid id, [Body] ConcertDto dto, CancellationToken cancellationToken);

    [Delete("/api/Concert/{id}")]
    Task DeleteConcert([AliasAs("id")] Guid id, CancellationToken cancellationToken);
    
    //Favorite
    [Get("/api/Favorite")]
    Task<List<Favorite>> GetFavorites(CancellationToken cancellationToken);

    [Post("/api/Favorite/{concertId}")]
    Task CreateFavorite([AliasAs("concertId")] Guid concertId, CancellationToken cancellationToken);

    [Delete("/api/Favorite/{id}")]
    Task DeleteFavorite([AliasAs("id")] Guid id, CancellationToken cancellationToken);
    
    //Post
    [Post("/api/Post")]
    Task<Guid> CreatePost([Body] PostDto dto, CancellationToken cancellationToken);

    [Get("/api/Post")]
    Task<Post?> GetPost([Query] Guid id, CancellationToken cancellationToken);

    [Get("/api/Post/paged")]
    Task<List<Post>?> GetPostPaged([Query] int page, [Query] int take, CancellationToken cancellationToken);

    [Delete("/api/Post")]
    Task DeletePost([Query] Guid id, CancellationToken cancellationToken);

    [Put("/api/Post/{id}")]
    Task<Post?> UpdatePost([AliasAs("id")] Guid id, [Body] PostDto dto, CancellationToken cancellationToken);
    
    //Ticket
    [Get("/api/Ticket")]
    Task<List<Ticket>?> GetTicketPaged([Query] int page, [Query] int take, CancellationToken cancellationToken);
    
    //User
    [Get("/api/User/{username}")]
    Task<User?> GetUserByUsername([AliasAs("username")] string username, CancellationToken cancellationToken);

    [Get("/api/User/me")]
    Task<User?> GetUserMe(CancellationToken cancellationToken);
    
    [Multipart]
    [Post("/api/User/picture")]
    Task<IActionResult> UpdateUser([AliasAs("file")] StreamPart file, CancellationToken cancellationToken);
}