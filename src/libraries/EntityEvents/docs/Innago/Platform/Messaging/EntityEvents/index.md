## Innago\.Platform\.Messaging\.EntityEvents Namespace

| Classes | |
| :--- | :--- |
| [EntityEventInfo&lt;T&gt;](EntityEventInfo_T_/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>') | Represents information about an event that occurred on a specific entity\. This record provides details such as the unique identifier for the event, the action performed \(verb\), the tenant context, and any associated data relevant to the event\. |
| [EntityWithIdExtensions](EntityWithIdExtensions/index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityWithIdExtensions') | Provides extension methods for converting entities implementing IEntityWithId\<TId\> into IEntityEventInfo\<T\> instances\. These methods assist in creating events associated with specific actions, such as create, update, delete, and purge, while also enabling the inclusion of optional tenant details\. |
