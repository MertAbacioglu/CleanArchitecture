﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class RepositoryDIExtension
{
    public static void AddScopedRepositoriesEndingWith(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<Type> repositoryImplementations = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"));

        foreach (var implementationType in repositoryImplementations)
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
