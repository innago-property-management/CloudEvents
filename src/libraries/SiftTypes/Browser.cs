namespace Innago.SiftTypes;

using System.Text.Json.Serialization;

/// <summary>
///     Represents browser information for Sift Science fraud detection.
/// </summary>
public record Browser(
    [property: JsonPropertyName("$user_agent")] string UserAgent,
    [property: JsonPropertyName("$accept_language")] string AcceptLanguage,
    [property: JsonPropertyName("$content_language")] string ContentLanguage
);