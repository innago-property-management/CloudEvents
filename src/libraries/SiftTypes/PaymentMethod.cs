namespace Innago.SiftTypes;

using System.Text.Json.Serialization;

using JetBrains.Annotations;

/// <summary>
///     Represents a payment method used for transactions with Sift Science integration.
/// </summary>
[PublicAPI]
public record PaymentMethod(
    [property: JsonPropertyName("$payment_type")] string PaymentType,

    // CC
    [property: JsonPropertyName("$card_bin")] string? CardBin,
    [property: JsonPropertyName("$card_last4")] string? CardLast4,
    [property: JsonPropertyName("$payment_gateway")] string? PaymentGateway,
    [property: JsonPropertyName("$verification_status")] string? VerificationStatus,

    // EFT
    [property: JsonPropertyName("$routing_number")] string? RoutingNumber,
    [property: JsonPropertyName("$bank_name")] string? BankName,
    [property: JsonPropertyName("$bank_country")] string? BankCountry,
    [property: JsonPropertyName("$account_number_last5")] string? AccountNumberLast5
);