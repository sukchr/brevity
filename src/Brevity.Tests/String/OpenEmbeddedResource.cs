// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class OpenEmbeddedResource
    {
        [Test]
        public void Open_existing_resource()
        {
            "Brevity.Tests.Data.embedded-resource.txt".OpenEmbeddedResource().ToText().ShouldBe("foo");
        }

        [Test]
        public void Open_non_existing_resource()
        {
            "foo".OpenEmbeddedResource().ShouldBe(null);
        }
    }
}

// ReSharper restore InconsistentNaming