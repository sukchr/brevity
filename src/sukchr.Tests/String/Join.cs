// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class Join
    {
        [Test]
        public void Join_multiple_values()
        {
            string[] values = {"foo", "bar"};
            values.Join().ShouldBe("foo, bar");
        }

        [Test]
        public void Join_no_values()
        {
            string[] values = {};
            values.Join().ShouldBe(string.Empty);
        }

        [Test]
        public void Join_single_values()
        {
            string[] values = {"foo"};
            values.Join().ShouldBe("foo");
        }
    }
}

// ReSharper restore InconsistentNaming