### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents')

## EntityWithIdExtensions Class

Provides extension methods for converting entities implementing IEntityWithId\<TId\> into IEntityEventInfo\<T\>
instances\. These methods assist in creating events associated with specific actions, such as create, update, delete,
and purge, while also enabling the inclusion of optional tenant details\.

```csharp
public static class EntityWithIdExtensions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; EntityWithIdExtensions

| Methods | |
| :--- | :--- |
| [ToCreateEntityEventInfo&lt;TId&gt;\(this IEntityWithId&lt;TId&gt;, string, string\)](ToCreateEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToCreateEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)') | Converts the entity into an IEntityEventInfo instance to represent a Create event\. |
| [ToDeleteEntityEventInfo&lt;TId&gt;\(this IEntityWithId&lt;TId&gt;, string, string\)](ToDeleteEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToDeleteEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)') | Converts the entity into an IEntityEventInfo instance to represent a Delete event\. |
| [ToEntityEventInfo&lt;TId&gt;\(this IEntityWithId&lt;TId&gt;, Verb, string, string\)](ToEntityEventInfo_TId_(thisIEntityWithId_TId_,Verb,string,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, Innago\.Platform\.Messaging\.EntityEvents\.Verb, string, string\)') | Converts the entity into an IEntityEventInfo instance with the specified action verb\. |
| [ToPurgeEntityEventInfo&lt;TId&gt;\(this IEntityWithId&lt;TId&gt;, string, string\)](ToPurgeEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToPurgeEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)') | Converts the entity into an IEntityEventInfo instance to represent a Purge event\. |
| [ToUpdateEntityEventInfo&lt;TId&gt;\(this IEntityWithId&lt;TId&gt;, string, string\)](ToUpdateEntityEventInfo_TId_(thisIEntityWithId_TId_,string,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions\.ToUpdateEntityEventInfo\<TId\>\(this Innago\.Platform\.Messaging\.EntityEvents\.IEntityWithId\<TId\>, string, string\)') | Converts the entity into an IEntityEventInfo instance to represent an Update event\. |
