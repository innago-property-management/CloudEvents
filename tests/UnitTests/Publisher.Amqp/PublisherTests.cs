namespace UnitTests.Publisher.Amqp;

using System.Diagnostics;
using System.Text.Json.Serialization.Metadata;

using AutoFixture;

using AwesomeAssertions;

using global::Amqp;
using global::Amqp.Framing;
using global::Amqp.Types;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using OpenTelemetry.Trace;

using Xunit.OpenCategories;

[UnitTest(nameof(Publisher))]
public class PublisherTests
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task DisposeAsync_CompletesSuccessfully()
    {
        var mockSenderLink = new Mock<ISenderLink>(MockBehavior.Strict);
        mockSenderLink.Setup(link => link.CloseAsync()).Returns(Task.CompletedTask);

        var mockSession = new Mock<ISession>(MockBehavior.Strict);
        
        mockSession.Setup(sess => 
            sess.CreateSender(It.IsAny<string>(), It.IsAny<Target>(), It.IsAny<OnAttached>()))
            .Returns(() => mockSenderLink.Object);
        
        mockSession.Setup(sess => sess.CloseAsync()).Returns(Task.CompletedTask);

        var mockConnection = new Mock<IConnection>(MockBehavior.Strict);
        mockConnection.Setup(conn => conn.CreateSession()).Returns(mockSession.Object);

        ILogger<Publisher> logger = MakeLogger();

        var publisher = new Publisher(
            mockConnection.Object,
            logger,
            "/prefix",
            "sender");

        Exception? ex = await Record.ExceptionAsync(() => publisher.DisposeAsync().AsTask());
        ex.Should().BeNull();
        mockSession.Verify(session => session.CloseAsync(), Times.Once);
    }

    [Fact]
    public async Task PublishAsyncShould()
    {
        const string addressPrefix = "/exchange/test-events";
        const string senderName = "test-sender";

        ISenderLink senderLink = SetupMockSenderLink();
        ISession session = CreateMockSession(senderLink);
        IConnection connection = CreateMockConnection(session);
        ILogger<Publisher> logger = MakeLogger();

        IHost host = CreateHostWithTelemetry();
        host.Start();

        var info = new EntityEventInfo<string>(Guid.NewGuid().ToString(), PublisherTests.Fixture.Create<Verb>());
        Publisher publisher = new(connection, logger, addressPrefix, senderName);
        await publisher.PublishAsync(info, new DefaultJsonTypeInfoResolver());

        await host.StopAsync();

        Mock.Get(senderLink).Verify(link => link.SendAsync(It.IsAny<Message>()));
    }

    private static IHost CreateHostWithTelemetry()
    {
        IHostBuilder hostBuilder = Host.CreateDefaultBuilder();
        List<Activity> activities = [];

        hostBuilder.ConfigureServices((_, services) =>
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder.AddSource(PublisherTracer.Source.Name);
                    builder.AddInMemoryExporter(activities);
                });
        });

        IHost host = hostBuilder.Build();
        return host;
    }

    private static IConnection CreateMockConnection(ISession session)
    {
        return Mock.Of<IConnection>(
            connection => connection.CreateSession() == session,
            MockBehavior.Strict);
    }

    private static ISession CreateMockSession(IAmqpObject senderLink)
    {
        return Mock.Of<ISession>(session =>
                session.CreateSender(It.IsAny<string>(), It.IsAny<Target>(), It.IsAny<OnAttached>()) == senderLink,
            MockBehavior.Strict);
    }

    private static ILogger<Publisher> MakeLogger()
    {
        var mock = new Mock<ILogger<Publisher>>(MockBehavior.Strict);

        mock.Setup(logger => logger.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception?>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        mock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

        return mock.Object;
    }

    private static ISenderLink SetupMockSenderLink(bool shouldThrow = false)
    {
        var mockSenderLink = new Mock<ISenderLink>(MockBehavior.Strict);

        mockSenderLink.Setup(link =>
                link.SendAsync(It.IsAny<Message>()))
            .Callback(() =>
            {
                if (!shouldThrow)
                {
                    return;
                }

                var error = new Error(new Symbol("error")) { Condition = "amqp:internal-error", Description = "Simulated error" };
                mockSenderLink.Object.CloseAsync(TimeSpan.Zero, error).GetAwaiter().GetResult();
            })
            .Returns(Task.CompletedTask);

        mockSenderLink.Setup(link => link.CloseAsync(It.IsAny<TimeSpan>(), It.IsAny<Error>()))
            .Returns(Task.CompletedTask);

        mockSenderLink.Setup(link => link.AddClosedCallback(It.IsAny<ClosedCallback>()));

        return mockSenderLink.Object;
    }
}