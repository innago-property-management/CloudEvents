namespace Innago.SiftTypes;

using JetBrains.Annotations;

/// <summary>
///     Represents a user BankAccount with all related information used for Sift Science fraud detection.
/// </summary>
/// <param name="UserId">The user identifier associated with the bank account.</param>
/// <param name="BankName">The name of the bank.</param>
/// <param name="AccountType">The type of the bank account (e.g., checking, savings).</param>
/// <param name="VerifiedStatus">The verification status of the bank account.</param>
/// <param name="Time">The timestamp of the bank account information.</param>
[PublicAPI]
public record BankAccount(    
    string UserId,
    string BankName,
    string AccountType,
    string VerifiedStatus,    
    DateTimeOffset Time
)
{    
    /// <summary>
    ///     Gets the timestamp as Unix time in milliseconds.
    /// </summary>
    public long UnixTime => this.Time.ToUnixTimeMilliseconds();    
}