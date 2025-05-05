# Cloud Events

## Libraries
- EntityEvents
  - Helpers for creating Entity Events
- EntityEvents.Abstractions
  - Defines a common format for entity event payloads
  - Helper for converting entity event into cloud event
- Publisher.Abstractions
  - Defines publish methods
- Publisher.Amqp
  - Implementation of IPublisher for AMQP 1.0

### Usage

in `appsettings.json`
```json
{
  "publisherAmqp": {
    "address": {
      "host": "localhost",
      "port": "5672",
      "scheme": "AMQP"
    }
  }
}
```

in `Program.cs`
```c#
// IServiceCollection services
// IConfiguration configuration
services.AddAmqpCloudEventsPublisher(configuration);
```

in a method which causes a mutation,
```c#
var data = new SomeEntity();

var info = new EntityEventInfo<SomeEntity>(entityId, verb, tenantId, Data: data);

// IPublisher publisher
await publisher.PublishAsync(entityEvent).ConfigureAwait(false);
```
