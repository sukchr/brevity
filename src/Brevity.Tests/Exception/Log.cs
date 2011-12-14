// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;

namespace Brevity.Tests.Exception
{
    [TestFixture]
    public class Log
    {
        [Test]
        public void LogError()
        {
            log4net.Config.BasicConfigurator.Configure();

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