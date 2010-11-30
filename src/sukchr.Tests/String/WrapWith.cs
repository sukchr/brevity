// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class WrapWith
    {
        [Test]
        public void Normal()
        {
            "foo".WrapWith("'").ShouldBe("'foo'");
        }

        [Test]
        public void Null_value_is_not_wrapped()
        {
            string s = null;
            s.WrapWith("'").ShouldBe(null);
        }

        [Test]
        public void Empty_string_is_wrapped()
        {
            string.Empty.WrapWith("'").ShouldBe("''");
        }

        [Test]
        public void Wrap_list()
        {
            string[] list = {"foo", "bar"};
            string[] expected = { "'foo'", "'bar'" };
            list.WrapWith("'").ShouldBe(expected);
        }

    }
}

// ReSharper restore InconsistentNaming