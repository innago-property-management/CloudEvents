namespace Innago.Platform.Messaging.Publisher.Amqp;

using System.Text.Json;

using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Amqp;
using CloudNative.CloudEvents.SystemTextJson;

using global::Amqp;
using global::Amqp.Framing;

using Innago.Platform.Messaging.Publisher;

using Microsoft.Extensions.Logging;

/// <summary>
/// Represents a publisher for sending CloudEvent messages to an AMQP-based messaging system.
/// Encapsulates the logic for establishing a session, creating sender links, and publishing events.
/// </summary>
public sealed class Publisher(
    IConnection connection,
    ILogger<Publisher> logger,
    string addressPrefix,
    string senderName) : IPublisher
{
    private readonly Lazy<ISession> session = new(connection.CreateSession);

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        return new ValueTask(this.session.Value.CloseAsync());
    }

    /// <inheritdoc />
    public async Task PublishAsync<T>(CloudEvent cloudEvent)
    {
        CloudEventFormatter formatter = new JsonEventFormatter<T>();

        Message message = cloudEvent.ToAmqpMessage(ContentMode.Binary, formatter);

        logger.PublishInformation(JsonSerializer.Serialize(cloudEvent.GetPopulatedAttributes().ToDictionary(pair => pair.Key.Name, pair => pair.Value), SourceGeneratorContext.Default.DictionaryStringObject));

        ISenderLink link = this.session.Value.CreateSender(
            senderName,
            new Target
            {
                Address = $"{addressPrefix}/{cloudEvent.Subject}",
                Durable = 1,
            });

        try
        {
            await link.SendAsync(message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.Error(ex.GetType().Name, ex.Message);
        }
        finally
        {
            await link.CloseAsync().ConfigureAwait(false);
        }
    }
}