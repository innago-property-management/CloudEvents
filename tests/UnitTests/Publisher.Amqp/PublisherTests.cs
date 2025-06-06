namespace UnitTests.Publisher.Amqp;

using System.Diagnostics;
using System.Text.Json.Serialization.Metadata;

using AutoFixture;

using FluentAssertions;
using FluentAssertions.Execution;

using global::Amqp;
using global::Amqp.Framing;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using OpenTelemetry.Trace;

using Xunit.Abstractions;
using Xunit.OpenCategories;

[UnitTest(nameof(Publisher))]
public class PublisherTests(ITestOutputHelper outputHelper)
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task PublishAsyncShould()
    {
        const string addressPrefix = "/exchanges/test-events";
        const string senderName = "test-sender";

        ISenderLink senderLink = SetupMockSenderLink();
        ISession session = CreateMockSession(senderLink);
        IConnection connection = CreateMockConnection(session);
        ILogger<Publisher> logger = SetupPublisherLogger();

        Activity? capturedActivity = null;

        ActivityListener listener = new()
        {
            ShouldListenTo = _ => true,
            ActivityStarted = activity => { capturedActivity = activity; },
        };

        ActivitySource.AddActivityListener(listener);
        IHost host = CreateHostWithTelemetry();
        host.Start();

        var info = new EntityEventInfo<string>(Guid.NewGuid().ToString(), PublisherTests.Fixture.Create<Verb>());
        Publisher publisher = new(connection, logger, addressPrefix, senderName);
        await publisher.PublishAsync(info, new DefaultJsonTypeInfoResolver());

        await host.StopAsync();

        Mock.Get(senderLink).Verify(link => link.SendAsync(It.IsAny<Message>()));

        Func<OnAttached, bool> onAttachedMatcher = OnAttachedMatcher;

        Mock.Get(session)
            .Verify(sess =>
                sess.CreateSender(senderName,
                    It.Is<Target>(target => target.Durable == 1 && target.Address == $"{addressPrefix}/{info.Subject}"),
                    It.Is<OnAttached>(attached => onAttachedMatcher(attached))));

        bool OnAttachedMatcher(OnAttached onAttached)
        {
            onAttached.Invoke(senderLink, new Attach());

            try
            {
                using var scope = new AssertionScope();

                capturedActivity!.Tags.ToList().ForEach(p => outputHelper.WriteLine($"{p.Key}={p.Value}"));

                outputHelper.WriteLine(
                    $"{info.TracingId:N},{info.Subject},{EntityEventInfoExtensions.EventType},{new Uri(EntityEventInfoExtensions.Source).AbsoluteUri}");

                capturedActivity!.Tags.Should().Contain(pair =>
                    pair.Key == "cloudEvent.Id" && pair.Value == info.TracingId.ToString("N"));

                capturedActivity!.Tags.Should().Contain(pair =>
                    pair.Key == "cloudEvent.Subject" && pair.Value == info.Subject);

                capturedActivity!.Tags.Should().Contain(pair =>
                    pair.Key == "cloudEvent.Source" && pair.Value == new Uri(EntityEventInfoExtensions.Source).AbsoluteUri);

                capturedActivity!.Tags.Should().Contain(pair =>
                    pair.Key == "cloudEvent.Type" && pair.Value == EntityEventInfoExtensions.EventType);

                capturedActivity!.Tags.Should().Contain(pair =>
                    pair.Key == "cloudEvent.Time" && pair.Value == info.Timestamp.ToString("O"));
            }
            catch
            {
                return false;
            }

            return true;
        }
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

    private static IConnection CreateMockConnection(ISession sess)
    {
        return Mock.Of<IConnection>(
            connection => connection.CreateSession() == sess,
            MockBehavior.Strict);
    }

    private static ISession CreateMockSession(ISenderLink senderLink)
    {
        return Mock.Of<ISession>(session =>
                session.CreateSender(It.IsAny<string>(), It.IsAny<Target>(), It.IsAny<OnAttached>()) == senderLink,
            MockBehavior.Strict);
    }

    private static ISenderLink SetupMockSenderLink()
    {
        var mockSenderLink = new Mock<ISenderLink>(MockBehavior.Strict);

        mockSenderLink.Setup(link =>
                link.SendAsync(It.IsAny<Message>()))
            .Returns(Task.CompletedTask);

        mockSenderLink.Setup(link => link.CloseAsync())
            .Returns(Task.CompletedTask);

        mockSenderLink.Setup(link => link.AddClosedCallback(It.IsAny<ClosedCallback>()));

        return mockSenderLink.Object;
    }

    private static ILogger<Publisher> SetupPublisherLogger()
    {
        return Mock.Of<ILogger<Publisher>>(MockBehavior.Loose);
    }
}