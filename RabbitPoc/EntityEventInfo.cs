namespace RabbitPoc;

internal record EntityEventInfo(string Name, string Id, Verb Verb, string? TenantId = null);