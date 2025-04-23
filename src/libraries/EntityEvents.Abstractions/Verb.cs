namespace Innago.Platform.Messaging.EntityEvents;

/// <summary>
///     Defines the possible actions or operations (verbs) that can be performed on an entity.
/// </summary>
public enum Verb
{
    /// <summary>
    /// Represents the action of creating a new entity.
    /// This verb is used to indicate that a new entity has been added to the system,
    /// and an associated event has occurred to reflect this operation.
    /// </summary>
    Create,

    /// <summary>
    /// Represents the action of modifying an existing entity.
    /// This verb is used to indicate that changes have been made to an entity's state or attributes,
    /// and an associated event has been triggered to reflect this update.
    /// </summary>
    Update,

    /// <summary>
    /// Represents the action of deleting an existing entity.
    /// This verb is used to indicate that an entity has been removed
    /// from the system, and an associated event reflects the removal operation.
    /// </summary>
    Delete,
}