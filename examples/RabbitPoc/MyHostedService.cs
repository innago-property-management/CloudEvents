namespace RabbitPoc;

using Bogus;

using Innago.Platform.Messaging.EntityEvents;
using Innago.Platform.Messaging.Publisher;
using Innago.Shared.TryHelpers;

using JetBrains.Annotations;

using Microsoft.Extensions.Hosting;

internal class MyHostedService(IPublisher publisher) : IHostedService
{
    private static readonly Faker Faker = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            IEntityEventInfo<Wrapper> entityEvent = MakeEntityEvent();

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

    private static IEntityEventInfo<Wrapper> MakeEntityEvent()
    {
        var verb = MyHostedService.Faker.PickRandom<Verb>();
        string entityId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string tenantId = MyHostedService.Faker.Random.AlphaNumeric(8);
        string emailAddress = MyHostedService.Faker.Person.Email;

        var data = new Wrapper(MyHostedService.Faker.Music.Genre());

        var info = new EntityEventInfo<Wrapper>(entityId, verb, tenantId, Data: data, emailAddress);

        return info;
    }
}

[UsedImplicitly]
internal record Wrapper(string Value);