// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class Get
    {
        [Test]
        public void Test()
        {
            "http://google.com".Get().Write();
        }
    }
}

// ReSharper restore InconsistentNaming