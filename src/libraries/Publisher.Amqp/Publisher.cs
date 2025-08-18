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
using global::Amqp.Types;

using Microsoft.Extensions.Logging;

using Shared.AsyncLazy;

/// <summary>
///     Represents a publisher for sending CloudEvent messages to an AMQP-based messaging system.
///     Encapsulates the logic for establishing a session, creating sender links, and publishing events.
/// </summary>
public sealed class Publisher(
    IConnectionFactory connectionFactory,
    ILogger<Publisher> logger,
    AmqpConfiguration configuration) : IPublisher
{
    internal (ISenderLink Link, ISession Session) Sender = MakeSenderLink(connectionFactory, configuration);

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await this.Sender.Link.CloseAsync();
        await this.Sender.Session.CloseAsync();
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
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            RespectNullableAnnotations = true,
        };

        CloudEventFormatter formatter = new JsonEventFormatter<IEntityEventInfo<T>>(options, new JsonDocumentOptions());

        Message message = cloudEvent.ToAmqpMessage(ContentMode.Binary, formatter);

        message.Properties = new Properties { Subject = cloudEvent.Subject };

        logger.PublishInformation(JsonSerializer.Serialize(cloudEvent.GetPopulatedAttributes().ToDictionary(pair => pair.Key.Name, pair => pair.Value),
            SourceGeneratorContext.Default.DictionaryStringObject));

        using Activity? activity = PublisherTracer.Source.StartActivity(ActivityKind.Producer);

        try
        {
            activity?.SetTag("cloudEvent.Id", cloudEvent.Id);
            activity?.SetTag("cloudEvent.Subject", cloudEvent.Subject);
            activity?.SetTag("cloudEvent.Source", cloudEvent.Source?.AbsoluteUri);
            activity?.SetTag("cloudEvent.Type", cloudEvent.Type);
            activity?.SetTag("cloudEvent.Time", cloudEvent.Time?.ToString("O"));
            await this.Sender.Link.SendAsync(message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var error = new Error(new Symbol("Error")) { Description = ex.Message };
            logger.Error(ex.GetType().Name, ex.Message);

            if (activity != null)
            {
                activity.SetStatus(ActivityStatusCode.Error, error.Description);
                activity.SetTag("amqp.error.condition", error.Condition);
                activity.SetTag("amqp.error.description", error.Description);
            }
        }
    }

    /// <inheritdoc />
    public Task PublishAsync<T>(IEntityEventInfo<T> entityEventInfo, IJsonTypeInfoResolver typeInfoResolver)
    {
        return this.PublishAsync<T>(entityEventInfo.ToCloudEvent(), typeInfoResolver);
    }

    private static (ISenderLink, ISession) MakeSenderLink(IConnectionFactory connectionFactory, AmqpConfiguration configuration)
    {
        IConnection conn = connectionFactory.CreateAsync(configuration.Address.ToAddress()).GetAwaiter().GetResult();

        ISession session = conn.CreateSession();

        Target target = new()
        {
            Address = configuration.Address.Path,
            Durable = 1,
        };

        ISenderLink link = session.CreateSender(
            configuration.SenderName,
            target);

        return (link, session);
    }
}
