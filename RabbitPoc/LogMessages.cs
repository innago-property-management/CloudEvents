namespace RabbitPoc;

using Microsoft.Extensions.Logging;

internal static partial class LogMessages
{
    [LoggerMessage(LogLevel.Information, "{Message}")]
    public static partial void PublishInformation(this ILogger<Publisher> logger, string message);
}