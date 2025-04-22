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
    private readonly Lazy<ISession> session = new(connection.CreateSession);

    public async Task PublishAsync(string subject, CloudEvent cloudEvent)
    {
        CloudEventFormatter formatter = new JsonEventFormatter<EntityEventInfo>();
        Message message = cloudEvent.ToAmqpMessage(ContentMode.Binary, formatter);

        logger.PublishInformation(JsonSerializer.Serialize(message));

        ISenderLink link = this.session.Value.CreateSender(
            "a",
            new Target
            {
                Address = $"/exchanges/innago-entity-events/{subject}",
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

    public ValueTask DisposeAsync()
    {
        return new ValueTask(this.session.Value.CloseAsync());
    }
}