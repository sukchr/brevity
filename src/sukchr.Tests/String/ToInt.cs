using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class ToInt
    {
        [Test]
        public void ValidInt()
        {
            "123".ToInt().ShouldBe(123);
        }

        [Test]
        public void InvalidIntDefaultsToZero()
        {
            "abc".ToInt().ShouldBe(0);
        }

        [Test]
        public void ValidIntWithDefault()
        {
            "123".ToInt(0).ShouldBe(123);
        }

        [Test]
        public void InvalidIntWithDefault()
        {
            "abc".ToInt(123).ShouldBe(123);
        }
    }
}