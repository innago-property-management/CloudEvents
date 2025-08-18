### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[IEntityEventInfo&lt;T&gt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>')

## IEntityEventInfo\<T\>\.Subject Property

Gets the subject identifier for the entity event, representing a unique string
that combines the entity's name, the action performed \(verb\), the event's unique
identifier, and the tenant\-specific context, providing a comprehensive context
for the event\.

```csharp
string Subject { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
