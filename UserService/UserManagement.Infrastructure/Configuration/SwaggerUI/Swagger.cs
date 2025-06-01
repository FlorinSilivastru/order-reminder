namespace UserService.Infrastructure.Configuration.SwaggerUI;

using Microsoft.Extensions.DependencyInjection;

public static class Swagger
{
    public static IServiceCollection ConfigureSwaggerUI(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }
}
