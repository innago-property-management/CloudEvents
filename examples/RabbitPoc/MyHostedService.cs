namespace RabbitPoc;

using Bogus;
using Bogus.DataSets;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;
using Innago.Shared.TryHelpers;
using Innago.SiftTypes;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Address = Innago.SiftTypes.Address;

internal class MyHostedService(IPublisher publisher, ILogger<MyHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            string random = this.Faker.PickRandom("Account", "Transaction");

            Task<Result> task = random switch
            {
                "Account" => PublishAsync(this.MakeAccountEntityEvent()),
                _ => PublishAsync(this.MakeTransactionEntityEvent()),
            };

            Result result = await task.ConfigureAwait(false);

            result.IfFailed(exception => { logger.Error(exception!.GetType().ToString(), exception.Message); });

            await Task.Delay(5_000, cancellationToken).ConfigureAwait(false);
        }

        return;

        Task<Result> PublishAsync<T>(IEntityEventInfo<T> ev)

        {
            return TryHelpers.TryAsync(() => publisher.PublishAsync(ev, SourceGenerationContext.Default));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private Faker Faker => new();

    private EntityEventInfo<Account> MakeAccountEntityEvent()
    {
        Verb verb = this.Faker.PickRandom(Verb.Create, Verb.Update);
        string entityId = this.Faker.Random.AlphaNumeric(8);
        string tenantId = this.Faker.Random.AlphaNumeric(8);
        string emailAddress = this.Faker.Person.Email;

        List<PaymentMethod> paymentMethods =
        [
            new(
                "$credit_card",
                this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                this.Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                this.Faker.PickRandom("$success", "$failure", "$pending"),
                null,
                null,
                null,
                null
            ),

            new(
                "$electronic_fund_transfer",
                null,
                null,
                null,
                null,
                this.Faker.Finance.RoutingNumber(),
                this.Faker.Company.CompanyName(),
                "US",
                this.Faker.Finance.Account(5)
            ),
        ];

        var data = new Account(
            emailAddress,
            this.Faker.Random.Guid().ToString(),
            this.Faker.Person.FullName,
            this.Faker.Internet.Ip(),
            new Browser(
                this.Faker.Internet.UserAgent(),
                "en-US,en;q=0.9",
                "en-US"
            ),
            new Address(
                this.Faker.Address.StreetAddress(),
                this.Faker.Random.Bool() ? this.Faker.Address.SecondaryAddress() : null,
                this.Faker.Address.City(),
                this.Faker.Address.StateAbbr(),
                "US",
                this.Faker.Address.ZipCode(),
                this.Faker.Phone.PhoneNumber("1-###-###-####")
            ),
            paymentMethods.ToArray(),
            DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Account>(entityId, verb, tenantId, data, emailAddress);

        return info;
    }

    private EntityEventInfo<Transaction> MakeTransactionEntityEvent()
    {
        const Verb verb = Verb.Create;
        string entityId = this.Faker.Random.AlphaNumeric(8);
        string tenantId = this.Faker.Random.AlphaNumeric(8);
        string emailAddress = this.Faker.Person.Email;

        PaymentMethod paymentMethod;

        if (this.Faker.Random.Bool())
        {
            // Credit Card
            paymentMethod = new PaymentMethod(
                "$credit_card",
                this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                this.Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                this.Faker.PickRandom("$success", "$failure", "$pending"),
                null,
                null,
                null,
                null
            );
        }
        else
        {
            // EFT
            paymentMethod = new PaymentMethod(
                "$electronic_fund_transfer",
                null,
                null,
                null,
                null,
                this.Faker.Finance.RoutingNumber(),
                this.Faker.Company.CompanyName(),
                "US",
                this.Faker.Finance.Account(5)
            );
        }

        var data = new Transaction(
            this.Faker.Finance.Amount(1, 5000),
            this.Faker.Finance.Currency().Code,
            this.Faker.Random.Bool()
                ? this.Faker.PickRandom("$avs_failed", "$cvv_failed", "$insufficient_funds", "$card_blacklist", "$declined")
                : null,
            this.Faker.Random.Guid().ToString(),
            this.Faker.Random.Guid().ToString(),
            this.Faker.Random.Guid().ToString(),
            this.Faker.Random.Guid().ToString(),
            this.Faker.PickRandom("$authorize", "$capture", "$sale"),
            this.Faker.Internet.Ip(),
            new Browser(
                this.Faker.Internet.UserAgent(),
                "en-US,en;q=0.9",
                "en-US"
            ),
            new Address(
                this.Faker.Address.StreetAddress(),
                this.Faker.Random.Bool() ? this.Faker.Address.SecondaryAddress() : null,
                this.Faker.Address.City(),
                this.Faker.Address.StateAbbr(),
                "US",
                this.Faker.Address.ZipCode(),
                this.Faker.Phone.PhoneNumber("1-###-###-####")
            ),
            paymentMethod,
            this.Faker.Person.FullName,
            emailAddress,
            this.Faker.Person.Email,
            DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Transaction>(entityId, verb, tenantId, data, emailAddress);

        return info;
    }
}
