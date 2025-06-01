namespace GatewayApi.Configurations.Settings.Dtos;

public record LocalIdentityStore
{
    public required string MongoDbConnectionString { get; set; }

    public required string IdentityProviderDatabaseName { get; set; }
}