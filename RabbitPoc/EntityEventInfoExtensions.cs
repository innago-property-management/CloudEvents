namespace RabbitPoc;

using System.Reflection;

using CloudNative.CloudEvents;

internal static class EntityEventInfoExtensions
{
    private const string EventType = "innago:entity-event";
    private static readonly string Source = $"urn:innago:{Assembly.GetExecutingAssembly().GetName().Name}";

    public static CloudEvent ToCloudEvent<T>(this EntityEventInfo<T> entityEventInfo)
    {
        CloudEvent cloudEvent = new()
        {
            Id = Guid.NewGuid().ToString(),
            Source = new Uri(EntityEventInfoExtensions.Source),
            Type = EntityEventInfoExtensions.EventType,
            Data = entityEventInfo.Data,
            Subject = entityEventInfo.Subject,
            Time = DateTimeOffset.UtcNow,
        };

        cloudEvent.SetAttributeFromString("entityname", entityEventInfo.EntityName);
        cloudEvent.SetAttributeFromString("entityid", entityEventInfo.Id);
        cloudEvent.SetAttributeFromString("tenantid", entityEventInfo.TenantId ?? string.Empty);
        cloudEvent.SetAttributeFromString("entityaction", $"{entityEventInfo.Verb}");

        return cloudEvent;
    }
}