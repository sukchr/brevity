using System;
using System.Diagnostics;
using Common.Logging;

namespace Brevity.Logging
{
    public static class ExceptionExtensions
    {
        public static TException LogError<TException>(this TException exception) where TException : Exception
        {
			return Log(exception, LogLevel.Error);
        }

        public static TException LogWarn<TException>(this TException exception) where TException : Exception
        {
			return Log(exception, LogLevel.Warn);
        }

        public static TException LogFatal<TException>(this TException exception) where TException : Exception
        {
			return Log(exception, LogLevel.Fatal);
        }

        private static TException Log<TException>(this TException exception, LogLevel level) where TException : Exception
        {
            var type = new StackTrace().GetCallingType(typeof(ExceptionExtensions));
            var log = LogManager.GetLogger(type.Name);
	        switch (level)
	        {
				case LogLevel.Error:
					log.Error(exception.Message, exception);
					break;
				case LogLevel.Warn:
					log.Warn(exception.Message, exception);
					break;
				case LogLevel.Fatal:
					log.Fatal(exception.Message, exception);
					break;
	        }
            return exception;
        }
    }
}