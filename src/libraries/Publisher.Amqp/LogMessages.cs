namespace Innago.Platform.Messaging.Publisher.Amqp;

using Microsoft.Extensions.Logging;

internal static partial class LogMessages
{
    [LoggerMessage(LogLevel.Error, "{ErrorType}: {Message}")]
    public static partial void Error(this ILogger logger, string errorType, string message);

    [LoggerMessage(LogLevel.Information, "{Message}")]
    public static partial void PublishInformation(this ILogger<Publisher> logger, string message);
}