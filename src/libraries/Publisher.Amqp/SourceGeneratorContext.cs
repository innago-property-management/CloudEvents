namespace Innago.Platform.Messaging.Publisher.Amqp;

using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Uri))]
[JsonSerializable(typeof(DateTimeOffset))]
internal partial class SourceGeneratorContext : JsonSerializerContext;