namespace UnitTests.Publisher.Amqp;

using System.Text.Json.Serialization.Metadata;

using global::Amqp;
using global::Amqp.Framing;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit.OpenCategories;

[UnitTest(nameof(Publisher))]
public class PublisherTests
{
    [Fact]
    public async Task PublishAsyncShould()
    {
        const string addressPrefix = "/exchanges/test-events";
        const string senderName = "test-sender";

        var senderLink = Mock.Of<ISenderLink>(link => 
                link.SendAsync(It.IsAny<Message>()) == Task.CompletedTask &&
            link.CloseAsync() == Task.CompletedTask,
            MockBehavior.Strict);

        var sess = Mock.Of<ISession>(session => 
                session.CreateSender(It.IsAny<string>(), It.IsAny<Target>(), It.IsAny<OnAttached>()) == senderLink,
            MockBehavior.Strict);

        var conn = Mock.Of<IConnection>(
            connection => connection.CreateSession() == sess,
            MockBehavior.Strict);

        var logger = Mock.Of<ILogger<Publisher>>(MockBehavior.Loose);

        var info = new EntityEventInfo<string>(Guid.NewGuid().ToString(), Verb.Create);
        Publisher publisher = new(conn, logger, addressPrefix, senderName);
        await publisher.PublishAsync(info, new DefaultJsonTypeInfoResolver());
        
        Mock.Get(senderLink)
            .Verify(link => link.SendAsync(It.IsAny<Message>()));
        
        Mock.Get(sess)
            .Verify(session => 
                session.CreateSender(senderName, 
                    It.Is<Target>(target => target.Durable == 1 && target.Address == $"{addressPrefix}/{info.Subject}"), It.IsAny<OnAttached>()));
    }
}