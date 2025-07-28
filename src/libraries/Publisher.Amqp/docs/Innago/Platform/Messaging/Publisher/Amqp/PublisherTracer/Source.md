### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[PublisherTracer](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.PublisherTracer')

## PublisherTracer\.Source Field

Represents an [System\.Diagnostics\.ActivitySource](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.activitysource 'System\.Diagnostics\.ActivitySource') for tracing publisher activities within the AMQP messaging system\.

```csharp
public static readonly ActivitySource Source;
```

#### Field Value
[System\.Diagnostics\.ActivitySource](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.activitysource 'System\.Diagnostics\.ActivitySource')

### Remarks
The [Source](Source.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.PublisherTracer\.Source') is utilized to initiate, manage, and track distributed tracing activities, enabling
proper monitoring and diagnostics of publisher operations\. It integrates with tracing frameworks for enhanced observability\.