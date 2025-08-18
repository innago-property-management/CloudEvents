namespace Innago.Platform.Messaging.Publisher.Amqp;

using global::Amqp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

/// <summary>
///     Provides extension methods for setting up dependencies related to AMQP-based CloudEvents messaging.
///     This static class enables configuration and registration of AMQP publishers in the service collection.
/// </summary>
public static class DependencyInjection
{
    private const string DefaultSectionName = "publisherAmqp";

    /// <summary>
    ///     Adds configuration and setup for an AMQP-based CloudEvents publisher to the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the publisher to.</param>
    /// <param name="configuration">The application configuration used to retrieve AMQP configuration settings.</param>
    /// <param name="sectionName">
    ///     Optional. The name of the configuration section containing AMQP settings. Defaults to
    ///     "publisherAmqp".
    /// </param>
    /// <returns>The updated <see cref="IServiceCollection" /> to allow method chaining.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the specified configuration section does not exist or is missing
    ///     required settings.
    /// </exception>
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

        AmqpConfiguration config = section.Get<AmqpConfiguration>() ?? throw new InvalidOperationException("missing config");
        Configure(services, config);

        return services;
    }

    private static void Configure(IServiceCollection services, AmqpConfiguration configuration)
    {
        services.TryAddSingleton<IConnectionFactory, ConnectionFactory>();

        services.TryAddTransient<IConnection>(provider =>
        {
            var address = configuration.Address.ToAddress();

            var factory = ActivatorUtilities.GetServiceOrCreateInstance<IConnectionFactory>(provider);

            IConnection connection = factory.CreateAsync(address).Result;

            return connection;
        });

        services.TryAddSingleton<IPublisher>(provider =>
        {
            var connectionFactory = ActivatorUtilities.GetServiceOrCreateInstance<IConnectionFactory>(provider);

            var logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<Publisher>>(provider);

            return new Publisher(connectionFactory, logger, configuration);
        });
    }
}
