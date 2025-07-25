### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents')

## IEntityEventInfo\<T\> Interface

Defines a contract for entity event information, providing details about an event
that occurred on an entity, such as its unique identifier, action performed \(verb\),
tenant\-specific context, related data, and more\.

```csharp
public interface IEntityEventInfo<out T>
```
#### Type parameters

<a name='Innago.Platform.Messaging.EntityEvents.IEntityEventInfo_T_.T'></a>

`T`

Represents the type of the data associated with the entity event\.

| Properties | |
| :--- | :--- |
| [Data](Data.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.Data') | Gets the data associated with the entity event\. This represents the specific information tied to the context of the event\. |
| [EntityName](EntityName.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.EntityName') | Gets the name of the entity associated with the event\. This property represents the fully qualified type name of the entity\. |
| [Id](Id.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.Id') | Gets the unique identifier associated with the entity event\. This identifier represents the specific instance of the event\. |
| [Subject](Subject.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.Subject') | Gets the subject identifier for the entity event, representing a unique string that combines the entity's name, the action performed \(verb\), the event's unique identifier, and the tenant\-specific context, providing a comprehensive context for the event\. |
| [TenantId](TenantId.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.TenantId') | Gets the identifier representing the tenant associated with the entity event\. This provides context for the specific tenant on which the event occurred\. The value may be null to indicate a tenant\-agnostic event\. |
| [Timestamp](Timestamp.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.Timestamp') | Gets the timestamp indicating when the entity event occurred\. Represents the date and time of the specific event instance\. |
| [TracingId](TracingId.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.TracingId') | Gets the tracing identifier associated with the event\. This identifier is used for tracking and correlating events across distributed systems or processes\. |
| [UserEmailAddress](UserEmailAddress.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.UserEmailAddress') | Gets the email address associated with the user who performed the action related to the entity event\. This information helps identify the user initiating the event\. |
| [Verb](Verb.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>\.Verb') | Gets the action or operation \(verb\) performed on the entity within the context of the event\. This value represents whether the entity was created, updated, or deleted\. |
