namespace Innago.Platform.Messaging.Publisher;

using System.Text.Json.Serialization.Metadata;

using CloudNative.CloudEvents;

using EntityEvents;

using JetBrains.Annotations;

/// <summary>
///     Defines a contract for publishing CloudEvent messages.
///     Implementations of this interface are responsible for sending event data
///     to a messaging system or external target.
/// </summary>
[PublicAPI]
public interface IPublisher : IAsyncDisposable
{
    /// <summary>
    ///     Publishes a CloudEvent message asynchronously.
    /// </summary>
    /// <param name="cloudEvent">The CloudEvent to be published.</param>
    /// <param name="typeInfoResolver">The resolver for types from calling code.</param>
    /// <typeparam name="T">The type of the payload contained in the CloudEvent.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PublishAsync<T>(CloudEvent cloudEvent, IJsonTypeInfoResolver typeInfoResolver);

    /// <summary>
    ///     Publishes an entity event asynchronously.
    /// </summary>
    /// <param name="entityEventInfo">The event information containing details about the entity event.</param>
    /// <param name="typeInfoResolver">The resolver for types from calling code.</param>
    /// <typeparam name="T">The type of the data associated with the entity event.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PublishAsync<T>(IEntityEventInfo<T> entityEventInfo, IJsonTypeInfoResolver typeInfoResolver);
}
