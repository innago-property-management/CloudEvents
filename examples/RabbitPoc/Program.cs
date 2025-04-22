using System.Diagnostics.CodeAnalysis;

using Amqp;

using Innago.Platform.Messaging.Publisher;
using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
    services.TryAddSingleton<IConnectionFactory, ConnectionFactory>();

    services.TryAddSingleton<IConnection>(provider =>
    {
        Address address = new(
            host: context.Configuration["rabbit:host"],
            port: int.Parse(context.Configuration["rabbit:port"]!),
            user: context.Configuration["rabbit:username"],
            password: context.Configuration["rabbit:password"],
            scheme: "AMQP");

        var factory = ActivatorUtilities.GetServiceOrCreateInstance<IConnectionFactory>(provider);
        var f = new Lazy<Task<IConnection>>(async () => await factory.CreateAsync(address).ConfigureAwait(false));

        return f.Value.Result;
    });

    services.TryAddSingleton<IPublisher, Publisher>();

    services.AddHostedService<MyHostedService>();
});

await builder.RunConsoleAsync();

[ExcludeFromCodeCoverage]
internal static partial class Program;