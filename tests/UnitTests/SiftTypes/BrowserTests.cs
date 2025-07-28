namespace UnitTests.SiftTypes;

using System.Text.Json.Serialization;

using AwesomeAssertions;

using Innago.Shared.UnitTesting.TestHelpers;
using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(Browser))]
public class BrowserTests
{
    private Browser browser = new("", "", "");

    [Fact]
    public void UserAgent_ShouldHaveJsonName_user_agent()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.browser.UserAgent);

        attribute.Name.Should().Be("$user_agent");
    }

    [Fact]
    public void AcceptLanguage_ShouldHaveJsonName_accept_language()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.browser.AcceptLanguage);

        attribute.Name.Should().Be("$accept_language");
    }

    [Fact]
    public void ContentLanguage_ShouldHaveJsonName_content_language()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.browser.ContentLanguage);

        attribute.Name.Should().Be("$content_language");
    }
}
