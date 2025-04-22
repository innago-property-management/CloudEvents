namespace Innago.Platform.Messaging.EntityEvents;

/// <summary>
/// Defines a contract for entity event information, providing details about an event
/// that occurred on an entity, such as its unique identifier, action performed (verb),
/// tenant-specific context, related data, and more.
/// </summary>
/// <typeparam name="T">
/// Represents the type of the data associated with the entity event.
/// </typeparam>
public interface IEntityEventsInfo<out T>
{
    /// <summary>
    /// Gets the unique identifier associated with the entity event.
    /// This identifier represents the specific instance of the event.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the action or operation (verb) performed on the entity within the context of the event.
    /// This value represents whether the entity was created, updated, or deleted.
    /// </summary>
    Verb Verb { get; }

    /// <summary>
    /// 
    /// </summary>
    string? TenantId { get; }

    /// <summary>
    /// 
    /// </summary>
    T? Data { get; }

    /// <summary>
    /// 
    /// </summary>
    string Subject { get; }

    /// <summary>
    /// 
    /// </summary>
    string EntityName { get; }
}