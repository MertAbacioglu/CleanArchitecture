﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSetting>(configuration.GetSection("EmailSetting"));

        services.AddTransient<IEMailService, EMailService>();

        return services;
    }
}
//todo : make this registirations like clean arch.