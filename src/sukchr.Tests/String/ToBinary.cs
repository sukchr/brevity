// ReSharper disable InconsistentNaming
using System.Text;
using NUnit.Framework;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class ToBinary
    {
        [Test]
        public void Test()
        {
            var binary = "hello world".ToBinary();
            Assert.AreEqual(Encoding.Default.GetBytes("hello world"), binary);
        }
    }
}

// ReSharper restore InconsistentNaming