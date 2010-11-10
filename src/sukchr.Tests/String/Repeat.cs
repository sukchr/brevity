// ReSharper disable InconsistentNaming
using NUnit.Framework;
using Shouldly;

namespace sukchr.Tests.String
{
    [TestFixture]
    public class Repeat
    {
        [Test]
        public void Single_repeat_should_output_the_string_as_is()
        {
            "*".Repeat(1).ShouldBe("*");
        }

        [Test]
        public void Multiple_repeat_should_output_the_string_the_given_number_of_times()
        {
            "*".Repeat(3).ShouldBe("***");
        }

        [Test]
        public void Zero_repeat_should_ouput_emptyString()
        {
            "*".Repeat(0).ShouldBe(string.Empty);
        }

        [Test]
        public void Negative_repeat_should_ouput_emptyString()
        {
            "*".Repeat(-1).ShouldBe(string.Empty);
        }
    }
}

// ReSharper restore InconsistentNaming