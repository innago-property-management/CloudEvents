namespace Innago.SiftTypes;

using System.Text.Json.Serialization;

using JetBrains.Annotations;

/// <summary>
///     Represents a financial transaction with all related information used for Sift Science fraud detection.
/// </summary>
/// <param name="Amount">The transaction amount in decimal format</param>
/// <param name="CurrencyCode">The currency code for the transaction (e.g., USD, EUR)</param>
/// <param name="DeclineCategory">The category of decline if the transaction was declined</param>
/// <param name="InvoiceId">The unique identifier for the invoice associated with this transaction</param>
/// <param name="OrganizationId">The unique identifier for the organization associated with this transaction</param>
/// <param name="SessionId">The unique identifier for the user session</param>
/// <param name="TransactionId">The unique identifier for this transaction</param>
/// <param name="TransactionType">The type of transaction (e.g., sale, refund)</param>
/// <param name="Ip">The IP address from which the transaction was initiated</param>
/// <param name="Browser">The browser information associated with this transaction</param>
/// <param name="BillingAddress">The billing address associated with this transaction</param>
/// <param name="PaymentMethod">The payment method used for this transaction</param>
/// <param name="UserFullName">The full name of the user making the transaction</param>
/// <param name="UserEmailAddress">The email address of the user making the transaction</param>
/// <param name="SellerUserId">The unique identifier of the seller associated with this transaction</param>
/// <param name="Time">The timestamp when the transaction occurred</param>
[UsedImplicitly]
public record Transaction(
    [property: JsonIgnore] decimal Amount,
    string CurrencyCode,
    string? DeclineCategory,
    string InvoiceId,
    string OrganizationId,
    string SessionId,
    string TransactionId,
    string TransactionType,
    string Ip,
    Browser Browser,
    Address BillingAddress,
    PaymentMethod PaymentMethod,
    string UserFullName,
    string UserEmailAddress,
    string SellerUserId,
    DateTimeOffset Time
)
{
    /// <summary>
    ///     Represents the amount of the transaction scaled and converted for Sift integration.
    /// </summary>
    /// <remarks>
    ///     The value is derived by multiplying the original amount by 10,000 and 100 to meet Sift's expected format.
    /// </remarks>
    [JsonPropertyName("amount")]
    public long AmountForSift => (long)(this.Amount * 10_000 * 100);

    /// <summary>
    ///     Represents the timestamp of the transaction in Unix time, expressed in milliseconds.
    /// </summary>
    /// <remarks>
    ///     The value is computed from the transaction's <see cref="Time" /> property using the
    ///     <c>ToUnixTimeMilliseconds</c> method, converting the time to the Unix epoch format.
    /// </remarks>
    public long UnixTime => this.Time.ToUnixTimeMilliseconds();

    /// <summary>
    ///     Represents the identifier of the user associated with the transaction.
    /// </summary>
    /// <remarks>
    ///     This property derives its value from the user's email address.
    /// </remarks>
    public string UserId => this.UserEmailAddress;

    /// <summary>
    ///     Represents the phone number associated with the user initiating the transaction.
    /// </summary>
    /// <remarks>
    ///     This property retrieves the phone number from the billing address linked to the transaction.
    /// </remarks>
    public string UserPhone => this.BillingAddress.Phone;
}
