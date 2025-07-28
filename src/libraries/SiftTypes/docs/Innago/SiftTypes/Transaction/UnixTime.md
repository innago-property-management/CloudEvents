### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes').[Transaction](index.md 'Innago\.SiftTypes\.Transaction')

## Transaction\.UnixTime Property

Represents the timestamp of the transaction in Unix time, expressed in milliseconds\.

```csharp
public long UnixTime { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

### Remarks
The value is computed from the transaction's [Time](Time.md 'Innago\.SiftTypes\.Transaction\.Time') property using the
`ToUnixTimeMilliseconds` method, converting the time to the Unix epoch format\.