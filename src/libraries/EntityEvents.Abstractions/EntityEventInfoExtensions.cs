namespace Innago.Platform.Messaging.EntityEvents;

using System.Reflection;

using CloudNative.CloudEvents;

/// <summary>
/// Provides extension methods for working with <see cref="IEntityEventInfo{T}"/> instances,
/// enabling transformations into other formats such as CloudEvents.
/// </summary>
public static class EntityEventInfoExtensions
{
    private const string EventType = "innago:entity-event";
    private static readonly string Source = $"urn:innago:{Assembly.GetExecutingAssembly().GetName().Name}";

    /// <summary>
    /// Converts an <see cref="IEntityEventInfo{T}"/> into a <see cref="CloudEvent"/> for standardized
    /// event handling and messaging.
    /// </summary>
    /// <typeparam name="T">The type of the entity data contained in the event.</typeparam>
    /// <param name="entityEventInfo">The source event information to be transformed into a CloudEvent.</param>
    /// <returns>A <see cref="CloudEvent"/> instance containing the transformed details of the source event.</returns>
    public static CloudEvent ToCloudEvent<T>(this IEntityEventInfo<T> entityEventInfo)
    {
        CloudEvent cloudEvent = new()
        {
            Id = Guid.NewGuid().ToString(),
            Source = new Uri(EntityEventInfoExtensions.Source),
            Type = EntityEventInfoExtensions.EventType,
            Data = entityEventInfo,
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