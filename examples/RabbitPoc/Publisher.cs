namespace RabbitPoc;

using System.Text.Json;

using Amqp;
using Amqp.Framing;

using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Amqp;
using CloudNative.CloudEvents.SystemTextJson;

using Microsoft.Extensions.Logging;

internal sealed class Publisher(IConnection connection, ILogger<Publisher> logger) : IAsyncDisposable
{
    private const string AddressPrefix = "/exchanges/innago-entity-events";
    private const string SenderName = "entity-event-publish";
    private readonly Lazy<ISession> session = new(connection.CreateSession);

    public ValueTask DisposeAsync()
    {
        return new ValueTask(this.session.Value.CloseAsync());
    }

    public async Task PublishAsync<T>(CloudEvent cloudEvent)
    {
        CloudEventFormatter formatter = new JsonEventFormatter<T>();

        Message message = cloudEvent.ToAmqpMessage(ContentMode.Binary, formatter);

        logger.PublishInformation(JsonSerializer.Serialize( cloudEvent.GetPopulatedAttributes().ToDictionary(pair => pair.Key.Name, pair => pair.Value)));

        ISenderLink link = this.session.Value.CreateSender(
            Publisher.SenderName,
            new Target
            {
                Address = $"{Publisher.AddressPrefix}/{cloudEvent.Subject}",
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