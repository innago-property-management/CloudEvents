namespace RabbitPoc;

using Microsoft.Extensions.Configuration;

using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

internal static class ProgramConfiguration
{
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