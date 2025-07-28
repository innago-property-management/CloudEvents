### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes')

## Transaction Class

Represents a financial transaction with all related information used for Sift Science fraud detection\.

```csharp
public record Transaction : System.IEquatable<Innago.SiftTypes.Transaction>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; Transaction

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[Transaction](index.md 'Innago\.SiftTypes\.Transaction')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Constructors | |
| :--- | :--- |
| [Transaction\(decimal, string, string, string, string, string, string, string, string, Browser, Address, PaymentMethod, string, string, string, DateTimeOffset\)](Transaction(decimal,string,string,string,string,string,string,string,string,Browser,Address,PaymentMethod,string,string,string,DateTimeOffset).md 'Innago\.SiftTypes\.Transaction\.Transaction\(decimal, string, string, string, string, string, string, string, string, Innago\.SiftTypes\.Browser, Innago\.SiftTypes\.Address, Innago\.SiftTypes\.PaymentMethod, string, string, string, System\.DateTimeOffset\)') | Represents a financial transaction with all related information used for Sift Science fraud detection\. |

| Properties | |
| :--- | :--- |
| [Amount](Amount.md 'Innago\.SiftTypes\.Transaction\.Amount') | The transaction amount in decimal format |
| [AmountForSift](AmountForSift.md 'Innago\.SiftTypes\.Transaction\.AmountForSift') | Represents the amount of the transaction scaled and converted for Sift integration\. |
| [BillingAddress](BillingAddress.md 'Innago\.SiftTypes\.Transaction\.BillingAddress') | The billing address associated with this transaction |
| [Browser](Browser.md 'Innago\.SiftTypes\.Transaction\.Browser') | The browser information associated with this transaction |
| [CurrencyCode](CurrencyCode.md 'Innago\.SiftTypes\.Transaction\.CurrencyCode') | The currency code for the transaction \(e\.g\., USD, EUR\) |
| [DeclineCategory](DeclineCategory.md 'Innago\.SiftTypes\.Transaction\.DeclineCategory') | The category of decline if the transaction was declined |
| [InvoiceId](InvoiceId.md 'Innago\.SiftTypes\.Transaction\.InvoiceId') | The unique identifier for the invoice associated with this transaction |
| [Ip](Ip.md 'Innago\.SiftTypes\.Transaction\.Ip') | The IP address from which the transaction was initiated |
| [OrganizationId](OrganizationId.md 'Innago\.SiftTypes\.Transaction\.OrganizationId') | The unique identifier for the organization associated with this transaction |
| [PaymentMethod](PaymentMethod.md 'Innago\.SiftTypes\.Transaction\.PaymentMethod') | The payment method used for this transaction |
| [SellerUserId](SellerUserId.md 'Innago\.SiftTypes\.Transaction\.SellerUserId') | The unique identifier of the seller associated with this transaction |
| [SessionId](SessionId.md 'Innago\.SiftTypes\.Transaction\.SessionId') | The unique identifier for the user session |
| [Time](Time.md 'Innago\.SiftTypes\.Transaction\.Time') | The timestamp when the transaction occurred |
| [TransactionId](TransactionId.md 'Innago\.SiftTypes\.Transaction\.TransactionId') | The unique identifier for this transaction |
| [TransactionType](TransactionType.md 'Innago\.SiftTypes\.Transaction\.TransactionType') | The type of transaction \(e\.g\., sale, refund\) |
| [UnixTime](UnixTime.md 'Innago\.SiftTypes\.Transaction\.UnixTime') | Represents the timestamp of the transaction in Unix time, expressed in milliseconds\. |
| [UserEmailAddress](UserEmailAddress.md 'Innago\.SiftTypes\.Transaction\.UserEmailAddress') | The email address of the user making the transaction |
| [UserFullName](UserFullName.md 'Innago\.SiftTypes\.Transaction\.UserFullName') | The full name of the user making the transaction |
| [UserId](UserId.md 'Innago\.SiftTypes\.Transaction\.UserId') | Represents the identifier of the user associated with the transaction\. |
| [UserPhone](UserPhone.md 'Innago\.SiftTypes\.Transaction\.UserPhone') | Represents the phone number associated with the user initiating the transaction\. |
