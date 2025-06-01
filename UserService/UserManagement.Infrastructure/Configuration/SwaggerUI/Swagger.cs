using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure.Configuration.SwaggerUI;

public static class Swagger
{
    public static IServiceCollection ConfigureSwaggerUI(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }
}
