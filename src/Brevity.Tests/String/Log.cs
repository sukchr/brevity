// ReSharper disable InconsistentNaming

using Brevity.Logging;
using NUnit.Framework;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Log
    {
        [Test]
        public void LogInfo()
        {
			Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter();
            "foo".LogInfo();
        }

        [Test]
        public void LogDebug()
        {
			Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter();
            "foo {0}".LogDebug("bar");
        }
    }
}

// ReSharper restore InconsistentNaming