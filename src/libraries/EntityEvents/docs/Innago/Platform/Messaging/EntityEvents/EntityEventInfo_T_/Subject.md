### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[EntityEventInfo&lt;T&gt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.EntityEventInfo\<T\>')

## EntityEventInfo\<T\>\.Subject Property

Gets the subject identifier for the event in a formatted string\.
Combines the entity name, the verb \(action\), event ID, and tenant ID
to create a unique subject string that can be used for identification
or messaging purposes\.

```csharp
public string Subject { get; }
```

Implements [Subject](https://learn.microsoft.com/en-us/dotnet/api/innago.platform.messaging.entityevents.ientityeventinfo-1.subject 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\`1\.Subject')

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
