### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[EntityEventInfoExtensions](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfoExtensions')

## EntityEventInfoExtensions\.ToCloudEvent\<T\>\(this IEntityEventInfo\<T\>\) Method

Converts an [IEntityEventInfo&lt;T&gt;](../IEntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>') into a [CloudNative\.CloudEvents\.CloudEvent](https://learn.microsoft.com/en-us/dotnet/api/cloudnative.cloudevents.cloudevent 'CloudNative\.CloudEvents\.CloudEvent') for standardized
event handling and messaging\.

```csharp
public static CloudNative.CloudEvents.CloudEvent ToCloudEvent<T>(this Innago.Platform.Messaging.EntityEvents.IEntityEventInfo<T> entityEventInfo);
```
#### Type parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfoExtensions.ToCloudEvent_T_(thisInnago.Platform.Messaging.EntityEvents.IEntityEventInfo_T_).T'></a>

`T`

The type of the entity data contained in the event\.
#### Parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfoExtensions.ToCloudEvent_T_(thisInnago.Platform.Messaging.EntityEvents.IEntityEventInfo_T_).entityEventInfo'></a>

`entityEventInfo` [Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo&lt;](../IEntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>')[T](ToCloudEvent_T_(thisIEntityEventInfo_T_).md#Innago.Platform.Messaging.EntityEvents.EntityEventInfoExtensions.ToCloudEvent_T_(thisInnago.Platform.Messaging.EntityEvents.IEntityEventInfo_T_).T 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfoExtensions\.ToCloudEvent\<T\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\)\.T')[&gt;](../IEntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>')

The source event information to be transformed into a CloudEvent\.

#### Returns
[CloudNative\.CloudEvents\.CloudEvent](https://learn.microsoft.com/en-us/dotnet/api/cloudnative.cloudevents.cloudevent 'CloudNative\.CloudEvents\.CloudEvent')  
A [CloudNative\.CloudEvents\.CloudEvent](https://learn.microsoft.com/en-us/dotnet/api/cloudnative.cloudevents.cloudevent 'CloudNative\.CloudEvents\.CloudEvent') instance containing the transformed details of the source event\.
