namespace Innvoicer.Application.Settings;

public class AuthSettings
{
    public int TokenExpirationMinutes { get; set; }
    public required string ValidIssuer { get; set; }
    public required string ValidAudience { get; set; }
    public required string SecretKey { get; set; }
    public int RefreshTokenExpirationMinutes { get; set; }
}