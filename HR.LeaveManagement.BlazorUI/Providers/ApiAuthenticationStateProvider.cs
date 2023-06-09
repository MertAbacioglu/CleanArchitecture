using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.BlazorUI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler tokenHandler;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        tokenHandler = new JwtSecurityTokenHandler();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity());
        bool isTokenPresent = await _localStorageService.ContainKeyAsync("token");
        if (!isTokenPresent)
        {
            return new AuthenticationState(user);
        }

        string savedToken = await _localStorageService.GetItemAsync<string>("token");
        JwtSecurityToken tokenContent = tokenHandler.ReadJwtToken(savedToken);

        if (tokenContent.ValidTo < DateTime.UtcNow)//todo : check this
        {
            await _localStorageService.RemoveItemAsync("token");
            return new AuthenticationState(user);
        }

        List<Claim> claims = await GetClaims();
        user = new (new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }

    public async Task<List<Claim>> GetClaims()
    {
        string savedToken = await _localStorageService.GetItemAsync<string>("token");
        JwtSecurityToken tokenContent = tokenHandler.ReadJwtToken(savedToken);
        List<Claim> claims = tokenContent.Claims.ToList();
        claims.Add(new(ClaimTypes.Name, tokenContent.Subject));//todo : check this
        return claims;
    }

    public async Task LoggedIn()
    {
        List<Claim> claims = await GetClaims();
        ClaimsPrincipal user = new(new ClaimsIdentity(claims, "jwt"));
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorageService.RemoveItemAsync("token");
        ClaimsPrincipal nobody = new(new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }
}