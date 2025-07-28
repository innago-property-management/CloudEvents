### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[AmqpConfiguration](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration').[AddressInfo](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.AmqpConfiguration\.AddressInfo')

## AmqpConfiguration\.AddressInfo\.ToAddress\(\) Method

Converts the AddressInfo structure into an AMQP Address object\.

```csharp
public Amqp.Address ToAddress();
```

#### Returns
[Amqp\.Address](https://learn.microsoft.com/en-us/dotnet/api/amqp.address 'Amqp\.Address')  
An instance of the AMQP Address class that encapsulates the configuration details
such as host, port, user credentials, path, and scheme for establishing a connection\.