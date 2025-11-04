### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes')

## BankAccount Class

Represents a user BankAccount with all related information used for Sift Science fraud detection\.

```csharp
public record BankAccount : System.IEquatable<Innago.SiftTypes.BankAccount>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; BankAccount

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[BankAccount](index.md 'Innago\.SiftTypes\.BankAccount')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [BankAccount\(string, string, string, string, DateTimeOffset\)](BankAccount(string,string,string,string,DateTimeOffset).md 'Innago\.SiftTypes\.BankAccount\.BankAccount\(string, string, string, string, System\.DateTimeOffset\)') | Represents a user BankAccount with all related information used for Sift Science fraud detection\. |

| Properties | |
| :--- | :--- |
| [AccountType](AccountType.md 'Innago\.SiftTypes\.BankAccount\.AccountType') | The type of the bank account \(e\.g\., checking, savings\)\. |
| [BankName](BankName.md 'Innago\.SiftTypes\.BankAccount\.BankName') | The name of the bank\. |
| [Time](Time.md 'Innago\.SiftTypes\.BankAccount\.Time') | The timestamp of the bank account information\. |
| [UnixTime](UnixTime.md 'Innago\.SiftTypes\.BankAccount\.UnixTime') | Gets the timestamp as Unix time in milliseconds\. |
| [UserId](UserId.md 'Innago\.SiftTypes\.BankAccount\.UserId') | The user identifier associated with the bank account\. |
| [VerifiedStatus](VerifiedStatus.md 'Innago\.SiftTypes\.BankAccount\.VerifiedStatus') | The verification status of the bank account\. |
