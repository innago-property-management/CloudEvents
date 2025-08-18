namespace Innago.Platform.Messaging.EntityEvents;

using JetBrains.Annotations;

/// <summary>
///     Defines the possible actions or operations (verbs) that can be performed on an entity.
/// </summary>
[PublicAPI]
public enum Verb
{
    /// <summary>
    ///     Represents the absence of a specific action or operation on an entity.
    ///     This verb is used when no particular operation has been performed or when the action is undefined.
    /// </summary>
    None = -1,

    /// <summary>
    ///     Represents the action of creating a new entity.
    ///     This verb is used to indicate that a new entity has been added to the system,
    ///     and an associated event has occurred to reflect this operation.
    /// </summary>
    Create,

    /// <summary>
    ///     Represents the action of modifying an existing entity.
    ///     This verb is used to indicate that changes have been made to an entity's state or attributes,
    ///     and an associated event has been triggered to reflect this update.
    /// </summary>
    Update,

    /// <summary>
    ///     Represents the action of deleting an existing entity.
    ///     This verb is used to indicate that an entity has been removed
    ///     from the system, and an associated event reflects the removal operation.
    /// </summary>
    Delete,

    /// <summary>
    ///     Represents a verb indicating the complete removal of an entity and its associated data from the system.
    ///     This action is irreversible and denotes permanent deletion beyond basic deletion operations.
    /// </summary>
    Purge,
}
