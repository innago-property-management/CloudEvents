### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[EntityEventInfo&lt;T&gt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>')

## EntityEventInfo\(string, Verb, string, T, string\) Constructor

Represents information about an event that occurred on a specific entity\.
This record provides details such as the unique identifier for the event, the action performed \(verb\),
the tenant context, and any associated data relevant to the event\.

```csharp
public EntityEventInfo(string Id, Innago.Platform.Messaging.EntityEvents.Verb Verb, string? TenantId=null, T? Data=default(T?), string? UserEmailAddress=null);
```
#### Parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.EntityEventInfo(string,Innago.Platform.Messaging.EntityEvents.Verb,string,T,string).Id'></a>

`Id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.EntityEventInfo(string,Innago.Platform.Messaging.EntityEvents.Verb,string,T,string).Verb'></a>

`Verb` [Innago\.Platform\.Messaging\.EntityEvents\.Verb](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.verb 'Innago\.Platform\.Messaging\.EntityEvents\.Verb')

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.EntityEventInfo(string,Innago.Platform.Messaging.EntityEvents.Verb,string,T,string).TenantId'></a>

`TenantId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.EntityEventInfo(string,Innago.Platform.Messaging.EntityEvents.Verb,string,T,string).Data'></a>

`Data` [T](index.md#Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.T 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.T')

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.EntityEventInfo(string,Innago.Platform.Messaging.EntityEvents.Verb,string,T,string).UserEmailAddress'></a>

`UserEmailAddress` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
