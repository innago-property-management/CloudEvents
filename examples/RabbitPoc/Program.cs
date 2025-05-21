using System.Diagnostics.CodeAnalysis;

using Innago.Platform.Messaging.Publisher.Amqp;
using Innago.Shared.HealthChecks.TcpHealthProbe;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RabbitPoc;

using Serilog;

using static RabbitPoc.ProgramConfiguration;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, provider, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(provider));

builder.ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddUserSecrets<MyHostedService>());

builder.ConfigureServices((context, services) =>
{
    services.AddLogging();
    services.AddHostedService<TcpHealthProbeService>();
    services.AddAmqpCloudEventsPublisher(context.Configuration);
    services.AddHostedService<MyHostedService>();
    services.AddHealthChecks().AddRabbitMQ();
    
    string serviceName = context.Configuration["serviceName"] ?? "RabbitPoc";
    string serviceVersion = context.Configuration["serviceVersion"] ?? "0.0.1";
    
    services.AddOpenTelemetry()
        .ConfigureResource(ConfigureResource(serviceName, serviceVersion))
        .WithTracing(ConfigureTracing(context.Configuration, serviceName))
        .WithMetrics(ConfigureMetrics(serviceName));
});

await builder.RunConsoleAsync();

[ExcludeFromCodeCoverage]
internal static partial class Program;