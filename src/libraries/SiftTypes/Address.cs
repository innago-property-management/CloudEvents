namespace Innago.SiftTypes;

using System.Text.Json.Serialization;

/// <summary>
///     Represents a physical address with all relevant components for Sift Science integration.
/// </summary>
public record Address(
    [property: JsonPropertyName("$address_1")] string AddressLine1,
    [property: JsonPropertyName("$address_2")] string? AddressLine2,
    [property: JsonPropertyName("$city")] string City,
    [property: JsonPropertyName("$region")] string State,
    [property: JsonPropertyName("$country")] string Country,
    [property: JsonPropertyName("$zipcode")] string PostalCode,
    [property: JsonPropertyName("$phone")] string Phone
);
