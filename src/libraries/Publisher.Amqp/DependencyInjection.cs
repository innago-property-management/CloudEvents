namespace Innago.Platform.Messaging.Publisher.Amqp;

using global::Amqp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides extension methods for setting up dependencies related to AMQP-based CloudEvents messaging.
/// This static class enables configuration and registration of AMQP publishers in the service collection.
/// </summary>
public static class DependencyInjection
{
    private const string DefaultSectionName = "publisherAmqp";

    /// <summary>
    /// Adds configuration and setup for an AMQP-based CloudEvents publisher to the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the publisher to.</param>
    /// <param name="configuration">The application configuration used to retrieve AMQP configuration settings.</param>
    /// <param name="sectionName">Optional. The name of the configuration section containing AMQP settings. Defaults to "publisherAmqp".</param>
    /// <returns>The updated <see cref="IServiceCollection"/> to allow method chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified configuration section does not exist or is missing required settings.</exception>
    public static IServiceCollection AddAmqpCloudEventsPublisher(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = DependencyInjection.DefaultSectionName)
    {
        sectionName ??= DependencyInjection.DefaultSectionName;

        IConfigurationSection section = configuration.GetSection(sectionName);

        if (!section.Exists())
        {
            throw new InvalidOperationException("missing config");
        }

        var config = section.Get<AmqpConfiguration>();
        Configure(services, config);

        return services;
    }

    private static void Configure(IServiceCollection services, AmqpConfiguration? configuration)
    {
        services.TryAddSingleton<IConnectionFactory, ConnectionFactory>();

        services.TryAddTransient<IConnection>(provider =>
        {
            var factory = ActivatorUtilities.GetServiceOrCreateInstance<IConnectionFactory>(provider);

            return factory.CreateAsync(configuration?.Address.ToAddress()).GetAwaiter().GetResult();
        });

        ObjectFactory<Publisher> factory =
            ActivatorUtilities.CreateFactory<Publisher>([typeof(IConnection), typeof(ILogger<Publisher>), typeof(string), typeof(string)]);

        services.TryAddTransient<IPublisher>(provider =>
        {
            var connection = ActivatorUtilities.GetServiceOrCreateInstance<IConnection>(provider);

            var logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<Publisher>>(provider);

            var address = $"/exchanges/{configuration?.ExchangeName ?? "innago-entity-events"}";

            string senderName = configuration?.SenderName ?? "entity-event-publisher";

            return factory.Invoke(provider, [connection, logger, address, senderName]);
        });
    }
}