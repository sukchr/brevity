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

        [Test]
        public void Join_with_custom_separator()
        {
            string[] values = { "foo", "bar" };
            values.Join(" ").ShouldBe("foo bar");
        }

        [Test]
        public void Join_with_null_values()
        {
            string[] values = { "foo", null, "bar" };
            values.Join(" ").ShouldBe("foo bar");
        }

        [Test]
        public void Join_with_empty_values()
        {
            string[] values = { "foo", string.Empty, "bar" };
            values.Join(" ").ShouldBe("foo bar");
        }
    }
}

// ReSharper restore InconsistentNaming