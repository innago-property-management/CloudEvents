namespace Innago.Platform.Messaging.Publisher.Amqp;

using System.Diagnostics;

/// <summary>
/// Provides tracing functionality for AMQP-based publishers.
/// </summary>
/// <remarks>
/// This class defines an activity source that can be used to create and manage tracing activities
/// for monitoring and diagnosing publisher behavior in an AMQP messaging system. It allows for
/// integration with distributed tracing systems.
/// </remarks>
public static class PublisherTracer
{
    /// <summary>
    /// Represents an <see cref="ActivitySource"/> for tracing publisher activities within the AMQP messaging system.
    /// </summary>
    /// <remarks>
    /// The <see cref="Source"/> is utilized to initiate, manage, and track distributed tracing activities, enabling
    /// proper monitoring and diagnostics of publisher operations. It integrates with tracing frameworks for enhanced observability.
    /// </remarks>
    public static readonly ActivitySource Source = new(typeof(PublisherTracer).FullName!);
}