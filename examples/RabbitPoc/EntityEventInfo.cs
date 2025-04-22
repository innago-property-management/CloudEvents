namespace RabbitPoc;

using JetBrains.Annotations;

[PublicAPI]
internal record EntityEventInfo<T>(string Id, Verb Verb, string? TenantId = null, T? Data = default)
{
    public string Subject => $"{this.EntityName}:{this.Verb}:{this.Id}:{this.TenantId}";

    public string EntityName => $"{typeof(T).FullName}";
}