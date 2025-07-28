### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents')

## EntityEventInfo\<T\> Class

Represents information about an event that occurred on a specific entity\.
This record provides details such as the unique identifier for the event, the action performed \(verb\),
the tenant context, and any associated data relevant to the event\.

```csharp
public record EntityEventInfo<T> : Innago.Platform.Messaging.EntityEvents.IEntityEventInfo<T>, System.IEquatable<Innago.Platform.Messaging.EntityEvents.EntityEventInfo<T>>
```
#### Type parameters

<a name='Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.T'></a>

`T`

The type of the entity data associated with the event\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; EntityEventInfo\<T\>

Implements [Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo&lt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1')[T](index.md#Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.T 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.T')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1'), [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo&lt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>')[T](index.md#Innago.Platform.Messaging.EntityEvents.EntityEventInfo_T_.T 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.T')[&gt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [EntityEventInfo\(string, Verb, string, T, string\)](EntityEventInfo(string,Verb,string,T,string).md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.EntityEventInfo\(string, Innago\.Platform\.Messaging\.EntityEvents\.Verb, string, T, string\)') | Represents information about an event that occurred on a specific entity\. This record provides details such as the unique identifier for the event, the action performed \(verb\), the tenant context, and any associated data relevant to the event\. |

| Properties | |
| :--- | :--- |
| [EntityName](EntityName.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.EntityName') | Gets the fully qualified name of the entity type associated with the event\. Provides a string representation of the entity type, enabling identification of the entity involved in the event\. |
| [Subject](Subject.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>\.Subject') | Gets the subject identifier for the event in a formatted string\. Combines the entity name, the verb \(action\), event ID, and tenant ID to create a unique subject string that can be used for identification or messaging purposes\. |
