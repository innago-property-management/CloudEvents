namespace Innago.Platform.Messaging.EntityEvents;

using JetBrains.Annotations;

/// <summary>
///     Represents information about an event that occurred on a specific entity.
///     This record provides details such as the unique identifier for the event, the action performed (verb),
///     the tenant context, and any associated data relevant to the event.
/// </summary>
/// <typeparam name="T">
///     The type of the entity data associated with the event.
/// </typeparam>
[PublicAPI]
public record EntityEventInfo<T>(string Id, Verb Verb, string? TenantId = null, T? Data = default, string? UserEmailAddress = null) : IEntityEventInfo<T>
{
    private readonly Lazy<Guid> tracingId = new(Guid.NewGuid);

    /// <summary>
    ///     Gets the subject identifier for the event in a formatted string.
    ///     Combines the entity name, the verb (action), event ID, and tenant ID
    ///     to create a unique subject string that can be used for identification
    ///     or messaging purposes.
    /// </summary>
    public string Subject => $"{this.EntityName}:{this.Verb}:{this.Id}:{this.TenantId}";

    /// <summary>
    ///     Gets the fully qualified name of the entity type associated with the event.
    ///     Provides a string representation of the entity type, enabling identification
    ///     of the entity involved in the event.
    /// </summary>
    public string EntityName => $"{typeof(T).FullName}";

    /// <inheritdoc />
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public Guid TracingId => this.tracingId.Value;
}
