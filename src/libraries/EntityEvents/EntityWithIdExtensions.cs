namespace Innago.Platform.Messaging.EntityEvents;

using JetBrains.Annotations;

/// <summary>
/// Provides extension methods for converting entities implementing IEntityWithId&lt;TId> into IEntityEventInfo&lt;T>
/// instances. These methods assist in creating events associated with specific actions, such as create, update, delete,
/// and purge, while also enabling the inclusion of optional tenant details.
/// </summary>
[PublicAPI]
public static class EntityWithIdExtensions
{
    /// <summary>
    /// Converts the entity into an IEntityEventInfo instance with the specified action verb.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <param name="entity">The entity to be converted into the event information.</param>
    /// <param name="verb">The action verb that represents the type of event (e.g., Create, Update, Delete).</param>
    /// <param name="tenantId">The tenant identifier associated with the entity, if applicable.</param>
    /// <returns>An instance of IEntityEventInfo encapsulating the given entity, the specified action verb, and tenant details.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the entity's ID is null.</exception>
    public static IEntityEventInfo<IEntityWithId<TId>> ToEntityEventInfo<TId>(
        this IEntityWithId<TId> entity,
        Verb verb,
        string? tenantId = null)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        string id = entity.Id?.ToString() ?? throw new InvalidOperationException("Id is null.");
        return new EntityEventInfo<IEntityWithId<TId>>(id, verb, tenantId, entity);
    }

    /// <summary>
    /// Converts the entity into an IEntityEventInfo instance to represent a Create event.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <param name="entity">The entity to be converted into the event information.</param>
    /// <param name="tenantId">The tenant identifier associated with the entity, if applicable.</param>
    /// <returns>An instance of IEntityEventInfo encapsulating the given entity and Create event details.</returns>
    public static IEntityEventInfo<IEntityWithId<TId>> ToCreateEntityEventInfo<TId>(
        this IEntityWithId<TId> entity,
        string? tenantId = null)
    {
        return entity.ToEntityEventInfo(Verb.Create, tenantId);
    }

    /// <summary>
    /// Converts the entity into an IEntityEventInfo instance to represent an Update event.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <param name="entity">The entity to be converted into the event information.</param>
    /// <param name="tenantId">The tenant identifier associated with the entity, if applicable.</param>
    /// <returns>An instance of IEntityEventInfo encapsulating the given entity and Update event details.</returns>
    public static IEntityEventInfo<IEntityWithId<TId>> ToUpdateEntityEventInfo<TId>(
        this IEntityWithId<TId> entity,
        string? tenantId = null)
    {
        return entity.ToEntityEventInfo(Verb.Update, tenantId);
    }

    /// <summary>
    /// Converts the entity into an IEntityEventInfo instance to represent a Delete event.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <param name="entity">The entity to be converted into the event information.</param>
    /// <param name="tenantId">The tenant identifier associated with the entity, if applicable.</param>
    /// <returns>An instance of IEntityEventInfo encapsulating the given entity and Delete event details.</returns>
    public static IEntityEventInfo<IEntityWithId<TId>> ToDeleteEntityEventInfo<TId>(
        this IEntityWithId<TId> entity,
        string? tenantId = null)
    {
        return entity.ToEntityEventInfo(Verb.Delete, tenantId);
    }

    /// <summary>
    /// Converts the entity into an IEntityEventInfo instance to represent a Purge event.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <param name="entity">The entity to be converted into the event information.</param>
    /// <param name="tenantId">The tenant identifier associated with the entity, if applicable.</param>
    /// <returns>An instance of IEntityEventInfo encapsulating the given entity and Purge event details.</returns>
    public static IEntityEventInfo<IEntityWithId<TId>> ToPurgeEntityEventInfo<TId>(
        this IEntityWithId<TId> entity,
        string? tenantId = null)
    {
        return entity.ToEntityEventInfo(Verb.Purge, tenantId);
    }
}