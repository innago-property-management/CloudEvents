### [Innago\.SiftTypes](../index.md 'Innago\.SiftTypes').[Transaction](index.md 'Innago\.SiftTypes\.Transaction')

## Transaction\(decimal, string, string, string, string, string, string, string, string, Browser, Address, PaymentMethod, string, string, string, DateTimeOffset\) Constructor

Represents a financial transaction with all related information used for Sift Science fraud detection\.

```csharp
public Transaction(decimal Amount, string CurrencyCode, string? DeclineCategory, string InvoiceId, string OrganizationId, string SessionId, string TransactionId, string TransactionType, string Ip, Innago.SiftTypes.Browser Browser, Innago.SiftTypes.Address BillingAddress, Innago.SiftTypes.PaymentMethod PaymentMethod, string UserFullName, string UserEmailAddress, string SellerUserId, System.DateTimeOffset Time);
```
#### Parameters

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).Amount'></a>

`Amount` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

The transaction amount in decimal format

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).CurrencyCode'></a>

`CurrencyCode` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The currency code for the transaction \(e\.g\., USD, EUR\)

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).DeclineCategory'></a>

`DeclineCategory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The category of decline if the transaction was declined

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).InvoiceId'></a>

`InvoiceId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier for the invoice associated with this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).OrganizationId'></a>

`OrganizationId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier for the organization associated with this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).SessionId'></a>

`SessionId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier for the user session

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).TransactionId'></a>

`TransactionId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier for this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).TransactionType'></a>

`TransactionType` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The type of transaction \(e\.g\., sale, refund\)

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).Ip'></a>

`Ip` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The IP address from which the transaction was initiated

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).Browser'></a>

`Browser` [Browser](../Browser/index.md 'Innago\.SiftTypes\.Browser')

The browser information associated with this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).BillingAddress'></a>

`BillingAddress` [Address](../Address/index.md 'Innago\.SiftTypes\.Address')

The billing address associated with this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).PaymentMethod'></a>

`PaymentMethod` [PaymentMethod](../PaymentMethod/index.md 'Innago\.SiftTypes\.PaymentMethod')

The payment method used for this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).UserFullName'></a>

`UserFullName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The full name of the user making the transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).UserEmailAddress'></a>

`UserEmailAddress` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The email address of the user making the transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).SellerUserId'></a>

`SellerUserId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The unique identifier of the seller associated with this transaction

<a name='Innago.SiftTypes.Transaction.Transaction(decimal,string,string,string,string,string,string,string,string,Innago.SiftTypes.Browser,Innago.SiftTypes.Address,Innago.SiftTypes.PaymentMethod,string,string,string,System.DateTimeOffset).Time'></a>

`Time` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The timestamp when the transaction occurred
