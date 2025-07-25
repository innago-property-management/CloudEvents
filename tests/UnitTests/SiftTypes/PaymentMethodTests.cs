namespace UnitTests.SiftTypes;

using System.Text.Json.Serialization;

using AwesomeAssertions;

using Innago.Shared.UnitTesting.TestHelpers;
using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(PaymentMethod))]
public class PaymentMethodTests
{
    private readonly PaymentMethod PaymentMethod = new("", "", "", "", "", "", "", "", "");

    [Fact]
    public void PaymentType_ShouldHaveJsonName_payment_type()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.PaymentType);

        attribute.Name.Should().Be("$payment_type");
    }

    [Fact]
    public void CardBin_ShouldHaveJsonName_card_bin()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.CardBin);

        attribute.Name.Should().Be("$card_bin");
    }

    [Fact]
    public void CardLast4_ShouldHaveJsonName_card_last4()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.CardLast4);

        attribute.Name.Should().Be("$card_last4");
    }

    [Fact]
    public void PaymentGateway_ShouldHaveJsonName_payment_gateway()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.PaymentGateway);

        attribute.Name.Should().Be("$payment_gateway");
    }

    [Fact]
    public void VerificationStatus_ShouldHaveJsonName_verification_status()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.VerificationStatus);

        attribute.Name.Should().Be("$verification_status");
    }

    [Fact]
    public void RoutingNumber_ShouldHaveJsonName_routing_number()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.RoutingNumber);

        attribute.Name.Should().Be("$routing_number");
    }

    [Fact]
    public void BankName_ShouldHaveJsonName_bank_name()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.BankName);

        attribute.Name.Should().Be("$bank_name");
    }

    [Fact]
    public void BankCountry_ShouldHaveJsonName_bank_country()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.BankCountry);

        attribute.Name.Should().Be("$bank_country");
    }

    [Fact]
    public void AccountNumberLast5_ShouldHaveJsonName_account_number_last5()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.PaymentMethod.AccountNumberLast5);

        attribute.Name.Should().Be("$account_number_last5");
    }
}
