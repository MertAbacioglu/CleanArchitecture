using HR.LeaveManagement.BlazorUI.Models;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts.Base;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<Response<RegistrationResponse>> RegisterAsync(RegisterVM registerVM);
    Task Logout();
}
