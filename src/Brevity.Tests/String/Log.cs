// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Log
    {
        [Test]
        public void LogInfo()
        {
            log4net.Config.BasicConfigurator.Configure();
            "foo".LogInfo();
        }

        [Test]
        public void LogDebug()
        {
            log4net.Config.BasicConfigurator.Configure();
            "foo {0}".LogDebug("bar");
        }
    }
}

// ReSharper restore InconsistentNaming