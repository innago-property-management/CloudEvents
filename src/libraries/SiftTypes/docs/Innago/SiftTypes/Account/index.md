### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes')

## Account Class

Represents a user account with all related information used for Sift Science fraud detection\.

```csharp
public record Account : System.IEquatable<Innago.SiftTypes.Account>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; Account

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[Account](index.md 'Innago\.SiftTypes\.Account')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [Account\(string, string, string, string, Browser, Address, PaymentMethod\[\], DateTimeOffset\)](Account(string,string,string,string,Browser,Address,PaymentMethod[],DateTimeOffset).md 'Innago\.SiftTypes\.Account\.Account\(string, string, string, string, Innago\.SiftTypes\.Browser, Innago\.SiftTypes\.Address, Innago\.SiftTypes\.PaymentMethod\[\], System\.DateTimeOffset\)') | Represents a user account with all related information used for Sift Science fraud detection\. |

| Properties | |
| :--- | :--- |
| [Phone](Phone.md 'Innago\.SiftTypes\.Account\.Phone') | Gets the phone number from the billing address associated with this account\. |
| [UnixTime](UnixTime.md 'Innago\.SiftTypes\.Account\.UnixTime') | Gets the timestamp as Unix time in milliseconds\. |
| [UserId](UserId.md 'Innago\.SiftTypes\.Account\.UserId') | Gets the user identifier, which is the same as the user's email address\. |
