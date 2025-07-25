### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[EntityWithIdExtensions](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions')

## EntityWithIdExtensions\.ToEntityEventInfo\<TId\>\(this IEntityWithId\<TId\>, Verb, string, string\) Method

Converts the entity into an IEntityEventInfo instance with the specified action verb\.

```csharp
public static Innago.Platform.Messaging.EntityEvents.IEntityEventInfo<Innago.Platform.Messaging.EntityEvents.IEntityWithId<TId>> ToEntityEventInfo<TId>(this Innago.Platform.Messaging.EntityEvents.IEntityWithId<TId> entity, Innago.Platform.Messaging.EntityEvents.Verb verb, string? tenantId=null, string? userEmailAddress=null);
```
#### Type parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).TId'></a>

`TId`

The type of the entity's identifier\.
#### Parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).entity'></a>

`entity` [Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[TId](ToEntityEventInfo_TId_(thisIEntityWithId_TId_,Verb,string,string).md#Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).TId 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, Innago\.Platform\.Messaging\.EntityEvents\.Verb, string, string\)\.TId')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')

The entity to be converted into the event information\.

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).verb'></a>

`verb` [Innago\.Platform\.Messaging\.EntityEvents\.Verb](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.verb 'Innago\.Platform\.Messaging\.EntityEvents\.Verb')

The action verb that represents the type of event \(e\.g\., Create, Update, Delete\)\.

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).tenantId'></a>

`tenantId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The tenant identifier associated with the entity, if applicable\.

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).userEmailAddress'></a>

`userEmailAddress` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The email address of the user who initiated the event, if applicable\.

#### Returns
[Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1')[Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[TId](ToEntityEventInfo_TId_(thisIEntityWithId_TId_,Verb,string,string).md#Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,Innago.Platform.Messaging.EntityEvents.Verb,string,string).TId 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, Innago\.Platform\.Messaging\.EntityEvents\.Verb, string, string\)\.TId')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1')  
An instance of IEntityEventInfo encapsulating the given entity, the specified action verb, and tenant details\.

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown when the entity's ID is null\.