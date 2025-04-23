namespace RabbitPoc;

using Microsoft.Extensions.Logging;

internal static partial class LogMessages
{
    [LoggerMessage(LogLevel.Error, "{ErrorType}: {Message}")]
    public static partial void Error(this ILogger logger, string errorType, string message);
}