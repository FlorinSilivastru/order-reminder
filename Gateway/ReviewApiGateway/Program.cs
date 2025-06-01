using GatewayApi.Configurations.Authentification;
using GatewayApi.Configurations.Proxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.SetupCookieAuthenticationn();

builder.Services.SetupIdentityProviderAuthentication();

builder.Services.ConfigureProxy(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.MapControllers();

await app.RunAsync();