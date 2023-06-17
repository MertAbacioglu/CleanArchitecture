using System.Reflection;

namespace HR.LeaveManagement.BlazorUI.Extensions
{
    public static class ServiceDIExtension
    {
        public static void AddScopedServicesEndingWith(this IServiceCollection services, Assembly assembly)
        {
            IEnumerable<Type> serviceImplementations = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Service"));

            foreach (Type implementationType in serviceImplementations)
            {
                Type? interfaceType = implementationType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{implementationType.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }
    }
}


