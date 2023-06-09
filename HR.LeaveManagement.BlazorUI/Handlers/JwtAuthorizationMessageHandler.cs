using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.BlazorUI.Handlers;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string token = await _localStorageService.GetItemAsStringAsync("token");

        if (!string.IsNullOrEmpty(token))
        {

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

        }

        return await base.SendAsync(request, cancellationToken);
    }
}