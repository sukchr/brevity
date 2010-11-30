// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.Examples
{
    [TestFixture]
    public class WrapJoinFormat
    {
        [Test]
        public void Test()
        {
            string[] tests = {"foo", "bar"};
            "where X in ({0})".FormatWith(tests.WrapWith("'").Join()).ShouldBe("where X in ('foo', 'bar')");
        }
    }
}

// ReSharper restore InconsistentNaming