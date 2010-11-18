// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class OpenEmbeddedResource
    {
        [Test]
        public void Open_existing_resource()
        {
            "sukchr.Tests.Data.embedded-resource.txt".OpenEmbeddedResource().ToText().ShouldBe("foo");
        }

        [Test]
        public void Open_non_existing_resource()
        {
            "foo".OpenEmbeddedResource().ShouldBe(null);
        }
    }
}

// ReSharper restore InconsistentNaming