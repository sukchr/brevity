// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class IsNeither
    {
        [Test]
        public void Value_is_either()
        {
            "foo".IsNeither("foo", "bar").ShouldBe(false);
        }

        [Test]
        public void Value_is_neither()
        {
            "baz".IsNeither("foo", "bar").ShouldBe(true);
        }
    }
}

// ReSharper restore InconsistentNaming