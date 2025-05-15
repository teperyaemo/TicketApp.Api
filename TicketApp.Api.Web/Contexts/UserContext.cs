using System.Security.Claims;
using TicketApp.Api.Domain.Intefaces;

namespace TicketApp.Api.Web.Contexts;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private Guid _userId = Guid.Empty;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            if (_userId != Guid.Empty) return _userId;

            var id = _httpContextAccessor.HttpContext?.User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return string.IsNullOrEmpty(id) ? Guid.Empty : _userId = Guid.Parse(id);
        }
    }
}