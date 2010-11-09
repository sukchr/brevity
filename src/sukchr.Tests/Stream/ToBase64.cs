using NUnit.Framework;

namespace sukchr.Tests.Stream
{
    [TestFixture]
    public class FileTests
    {
        [Test]
        public void ConvertFileToBase64AndSave()
        {
            @"c:\temp\data.doc".Open().ToBase64().Save(@"c:\temp\0f4a0dfb-4e90-4307-acac-55829040138c_g9d8f176ddf7748818697cf380a26d243_gc49db88959374cb1a841b0b9653c25ca");
        }
    }
}
