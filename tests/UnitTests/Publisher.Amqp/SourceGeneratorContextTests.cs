namespace UnitTests.Publisher.Amqp;

using System.Text.Json;

using AwesomeAssertions;

using Innago.Platform.Messaging.Publisher.Amqp;

using Xunit.OpenCategories;

[UnitTest(nameof(SourceGeneratorContext))]
public class SourceGeneratorContextTests
{
    [Fact]
    public void DateTimeOffsetShouldBeSetup()
    {
        SourceGeneratorContext.Default.GetTypeInfo(typeof(DateTimeOffset)).Should().NotBeNull();
    }

    [Fact]
    public void DictionaryStringObjectShouldBeSetup()
    {
        SourceGeneratorContext.Default.GetTypeInfo(typeof(Dictionary<string, object>)).Should().NotBeNull();
    }

    [Fact]
    public void EnumsShouldSerializeAsStrings()
    {
        SourceGeneratorContext.Default.Options.PropertyNamingPolicy.Should().Be(JsonNamingPolicy.CamelCase);
    }

    [Fact]
    public void UriShouldBeSetup()
    {
        SourceGeneratorContext.Default.GetTypeInfo(typeof(Uri)).Should().NotBeNull();
    }
}