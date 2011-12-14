// ReSharper disable InconsistentNaming

using NUnit.Framework;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Write
    {
        [Test]
        public void Simple_write()
        {
            "hello world".Write();
        }

        [Test]
        public void With_format_string()
        {
            "hello {0}".Write("world");
        }
    }
}

// ReSharper restore InconsistentNaming