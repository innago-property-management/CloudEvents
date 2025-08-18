### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[Publisher](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher')

## Publisher\(IConnectionFactory, ILogger\<Publisher\>, AmqpConfiguration\) Constructor

Represents a publisher for sending CloudEvent messages to an AMQP\-based messaging system\.
Encapsulates the logic for establishing a session, creating sender links, and publishing events\.

```csharp
public Publisher(Amqp.IConnectionFactory connectionFactory, Microsoft.Extensions.Logging.ILogger<Innago.Platform.Messaging.Publisher.Amqp.Publisher> logger, Innago.Platform.Messaging.Publisher.Amqp.AmqpConfiguration configuration);
```
#### Parameters

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnectionFactory,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,Innago.Platform.Messaging.Publisher.Amqp.AmqpConfiguration).connectionFactory'></a>

`connectionFactory` [Amqp\.IConnectionFactory](https://learn.microsoft.com/en-us/dotnet/api/amqp.iconnectionfactory 'Amqp\.IConnectionFactory')

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnectionFactory,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,Innago.Platform.Messaging.Publisher.Amqp.AmqpConfiguration).logger'></a>

`logger` [Microsoft\.Extensions\.Logging\.ILogger&lt;](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1 'Microsoft\.Extensions\.Logging\.ILogger\`1')[Publisher](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1 'Microsoft\.Extensions\.Logging\.ILogger\`1')

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnectionFactory,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,Innago.Platform.Messaging.Publisher.Amqp.AmqpConfiguration).configuration'></a>

`configuration` [AmqpConfiguration](../AmqpConfiguration/index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration')
