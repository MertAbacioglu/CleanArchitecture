using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Infrastructure.Logging;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HrDatabaseContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
        });

        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        //services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        //services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        //services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        RepositoryDIExtension.AddScopedRepositoriesEndingWith(services, Assembly.GetExecutingAssembly());
        services.AddScopedRepositoriesEndingWith(Assembly.GetExecutingAssembly());


        return services;
    }
}
