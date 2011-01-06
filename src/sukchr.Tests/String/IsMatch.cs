// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class IsMatch
    {
        [Test]
        public void ShouldMatch()
        {
            "1".IsMatch(@"\d+\w?").ShouldBe(true);
            "1A".IsMatch(@"\d+\w?").ShouldBe(true);
            "123A".IsMatch(@"\d+\w?").ShouldBe(true);
            "123".IsMatch(@"\d+\w?").ShouldBe(true);
        }

        [Test]
        public void ShouldNotMatch()
        {
            "A".IsMatch(@"\d+\w?").ShouldBe(false);
            "AB".IsMatch(@"\d+\w?").ShouldBe(false);
            "123AB".IsMatch(@"^\d+\w?$").ShouldBe(false);
        }
    }
}

// ReSharper restore InconsistentNaming