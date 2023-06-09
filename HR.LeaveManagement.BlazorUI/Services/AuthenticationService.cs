using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services;

public class AuthenticationService : BaseHttpService,IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    public AuthenticationService(IClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authRequest = new() { Email = email, Password = password };
            AuthResponse response = await _client.LoginAsync(authRequest);

            if (response.Token != string.Empty)
            {
                await _localStorageService.SetItemAsync("token", response.Token);
                //Set claims in blazor and login state
                //await _authenticationStateProvider.LoggedIn();//TODO: check this. override is important!!!
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

                return true;
            }
            return false;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task Logout()
    {
        //await _localStorageService.RemoveItemAsync("token"); //todo: duplicated. check this

        //remove claims in blazor and valdiate login state
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();

    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
    {
        RegistrationRequest registrationRequest = new()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            UserName = userName
        };

        RegistrationResponse response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }
        return false;
    }
}
