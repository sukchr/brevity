// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace sukchr.Tests.Stream
{
    [TestFixture]
    public class ToBinary
    {
        [Test]
        public void Test()
        {
            "sukchr.Tests.Data.data.xml".Open().ToBinary();
        }
    }
}

// ReSharper restore InconsistentNaming