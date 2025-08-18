namespace UnitTests.Publisher.Amqp;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using global::Amqp;
using global::Amqp.Framing;

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
    [Fact]
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

        IServiceProvider provider = services.BuildServiceProvider();

        var actual = ActivatorUtilities.GetServiceOrCreateInstance<IConnection>(provider);

        actual.Should().NotBeNull();
    }

    [Fact]
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

        var senderLink = Mock.Of<ISenderLink>(link => link.Name == "entity-event-publisher", MockBehavior.Strict);

        var sess = Mock.Of<ISession>(session => session.CreateSender(It.IsAny<string>(), It.IsAny<Target>(), It.IsAny<OnAttached>()) == senderLink,
            MockBehavior.Strict);

        var conn = Mock.Of<IConnection>(connection => connection.CreateSession() == sess, MockBehavior.Strict);

        var factory = Mock.Of<IConnectionFactory>(connectionFactory =>
                connectionFactory.CreateAsync(It.IsAny<Address>()) == Task.FromResult(conn),
            MockBehavior.Strict);

        services.AddAmqpCloudEventsPublisher(configuration);
        services.Replace(new ServiceDescriptor(typeof(IConnectionFactory), _ => factory, ServiceLifetime.Transient));
        services.AddTransient<ILogger<Publisher>>(_ => Mock.Of<ILogger<Publisher>>(MockBehavior.Strict));

        IServiceProvider provider = services.BuildServiceProvider();

        var actual = ActivatorUtilities.GetServiceOrCreateInstance<IPublisher>(provider) as Publisher;

        using var scope = new AssertionScope();
        actual!.Sender.Link.Name.Should().Be("entity-event-publisher");
    }
}
