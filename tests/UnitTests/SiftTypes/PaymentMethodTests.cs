namespace UnitTests.SiftTypes;

using System.Text.Json.Serialization;

using AwesomeAssertions;

using Innago.Shared.UnitTesting.TestHelpers;
using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(PaymentMethodTests.paymentMethod))]
public class PaymentMethodTests
{
    private readonly PaymentMethod paymentMethod = new("", "", "", "", "", "", "", "", "");

    [Fact]
    public void AccountNumberLast5_ShouldHaveJsonName_account_number_last5()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.AccountNumberLast5);

        attribute.Name.Should().Be("$account_number_last5");
    }

    [Fact]
    public void BankCountry_ShouldHaveJsonName_bank_country()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.BankCountry);

        attribute.Name.Should().Be("$bank_country");
    }

    [Fact]
    public void BankName_ShouldHaveJsonName_bank_name()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.BankName);

        attribute.Name.Should().Be("$bank_name");
    }

    [Fact]
    public void CardBin_ShouldHaveJsonName_card_bin()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.CardBin);

        attribute.Name.Should().Be("$card_bin");
    }

    [Fact]
    public void CardLast4_ShouldHaveJsonName_card_last4()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.CardLast4);

        attribute.Name.Should().Be("$card_last4");
    }

    [Fact]
    public void PaymentGateway_ShouldHaveJsonName_payment_gateway()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.PaymentGateway);

        attribute.Name.Should().Be("$payment_gateway");
    }

    [Fact]
    public void PaymentType_ShouldHaveJsonName_payment_type()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.PaymentType);

        attribute.Name.Should().Be("$payment_type");
    }

    [Fact]
    public void RoutingNumber_ShouldHaveJsonName_routing_number()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.RoutingNumber);

        attribute.Name.Should().Be("$routing_number");
    }

    [Fact]
    public void VerificationStatus_ShouldHaveJsonName_verification_status()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.paymentMethod.VerificationStatus);

        attribute.Name.Should().Be("$verification_status");
    }
}
