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
    // ReSharper disable once MemberCanBeMadeStatic.Local
    private Faker Faker => new();

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
                PaymentType: "$credit_card",
                CardBin: this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                CardLast4: this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                PaymentGateway: this.Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                VerificationStatus: this.Faker.PickRandom("$success", "$failure", "$pending"),
                RoutingNumber: null,
                BankName: null,
                BankCountry: null,
                AccountNumberLast5: null
            );
        }
        else
        {
            // EFT
            paymentMethod = new PaymentMethod(
                PaymentType: "$electronic_fund_transfer",
                CardBin: null,
                CardLast4: null,
                PaymentGateway: null,
                VerificationStatus: null,
                RoutingNumber: this.Faker.Finance.RoutingNumber(),
                BankName: this.Faker.Company.CompanyName(),
                BankCountry: "US",
                AccountNumberLast5: this.Faker.Finance.Account(5)
            );
        }

        var data = new Transaction(
            Amount: this.Faker.Finance.Amount(1, 5000),
            CurrencyCode: this.Faker.Finance.Currency().Code,
            DeclineCategory: this.Faker.Random.Bool()
                ? this.Faker.PickRandom("$avs_failed", "$cvv_failed", "$insufficient_funds", "$card_blacklist", "$declined")
                : null,
            InvoiceId: this.Faker.Random.Guid().ToString(),
            OrganizationId: this.Faker.Random.Guid().ToString(),
            SessionId: this.Faker.Random.Guid().ToString(),
            TransactionId: this.Faker.Random.Guid().ToString(),
            TransactionType: this.Faker.PickRandom("$authorize", "$capture", "$sale"),
            Ip: this.Faker.Internet.Ip(),
            Browser: new Browser(
                UserAgent: this.Faker.Internet.UserAgent(),
                AcceptLanguage: "en-US,en;q=0.9",
                ContentLanguage: "en-US"
            ),
            BillingAddress: new Address(
                AddressLine1: this.Faker.Address.StreetAddress(),
                AddressLine2: this.Faker.Random.Bool() ? this.Faker.Address.SecondaryAddress() : null,
                City: this.Faker.Address.City(),
                State: this.Faker.Address.StateAbbr(),
                Country: "US",
                PostalCode: this.Faker.Address.ZipCode(),
                Phone: this.Faker.Phone.PhoneNumber("1-###-###-####")
            ),
            PaymentMethod: paymentMethod,
            UserFullName: this.Faker.Person.FullName,
            UserEmailAddress: emailAddress,
            SellerUserId: this.Faker.Person.Email,
            Time: DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Transaction>(entityId, verb, tenantId, Data: data, UserEmailAddress: emailAddress);

        return info;
    }

    private EntityEventInfo<Account> MakeAccountEntityEvent()
    {
        Verb verb = this.Faker.PickRandom(Verb.Create, Verb.Update);
        string entityId = this.Faker.Random.AlphaNumeric(8);
        string tenantId = this.Faker.Random.AlphaNumeric(8);
        string emailAddress = this.Faker.Person.Email;

        List<PaymentMethod> paymentMethods =
        [
            new(
                PaymentType: "$credit_card",
                CardBin: this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                CardLast4: this.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                PaymentGateway: this.Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                VerificationStatus: this.Faker.PickRandom("$success", "$failure", "$pending"),
                RoutingNumber: null,
                BankName: null,
                BankCountry: null,
                AccountNumberLast5: null
            ),

            new(
                PaymentType: "$electronic_fund_transfer",
                CardBin: null,
                CardLast4: null,
                PaymentGateway: null,
                VerificationStatus: null,
                RoutingNumber: this.Faker.Finance.RoutingNumber(),
                BankName: this.Faker.Company.CompanyName(),
                BankCountry: "US",
                AccountNumberLast5: this.Faker.Finance.Account(5)
            ),
        ];

        var data = new Account(
            UserEmailAddress: emailAddress,
            SessionId: this.Faker.Random.Guid().ToString(),
            UserFullName: this.Faker.Person.FullName,
            Ip: this.Faker.Internet.Ip(),
            Browser: new Browser(
                UserAgent: this.Faker.Internet.UserAgent(),
                AcceptLanguage: "en-US,en;q=0.9",
                ContentLanguage: "en-US"
            ),
            BillingAddress: new Address(
                AddressLine1: this.Faker.Address.StreetAddress(),
                AddressLine2: this.Faker.Random.Bool() ? this.Faker.Address.SecondaryAddress() : null,
                City: this.Faker.Address.City(),
                State: this.Faker.Address.StateAbbr(),
                Country: "US",
                PostalCode: this.Faker.Address.ZipCode(),
                Phone: this.Faker.Phone.PhoneNumber("1-###-###-####")
            ),
            PaymentMethods: paymentMethods.ToArray(),
            Time: DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Account>(entityId, verb, tenantId, Data: data, UserEmailAddress: emailAddress);

        return info;
    }
}
