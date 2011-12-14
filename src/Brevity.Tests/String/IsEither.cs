// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class IsEither
    {
        [Test]
        public void Hit()
        {
            "foo".IsEither("foo", "bar").ShouldBe(true);
        }

        public void Miss()
        {
            "baz".IsEither("foo", "bar").ShouldBe(false);
        }

        public void Null_when_argument_is_not_null()
        {
            string s = null;
            s.IsEither("foo", "bar").ShouldBe(false);
        }

        [Test]
        public void Null_when_argument_is_null()
        {
            string s = null;
            s.IsEither(null, "foo").ShouldBe(true);
        }

        [Test]
        public void Empty_string_when_argument_is_not_empty_string()
        {
            string.Empty.IsEither("foo", "bar").ShouldBe(false);
        }

        [Test]
        public void Empty_string_when_argument_is_empty_string()
        {
            string.Empty.IsEither(string.Empty, "foo").ShouldBe(true);
        }

        [Test]
        public void No_args_should_throw()
        {
            Assert.Throws<ArgumentException>(() => "foo".IsEither());
        }

    }
}

// ReSharper restore InconsistentNaming