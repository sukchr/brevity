using System;
using System.Globalization;
using System.Threading;

namespace Brevity
{
    public class Culture : IDisposable
    {
        private CultureInfo _previousCulture;
        
        /// <summary>
        /// Sets the given culture into the current thread.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static Culture Scope(string culture)
        {
            if (culture == null) throw new ArgumentNullException("culture");
            var response = new Culture { _previousCulture = Thread.CurrentThread.CurrentCulture };
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            return response;
        }
        
        /// <summary>
        /// Sets the previous culture into the current thread.
        /// </summary>
        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _previousCulture;
        }
    }
}