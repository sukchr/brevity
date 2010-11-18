// ReSharper disable InconsistentNaming
using System.IO;
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.Stream
{
    [TestFixture]
    public class ToText
    {
        [Test]
        public void Open_stream_with_content()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream) { AutoFlush = true };
            writer.Write("foo");
            memoryStream.Position = 0;
            memoryStream.ToText().ShouldBe("foo");
        }

        [Test]
        public void Open_empty_stream()
        {
            var memoryStream = new MemoryStream();
            memoryStream.ToText().ShouldBe(string.Empty);
        }
    }
}

// ReSharper restore InconsistentNaming