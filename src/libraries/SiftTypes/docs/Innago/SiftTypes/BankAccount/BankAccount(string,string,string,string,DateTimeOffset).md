### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes').[BankAccount](index.md 'Innago\.SiftTypes\.BankAccount')

## BankAccount\(string, string, string, string, DateTimeOffset\) Constructor

Represents a user BankAccount with all related information used for Sift Science fraud detection\.

```csharp
public BankAccount(string UserId, string BankName, string AccountType, string VerifiedStatus, System.DateTimeOffset Time);
```
#### Parameters

<a name='Innago.SiftTypes.BankAccount.BankAccount(string,string,string,string,System.DateTimeOffset).UserId'></a>

`UserId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The user identifier associated with the bank account\.

<a name='Innago.SiftTypes.BankAccount.BankAccount(string,string,string,string,System.DateTimeOffset).BankName'></a>

`BankName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the bank\.

<a name='Innago.SiftTypes.BankAccount.BankAccount(string,string,string,string,System.DateTimeOffset).AccountType'></a>

`AccountType` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The type of the bank account \(e\.g\., checking, savings\)\.

<a name='Innago.SiftTypes.BankAccount.BankAccount(string,string,string,string,System.DateTimeOffset).VerifiedStatus'></a>

`VerifiedStatus` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The verification status of the bank account\.

<a name='Innago.SiftTypes.BankAccount.BankAccount(string,string,string,string,System.DateTimeOffset).Time'></a>

`Time` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The timestamp of the bank account information\.