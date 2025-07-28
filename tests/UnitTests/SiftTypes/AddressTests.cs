namespace UnitTests.SiftTypes;

using System.Text.Json.Serialization;

using AwesomeAssertions;

using Innago.Shared.UnitTesting.TestHelpers;
using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(Address))]
public class AddressTests
{
    private readonly Address address = new("", "", "", "", "", "", "");

    [Fact]
    public void AddressLine1_ShouldHaveJsonName_address_1()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.AddressLine1);

        attribute.Name.Should().Be("$address_1");
    }

    [Fact]
    public void AddressLine2_ShouldHaveJsonName_address_2()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.AddressLine2);

        attribute.Name.Should().Be("$address_2");
    }

    [Fact]
    public void City_ShouldHaveJsonName_city()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.City);

        attribute.Name.Should().Be("$city");
    }

    [Fact]
    public void State_ShouldHaveJsonName_region()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.State);

        attribute.Name.Should().Be("$region");
    }

    [Fact]
    public void Country_ShouldHaveJsonName_country()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.Country);

        attribute.Name.Should().Be("$country");
    }

    [Fact]
    public void PostalCode_ShouldHaveJsonName_zipcode()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.PostalCode);

        attribute.Name.Should().Be("$zipcode");
    }

    [Fact]
    public void Phone_ShouldHaveJsonName_phone()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.address.Phone);

        attribute.Name.Should().Be("$phone");
    }
}
