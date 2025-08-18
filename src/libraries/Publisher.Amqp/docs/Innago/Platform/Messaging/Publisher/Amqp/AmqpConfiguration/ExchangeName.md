### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[AmqpConfiguration](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration')

## AmqpConfiguration\.ExchangeName Property

Gets the name of the exchange used for message publishing in the AMQP protocol\.
This property determines the target exchange where messages will be routed
for further delivery to the appropriate queues or consumers\.

```csharp
public string ExchangeName { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
