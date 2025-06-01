namespace GatewayApi.Configurations.Settings.Dtos;

public record IdentityProviderSettings
{
    public required string SymmetricEncryptionKey { get; set; }

    public required string Uri { get; set; }

    public required string ClientId { get; set; }

    public required string ClientSecret { get; set; }

    public required string Scope { get; set; }
}
