using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;
using Innago.Platform.Messaging.Publisher.Amqp;
using Innago.Shared.HealthChecks.TcpHealthProbe;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using OpenTelemetry.Trace;

using Prometheus;

using RabbitPoc;

using Serilog;

using static RabbitPoc.ProgramConfiguration;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, provider, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(provider));

builder.ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddUserSecrets<MyHostedService>());

Lazy<MetricPusher> pusher = null!;

builder.ConfigureServices((context, services) =>
{
    services.AddLogging();
    services.AddHostedService<TcpHealthProbeService>();
    services.AddAmqpCloudEventsPublisher(context.Configuration);
    services.AddHostedService<MyHostedService>();
    services.AddMetrics();

    string serviceName = context.Configuration["serviceName"] ?? "RabbitPoc";
    string serviceVersion = context.Configuration["serviceVersion"] ?? "0.0.1";

    services.AddHealthChecks().AddAsyncCheck("amqp-connection",
        AmqpCheck(context, serviceName)).ForwardToPrometheus();

    services.AddOpenTelemetry()
        .ConfigureResource(ConfigureResource(serviceName, serviceVersion))
        .WithTracing(ConfigureTracing(context.Configuration, serviceName))
        .WithMetrics(ConfigureMetrics(context.Configuration, serviceName));

    services.TryAddTransient(_ => TracerProvider.Default.GetTracer(serviceName));

    pusher = new Lazy<MetricPusher>(() => MetricPusher(serviceName, context.Configuration));

    services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.TypeInfoResolverChain.Insert(0, SourceGenerationContext.Default);

        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Always;

        options.SerializerOptions.WriteIndented = true;

        options.SerializerOptions.TypeInfoResolver = SourceGenerationContext.Default;
    });
});

IHost app = builder.UseConsoleLifetime().Build();
pusher.Value.Start();
await app.StartAsync();

var publisher = ActivatorUtilities.GetServiceOrCreateInstance<IPublisher>(app.Services);

var paymentFailed = new CreditCardPaymentFailed("my-id", "my error");

IEntityEventInfo<IEntityWithId<string>> eventInfo = paymentFailed.ToCreateEntityEventInfo();

MyMetrics.PaymentFailed.Inc();

await publisher.PublishAsync(eventInfo, SourceGenerationContext.Default).ConfigureAwait(false);

[ExcludeFromCodeCoverage]
internal static partial class Program;

internal record CreditCardPaymentFailed(string Id, string ErrorMessage) : IEntityWithId<string>;

internal static class MyMetrics
{
    internal static readonly Counter PaymentFailed = Metrics.CreateCounter(nameof(MyMetrics.PaymentFailed), "payment failed");
}
