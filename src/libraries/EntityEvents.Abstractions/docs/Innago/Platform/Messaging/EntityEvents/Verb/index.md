### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents')

## Verb Enum

Defines the possible actions or operations \(verbs\) that can be performed on an entity\.

```csharp
public enum Verb
```
### Fields

<a name='Innago.Platform.Messaging.EntityEvents.Verb.None'></a>

`None` -1

Represents the absence of a specific action or operation on an entity\.
This verb is used when no particular operation has been performed or when the action is undefined\.

<a name='Innago.Platform.Messaging.EntityEvents.Verb.Create'></a>

`Create` 0

Represents the action of creating a new entity\.
This verb is used to indicate that a new entity has been added to the system,
and an associated event has occurred to reflect this operation\.

<a name='Innago.Platform.Messaging.EntityEvents.Verb.Update'></a>

`Update` 1

Represents the action of modifying an existing entity\.
This verb is used to indicate that changes have been made to an entity's state or attributes,
and an associated event has been triggered to reflect this update\.

<a name='Innago.Platform.Messaging.EntityEvents.Verb.Delete'></a>

`Delete` 2

Represents the action of deleting an existing entity\.
This verb is used to indicate that an entity has been removed
from the system, and an associated event reflects the removal operation\.

<a name='Innago.Platform.Messaging.EntityEvents.Verb.Purge'></a>

`Purge` 3

Represents a verb indicating the complete removal of an entity and its associated data from the system\.
This action is irreversible and denotes permanent deletion beyond basic deletion operations\.