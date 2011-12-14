// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.Stream
{
    [TestFixture]
    public class ToXml
    {
        [Test]
        public void Test()
        {
            var xml = "Brevity.Tests.Data.data.xml".OpenEmbeddedResource().ToXml();
            Assert.AreEqual(2, xml.FirstChild.ChildNodes.Count);
        }
    }
}

// ReSharper restore InconsistentNaming