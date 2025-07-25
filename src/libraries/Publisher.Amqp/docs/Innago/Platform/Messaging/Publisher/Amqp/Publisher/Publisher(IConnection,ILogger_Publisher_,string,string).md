### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[Publisher](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher')

## Publisher\(IConnection, ILogger\<Publisher\>, string, string\) Constructor

Represents a publisher for sending CloudEvent messages to an AMQP\-based messaging system\.
Encapsulates the logic for establishing a session, creating sender links, and publishing events\.

```csharp
public Publisher(Amqp.IConnection connection, Microsoft.Extensions.Logging.ILogger<Innago.Platform.Messaging.Publisher.Amqp.Publisher> logger, string address, string senderName);
```
#### Parameters

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnection,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,string,string).connection'></a>

`connection` [Amqp\.IConnection](https://learn.microsoft.com/en-us/dotnet/api/amqp.iconnection 'Amqp\.IConnection')

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnection,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,string,string).logger'></a>

`logger` [Microsoft\.Extensions\.Logging\.ILogger&lt;](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1 'Microsoft\.Extensions\.Logging\.ILogger\`1')[Publisher](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.Publisher')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1 'Microsoft\.Extensions\.Logging\.ILogger\`1')

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnection,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,string,string).address'></a>

`address` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Innago.Platform.Messaging.Publisher.Amqp.Publisher.Publisher(Amqp.IConnection,Microsoft.Extensions.Logging.ILogger_Innago.Platform.Messaging.Publisher.Amqp.Publisher_,string,string).senderName'></a>

`senderName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')