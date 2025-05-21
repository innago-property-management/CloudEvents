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

            await TryHelpers.TryAsync(() => publisher.PublishAsync(entityEvent)).ConfigureAwait(false);

            await Task.Delay(30_000, cancellationToken).ConfigureAwait(false);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return publisher.DisposeAsync().AsTask();
    }

    private static IEntityEventInfo<SomeEntity> MakeEntityEvent()
    {
        var verb = MyHostedService.Faker.PickRandom<Verb>();
        string entityId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string tenantId = MyHostedService.Faker.Random.AlphaNumeric(8);

        var data = new SomeEntity(MyHostedService.Faker.Commerce.Color());

        var info = new EntityEventInfo<SomeEntity>(entityId, verb, tenantId, Data: data);
        
        return info;
    }

    // ReSharper disable once NotAccessedPositionalProperty.Local
    private record SomeEntity(string Value);
}