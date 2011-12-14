// ReSharper disable InconsistentNaming

using NUnit.Framework;
using Shouldly;

namespace Brevity.Tests.String
{
    [TestFixture]
    public class Remove
    {
        [Test]
        public void Given_value_with_no_occurences_of_the_string_to_remove_ouputs_the_string()
        {
            "123".Remove("*").ShouldBe("123");
        }

        [Test]
        public void Given_value_with_single_occurence_of_the_string_to_removes_the_value()
        {
            "12*3".Remove("*").ShouldBe("123");
        }

        [Test]
        public void Given_value_with_multiple_occurences_of_the_string_to_removes_the_values()
        {
            "*12*3*".Remove("*").ShouldBe("123");
        }

        [Test]
        public void Given_value_with_multiple_strings_to_remove_removes_the_values()
        {
            "1%2%3*".Remove("*", "%").ShouldBe("123");
        }
    }
}

// ReSharper restore InconsistentNaming