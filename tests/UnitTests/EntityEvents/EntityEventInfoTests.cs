namespace UnitTests.EntityEvents;

using AutoFixture;

using FluentAssertions;

using Innago.Platform.Messaging.EntityEvents;

using Xunit.OpenCategories;

[UnitTest(nameof(EntityEventInfo<object>))]
public class EntityEventInfoTests
{
    private static Fixture Fixture { get; } = new();

    [Fact]
    public void EntityNameShouldBeFullNameOfType()
    {
        var info = Fixture.Create<EntityEventInfo<EntityEventInfoTests>>();

        string expected = typeof(EntityEventInfoTests).FullName!;

        info.EntityName.Should().Be(expected);
    }

    [Fact]
    public void SubjectShouldBeNameVerbIdTenant()
    {
        var info = Fixture.Create<EntityEventInfo<object>>();

        var expected = $"{info.EntityName}:{info.Verb}:{info.Id}:{info.TenantId}";

        info.Subject.Should().Be(expected);
    }

    [Fact]
    public void TimestampShouldBeNow()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        var info = Fixture.Create<EntityEventInfo<object>>();

        info.Timestamp.Should().BeCloseTo(now, TimeSpan.FromMilliseconds(50));
    }

    [Fact]
    public void TracingIdShouldBeSet()
    {
        var info = Fixture.Create<EntityEventInfo<object>>();

        info.TracingId.Should().NotBe(Guid.Empty);
    }
}