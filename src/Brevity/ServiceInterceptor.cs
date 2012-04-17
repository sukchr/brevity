using System;
using System.Collections.Generic;
using System.Globalization;
using Castle.DynamicProxy;
using log4net;

namespace Brevity
{
    public class ServiceInterceptor<TService> where TService : class
    {
        private readonly TService _wrapped;
        private readonly List<IInterceptor> _interceptors;
        private readonly ProxyGenerator _generator;

        public ServiceInterceptor(TService wrapped)
        {
            _wrapped = wrapped;
            _interceptors = new List<IInterceptor>();
            _generator = new ProxyGenerator();
        }

        //public ServiceInterceptor<TService> LogRequest()
        //{
        //    _interceptors.Add(new LogRequestInterceptor());
        //    return this;
        //}

        //public ServiceInterceptor<TService> LogRequest(Intercept intercept)
        //{
        //    _interceptors.Add(new GenericInterceptor(intercept));
        //    return this;
        //}

        /// <summary>
        /// Returns the service that is intercepted. 
        /// </summary>
        /// <returns></returns>
        public TService Build()
        {
            return _generator.CreateInterfaceProxyWithTarget(_wrapped, _interceptors.ToArray());
        }

        public ServiceInterceptor<TService> LogResponse()
        {
            AddInterceptor(new LogResponseInterceptor());
            return this;
        }

        protected void AddInterceptor(IInterceptor interceptor)
        {
            _interceptors.Add(interceptor);
        }

        #region interceptors
        internal sealed class LogResponseInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                invocation.Proceed();

                var logger = LogManager.GetLogger(invocation.TargetType);

                if (!logger.IsDebugEnabled)
                    return;

                var message = "{0}".FormatWith(invocation.Method.ToString(true, invocation.Arguments));

                if (invocation.Method.ReturnType == typeof(void))
                    message += " -> <void>";
                else if (invocation.ReturnValue == null)
                    message += " -> <null>";
                else if (invocation.ReturnValue is System.IO.Stream)
                    message += invocation.ReturnValue.GetType().Name; //output type of stream 
                else if (invocation.ReturnValue is char)
                    message += @" -> '{0}'".FormatWith(invocation.ReturnValue);
                else if (invocation.ReturnValue.GetType().IsPrimitive)
                    message += @" -> {0}".FormatWith(invocation.ReturnValue);
                else if (invocation.ReturnValue is string) //TODO: StringBuilder?
                    message += @" -> ""{0}""".FormatWith(invocation.ReturnValue);
                else if (invocation.ReturnValue is DateTime) //TODO: StringBuilder?
                    message += string.Format(CultureInfo.CurrentCulture, @" -> ""@{0}""", invocation.ReturnValue);
                //TODO: need to check for array?? 
                else
                {
                    message += "\n===== <response> =====";
                    message += "\n{0}".FormatWith(invocation.ReturnValue.ToJson());
                }

                logger.Debug(message);
            }
        }
        #endregion
    }
}