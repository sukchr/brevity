// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class Mask
    {
        [Test]
        public void Given_no_mask_length_should_mask_half_the_input()
        {
            "secret".Mask().ShouldBe("sec***");
            "secret1".Mask().ShouldBe("sec****");
            string.Empty.Mask().ShouldBe(string.Empty);
            "f".Mask().ShouldBe("*");
            "fo".Mask().ShouldBe("f*");
        }

        [Test]
        public void Given_value_with_shorter_length_than_visibleLimit_outputs_the_entire_string()
        {
            "2412".Mask(6).ShouldBe("2412");
        }

        [Test]
        public void Given_value_with_same_length_as_visibleLimit_outputs_the_entire_string()
        {
            "241281".Mask(6).ShouldBe("241281");
        }

        [Test]
        public void Given_value_with_longer_length_than_visibleLimit_outputs_rest_of_string_as_asterisks()
        {
            "24128112345".Mask(6).ShouldBe("241281*****");
        }
    }
}

// ReSharper restore InconsistentNaming