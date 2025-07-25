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
    private static readonly Faker Faker = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            string random = MyHostedService.Faker.PickRandom("Account", "Transaction");

            Task<Result> task = random switch
            {
                "Account" => PublishAsync(MakeAccountEntityEvent()),
                _ => PublishAsync(MakeTransactionEntityEvent()),
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

    private static EntityEventInfo<Transaction> MakeTransactionEntityEvent()
    {
        const Verb verb = Verb.Create;
        string entityId = Faker.Random.AlphaNumeric(8);
        string tenantId = Faker.Random.AlphaNumeric(8);
        string emailAddress = Faker.Person.Email;

        PaymentMethod paymentMethod;

        if (Faker.Random.Bool())
        {
            // Credit Card
            paymentMethod = new PaymentMethod(
                PaymentType: "$credit_card",
                CardBin: MyHostedService.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                CardLast4: Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                PaymentGateway: Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                VerificationStatus: Faker.PickRandom("$success", "$failure", "$pending"),
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
                RoutingNumber: Faker.Finance.RoutingNumber(),
                BankName: Faker.Company.CompanyName(),
                BankCountry: "US",
                AccountNumberLast5: Faker.Finance.Account(5)
            );
        }

        var data = new Transaction(
            Amount: Faker.Finance.Amount(1, 5000),
            CurrencyCode: Faker.Finance.Currency().Code,
            DeclineCategory: Faker.Random.Bool()
                ? Faker.PickRandom("$avs_failed", "$cvv_failed", "$insufficient_funds", "$card_blacklist", "$declined")
                : null,
            InvoiceId: Faker.Random.Guid().ToString(),
            OrganizationId: Faker.Random.Guid().ToString(),
            SessionId: Faker.Random.Guid().ToString(),
            TransactionId: Faker.Random.Guid().ToString(),
            TransactionType: Faker.PickRandom("$authorize", "$capture", "$sale"),
            Ip: Faker.Internet.Ip(),
            Browser: new Browser(
                UserAgent: Faker.Internet.UserAgent(),
                AcceptLanguage: "en-US,en;q=0.9",
                ContentLanguage: "en-US"
            ),
            BillingAddress: new Address(
                AddressLine1: Faker.Address.StreetAddress(),
                AddressLine2: Faker.Random.Bool() ? Faker.Address.SecondaryAddress() : null,
                City: Faker.Address.City(),
                State: Faker.Address.StateAbbr(),
                Country: "US",
                PostalCode: Faker.Address.ZipCode(),
                Phone: Faker.Person.Phone
            ),
            PaymentMethod: paymentMethod,
            UserFullName: Faker.Person.FullName,
            UserEmailAddress: emailAddress,
            SellerUserId: MyHostedService.Faker.Person.Email,
            Time: DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Transaction>(entityId, verb, tenantId, Data: data, UserEmailAddress: emailAddress);

        return info;
    }

    private static EntityEventInfo<Account> MakeAccountEntityEvent()
    {
        Verb verb = MyHostedService.Faker.PickRandom(Verb.Create, Verb.Update);
        string entityId = Faker.Random.AlphaNumeric(8);
        string tenantId = Faker.Random.AlphaNumeric(8);
        string emailAddress = Faker.Person.Email;

        List<PaymentMethod> paymentMethods =
        [
            new(
                PaymentType: "$credit_card",
                CardBin: MyHostedService.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[..6],
                CardLast4: MyHostedService.Faker.Finance.CreditCardNumber(CardType.Visa).Replace("-", string.Empty)[^4..],
                PaymentGateway: MyHostedService.Faker.PickRandom("$authorizenet", "$stripe", "$braintree"),
                VerificationStatus: MyHostedService.Faker.PickRandom("$success", "$failure", "$pending"),
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
                RoutingNumber: MyHostedService.Faker.Finance.RoutingNumber(),
                BankName: MyHostedService.Faker.Company.CompanyName(),
                BankCountry: "US",
                AccountNumberLast5: MyHostedService.Faker.Finance.Account(5)
            ),
        ];

        var data = new Account(
            UserEmailAddress: emailAddress,
            SessionId: Faker.Random.Guid().ToString(),
            UserFullName: Faker.Person.FullName,
            Ip: Faker.Internet.Ip(),
            Browser: new Browser(
                UserAgent: Faker.Internet.UserAgent(),
                AcceptLanguage: "en-US,en;q=0.9",
                ContentLanguage: "en-US"
            ),
            BillingAddress: new Address(
                AddressLine1: Faker.Address.StreetAddress(),
                AddressLine2: Faker.Random.Bool() ? Faker.Address.SecondaryAddress() : null,
                City: Faker.Address.City(),
                State: Faker.Address.StateAbbr(),
                Country: "US",
                PostalCode: Faker.Address.ZipCode(),
                Phone: Faker.Person.Phone
            ),
            PaymentMethods: paymentMethods.ToArray(),
            Time: DateTimeOffset.Now
        );

        var info = new EntityEventInfo<Account>(entityId, verb, tenantId, Data: data, UserEmailAddress: emailAddress);

        return info;
    }
}