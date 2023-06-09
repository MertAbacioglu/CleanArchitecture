using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    public LoginVM Model { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string Message { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    public Login()
    {
        //why we need this? why we created an instance of LoginVM in OnInitialized method?
    }

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    protected async Task HandleLogin()
    {
        Console.WriteLine("HandleLogin method called");

        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Username/password combination unknown";
        Console.WriteLine("Authentication failed");
    }
}