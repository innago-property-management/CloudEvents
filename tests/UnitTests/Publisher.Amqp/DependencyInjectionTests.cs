namespace UnitTests.Publisher.Amqp;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using global::Amqp;

using Innago.Platform.Messaging.Publisher;
using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit.OpenCategories;

[UnitTest(nameof(DependencyInjection))]
public class DependencyInjectionTests
{
    [Fact(Skip = "Needs reworked since factory cannot be used")]
    public void AddAmqpCloudEventsPublisherShouldDefaultToSectionNamePublisherAmqp()
    {
        var data = new Dictionary<string, string?>
        {
            { "publisherAmqp:address:host", "localhost" },
            { "publisherAmqp:address:port", "5672" },
            { "publisherAmqp:address:scheme", "AMQP" },
            { "publisherAmqp:address:user", "user" },
            { "publisherAmqp:address:password", "pass" },
        };

        var builder = new ConfigurationBuilder();

        builder.AddInMemoryCollection(data);

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();

        var factory = Mock.Of<IConnectionFactory>(connectionFactory =>
                connectionFactory.CreateAsync(It.IsAny<Address>()) == Task.FromResult(Mock.Of<IConnection>(MockBehavior.Strict)),
            MockBehavior.Strict);

        services.AddAmqpCloudEventsPublisher(configuration);
        services.Replace(new ServiceDescriptor(typeof(IConnectionFactory), _ => factory, ServiceLifetime.Singleton));
        services.Replace(new ServiceDescriptor(typeof(IConnection), _ => factory, ServiceLifetime.Singleton));

        IServiceProvider provider = services.BuildServiceProvider();

        var actual = ActivatorUtilities.GetServiceOrCreateInstance<IConnection>(provider);

        actual.Should().NotBeNull();
    }

    [Fact(Skip = "Needs an update")]
    public void AddAmqpCloudEventsPublisherShouldSetCorrectAddressAndSender()
    {
        var data = new Dictionary<string, string?>
        {
            { "publisherAmqp:address:host", "localhost" },
            { "publisherAmqp:address:port", "5672" },
            { "publisherAmqp:address:scheme", "AMQP" },
            { "publisherAmqp:address:user", "user" },
            { "publisherAmqp:address:password", "pass" },
        };

        var builder = new ConfigurationBuilder();

        builder.AddInMemoryCollection(data);

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();

        var factory = Mock.Of<IConnectionFactory>(connectionFactory =>
                connectionFactory.CreateAsync(It.IsAny<Address>()) == Task.FromResult(Mock.Of<IConnection>(MockBehavior.Strict)),
            MockBehavior.Strict);

        services.AddAmqpCloudEventsPublisher(configuration);
        services.Replace(new ServiceDescriptor(typeof(IConnectionFactory), _ => factory, ServiceLifetime.Transient));
        services.AddTransient<ILogger<Publisher>>(_ => Mock.Of<ILogger<Publisher>>(MockBehavior.Strict));

        IServiceProvider provider = services.BuildServiceProvider();

        var actual = ActivatorUtilities.GetServiceOrCreateInstance<IPublisher>(provider) as Publisher;

        using var scope = new AssertionScope();
        actual!.SenderName.Should().Be("entity-event-publisher");
        actual.Address.Should().Be("/exchange/innago-entity-events");
    }
}