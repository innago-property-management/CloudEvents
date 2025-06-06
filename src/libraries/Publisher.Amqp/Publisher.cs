namespace Innago.Platform.Messaging.Publisher.Amqp;

using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Amqp;
using CloudNative.CloudEvents.SystemTextJson;

using EntityEvents;

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

    internal string AddressPrefix => addressPrefix;
    internal string SenderName => senderName;

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        return new ValueTask(this.session.Value.CloseAsync());
    }

    /// <inheritdoc />
    public async Task PublishAsync<T>(CloudEvent cloudEvent, IJsonTypeInfoResolver typeInfoResolver)
    {
        IJsonTypeInfoResolver combinedResolver = JsonTypeInfoResolver.Combine(
            SourceGeneratorContext.Default,
            typeInfoResolver);

        JsonSerializerOptions options = new()
        {
            TypeInfoResolver = combinedResolver,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };

        CloudEventFormatter formatter = new JsonEventFormatter<IEntityEventInfo<T>>(options, new JsonDocumentOptions());

        Message message = cloudEvent.ToAmqpMessage(ContentMode.Binary, formatter);

        logger.PublishInformation(JsonSerializer.Serialize(cloudEvent.GetPopulatedAttributes().ToDictionary(pair => pair.Key.Name, pair => pair.Value),
            SourceGeneratorContext.Default.DictionaryStringObject));

        ISenderLink link = this.session.Value.CreateSender(
            this.SenderName,
            new Target
            {
                Address = $"{this.AddressPrefix}/{cloudEvent.Subject}",
                Durable = 1,
            },
            OnAttached);

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

        return;

        void OnAttached(ILink lnk, Attach attach)
        {
            Activity? activity = PublisherTracer.Source.StartActivity(ActivityKind.Producer);
            activity?.SetTag("cloudEvent.Id", cloudEvent.Id);
            activity?.SetTag("cloudEvent.Subject", cloudEvent.Subject);
            activity?.SetTag("cloudEvent.Source", cloudEvent.Source?.AbsoluteUri);
            activity?.SetTag("cloudEvent.Type", cloudEvent.Type);
            activity?.SetTag("cloudEvent.Time", cloudEvent.Time?.ToString("O"));

            lnk.AddClosedCallback(OnClosed);

            void OnClosed(IAmqpObject sender, Error error)
            {
                if (activity != null && error != null!)
                {
                    activity.SetStatus(ActivityStatusCode.Error, error.Description);
                    activity.SetTag("amqp.error.condition", error.Condition);
                    activity.SetTag("amqp.error.description", error.Description);
                }
                
                activity?.Dispose();
            }
        }
    }

    /// <inheritdoc />
    public Task PublishAsync<T>(IEntityEventInfo<T> entityEventInfo, IJsonTypeInfoResolver typeInfoResolver)
    {
        return this.PublishAsync<T>(entityEventInfo.ToCloudEvent(), typeInfoResolver);
    }
}