using System.Diagnostics;
using log4net;
using log4net.Core;

namespace Brevity.Logging
{
    public static class StringExtensions
    {
        public static string LogInfo(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Info);
        }

        public static string LogInfo(this string message)
        {
            return Log(message, Level.Info);
        }

        public static string LogDebug(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Debug);
        }

        public static string LogDebug(this string message)
        {
            return Log(message, Level.Debug);
        }

        public static string LogWarn(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Warn);
        }

        public static string LogWarn(this string message)
        {
            return Log(message, Level.Warn);
        }

        public static string LogError(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Error);
        }

        public static string LogError(this string message)
        {
            return Log(message, Level.Error);
        }

        public static string LogFatal(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Fatal);
        }

        public static string LogFatal(this string message)
        {
            return Log(message, Level.Fatal);
        }

        private static string Log(this string message, Level level)
        {
            var type = new StackTrace().GetCallingType(typeof(StringExtensions));
            var log = LogManager.GetLogger(type.FullName);
            log.Logger.Log(type, level, message, null);
            return message;
        }
    }
}