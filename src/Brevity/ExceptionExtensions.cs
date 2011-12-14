using System;
using System.Diagnostics;
using log4net;
using log4net.Core;

namespace Brevity
{
    public static class ExceptionExtensions
    {
        public static TException LogError<TException>(this TException exception) where TException : Exception
        {
            return Log(exception, Level.Error);
        }

        public static TException LogWarn<TException>(this TException exception) where TException : Exception
        {
            return Log(exception, Level.Warn);
        }

        public static TException LogFatal<TException>(this TException exception) where TException : Exception
        {
            return Log(exception, Level.Fatal);
        }

        private static TException Log<TException>(this TException exception, Level level) where TException : Exception
        {
            var type = new StackTrace().GetCallingType(typeof(ExceptionExtensions));
            var log = LogManager.GetLogger(type.Name);
            log.Logger.Log(type, level, exception.Message, exception);
            return exception;
        }
    }
}