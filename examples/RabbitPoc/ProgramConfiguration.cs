namespace RabbitPoc;

using System.Net;

using Amqp;

using Innago.Platform.Messaging.Publisher.Amqp;
using Innago.Shared.TryHelpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Prometheus;

internal static class ProgramConfiguration
{
    internal static MetricPusher MetricPusher(string serviceName, IConfiguration configuration)
    {
        return new MetricPusher(new MetricPusherOptions
        {
            Endpoint = configuration["metricsEndpoint"],
            Job = serviceName,
            IntervalMilliseconds = 30_000,
        });
    }

    internal static Func<CancellationToken, Task<HealthCheckResult>> AmqpCheck(HostBuilderContext hostBuilderContext, string serviceName)
    {
        return DoIt;

        async Task<HealthCheckResult> DoIt(CancellationToken cancellationToken)
        {
            using TelemetrySpan span = TracerProvider.Default.GetTracer(serviceName).StartSpan(nameof(AmqpCheck));
            IConfiguration configuration = hostBuilderContext.Configuration;

            string userName = configuration["publisherAmqp:address:user"] ?? "guest";
            string password = configuration["publisherAmqp:address:password"] ?? "guest";
            string? hostName = configuration["publisherAmqp:address:host"];
            string portStr = configuration["publisherAmqp:address:port"] ?? "5672";
            string scheme = configuration["publisherAmqp:address:scheme"] ?? "amqp";
            string? virtualHost = configuration["publisherAmqp:address:virtualHost"];

            string virtualHostPath = string.IsNullOrEmpty(virtualHost) || virtualHost == "/" ? "" : $"/{virtualHost.TrimStart('/')}";
            
            var amqpUriString = $"{scheme}://{WebUtility.UrlEncode(userName)}:{WebUtility.UrlEncode(password)}@{hostName}:{portStr}{virtualHostPath}";

            var amqpLiteConnectionFactory = new ConnectionFactory();
            var address = new Address(amqpUriString);

            Result<Connection?> result = await TryHelpers.TryAsync(() => amqpLiteConnectionFactory.CreateAsync(address)).ConfigureAwait(false);

            HealthCheckResult healthCheckResult = result.Map(OnSuccess, OnError);

            await result.IfSucceededAsync(connection => connection!.CloseAsync()).ConfigureAwait(false);

            return healthCheckResult;
        }
    }

    private static HealthCheckResult OnError(Exception? _)
    {
        return HealthCheckResult.Unhealthy();
    }

    private static HealthCheckResult OnSuccess(Connection? connection)
    {
        HealthCheckResult result = connection switch
        {
            null => HealthCheckResult.Unhealthy(),
            { IsClosed: true } => HealthCheckResult.Unhealthy(),
            { Error: not null } => HealthCheckResult.Unhealthy(),
            _ => HealthCheckResult.Healthy(),
        };

        // TryHelpers.TryAsync(async () => await (connection?.CloseAsync() ?? Task.CompletedTask).ConfigureAwait(false));

        return result;
    }

    internal static Action<MeterProviderBuilder> ConfigureMetrics(IConfiguration configuration, string serviceName)
    {
        return Configure;

        void Configure(MeterProviderBuilder meterProviderBuilder)
        {
            meterProviderBuilder.AddMeter(serviceName);
            meterProviderBuilder.AddOtlpExporter(ConfigureOtlpExporter(configuration));
        }
    }

    internal static Action<ResourceBuilder> ConfigureResource(string serviceName, string serviceVersion)
    {
        return Configure;

        void Configure(ResourceBuilder resourceBuilder)
        {
            resourceBuilder.AddService(serviceName, serviceVersion);
        }
    }

    internal static Action<TracerProviderBuilder> ConfigureTracing(
        IConfiguration configuration,
        string serviceName)
    {
        return Configure;

        void Configure(TracerProviderBuilder tracerProviderBuilder)
        {
            tracerProviderBuilder.AddSource(serviceName);
            tracerProviderBuilder.AddSource(PublisherTracer.Source.Name);

            tracerProviderBuilder.AddOtlpExporter(ConfigureOtlpExporter(configuration));

            if (configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
            {
                tracerProviderBuilder.AddConsoleExporter();
            }
        }
    }

    private static Action<OtlpExporterOptions> ConfigureOtlpExporter(IConfiguration configuration)
    {
        return Configure;

        void Configure(OtlpExporterOptions options)
        {
            options.Protocol = OtlpExportProtocol.HttpProtobuf;
            options.Endpoint = new Uri(configuration["otlpEndpoint"] ?? throw new InvalidOperationException("missing otlp uri"));
        }
    }
}