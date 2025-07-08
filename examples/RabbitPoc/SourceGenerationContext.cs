namespace RabbitPoc;

using System.Text.Json.Serialization;

using Innago.Platform.Messaging.EntityEvents;

[JsonSerializable(typeof(IEntityEventInfo<Wrapper>))]
internal partial class SourceGenerationContext : JsonSerializerContext;