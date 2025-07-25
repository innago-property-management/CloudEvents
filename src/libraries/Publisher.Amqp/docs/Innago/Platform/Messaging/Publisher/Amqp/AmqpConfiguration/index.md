### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp')

## AmqpConfiguration Class

Represents the configuration settings for AMQP \(Advanced Message Queuing Protocol\)\.
This class is used to define essential details required to establish
and configure an AMQP connection and communication\.

```csharp
public class AmqpConfiguration
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; AmqpConfiguration

| Properties | |
| :--- | :--- |
| [Address](Address.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.Address') | Gets the AMQP server address configuration used for establishing the connection\. This property specifies the endpoint for the AMQP server, which includes details such as the protocol, hostname, port, and credentials if required\. |
| [ExchangeName](ExchangeName.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.ExchangeName') | Gets the name of the exchange used for message publishing in the AMQP protocol\. This property determines the target exchange where messages will be routed for further delivery to the appropriate queues or consumers\. |
| [SenderName](SenderName.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.SenderName') | Specifies the name of the AMQP sender that is used for identifying the producer of messages\. This property allows customization of the sender's identifier, which can be used for logging, debugging, or message tracing purposes\. |
