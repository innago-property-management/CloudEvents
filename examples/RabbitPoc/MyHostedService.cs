namespace RabbitPoc;

using Bogus;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;
using Innago.Shared.TryHelpers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class MyHostedService(IServiceProvider provider) : IHostedService
{
    private static readonly Faker Faker = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            IEntityEventInfo<SomeEntity> entityEvent = MakeEntityEvent();

            await TryHelpers.TryAsync(async () =>
            {
                await using var publisher = ActivatorUtilities.GetServiceOrCreateInstance<IPublisher>(provider);
                await publisher.PublishAsync(entityEvent, SourceGenerationContext.Default).ConfigureAwait(false);
            }).ConfigureAwait(false);

            await Task.Delay(30_000, cancellationToken).ConfigureAwait(false);
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

        var data = new SomeEntity(
            MyHostedService.Faker.Commerce.Color(),
            MyHostedService.Faker.Internet.Email());

        var info = new EntityEventInfo<SomeEntity>(entityId, verb, tenantId, Data: data);

        return info;
    }

    internal record SomeEntity(string Value, string EmailAddress);
}