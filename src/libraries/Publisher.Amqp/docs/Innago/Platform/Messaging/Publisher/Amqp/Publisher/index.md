### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp')

## Publisher Class

Represents a publisher for sending CloudEvent messages to an AMQP\-based messaging system\.
Encapsulates the logic for establishing a session, creating sender links, and publishing events\.

```csharp
public sealed class Publisher : Innago.Platform.Messaging.Publisher.IPublisher, System.IAsyncDisposable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; Publisher

Implements [Innago\.Platform\.Messaging\.Publisher\.IPublisher](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.publisher.ipublisher 'Innago\.Platform\.Messaging\.Publisher\.IPublisher'), [System\.IAsyncDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable 'System\.IAsyncDisposable')

| Constructors | |
| :--- | :--- |
| [Publisher\(IConnectionFactory, ILogger&lt;Publisher&gt;, AmqpConfiguration\)](Publisher(IConnectionFactory,ILogger_Publisher_,AmqpConfiguration).md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher\.Publisher\(Amqp\.IConnectionFactory, Microsoft\.Extensions\.Logging\.ILogger\<Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher\>, Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\)') | Represents a publisher for sending CloudEvent messages to an AMQP\-based messaging system\. Encapsulates the logic for establishing a session, creating sender links, and publishing events\. |
