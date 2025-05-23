namespace Innago.Platform.Messaging.Publisher.Amqp;

using System.Text.Json.Serialization;

using EntityEvents;

[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Uri))]
[JsonSerializable(typeof(DateTimeOffset))]
internal partial class SourceGeneratorContext : JsonSerializerContext;