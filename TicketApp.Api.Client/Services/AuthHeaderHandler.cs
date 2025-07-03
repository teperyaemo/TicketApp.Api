namespace TicketApp.Api.Client.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenService _tokenService;

    public AuthHeaderHandler(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_tokenService.AccessToken))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
