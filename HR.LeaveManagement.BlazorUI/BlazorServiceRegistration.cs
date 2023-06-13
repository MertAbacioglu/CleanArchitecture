using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Handlers;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using HR.LeaveManagement.BlazorUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using Blazored.LocalStorage;
using Blazored.Toast;
using HR.LeaveManagement.BlazorUI.Extensions;

namespace HR.LeaveManagement.BlazorUI
{
    public static class BlazorServiceRegistration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {
            services.AddTransient<JwtAuthorizationMessageHandler>();

            services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7122"))
                .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();
            services.AddAuthorizationCore();

            //services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            //services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
            //services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            //services.AddScoped<IAuthenticationService, AuthenticationService>();

            ServiceDIExtension.AddScopedServicesEndingWith(services, Assembly.GetExecutingAssembly());
            services.AddScopedServicesEndingWith(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
