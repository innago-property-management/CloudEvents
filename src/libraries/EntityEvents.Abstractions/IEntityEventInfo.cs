﻿namespace Innago.Platform.Messaging.EntityEvents;

/// <summary>
/// Defines a contract for entity event information, providing details about an event
/// that occurred on an entity, such as its unique identifier, action performed (verb),
/// tenant-specific context, related data, and more.
/// </summary>
/// <typeparam name="T">
/// Represents the type of the data associated with the entity event.
/// </typeparam>
public interface IEntityEventInfo<out T>
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
    /// Gets the identifier representing the tenant associated with the entity event.
    /// This provides context for the specific tenant on which the event occurred.
    /// The value may be null to indicate a tenant-agnostic event.
    /// </summary>
    string? TenantId { get; }

    /// <summary>
    /// Gets the data associated with the entity event.
    /// This represents the specific information tied to the context of the event.
    /// </summary>
    T? Data { get; }

    /// <summary>
    /// Gets the subject identifier for the entity event, representing a unique string
    /// that combines the entity's name, the action performed (verb), the event's unique
    /// identifier, and the tenant-specific context, providing a comprehensive context
    /// for the event.
    /// </summary>
    string Subject { get; }

    /// <summary>
    /// Gets the name of the entity associated with the event.
    /// This property represents the fully qualified type name of the entity.
    /// </summary>
    string EntityName { get; }
}