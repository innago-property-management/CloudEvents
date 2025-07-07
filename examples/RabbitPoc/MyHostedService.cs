namespace RabbitPoc;

using Bogus;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;
using Innago.Shared.TryHelpers;

using Microsoft.Extensions.Hosting;

internal class MyHostedService(IPublisher publisher) : IHostedService
{
    private static readonly Faker Faker = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            IEntityEventInfo<SomeEntity> entityEvent = MakeEntityEvent();

            await TryHelpers.TryAsync(async () =>
            {
                await publisher.PublishAsync(entityEvent, SourceGenerationContext.Default).ConfigureAwait(false);
            }).ConfigureAwait(false);

            await Task.Delay(3_000, cancellationToken).ConfigureAwait(false);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static IEntityEventInfo<SomeEntity> MakeEntityEvent()
    {
        var verb = MyHostedService.Faker.PickRandom<Verb>();
        string entityId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string tenantId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string emailAddress = MyHostedService.Faker.Person.Email;

        var data = new SomeEntity(
            MyHostedService.Faker.Commerce.Color(),
            emailAddress);

        var info = new EntityEventInfo<SomeEntity>(entityId, verb, tenantId, Data: data, emailAddress);

        return info;
    }

    internal record SomeEntity(string Value, string EmailAddress);
}