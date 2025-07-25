### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents')

## EntityEventInfoExtensions Class

Provides extension methods for working with [IEntityEventInfo&lt;T&gt;](../IEntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>') instances,
enabling transformations into other formats such as CloudEvents\.

```csharp
public static class EntityEventInfoExtensions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; EntityEventInfoExtensions

| Methods | |
| :--- | :--- |
| [ToCloudEvent&lt;T&gt;\(this IEntityEventInfo&lt;T&gt;\)](ToCloudEvent_T_(thisIEntityEventInfo_T_).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfoExtensions\.ToCloudEvent\<T\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\)') | Converts an [IEntityEventInfo&lt;T&gt;](../IEntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>') into a [CloudNative\.CloudEvents\.CloudEvent](https://learn.microsoft.com/en-us/dotnet/api/cloudnative.cloudevents.cloudevent 'CloudNative\.CloudEvents\.CloudEvent') for standardized event handling and messaging\. |
