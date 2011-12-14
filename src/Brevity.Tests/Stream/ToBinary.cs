// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.Stream
{
    [TestFixture]
    public class ToBinary
    {
        [Test]
        public void Test()
        {
            "Brevity.Tests.Data.data.xml".OpenEmbeddedResource().ToBinary();
        }
    }
}

// ReSharper restore InconsistentNaming