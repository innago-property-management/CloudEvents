namespace UnitTests.SiftTypes;

using AwesomeAssertions;

using Bogus;

using Innago.SiftTypes;

using Xunit.OpenCategories;

[UnitTest(nameof(Account))]
public class AccountTests
{
    private static readonly Faker ValueFaker = new();
    private readonly Account account = new(null!, null!, null!, null!, null!, null!, null!, default!);

    [Fact]
    public void Phone_ShouldUseValueFrom_BillingAddress_Phone()
    {
        Address address = new(null!, null, null!, null!, null!, null!, null!) { Phone = AccountTests.ValueFaker.Phone.PhoneNumber() };

        Account acct = this.account with
        {
            BillingAddress = address,
        };

        acct.Phone.Should().Be(acct.BillingAddress.Phone);
    }

    [Fact]
    public void UnixTime_ShouldUseValueFrom_Time()
    {
        DateTimeOffset time = AccountTests.ValueFaker.Date.RecentOffset();

        Account acct = this.account with
        {
            Time = time,
        };

        acct.UnixTime.Should().Be(time.ToUnixTimeMilliseconds());
    }

    [Fact]
    public void UserId_ShouldUseValueFrom_UserEmailAddress()
    {
        string? email = AccountTests.ValueFaker.Internet.Email();

        Account acct = this.account with
        {
            UserEmailAddress = email,
        };

        acct.UserId.Should().Be(email);
    }
}