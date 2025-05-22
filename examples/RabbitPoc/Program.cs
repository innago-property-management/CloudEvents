using System.Diagnostics.CodeAnalysis;

using Innago.Platform.Messaging.Publisher.Amqp;
using Innago.Shared.HealthChecks.TcpHealthProbe;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Prometheus;

using RabbitPoc;

using Serilog;

using static RabbitPoc.ProgramConfiguration;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, provider, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(provider));

builder.ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddUserSecrets<MyHostedService>());

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

    pusher = new Lazy<MetricPusher>(() => ProgramConfiguration.MetricPusher(serviceName, context.Configuration));
});

IHost app = builder.UseConsoleLifetime().Build();
pusher.Value.Start();
await app.StartAsync();

[ExcludeFromCodeCoverage]
internal static partial class Program;