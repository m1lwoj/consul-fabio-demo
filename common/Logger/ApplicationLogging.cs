using Microsoft.Extensions.Logging;

namespace Common
{
    public static class ApplicationLogging
    {
        internal static ILoggerFactory LoggerFactory { get; private set; }
        internal static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
        internal static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);

        public static void RegisterFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }
    }
}