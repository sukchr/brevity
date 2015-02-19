using System.Diagnostics;
using Common.Logging;

namespace Brevity.Logging
{
    public static class StringExtensions
    {
        public static string LogInfo(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), LogLevel.Info);
        }

        public static string LogInfo(this string message)
        {
            return Log(message, LogLevel.Info);
        }

        public static string LogDebug(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), LogLevel.Debug);
        }

        public static string LogDebug(this string message)
        {
            return Log(message, LogLevel.Debug);
        }

        public static string LogWarn(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), LogLevel.Warn);
        }

        public static string LogWarn(this string message)
        {
            return Log(message, LogLevel.Warn);
        }

        public static string LogError(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), LogLevel.Error);
        }

        public static string LogError(this string message)
        {
            return Log(message, LogLevel.Error);
        }

        public static string LogFatal(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), LogLevel.Fatal);
        }

        public static string LogFatal(this string message)
        {
            return Log(message, LogLevel.Fatal);
        }

        private static string Log(this string message, LogLevel level)
        {
            var type = new StackTrace().GetCallingType(typeof(StringExtensions));
            var log = LogManager.GetLogger(type.FullName);
			switch (level)
			{
				case LogLevel.Debug:
					log.Error(message);
					break;
				case LogLevel.Info:
					log.Error(message);
					break;
				case LogLevel.Error:
					log.Error(message);
					break;
				case LogLevel.Warn:
					log.Error(message);
					break;
				case LogLevel.Fatal:
					log.Error(message);
					break;
			}

            return message;
        }
    }
}