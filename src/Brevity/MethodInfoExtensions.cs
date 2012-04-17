using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Brevity
{
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Outputs the method and its arguments. E.g. Foo('bar',123,null).
        /// Use the overloaded version if you want to see parameter names as well. 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static string ToString(this MethodInfo method, object[] arguments)
        {
            return method.ToString(false, arguments);
        }

        /// <summary>
        /// Outputs the method and its arguments. E.g. Foo('bar',123,null).
        /// </summary>
        /// <param name="method"></param>
        /// <param name="outputParameterNames">Set to true if you want the parameter name to be output with the arguments.</param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static string ToString(this MethodInfo method, bool outputParameterNames, object[] arguments)
        {
            if (method == null) throw new ArgumentNullException("method");

            var parameters = method.GetParameters();

            var args = string.Empty;
            var nonPrimitiveArguments = new List<int>();

            if (arguments != null && arguments.Length > 0)
            {
                var list = new List<string>(arguments.Select(
                    (argument, index) =>
                    {
                        //TODO: support IEnumerable, "{1,2,3}"

                        string value;
                        if (argument == null) value = "<null>";
                        else
                        {
                            var stringValue = argument as string;

                            if (stringValue != null)
                                value = @"""{0}""".FormatWith(stringValue);
                            else if(argument is DateTime)
                            {
                                var dateTime = (DateTime)argument;
                                value = string.Format("@{0}", dateTime.ToString(CultureInfo.CurrentCulture));
                            }
                            else if (argument.GetType().IsPrimitive)
                                value = argument.ToString();
                            else
                            {
                                nonPrimitiveArguments.Add(index);
                                value = "<{0}>".FormatWith(nonPrimitiveArguments.Count);
                            }
                        }

                        if (outputParameterNames) value = "{0}={1}".FormatWith(parameters[index].Name, value);

                        return value;
                    }));
                args = string.Join(",", list.ConvertAll(Convert.ToString).ToArray());
            }

            var message = new StringBuilder("{0}({1})".FormatWith(method.Name, args));
            for(var i = 0 ; i < nonPrimitiveArguments.Count ; i++)
            {
                message.AppendLine("\n===== <{0}> =====".FormatWith(i + 1));
                message.AppendLine(arguments[nonPrimitiveArguments[i]].ToJson());
            }

            return message.ToString();
        }
    }
}