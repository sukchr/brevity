using System;
using System.Text;

namespace Brevity
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/>
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Returns the message of the exception and all inner exceptions.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetAllMessages(this Exception exception)
        {
            if (exception.InnerException == null)
                return string.Format("{0}: {1}", exception.GetType().FullName, exception.Message);

            var message = new StringBuilder();
            message.Append(exception.GetType().FullName);
            message.Append(": ");
            message.Append(exception.Message);

            exception = exception.InnerException;

            do
            {
                message.Append(" ---> ");
                message.Append(exception.GetType().FullName);
                message.Append(": ");
                message.Append(exception.Message);

                exception = exception.InnerException;
            } while (exception != null);

            return message.ToString();
        }
    }
}