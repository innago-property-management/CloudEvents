namespace UnitTests.SiftTypes;

using System.Text.Json.Serialization;

using AwesomeAssertions;

using Bogus;

using Innago.Shared.UnitTesting.TestHelpers;
using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(Transaction))]
public class TransactionTests
{
    private static readonly Faker ValueFaker = new();

    private readonly Transaction transaction = new(0,
        null!,
        null,
        null!,
        null!,
        null!,
        null!,
        null!,
        null!,
        new Browser(string.Empty, string.Empty, string.Empty),
        new Address(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
        null!,
        null!,
        null!,
        null!,
        default);

    [Fact]
    public void Amount_ShouldHaveJsonIgnoreAttribute()
    {
        PropertyChecker.CheckAttribute<JsonIgnoreAttribute>(() => this.transaction.Amount)
            .Should()
            .NotBeNull();
    }

    [Fact]
    public void AmountForSift_ShouldBe_Amount_Times_1_000_000()
    {
        decimal amount = TransactionTests.ValueFaker.Random.Decimal(1, 100);
        Transaction tx = this.transaction with { Amount = amount };
        tx.AmountForSift.Should().Be((long)(amount * 1_000_000));
    }

    [Fact]
    public void AmountForSift_ShouldHaveJsonName_amount()
    {
        var attribute = PropertyChecker.CheckAttribute<JsonPropertyNameAttribute>(() => this.transaction.AmountForSift);

        attribute.Name.Should().Be("amount");
    }

    [Fact]
    public void UnixTime_ShouldUseValueFrom_Time()
    {
        DateTimeOffset time = TransactionTests.ValueFaker.Date.RecentOffset();
        Transaction tx = this.transaction with { Time = time };
        tx.UnixTime.Should().Be(time.ToUnixTimeMilliseconds());
    }

    [Fact]
    public void UserId_ShouldUseValueFrom_UserEmailAddress()
    {
        string? email = TransactionTests.ValueFaker.Internet.Email();
        Transaction tx = this.transaction with { UserEmailAddress = email };
        tx.UserId.Should().Be(email);
    }

    [Fact]
    public void UserPhone_ShouldUseValueFrom_BillingAddress_Phone()
    {
        string? phone = TransactionTests.ValueFaker.Phone.PhoneNumber();
        Address address = this.transaction.BillingAddress with { Phone = phone };
        Transaction tx = this.transaction with { BillingAddress = address };
        tx.UserPhone.Should().Be(phone);
    }
}