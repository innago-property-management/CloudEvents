### [Innago\.Platform\.Messaging\.EntityEvents](../index.md 'Innago\.Platform\.Messaging\.EntityEvents').[IEntityEventInfo&lt;T&gt;](index.md 'Innago\.Platform\.Messaging\.EntityEvents\.IEntityEventInfo\<T\>')

## IEntityEventInfo\<T\>\.TenantId Property

Gets the identifier representing the tenant associated with the entity event\.
This provides context for the specific tenant on which the event occurred\.
The value may be null to indicate a tenant\-agnostic event\.

```csharp
string? TenantId { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')