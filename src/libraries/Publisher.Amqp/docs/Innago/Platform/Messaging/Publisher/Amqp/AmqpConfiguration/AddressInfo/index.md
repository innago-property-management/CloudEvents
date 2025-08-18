### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[AmqpConfiguration](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration')

## AmqpConfiguration\.AddressInfo Struct

Represents connection details required to establish communication
with a server or broker, including host, port, credentials, and scheme\.

```csharp
public record struct AmqpConfiguration.AddressInfo : System.IEquatable<Innago.Platform.Messaging.Publisher.Amqp.AmqpConfiguration.AddressInfo>
```

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[AddressInfo](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [AddressInfo\(string, int, string, string, string, string\)](AddressInfo(string,int,string,string,string,string).md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.AddressInfo\(string, int, string, string, string, string\)') | Represents connection details required to establish communication with a server or broker, including host, port, credentials, and scheme\. |

| Properties | |
| :--- | :--- |
| [Host](Host.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.Host') | |
| [Password](Password.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.Password') | |
| [Path](Path.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.Path') | |
| [Port](Port.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.Port') | |
| [Scheme](Scheme.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.Scheme') | |
| [User](User.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.User') | |

| Methods | |
| :--- | :--- |
| [ToAddress\(\)](ToAddress().md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo\.ToAddress\(\)') | Converts the AddressInfo structure into an AMQP Address object\. |
