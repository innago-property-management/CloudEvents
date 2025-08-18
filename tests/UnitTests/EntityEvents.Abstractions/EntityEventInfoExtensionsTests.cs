namespace UnitTests.EntityEvents.Abstractions;

using AutoFixture;

using AwesomeAssertions;

using Innago.Platform.Messaging.EntityEvents;

using Moq;

using Xunit.OpenCategories;

[UnitTest(nameof(EntityEventInfoExtensions))]
public class EntityEventInfoExtensionsTests
{
    private static Fixture Fixture { get; } = new();

    [Fact]
    public void ToCloudEventShouldEmailAddressCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent["emailaddress"].Should().Be(entityEventInfo.UserEmailAddress);
    }

    [Fact]
    public void ToCloudEventShouldSetDataCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Data.Should().Be(entityEventInfo);
    }

    [Fact]
    public void ToCloudEventShouldSetEntityActionCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent["entityaction"].Should().Be(entityEventInfo.Verb.ToString());
    }

    [Fact]
    public void ToCloudEventShouldSetEntityIdCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent["entityid"].Should().Be(entityEventInfo.Id);
    }

    [Fact]
    public void ToCloudEventShouldSetEntityNameCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent["entityname"].Should().Be(entityEventInfo.EntityName);
    }

    [Fact]
    public void ToCloudEventShouldSetIdCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Id.Should().Be(entityEventInfo.TracingId.ToString("N"));
        cloudEvent["entityid"].Should().Be(entityEventInfo.Id);
    }

    [Fact]
    public void ToCloudEventShouldSetSourceCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Source.Should().Be(new Uri($"urn:innago:{typeof(EntityEventInfoExtensions).Assembly.GetName().Name}"));
    }

    [Fact]
    public void ToCloudEventShouldSetSubjectCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Subject.Should().Be(entityEventInfo.Subject);
    }

    [Fact]
    public void ToCloudEventShouldSetTenantIdCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent["tenantid"].Should().Be(entityEventInfo.TenantId);
    }

    [Fact]
    public void ToCloudEventShouldSetTimeCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Time.Should().Be(entityEventInfo.Timestamp);
        cloudEvent["timestamp"].Should().Be(entityEventInfo.Timestamp.ToString("O"));
    }

    [Fact]
    public void ToCloudEventShouldSetTypeCorrectly()
    {
        IEntityEventInfo<(string K, string V)> entityEventInfo = MakeEntityEventInfo();
        var cloudEvent = entityEventInfo.ToCloudEvent();

        cloudEvent.Type.Should().Be("innago:entity-event");
    }

    private static IEntityEventInfo<(string K, string V)> MakeEntityEventInfo()
    {
        return Mock.Of<IEntityEventInfo<(string K, string V)>>(info =>
                info.TracingId == Fixture.Create<Guid>() &&
                info.Subject == Fixture.Create<string>() &&
                info.Timestamp == Fixture.Create<DateTimeOffset>() &&
                info.EntityName == typeof((string K, string V)).FullName &&
                info.Id == Fixture.Create<string>() &&
                info.TenantId == Fixture.Create<string>() &&
                info.Verb == Fixture.Create<Verb>() &&
                info.UserEmailAddress == Fixture.Create<string>(),
            MockBehavior.Strict);
    }
}