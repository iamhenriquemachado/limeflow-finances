namespace LimeFlow.Infrastructure.Auth;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public const int MinimumSecretKeyLength = 32;

    public string SecretKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpirationMinutes { get; init; } = 60;
}
