using System.Diagnostics.CodeAnalysis;

using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RabbitPoc;

using Serilog;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.UseSerilog((context, provider, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(provider));

builder.ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddUserSecrets<MyHostedService>());

builder.ConfigureServices((context, services) =>
{
    services.AddLogging();
    services.AddAmqpCloudEventsPublisher(context.Configuration);
    services.AddHostedService<MyHostedService>();
});

await builder.RunConsoleAsync();

[ExcludeFromCodeCoverage]
internal static partial class Program;