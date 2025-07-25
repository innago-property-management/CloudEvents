### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes').[Transaction](index.md 'Innago\.SiftTypes\.Transaction')

## Transaction\.AmountForSift Property

Represents the amount of the transaction scaled and converted for Sift integration\.

```csharp
public long AmountForSift { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

### Remarks
The value is derived by multiplying the original amount by 10,000 and 100 to meet Sift's expected format\.