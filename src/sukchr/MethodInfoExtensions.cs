using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace sukchr
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
            if (arguments != null && arguments.Length > 0)
            {
                var list = new List<string>(arguments.Select(
                    (argument, index) =>
                    {
                        //TODO: support IEnumerable, "{1,2,3}"

                        string value;
                        if (argument == null) value = "null";
                        else
                        {
                            var stringValue = argument as string;
                            value = stringValue != null ? "'{0}'".FormatWith(stringValue) : argument.ToString();
                        }

                        if (outputParameterNames) value = "{0}={1}".FormatWith(parameters[index].Name, value);

                        return value;
                    }));
                args = string.Join(",", list.ConvertAll(Convert.ToString).ToArray());
            }

            return "{0}({1})".FormatWith(method.Name, args);
        }
    }
}