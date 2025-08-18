### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[EntityWithIdExtensions](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions')

## EntityWithIdExtensions\.ToDeleteEntityEventInfo\<TId\>\(this IEntityWithId\<TId\>, string, string\) Method

Converts the entity into an IEntityEventInfo instance to represent a Delete event\.

```csharp
public static Innago.Platform.Messaging.EntityEvents.IEntityEventInfo<Innago.Platform.Messaging.EntityEvents.IEntityWithId<TId>> ToDeleteEntityEventInfo<TId>(this Innago.Platform.Messaging.EntityEvents.IEntityWithId<TId> entity, string? tenantId=null, string? userEmailAddress=null);
```
#### Type parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).TId'></a>

`TId`

The type of the entity's identifier\.
#### Parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).entity'></a>

`entity` [Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[TId](ToDeleteEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md#Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).TId 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToDeleteEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)\.TId')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')

The entity to be converted into the event information\.

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).tenantId'></a>

`tenantId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The tenant identifier associated with the entity, if applicable\.

<a name='Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).userEmailAddress'></a>

`userEmailAddress` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The email address of the user who initiated the event, if applicable\.

#### Returns
[Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1')[Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[TId](ToDeleteEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md#Innago.Platform.Messaging.EntityEvents.EntityWithIdExtensions.ToDeleteEntityEventInfo_TId_(thisInnago.Platform.Messaging.EntityEvents.IEntityWithId_TId_,string,string).TId 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToDeleteEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)\.TId')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientitywithid-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1')  
An instance of IEntityEventInfo encapsulating the given entity and Delete event details\.
