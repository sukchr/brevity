// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Brevity.Logging;

namespace Brevity.Tests.Exception
{
    [TestFixture]
    public class Log
    {
        [Test]
        public void LogError()
        {
			Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter();

            try
            {
                throw new InvalidOperationException("foo");
            }
            catch (System.Exception ex)
            {
                ex.LogError();
            }
        }
    }
}

// ReSharper restore InconsistentNaming