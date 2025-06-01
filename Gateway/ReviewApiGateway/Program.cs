using GatewayApi.Configurations.Authentification;
using GatewayApi.Configurations.Proxy;
using GatewayApi.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .ConfigureApplicationSettings(builder.Configuration)
    .SetupCookieAuthenticationn()
    .SetupIdentityProviderAuthentication()
    .ConfigureProxy(builder.Configuration)
    .AddHealthChecks();

var app = builder.Build();

app.UseRouting();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/healthCheck");

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.MapControllers();

await app.RunAsync();