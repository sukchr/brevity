// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class ToXml
    {
        [Test]
        public void Test()
        {
            var xml = "<data><item/><item/></data>".ToXml();
            Assert.AreEqual(2, xml.FirstChild.ChildNodes.Count);
        }
    }
}

// ReSharper restore InconsistentNaming