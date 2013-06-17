// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Truncate
    {
        [Test]
        public void Given_emptyString_returns_the_emptyString()
        {
            string.Empty.Truncate(7).ShouldBe(string.Empty);
        }

        [Test]
        public void Given_value_shorter_than_truncateLength_returns_the_entire_value()
        {
            "foobar".Truncate(7).ShouldBe("foobar");
        }

        [Test]
        public void Given_value_with_same_length_as_truncateLength_returns_the_entire_value()
        {
            "foobar".Truncate(6).ShouldBe("foobar");
        }

        [Test]
        public void Given_value_longer_than_truncateLength_truncates_the_value()
        {
            "foobar".Truncate(5).ShouldBe("fo...");
        }

        [Test]
        public void Given_truncateLength_shorter_than_truncateIndicator_throws_exception()
        {
            Assert.Throws<ArgumentException>(() => "foo".Truncate(2));
        }

		[Test]
		public void Given_truncateLength_shorter_than_truncateIndicator_doesnt_throw_exception_when_value_doesnt_need_truncating()
		{
			"f".Truncate(1).ShouldBe("f");
		}

        [Test]
        public void Given_negative_truncateLength_throws_exception()
        {
            Assert.Throws<ArgumentException>(() => "foo".Truncate(-1));
        }
    }
}

// ReSharper restore InconsistentNaming