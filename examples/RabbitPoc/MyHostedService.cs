namespace RabbitPoc;

using Bogus;

using CloudNative.CloudEvents;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;

using Microsoft.Extensions.Hosting;

internal class MyHostedService(IPublisher publisher) : IHostedService
{
    private static readonly Faker Faker = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            CloudEvent cloudEvent = MakeCloudEvent();

            await publisher.PublishAsync<SomeEntity>(cloudEvent);

            Thread.Sleep(1_000);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return publisher.DisposeAsync().AsTask();
    }

    private static CloudEvent MakeCloudEvent()
    {
        var verb = MyHostedService.Faker.PickRandom<Verb>();
        string entityId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string tenantId = MyHostedService.Faker.Random.AlphaNumeric(8);

        var data = new SomeEntity(MyHostedService.Faker.Commerce.Color());

        var info = new EntityEventInfo<SomeEntity>(entityId, verb, tenantId, Data: data);

        var cloudEvent = info.ToCloudEvent();

        return cloudEvent;
    }

    private record SomeEntity(string Value);
}