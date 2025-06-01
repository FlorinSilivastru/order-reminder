namespace UserService.Infrastructure.Configuration.Settings.Dtos;

public record IdentityProviderSettings
{
    public required string Uri { get; set; }

    public required string Scope { get; set; }

    public required string Audience { get; set; }
}
