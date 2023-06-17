using Blazored.LocalStorage;
using Blazored.Toast;
using HR.LeaveManagement.BlazorUI.Extensions;
using HR.LeaveManagement.BlazorUI.Handlers;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

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

            ServiceDIExtension.AddScopedServicesEndingWith(services, Assembly.GetExecutingAssembly());
            services.AddScopedServicesEndingWith(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
