namespace UnitTests.EntityEvents;

using AutoFixture;

using FluentAssertions;

using Innago.Platform.Messaging.EntityEvents;

using JetBrains.Annotations;

using Moq;

using Xunit.OpenCategories;

[UnitTest(nameof(EntityWithIdExtensions))]
public class EntityWithIdExtensionsTests
{
    private static Fixture Fixture { get; } = new();
    
    [Fact]
    public void ToEntityEventInfoShouldThrowIfNull()
    {
        IEntityWithId<int> entity = null!;
        
        Action act = () => entity.ToEntityEventInfo(Verb.Create);

        act.Should().Throw<ArgumentNullException>().WithParameterName(nameof(entity));
    }
    
    [Fact]
    public void ToEntityEventInfoShouldThrowIfIdNull()
    {
        var entity = Mock.Of<IEntityWithId<object>>(e => e.Id == null!, MockBehavior.Strict);
        
        Action act = () => entity.ToEntityEventInfo(Verb.Create);

        act.Should().Throw<InvalidOperationException>().WithMessage($"{nameof(entity.Id)}*null*");
    }
    
    [Fact]
    public void ToEntityEventInfoShouldMapCorrectly()
    {
        var entity = Mock.Of<IEntityWithId<string>>(e => 
            e.Id == Fixture.Create<string>(), 
            MockBehavior.Strict);

        var verb = Fixture.Create<Verb>();
        var tenantId = Fixture.Create<string>();
        
        IEntityEventInfo<IEntityWithId<string>> actual = entity.ToEntityEventInfo(verb, tenantId);

        actual.Id.Should().Be(entity.Id);
        actual.Verb.Should().Be(verb);
        actual.TenantId.Should().Be(tenantId);
        actual.Data.Should().Be(entity);
    }

    [Fact]
    public void ToCreateEntityEventInfoShouldSetVerbCorrectly()
    {
        var entity = Mock.Of<IEntityWithId<string>>(e => 
                e.Id == Fixture.Create<string>(), 
            MockBehavior.Strict);
        
        var tenantId = Fixture.Create<string>();

        IEntityEventInfo<IEntityWithId<string>> actual = entity.ToCreateEntityEventInfo(tenantId);

        actual.Verb.Should().Be(Verb.Create);
        actual.TenantId.Should().Be(tenantId);
    }

    [Fact]
    public void ToUpdateEntityEventInfoShouldSetVerbCorrectly()
    {
        var entity = Mock.Of<IEntityWithId<string>>(e => 
                e.Id == Fixture.Create<string>(), 
            MockBehavior.Strict);
        
        var tenantId = Fixture.Create<string>();

        IEntityEventInfo<IEntityWithId<string>> actual = entity.ToUpdateEntityEventInfo(tenantId);

        actual.Verb.Should().Be(Verb.Update);
        actual.TenantId.Should().Be(tenantId);
    }

    [Fact]
    public void ToDeleteEntityEventInfoShouldSetVerbCorrectly()
    {
        var entity = Mock.Of<IEntityWithId<string>>(e => 
                e.Id == Fixture.Create<string>(), 
            MockBehavior.Strict);
        
        var tenantId = Fixture.Create<string>();

        IEntityEventInfo<IEntityWithId<string>> actual = entity.ToDeleteEntityEventInfo(tenantId);

        actual.Verb.Should().Be(Verb.Delete);
        actual.TenantId.Should().Be(tenantId);
    }

    [Fact]
    public void ToPurgeEntityEventInfoShouldSetVerbCorrectly()
    {
        var entity = Mock.Of<IEntityWithId<string>>(e => 
                e.Id == Fixture.Create<string>(), 
            MockBehavior.Strict);
        
        var tenantId = Fixture.Create<string>();

        IEntityEventInfo<IEntityWithId<string>> actual = entity.ToPurgeEntityEventInfo(tenantId);

        actual.Verb.Should().Be(Verb.Purge);
        actual.TenantId.Should().Be(tenantId);
    }
}