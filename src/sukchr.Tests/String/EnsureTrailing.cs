// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class EnsureTrailing
    {
        [Test]
        public void Given_string_does_not_end_with_argument_trailing_is_added()
        {
            "http://google.com".EnsureTrailing("/").ShouldBe("http://google.com/");
        }

        [Test]
        public void Given_string_ends_with_argument_trailing_is_not_added()
        {
            "http://google.com/".EnsureTrailing("/").ShouldBe("http://google.com/");
        }

        [Test]
        [Ignore("Not supported yet")]
        public void Given_string_partially_ends_with_argument_trailing_is_ensured()
        {
            "http://google.com/".EnsureTrailing("//").ShouldBe("http://google.com//");
        }
    }
}

// ReSharper restore InconsistentNaming