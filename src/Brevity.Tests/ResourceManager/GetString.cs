// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.ResourceManager
{
    [TestFixture]
    public class GetString
    {
        [Test]
        public void Getting_resource_that_exists_should_format_resource()
        {
            var message = Resource.ResourceManager.GetString("user_not_found", "foo");
            message.ShouldBe("User with username foo not found.");
        }

        [Test]
        public void Getting_resource_that_doesnt_exists_should_not_throw_exception()
        {
            var message = Resource.ResourceManager.GetString("foo", "foo");
            message.ShouldBe(null);
        }
    }
}

// ReSharper restore InconsistentNaming