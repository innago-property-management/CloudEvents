namespace Innago.Platform.Messaging.EntityEvents;

/// <summary>
/// Defines an entity that is identified by a unique identifier of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public interface IEntityWithId<TId>
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    TId Id { get; set; }
}