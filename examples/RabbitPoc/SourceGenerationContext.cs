namespace RabbitPoc;

using System.Text.Json.Serialization;

using CloudNative.CloudEvents;

using Innago.Platform.Messaging.EntityEvents;
using Innago.SiftTypes;

using Transaction = Innago.SiftTypes.Transaction;

[JsonSerializable(typeof(Transaction))]
[JsonSerializable(typeof(Address))]
[JsonSerializable(typeof(Browser))]
[JsonSerializable(typeof(PaymentMethod))]
[JsonSerializable(typeof(IEntityEventInfo<object>))]
[JsonSerializable(typeof(IEntityEventInfo<Account>))]
[JsonSerializable(typeof(IEntityEventInfo<Transaction>))]
[JsonSerializable(typeof(CloudEvent))]
internal partial class SourceGenerationContext : JsonSerializerContext;
