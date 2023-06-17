using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Register
{

    public RegisterVM Model { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public string Message { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        Model = new RegisterVM();
    }

    protected async Task HandleRegister()
    {
        Response<RegistrationResponse> result = await AuthenticationService.RegisterAsync(Model);

        if (result.Success)
        {
            NavigationManager.NavigateTo("/");
        }
        else
        {
            result.ValidationErrors.ForEach(x => Message += $"{x}\n");
        }

        //todo : fluent validation or etc...
    }
}