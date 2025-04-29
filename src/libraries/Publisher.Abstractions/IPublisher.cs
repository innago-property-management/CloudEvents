namespace Innago.Platform.Messaging.Publisher;

using CloudNative.CloudEvents;

using EntityEvents;

/// <summary>
/// Defines a contract for publishing CloudEvent messages.
/// Implementations of this interface are responsible for sending event data
/// to a messaging system or external target.
/// </summary>
public interface IPublisher : IAsyncDisposable
{
    /// <summary>
    /// Publishes a CloudEvent message asynchronously.
    /// </summary>
    /// <param name="cloudEvent">The CloudEvent to be published.</param>
    /// <typeparam name="T">The type of the payload contained in the CloudEvent.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PublishAsync<T>(CloudEvent cloudEvent);

    /// <summary>
    /// Publishes an entity event asynchronously.
    /// </summary>
    /// <param name="entityEventInfo">The event information containing details about the entity event.</param>
    /// <typeparam name="T">The type of the data associated with the entity event.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PublishAsync<T>(IEntityEventInfo<T> entityEventInfo);
}