using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Brevity
{
    internal static class StackTraceExtensions
    {
        public static Type GetCallingType(this StackTrace stackTrace, params Type[] skipTypes)
        {
            StackFrame frame;
            MethodBase method;
            var list = new List<Type>(skipTypes);

            //find first call outside of StringExtensions
            for (var i = 0; i < stackTrace.FrameCount; i++)
            {
                frame = stackTrace.GetFrame(i);
                method = frame.GetMethod();
                if (method.DeclaringType != typeof(StackTraceExtensions) && !list.Contains(method.DeclaringType))
                    return method.DeclaringType;
            }

            return null;
        }
    }
}