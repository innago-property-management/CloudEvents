### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[AmqpConfiguration](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration')

## AmqpConfiguration\.SenderName Property

Specifies the name of the AMQP sender that is used for identifying the producer of messages\.
This property allows customization of the sender's identifier, which can be used for logging,
debugging, or message tracing purposes\.

```csharp
public string? SenderName { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')