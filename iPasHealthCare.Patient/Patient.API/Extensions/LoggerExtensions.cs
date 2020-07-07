using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Patient.API.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogAppError<T>(this ILogger<T> logger, EventId eventId, Exception exception, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using (var prop = LogContext.PushProperty("MemberName", memberName))
            {
                LogContext.PushProperty("FilePath", sourceFilePath);
                LogContext.PushProperty("LineNumber", sourceLineNumber);
                logger.LogError(eventId, exception, message);
            }
        }
    }
}
