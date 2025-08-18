namespace Innago.SiftTypes;

using JetBrains.Annotations;

/// <summary>
///     Represents a user account with all related information used for Sift Science fraud detection.
/// </summary>
[PublicAPI]
public record Account(
    string UserEmailAddress,
    string SessionId,
    string UserFullName,
    string Ip,
    Browser? Browser,
    Address BillingAddress,
    PaymentMethod[]? PaymentMethods,
    DateTimeOffset Time
)
{
    /// <summary>
    ///     Gets the phone number from the billing address associated with this account.
    /// </summary>
    public string Phone => this.BillingAddress.Phone;

    /// <summary>
    ///     Gets the timestamp as Unix time in milliseconds.
    /// </summary>
    public long UnixTime => this.Time.ToUnixTimeMilliseconds();

    /// <summary>
    ///     Gets the user identifier, which is the same as the user's email address.
    /// </summary>
    public string UserId => this.UserEmailAddress;
}