using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Packages.Mediatr.Contracts.Handlers;
using Packages.Mediatr.Contracts.Services;

namespace Packages.Mediatr.Configurations;

public static class IocConfig
{
    public static IServiceCollection RegisterMediatr(this IServiceCollection services, params Assembly[] assemblies)
    {
        RegisterCommandHandlers(services, assemblies);
        services.AddTransient<IMediatr, Mediatr>();

        return services;
    }

    private static void RegisterCommandHandlers(IServiceCollection services, Assembly[] assemblies)
    {
        var baseTypes = new Type[] { typeof(IRequestHandler<>), typeof(IRequestHandler<,>) };

        var handlerTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new
            {
                Type = t,
                Interface = t.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && baseTypes.Contains(i.GetGenericTypeDefinition())),
            })
            .Where(x => x.Interface != null);

        foreach (var handler in handlerTypes)
        {
            services.AddTransient(handler.Interface!, handler.Type);
        }
    }
}
