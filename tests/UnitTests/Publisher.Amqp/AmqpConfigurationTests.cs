namespace UnitTests.Publisher.Amqp;

using FluentAssertions;
using FluentAssertions.Execution;

using Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Configuration;

using Xunit.OpenCategories;

[UnitTest(nameof(AmqpConfiguration))]
public class AmqpConfigurationTests
{
    [Fact]
    public void ItShouldBindCorrectly()
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

        IConfigurationSection section = configuration.GetSection("publisherAmqp");

        var actual = section.Get<AmqpConfiguration>()!;

        using var scope = new AssertionScope();

        actual.Address.Host.Should().Be(data["publisherAmqp:address:host"]);
        actual.Address.Port.ToString().Should().Be(data["publisherAmqp:address:port"]);
        actual.Address.User.Should().Be(data["publisherAmqp:address:user"]);
        actual.Address.Password.Should().Be(data["publisherAmqp:address:password"]);
        actual.Address.Scheme.Should().Be(data["publisherAmqp:address:scheme"]);
    }
}