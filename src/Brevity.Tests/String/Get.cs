// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.String
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