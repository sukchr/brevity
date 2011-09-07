// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace sukchr.Tests.Stream
{
    [TestFixture]
    public class ToXml
    {
        [Test]
        public void Test()
        {
            var xml = "sukchr.Tests.Data.data.xml".OpenEmbeddedResource().ToXml();
            Assert.AreEqual(2, xml.FirstChild.ChildNodes.Count);
        }
    }
}

// ReSharper restore InconsistentNaming