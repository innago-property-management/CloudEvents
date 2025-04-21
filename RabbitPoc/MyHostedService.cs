namespace RabbitPoc;

using Bogus;

using CloudNative.CloudEvents;

using Microsoft.Extensions.Hosting;

internal class MyHostedService(Publisher publisher) : IHostedService
{
    private static readonly Faker Faker = new();

    private static readonly IEnumerable<string> EntityNames =
        MyHostedService.Faker.Make(5, () => MyHostedService.Faker.Commerce.Product().Replace(" ", string.Empty));

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            (string subject, CloudEvent cloudEvent) = MakeCloudEvent();

            await publisher.PublishAsync(subject, cloudEvent);

            Thread.Sleep(1_000);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return publisher.DisposeAsync().AsTask();
    }

    private static (string subject, CloudEvent cloudEvent) MakeCloudEvent()
    {
        var entityName = $"Innago.Demo.CloudEvents.{MyHostedService.Faker.PickRandom(MyHostedService.EntityNames)}";
        var verb = MyHostedService.Faker.PickRandom<Verb>();
        string entityId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string tenantId = MyHostedService.Faker.Random.AlphaNumeric(8);
        var subject = $"{entityName}.{verb}.{entityId}";

        CloudEvent cloudEvent = new()
        {
            Id = Guid.NewGuid().ToString(),
            Source = new Uri("urn:innago-com:poc-emitter"),
            Type = "com.innago.entity-event",
            Data = new EntityEventInfo(entityName, entityId, verb, tenantId),
            Subject = subject,
            Time = DateTimeOffset.UtcNow,
        };

        return (subject, cloudEvent);
    }
}